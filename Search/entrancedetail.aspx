<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="entrancedetail.aspx.cs" Inherits="Search_entrancedetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .panel {
            max-width: 900px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

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
    <form id="Form1" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Entrance Exam Details</h2>
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
                <div class="panel panel-primary">
                    <div class="panel-heading ">
                        <div class="panel-title text-center ">
                            Entrance Exam Details
                        </div>
                    </div>
                    <div id="div_msg" runat="server" class="alert " style="text-align: center;">
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-8 col-md-offset-2">
                            <h4>Entrance Exam Name :- &nbsp;<asp:Label ID="lbl_entrancename" runat="server" class="text-primary"></asp:Label></h4>
                        </div>
                    </div>
                    <hr class="divider"></hr>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Exam Detail :-
                        </label>
                        <div class="col-md-6">
                            <asp:Label ID="lbl_detail" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Requirement :-
                        </label>
                        <div class="col-md-6">
                            <asp:Label ID="lbl_req" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Exam Fee :-
                        </label>
                        <div class="col-md-6">
                            <asp:Label ID="lbl_fee" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Exam Date :-
                        </label>
                        <div class="col-md-6">
                            <asp:Label ID="lbl_edate" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Application Date :-
                        </label>
                        <div class="col-md-6">
                            <asp:Label ID="lbl_adate" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <label class="col-md-3 col-md-offset-2 control-label">
                            Application Link :-
                        </label>
                        <div class="col-md-6">
                            <asp:HyperLink ID="entrancelink" runat="server" Font-Names="Verdana" Font-Size="11px" Target="_blank" Font-Underline="true" ForeColor="#404040" Font-Bold="true">[entrancelink]</asp:HyperLink>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

