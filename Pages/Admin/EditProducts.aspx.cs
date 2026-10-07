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
    public partial class EditProducts : System.Web.UI.Page
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
            {
                LoadCategories();
                LoadProduct();
            }
        }

        private int GetProductID()
        {
            return Convert.ToInt32(
                Request.QueryString["ProductID"]);
        }

        private void LoadProduct()
        {
            string query = @"
                SELECT ProductName,
                       Creator,
                       Price,
                       Quantity,
                       CategoryID,
                       ImageURL
                FROM Products
                WHERE ProductID = @ProductID";

            SqlDataReader dr =
                SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query,
                    new SqlParameter(
                        "@ProductID",
                        GetProductID()));

            if (dr.Read())
            {
                txtProductName.Text =
                    dr["ProductName"].ToString();

                txtCreator.Text =
                    dr["Creator"].ToString();

                txtPrice.Text =
                    dr["Price"].ToString();

                txtQuantity.Text =
                    dr["Quantity"].ToString();

                imgProduct.ImageUrl = dr["ImageURL"].ToString();

                ddlCategory.SelectedValue = dr["CategoryID"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string imageUrl = imgProduct.ImageUrl;

            if (fuImage.HasFile)
            {
                imageUrl = ImageManager.SaveImage(
                    fuImage,
                    "~/Content/Images/Books/");
            }

            string query = @"
                UPDATE Products
                SET ProductName = @ProductName,
                    Creator = @Creator,
                    Price = @Price,
                    Quantity = @Quantity,
                    CategoryID = @CategoryID,
                    ImageURL = @ImageURL
                WHERE ProductID = @ProductID";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@ProductID",
                    GetProductID()),

                new SqlParameter(
                    "@ProductName",
                    txtProductName.Text.Trim()),

                new SqlParameter(
                    "@Creator",
                    txtCreator.Text.Trim()),

                new SqlParameter(
                    "@Price",
                    Convert.ToDecimal(txtPrice.Text)),

                new SqlParameter(
                    "@Quantity",
                    Convert.ToInt32(txtQuantity.Text)),

               new SqlParameter(
                    "@CategoryID",
                     Convert.ToInt32(ddlCategory.SelectedValue)),

               new SqlParameter("@ImageURL", imageUrl)
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            lb_status.Text =
                "اطلاعات کتاب با موفقیت ذخیره شد.";

            Response.Redirect("/Pages/Admin/ProductsManagment.aspx");
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