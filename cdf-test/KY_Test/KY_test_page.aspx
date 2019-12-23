<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/TestMaster.master" AutoEventWireup="true"
    CodeFile="KY_test_page.aspx.cs" Inherits="KY_Test_KY_test_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }
        .panel
        {
            max-width: 1100px;
            text-align: center;
        }
        .align-left
        {
            float: left;
        }
        .align-right
        {
            float: right;
        }
        .radio
        {
            margin-top: -2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div align="center" style="margin-top: 0px">
                <div class="panel panel-primary">
                    <div class="panel-heading ">
                        <div class="panel-title text-center ">
                            <asp:Label ID="lblassessment" class="test-title" runat="server" Text="Assesment Portal"
                                meta:resourcekey="lblassessment"></asp:Label>
                        </div>
                    </div>
                    <div style="margin-top: 10px; padding-left: 15px; padding-right: 15px; padding-bottom: 15px;
                        text-align: right; margin-bottom: 20px">
                        <div style="margin-top: 10px; text-align: center; margin-bottom: 20px">
                            <asp:Label ID="test_no_lbl" class="test-title" runat="server" meta:resourcekey="test_no_lblResource1"
                                Text="Test No. :-"></asp:Label>
                            <asp:Label ID="lblTestNo" class="test-title" runat="server" meta:resourcekey="lblTestNoResource1"></asp:Label>
                        </div>
                        <div class="row" style="margin-top: 10px; margin-bottom: 20px">
                            <div class="col-md-4 col-xs-12 text-center">
                                <asp:Label ID="c_q_no_lbl" class="test-sub-title" runat="server" ForeColor="#10a4ec"
                                    Font-Bold="true" meta:resourcekey="c_q_no_lblResource1" Text="Question No :-"></asp:Label>
                                &nbsp;<asp:Label ID="lblQuestionNo" class="test-sub-title" runat="server" ForeColor="#555555"
                                    Font-Bold="true" meta:resourcekey="lblQuestionNoResource1"></asp:Label>
                            </div>
                            <div class="col-md-4 col-md-offset-4 col-xs-12 text-center">
                                <asp:Label ID="q_lft_lbl" class="test-sub-title" runat="server" meta:resourcekey="q_lft_lblResource1"
                                    Text="Total Questions :-" Font-Bold="True" ForeColor="#10A4EC"></asp:Label>
                                &nbsp;<asp:Label ID="lblQuestionsLeft" class="test-sub-title" runat="server" ForeColor="#555555"
                                    Font-Bold="true" meta:resourcekey="lblQuestionsLeftResource1">90</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <table class="table" width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="q_1_lbl" runat="server" Text="Question:" meta:resourcekey="q_1_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="lblQuestionname" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="o_1_lbl" runat="server" Text="Options:" meta:resourcekey="o_1_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="rblOptions" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="rblOptionsResource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblOptions"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label1" runat="server" Text="Question:" meta:resourcekey="q_2_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label2" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label3" runat="server" Text="Options:" meta:resourcekey="o_2_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadioButtonList1"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator2Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label4" runat="server" Text="Question:" meta:resourcekey="q_3_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label5" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label6" runat="server" Text="Options:" meta:resourcekey="o_3_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadioButtonList2"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator3Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label7" runat="server" Text="Question:" meta:resourcekey="q_4_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label8" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label9" runat="server" Text="Options:" meta:resourcekey="o_4_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadioButtonList3"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator4Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label10" runat="server" Text="Question:" meta:resourcekey="q_5_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label11" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label12" runat="server" Text="Options:" meta:resourcekey="o_5_lblResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="rblOptionsResource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="RadioButtonList4"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator5Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-sm-6">
                                <table class="table" width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label13" runat="server" Text="Question:" meta:resourcekey="Label5Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label14" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label15" runat="server" Text="Options:" meta:resourcekey="Label7Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="RadioButtonList5"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator6Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label16" runat="server" Text="Question:" meta:resourcekey="Label8Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label17" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label18" runat="server" Text="Options:" meta:resourcekey="Label10Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList6" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadioButtonList6"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator7Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label19" runat="server" Text="Question:" meta:resourcekey="Label11Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label20" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label21" runat="server" Text="Options:" meta:resourcekey="Label13Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList7" runat="server" CellPadding="0" CellSpacing="0"
                                                    class="radio" meta:resourcekey="RadioButtonList1Resource1" CssClass="radio test-body">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="RadioButtonList7"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator8Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label22" runat="server" Text="Question:" meta:resourcekey="Label14Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label23" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label24" runat="server" Text="Options:" meta:resourcekey="Label16Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="align-left">
                                                <asp:RadioButtonList ID="RadioButtonList8" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadioButtonList8"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator9Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label25" runat="server" Text="Question:" meta:resourcekey="Label17Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <asp:Label ID="Label26" runat="server" Class="control-label" meta:resourcekey="lblQuestionnameResource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="20%">
                                            <asp:Label ID="Label27" runat="server" Text="Options:" meta:resourcekey="Label19Resource1"
                                                CssClass="test-body"></asp:Label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="text-left">
                                                <asp:RadioButtonList ID="RadioButtonList9" runat="server" CellPadding="0" CellSpacing="0"
                                                    CssClass="radio test-body" meta:resourcekey="RadioButtonList1Resource1">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="align-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="RadioButtonList9"
                                                    Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator10Resource1"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="margin-top: 20px; text-align: center; margin-bottom: 20px">
                            <asp:Button class="btn btn-md btn-info test-body" ID="btnGiveTest" runat="server"
                                OnClick="btnGiveTest_Click" Text="Next Question" meta:resourcekey="btnGiveTestResource1" />
                        </div>
                        <div style="text-align: center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                meta:resourcekey="ValidationSummary1Resource1" DisplayMode="SingleParagraph"
                                HeaderText="Error: " CssClass="test-body" />
                        </div>
                        <div style="margin-top: 30px; text-align: center; margin-bottom: 20px">
                            <asp:Label ID="inst_note_lbl" class="test-sub-body" runat="server" ForeColor="#e91e63"
                                Font-Bold="true" meta:resourcekey="inst_note_lblResource1" Text="Note : Select the Correct Option and Click Next Question."></asp:Label>
                        </div>
                        <asp:HiddenField ID="Hcid" runat="server" />
                        <%--<asp:HiddenField ID="Hage" runat="server" />
                        <asp:HiddenField ID="HcandName" runat="server" />--%>
                        <asp:HiddenField ID="HQno" runat="server" />
                        <%--<asp:HiddenField ID="HtestNo" runat="server" />--%>
                        <asp:HiddenField ID="Hlang" runat="server" />
                        <%--<asp:HiddenField ID="Hgender" runat="server" />--%>
                        <script language="javascript" type="text/jscript">

                            function Confirmation() {
                                var win = window.open('', '_self');
                                (alert("Some Error Might have Occurred Please Start test Again ! Your window will get closed") == true)
                                win.close();
                            }


                            function closewin() {
                                var win = window.open('', '_self');
                                win.close();
                            }





                            ctl00_ContentPlaceHolder1_btnGiveTest

                        </script>
                        <script type="text/javascript">
                            disableSelection(document.body) //disable text selection on entire body of page
                        </script>
                        <script type="text/javascript">
                            function DisableButton() {
                                document.getElementById("<%=btnGiveTest.ClientID %>").disabled = true;
                                document.getElementById("<%=btnGiveTest.ClientID %>").value = "Processing..."
                            }
                            window.onbeforeunload = DisableButton;
                        </script>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
