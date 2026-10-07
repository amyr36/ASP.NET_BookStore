<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebApp.Pages.User.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <h2>سبد خرید <img src="../../Content/Images/shopping-cart.png"
            width="32"
            height="32"></h2>
        

        <asp:GridView ID="gv_cart"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gv_cart_RowCommand"
            EmptyDataText="سبد خرید شما خالی است">

            <Columns>

                <asp:BoundField DataField="ProductName" HeaderText="نام کتاب" />

                <asp:BoundField DataField="Creator" HeaderText="پدید آورنده" />

                <asp:BoundField DataField="Price"
                    HeaderText="قیمت"
                    DataFormatString="{0:N0}" />

                <asp:TemplateField HeaderText="تعداد">
                    <ItemTemplate>

                    <asp:ImageButton
                        ID="btnMinus"
                        runat="server"
                        ImageUrl="~/Content/Images/negative-symbol.png"
                        CommandName="Decrease"
                        CommandArgument='<%# Eval("CartID") %>'
                        Width="20px" />

                    <asp:Label
                        ID="lblQuantity"
                        runat="server"
                        Text='<%# Eval("Quantity") %>'
                        CssClass="mx-2">
                    </asp:Label>

                    <asp:ImageButton
                        ID="btnPlus"
                        runat="server"
                        ImageUrl="~/Content/Images/plus.png"
                        CommandName="Increase"
                        CommandArgument='<%# Eval("CartID") %>'
                        Width="20px" />

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Total"
                    HeaderText="جمع"
                    DataFormatString="{0:N0}" />

                <asp:TemplateField HeaderText="عملیات">
                    <ItemTemplate>    
                        <asp:ImageButton
                            ID="btnDelete"
                            runat="server"
                            ImageUrl="~/Content/Images/close.png"
                            CommandName="DeleteItem"
                            CommandArgument='<%# Eval("CartID") %>'
                            width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <div class="mt-3 text-end fw-bold">
            جمع کل:
            <asp:Label ID="total_lb" runat="server"></asp:Label>
            تومان
        </div>

        <asp:TextBox
            ID="txtDiscountCode"
            runat="server"
            CssClass="form-control"
            placeholder="کد تخفیف">
        </asp:TextBox>

        <br />

        <asp:Button
            ID="btnApplyDiscount"
            runat="server"
            Text="اعمال کد تخفیف"
            CssClass="btn btn-warning"
            OnClick="btnApplyDiscount_Click" />

        <br /><br />

        <asp:Label
            ID="discount_lb"
            runat="server">
        </asp:Label>

        <div class="mt-4">
            <asp:Button ID="btnCheckout"
                runat="server"
                Text="نهایی کردن خرید"
                CssClass="btn btn-success"
                OnClick="btnCheckout_Click" />

            <a href="Products.aspx" class="btn btn-secondary" >
                ادامه خرید
            </a>

        </div>

        <br />

        <asp:Label ID="status_lb"
            runat="server"
            CssClass="alert-danger">
        </asp:Label>

    </div>

</asp:Content>
