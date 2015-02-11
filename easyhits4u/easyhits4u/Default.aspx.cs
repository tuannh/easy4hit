using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using easyhits4u.Domain;
using easyhits4u.Helper;
using easyhits4u.Code;
using System.Drawing;

namespace easyhits4u
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCompare_Click(object sender, EventArgs e)
        {
            Easyhits4u hit1 = DomainManager.GetObject<Easyhits4u>(int.Parse(txtID1.Text.Trim()));
            Easyhits4u hit2 = DomainManager.GetObject<Easyhits4u>(int.Parse(txtID2.Text.Trim()));

            Bitmap bm1 = easyhits4u.Code.Helper.GetBitmap(hit1.Image);
            Bitmap bm2 = easyhits4u.Code.Helper.GetBitmap(hit2.Image);

            int pos = 0;
            bool result = easyhits4u.Code.Helper.BitmapContains(bm1, bm2, out pos);
            Response.Write(result.ToString());

        }
    }
}