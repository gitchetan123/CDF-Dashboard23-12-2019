<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="my-documents.aspx.cs" Inherits="CDF_my_documents" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

     <%--Add Facebook Share Button--%>
   <%--<!—- ShareThis BEGIN -—>--%>
<script async="async" src="https://platform-api.sharethis.com/js/sharethis.js#property=5dccfe7025f7ed00128a9974&product=sticky-share-buttons"></script>
<%--<!—- ShareThis END -—>--%>
    <%-- Prevent cut , copy paste end code --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>My Documents</h2>
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
                <div class="row">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3" style="text-align: center; font-size: 14px;">
                        <table class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <td><strong>Document Name</strong>
                                    </td>
                                    <td><strong>Status</strong>
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Id Card"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_idcard" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Certificate"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_certificate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Visiting Card"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_visitingcard" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="NDA Copy"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_ndacopy" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Child's Test"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_childTest" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Child's Session"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_childSession" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Spouse's Test"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_spouseTest" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Number of Shadow Sessions"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_shadowSessions" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

