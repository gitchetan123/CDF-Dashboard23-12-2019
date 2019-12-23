<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Executive/executive-master.master" AutoEventWireup="true" CodeFile="executive-home.aspx.cs" Inherits="executive_ExecutiveHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
        }

        .panel {
            padding: 10px;
            text-align: center;
            max-width: 700px;
            padding: 5px;
            margin-top: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
       
        <div class="row top_tiles">
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon"><i class="fa fa-pencil-square-o blue"></i></div>
                    <div class="count">
                        <asp:Label ID="test_sent" runat="server"></asp:Label>
                    </div>
                    <h3 style="margin-top: 15px;">Test Sent</h3>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon"><i class="fa fa-check-square-o green"></i></div>
                    <div class="count">
                        <%-- <span style="font-family: Rupee Foradian">` </span>--%>
                        <%-- <span><i class="fa fa-inr" aria-hidden="true"></i></span>--%>
                        <asp:Label ID="test_completed" runat="server"></asp:Label>
                    </div>
                    <h3 style="margin-top: 15px;">Test Completed</h3>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon"><i class="fa fa-money purple"></i></div>
                    <div class="count">
                        <asp:Label ID="payment_recieved" runat="server"></asp:Label>
                    </div>
                    <h3 style="margin-top: 15px;">Payment Received</h3>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon" id="lbl_lastvisit" runat="server"><i class="fa fa-line-chart red"></i></div>
                    <div class="count">
                        <asp:Label ID="conversion_rate" runat="server"></asp:Label>%
                    </div>
                    <h3 style="margin-top: 15px;">Conversion Rate</h3>
                </div>

            </div>
        </div>
        <div class="x_panel">
            <div class="x_title">
                <h2>Home</h2>
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

                <div align="center">
                    <h2 class="text-primary" align="center">Welcome to Executive Panel.</h2>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../../js/custom.min.js"></script>
</asp:Content>

