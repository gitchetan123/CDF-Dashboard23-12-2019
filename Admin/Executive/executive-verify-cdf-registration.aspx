<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Executive/executive-master.master" AutoEventWireup="true" CodeFile="executive-verify-cdf-registration.aspx.cs" Inherits="executiveVerifycdfRegistration" %>


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
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Email :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_email" placeholder="Enter Email Id" class="form-control"
                                runat="server"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Dynamic"
                                ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                ForeColor="Red" ValidationGroup="cdf">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red" ValidationGroup="cdf">*</asp:RegularExpressionValidator>
                        </div>
                    </div>

                     <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Mobile :</label>
                        <div class="col-sm-6">
                             <asp:TextBox ID="txt_contact" runat="server" placeholder="Contact Number" class="form-control"
                                    MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_contact" runat="server" ErrorMessage="Contact No. is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                        
                    </div>

                    <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Executive Name:*
                        </label>
                        <div class="col-sm-6">

                            <asp:DropDownList ID="ddl_executiveName" runat="server" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                ControlToValidate="ddl_executiveName" runat="server" ErrorMessage="Executive name is required."
                                InitialValue="--Select--" ForeColor="Red" ValidationGroup="cdf">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_payment" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Create" OnClick="btn_payment_Click" ValidationGroup="cdf" />
                        </div>
                        <div class=" col-sm-3">
                            <%-- <asp:Button ID="btn_export" Text="Export" runat="server" CssClass="btn btn-primary btn-block btn1"
                                OnClick="btn_export_Click" />--%>
                            <a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="Create ticket to report issues." tabindex="5" data-target="#myModal">Advance Search</a>
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
                                <asp:GridView ID="grid_Pyment" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="id" AllowPaging="true" CssClass="table" PageSize="100"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grid_Pyment_RowCommand" OnPageIndexChanging="grid_Pyment_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>
                                        <%--<asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id" />--%>
                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />

                                        <asp:TemplateField HeaderText="More Details">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="Details" Target="_blank" runat="server" NavigateUrl='<%# Eval("id", "~/Admin/Executive/executive-custom-payment.aspx?id={0}") %>'
                                                    CssClass="bodytext">More Info...</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                        <%--<asp:BoundField DataField="executiveId" HeaderText="executiveId" SortExpression="executiveId" />--%>
                                        <asp:BoundField DataField="createDate" HeaderText="Created Date" SortExpression="createDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="exeName" HeaderText="Executive Name" SortExpression="exeName" />
                                        <asp:BoundField DataField="teststatus" HeaderText="Test Status" SortExpression="teststatus" />
                                        <asp:BoundField DataField="TestApproval" HeaderText="Test Approval" SortExpression="TestApproval" />
                                        <asp:BoundField DataField="TotalPayment" HeaderText="Payment" SortExpression="TotalPayment" />

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
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Preview" OnClick="btn_advance_preview_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Clear" OnClick="btn_clear_Click" />
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
</asp:Content>

