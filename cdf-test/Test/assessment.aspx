<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/Candidatemaster.master" AutoEventWireup="true"
    CodeFile="assessment.aspx.cs" Inherits="Candidate_assessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row
        {
            margin-left: 10px;
            margin-right: 10px;
            margin-bottom: 10px;
            margin-top: 10px;
        }
        
        .panel
        {
            max-width: 1000px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="panel panel-primary">
        <div class="panel-heading ">
            <div class="panel-title ">
                <asp:Label ID="lblheading" runat="server" class="test-title" Text="Assessment Detail"
                    meta:resourcekey="lblheadingResource1"></asp:Label>
            </div>
        </div>
        <div class="row">
            <table class="table-condensed">
                <tr>
                    <td>
                        <asp:Label ID="lblmainmsg" runat="server" Text="This is a 2 step test process, Know Your Self and Personality Test."
                            class="test-body" meta:resourcekey="lblmainmsgResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblassesment" runat="server" Text="Test Details" class="test-sub-title"
                            Font-Bold="true" meta:resourcekey="lblassesmentResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        <img src="../../images/TestImages/Interest.jpg" style="height:184px;width:300px;" alt="" title="" />
                        <img src="../../images/TestImages/Personality.jpg" style="height:184px;width:300px;" alt="" title="" />
                    </td>
                </tr>
                <tr>
                    <td align="justify">
                        <asp:Label ID="lblinfo" runat="server" Text="Personality test consists of 2 tests. Answering all questions is mandatory and this test is not time bound. 1st test comprises of 90 questions which helps in understanding your personality and characteristics. 2nd test consists of 24 questions which helps in understanding the behavior of an individual."
                            class="test-body" meta:resourcekey="lblinfoResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnPersonality" runat="server" Text="Start Test" CssClass="btn btn-lg btn-success test-sub-title"
                            OnClick="btnPersonality_Click" meta:resourcekey="btnPersonalityResource1" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
