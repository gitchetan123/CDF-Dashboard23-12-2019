<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="prospected-cdf.aspx.cs" Inherits="Admin_ExportData_prospected_cdf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Prospected CDF List</h2>
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
                            <asp:TextBox ID="txt_name" placeholder="First Name or Last Name or Email or Contact No." class="form-control"
                                runat="server"></asp:TextBox>
                            <%--<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                TargetControlID="txt_name" ValidChars=". @ _ " />--%>
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
                            <a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="click here to make filter" tabindex="5" data-target="#myModal">Advance Filter</a>
                        </div>
                        <div class=" col-sm-2">
                            <a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="click here to make filter" tabindex="5" data-target="#myModal_fields">Export to Excel</a>
                            <%--<asp:Button ID="btn_Export" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Export" OnClick="ExportToExcel"  />--%>
                        </div>
                    </div>
                </div>

                <div style="height: 20px; width: 100%">
                    <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        OnDataBound="GridView1_DataBound" Width="100%" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" OnRowCommand="GridView1_RowCommand"
                        AllowSorting="True" OnSorting="GridView1_Sorting">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="false" />
                            <%--<asp:TemplateField HeaderText="More Details">
                                <ItemTemplate>
                                    <asp:HyperLink ID="Details" Target="_blank" runat="server" NavigateUrl='<%# Eval("id", "~/Admin/candidate-details.aspx?id={0}") %>'
                                        CssClass="bodytext">More Info...</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%-- <asp:TemplateField HeaderText="More Details">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <asp:Button ID="btn_CandDetails" CssClass="btn btn-danger btn-sm" 
                                                            runat="server" Text="More Info..." 
                                                            CommandArgument='<%# Eval("id")%>'  CommandName="CandDetails" />

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                            <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" Visible="False" />
                            <asp:BoundField DataField="fname" HeaderText="First Name" SortExpression="fname">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lname" HeaderText="Last Name" SortExpression="lname">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                            <asp:BoundField DataField="regDateTime" HeaderText="Registration Date" SortExpression="regDateTime"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Teststatus" HeaderText="Test" SortExpression="Teststatus" />
                            <asp:BoundField DataField="TotalPayment" HeaderText="Total Payment" SortExpression="TotalPayment" />
                            <asp:BoundField DataField="Status" HeaderText="Approve" SortExpression="Status" />
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
                                    <span class="leftalign">Executive Name</span>
                                    <asp:DropDownList ID="ddl_ename" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
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
                        <%--<div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Payment Refundable Status</span>
                                    <asp:DropDownList ID="ddl_refundStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                              <div class="form-group">
                                    <span class="leftalign">Rating</span>
                                    <asp:DropDownList ID="ddl_rating" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="1.00">1</asp:ListItem>
                                        <asp:ListItem Value="2.00">2</asp:ListItem>
                                        <asp:ListItem Value="3.00">3</asp:ListItem>
                                        <asp:ListItem Value="4.00">4</asp:ListItem>
                                        <asp:ListItem Value="5.00">5</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>--%>

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




        <!-- Modal Select fields -->
        <div class="modal fade" id="myModal_fields" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Select Colomn Name</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div1" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            <asp:CheckBox ID="chk_all" Text="Select All" AutoPostBack="true" OnCheckedChanged="chk_all_click" CssClass="checkbox-inline" runat="server" />
                            <hr />
                            <asp:UpdatePanel ID="UpdatePanelPopup" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chk_all" EventName="CheckedChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="max-width: 100%">

                                        <div class="row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_fname" Checked="true" Text="First Name" CssClass="checkbox-inline" runat="server" />
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_lname" Checked="true" Text="Last Name" CssClass="checkbox-inline" runat="server" />
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_email" Checked="true" Text="Email" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_contact" Checked="true" Text="Contact" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_dob" Text="Birth Date" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_regDate" Text="Reg.Date" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_address" Text="Address" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_city" Text="City" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_testStatus" Text="Test Status" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_dateoftestcomplete" Text="Test Complete Date" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_status" Text="Work Status" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_userStatus" Text="User Acccess Status" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chk_exeName" Text="Executive Name" CssClass="checkbox-inline" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="row">
                                <div class=" col-md-offset-4 col-md-3">
                                    <asp:Button ID="btn_Export" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Export" OnClick="ExportToExcel" />
                                </div>
                                <div class=" col-md-1">
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

