<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WebApp.Pages.User.Products" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="card">

        <h2>لیست کتاب ها <img src="../../Content/Images/read-book-icon.png" width="30"/></h2>

        <div class="row">
            <div class="col-md-8">
                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="نام کتاب یا نویسنده">
                </asp:TextBox>
            </div>

            <div class="col-md-4">
                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="جستجو"
                    CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />

                <asp:Button
                    ID="btnClear"
                    runat="server"
                    Text="نمایش همه"
                    CssClass="btn btn-secondary"
                    OnClick="btnClear_Click" />
            </div>
        </div>

        <asp:GridView
            ID="gv_books"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gvBooks_RowCommand">

            <Columns>

                <asp:TemplateField HeaderText="تصویر">

                    <ItemTemplate>

                        <asp:Image
                            ID="imgBook"
                            runat="server"
                            ImageUrl='<%# Eval("ImageURL") %>'
                            Width="80px"
                            Height="100px" />

                    </ItemTemplate>

                </asp:TemplateField>

                <asp:BoundField
                    DataField="ProductName"
                    HeaderText="نام کتاب" />

                <asp:BoundField
                    DataField="Creator"
                    HeaderText="نویسنده" />

                <asp:BoundField
                    DataField="Price"
                    HeaderText="قیمت"
                    DataFormatString="{0:N0}" />

                <asp:TemplateField HeaderText="خرید">

                    <ItemTemplate>

                        <asp:ImageButton
                        ID="btnAddToCart"
                        runat="server"
                        ImageUrl="~/Content/Images/shopping-cart.png"
                        CommandName="AddToCart"
                        CssClass="btn-success"
                        CommandArgument='<%# Eval("ProductID") %>'
                        width="20" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

        <asp:Label
            ID="status_lb"
            runat="server"
            CssClass="alert-danger" >
        </asp:Label>

    </div>

</asp:Content>
