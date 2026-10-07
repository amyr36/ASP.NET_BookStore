<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApp.Pages.User.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

    <h2>اطلاعات کاربری</h2>

    <asp:Label runat="server" Text="نام کاربری"></asp:Label>
    <asp:TextBox
        ID="txtUserName"
        runat="server"
        CssClass="form-control">
    </asp:TextBox>

    <br />

    <asp:Label runat="server" Text="شماره تماس"></asp:Label>
    <asp:TextBox
        ID="txtPhone"
        runat="server"
        CssClass="form-control">
    </asp:TextBox>

    <br />

    <asp:Label runat="server" Text="آدرس"></asp:Label>

    <asp:TextBox
        ID="txtAddress"
        runat="server"
        CssClass="form-control"
        TextMode="MultiLine"
        Rows="5">
    </asp:TextBox>

    <br />

    <asp:Button
        ID="btnSave"
        runat="server"
        Text="ذخیره اطلاعات"
        CssClass="btn btn-success"
        OnClick="btnSave_Click" />

        <br />

        <a href="MyOrders.aspx">سفارشات من</a>

        <asp:Label ID="lb_status" runat="server" CssClass="alert-success"></asp:Label>

     </div>

</asp:Content>
