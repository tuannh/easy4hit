using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using easyhits4u.Code;
using easyhits4u.Domain;
using easyhits4u.Helper;

namespace JSONApp
{
    public partial class hits4u : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string imgUrl = Request.QueryString["imgUrl"] ?? string.Empty;
                imgUrl = Server.UrlDecode(imgUrl);

                if (!string.IsNullOrEmpty(imgUrl))
                {
                    string train = Request.QueryString["train"] ?? string.Empty;
                    if (train == "1")
                        Helper.TrainData(imgUrl);

                    ProcessRequest(HttpContext.Current, imgUrl);
                }
            }
        }

        public void ProcessRequest(HttpContext context, string imgUrl)
        {
            string callback = context.Request.QueryString["callback"] ?? string.Empty;
            string clickText = string.Empty;

            int pos = ProcessData(imgUrl, out clickText);
            string data =  callback + "({\"ImageIndex\": " + pos + ", \"ClickText\": \"" + clickText + "\"})";

            divContainer.Visible = false;
            context.Response.Clear();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(data);
        }

        protected int ProcessData(string url, out string clickText)
        {
            clickText = string.Empty;
            Bitmap[] data = Helper.GetBitmapData(url);
            
            clickText = "test message";
            // singlge check
            if (data.Length == 1)
            {
                #region Number captcha validate
               
                return -100;

                #endregion
            }

            List<DataEntry> lstEntry = new List<DataEntry>();
            int index = 0;
            foreach (Bitmap bm in data)
            {
                byte[] byteData = Helper.GetBytes(bm);
                Easyhits4u hit4u = Helper.GetBitmapFromDB(bm, byteData.Length);
                if (hit4u == null)
                    hit4u = new Easyhits4u();

                DataEntry entry = new DataEntry();
                entry.Index = index++;
                entry.Data = bm;
                entry.Easyhits4u = hit4u;
                lstEntry.Add(entry);
            }

            if (lstEntry != null)
            {
                Easyhits4uType type = Helper.GetTheSameType(lstEntry);
                if (type != null)
                {
                    DataEntry entry = lstEntry.Where(p => p.Easyhits4u.Easyhits4uType != null && p.Easyhits4u.Easyhits4uType.Id == type.Id)
                                              .FirstOrDefault();
                    if (entry != null)
                        return entry.Index;
                }
            }

            return -1;
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string tmp = string.Empty;
            int pos = ProcessData(txtUrl.Text.Trim(), out tmp);
            Response.Write(pos.ToString());
        }

        protected void btnSlitter_Click(object sender, EventArgs e)
        {
            Helper.TrainData(txtUrl.Text.Trim());
        }
    }
}
