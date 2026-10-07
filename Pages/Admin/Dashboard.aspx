<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebApp.Pages.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">

        <h2>پنل مدیریت</h2>

        <hr />

        <div class="row">

            <div class="col-md-3">
                <div class="alert alert-primary">
                    تعداد کاربران:
                    <asp:Label ID="lblUsers" runat="server" />
                </div>
            </div>

            <div class="col-md-3">
                <div class="alert alert-success">
                    تعداد کتاب ها:
                    <asp:Label ID="lblProducts" runat="server" />
                </div>
            </div>

            <div class="col-md-3">
                <div class="alert alert-warning">
                    تعداد سفارشات:
                    <asp:Label ID="lblOrders" runat="server" />
                </div>
            </div>

            <div class="col-md-3">
                <div class="alert alert-danger">
                    فروش کل:
                    <asp:Label ID="lblSales" runat="server" />
                </div>
            </div>

        </div>

        <hr />

        <asp:Button
            ID="btnProducts"
            runat="server"
            Text="مدیریت کتاب ها"
            CssClass="btn btn-primary"
            PostBackUrl="ProductsManagment.aspx" />

        <asp:Button
            ID="btnOrders"
            runat="server"
            Text="مدیریت سفارشات"
            CssClass="btn btn-success"
            PostBackUrl="Orders.aspx" />

        <asp:Button
            ID="btnUsers"
            runat="server"
            Text="مدیریت کاربران"
            CssClass="btn btn-warning"
            PostBackUrl="~/Pages/Admin/UsersManagement.aspx" />

    </div>
</asp:Content>
