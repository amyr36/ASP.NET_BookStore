<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="WebApp.Pages.Admin.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="card">

        <h2>ویرایش کاربر</h2>

        <br />

        <asp:Label runat="server" Text="نام کاربری"></asp:Label>

        <asp:TextBox
            ID="txtUserName"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label runat="server" Text="تلفن"></asp:Label>

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
            TextMode="MultiLine"
            Rows="4"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label runat="server" Text="نقش"></asp:Label>

        <asp:DropDownList
            ID="ddlRole"
            runat="server"
            CssClass="form-control">

            <asp:ListItem Value="1">
                کاربر عادی
            </asp:ListItem>

            <asp:ListItem Value="13">
                ادمین
            </asp:ListItem>

        </asp:DropDownList>

        <br />

         <br />

        <asp:Label runat="server" Text="وضعیت"></asp:Label>

        <asp:DropDownList
            ID="ddlActive"
            runat="server"
            CssClass="form-control">

            <asp:ListItem Value="1">
                فعال
            </asp:ListItem>

            <asp:ListItem Value="0">
                غیر فعال
            </asp:ListItem>

        </asp:DropDownList>

        <br />

        <asp:Button
            ID="btnSave"
            runat="server"
            Text="ذخیره تغییرات"
            CssClass="btn btn-success"
            OnClick="btnSave_Click" />

        <br />
        <br />

        <asp:Label
            ID="lb_status"
            runat="server">
        </asp:Label>

    </div>

</asp:Content>
