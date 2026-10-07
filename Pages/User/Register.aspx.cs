using System;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using WebApp.App_Code;
using System.Web.Security;
using System.Data;
using WebApp.Pages.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.User
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text.Trim();
            string password = textPassword.Text.Trim();
            string confirmPassword = textPassword2.Text.Trim();

            string CheckQuery = "SELECT 1 FROM Users WHERE UserName = @UserName";

            SqlParameter[] CheckUserName = {
                new SqlParameter("@UserName", username)
            };

            SqlDataReader dr_register = SqlHelper.ExecuteReader(DBConfigs.ConnectionString,
                 CommandType.Text, CheckQuery, CheckUserName);

            if(dr_register.HasRows)
            {
                lable_er.Text = "این کاربر قبلا ثبت نام کرده است!";
                return;
            }

            string hashedPassword = password;

            string RegisterQuery = @"INSERT INTO Users (UserName, Password, RoleID) 
                           VALUES (@UserName, @Password, 1)";

            SqlParameter[] AddUser = {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Password", hashedPassword)
            };

            int result = SqlHelper.ExecuteNonQuery(DBConfigs.ConnectionString,
                CommandType.Text, RegisterQuery, AddUser);

            if (result > 0)
            {
                lable_er.Text = "ثبت نام با موفقیت انجام شد!";
                btnRegister.Enabled = false;
                textUsername.Enabled = false;
                textPassword.Enabled = false;   
                textPassword2.Enabled = false;

                Response.Redirect("~/Pages/User/Login.aspx");
            }

            else
            {
                lable_er.Text = "!خطایی رخ داد";
            }
        }
    }
}