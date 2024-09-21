<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="categories.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">

    <div class="row mt-5">
        <div class="col-md-1"></div>
        <div class="col-md-10 bg-white">
            <div class="row">
                <div class="col-5">

                    <div class="row mb-3">
                        <div class="form-group col-md-6">
                            <label>First Name</label>
                            <asp:TextBox ID="F_name" type="text" class="form-control" runat="server" placeholder="Enter First Name" ValidationGroup="allotment"></asp:TextBox>
                           </div>
                        <div class="col">
                            <input type="text" id="txtBox" class="form-control" placeholder="Category Name" aria-label="First name" runat="server">
                        </div>

                        <div class="row mb-3">
                            <div class="col" d-grid>
                                <asp:Button ID="editBtn" runat="server" Text="Edit" class="btn btn-primary btn-block" OnClick="editBtn_Click" />
                            </div>
                            <div class="col" d-grid>
                                <asp:Button ID="saveBtn" runat="server" Text="Save" class="btn btn-success btn-block" OnClick="saveBtn_Click" />

                            </div>
                            <div class="col" d-grid>
                                <asp:Button ID="deleteBtn" runat="server" Text="Delete" class="btn btn-danger btn-block" OnClick="deleteBtn_Click" />

                            </div>

                        </div>

                    </div>

                </div>

                <div class="col-8">
                    <h2>Medicines List</h2>
                    <asp:GridView ID="MedicineList" class="table" runat="server"></asp:GridView>
                </div>

            </div>
            <div class="col-md-1"></div>
        </div>
    </div>
    </
</asp:Content>
