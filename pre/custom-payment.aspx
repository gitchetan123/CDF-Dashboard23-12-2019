<%@ Page Title="" Language="C#" MasterPageFile="~/pre/PreCDFMasterPage.master" AutoEventWireup="true" CodeFile="custom-payment.aspx.cs" Inherits="pre_custom_payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
         <%-- Prevent cut , copy paste start code--%>
    <script
        src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });
        });
    </script>
     <%-- Prevent cut , copy paste end code --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="x_panel">
        <div class="x_title">
            <h2>Advance Payment</h2>
            <ul class="nav navbar-right panel_toolbox">
                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                </li>
                <li><a class="close-link"><i class="fa fa-close"></i></a>
                </li>
            </ul>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <br />
            <div class="row rowspace">
                <div class="" id="div_payment" runat="server">
                    <center>
                    <form action="custompay-process.aspx" method="post">
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
    </div>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

