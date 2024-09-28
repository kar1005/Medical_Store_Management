﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="medicines.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.medicines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">

    <div class="container-fluid">
        <div class="row">
        </div>
        <div class="row mt-5">
            <div class="col-md-1"></div>
            <div class="col-md-10 bg-white">
                <div class="row">
                    <div class="col-5">

                    <asp:Panel ID="panelEdit" Visible="false" runat="server">
                        <asp:HiddenField ID="itemId" runat="server" />
                        <div class="form-group form-primary">
                                       <asp:TextBox ID="tbItem" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="lblemail">Medicine Name</asp:Label>
                                    </div> 

                                    <div class="form-group form-primary">
                                       <asp:TextBox ID="tbAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="Label2">Amount</asp:Label>
                                    </div>

                                    <div class="form-group form-primary">
                                       <asp:TextBox ID="tbQuantity" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="Label3">Quantity</asp:Label>
                                    </div>

                                    <div class="form-group form-primary">
                                       <asp:TextBox ID="tbDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="Label4">Medicine Description</asp:Label>
                                    </div>
                        <div class="col" style="margin-top:20px;">
                                <asp:Button ID="saveBtn" runat="server" Text="Save" class="btn btn-success" OnClick="OnItemUpdated" />

                            </div>
                            <div class="col" style="margin-top:20px;">
                                <asp:Button ID="deleteBtn" runat="server" Text="Delete" class="btn btn-danger" OnClick="OnItemDeleted"/>

                            </div>
                    </asp:Panel>

                    </div>

                </div>

                <div class="col-8" style="margin-top:100px;">
                    <h2>Medicines List</h2> 
                   <asp:GridView ID="GridView1" runat="server" CssClass="form-control" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Item" HeaderText="Item" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRow" CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </div>

            </div>
            <div class="col-md-1"></div>
        </div>
    </div>

</asp:Content>
