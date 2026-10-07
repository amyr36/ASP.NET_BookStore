using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using WebApp.App_Code;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Pages.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
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


        private void LoadCart()
        {
            string cart_query = @"SELECT s.CartID, s.ProductID, s.Quantity, 
                                  p.ProductName, p.Creator, p.Price, 
                                  (p.Price * s.Quantity) AS Total
                                  FROM ShoppingCart s
                                  JOIN Products p ON s.ProductID = p.ProductID
                                  WHERE s.CustomerID = @UserID";

            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", GetUserID())
                };

            string[] table_names = new string[1];
            table_names[0] = "ShoppingCart";

            DataSet ds = new DataSet();
            SqlHelper.FillDataset(DBConfigs.ConnectionString,
                CommandType.Text,
                cart_query,
                ds,
                table_names,
                parameters);

            gv_cart.DataSource = ds.Tables[0];
            gv_cart.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                total_lb.Text = CalculateTotalPrice().ToString("N0");
            }
            else
            {
                total_lb.Text = "0";
            }

        }


        private bool CheckQuantity(int CustomerID)
        {
            string check_query = @"SELECT 1
                             FROM ShoppingCart s
                             JOIN Products p
                             ON s.ProductID = p.ProductID
                             WHERE s.CustomerID = @CustomerID
                             AND s.Quantity > p.Quantity";

            SqlParameter[] customer_params = {
                    new SqlParameter("@CustomerID", GetUserID())
            };

            SqlDataReader dr = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                check_query,
                customer_params);

            bool quantity_ok = !dr.Read();

            return quantity_ok;
        }


        private decimal CalculateTotalPrice()
        {
            string total_price_query = @"SELECT SUM(p.Price * s.Quantity)
                                         FROM ShoppingCart s
                                         JOIN Products p
                                         ON s.ProductID = p.ProductID
                                         WHERE s.CustomerID = @UserID";

            SqlDataReader dr_total_price = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                total_price_query,
                new SqlParameter("@UserID", GetUserID()));

            decimal total_price = 0;

            if (dr_total_price.Read() && dr_total_price[0] != DBNull.Value)
            {
                total_price = Convert.ToDecimal(dr_total_price[0]);
            }

            decimal discount_percentage = GetDiscountPercentage();

            return total_price - (total_price * discount_percentage / 100);
        }

        
        protected void gv_cart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command_query = null;

            int cart_id = Convert.ToInt32(e.CommandArgument);


            switch (e.CommandName)
            {
                case "Increase":

                    command_query = @"UPDATE ShoppingCart
                                      SET Quantity = Quantity + 1
                                      WHERE CartID = @CartID
                                      AND Quantity < 11";
                    break;

                case "Decrease":

                    command_query = @"UPDATE ShoppingCart
                                      SET Quantity = Quantity - 1
                                      WHERE CartID = @CartID
                                      AND Quantity > 1";
                    break;

                case "DeleteItem":

                    command_query = @"DELETE FROM ShoppingCart
                                      WHERE CartID = @CartID";

                    status_lb.Text = "محصول با موفقیت حذف شد.";
                    break;

                default:
                    return;
            }


            SqlParameter[] parameters = {
                new SqlParameter("@CartID", cart_id)
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                command_query,
                parameters);

            LoadCart();
        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {

            CheckLogin();

            string error = CheckUserInfo(GetUserID());

            if (!string.IsNullOrEmpty(error))
            {
                status_lb.Text = error;
                return;
            }

            int user_id = GetUserID();

            if(!CheckQuantity(user_id))
            {
                status_lb.Text = "موجودی برخی محصولات کافی نیست. لطفاً سبد خرید را اصلاح کنید.";
                return;
            }

            decimal total_price = CalculateTotalPrice();

            if(total_price == 0)
            {
                status_lb.Text = "سبد خرید خالی است!";
                return;
            }


            object discount_id = DBNull.Value;

            if (Session["DiscountID"] != null)
                discount_id = Session["DiscountID"];

            string order_query = @"INSERT INTO Orders
                                  (CustomerID, DiscountID, TotalSales, CreatedAt)
                                  VALUES
                                  (@CustomerID, @DiscountID, @TotalSales, GETDATE())

                                  SELECT SCOPE_IDENTITY() AS OrderID";

            SqlParameter[] order_params = {
                    new SqlParameter("@CustomerID", user_id),
                    new SqlParameter("@DiscountID", discount_id),
                    new SqlParameter("@TotalSales", total_price)
            };

            SqlDataReader dr_order_id = SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    order_query,
                    order_params);

            if(!dr_order_id.Read())
            {
                status_lb.Text = "مشکلی پیس آمده است!";
                return;
            }


            int order_id = Convert.ToInt32(dr_order_id[0]);

            string detail_query = @"INSERT INTO OrderDetails
                                    (OrderID, ProductID, Quantity, UnitPrice)
                                    SELECT @OrderID, s.ProductID, s.Quantity, p.Price
                                    FROM ShoppingCart s
                                    JOIN Products p
                                    ON s.ProductID = p.ProductID
                                    WHERE s.CustomerID = @CustomerID";

            SqlParameter[] detail_params = {
                    new SqlParameter("@CustomerID", user_id),
                    new SqlParameter("@OrderID", order_id)
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                detail_query,
                detail_params);

            string update_quantity_query = @"UPDATE p
                                          SET p.Quantity = p.Quantity - s.Quantity
                                          FROM Products p
                                          JOIN ShoppingCart s
                                          ON p.ProductID = s.ProductID
                                          WHERE s.CustomerID = @CustomerID";

            SqlParameter[] update_quantity_params = {
                    new SqlParameter("@CustomerID", user_id)
            };

            SqlHelper.ExecuteNonQuery(
                DBConfigs.ConnectionString,
                CommandType.Text,
                update_quantity_query,
                update_quantity_params);


            string query = @"DELETE FROM ShoppingCart WHERE CustomerID = @UserID";

            SqlParameter[] parameters = {
                new SqlParameter("@UserID", user_id)
            };

            SqlHelper.ExecuteNonQuery(DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            status_lb.Text = "خرید با موفقیت ثبت شد.";

            LoadCart();

            if (Session["DiscountCode"] != null)
            {
                string inactive_query = @"UPDATE Discounts
                                          SET IsUsed = 1,
                                          UsedAt = GETDATE()
                                          WHERE Code = @Code";

                SqlParameter[] disc_params = {
                    new SqlParameter("@Code", Session["DiscountCode"].ToString())
                };

                SqlHelper.ExecuteNonQuery(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    inactive_query,
                    disc_params
                    );

                Session.Remove("DiscountCode");
                Session.Remove("DiscountID");
            }

        }


        protected void btnApplyDiscount_Click(object sender, EventArgs e)
        {

            CheckLogin();

            string query = @"SELECT DiscountID
                            FROM Discounts
                            WHERE Code = @Code
                            AND CustomerID = @CustomerID
                            AND IsUsed = 0
                            AND ExpiresAt > GETDATE()";

            SqlParameter[] parameters = {
                new SqlParameter("@Code", txtDiscountCode.Text.Trim()),
                new SqlParameter("@CustomerID", GetUserID())
             };

            SqlDataReader dr_discount_id = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                query,
                parameters);

            if (!dr_discount_id.Read())
            {
                status_lb.Text = "کد تخفیف معتبر نیست.";
                return ;
            }

            Session["DiscountCode"] = txtDiscountCode.Text.Trim();
            Session["DiscountID"] = Convert.ToInt32(dr_discount_id["DiscountID"]);

            status_lb.Text = "کد تخفیف با موفقیت اعمال شد.";

            LoadCart();
        }


        private decimal GetDiscountPercentage()
        {
            decimal percentage = 0;

            if (Session["DiscountCode"] == null)
                return 0;

            string discount_percentage_query = @"SELECT Percentage
                                                 FROM Discounts
                                                 WHERE Code = @Code";

            SqlParameter[] discount_code_params = {
                    new SqlParameter("@Code", Session["DiscountCode"].ToString())
                };

            SqlDataReader dr_percentage = SqlHelper.ExecuteReader(
                DBConfigs.ConnectionString,
                CommandType.Text,
                discount_percentage_query,
                discount_code_params);

            if (dr_percentage.Read())
                percentage = Convert.ToDecimal(dr_percentage[0]);

            return percentage;
        }


        private string CheckUserInfo(int user_Id)
        {
            string query = @"SELECT PhoneNumber,
                             Address
                             FROM Users
                             WHERE UserID = @UserID";

            SqlDataReader dr =
                SqlHelper.ExecuteReader(
                    DBConfigs.ConnectionString,
                    CommandType.Text,
                    query,
                    new SqlParameter("@UserID", user_Id));

            string message = "";

            if (dr.Read())
            {
                if (string.IsNullOrWhiteSpace(
                    dr["PhoneNumber"].ToString()))
                {
                    message += "شماره تلفن وارد نشده است.";
                }

                if (string.IsNullOrWhiteSpace(
                    dr["Address"].ToString()))
                {
                    message += "آدرس وارد نشده است.";
                }
            }

            return message;
        }
    }
}