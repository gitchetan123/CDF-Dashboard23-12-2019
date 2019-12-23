<%@ Page Language="C#" AutoEventWireup="true" CodeFile="custom-pay-page.aspx.cs" Inherits="pre_payment_custom_pay_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <link href="<%=ResolveUrl("~") %>vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />
    <!-- NProgress -->
    <link href="<%=ResolveUrl("~") %>vendors/nprogress/nprogress.css" rel="stylesheet" />
    <!-- bootstrap-progressbar -->
    <link href="<%=ResolveUrl("~") %> vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <%--Favicon--%>
    <link rel="icon" href="../images/fevicon.ico" type="image/x-icon" />
    <!-- Custom Theme Style -->
    <link href="<%=ResolveUrl("~") %>css/custom.min.css" rel="stylesheet" />
    <link href="<%=ResolveUrl("~") %>css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .adress {
            max-width: 200px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }


        /*html, body {
            height: 100%;*/ /* The html and body elements cannot have any padding or margin. */
        /*overflow-x: hidden;*/
        /*overflow-y: scroll;*/
        /*}*/
    </style>
    <style type="text/css">
        .rowspace {
            padding: 0 10px 0 10px;
        }

        .razorpay-payment-button {
            padding: 8px 60px 8px 60px;
            background-color: #428bca;
            align-content: center;
            border-color: #428bca;
            color: #ffffff;
            margin: 15px 0 0 0;
        }

        p {
            font-size: 15px;
        }
    </style>
</head>
<body>
    <div>
        <div class="x_panel">
            <div class="x_title">
                <h2>Advance Payment</h2>

                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div class="row rowspace">
                    <div class="" id="div_payment" runat="server">
                        <center>
                    <form action="custompay-process-page.aspx" method="post">
                        <p style="text-align:justify;">
                            Thanks for showing your interest and faith in us by confirming the same while making payment of the aforesaid amount to Join Dheya community to help national youth to shape their career. You are requested to pay remaining amount as described by our executing before completion of your training.   
<br />
We welcome you to “Dheya” community and will update you with your payment details. We will update our records and will proceed with other formalities for your candidature. Our executive will be in touch with you to discuss other formalities and event details. 
<br />
Please feel free to contact us for any further queries to assigned executive or at customer support at phone numbers: +91 99 23 400 555 | 020-24223655 / 65007555  or write us at  care@dheya.com.

                        </p>                        
                        <script
                            src="https://checkout.razorpay.com/v1/checkout.js"
                           <%--data-key="rzp_test_ERVr7EOp6kqmYn"--%>
                             <%-- Original Acc Details--%>
                            <%--data-key="rzp_live_pAKEJ7HxaRkA5W"--%>
                            data-key="<%=razorkey11%>"
                            data-amount="<%=price%>"
                            data-name="Dheya CDF"
                            data-description="Training Fees"
                            data-order_id="<%=orderId%>"
                            data-image="../images/Dheya-icon.png"
                            data-prefill.name="<%=usename%>"
                            data-prefill.email="<%=email%>"
                            data-prefill.contact="<%=contact%>"
                            data-theme.color="#2380D9"></script>  <%--#F37254--%>
                        <input type="hidden" value="Hidden Element" name="hidden" />
                    </form>
                        </center>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="x_title">
                © Copyright
            <script>document.write(new Date().getFullYear());</script>
                &nbsp;| <a href="https://dheya.com" target="_blank">Dheya Career Mentors</a>

                <div class="clearfix"></div>
            </div>
        </div>
        <!-- Custom Theme Scripts -->
        <script src="../../js/custom.min.js"></script>
    </div>
</body>
</html>
