﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="Medical_Store_Management.Forms.AddItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="icon" href="../../assets/images/favicon.ico" type="image/x-icon"/>
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet"/>
    <!-- waves.css -->
    <link rel="stylesheet" href="../../assets/pages/waves/css/waves.min.css" type="text/css" media="all"/>
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="../../assets/css/bootstrap/css/bootstrap.min.css"/>
    <!-- waves.css -->
    <link rel="stylesheet" href="../../assets/pages/waves/css/waves.min.css" type="text/css" media="all"/>
    <!-- themify icon -->
    <link rel="stylesheet" type="text/css" href="../../assets/icon/themify-icons/themify-icons.css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="../../assets/icon/font-awesome/css/font-awesome.min.css"/>
    <!-- scrollbar.css -->
    <link rel="stylesheet" type="text/css" href="../../assets/css/jquery.mCustomScrollbar.css"/>
    <!-- am chart export.css -->
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="../../assets/css/style.css"/>
</head>
<body">
    <section class="login-block">
        <!-- Container-fluid starts -->
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <!-- Authentication card start -->
                        <form class="md-float-material form-material" runat="server">
                            <div class="auth-box card">
                                <div class="card-block">
                                    <div class="row m-b-20">
                                        <div class="col-md-12">
                                            <h3 class="text-center">Add Medicine</h3>
                                        </div>
                                    </div>
                                    <div class="form-group form-primary">
                                       <asp:TextBox ID="tbItem" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="lblemail">Medicine Name</asp:Label>
                                    </div> 

                                    <div class="form-group form-primary">
                                       <asp:DropDownList runat="server" ID="ddlCompany" CssClass="form-control"></asp:DropDownList>
                                        <span class="form-bar"></span>
                                        <asp:Label Cssclass="float-label" runat="server" ID="Label1">Company</asp:Label>
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

                                    <div class="row m-t-30">
                                        <div class="col-md-12">
                                            <asp:Button runat="server" OnClick="btnSubmit_Click" Cssclass="btn btn-primary btn-md btn-block waves-effect waves-light text-center m-b-20" Text="Add"></asp:Button>
                                        </div>
                                    </div>

                                    <hr/>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="text-inverse text-left m-b-0">Thank you.</p>
                                            <p class="text-inverse text-left"><a href="index.html"><b>Back to website</b></a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <!-- end of form -->
                </div>
                <!-- end of col-sm-12 -->
            </div>
            <!-- end of row -->
        </div>
        <!-- end of container-fluid -->
    </section>
    <script src="../../assets/pages/waves/js/waves.min.js"></script>
    <script type="text/javascript" src="../../assets/js/modernizr/modernizr.js "></script>
    <!-- slimscroll js -->
    <script type="text/javascript" src="../../assets/js/SmoothScroll.js"></script>
    
    <script type="text/javascript" src="../../assets/js/chart.js/Chart.js"></script>
    <!-- amchart js -->
    <script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
    <script src="../../assets/pages/widget/amchart/gauge.js"></script>
    <script src="../../assets/pages/widget/amchart/serial.js"></script>
    <script src="../../assets/pages/widget/amchart/light.js"></script>
    <script src="../../assets/pages/widget/amchart/pie.min.js"></script>
    <script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
</body>
</html>