<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterMaster.master" AutoEventWireup="true"
    CodeFile="cdf-registration.aspx.cs" Inherits="cdf_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Registration Form</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="container" style="padding-top: 20px; margin-bottom: 10px;">
                    <div class="row">
                        <div class="" id="div_msg" runat="server" style="text-align: center; margin-left: 70px; margin-right: 70px;">
                        </div>
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">First Name:*
                                    <asp:RequiredFieldValidator ID="rfv_fname" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_fname" runat="server" ErrorMessage="First Name is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:TextBox ID="txt_fname" runat="server" placeholder="First Name" class="form-first-name form-control"
                                    MaxLength="50"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                    TargetControlID="txt_fname" ValidChars=". " />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Last Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_lname" runat="server" ErrorMessage="Last Name is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:TextBox ID="txt_lname" runat="server" placeholder="Last Name" class="form-first-name form-control"
                                    MaxLength="50"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                    TargetControlID="txt_lname" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">Contact No:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_contact" runat="server" ErrorMessage="Contact No. is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:TextBox ID="txt_contact" runat="server" placeholder="Contact Number" class="form-first-name form-control"
                                    MaxLength="10"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                    TargetControlID="txt_contact" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Email Address : (User Name) *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Dynamic"
                                        ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                        meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txt_email" runat="server" class="form-control" placeholder="Your Email Id"
                                    ValidationGroup="GO" MaxLength="100" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">Password:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_password" runat="server" ErrorMessage="Password is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                            runat="server" ErrorMessage="Password length should be more than 6 and less than 15"
                                            ValidationExpression="(.{6,15})" ControlToValidate="txt_password" Display="Dynamic"
                                            ForeColor="Red">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txt_password" runat="server" placeholder="length should be than 6 to 15"
                                    class="form-first-name form-control" MaxLength="15" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Confirm Password:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_conpassword" runat="server" ErrorMessage="Confirm password is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1"
                                            runat="server" ErrorMessage="Password is not match" ControlToCompare="txt_password"
                                            ControlToValidate="txt_conpassword" ForeColor="Red">*</asp:CompareValidator>
                                </span>
                                <asp:TextBox ID="txt_conpassword" runat="server" placeholder="Confirm Password" class="form-first-name form-control"
                                    MaxLength="15" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">                       
                        <%--<div class="col-sm-4">
                        </div>--%>
                        <div class="col-md-6 col-md-offset-4">
                            <div style="font-size: 14px;">
                                <asp:CheckBox ID="chkTerms" runat="server" />
                                &nbsp; I accept the <u><a href="https://www.dheya.com/terms-and-conditions/" target="_blank">Terms and Conditions</a></u> </div>
                        </div>
                       <%-- <div class="col-sm-4">
                        </div>--%>
                    </div>
                    <div class="row" style="margin: 20px">
                        <div class="form-group">
                            <div class="col-md-3 col-md-offset-3 ">
                                <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    Text="Submit" OnClick="btn_submit_Click" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btn_clear" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    ValidationGroup="clear" Text="Clear" OnClick="btn_clear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btn_submit.ClientID %>").disabled = true;
            document.getElementById("<%=btn_submit.ClientID %>").value = "Submiting..."
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        disableSelection(document.body) //disable text selection on entire body of page
    </script>

    <script type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>
