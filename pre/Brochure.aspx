<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/pre/PreCDFMasterPage.master"
    CodeFile="brochure.aspx.cs" Inherits="brochure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Brochure & Proposal</h2>
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
                <div class="container" style="margin-top: 10px;">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5 ">
                            <h3 style="margin-bottom: 20px; margin-top: 20px; text-align: center;">CDF Proposal</h3>
                            <p style="font-size: 14px; text-align: justify;">
                                The Dheya Career Development Facilitator (CDF) is a certified professional who, on behalf
                                of Dheya works with parents and students to facilitate “one on one” career development
                                programs.
                            </p>
                            <center> <a class="btn btn-primary" style="padding:5px 50px 5px 50px;margin-top:40px;" href="https://www.dheya.com/email-docs/CDF-Proposal-Aug-2017.pdf" target="_blank">Download</a></center>
                        </div>
                        <div class="col-sm-5">
                            <h3 style="margin-bottom: 20px; margin-top: 20px; text-align: center;">CDF FAQ's</h3>
                            <p style="font-size: 14px; text-align: justify;">
                                Dheya has developed processes that are proven and impactful. Dheya has extensive
                                and detailed study of more than 600 career streams, over 250 education streams
                                and a complete detailed information base of institutes, entrance exams, degrees,
                                specializations etc.
                            </p>
                            <center><a class="btn btn-primary" style="padding:5px 50px 5px 50px;" href="https://www.dheya.com/email-docs/FAQs-2017.pdf" target="_blank">Download</a></center>
                        </div>
                        <div class="col-sm-1"></div>

                    </div>
                </div>
            </div>
        </div>
        <div id="div_msg1" runat="server" class="alert alert-danger" visible="false" style="max-height: 800px; padding: 60px 20px 60px 20px; margin-top: 40px;">
            <h4 style="font-size: 22px; font-weight: bold;">Oops. Something Went Wrong! Please Try After sometime.</h4>
        </div>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="bro" />
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>
