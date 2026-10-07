<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApp.Pages.User.Register" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>ایجاد حساب کاربری</title>
     

    <link href="../../Content/MyStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container" dir="rtl">
            <h2>ثبت نام</h2>
            
            <div class="form-group">
                <label class="label">نام کاربری:<br />
                <label dir="rtl">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="textUsername" ErrorMessage="ورودی نامعتبر" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
                </label>
&nbsp;<asp:TextBox ID="textUsername" runat="server" CssClass="txt_box"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label dir="rtl">رمز عبور:<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="textPassword" ControlToValidate="textPassword2" ErrorMessage="CompareValidator" ForeColor="Red">رمز عبور ها یکسان نیستند</asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textPassword" ErrorMessage="ورودی نامعتبر" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:TextBox ID="textPassword" runat="server" TextMode="Password" CssClass="txt_box"></asp:TextBox>
            </div>

            <div class="form-group">
                <label dir="rtl">رمز عبور دوباره:<br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="textPassword2" ErrorMessage="ورودی نامعتبر" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:TextBox ID="textPassword2" runat="server" TextMode="Password" CssClass="txt_box"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnRegister" class="btn" runat="server" Width="50%" Text="ثبت نام" OnClick="btnRegister_Click" />

            <div class="form-group">
                <a href="#" style="color:deepskyblue">وارد شوید</a>
            </div>
            
            <asp:Label ID="lable_er" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
