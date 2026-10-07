using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WebApp.App_Code;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.Admin
{
    public partial class UsersManagement : System.Web.UI.Page
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
                LoadUsers();
        }

        private void LoadUsers(string search = "")
        {
            string query = @"
                SELECT UserID,
                       UserName,
                       Address,
                       PhoneNumber,
                       RoleID,
                       RegisterationAt,
                       IsActive
                FROM Users
                WHERE UserName LIKE @Search
                ORDER BY UserID DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Search",
                    "%" + search + "%")
            };

            DataSet ds = new DataSet();

            SqlHelper.FillDataset(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                ds,
                new string[] { "Users" },
                parameters);

            gv_users.DataSource = ds.Tables[0];
            gv_users.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers(txtSearch.Text.Trim());
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadUsers();
        }

        protected void gv_users_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            int user_id =
                Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditUser":

                    Response.Redirect(
                        $"EditUser.aspx?UserID={user_id}");
                    break;

                case "DisableUser":

                    DisableUser(user_id);
                    break;
            }
        }

        private void DisableUser(int userId)
        {
            string query = @"
                UPDATE Users
                SET IsActive = 0
                WHERE UserID = @UserID";

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                new SqlParameter(
                    "@UserID",
                    userId));

            lb_status.Text =
                "کاربر غیرفعال شد.";

            LoadUsers();
        }
    }
}