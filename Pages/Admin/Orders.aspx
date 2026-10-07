<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="WebApp.Pages.Admin.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <h2>مدیریت سفارشات</h2>

        <br />

        <asp:GridView
            ID="gv_orders"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gv_orders_RowCommand" Width="616px">

            <Columns>

                <asp:BoundField
                    DataField="OrderID"
                    HeaderText="کد سفارش" />

                <asp:BoundField
                    DataField="UserName"
                    HeaderText="مشتری" />

                <asp:BoundField
                    DataField="TotalSales"
                    HeaderText="مبلغ"
                    DataFormatString="{0:N0}" />

                <asp:BoundField
                    DataField="CreatedAt"
                    HeaderText="تاریخ سفارش"
                    DataFormatString="{0:yyyy/MM/dd}" />

                <asp:TemplateField HeaderText="جزئیات">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnDetails"
                            runat="server"
                            Text="مشاهده"
                            CommandName="Details"
                            CommandArgument='<%# Eval("OrderID") %>'
                            CssClass="btn btn-primary" />

                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField>

                    <ItemTemplate>

                        <asp:DropDownList
                            ID="ddlStatus"
                            runat="server"
                            CssClass="form-control"
                            SelectedValue='<%# Eval("Status") %>'>

                            <asp:ListItem>در حال پردازش</asp:ListItem>
                            <asp:ListItem>در حال آماده سازی</asp:ListItem>
                            <asp:ListItem>ارسال شده</asp:ListItem>
                            <asp:ListItem>تحویل شده</asp:ListItem>
                            <asp:ListItem>لغو شده</asp:ListItem>

                        </asp:DropDownList>

                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="ثبت وضعیت">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnApply"
                            runat="server"
                            Text="ثبت"
                            CommandName="ChangeStatus"
                            CommandArgument='<%# Eval("OrderID") %>'
                            CssClass="btn-success"
                            width="100"
                            height="40"/>

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />
        <br />
        <br />

        <asp:Label ID="lb_status" runat="server" CssClass="alert-success"></asp:Label>

        <asp:GridView
            ID="gv_details"
            runat="server"
            AutoGenerateColumns="True"
            CssClass="table table-striped"
            GridLines="None" Width="603px">

        </asp:GridView>

    </div>

</asp:Content>
