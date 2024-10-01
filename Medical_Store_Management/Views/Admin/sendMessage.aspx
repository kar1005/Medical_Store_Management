<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/adminMaster.Master" AutoEventWireup="true" CodeBehind="sendMessage.aspx.cs" Inherits="Medical_Store_Management.Views.Admin.sendMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style>
        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-control {
            width: 100%;
            padding: 8px;
            font-size: 16px;
        }
        .btn {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .message {
            margin-top: 15px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#<%= txtPhoneNumber.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Views/Admin/sendMessage.aspx/GetCustomerSuggestions") %>',
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
                        error: function(xhr, status, error) {
                            console.log("Error: " + error);
                        }
                    });
                },
                minLength: 1,
                select: function(event, ui) {
                    $("#<%= txtPhoneNumber.ClientID %>").val(ui.item.value);
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <div class="container">
        <h2>Send a WhatsApp Message</h2>
        <div class="form-group">
            <label for="txtPhoneNumber">Enter Phone Number (with country code):</label>
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="+1234567890"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtMessage">Enter Message:</label>
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnSend" runat="server" Text="Send Message" OnClick="btnSend_Click" CssClass="btn" />
        <div class="message">
            <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
</asp:Content>