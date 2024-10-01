<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="ViewBills.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.ViewBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <h2>Bills for <%= Request.QueryString["date"] %></h2>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    <asp:Repeater ID="rptBills" runat="server" OnItemDataBound="rptBills_ItemDataBound">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="card-header">
                    <h3>Order ID: <%# Eval("OrderId") %></h3>
                    <p>Customer: <%# Eval("CustomerName") %></p>
                </div>
                <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Medicine</th>
                                <th>Quantity</th>
                                <th>Price per Unit</th>
                                <th>Total Price</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptMedicines" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Item") %></td>
                                        <td><%# Eval("Quantity") %></td>
                                        <td><%# String.Format("{0:C}", Eval("UnitPrice")) %></td>
                                        <td><%# String.Format("{0:C}", Eval("TotalPrice")) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="card-footer">
                    <p><strong>Total Bill Amount: </strong><%# String.Format("{0:C}", Eval("TotalBill")) %></p>
                    <p><strong>Payment Mode: </strong><%# Eval("PaymentMode") %></p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>