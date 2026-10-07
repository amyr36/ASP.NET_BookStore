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
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/Pages/User/Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadOrders();
        }

        private int GetUserID()
        {
            return Convert.ToInt32(Session["UserID"]);
        }

        private void LoadOrders()
        {
            string query = @"
                SELECT
                    OrderID,
                    TotalSales,
                    Status,
                    CreatedAt
                FROM Orders
                WHERE CustomerID = @CustomerID
                ORDER BY OrderID DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@CustomerID",
                    GetUserID())
            };

            DataSet ds = new DataSet();

            string[] tables = new string[1];
            tables[0] = "Orders";

            SqlHelper.FillDataset(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                ds,
                tables,
                parameters);

            gv_orders.DataSource = ds.Tables[0];
            gv_orders.DataBind();
        }

        protected void gv_orders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Details")
                return;

            int orderId =
                Convert.ToInt32(
                    e.CommandArgument);

            ShowOrderDetails(orderId);
        }

        private void ShowOrderDetails(
            int orderId)
        {
            string checkQuery = @"
                SELECT OrderID
                FROM Orders
                WHERE OrderID = @OrderID
                AND CustomerID = @CustomerID";

            SqlDataReader dr =
                SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    checkQuery,
                    new SqlParameter("@OrderID", orderId),
                    new SqlParameter("@CustomerID", GetUserID()));

            if (!dr.Read())
            {
                return;
            }

            dr.Close();

            string query = @"
                SELECT
                    p.ProductName,
                    d.Quantity,
                    d.UnitPrice,
                    d.Quantity * d.UnitPrice AS Total
                FROM OrderDetails d
                JOIN Products p
                    ON d.ProductID = p.ProductID
                WHERE d.OrderID = @OrderID";

            DataSet ds = new DataSet();

            string[] tables = { "Details" };

            SqlHelper.FillDataset(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                ds,
                tables,
                new SqlParameter(
                    "@OrderID",
                    orderId));

            gv_details.DataSource = ds.Tables[0];
            gv_details.DataBind();
        }
    }
}