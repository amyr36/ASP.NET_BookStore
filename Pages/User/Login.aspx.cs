using System;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using WebApp.App_Code;
using WebApp.Pages.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.User
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.Identity.IsAuthenticated)
               Response.Redirect("Default.aspx");

        }
        protected void btnLogin_Click1(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            SqlParameter[] parameters = {
                new SqlParameter("@UserName", txtUsername.Text.Trim()),
                new SqlParameter("@Password", txtPassword.Text.Trim())
            };

            string query = "SELECT UserID, UserName, RoleID FROM Users WHERE UserName = @username AND Password = @password AND RoleID=1 AND IsActive=1";

            SqlDataReader dr_login = SqlHelper.ExecuteReader(DBConfigs.ConnectionString, CommandType.Text, query, parameters);

            if(dr_login.Read())
            {
                FormsAuthentication.SetAuthCookie(@username, false);

                Session["UserID"] = dr_login["UserID"];
                Session["UserName"] = dr_login["UserName"];
                Session["RoleID"] = dr_login["RoleID"];

                Response.Redirect("~/Pages/User/Default.aspx");
            }

            else
            {
                lb_error.Text = "نام کاربری یا رمز عبور اشتباه است";
            }

        }
    }
}