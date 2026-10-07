using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages
{
    public partial class General : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                pnlAuth.Visible = true;
                pnlGuest.Visible = false;

                if (Session["UserName"] != null)
                {
                    ltrUser.Text = Session["UserName"].ToString();
                }
                else
                {
                    ltrUser.Text = Context.User.Identity.Name;
                }

                if (Session["RoleID"] != null && Convert.ToInt32(Session["RoleID"]) == 13)
                {
                    pnlAdmin.Visible = true;
                }
                else
                {
                    pnlAdmin.Visible = false;
                }
            }
            else
            {
                pnlAuth.Visible = false;
                pnlGuest.Visible = true;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Pages/User/Login.aspx");
        }
    }
}