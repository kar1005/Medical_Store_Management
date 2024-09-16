<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Medical_Store_Management.Forms.index" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pharmacy Management System - Login</title>
</head>
<body>
    <form id="loginForm" runat="server">
        <div>
            <asp:Label ID="lblUsername" runat="server" Text="Username:" />
            <asp:TextBox ID="txtUsername" runat="server" />
        </div>
        <div>
            <asp:Label ID="lblPassword" runat="server" Text="Password:" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
        </div>
        <div>
            <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember Me" />
        </div>
        <div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" />
        </div>
    </form>
</body>
</html>