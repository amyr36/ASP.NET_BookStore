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
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 13)
            {
                Response.Redirect("LoginAdmin.aspx");
                return;
            }

            if (!IsPostBack)
               LoadOrders();
        }

        private void LoadOrders()
        {
            string order_query = @"SELECT o.OrderID, u.UserName, o.TotalSales, o.CreatedAt, o.Status
                                   FROM Orders o
                                   JOIN Users u
                                   ON o.CustomerID = u.UserID
                                   ORDER BY o.OrderID DESC";

            string[] order_tables = new string[1];
            order_tables[0] = "Orders";

            DataSet ds_order = new DataSet();

            SqlHelper.FillDataset(
                DBConfigs.ConnectionString,
                CommandType.Text,
                order_query,
                ds_order,
                order_tables);

            gv_orders.DataSource = ds_order.Tables[0];
            gv_orders.DataBind();
        }

        protected void gv_orders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int order_id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Details")
            {
                string query = @"SELECT p.ProductName,
                                d.Quantity,
                                d.UnitPrice,
                                d.Quantity * d.UnitPrice AS Total
                         FROM OrderDetails d
                         JOIN Products p
                         ON d.ProductID = p.ProductID
                         WHERE d.OrderID = @OrderID";

                SqlParameter[] order_params = {
            new SqlParameter("@OrderID", order_id)
        };

                DataSet ds = new DataSet();

                string[] detail_tables = { "Details" };

                SqlHelper.FillDataset(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query,
                    ds,
                    detail_tables,
                    order_params);

                gv_details.DataSource = ds.Tables[0];
                gv_details.DataBind();

                return;
            }

            if (e.CommandName == "ChangeStatus")
            {
                string query_status = @"UPDATE Orders
                                SET Status = @Status
                                WHERE OrderID = @OrderID";

                SqlParameter[] status_params = {
                    new SqlParameter("@OrderID", order_id),
                    new SqlParameter("@Status", "ارسال شده")
                };

                SqlHelper.ExecuteNonQuery(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query_status,
                    status_params);

                LoadOrders();

                lb_status.Text = "وضعیت سفارش با موفقیت تغییر کرد.";
            }
        }
    }
}