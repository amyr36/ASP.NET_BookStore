<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="AddProducts.aspx.cs" Inherits="WebApp.Pages.Admin.AddProducts" %>
<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="card">

        <h2> اضافه کردن کتاب</h2>

        <br />

        <asp:Image
            ID="imgProduct"
            runat="server"
            Width="130px"
            Height="150px" />

            <br />

            <asp:FileUpload
            ID="fuImage"
            runat="server" />

        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="fuImage" ErrorMessage="مقداری را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>

        <br />

        <asp:Label runat="server" Text="نام کتاب   "></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductName" ErrorMessage="مقداری را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox
            ID="txtProductName"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label runat="server" Text="نویسنده   "></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCreator" ErrorMessage="مقداری را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox
            ID="txtCreator"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label runat="server" Text="قیمت  "></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" ErrorMessage="مقداری را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox
            ID="txtPrice"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label runat="server" Text="موجودی  "></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQuantity" ErrorMessage="مقداری را وارد نمایید" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox
            ID="txtQuantity"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

        <br />

        <asp:Label
            runat="server"
            Text="دسته بندی">
        </asp:Label>

        <asp:DropDownList
            ID="ddlCategory"
            runat="server"
            CssClass="form-control">
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
            runat="server"
            cssclass="alert-success">
        </asp:Label>

    </div>

</asp:Content>
