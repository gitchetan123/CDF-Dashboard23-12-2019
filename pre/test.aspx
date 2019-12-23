<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/pre/PreCDFMasterPage.master"
    CodeFile="test.aspx.cs" Inherits="test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Personality Test</h2>
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
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-left: 10px; margin-right: 10px">
                </div>
                <div id="div_TakeTest" runat="server">
                    <h3 style="text-align: center; margin-top: 10px;">CDF Personality Test</h3>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <p style="text-align: center; margin-top: 15px;">
                                    This is not an exam or has no right or wrong answers. This assessment will enable
                                            you to understand your true potential and know the psychological profile of yours.
                                </p>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4">
                                <asp:Button ID="btn_test" class="btn btn-success btn-block" runat="server" Text="Take a Test"
                                    OnClick="btn_test_Click" ValidationGroup="bro" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="div_msg1" runat="server" class="alert alert-danger" visible="false" style="max-height: 800px; padding: 60px 20px 60px 20px; margin-top: 40px;">
                    <h4 style="font-size: 22px; font-weight: bold;">Oops. Something Went Wrong! Please Try After sometime.</h4>
                </div>
            </div>
        </div>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="bro" />
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>
