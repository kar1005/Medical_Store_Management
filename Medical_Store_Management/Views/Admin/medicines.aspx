<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="medicines.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.medicines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .medicine-container {
            background-color: #f8f9fa;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .search-box {
            margin-bottom: 20px;
        }
        .edit-panel {
            background-color: #ffffff;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }
        .grid-container {
            overflow-x: auto;
        }
        .btn-custom {
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <div class="container-fluid">
        <div class="row mt-4">
            <div class="col-md-10 offset-md-1 medicine-container">
                <h2 class="text-center mb-4">Medicines Management</h2>

                <div class="search-box">
                    <div class="input-group">
                        <asp:TextBox ID="tbSearch" runat="server" CssClass="form-control" placeholder="Search medicines..."></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="OnSearch" />
                        </div>
                    </div>
                </div>

                <asp:Panel ID="panelEdit" Visible="false" runat="server" CssClass="edit-panel">
                    <h4 class="mb-3">Edit Medicine</h4>
                    <asp:HiddenField ID="itemId" runat="server" />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbItem">Medicine Name</asp:Label>
                        <asp:TextBox ID="tbItem" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbAmount">Amount</asp:Label>
                        <asp:TextBox ID="tbAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbQuantity">Quantity</asp:Label>
                        <asp:TextBox ID="tbQuantity" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbDescription">Medicine Description</asp:Label>
                        <asp:TextBox ID="tbDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                    <div class="mt-3">
                        <asp:Button ID="saveBtn" runat="server" Text="Save" CssClass="btn btn-success btn-custom" OnClick="OnItemUpdated" />
                        <asp:Button ID="deleteBtn" runat="server" Text="Delete" CssClass="btn btn-danger btn-custom" OnClick="OnItemDeleted" />
                        <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="btn btn-secondary btn-custom" OnClick="OnCancel" />
                    </div>
                </asp:Panel>

                <div class="grid-container">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Item" HeaderText="Item" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRow" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>