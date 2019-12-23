<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true"
    CodeFile="Update_Info.aspx.cs" Inherits="Admin_UpdateInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/jquery.datepick.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        #dvPreview {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
            min-height: 400px;
            min-width: 400px;
            display: none;
        }

        .row {
            padding: 1px;
        }

        .leftalign {
            float: left;
        }

        html, body {
            height: 100%; /* The html and body elements cannot have any padding or margin. */
        }

        /* Wrapper for page content to push down footer */
        #wrap {
            min-height: 100%;
            height: auto; /* Negative indent footer by its height */
            margin: 0 auto -60px; /* Pad bottom by footer height */
            padding: 0 0 60px;
        }

        /* Set the fixed height of the footer here */
        #footer {
            height: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div class="x_panel">
            <div class="x_title">
                <h2>Update User Information</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div id="div_con2" class="container" runat="server" align="center" style="margin-bottom: 10px;">
                    <div class="row">
                        <asp:Panel ID="pan_info" runat="server" Enabled="true">
                            <div class="row" style="margin-top: 25px;">
                                <div class="alert " id="div_msg" runat="server" style="text-align: center; max-width: 80%;">
                                </div>
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">First Name:*
                                    <asp:RequiredFieldValidator ID="rfv_fname" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_fname" runat="server" ErrorMessage="First Name is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_fname" runat="server" placeholder="First Name" class="form-first-name form-control"
                                            MaxLength="100"></asp:TextBox>
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
                                            MaxLength="100"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                            TargetControlID="txt_lname" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Gender:*
                                    <asp:RequiredFieldValidator ID="rfGender" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_gender" runat="server" ErrorMessage="Gender field is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select an Option</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">Date Of Birth: (dd/mm/yyyy)*
                                    <asp:RequiredFieldValidator ID="rfDob" CssClass="Validators" Display="Static" ControlToValidate="tbDate1"
                                        runat="server" ErrorMessage="Date of birth is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Static"
                                                ErrorMessage="Invalid Date." ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
                                                ControlToValidate="tbDate1" ForeColor="Red">*</asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox class="form-control" ID="tbDate1" runat="server" placeholder="Enter DOB"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                            ValidChars="/ -" TargetControlID="tbDate1" />
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
                                        <span class="leftalign">Present Postal Address:
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_address" runat="server" ErrorMessage="Address is required."
                                        ForeColor="Red" Enabled="False">*</asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txt_address" runat="server" placeholder="Postal Address" class="form-first-name form-control"
                                            MaxLength="500"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;() &quot;#:&" TargetControlID="txt_address" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">State:*
                                    <asp:RequiredFieldValidator ID="rfState" CssClass="Validators" Display="Static" ControlToValidate="ddl_state"
                                        runat="server" ErrorMessage="State is required." InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control"
                                            OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5 ">
                                    <div class="form-group">
                                        <span class="leftalign">City:*
                                    <asp:RequiredFieldValidator ID="rfCity" CssClass="Validators" Display="Static" ControlToValidate="ddl_city"
                                        runat="server" ErrorMessage="City is required." InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
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
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Email Id:
                                    <asp:RequiredFieldValidator ID="rfemail" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_email" runat="server" ErrorMessage="Email id is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                                meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red">*</asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txt_email" runat="server" placeholder="Email Id" class="form-first-name form-control"
                                            MaxLength="100"></asp:TextBox>


                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">CDF Level:
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="Validators" Display="Static"
                                                ControlToValidate="ddl_cdfLevel" runat="server" ErrorMessage="CDF Level is required."
                                                ForeColor="Red" Enabled="False" InitialValue="Select">*</asp:RequiredFieldValidator></span>

                                        <asp:DropDownList ID="ddl_cdfLevel" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Refund Amount:
                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="txt_refamt" />
                                        </span>
                                        <asp:TextBox ID="txt_refamt" runat="server" placeholder="Refund Amount Id" class="form-first-name form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">Refund Status:
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" Display="Static"
                                                   ControlToValidate="ddl_refsts" runat="server" ErrorMessage="Refund status required"
                                                   ForeColor="Red" InitialValue="Select">*</asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddl_refsts" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Classification:</span>
                                          <asp:DropDownList ID="ddl_classification" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">--Select--</asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">Password:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_password" runat="server" ErrorMessage="Password is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_password" runat="server" placeholder="Password" ReadOnly="true" class="form-first-name form-control"
                                            MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">About yourself (MAX 1000 Characters): </span>
                                        <asp:TextBox ID="txt_profile" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;"
                                            placeholder="Your Profile" MaxLength="300"></asp:TextBox>
                                        <asp:RegularExpressionValidator Style="float: left" ID="RegularExpressionValidator4"
                                            ControlToValidate="txt_profile" runat="server" ValidationExpression="^[\s\S]{0,1000}$"
                                            Text="1000 characters max" />
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;() &quot;" TargetControlID="txt_profile" />
                                        <div id="counter" style="float:right;color:red;">
                                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="moreInfo" runat="server" visible="false">
                                <h2>More Details(only for Super-Admin):</h2>
                                <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Field of Work:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_fieldOfWork" runat="server" ErrorMessage="Field of Work field is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_fieldOfWork" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                             <span class="leftalign">Mode Of Work:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_modeOfWork" runat="server" ErrorMessage="Mode Of Work field is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />

                                        <asp:DropDownList ID="ddl_modeOfWork" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select an Option">Select an Option</asp:ListItem>
                                            <asp:ListItem Value="Job">Job</asp:ListItem>
                                            <asp:ListItem Value="Self Employed">Self Employed</asp:ListItem>
                                            <asp:ListItem Value="Business">Business</asp:ListItem>
                                            <asp:ListItem Value="Entrepreneur">Entrepreneur</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Industry Sector:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_industrySector" runat="server" ErrorMessage="Industry Sector field is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_industrySector" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                             <span class="leftalign">New:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_New" runat="server" ErrorMessage="Industry Sector field is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_New" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </div>

                            <h2>Bank Details:</h2>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Account Holder Name:</span>
                                            <asp:TextBox ID="txt_accountHolderName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">Account Number:</span>
                                    <asp:TextBox ID="txt_accountNumber" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Bank Name:</span>
                                            <asp:TextBox ID="txt_bankName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <span class="leftalign">Branch Name:</span>
                                    <asp:TextBox ID="txt_branchName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">IFSC Code:</span>
                                            <asp:TextBox ID="txt_ifscNo" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                       
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                          <div class="ln_solid"></div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-4  col-md-offset-4">
                                    <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block" Style="margin-bottom: 20px;"
                                        Text="Update" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
                    </div>
                </div>

            <asp:HiddenField ID="hd_id" runat="server" />

        </div>
        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>

    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../js/MaxLength.min.js"></script>
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
