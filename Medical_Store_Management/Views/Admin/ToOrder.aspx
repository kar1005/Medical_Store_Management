<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="ToOrder.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.ToOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <h2>Medicines to Order</h2>
    <asp:GridView ID="gvMedicinesToOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" />
            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
            <asp:BoundField DataField="Quantity" HeaderText="Current Quantity" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
        </Columns>
    </asp:GridView>
</asp:Content>
