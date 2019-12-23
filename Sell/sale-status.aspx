<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="sale-status.aspx.cs" Inherits="Sale_sale_status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 1px;
        }

        .filterspace {
            padding-left: 20px;
            padding-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Sell Status</h2>

                <ul class="nav navbar-right panel_toolbox">
                    <%--<li><a href="#" data-toggle="modal" data-target="#myModalHelpReportGraph"><i class="fa fa-question"></i></a>
                            </li>--%>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div>
                    <div class="row form-group" style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_search" class="form-control"
                                placeholder="Name, Contact No" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                   
                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_search" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Search" OnClick="btn_search_Click" />
                        </div>

                    </div>
                </div>

                <div style="height: 20px; width: 100%">
                    <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="grid_reffStatus" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        Width="100%" PageSize="15" AllowSorting="True" EmptyDataText="no record found" OnPageIndexChanging="grid_reffStatus_PageIndexChanging">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="uId" HeaderText="User Id" Visible="false" />
                            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                            <asp:BoundField DataField="Contact" HeaderText="Contact" />
                            <asp:BoundField DataField="Email" HeaderText="Email ID" />
                            <asp:BoundField DataField="Refer_status" HeaderText="Refer Type" />
                            <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />
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

        <!-- Custom Theme Scripts -->
        <script src="../js/custom.min.js"></script>
        <script type="text/javascript">
            function UserApproved() {
                return confirm("Are you sure you want to Approved this user?");
            }
            function UserActive() {
                return confirm("Are you sure you want to Active this user?");
            }
            function UserDeactive() {
                return confirm("Are you sure you want to Deactive this user?");
            }
        </script>
    </form>
</asp:Content>

