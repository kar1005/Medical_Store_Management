<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="Medical_Store_Management.Forms.AddItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style5 {
            width: 151px;
            height: 69px;
        }
        .auto-style6 {
            height: 69px;
        }
        .auto-style7 {
            width: 151px;
            height: 74px;
        }
        .auto-style8 {
            height: 74px;
        }
        .auto-style9 {
            width: 151px;
            height: 95px;
        }
        .auto-style10 {
            height: 95px;
        }
        .auto-style11 {
            width: 151px;
            height: 87px;
        }
        .auto-style12 {
            height: 87px;
        }
        .auto-style13 {
            width: 151px;
            height: 79px;
        }
        .auto-style14 {
            height: 79px;
        }
        .auto-style15 {
            width: 151px;
            height: 46px;
        }
        .auto-style16 {
            height: 46px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table class="auto-style1">
        <tr>
            <td class="auto-style13">Item</td>
            <td class="auto-style14">
                <asp:TextBox ID="tbItem" runat="server" Height="26px" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">Company</td>
            <td class="auto-style12">
                <asp:DropDownList ID="ddlCompany" runat="server" Height="23px" Width="183px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Amount</td>
            <td class="auto-style10">
                <asp:TextBox ID="tbAmount" runat="server" Height="30px" TextMode="Number" Width="177px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">Quantity</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbQuantity" runat="server" Height="26px" TextMode="Number" Width="177px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">Description</td>
            <td class="auto-style6">
                <asp:TextBox ID="tbDescription" runat="server" Height="25px" TextMode="MultiLine" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style15"></td>
            <td class="auto-style16">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="95px" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
 </form>
</body>
</html>
