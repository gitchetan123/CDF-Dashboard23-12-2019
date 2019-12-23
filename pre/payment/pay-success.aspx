<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pay-success.aspx.cs" Inherits="pre_payment_pay_success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- plugins:css -->
    <link rel="stylesheet" href="../../vendors/iconfonts/mdi/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="../../vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="../../vendors/css/vendor.bundle.addons.css" />
    <!-- endinject -->
    <!-- plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../../css/style.css" />
    <!-- endinject -->
    <link rel="shortcut icon" href="../../images/favicon.png" />
    <style type="text/css">
        .text_f {
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container-scroller">
                <div class="container-fluid page-body-wrapper full-page-wrapper auth-page">
                    <div class="content-wrapper d-flex align-items-center auth auth-bg-pay-success theme-one">
                        <div class="row w-100">
                            <div class="col-lg-5 mx-auto">
                                <div class="auto-form-wrapper">
                                    <form action="#">
                                        <h1>Payment Success</h1>
                                        <hr />
                                        <br />

                                        <div class="alert alert-success" id="div_msg" style="text-align: center; margin-left: 10px; margin-right: 10px;">
                                            Payment Successfully Completed ...
                                        </div>

                                        <div class="row text-center fontc">
                                            <div class="row" style="text-align: center; margin-top: 8px; margin-left: 40px; font-weight: normal;"><u><a href="https://dheya.com/cdf-dashboard/login.aspx" style="color: #000000;">Go to Dashboard and check you payment status</a></u></div>
                                            <br /><br />
                                            <div id="div_note" runat="server">
                                                <p>Note: You can pay your Balance payment through Dashboard... OR You can again use the same link that you have given by Email.</p>
                                            </div>
                                        </div>
                                        <br />
                                        <hr />
                                        <p class="text_f text-center">
                                            © Copyright
                                            <script>document.write(new Date().getFullYear());</script>
                                            &nbsp;| <a href="https://dheya.com" target="_blank">Dheya Career Mentors</a>
                                        </p>



                                    </form>
                                </div>

                            </div>

                        </div>
                    </div>
                    <!-- content-wrapper ends -->
                </div>
                <!-- page-body-wrapper ends -->
            </div>
            <!-- container-scroller -->
            <!-- plugins:js -->
            <script src="../../vendors/js/vendor.bundle.base.js"></script>
            <script src="../../vendors/js/vendor.bundle.addons.js"></script>
            <!-- endinject -->
            <!-- inject:js -->
            <script src="../../js/off-canvas.js"></script>
            <script src="../../js/misc.js"></script>
            <!-- endinject -->
        </div>
    </form>
</body>
</html>
