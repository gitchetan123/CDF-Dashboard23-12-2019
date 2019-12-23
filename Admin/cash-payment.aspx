<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="cash-payment.aspx.cs" Inherits="cashpayment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <h2>Add Payment Details<small><strong><i class="fa fa-user"></i> <asp:Label ID="lbl_name" runat="server"></asp:Label></strong></small></h2>
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
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>

                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Transaction ID :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_TransactionID" placeholder="Enter Transaction/receipt no" class="form-control"
                                runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=".,;() &quot;" TargetControlID="txt_TransactionID" />
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter transaction ID" ControlToValidate="txt_TransactionID">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Amount :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_amount" placeholder="Enter amount here" class="form-control"
                                runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                TargetControlID="txt_amount" ValidChars=". " />
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="rvAmount" runat="server" ErrorMessage="Please enter Amount" ControlToValidate="txt_amount">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Transaction Details :
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_details" placeholder="Transaction Details" class="form-control" runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=".,;() &quot;" TargetControlID="txt_details" />
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="rvCreateBy" runat="server" ErrorMessage="Please enter Transaction Details" ControlToValidate="txt_details">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Payment Date :
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_paymentDate" placeholder="(DD/MM/YYYY)" class="form-control" runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                ValidChars="/" TargetControlID="txt_paymentDate" />
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter transaction details" ControlToValidate="txt_paymentDate">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_paymentDate"
                                ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                ErrorMessage="Invalid date format.">*</asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="ln_solid"></div>

                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_payment" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Add Payment" OnClick="btn_payment_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grid_Pyment" runat="server"
                                    AllowPaging="True" CssClass="table" PageSize="50" DataKeyNames="id"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="productInfo" HeaderText="Product Name" SortExpression="productInfo" />
                                        <asp:BoundField DataField="txnId" HeaderText="txnId" SortExpression="txnId" />
                                        <asp:BoundField DataField="payDate" HeaderText="Registration Date" SortExpression="payDate"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                        <asp:BoundField DataField="paymentgateway" HeaderText="Payment Gateway" SortExpression="paymentgateway" />
                                    </Columns>
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:HiddenField ID="hf_id" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txt_paymentDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });
        });
    </script>
</asp:Content>

