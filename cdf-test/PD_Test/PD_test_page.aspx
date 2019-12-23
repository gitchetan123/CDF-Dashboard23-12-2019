<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/TestMaster.master" AutoEventWireup="true"
    CodeFile="PD_test_page.aspx.cs" Inherits="PD_Test_PD_test_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        .left {
            padding-left: 10PX;
        }
        
         audio::-internal-media-controls-download-button
        {
            display: none;
        }
        audio::-webkit-media-controls
        {
            overflow: hidden !important;
        }
        audio::-webkit-media-controls-enclosure
        {
            width: calc(100% + 32px);
            margin-left: auto;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="margin-top: 30px">
        <div class="panel panel-primary">
            <div class="panel-heading ">
                <div class="panel-title text-center ">
                    <asp:Label ID="lblassessment" class="test-title" runat="server" Text="Assesment Portal"
                        meta:resourcekey="lblassessment"></asp:Label>
                </div>
            </div>

           


            <div style="margin-top: 10px; padding-left: 15px; padding-right: 15px; padding-bottom: 15px; text-align: right; margin-bottom: 20px">
                <div style="margin-top: 10px; text-align: center; margin-bottom: 20px">
                    <asp:Label ID="p_test_lbl" class="test-title" runat="server" meta:resourcekey="p_test_lblResource1"
                        Text="Personality Test&nbsp;"></asp:Label>
                    &nbsp;<asp:Label ID="lblTestNo" class="test-title" runat="server" meta:resourcekey="lblTestNoResource1"></asp:Label>
                </div>

                  <%--  Audio control --%>
                <div class="row" style="padding-top: 20px;" runat="server" id="divAudio" visible="false">
                    <div class="col-sm-3 col-sm-offset-3 ">
                        <audio controls id="audioControl" runat="server">
                    <source  type="audio/mpeg"></source>
                    Your browser does not support HTML5 Please Upgrate your browser.
                    </audio>
                    </div>
                    <script type="text/jscript">
                        var audio = document.getElementById("ContentPlaceHolder1_audioControl");

                        function playAudio() {
                            audio.play();
                        }
                        function pauseAudio() {
                            audio.pause();
                        }
                        function restartAudio() {
                            audio.currentTime = 0;
                            audio.play();
                        } 
                    </script>
                    <div class="col-sm-2 " style="margin-left: 25px; margin-right: 25px">
                        <button onclick="restartAudio()" type="button" class="btn btn-success" style="padding: 4px;">
                            Play Again</button>
                    </div>
                </div>

                <div style="margin-top: 10px; text-align: center; margin-bottom: 20px">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="row" style="margin-top: 10px; margin-bottom: 20px">
                    <div class="col-md-4 col-xs-12 text-center">
                        <asp:Label ID="q_no_lbl" class="test-sub-title" runat="server" meta:resourcekey="q_no_lblResource1"
                            Text="Question No :-" Font-Bold="True" ForeColor="#10A4EC"></asp:Label>
                        &nbsp;<asp:Label ID="lblQuestionNo" class="test-sub-title" runat="server" ForeColor="#555555"
                            Font-Bold="true" meta:resourcekey="lblQuestionNoResource1"></asp:Label>
                    </div>
                    <div class="col-md-4 col-md-offset-4 col-xs-12 text-center">
                        <asp:Label ID="q_lft_lbl" class="test-sub-title" runat="server" meta:resourcekey="q_lft_lblResource1"
                            Text="Total Questions :-" Font-Bold="True" ForeColor="#10A4EC"></asp:Label>
                        &nbsp;<asp:Label ID="lblQuestionsLeft" class="test-sub-title" runat="server" ForeColor="#555555"
                            Font-Bold="true" meta:resourcekey="lblQuestionsLeftResource1">24</asp:Label>
                    </div>
                </div>
                <div class="row" align="center">
                    <table width="90%" class=" table-bordered text-left">
                        <tr bgcolor="#eaf4fd">
                            <td style="width: 60%"></td>
                            <td style="width: 20%">
                                <asp:Label ID="MS_lbl" runat="server" meta:resourcekey="MS_lblResource1" Text="Most Suitable"
                                    CssClass="left test-body"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="ls_lbl" runat="server" meta:resourcekey="ls_lblResource1" Text="Least Suitable"
                                    CssClass="left test-body"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="lblword" runat="server" meta:resourcekey="lblwordResource1" CssClass="left  test-body"></asp:Label>
                            </td>
                            <td rowspan="4" style="width: 15%; padding-left: 10PX">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadioButtonList1" CellPadding="0" CellSpacing="0" runat="server"
                                                Height="120px" meta:resourcekey="RadioButtonList1Resource1" Font-Size="0pt">
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioButtonList1"
                                                Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                Text="&nbsp;*" CssClass="left" Font-Bold="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td rowspan="4" style="width: 20%; padding-left: 10px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList CellPadding="0" CellSpacing="0" ID="RadioButtonList2" runat="server"
                                                Class="left" Height="120px" meta:resourcekey="RadioButtonList2Resource1" Font-Size="0pt">
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadioButtonList2"
                                                Display="Dynamic" ErrorMessage="Please Choose the Correct Option" meta:resourcekey="RequiredFieldValidator2Resource1"
                                                Text="*" CssClass="left"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr bgcolor="#eaf4fd">
                            <td width="60%">
                                <asp:Label ID="lblword1" runat="server" meta:resourcekey="lblword1Resource1" CssClass="left  test-body"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:Label ID="lblword2" runat="server" meta:resourcekey="lblword2Resource1" CssClass="left  test-body"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#eaf4fd">
                            <td width="60%">
                                <asp:Label ID="lblword3" runat="server" meta:resourcekey="lblword3Resource1" CssClass="left  test-body"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="RadioButtonList1"
                                    ControlToValidate="RadioButtonList2" Display="Dynamic" ErrorMessage="Please enter different words"
                                    Operator="NotEqual" meta:resourcekey="CompareValidator1Resource1" Text="Please select different options"
                                    CssClass="test-body"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="row" style="margin-top: 20px">
                    <div class="col-sm-4 col-sm-offset-4 col-xs-4 col-xs-offset-4">
                        <asp:Button ID="btnGiveTest" runat="server" class=" btn btn-info btn-block test-body"
                            Text="Continue" OnClick="btnGiveTest_Click" meta:resourcekey="btnGiveTestResource1" />
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False"
                        ShowMessageBox="True" />
                </div>
                <div style="margin-top: 30px; text-align: center; margin-bottom: 5px">
                    <asp:Label ID="not_lbl" runat="server" class="test-sub-body" ForeColor="#e91e63"
                        Font-Bold="true" meta:resourcekey="not_lblResource1" Text="Note : Select the Correct Option and Click Continue."></asp:Label>
                    <asp:Label ID="lblTimeout" runat="server"></asp:Label>
                </div>
                <asp:HiddenField ID="Hcid" runat="server" />
                <asp:HiddenField ID="Hlang" runat="server" />
                <asp:HiddenField ID="Hname" runat="server" />
                <asp:HiddenField ID="Hbatid" runat="server" />
                <asp:HiddenField ID="Hqid" runat="server" />
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
</asp:Content>
