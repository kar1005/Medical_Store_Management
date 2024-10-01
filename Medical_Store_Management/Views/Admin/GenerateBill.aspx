<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="GenerateBill.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.GenerateBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <style>
        .medicine-table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }
        .medicine-table th, .medicine-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .medicine-table th {
            background-color: #f2f2f2;
        }
        .remove-btn {
            background-color: #f44336;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
        }
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <div class="auth-box card">
        <div class="card-block">
            <div class="row m-b-20">
                <div class="col-md-12">
                    <h3 class="text-center">Generate Bill</h3>
                </div>
            </div>
            
            <asp:HiddenField ID="hfMedicineList" runat="server" />
            <asp:HiddenField ID="hfSelectedMedicineId" runat="server" />

            <div class="row mb-3">
                <div class="col-md-4">
                    <label for="tbMedicineName" class="form-label">Medicine Name</label>
                    <asp:TextBox ID="tbMedicineName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label for="tbPrice" class="form-label">Price</label>
                    <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <label for="tbInStock" class="form-label">In Stock</label>
                    <asp:TextBox ID="tbInStock" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <label for="tbQuantity" class="form-label">Quantity</label>
                    <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" TextMode="Number" min="1" />
                </div>
                <div class="col-md-2 d-flex align-items-end">
                    <button type="button" class="btn btn-primary" onclick="addMedicine()">Add to Order</button>
                </div>
            </div>
            
            <h4 class="text-center mt-4">Ordered Medicines</h4>
            <table id="medicineTable" class="medicine-table">
                <thead>
                    <tr>
                        <th>Medicine Name</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Total</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="medicineList">
                    <!-- Medicine rows will be added here dynamically -->
                </tbody>
            </table>
            
            <div class="row mt-4">
                <div class="col-md-6">
                    <label for="lblTotalAmount" class="form-label">Total Amount</label>
                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-control" Text="₹0"></asp:Label>
                    <asp:HiddenField ID="hiddenTotalAmount" runat="server" />
                </div>
            </div>
            
            <div class="row mt-4">
                <div class="col-md-6">
                    <label for="tbContactNumber" class="form-label">Customer Contact Number</label>
                    <asp:TextBox ID="tbContactNumber" runat="server" CssClass="form-control" Placeholder="Enter Contact Number" autocomplete="off" />
                </div>
                <div class="col-md-6">
                    <label for="tbCustomerName" class="form-label">Customer Name</label>
                    <asp:TextBox ID="tbCustomerName" runat="server" CssClass="form-control" Placeholder="Enter Customer Name" />
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-3 d-flex align-items-end">
                    <asp:Button ID="btnPlaceOrder" runat="server" CssClass="btn btn-success" Text="Place Order" OnClick="PlaceOrder_Click" OnClientClick="return validateOrder();" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        const medicines = [];

        function addMedicine() {
            var medicineName = $("#<%= tbMedicineName.ClientID %>").val();
            var id = $("#<%= hfSelectedMedicineId.ClientID %>").val();
            var price = parseFloat($("#<%= tbPrice.ClientID %>").val().replace('₹', ''));
            var quantity = parseInt($("#<%= tbQuantity.ClientID %>").val());
            var inStock = parseInt($("#<%= tbInStock.ClientID %>").val());

            if (medicineName && quantity > 0 && quantity <= inStock) {
                var amount = quantity * price;
                medicines.push({ id: parseInt(id), name: medicineName, quantity: quantity, price: price });
                updateTotalAmount(amount);
                displayMedicines();
                clearInputs();
            } else if (quantity > inStock) {
                alert('Quantity exceeds available stock');
            } else {
                alert('Please enter valid medicine and quantity');
            }
        }

        function displayMedicines() {
            const medicineList = document.getElementById('medicineList');
            medicineList.innerHTML = '';

            medicines.forEach((medicine, index) => {
                const row = medicineList.insertRow();
                row.innerHTML = `
                    <td>${medicine.name}</td>
                    <td>${medicine.quantity}</td>
                    <td>₹${medicine.price.toFixed(2)}</td>
                    <td>₹${(medicine.price * medicine.quantity).toFixed(2)}</td>
                    <td><button class="remove-btn" onclick="removeMedicine(${index})">Remove</button></td>
                `;
            });

            document.getElementById('<%= hfMedicineList.ClientID %>').value = JSON.stringify(medicines);
        }

        function removeMedicine(index) {
            var removedMedicine = medicines[index];
            medicines.splice(index, 1);
            displayMedicines();
            updateTotalAmount(-1 * removedMedicine.price * removedMedicine.quantity);
        }

        function clearInputs() {
            $("#<%= tbMedicineName.ClientID %>").val('');
            $("#<%= tbQuantity.ClientID %>").val('');
            $("#<%= tbPrice.ClientID %>").val('');
            $("#<%= tbInStock.ClientID %>").val('');
            $("#<%= hfSelectedMedicineId.ClientID %>").val('');
        }

        function updateTotalAmount(amount) {
            let current = parseFloat(document.getElementById('<%= lblTotalAmount.ClientID %>').innerText.replace('₹', '')) || 0;
            let totalAmount = current + amount;
            document.getElementById('<%= lblTotalAmount.ClientID %>').innerText = `₹${totalAmount.toFixed(2)}`;
            document.getElementById('<%= hiddenTotalAmount.ClientID %>').value = totalAmount.toFixed(2);
        }

        function validateOrder() {
            if (medicines.length === 0) {
                alert('Please add at least one medicine to the order');
                return false;
            }
            if (!document.getElementById('<%= tbContactNumber.ClientID %>').value) {
                alert('Please enter customer contact number');
                return false;
            }
            if (!document.getElementById('<%= tbCustomerName.ClientID %>').value) {
                alert('Please enter customer name');
                return false;
            }
            return true;
        }

        function clearMedicineList() {
            medicines.length = 0;
            displayMedicines();
            updateTotalAmount(0);
        }

        function showSuccessMessage(message) {
            alert(message);
        }

        function showErrorMessage(message) {
            alert(message);
        }

        $(document).ready(function() {
            $("#<%= tbMedicineName.ClientID %>").autocomplete({
                source: function(request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Views/Admin/GenerateBill.aspx/GetMedicineSuggestions") %>',
                        data: JSON.stringify({ prefix: request.term }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    label: item.Name,
                                    value: item.Name,
                                    id: item.Id,
                                    price: item.Price,
                                    inStock: item.Quantity
                                };
                            }));
                        },
                        error: function(response) {
                            alert(response.responseText);
                        },
                        failure: function(response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function(e, i) {
                    $("#<%= tbPrice.ClientID %>").val('₹' + i.item.price);
                    $("#<%= tbInStock.ClientID %>").val(i.item.inStock);
                    $("#<%= hfSelectedMedicineId.ClientID %>").val(i.item.id);
                },
                minLength: 1
            });

            $("#<%= tbContactNumber.ClientID %>").autocomplete({
                source: function(request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Views/Admin/GenerateBill.aspx/GetCustomerSuggestions") %>',
                        data: JSON.stringify({ prefix: request.term }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    label: item.Phone_no + " (" + item.Name + ")",
                                    value: item.Phone_no,
                                    name: item.Name
                                };
                            }));
                        },
                        error: function(response) {
                            alert(response.responseText);
                        },
                        failure: function(response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function(e, i) {
                    $("#<%= tbCustomerName.ClientID %>").val(i.item.name);
                },
                minLength: 1
            });
        });
    </script>
</asp:Content>