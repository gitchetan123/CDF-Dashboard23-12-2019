<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="institutedetails.aspx.cs" Inherits="Search_institutedetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .panel {
            text-align: justify;
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
                <h2> Institute & Sub-course Details</h2>
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
                            Institute & Sub-course Details
                        </div>
                    </div>
                    <div id="div_msg" runat="server" class="alert " style="text-align: center;">
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-10 col-md-offset-1">
                            <h4>Institute Name :- &nbsp;<asp:Label ID="lbl_instname" runat="server" class="text-primary"></asp:Label></h4>
                        </div>
                    </div>
                    <hr class="divider"></hr>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Category :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_catagory" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Region :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_region" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            State :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_state" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            City :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_city" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Affiliation :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_affil" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Website :-
                        </label>
                        <div class="col-md-3">
                            <asp:HyperLink ID="hpl_website" runat="server" Target="_blank" Font-Names="Verdana" Font-Size="11px" Font-Underline="true" ForeColor="#404040" Font-Bold="true">[website]</asp:HyperLink>

                            <%--<asp:Label ID="lbl_website" runat="server" class="control-label"></asp:Label>--%>
                        </div>

                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Email-ID :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_email" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Contact No. :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_contact" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Address :-
                        </label>
                        <div class="col-md-9">
                            <asp:Label ID="lbl_address" runat="server" class="control-label"></asp:Label>
                        </div>

                    </div>
                    <hr class="divider"></hr>
                    <div class="row">
                        <div class="col-md-10 col-md-offset-1">
                            <h4>Sub-course Name :- &nbsp;<asp:Label ID="lbl_subconame" runat="server" class="text-primary"></asp:Label></h4>
                        </div>
                    </div>
                    <hr class="divider"></hr>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Category :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_subcocatagory" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Stream :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_subcostream" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Specialization :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_spe" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Duration :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_duration" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Requirement :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_req" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Institute Req. :-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_instreq" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Rank :-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_rank" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Dheya Rank:-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_drank" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            India Today:-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_itrank" runat="server" class="control-label"></asp:Label>
                        </div>
                        <label class="col-md-2 control-label">
                            Business Today:-</label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_btrank" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1 control-label">
                            Hindustan Times:-
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="lbl_htrank" runat="server" class="control-label"></asp:Label>
                        </div>

                    </div>

                    <div class="row">
                        <label class="col-md-2 col-md-offset-1  control-label">
                            Description :-</label>
                        <div class="col-md-9">
                            <asp:Label ID="lbl_descrip" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-md-offset-1  control-label">
                            Entrance Exam :-</label>
                        <div class="col-md-9">
                            <asp:HyperLink ID="entrancename" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="true" ForeColor="#404040" Font-Bold="true">[entrancename]</asp:HyperLink>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 text-right control-label">
                            Other Specialization :-</label>
                        <div class="col-md-8">
                            <asp:Label ID="lbl_otherspe" runat="server" class="control-label"></asp:Label>
                        </div>
                    </div>
                    <hr class="divider"></hr>

                </div>
            </div>
        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

