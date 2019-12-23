<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/Candidatemaster.master" AutoEventWireup="true"
    CodeFile="all_test_complete.aspx.cs" Inherits="candidate_all_test_complete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
        }

        .panel {
            max-width: 900px;
            text-align: center;
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="panel panel-primary" style="margin-top: 50px; margin-bottom: 50px">
            <div class="panel-heading ">
                <div class="panel-title text-center">
                    <asp:Label ID="all_tst_msg_lbl" class="test-title" runat="server" meta:resourcekey="all_tst_msg_lblResource1"
                        Text="You have completed all tests."></asp:Label>
                </div>
            </div>
            <div class="row" style="padding: 50px;">
              <%--  <asp:HyperLink ID="hlTakeTest" runat="server" class="test-sub-title" NavigateUrl="~/Dashboard/DownloadReport.aspx"
                    Font-Bold="True">Click here to download report </asp:HyperLink><br />--%>
                <br />
                <asp:Label ID="tst_msg_lbl" class="test-sub-title" runat="server" meta:resourcekey="tst_msg_lblResource2"
                    Text="Thank you"></asp:Label>&nbsp;&nbsp;
                <a href="<%=ResolveUrl("~/pre/home.aspx") %>">Go to Dashboard Home</a>
            </div>
        </div>
    </div>
</asp:Content>
