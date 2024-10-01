<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="BillHistory.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.BillHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
 <h2>Bill History</h2>
    <asp:GridView ID="gvBillHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="BillDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="TotalEarnings" HeaderText="Total Earnings" DataFormatString="{0:C}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnViewDetails" runat="server" Text="View Bills" CommandArgument='<%# Eval("BillDate", "{0:yyyy-MM-dd}") %>' OnClick="btnViewDetails_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
</asp:Content>
