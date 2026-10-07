<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/General.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="WebApp.Pages.Admin.UsersManagement" %>
<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="card">

        <h2>مدیریت کاربران 👥</h2>

        <br />

        <asp:TextBox
            ID="txtSearch"
            runat="server"
            CssClass="form-control"
            placeholder="نام کاربری">
        </asp:TextBox>

        <br />

        <asp:Button
            ID="btnSearch"
            runat="server"
            Text="جستجو"
            CssClass="btn btn-primary"
            OnClick="btnSearch_Click" />

        <asp:Button
            ID="btnShowAll"
            runat="server"
            Text="نمایش همه"
            CssClass="btn btn-secondary"
            OnClick="btnShowAll_Click" />

        <br />
        <br />

        <asp:GridView
            ID="gv_users"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered"
            GridLines="None"
            OnRowCommand="gv_users_RowCommand" Width="910px" Height="381px">

            <Columns>

                <asp:BoundField
                    DataField="UserID"
                    HeaderText="کد" />

                <asp:BoundField
                    DataField="UserName"
                    HeaderText="نام کاربری" />

                <asp:TemplateField HeaderText="نقش">
                    <ItemTemplate>
                         <%# Convert.ToInt32(Eval("RoleID")) == 13 ? "ادمین" : "کاربر" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField
                    DataField="Address"
                    HeaderText="نشانی" />

                <asp:BoundField
                    DataField="PhoneNumber"
                    HeaderText="تلفن" />

                <asp:BoundField
                    DataField="RegisterationAt"
                    HeaderText="تاریخ عضویت"
                    DataFormatString="{0:yyyy/MM/dd}" />

                <asp:TemplateField HeaderText="وضعیت">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("IsActive")) ? "فعال" : "غیرفعال" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>

                    <ItemTemplate>

                        <asp:LinkButton
                            ID="btnEdit"
                            runat="server"
                            Text="ویرایش"
                            CssClass="btn btn-warning"
                            CommandName="EditUser"
                            CommandArgument='<%# Eval("UserID") %>' />

                        </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField>

                    <ItemTemplate>

                        <asp:LinkButton
                            ID="btnDisable"
                            runat="server"
                            Text="غیرفعال"
                            CssClass="btn btn-danger"
                            CommandName="DisableUser"
                            CommandArgument='<%# Eval("UserID") %>'
                            />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

        <asp:Label
            ID="lb_status"
            runat="server">
        </asp:Label>

    </div>

</asp:Content>
