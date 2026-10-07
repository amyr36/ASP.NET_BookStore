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

namespace WebApp.Pages.Admin
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null ||
                Convert.ToInt32(Session["RoleID"]) != 13)
            {
                Response.Redirect("LoginAdmin.aspx");
                return;
            }

            if (!IsPostBack)
                LoadUser();
        }

        private int GetUserID()
        {
            return Convert.ToInt32(
                Request.QueryString["UserID"]);
        }

        private void LoadUser()
        {
            string query = @"
                SELECT UserName,
                       PhoneNumber,
                       Address,
                       RoleID,
                       IsActive
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
                txtUserName.Text = dr["UserName"].ToString();

                txtPhone.Text = dr["PhoneNumber"].ToString();

                txtAddress.Text = dr["Address"].ToString();

                ddlRole.SelectedValue = dr["RoleID"].ToString();

                ddlActive.SelectedValue = dr["IsActive"].ToString();
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
                    Address = @Address,
                    RoleID = @RoleID,
                    IsActive = @IsActive
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
                    txtAddress.Text.Trim()),

                new SqlParameter(
                    "@RoleID",
                    Convert.ToInt32(
                        ddlRole.SelectedValue)),

                new SqlParameter(
                    "@IsActive",
                    Convert.ToInt32(ddlActive.SelectedValue))
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            Response.Redirect(
                "UsersManagement.aspx");
        }
    }
}