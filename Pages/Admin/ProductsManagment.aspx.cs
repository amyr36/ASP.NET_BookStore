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
    public partial class ProductsManagment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 13)
            {
                Response.Redirect("LoginAdmin.aspx");
                return;
            }

            if (!IsPostBack)
                LoadProducts();
        }

        private void LoadProducts()
        {
            string product_query = @"SELECT ProductID, ProductName, Price, Quantity, ImageURL
                                   FROM Products
                                   WHERE ProductID = ProductID
                                   ORDER BY ProductID DESC";

            string[] product_tables = new string[1];
            product_tables[0] = "Products";

            DataSet ds_product = new DataSet();

            SqlHelper.FillDataset(
                DBConfigs.ConnectionString,
                CommandType.Text,
                product_query,
                ds_product,
                product_tables
                );

            gv_producrts.DataSource = ds_product.Tables[0];
            gv_producrts.DataBind();
        }

        protected void gv_products_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int product_id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Delete":
                    UnavailableProduct(product_id);
                    break;

                case "Edit":
                    Response.Redirect($"EditProducts.aspx?ProductID={product_id}");
                    break;

            }

        }

        private void UnavailableProduct(int ProductID)
        {

            string unavailable_query = "UPDATE FROM Products SET Quantity=0 WHERE ProductID=@ProductID";

            SqlParameter[] unavailable_params = {
                new SqlParameter("@ProductID", ProductID)
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                unavailable_query,
                unavailable_params);

            lb_status.Text = "کالا با موفقیت غیر فعال شد!";

            LoadProducts();
        }   
    }  
}