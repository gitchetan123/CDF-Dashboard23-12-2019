<%@ Page Title="" Language="C#" MasterPageFile="~/pre/PreCDFMasterPage.master" AutoEventWireup="true" CodeFile="payment-success.aspx.cs" Inherits="pre_payment_success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="x_panel">
        <div class="x_title">
           
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
            <div class="row">

                <div class="alert alert-success" id="div_msg"  style="text-align: center; margin-left: 10px; margin-right: 10px;">
                    Payment Successful
                </div>
            </div>
        </div>
    </div>
      <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

