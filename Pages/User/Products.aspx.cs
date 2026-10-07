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

namespace WebApp.Pages.User
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadBooks();
        }


        private void CheckLogin()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/User/Login.aspx");
                return;
            }
        }

        private int GetUserID()
        {
            return Convert.ToInt32(Session["UserID"]);
        }

        private void LoadBooks(string search = "")
        {
            string books_query = @"SELECT ProductID, ProductName, Creator, Price, ImageURL
                                   FROM Products
                                   WHERE Quantity > 0";

            SqlParameter[] books_params = null;

            if (!string.IsNullOrEmpty(search))
            {
                books_query += " AND (ProductName LIKE @Search OR Creator LIKE @Search)";

                books_params = new SqlParameter[]{
                    new SqlParameter("@Search", "%" + search + "%")
                };
            }

            DataSet ds_books = new DataSet();

            string[] books_table = new string[1];
            books_table[0] = "Products";

            SqlHelper.FillDataset(DBConfigs.ConnectionString,
                CommandType.Text,
                books_query,
                ds_books,
                books_table,
                books_params);

            gv_books.DataSource = ds_books.Tables[0];
            gv_books.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search_text = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search_text))
            {
                status_lb.Text = "برای جست و جو مقداری را وارد نمایید!";
                return;
            }

            LoadBooks(search_text);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            LoadBooks();
        }

        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CheckLogin();

            if (e.CommandName != "AddToCart")
                return;

            int product_id = Convert.ToInt32(e.CommandArgument);

            string check_query = @"SELECT CartID
                                   FROM ShoppingCart
                                   WHERE CustomerID = @CustomerID
                                   AND ProductID = @ProductID";

            SqlParameter[] parametes = {
                new SqlParameter("@CustomerID", GetUserID()),
                new SqlParameter("@ProductID", product_id)
            };

            SqlDataReader dr_check_cartID = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                check_query,
                parametes);

            if(dr_check_cartID.Read())
            {
                string update_query = @"UPDATE ShoppingCart
                                        SET Quantity = Quantity + 1
                                        WHERE CustomerID = @CustomerID
                                        AND ProductID = @ProductID";

                SqlHelper.ExecuteNonQuery(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    update_query,
                    parametes);
            }

            else
            {
                string insert_query = @"INSERT INTO ShoppingCart
                                        (CustomerID, ProductID, Quantity)
                                        VALUES
                                        (@CustomerID, @ProductID, 1)";

                SqlHelper.ExecuteNonQuery(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    insert_query,
                    parametes);
            }

            status_lb.Text = "کتاب با موفقیت به سبد خرید اضافه شد!";

        }

    }
}