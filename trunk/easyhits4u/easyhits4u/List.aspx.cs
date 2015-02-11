using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using easyhits4u.Domain;
using easyhits4u.Helper;
using easyhits4u.Code;

namespace easyhits4u
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptList.ItemDataBound += new RepeaterItemEventHandler(rptList_ItemDataBound);
            rptList.ItemCommand += new RepeaterCommandEventHandler(rptList_ItemCommand);
            if (!Page.IsPostBack)
            {
                LoadData();

                IList<Easyhits4uType> lst = DomainManager.GetAll<Easyhits4uType>();
                if (lst != null)
                    lst = lst.OrderBy(p => p.Easyhits4uTypeName).ToList();

                rptCategories.DataSource = lst;
                rptCategories.DataBind();
            }
        }

        void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Easyhits4u hit = DomainManager.GetObject<Easyhits4u>(id);
                if (hit != null)
                {
                    DomainManager.Delete(hit);
                    lblMsg.Text = "Xóa thành công";
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            IList<Easyhits4u> lst = HelperData.GetAllEasyhits4u();

            string check = Request.QueryString["check"] ?? string.Empty;
            string type = Request.QueryString["type"] ?? string.Empty;

            if (check == "0")
                lst = lst.Where(p => !p.IsApproved).ToList();
            else if (check == "1")
                lst = lst.Where(p => p.IsApproved).ToList();

            if (!string.IsNullOrEmpty(type))
                lst = lst.Where(p => p.Easyhits4uType != null && p.Easyhits4uType.Id.ToString() == type).ToList();

            if (lst != null)
            {
                lst = lst.OrderBy(p => p.Length).ToList();
                litTotal.Text = lst.Count.ToString("N0");
            }

            rptList.DataSource = lst;
            rptList.DataBind();
        }

        void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Easyhits4u data = e.Item.DataItem as Easyhits4u;
                Image imgPhoto = e.Item.FindControl("imgPhoto") as Image;
                if (imgPhoto != null & data.Image != null)
                {
                    string base64String = Convert.ToBase64String(data.Image, 0, data.Image.Length);
                    imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
                }

                DropDownList ddlType = e.Item.FindControl("ddlType") as DropDownList;
                if (ddlType != null)
                {
                    IList<Easyhits4uType> lst = DomainManager.GetList<Easyhits4uType>();
                    if (lst != null)
                        lst = lst.OrderBy(p => p.Easyhits4uTypeName).ToList();

                    ddlType.DataSource = lst;
                    ddlType.DataTextField = "Easyhits4uTypeName";
                    ddlType.DataValueField = "Id";
                    ddlType.DataBind();

                    ddlType.Items.Insert(0, new ListItem("None", ""));
                    if (data.Easyhits4uType != null)
                    {
                        ListItem item = ddlType.Items.FindByValue(data.Easyhits4uType.Id.ToString());
                        if (item != null)
                            item.Selected = true;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string kw = txtKeyword.Text;
            int length = int.Parse(kw);

            List<Easyhits4u> lst = HelperData.GeEasyhits4uByLength(length);
            if (lst != null)
                lst = lst.OrderBy(p => p.Length).ToList();

            rptList.DataSource = lst;
            rptList.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptList.Items)
            {
                HiddenField hfId = item.FindControl("hfId") as HiddenField;
                TextBox txtNote = item.FindControl("txtNote") as TextBox;
                DropDownList ddlType = item.FindControl("ddlType") as DropDownList;
                if (hfId != null)
                {
                    int id = int.Parse(hfId.Value);
                    Easyhits4u easyhits4u = DomainManager.GetObject<Easyhits4u>(id);
                    if (easyhits4u != null)
                    {
                        if (ddlType != null && !string.IsNullOrEmpty(ddlType.SelectedValue))
                        {
                            Easyhits4uType type = DomainManager.GetObject<Easyhits4uType>(int.Parse(ddlType.SelectedValue));
                            easyhits4u.Easyhits4uType = type;
                            if (type != null)
                                easyhits4u.IsApproved = true;

                            if (txtNote != null)
                                easyhits4u.Note = txtNote.Text.Trim();

                            DomainManager.Update(easyhits4u);
                        }
                    }
                }
            }

            Response.Redirect(Request.RawUrl, true);
        }
    }
}