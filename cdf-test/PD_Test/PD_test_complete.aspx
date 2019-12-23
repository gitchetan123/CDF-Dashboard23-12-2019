<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/TestMaster.master" AutoEventWireup="true"
    CodeFile="PD_test_complete.aspx.cs" Inherits="PD_Test_PD_test_complete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row
        {
            padding: 5px;
        }
        
        .panel
        {
            max-width: 900px;
            text-align: center;
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="margin-top: 30px">
        <div class="panel panel-primary" style="margin-top: 50px; margin-bottom: 50px">
            <div class="panel-heading ">
                <div class="panel-title text-center">
                    <asp:Label ID="pd_cmplt_no_lbl" class="test-title" runat="server" meta:resourcekey="pd_cmplt_no_lblResource1"
                        Text="Test No "></asp:Label>
                    <asp:Label ID="lblTestNo" class="test-title" runat="server" meta:resourcekey="lblTestNoResource1"></asp:Label>
                    <asp:Label ID="pd_test_cmplt_lbl" class="test-title" runat="server" meta:resourcekey="pd_test_cmplt_lblResource1"
                        Text=" Completed"></asp:Label>
                </div>
            </div>
            <div class="row" style="padding: 50px;">
                <asp:LinkButton ID="lbContinue" runat="server" class="test-title" PostBackUrl="javascript:window.close()"
                    meta:resourcekey="LinkButton2Resource1" Text="Click to Continue"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
