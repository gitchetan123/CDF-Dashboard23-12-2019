<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="cdfedit.aspx.cs" Inherits="CDF_cdfedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        .panel {
            margin: 0 auto;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
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
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                <div class="row" style="margin-top: 10px;">
                    <label for="status" class="col-md-5 control-label">
                        CDF Name</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Email Id</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Dheya Email Id</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_dheya_email" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Contact No</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_contact" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        State</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_state" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        City</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Gender</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        DOB</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Level</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_level" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Status</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_status" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Password</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_password" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Created Date</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_crdate" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label for="status" class="col-md-5 control-label">
                        Updated Date</label>
                    <div class="col-md-6">
                        <asp:Label ID="lbl_update" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <!-- Custom Theme Scripts -->
        <script src="../../js/custom.min.js"></script>
    </form>
</asp:Content>

