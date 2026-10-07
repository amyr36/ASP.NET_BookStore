using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WebApp.App_Code;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.User
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();

            if (!IsPostBack)
                LoadUserInfo();
        }

        private void CheckLogin()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        private int GetUserID()
        {
            return Convert.ToInt32(Session["UserID"]);
        }

        private void LoadUserInfo()
        {
            string query = @"
                SELECT UserName,
                       PhoneNumber,
                       Address
                FROM Users
                WHERE UserID = @UserID";

            SqlDataReader dr =
                SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query,
                    new SqlParameter(
                        "@UserID",
                        GetUserID()));

            if (dr.Read())
            {
                txtUserName.Text =
                    dr["UserName"].ToString();

                txtPhone.Text =
                    dr["PhoneNumber"].ToString();

                txtAddress.Text =
                    dr["Address"].ToString();
            }

            dr.Close();
        }

        protected void btnSave_Click(
            object sender,
            EventArgs e)
        {
            string query = @"
                UPDATE Users
                SET UserName = @UserName,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address
                WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@UserID",
                    GetUserID()),

                new SqlParameter(
                    "@UserName",
                    txtUserName.Text.Trim()),

                new SqlParameter(
                    "@PhoneNumber",
                    txtPhone.Text.Trim()),

                new SqlParameter(
                    "@Address",
                    txtAddress.Text.Trim())
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            Session["UserName"] =
                txtUserName.Text.Trim();

            lb_status.Text =
                "اطلاعات با موفقیت ذخیره شد.";
        }
    }
}