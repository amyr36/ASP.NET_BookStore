using System;
using WebApp.App_Code;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 13)
            {
                Response.Redirect("LoginAdmin.aspx");
                return;
            }

            if (!IsPostBack)
                LoadStatistics();
        }


        private void LoadStatistics()
        {
            lblUsers.Text = GetCount("SELECT COUNT(*) FROM Users");

            lblProducts.Text = GetCount("SELECT SUM(Quantity) FROM Products");

            lblOrders.Text = GetCount("SELECT COUNT(*) FROM Orders");

            lblSales.Text = GetTotalSales().ToString("N0") + " تومان";
        }

        private string GetCount(string query)
        {
            SqlDataReader dr_statistics = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query);

            if(dr_statistics.Read())
            {
                string count = dr_statistics[0].ToString();
                return count;
            }

            return "0";
        }

        private decimal GetTotalSales()
        {
            string total_sales_query = "SELECT ISNULL(SUM(TotalSales),0) FROM Orders";

            SqlDataReader dr = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                total_sales_query);

            decimal total = 0;

            if (dr.Read())
                total = Convert.ToDecimal(dr[0]);

            return total;
        }
    }
}