<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="sell-details.aspx.cs" Inherits="Admin_Sell_sell_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>User Details</h2>
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
                <div>
                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_Search" placeholder="Candidate Name or Email or Contact No." class="form-control"
                                runat="server"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_Search_Ref" placeholder="CDF Email (Refer By Email)" class="form-control"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By Executive :</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddl_executiveName" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By Payment Status :</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddl_Payment_Status" CssClass="form-control" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="ACTIVE">Paid</asp:ListItem>
                                <asp:ListItem Value="INACTIVE">Not Paid</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-2">
                            <asp:Button ID="btn_preview" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Preview" OnClick="btn_preview_Click" />
                        </div>
                        <div class=" col-sm-2">
                            <asp:Button ID="btn_reset" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Reset" OnClick="btn_clear_Click" />
                        </div>

                    </div>

                </div>
                <div>
                    <div style="height: 20px; width: 100%">
                        <asp:Label ID="lbl_rowcount" CssClass="control-label" Style="float: left;" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grid_Pyment" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found ..."
                                    DataKeyNames="uId" AllowPaging="true" CssClass="table" PageSize="100"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grid_Pyment_RowCommand" OnPageIndexChanging="grid_Pyment_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>

                                        <asp:BoundField DataField="uId" HeaderText="uId" SortExpression="uId" Visible="false" />
                                        <%--<asp:TemplateField HeaderText="Payment Link">
                                            <ItemTemplate>
                                                    <asp:HyperLink ID="Details" runat="server" NavigateUrl='<%# string.Format("~/CPA/Executive/custom-payment-byAdmin.aspx?uId={0}&amount={1}&ProductId={2}&userTypeId={3}&emailId={4}", 
                                                        HttpUtility.UrlEncode(Eval("uId").ToString()),
                                                        HttpUtility.UrlEncode(Eval("amount").ToString()),
                                                         HttpUtility.UrlEncode(Eval("ProductId").ToString()),
                                                         HttpUtility.UrlEncode(Eval("userTypeId").ToString()),
                                                         HttpUtility.UrlEncode(Eval("email").ToString())) %>'>Send Payment Link</asp:HyperLink>
                                            </ItemTemplate>

                                        </asp:TemplateField>--%>

                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                        <asp:BoundField DataField="Contact" HeaderText="Contact No" SortExpression="Contact" />
                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" Visible="false" />
                                        <asp:BoundField DataField="ProductId" HeaderText="ProductId" SortExpression="ProductId" Visible="false" />
                                        <asp:BoundField DataField="ReferByEmail" HeaderText="Refer By (CDF Email)" SortExpression="ReferByEmail" />
                                        <asp:BoundField DataField="refType" HeaderText="Refer Type" SortExpression="refType" Visible="false" />
                                        <asp:BoundField DataField="LeadCategory" HeaderText="Lead Category" SortExpression="LeadCategory" />
                                        <asp:BoundField DataField="Program" HeaderText="Product" SortExpression="Program" />
                                        <asp:BoundField DataField="userStatus" HeaderText="Status" SortExpression="userStatus" Visible="false" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" SortExpression="PaymentStatus" />

                                    </Columns>
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:HiddenField ID="hf_id" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="cdf" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Advance Search</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Test Approval Status</span>
                                    <asp:DropDownList ID="ddl_testApproveStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="APPROVED">Approved</asp:ListItem>
                                        <asp:ListItem Value="NOTAPPROVED">Not Approved</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Test Complete Status</span>
                                    <asp:DropDownList ID="ddl_testCompStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="Complete">Complete</asp:ListItem>
                                        <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">CDF Batch</span>
                                    <asp:DropDownList ID="ddl_batch" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">CDF Level</span>
                                    <asp:DropDownList ID="ddl_cdfLevel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">City</span>
                                    <asp:DropDownList ID="ddl_city" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Search By </span>
                                    <asp:TextBox ID="txt_name" placeholder="First Name or Last Name or Email or Contact No." class="form-control"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">From                                                               
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from"
                                     ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                     ErrorMessage="Invalid date format.">*</asp:RegularExpressionValidator></span>
                                    <asp:TextBox ID="txt_from" placeholder="(DD/MM/YYYY)" class="form-control" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="/" TargetControlID="txt_from" />
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">To                                                              
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to"
                                    ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                    ErrorMessage="Invalid date format.">*</asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox ID="txt_to" placeholder="(DD/MM/YYYY)" class="form-control" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="/" TargetControlID="txt_to" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">CDF Approval Status</span>
                                    <asp:DropDownList ID="ddl_cdfAproveStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="APPROVED">Approved</asp:ListItem>
                                        <asp:ListItem Value="NOTAPPROVED">Not Approved</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                            </div>
                        </div>

                        <div class="row" style="margin-top: 20px;">
                            <div class="form-group">
                                <div class="col-md-4 col-md-offset-2">
                                    <%-- <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Preview" OnClick="btn_advance_preview_Click" />--%>
                                </div>
                                <div class="col-md-4">
                                    <%--<asp:Button ID="btn_clear" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Clear" OnClick="btn_clear_Click" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../../js/custom.min.js"></script>
    <script src="http://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txt_from").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

            $("#ctl00_ContentPlaceHolder1_txt_to").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

        });
    </script>
</asp:Content>

