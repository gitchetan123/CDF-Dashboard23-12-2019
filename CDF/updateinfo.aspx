<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="updateinfo.aspx.cs" Inherits="updateinfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <%--<link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="../css/custom.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>CDF Information</h2>
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
                        <div id="demo-form2" class="form-horizontal form-label-left">
                            <asp:Panel ID="pan" runat="server" Enabled="False">
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        First Name:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_fname" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="First Name is required." ControlToValidate="txt_fname"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="last-name" class="control-label col-md-4 col-sm-4 col-xs-12">Last Name:</label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_lname" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_lname" runat="server" ErrorMessage="Last Name is requied." ControlToValidate="txt_lname"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Date Of Birth:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_dob" class="form-control col-md-7 col-xs-12" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_dob" runat="server" ErrorMessage="Birth Date is requied." ControlToValidate="txt_dob"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Contact No.:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_contactno" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_contactno" runat="server" ErrorMessage="Contact No. is required." ControlToValidate="txt_contactno"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_contactno" runat="server" ErrorMessage="Only Numbers allowed"
                                            ValidationExpression="\d+" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="address">
                                        Address:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txtAddress" class="form-control col-md-7 col-xs-12" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Address is required." ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        State:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control"
                                            OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfState" CssClass="Validators" Display="Static" ControlToValidate="ddl_state"
                                            runat="server" ErrorMessage="State is required." InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        City:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_city" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_state" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfCity" CssClass="Validators" Display="Static" ControlToValidate="ddl_city"
                                            runat="server" ErrorMessage="City is required." InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Pin Code:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox class="form-control col-md-7 col-xs-12" ID="txt_pincode" runat="server" placeholder="Pin Code" MaxLength="6"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                            TargetControlID="txt_pincode" />
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_pincode" runat="server" ErrorMessage="Pin Code is required." ControlToValidate="txt_pincode"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">Gender:</label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList class="form-control" ID="ddl_gender" runat="server">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Qualification:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox class="form-control col-md-7 col-xs-12" ID="txt_qualification" runat="server" placeholder="Qualification" MaxLength="500"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Designation:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox class="form-control col-md-7 col-xs-12" ID="txt_designation" runat="server" placeholder="Designation" MaxLength="500"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Total Years of Experience:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddl_yearsOfExperience" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Description:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox class="form-control col-md-7 col-xs-12" ID="txt_desc" runat="server" placeholder="Description" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                        <div id="counter" style="float: right; color: red;">
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Field of Work:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddl_fieldOfWork" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Mode Of Work:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddl_modeOfWork" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select an Option">Select an Option</asp:ListItem>
                                            <asp:ListItem Value="Job">Job</asp:ListItem>
                                            <asp:ListItem Value="Self Employed">Self Employed</asp:ListItem>
                                            <asp:ListItem Value="Business">Business</asp:ListItem>
                                            <asp:ListItem Value="Entrepreneur">Entrepreneur</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Industry Sector:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddl_industrySector" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                            </asp:Panel>
                            <div class="ln_solid"></div>

                            <div class="form-group ">
                                <asp:Button ID="btn_edit_update" class="btn btn-primary col-md-2 col-sm-12 col-xs-12 col-md-offset-5" runat="server" Text="Update" Visible="false" OnClick="btn_edit_update_Click" />

                                <asp:Button ID="btn_edit" class="btn btn-primary col-md-2 col-sm-12 col-xs-12 col-md-offset-5" runat="server" Text="Edit" CausesValidation="false" OnClick="btn_edit_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- bootstrap-daterangepicker -->

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../js/MaxLength.min.js"></script>
    <script>
        $(function () {

            $("#ctl00_ContentPlaceHolder1_txt_dob").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });
            //$("[id*=txt_desc]").MaxLength({ MaxLength: 1000, CharacterCountControl: $('#counter') });
        });
    </script>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>

</asp:Content>

