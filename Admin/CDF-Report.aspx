<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="CDF-Report.aspx.cs" Inherits="Admin_CDF_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" class="form-horizontal" role="form" runat="server">       
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Report</h2>
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
         <div style="height: 20px; width: 100%">
                        <asp:Label ID="lbl_rowcount" CssClass="control-label" Style="float: left;" runat="server" Text=""></asp:Label>
                         <div id="div_Error" runat="server" class="" style="text-align: center;">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                </div>
                    </div>
                            <div class="col-md-12">
                                <asp:GridView ID="grid_CDFReport" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        Width="100%" OnPageIndexChanging="grid_CDFReport_PageIndexChanging"  PageSize="15">                                
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>
                      <%--                  <asp:BoundField DataField="batchId" HeaderText="BatchId" SortExpression="batchId" />     --%>                                  
                                        <asp:BoundField DataField="batchName" HeaderText="BatchName" SortExpression="batchName" />
                                        <asp:BoundField DataField="BatchCount" HeaderText="Batch Count" SortExpression="BatchCount" />
                                        <asp:BoundField DataField="ChildTest" HeaderText="ChildTest" SortExpression="ChildTest" />
                                        <asp:BoundField DataField="ChildSession" HeaderText="ChildSession" SortExpression="ChildSession" />
                                        <asp:BoundField DataField="SpouseTest" HeaderText="SpouseTest" SortExpression="SpouseTest" />
                                        <asp:BoundField DataField="ActiveCDF" HeaderText="ActiveCDF" SortExpression="ActiveCDF" />
                                        <asp:BoundField DataField="DeactiveCDF" HeaderText="DeactiveCDF" SortExpression="DeactiveCDF" />
                                        <asp:BoundField DataField="Level1" HeaderText="Level1" SortExpression="Level1" />
                                        <asp:BoundField DataField="Level2" HeaderText="Level2" SortExpression="Level2" />
                                      <%--<asp:BoundField DataField="Level3" HeaderText="Level3" SortExpression="Level3" />
                                        <asp:BoundField DataField="Level4" HeaderText="Level4" SortExpression="Level4" />--%>
                                        <asp:BoundField DataField="RefundCount" HeaderText="RefundCount" SortExpression="RefundCount" />
                                        <asp:BoundField DataField="RefAmountCount" HeaderText="RefAmountCount" SortExpression="RefAmountCount" />

                                      
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
        </form>
        <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

