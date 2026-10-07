<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="WebApp.Pages.User.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="card">

        <h2>سفارشات من</h2>

        <br />

        <asp:GridView
            ID="gv_orders"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gv_orders_RowCommand" Height="159px" Width="669px">

            <Columns>

                <asp:BoundField
                    DataField="OrderID"
                    HeaderText="شماره سفارش" />

                <asp:BoundField
                    DataField="TotalSales"
                    HeaderText="مبلغ" />

                <asp:BoundField
                    DataField="Status"
                    HeaderText="وضعیت" />

                <asp:BoundField
                    DataField="CreatedAt"
                    HeaderText="تاریخ سفارش"
                    DataFormatString="{0:yyyy/MM/dd}" />

                <asp:TemplateField HeaderText="عملیات">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnDetails"
                            runat="server"
                            Text="مشاهده جزئیات"
                            CssClass="btn btn-primary"
                            CommandName="Details"
                            CommandArgument='<%# Eval("OrderID") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

            <h4>جزئیات سفارش</h4>

            <asp:GridView
                ID="gv_details"
                runat="server"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped">

                <Columns>

                    <asp:BoundField
                        DataField="ProductName"
                        HeaderText="کتاب" />

                    <asp:BoundField
                        DataField="Quantity"
                        HeaderText="تعداد" />

                    <asp:BoundField
                        DataField="UnitPrice"
                        HeaderText="قیمت واحد" />

                    <asp:BoundField
                        DataField="Total"
                        HeaderText="جمع" />

                </Columns>

            </asp:GridView>

    </div>

</asp:Content>
