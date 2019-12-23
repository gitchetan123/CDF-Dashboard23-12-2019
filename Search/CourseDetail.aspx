<%@ Page Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="CourseDetail.aspx.cs" Inherits="Search_CourseDetail" Title="Course Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
            margin: 5px;
            padding-left: 10px;
            padding-right: 10px;
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
                <h2>Course Details</h2>
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
                     <div class="" id="div_msg" runat="server" style="text-align: center; margin-left: 70px; margin-right: 70px;">
                        </div>
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-6">
                        <h4>Course :-
                    <asp:Label ID="lblCareerName" runat="server" class="text-primary"></asp:Label></h4>
                    </div>
                    <div class="col-sm-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblMsg" runat="server" CssClass="main2" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-8">
                        <table class="table" cellpadding="5px">
                            <%--table-bordered--%>
                            <tr>
                                <td align="left" bgcolor="WhiteSmoke" class="main2" valign="top" width="180">Stream in 10+2
                                </td>
                                <td align="left" bgcolor="WhiteSmoke" class="main4" valign="top" width="500">
                                    <asp:Label ID="course7Label" runat="server" Text='<%# Eval("course7") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#ffffff" class="main2" valign="top">Course Category
                                </td>
                                <td align="left" bgcolor="#ffffff" class="main4" valign="top">
                                    <asp:Label ID="course6Label" runat="server" Text='<%# Eval("course6") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="WhiteSmoke" class="main2" valign="top">Physics
                                </td>
                                <td align="left" bgcolor="WhiteSmoke" class="main4" valign="top">
                                    <asp:Label ID="course8Label" runat="server" Text='<%# Eval("course8") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#ffffff" class="main2" valign="top">Chemistry
                                </td>
                                <td align="left" bgcolor="#ffffff" class="main4" valign="top">
                                    <asp:Label ID="course9Label" runat="server" Text='<%# Eval("course9") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="WhiteSmoke" class="main2" valign="top">Mathematics
                                </td>
                                <td align="left" bgcolor="WhiteSmoke" class="main4" valign="top">
                                    <asp:Label ID="course10Label" runat="server" Text='<%# Eval("course10") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#ffffff" class="main2" valign="top">Biology
                                </td>
                                <td align="left" bgcolor="#ffffff" class="main4" valign="top">
                                    <asp:Label ID="course11Label" runat="server" Text='<%# Eval("course11") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="WhiteSmoke" class="main2" valign="top">Other Subjects
                                </td>
                                <td align="left" bgcolor="WhiteSmoke" class="main4" valign="top">
                                    <asp:Label ID="course12Label" runat="server" Text='<%# Eval("course12") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        What's this Course all about</label>
                </div>

                <div class="row">
                    <asp:Label ID="course2Label" class="col-sm-12 control-label" runat="server" Text='<%# Eval("course2") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        Job Market</label>
                </div>
                <div class="row">
                    <asp:Label ID="course3Label" class="col-sm-12 control-label" runat="server" Text='<%# Eval("course3") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        India - Demand</label>
                </div>
                <div class="row">
                    <asp:Label ID="course4Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("course4") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        Overseas Demand</label>
                </div>
                <div class="row">
                    <asp:Label ID="course5Label" runat="server" class="col-sm-12 control-label" Text='<%# Eval("course5") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        This Course will lead you to following Careers</label>
                </div>
                <div class="row">
                    <asp:Label ID="lblListOfCareers" runat="server" class="col-sm-12 control-label" Text='<%# Eval("course5") %>'></asp:Label>
                </div>
                <hr class="divider"></hr>
                <div class="row">
                    <label for="name" class="col-sm-12 control-label ">
                        Top Institutes</label>
                </div>
                <div class="row">
                    <label for="name" class="col-sm-2 control-label ">
                        Select Zone
                    </label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblListOfInstitutes" class="control-label" runat="server"
                            Text='<%# Eval("Institute_id") %>'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                    </div>
                    <div class="col-sm-1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#top">Top</asp:HyperLink>
                    </div>
                </div>

                <hr class="divider"></hr>
            </div>
        </div>
    </form>
    <!-- /form -->
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
