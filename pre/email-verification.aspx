<%@ Page Title="" Language="C#" MasterPageFile="~/RegisterMaster.master" AutoEventWireup="true" CodeFile="email-verification.aspx.cs" Inherits="pre_email_verification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #dvPreview {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
            min-height: 400px;
            min-width: 400px;
            display: none;
        }

        .row {
            padding: 1x;
        }

        .panel {
            max-width: 750px;
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
    <form id="form1" runat="server">
        <div id="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="">
                <div id="div_con1" class="container" runat="server" visible="true" style="padding-top: 50px; margin-bottom: 80px;">
                    <center>
                        <div class="panel panel-primary">
                            <div class="panel-body" style="margin-top: 15px; margin-bottom: 20px;">
                                <h2 style="padding-bottom: 20px;">
                                    Verify Your Email ID</h2>
                                <img src="../images/vemail.png" alt="verify email" style="height:80px;width:80px;" />
                                <p style="padding-top: 10px;">
                                    For completing the Registration, please enter the same email ID which you had given during the Verification Process to our Executive.
                                    </p>
                                <h4 style="padding-bottom: 10px;">
                                    Don't worry, It's super easy!</h4>
                                <div class="row">
                                    <div class="col-md-5 col-sm-8 col-xs-8 col-md-offset-2 ">
                                        <asp:TextBox ID="txt_email" runat="server" SkinID="TextBox" class="form-control"
                                            placeholder="Your Email Id" ValidationGroup="GO" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Dynamic"
                                            ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                            InitialValue="" ValidationGroup="GO" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                            meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ValidationGroup="GO"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-xs-2 ">
                                        <asp:Button ID="btn_verify" CssClass="btn btn-success" runat="server" Text="Verify EmailId"
                                            ValidationGroup="GO" OnClick="btn_verify_Click" />
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px; padding: 10px">
                                    <div id="div_msg3" runat="server" class="" visible="false">
                                    </div>
                                </div>
                                <br />
                                <div id="div_msg2" runat="server" class="" visible="false">
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-2 ">
                                        <asp:TextBox ID="txt_otp" runat="server" class="form-control" Visible="False" placeholder="Enter OTP"
                                            ValidationGroup="CGP" MaxLength="4"></asp:TextBox>
                                        <ajax:filteredtextboxextender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txt_otp" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" Display="Dynamic"
                                            ControlToValidate="txt_otp" runat="server" ErrorMessage="OTP is required." InitialValue=""
                                            ValidationGroup="OTP" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btn_otp" CssClass="btn btn-success" runat="server" Text="Confirm OTP Code"
                                            Visible="False" ValidationGroup="OTP" OnClick="btn_otp_Click" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="change_email" ValidationGroup="changeemail" runat="server" Visible="false"
                                            OnClick="change_email_Click">Edit_Email</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </center>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

