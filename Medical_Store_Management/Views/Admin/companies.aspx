<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="companies.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.companies" %>
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
                                       <asp:TextBox ID="tbName" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="lblemail">Company Name</asp:Label>
                                    </div> 
                        <div class="col" style="margin-top:20px;">
                                <asp:Button ID="saveBtn" runat="server" Text="Save" class="btn btn-success" OnClick="OnStaffUpdated" />

                            </div>
                            <div class="col" style="margin-top:20px;">
                                <asp:Button ID="deleteBtn" runat="server" Text="Delete" class="btn btn-danger" OnClick="OnStaffDeleted"/>

                            </div>
                    </asp:Panel>

                    </div>

                </div>

                <div class="col-8" style="margin-top:100px;">
                    <h2>Company List</h2> 
                   <asp:GridView ID="GridViewCompany" runat="server" CssClass="form-control" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
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
