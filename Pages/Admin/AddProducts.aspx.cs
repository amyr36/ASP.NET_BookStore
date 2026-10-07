using System;
using System.Data;
using System.Data.SqlClient;
using WebApp.App_Code;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.Admin
{
    public partial class AddProducts : System.Web.UI.Page
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
                LoadCategories();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string imageUrl = "";

            try
            {
                imageUrl = ImageManager.SaveImage(
                    fuImage,
                    "~/Content/Images/Books/");
            }
            catch (Exception ex)
            {
                lb_status.Text = ex.Message;
                return;
            }

            string query = @"
                INSERT INTO Products
                (
                    ProductName,
                    Creator,
                    Price,
                    Quantity,
                    CategoryID,
                    ImageURL
                )
                VALUES
                (
                    @ProductName,
                    @Creator,
                    @Price,
                    @Quantity,
                    @CategoryID,
                    @ImageURL
                )";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductName",
                    txtProductName.Text.Trim()),

                new SqlParameter("@Creator",
                    txtCreator.Text.Trim()),

                new SqlParameter("@Price",
                    Convert.ToDecimal(txtPrice.Text)),

                new SqlParameter("@Quantity",
                    Convert.ToInt32(txtQuantity.Text)),
                new SqlParameter("@CategoryID",
                    Convert.ToInt32(ddlCategory.SelectedValue)),

                new SqlParameter("@ImageURL",
                    imageUrl ?? "")
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            lb_status.Text =
                "کتاب با موفقیت ثبت شد.";

            txtProductName.Text = "";
            txtCreator.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";

            Response.Redirect("ProductsManagement.aspx");
        }


        private void LoadCategories()
        {
            string query =
                @"SELECT CategoryID,
                 CategoryType
          FROM Categories";

            SqlDataReader dr =
                SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query);

            ddlCategory.DataSource = dr;
            ddlCategory.DataTextField = "CategoryType";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
        }
    }
}