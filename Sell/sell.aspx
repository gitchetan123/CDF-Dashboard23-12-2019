<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="sell.aspx.cs" Inherits="Sale_sell" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show');
            //$('input:radio[name="name_full_fees"]').filter('[value="value_full_fees"]').attr('checked', true);
        }
    </script>

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
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                <div>
                    <div class="row form-group" style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-2 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txt_search" class="form-control"
                                placeholder="Name, Contact No" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                            <asp:Button ID="btn_search" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Search" OnClick="btn_search_Click" />
                        </div>
                    </div>
                </div>
                <div class="ln_solid"></div>
                <div>
                    <div style="height: 20px; width: 100%">
                        <asp:Label ID="lbl_rowcount" CssClass="control-label" Style="float: left;" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grid_Pyment" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="uId" AllowPaging="true" CssClass="table" PageSize="100" EmptyDataText="no record found"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grid_Pyment_RowCommand" OnPageIndexChanging="grid_Pyment_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>
                                        <%--<asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id" />--%>
                                        <asp:BoundField DataField="uId" HeaderText="uId" SortExpression="uId" Visible="false" />

                                        <asp:TemplateField HeaderText="Send Payment Link" ItemStyle-HorizontalAlign="Justify">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="Details" runat="server" NavigateUrl='<%# string.Format("~/Sale/custom-payment.aspx?uId={0}&amount={1}&ProductId={2}&userTypeId={3}&emailId={4}", 
                                                        HttpUtility.UrlEncode(Eval("uId").ToString()),
                                                        HttpUtility.UrlEncode(Eval("amount").ToString()),
                                                         HttpUtility.UrlEncode(Eval("ProductId").ToString()),
                                                         HttpUtility.UrlEncode(Eval("userTypeId").ToString()),
                                                         HttpUtility.UrlEncode(Eval("email").ToString())) %>'>Send Payment Link</asp:HyperLink>--%>


                                                <asp:LinkButton ID="lnView" runat="server" CommandName="View" ToolTip="click here to send payment link to selected candidate" CommandArgument='<%# Eval("uId") %>'>Click to Send</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Program" HeaderText="Product" SortExpression="Program" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                        <asp:BoundField DataField="Contact" HeaderText="Contact No" SortExpression="Contact" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <%--<asp:BoundField DataField="executiveId" HeaderText="executiveId" SortExpression="executiveId" />--%>
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="userStatus" HeaderText="Status" SortExpression="userStatus" Visible="false" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" SortExpression="userStatus" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" Visible="false" />
                                        <asp:BoundField DataField="ProductId" HeaderText="ProductId" SortExpression="ProductId" Visible="false" />

                                        <%--<asp:BoundField DataField="exeName" HeaderText="Executive Name" SortExpression="exeName" />
                                        <asp:BoundField DataField="teststatus" HeaderText="Test Status" SortExpression="teststatus" />
                                        <asp:BoundField DataField="TestApproval" HeaderText="Test Approval" SortExpression="TestApproval" />--%>




                                        <%-- <asp:TemplateField HeaderText="Active/Deactive">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <asp:Button ID="Button2" CssClass="btn btn-danger btn-sm" Visible='<%#  (Eval("status").ToString()) == "ACTIVE" %>'
                                                            runat="server" Text="Deactive" OnClientClick="if ( ! UserDeactive()) return false;"
                                                            CommandArgument='<%# Eval("id")%>'  />

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>

                        <h4 class="modal-title"><a><i class="fa fa-credit-card"></i>&nbsp;&nbsp;Fill the Payment Details</a></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rblfull" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl5" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl10" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl15" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl20" EventName="CheckedChanged" />
                                    <%--<asp:AsyncPostBackTrigger ControlID="rbl0" EventName="CheckedChanged" />--%>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-md-8 col-md-offset-2">
                                        <div class="form-group">
                                            <span class="leftalign">Product Price :*</span>
                                            <asp:TextBox ID="txt_amount" placeholder="" class="form-control" Text="" ReadOnly="true"
                                                runat="server"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                TargetControlID="txt_amount" ValidChars=". " />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-9 col-md-offset-2">
                                <div class="form-group">
                                    <span class="leftalign">Any discount offers :*</span><br />
                                    <br />
                                    <asp:RadioButton ID="rblfull" runat="server" Text=" Full Fees" GroupName="discount" OnCheckedChanged="rblfull_CheckedChanged" AutoPostBack="true" />
                                    &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl5" runat="server" Text="  5 %" GroupName="discount" OnCheckedChanged="rbl5_CheckedChanged" AutoPostBack="true" />
                                    &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl10" runat="server" Text="  10 %" GroupName="discount" OnCheckedChanged="rbl10_CheckedChanged" AutoPostBack="true" />
                                    &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl15" runat="server" Text="  15 %" GroupName="discount" OnCheckedChanged="rbl15_CheckedChanged" AutoPostBack="true" />
                                    &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl20" runat="server" Text="  20 %" GroupName="discount" OnCheckedChanged="rbl20_CheckedChanged" AutoPostBack="true" />
                                      <%--&nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl0" runat="server" Text="  0 %" GroupName="discount" OnCheckedChanged="rbl0_CheckedChanged" AutoPostBack="true" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 20px;">
                            <div class="form-group">
                                <div class="col-md-4 col-md-offset-2">
                                    <asp:Button ID="btn_payment" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Send" OnClick="btn_payment_Click" ValidationGroup="payment" />


                                    <asp:HiddenField ID="FullFees" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

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
    <script>
        function rblfull_checked() {
            alert('hi')
        }
    </script>
</asp:Content>

