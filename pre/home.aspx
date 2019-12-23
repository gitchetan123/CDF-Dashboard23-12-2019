<%@ Page Title="" Language="C#" MasterPageFile="~/pre/PreCDFMasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="Home_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lifont {
            font-size: 16px;
            padding-top: 8px;
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
    <form id="form1" runat="server">
      
        
        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="dashboard_graph x_panel">
                    <div class="x_title">

                        <h2>Brochures And Webinar Links</h2>

                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <p style="font-size: 16px; font-weight: bold">Documents:</p>
                        <ul>
                            <li class="lifont"><a href="https://www.dheya.com/email-docs/CDF-Proposal.pdf" target="_blank"><i class="fa fa-book"></i>&nbsp; CDF Proposal</a>
                            </li>
                            <%--<li class="lifont"><a href="https://www.dheya.com/email-docs/DHEYA-corporate-Proposal.pdf"><i class="fa fa-book"></i>&nbsp;DHEYA Corporate Proposal</a>
                            </li>
                            <li class="lifont"><a href="https://www.dheya.com/email-docs/Dheya-Partial-Client-List.pdf"><i class="fa fa-book"></i>&nbsp;Dheya Partial Client List</a>
                            </li>--%>
                            <%--<li class="lifont"><a href="https://www.dheya.com/email-docs/FAQs-2017.pdf"><i class="fa fa-book"></i>&nbsp;FAQs</a>
                            </li>--%>
                        </ul>
                        <p style="font-size: 16px; font-weight: bold">Webinar Links:</p>
                        <ul>
                            <li class="lifont"><a href="https://youtu.be/AU7Jbi7g8aU" target="_blank"><i class="fa fa-play"></i>&nbsp; Webinar 1</a>
                            </li>
                            <li class="lifont"><a href="https://www.youtube.com/watch?v=FjQDRr__vh8" target="_blank"><i class="fa fa-play"></i>&nbsp; Webinar 2</a>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Test and Payment Details</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <h4>Test Completed:&nbsp;<asp:Label ID="test_status" runat="server" Text=""></asp:Label></h4>
                        <div id="hiddendiv1" runat="server">
                            <h4>To take a test <a href="test.aspx" style="color: cornflowerblue; font-weight: 500;">Click Here</a></h4>
                        </div>
                        <h4>Payment Status:&nbsp;<asp:Label ID="lbl_payStatus" runat="server" Text=""></asp:Label></h4>
                        <div style="margin-bottom: 44px;"></div>
                    </div>
                </div>
            </div>
        </div>

         <div class="row" id="RowProfile" runat="server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="dashboard_graph x_panel">
                    <div class="row x_title">
                        <div class="col-md-6">
                            <h2>Profile Analysis</h2>
                        </div>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <asp:Label ID="lblProfilAnalysis" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="dashboard_graph x_panel">
                    <div class="row x_title">
                        <div class="col-md-6">
                            <h2>The Journey of a Mentor</h2>
                        </div>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                       <%-- <img src="../images/The-Journey-of-a-Mentor.png" style="width: 100%; height: 100%;" alt="Registration Process" />--%>
                        <img src="../images/Infographic version 2.png" style="width: 100%; height: 100%;" alt="Registration Process" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="dashboard_graph x_panel">
                    <div class="row x_title">
                        <div class="col-md-6">
                            <h2>Upcoming Training Dates</h2>
                        </div>
                        <%-- <div class="col-md-6">--%>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="row">
                            <div class="col-md-8 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                                <div id="div_msg" runat="server" class="" style="text-align: center; margin: 0 10px 10px 10px"></div>
                                <%--<div class="row">
                                <div class="col-md-10 col-sm-10 col-xs-10 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">--%>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                                    Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    PageSize="5">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Date" HeaderText="Training Date" SortExpression="Date"
                                            DataFormatString="{0:d}">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>

                                    </Columns>
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" Wrap="True" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

