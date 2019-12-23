<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap -->
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <%--<link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />--%>
    <!-- NProgress -->
    <link href="vendors/nprogress/nprogress.css" rel="stylesheet" />
    <!-- bootstrap-progressbar -->
    <link href="vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />

    <!-- Custom Theme Style -->
    <link href="css/custom.min.css" rel="stylesheet" />
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="row top_tiles">
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-users blue"></i></div>
                <div class="count">
                    <asp:Label ID="session_count" runat="server"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Sessions Done</h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-line-chart green"></i></div>
                <div class="count">
                    <span style="font-family: Rupee Foradian">` </span>
                    <span><i class="fa fa-inr" aria-hidden="true"></i></span>
                    <asp:Label ID="revenue_count" runat="server"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Revenue Earned</h3>
            </div>
        </div>
       </div>--%>

    <%--<div class="row top_tiles">
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus purple"></i></div>        
                <div class="count">
                    <a href="#" data-toggle="modal" title="Click here to check leads converted" class="" runat="server" id="A1" data-target="#myModal_lead_converted">
                    <asp:Label ID="lbl_converted" runat="server" ToolTip="Total leads converted"></asp:Label>
                    </a> |
                    <a href="#" data-toggle="modal" title="Click here to check leads converted" class="" runat="server" id="A2" data-target="#myModal_lead_created">
                    <asp:Label ID="lbl_created" runat="server" ToolTip="Total leads created"></asp:Label>
                    </a>
                </div>
                <h3 style="margin-top: 15px;">My Leads </h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon" id="Div1" runat="server"><i class="fa fa-asterisk"></i></div>
                <div class="count">
                    <asp:Image ID="img_star1" runat="server" ImageUrl="~/images/rated_strar.png" Height="40px" Width="40px" />
                    <asp:Image ID="img_star2" runat="server" ImageUrl="~/images/rated_strar.png" Height="40px" Width="40px" />
                    <asp:Image ID="img_star3" runat="server" ImageUrl="~/images/rated_strar.png" Height="40px" Width="40px" />
                    <asp:Image ID="img_star4" runat="server" ImageUrl="~/images/STAR.png" Height="30px" Width="30px" />
                    <asp:Image ID="img_star5" runat="server" ImageUrl="~/images/STAR.png" Height="30px" Width="30px" />
                </div>
                <h3 style="margin-top: 15px;">Rating</h3>
            </div>
        </div>
    </div>--%>

   <%-- original --%>
    <div class="row top_tiles">
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus purple"></i></div>
                <div class="count">
                    <asp:Label ID="leads_count" runat="server"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Leads Created </h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon" id="lbl_lastvisit" runat="server"><i class="fa fa-eye red"></i></div>
                <div class="count">
                    <asp:Label ID="visits_count" runat="server"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Total Visits</h3>
            </div>

        </div>
    </div>
    <br />

    <%--<div class="row top_tiles">
            <div class="col-md-6 col-sm-10 col-xs-12">
            <div class="dashboard_graph x_panel">
                <div class=" x_title">
                    <h2>Number of Leads New</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div id="chart_div100" style="width: 95%; height: 100%;">
                        <asp:Literal ID="lt100" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
      
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-credit-card"></i></div>
                <div class="count">
                    <asp:Label ID="lbl_wallet" runat="server" Text="INR 0.00"></asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Wallet  </h3>
            </div>
        </div>
    </div>--%>
    <br />



    <div class="row">
        <div class="col-md-6 col-sm-10 col-xs-12">
            <div class="dashboard_graph x_panel">
                <div class=" x_title">
                    <h2>Number of Referrals By Months</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div id="chart_div" style="width: 95%; height: 100%;">
                        <asp:Literal ID="lt" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-10 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>All CDF's Referrals</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>

                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div id="chart_div2" style="width: 95%; height: 100%;">
                        <asp:Literal ID="lt2" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-8 col-sm-10 col-xs-12">
                <div class="x_panel fixed_height_320">
                    <div class="x_title">
                        <h2>Referral Lead Status</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="leads/Previewleads.aspx">View Details</a>
                                    </li>
                                </ul>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">

                        <div id="div_leadstatus" runat="server" class="" style="text-align: center; margin: 90px 10px 0 10px;">
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                            Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="5">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
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

            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="x_panel fixed_height_320">
                    <div class="x_title">
                        <h2>Profile Details</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="CDF/updateinfo.aspx">Edit Information</a>
                                    </li>
                                    <li><a href="CDF/education.aspx">Edit Education</a>
                                    </li>
                                    <li><a href="CDF/experience.aspx">Edit Experience</a>
                                    </li>
                                </ul>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="dashboard-widget-content">
                            <div id="chart11" style="width: 100%; height: 100%;"></div>
                            <h4 style="text-align: center;">
                                <asp:Label ID="profilec" runat="server"></asp:Label>%  Profile Completion</h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="margin-bottom: 50px;">
            <div class="col-md-4 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>News Feed</h2>
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
                        <ul class="list-unstyled timeline widget">
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title1" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr1" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title2" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr2" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content2" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="block">
                                    <div class="block_content">
                                        <h2 class="title">
                                            <asp:Label ID="lbl_title3" runat="server"></asp:Label>
                                        </h2>
                                        <div class="byline">
                                            <span>
                                                <asp:Label ID="lbl_hr3" runat="server"></asp:Label>
                                                days ago</span>
                                        </div>
                                        <p class="excerpt">
                                            <asp:Label ID="lbl_content3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Dheya's Networks</h2>
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
                        <center>
                                <div id="chart_div1" style="width: 100%; height: 100%;"></div>
                                    </center>
                    </div>
                </div>

                <div class="x_panel">

                    <div class="x_title">
                        <h2>Dheya Updates</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <asp:TextBox ID="lblDheyaUpdates" runat="server" TextMode="MultiLine" Width="650px" Height="100px" Wrap="true"></asp:TextBox>
                    </div>

                </div>
            </div>
        </div>

         <!-- Modal Lead Details - Converted-->
        <div class="modal fade" id="myModal_lead_converted" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Lead Converted</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div_leadstatus_converted" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            
                            <div class="row">
                                <asp:GridView ID="gv_lead_converted" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive" EmptyDataText="No Data"
                            Width="100%" OnPageIndexChanging="gv_lead_converted_PageIndexChanging"
                            PageSize="5">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="false">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
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

        <!-- Modal Lead Details - Created-->
        <div class="modal fade" id="myModal_lead_created" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Lead Created</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div_leadstatus_created" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>

                            <div class="row">
                                <asp:GridView ID="gv_lead_created" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                            Width="100%" OnPageIndexChanging="gv_lead_created_PageIndexChanging"
                            PageSize="5">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="false">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
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

    <%-- <div class="row" style="margin-bottom: 30px;">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Dheya Summer Offers</h2>
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
                    <center><img src="images/Dheya-Summer-Bonanza.jpg" alt="Dheya Summer Bonanza" style="height:372px; width:651px;"/></center>
                    </div>
                </div>
            </div>
        </div>--%>

    <!-- jQuery -->
    <script src="js/custom.min.js"></script>
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript'>
        google.load('visualization', '1', { 'packages': ['geochart'] });
        google.setOnLoadCallback(drawMarkersMap);

        function drawMarkersMap() {
            var data = google.visualization.arrayToDataTable([
               ['State', 'Total Number of CDF'],
              ['West Bengal', 29],
              ['Maharashtra', 226],
              ['Uttar Pradesh', 21],
              ['Madhya Pradesh', 11],
              ['Tamil Nadu', 61],
              ['Rajasthan', 12],
              ['Karnataka', 111],
              ['Gujarat', 50],
              ['Andhra Pradesh', 1],
              ['Orissa', 5],
              ['Telangana', 49],
              ['Chhattisgarh', 1],
              ['Jharkhand', 7],
              ['Bihar', 1],
              ['Punjab', 1],
              ['Haryana', 11],
              ['Delhi', 21],
              ['Goa', 2],
              ['Tripura', 1],
              ['Uttarakhand', 1],
            ]);

            var options = {
                region: 'IN',
                displayMode: 'regions',
                resolution: 'provinces',
                colorAxis: { colors: ['#81aff9', '#4374e0'] },
                backgroundColor: '#ffffff',
            };

            var chart = new google.visualization.GeoChart(document.getElementById('chart_div1'));
            chart.draw(data, options);
        };
    </script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">


        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                legend: 'none',
                //pieSliceText: 'label',
                slices: {
                    0: { color: '#3498DB' },
                    1: { color: '#9CC2CB' }
                },
                pieHole: 0.4
            };
            $.ajax({
                type: "POST",
                url: "home.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.PieChart($("#chart11")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>
</asp:Content>

