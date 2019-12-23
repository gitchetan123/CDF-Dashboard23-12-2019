<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterMaster.master" AutoEventWireup="true" CodeFile="manual-registration.aspx.cs" Inherits="manual_registration" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.datepick.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
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
                <div class="row">
                <div class="col-md-8 col-md-offset-2 alert alert-danger" id="div_msg" runat="server" style="text-align: center; "></div>
                    </div>
                <div class="row" style="margin-top: 10px;">

                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
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
                    <div class="col-md-5 col-sm-10 col-xs-10">
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
                            <span class="leftalign">Email Address : (Personal Email Id) *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Dynamic"
                                        ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                    meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red">*</asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox ID="txt_email" runat="server" class="form-control" placeholder="Your Email Id" ReadOnly="true"
                                ValidationGroup="GO" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">Gender:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_gender" runat="server" ErrorMessage="Gender field is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                            <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-control" SkinID="DropDown">
                                <asp:ListItem>Select an Option</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-10 col-xs-10">
                        <div class="form-group">
                            <span class="leftalign">Date Of Birth: (dd/mm/yyyy)*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" Display="Static"
                                        ControlToValidate="tbDate1" runat="server" ErrorMessage="Date of birth is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Static"
                                    ErrorMessage="Invalid Date." ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
                                    ControlToValidate="tbDate1" ForeColor="Red">*</asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator1" ForeColor="Red" runat="server" ControlToValidate="tbDate1"
                                    ControlToCompare="txtEndDate" Operator="LessThan" Type="Date" ErrorMessage="Your age must be above 12+ year.">*</asp:CompareValidator>
                            </span>
                            <asp:TextBox class="form-control" ID="tbDate1" runat="server" placeholder="Enter DOB"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                ValidChars="/" TargetControlID="tbDate1" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">State:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_state" runat="server" ErrorMessage="State is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                            <asp:DropDownList ID="ddl_state" runat="server" SkinID="DropDown" class="form-control"
                                OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-10 col-xs-10">
                        <div class="form-group">
                            <span class="leftalign">City:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_city" runat="server" ErrorMessage="City is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_city" runat="server" SkinID="DropDown" class="form-control"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_state" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">Qualification:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_qualification" runat="server" ErrorMessage="Qualification field is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                            <asp:TextBox ID="txt_qualification" runat="server" placeholder="Qualification" class="form-first-name form-control"
                                MaxLength="50"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                TargetControlID="txt_qualification" ValidChars="., " />
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-10 col-xs-10">
                        <div class="form-group">
                            <span class="leftalign">Present Postal Address:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_address" runat="server" ErrorMessage="Address is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_address" runat="server" placeholder="Postal Address" class="form-first-name form-control"
                                MaxLength="500"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=".,;()#:/\ " TargetControlID="txt_address" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">Current Designation:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_designation" runat="server" ErrorMessage="Current Designation is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                            <asp:TextBox ID="txt_designation" runat="server" placeholder="Current Designation"
                                MaxLength="50" class="form-first-name form-control"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                TargetControlID="txt_designation" ValidChars="., " />
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-10 col-xs-10">
                        <div class="form-group">
                            <span class="leftalign">Why looking for this opportunity?:</span>
                            <asp:TextBox ID="txt_why_opportunity" runat="server" placeholder="Why opportunity?"
                                class="form-first-name form-control" MaxLength="200"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                TargetControlID="txt_why_opportunity" ValidChars="., " />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">Are you married?*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_married_status" runat="server" ErrorMessage="Married status field is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                            <asp:DropDownList ID="ddl_married_status" runat="server" CssClass="form-control"
                                SkinID="DropDown" AutoPostBack="True" OnSelectedIndexChanged="ddl_married_status_SelectedIndexChanged">
                                <asp:ListItem>Select an Option</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-5 col-sm-10 col-xs-10">
                        <div class="form-group">
                            <span class="leftalign">Executive Name:</span>
                            <asp:DropDownList ID="ddl_ename" runat="server" CssClass="form-control" SkinID="DropDown">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%-- <div class="row">--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row" runat="server" id="div_married">
                            <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Spouse Name:</span>                             
                                    <asp:TextBox ID="txt_spouse" runat="server" placeholder="Spouse Name" class="form-first-name form-control"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                        TargetControlID="txt_spouse" ValidChars=" " />
                                </div>
                            </div>
                            <div class="col-md-5 col-sm-10 col-xs-10">
                                <div class="form-group">
                                    <span class="leftalign">Do you have Children? if Yes what Age?:</span>                                  
                                    <asp:TextBox ID="txt_children" runat="server" placeholder="Ex- 10, 12" class="form-first-name form-control"
                                        MaxLength="20"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="," TargetControlID="txt_children" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_married_status" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="col-md-5 col-sm-10 col-xs-10 col-md-offset-1">
                    <div class="form-group">
                        <span class="leftalign">Attache Photo (Passport size photo with plain background):</span>
                        <asp:FileUpload ID="file_image" onchange="validateFileSize();" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" Display="Dynamic"
                            ControlToValidate="file_image" runat="server" ErrorMessage="Photo is required."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="(([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.bmp|.GIF|.JPG|.JPEG|.PNG|.BMP)$)$"
                            ControlToValidate="file_image" runat="server" ForeColor="Red" ErrorMessage="Please select a valid png,jpg,jpeg or bmp File."
                            Display="Dynamic" /><br />
                        <div id="dvMsg" style="background-color: Red; color: White; width: 230px; padding: 3px; display: none;">
                            Maximum file size allowed is 2 MB
                        </div>
                    </div>
                </div>
                <div class="col-md-5 col-sm-10 col-xs-10">
                    <div class="form-group">
                        <span class="leftalign">Attache Photo (Casual photo):</span>
                        <asp:FileUpload ID="file_image2" onchange="validateFileSize2();" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" CssClass="Validators" Display="Dynamic"
                            ControlToValidate="file_image2" runat="server" ErrorMessage="Photo is required."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="(([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.bmp|.GIF|.JPG|.JPEG|.PNG|.BMP)$)$"
                            ControlToValidate="file_image2" runat="server" ForeColor="Red" ErrorMessage="Please select a valid png,jpg,jpeg or bmp File."
                            Display="Dynamic" /><br />
                        <div id="dvMsg2" style="background-color: Red; color: White; width: 230px; padding: 3px; display: none;">
                            Maximum file size allowed is 2 MB
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 col-sm-10 col-xs-10 col-md-offset-1">
                    <div class="form-group">
                        <span class="leftalign">Brief your profile (1000 Characters):*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_profile" runat="server" ErrorMessage="Your Profile is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                        <asp:TextBox ID="txt_profile" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;"
                            placeholder="Your Profile" MaxLength="1000"></asp:TextBox>
                        <asp:RegularExpressionValidator Style="float: left" ID="RegularExpressionValidator4"
                            ControlToValidate="txt_profile" runat="server" ValidationExpression="^[\s\S]{0,1000}$"
                            Text="1000 characters max"
                            ErrorMessage="Profile details max 1000 characters" />
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                            ValidChars=".,;()13" TargetControlID="txt_profile" />
                         <div id="counter" style="float:right;color:red;">
                                                        </div>
                    </div>
                </div>
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
        <asp:TextBox ID="txtEndDate" runat="server" Enabled="False" BorderStyle="None" Font-Size="0pt"
            Height="0px" Width="0px"></asp:TextBox>
       
    </form>


    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler.
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };
    </script>
    <!-- Custom Theme Scripts -->
    <script src="js/custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.plugin.js"></script>
    <script type="text/javascript" src="js/jquery.datepick.js"></script>
    <script type="text/javascript" src="js/MaxLength.min.js"></script>

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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_tbDate1").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

            $("[id*=txt_profile]").MaxLength({ MaxLength: 1000, CharacterCountControl: $('#counter') });
        });
    </script>
</asp:Content>

