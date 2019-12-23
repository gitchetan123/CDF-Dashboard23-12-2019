<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="Profile_Change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
        }

        .panel {
            max-width: 650px;
            padding-bottom: 15px;
            text-align: center;
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Stack the columns on mobile by making one full-width and the other half-width -->
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
            <form id="loginform" class="form-horizontal" role="form" runat="server">
                <div id="div_success" runat="server" class=" alert alert-success" style="text-align: center;">
                    Password change Successfully.
                </div>
                <div id="div_Error" runat="server" class=" alert alert-danger" style="text-align: center;">
                    Please Enter Valid old Password.
                </div>
                <div class="row" style="padding-top: 20px;">
                    <label class="col-md-3 col-md-offset-2 control-label">
                        Old Password</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="tbOldPassword" runat="server" TextMode="Password"
                            class="form-control" placeholder="Old Password"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbOldPassword"
                            ErrorMessage="Please Enter old Password" Display="Static">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-3 col-md-offset-2 control-label">
                        New Password</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password"
                            class="form-control" placeholder="New Password"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbNewPassword"
                            Display="Static" ErrorMessage="Please Enter New Password">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-3 col-md-offset-2 control-label">
                        Confirm Password</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password"
                            class="form-control" placeholder="New Password"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbConfirmPassword"
                            Display="Static" ErrorMessage="Please Enter Confirm Password">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbConfirmPassword"
                            ControlToValidate="tbNewPassword" Display="Static" ErrorMessage="Confirm Password should match New Password">*</asp:CompareValidator></td>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-md-offset-5 " style="text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                            ShowSummary="false" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            class=" btn btn-primary btn-md btn-block" />
                    </div>
                </div>
            </form>
        </div>
    </div>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>
