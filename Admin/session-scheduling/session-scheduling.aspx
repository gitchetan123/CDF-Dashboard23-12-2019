<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true"
    CodeFile="session-scheduling.aspx.cs" Inherits="admin_sessionScheduling_sessionScheduling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--calender--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Session Scheduling</h2>
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
                                <span class="leftalign">Candidate Name:*
                                    </span>
                                <asp:TextBox ID="txt_fname" runat="server" placeholder="Name" class="form-first-name form-control"
                                    MaxLength="50" Enabled="false"></asp:TextBox>
                              
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">CDF Level:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_CDFLevel" runat="server" ErrorMessage="CDF Level is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                <asp:DropDownList ID="ddl_CDFLevel" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_CDFLevel_SelectedIndexChanged">
                                    <asp:ListItem>Select an Option</asp:ListItem>
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
                                <span class="leftalign">CDF Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_cdf" runat="server" ErrorMessage="CDF is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_cdf" runat="server" class="form-control">
                                </asp:DropDownList>
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
                                <span class="leftalign">Product Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_product" runat="server" ErrorMessage="Product is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_product" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Session Level*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_sessionLevel" runat="server" ErrorMessage="Product is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_sessionLevel" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">Date *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_date" runat="server" ErrorMessage="session Date is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Static"
                                        ErrorMessage="Invalid Date." ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
                                        ControlToValidate="txt_date" ForeColor="Red">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox class="form-control" ID="txt_date" runat="server" placeholder="Enter Date"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="/" TargetControlID="txt_date" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Time Slot *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_timeSlot" runat="server" ErrorMessage="Time Slot is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_timeSlot" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">State:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_state" runat="server" ErrorMessage="State is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_state" runat="server" class="form-control"
                                    OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <span class="leftalign">City:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_city" runat="server" ErrorMessage="City is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_city" runat="server" class="form-control"
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
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">Session Type:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_sessionType" runat="server" ErrorMessage="Product is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_sessionType" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <span class="leftalign">Status *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_Status" runat="server" ErrorMessage="Product is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddl_Status" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">Details *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_details" runat="server" ErrorMessage="Password is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                <asp:TextBox ID="txt_details" runat="server" placeholder="Details is required"
                                    class="form-first-name form-control" MaxLength="15" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                       
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Shadow CDF Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_ShadowCdf" runat="server" ErrorMessage="CDF is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddl_ShadowCdf" runat="server" class="form-control">
                                    </asp:DropDownList>
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
    <script src="../../js/custom.min.js"></script>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
           

            $("#ctl00_ContentPlaceHolder1_txt_date").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

        });
    </script>

</asp:Content>
