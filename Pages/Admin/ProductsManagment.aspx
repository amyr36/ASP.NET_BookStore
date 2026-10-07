<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="ProductsManagment.aspx.cs" Inherits="WebApp.Pages.Admin.ProductsManagment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <h2>مدیریت محصولات</h2>

        <br />

        <asp:GridView
            ID="gv_producrts"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gv_products_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="ProductID"
                    HeaderText="کد محصول" />

                <asp:TemplateField HeaderText="تصویر">

                <ItemTemplate>

                    <asp:Image
                        ID="imgBook"
                        runat="server"
                        ImageUrl='<%# Eval("ImageURL") %>'
                        Width="70px"
                        Height="90px" />

                </ItemTemplate>

            </asp:TemplateField>

                <asp:BoundField
                    DataField="ProductName"
                    HeaderText="نام محصول" />

                <asp:BoundField
                    DataField="Price"
                    HeaderText="مبلغ"
                    DataFormatString="{0:N0}" />

                <asp:BoundField
                    DataField="Quantity"
                    HeaderText="تعداد" />

                <asp:TemplateField HeaderText="حذف">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnDelete"
                            runat="server"
                            Text="حذف"
                            CommandName="Delete"
                            CommandArgument='<%# Eval("ProductID") %>'
                            CssClass="btn-danger" />

                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="ویرایش">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnEdit"
                            runat="server"
                            Text="ویرایش"
                            CommandName="Edit"
                            CommandArgument='<%# Eval("ProductID") %>'
                            CssClass="btn-primary" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

    <div>

        <asp:Button
    ID="btnAddProduct"
    runat="server"
    Text="افزودن کتاب جدید"
    CssClass="btn btn-success"
    PostBackUrl="~/Pages/Admin/AddProducts.aspx" />

    </div>

    <div>
        <asp:Label ID="lb_status" CssClass="alert" runat="server"></asp:Label>
    </div>

</asp:Content>

