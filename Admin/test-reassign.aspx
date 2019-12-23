<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="test-reassign.aspx.cs" Inherits="Admin_test_reassign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table.td {
            vertical-align: middle;
        }
    </style>

    <script type="text/javascript">
        function confirmation(testName) {
            if (confirm('Are you sure, you want to reassign ' + testName + ' ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="x_panel">
        <div class="x_title">
            <h2>Test Reassign<small><strong><i class="fa fa-user"></i> <asp:Label ID="lbl_name" runat="server"></asp:Label></strong></small></h2>
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
            <form runat="server">
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>

                <asp:Label ID="Label1" runat="server" Text="CDF Test Status Reports"></asp:Label>
                <table class="table text-left">

                    <tr>
                        <td>
                            <label>Test Name </label>
                        </td>
                        <td>
                            <label>Total Que.</label>
                        </td>
                        <td>
                            <label>Completed Que.</label>
                        </td>
                        <td style="width: 111px">
                            <label>Remain Que.</label>
                        </td>
                        <td>
                            <label>Total Time/Factor</label>
                        </td>
                        <td>
                            <label>Test Status</label>
                        </td>
                        <td>
                            <label>Reassign Test</label>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 25px;">
                            <asp:Label ID="Label2" runat="server">1. Know Your Self</asp:Label>
                        </td>
                        <td style="height: 25px">
                            <asp:Label ID="Label3" runat="server">90</asp:Label>
                        </td>
                        <td style="height: 25px">
                            <asp:Label ID="ky_completed_que" runat="server"></asp:Label>
                        </td>
                        <td style="width: 111px; height: 25px;">
                            <asp:Label ID="ky_remin_que" runat="server"></asp:Label>
                        </td>
                        <td style="height: 25px">
                            <asp:Label ID="ky_factor" runat="server"></asp:Label>
                        </td>
                        <td style="height: 25px">
                            <asp:Label ID="ky_status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_ky" class="btn btn-md btn-block btn-info" runat="server" Text="Know Your Self Reassign" OnClientClick="return confirmation('Know Your Self Test');"
                                OnClick="btn_ky_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server">2. Personality Test</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server">24</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="pd_completed_que" runat="server"></asp:Label>
                        </td>
                        <td style="width: 111px">
                            <asp:Label ID="pd_remin_que" runat="server"></asp:Label>
                        </td>
                        <td>-
                        </td>
                        <td>
                            <asp:Label ID="pd_status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_pd" class="btn btn-md btn-block btn-info" runat="server" Text="Personality Test Reassign" OnClientClick="return confirmation('Personality Test');"
                                OnClick="btn_pd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server">All Test Status</asp:Label>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="all_test_status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_all_test" class="btn btn-md btn-block btn-info" runat="server" Text="All Test Reassign" OnClientClick="return confirmation('All Test');"
                                OnClick="btn_all_test_Click" />
                        </td>
                    </tr>
                </table>


            </form>
        </div>
    </div>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

