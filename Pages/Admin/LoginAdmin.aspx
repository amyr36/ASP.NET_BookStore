<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="WebApp.Pages.Admin.LoginAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ورود به سیستم</title>

<link href="../../Content/MyStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container" dir="rtl">
            <h2>ورود مدیر</h2>
            
            <div class="form-group">
                <label class="label">نام کاربری:</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="ورودی نامعتبر" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="txt_box"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label dir="rtl">رمز عبور:<br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="ورودی نامعتبر" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txt_box"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnLogin" class="btn" runat="server" Width="50%" Text="ورود" OnClick="btnLogin_Click1" />

            <div class="form-group">
                <a href="Register.aspx" style="color:deepskyblue">عضو شوید</a>
            </div>
            
            <asp:Label ID="lb_error" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
