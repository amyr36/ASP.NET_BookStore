<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Pages.User.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h1>📚 به فروشگاه کتاب افکار خوش آمدید</h1>
        <p style="margin: 20px 0; font-size: 16px; color: #555;">
            بهترین کتاب‌ها را با بهترین قیمت پیدا کنید
        </p>
        
        <asp:PlaceHolder ID="pnlWelcome" runat="server" Visible="false">
            <div class="alert alert-success">
                ✅ خوش آمدید <asp:Literal ID="ltrName" runat="server" />!
            </div>
        </asp:PlaceHolder>
        
        <div style="margin-top: 20px;">
            <a href="Products.aspx" class="btn btn-primary">مشاهده کتاب‌ها</a>
        </div>
    </div>
</asp:Content>
