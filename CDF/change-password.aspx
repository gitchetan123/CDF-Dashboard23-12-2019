<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="Profile_Change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
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
    <!-- Stack the columns on mobile by making one full-width and the other half-width -->
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Change Password</h2>
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
                <div style="padding-top: 10px" class="panel-body">
                    <div style="margin-top: 10px">
                        <div class="form-group text-info" align="center" style="margin-bottom: 25px">
                        </div>
                        <asp:Label ID="lblMsg" runat="server">
                        </asp:Label>
                        <div id="div_success" runat="server" class=" alert alert-success" style="text-align: center; margin-bottom: 25px">
                            Password change Successfully.
                        </div>
                        <div id="div_Error" runat="server" class=" alert alert-danger" style="text-align: center; margin-bottom: 25px">
                            Please Enter Valid old Password.
                        </div>

                        <div style="margin-bottom: 5px" class=" col-md-12  col-xs-12 col-sm-12">
                            <div class="col-md-4 text-right" style="padding-top: 5px">
                                Old Password :
                            </div>
                            <div class="input-group input-group col-md-6 col-sm-12 col-xs-12">
                                <asp:TextBox ID="tbOldPassword" runat="server" TextMode="Password"
                                    class="form-control" placeholder="Old Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbOldPassword"
                                    ErrorMessage="Please Enter old Password" Display="None">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="margin-bottom: 5px" class=" col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-4 text-right" style="padding-top: 5px">
                                New Password :
                            </div>
                            <div class="input-group input-group col-md-6 col-sm-12 col-xs-12">
                                <asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password"
                                    class="form-control" placeholder="New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbNewPassword"
                                    Display="None" ErrorMessage="Please Enter New Password">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                    runat="server" ErrorMessage="Password length should be more than 6 and less than 15"
                                    ValidationExpression="(.{6,15})" ControlToValidate="tbNewPassword" Display="None"
                                    ForeColor="Red">*</asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div style="margin-bottom: 5px" class=" col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-4 text-right" style="padding-top: 5px">
                                Confirm Password :
                            </div>
                            <div class="input-group input-group col-md-6 col-sm-12 col-xs-12">
                                <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password"
                                    class="form-control" placeholder="New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbConfirmPassword"
                                    Display="None" ErrorMessage="Please Enter Confirm Password">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbConfirmPassword"
                                    ControlToValidate="tbNewPassword" Display="None" ErrorMessage="Confirm Password should match New Password">*</asp:CompareValidator></td>
                            </div>
                        </div>
                        <div style="margin-top: 10px" class="form-group ">
                            <!-- Button -->
                            <div class="col-md-12 col-sm-12 col-xs-12" style="text-align: center">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                                <div class="ln_solid"></div>

                                <div class="form-group ">
                                    <asp:Button ID="btnSubmit" class="btn btn-primary col-md-2 col-sm-12 col-xs-12 col-md-offset-5" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="Hf_password" runat="server" />
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
