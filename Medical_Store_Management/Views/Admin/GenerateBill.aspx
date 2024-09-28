<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="GenerateBill.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.GenerateBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <div class="auth-box card">
        <div class="card-block">
            <div class="row m-b-20">
                <div class="col-md-12">
                    <h3 class="text-center">Generate Bill</h3>
                </div>
            </div>
            
            <!-- Input Fields for Medicines -->
            <asp:HiddenField ID="hfMedicineList" runat="server" />

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="medicineName" class="form-label">Medicine Name</label>
                    <asp:DropDownList runat="server" ID="ddlMedcineName" CssClass="form-control"></asp:DropDownList>
                </div>
                <asp:HiddenField ID="medicinePrice" runat="server"></asp:HiddenField>
                <div class="col-md-3">
                    <label for="quantity" class="form-label">Quantity</label>
                    <asp:TextBox ID="quantity" runat="server" CssClass="form-control" Placeholder="Enter quantity" />
                </div>
                <div class="col-md-3 d-flex align-items-end">
                    <button type="button" class="btn btn-primary" onclick="addMedicine()">Add to Order</button>
                </div>
            </div>
            
            <!-- Display List of Ordered Medicines -->
            <h4 class="text-center">Ordered Medicines</h4>
            <ul class="list-group" id="medicineList">
                <!-- Medicine list will appear here -->
            </ul>
            
            <!-- Total Amount Label -->
            <div class="row mt-4">
                <div class="col-md-6">
                    <label for="totalAmount" class="form-label">Total Amount</label>
                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-control" Text=""></asp:Label>
                </div>
            </div>
            
            <!-- Contact Number Field -->
            <div class="row mt-4">
                <div class="col-md-6">
                    <label for="contactNumber" class="form-label">Customer Contact Number</label>
                    <asp:TextBox ID="tbcontactNumber" runat="server" CssClass="form-control" Placeholder="Enter Contact Number" />
                </div>
            </div>

            <!-- Panel for Customer Name (Conditionally Visible) -->
            <asp:Panel ID="panelName" runat="server" Visible="false">
                <div class="row mt-4">
                    <div class="col-md-6">
                        <label for="customerName" class="form-label">Customer Name</label>
                        <asp:TextBox ID="tbCustomerName" runat="server" CssClass="form-control" Placeholder="Enter Customer Name" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Place Order Button -->
            <div class="row mt-3">
                <div class="col-md-3 d-flex align-items-end">
                    <asp:Button ID="btnPlaceOrder" runat="server" CssClass="btn btn-success" Text="Place Order" OnClick="PlaceOrder_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        const medicines = [];
        let totalAmount = 0;

        function addMedicine() {
            var ddl = document.getElementById('<%= ddlMedcineName.ClientID %>');
            var medicineName = ddl.options[ddl.selectedIndex].text;
            const quantity = document.getElementById('<%= quantity.ClientID %>').value;
            var quantity = parseInt(document.getElementById('quantity').value);
            var price = parseInt(document.getElementById('medicinePrice').value);
            var amount = quantity * price;
            if (medicineName && quantity) {
                medicines.push({ name: medicineName, quantity: quantity, amount: amount });
                updateTotalAmount(amount);
                displayMedicines();
                clearInputs();
            } else {
                alert('Please enter both medicine name and quantity');
            }
        }

        function displayMedicines() {
            const medicineList = document.getElementById('medicineList');
            medicineList.innerHTML = '';

            medicines.forEach((medicine, index) => {
                const listItem = document.createElement('li');
                listItem.className = 'list-group-item d-flex justify-content-between align-items-center';
                listItem.innerHTML = `${medicine.name} - ${medicine.quantity} - ${amount}
                    <button class="btn btn-danger btn-sm" onclick="removeMedicine(${index})">Remove</button>`;
                medicineList.appendChild(listItem);
            });
        }

        function removeMedicine(index) {
            medicines.splice(index, 1);
            displayMedicines();
            updateTotalAmount();
        }


        function clearInputs() {
            document.getElementById('medicineName').value = '';
            document.getElementById('quantity').value = '';
        }

        function updateTotalAmount(amount) {
            int current = parseInt(document.getElementById('<%= lblTotalAmount.ClientID %>').innerText);
            var totalAmount = current + amount;
            document.getElementById('<%= lblTotalAmount.ClientID %>').innerText = `$${totalAmount}`;
        }

    </script>
</asp:Content>
        