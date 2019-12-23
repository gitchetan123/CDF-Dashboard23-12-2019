<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/TestMaster.master" AutoEventWireup="true"
    CodeFile="PD_test_instructions.aspx.cs" Inherits="PD_Test_PD_test_instructions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
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


    <script language="javascript" type="text/jscript">

        function Confirmation() {
            var win = window.open('', '_self');
            (alert("Some Error Might have Occurred Please Start test Again ! Your window will get closed") == true)
            win.close();

        }
        function TestConfirmation() {
            var win = window.open('', '_self');
            (alert("Test Completed !") == true)
            win.close();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 30px">
        <div class="panel panel-primary">
            <div class="panel-heading ">
                <div class="panel-title text-center ">
                    <asp:Label ID="lblassessment" class="test-title" runat="server" Text="Assesment Portal"
                        meta:resourcekey="lblassessment"></asp:Label>
                </div>
            </div>


              <div class="row" style="padding-top: 20px;" runat="server" id="divAudio" visible="false">
            <div class="col-sm-3 col-sm-offset-3 ">
                <audio  width="12" height="80"  controls id="audioControl" runat="server" >
                    <source  type="audio/mpeg"></source>
                    Your browser does not support HTML5 Please Upgrate your browser.
                    </audio>
                    </div>
                <script type="text/jscript">
                    var audio = document.getElementById("ctl00_ContentPlaceHolder1_audioControl");

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
                <div class="col-sm-4 " style="margin-left:25px;margin-right:25px" >
                <button  onclick="restartAudio()" type="button" class="btn btn-success" style="padding :4px;" >Play Again</button>
                </div>
            </div>

            <div style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                <div style="margin-top: 10px; text-align: center; margin-bottom: 20px">
                    <asp:Label ID="pd_hd_lbl" class="test-sub-title" runat="server" meta:resourcekey="pd_hd_lblResource1"
                        Text="Behavioural Profiller Test Details" Font-Bold="True"></asp:Label>
                </div>
                <div style="margin-top: 10px; text-align: left; margin-bottom: 20px">
                    <asp:Label ID="pd_tst_no_lbl" class="test-sub-title" runat="server" meta:resourcekey="pd_tst_no_lblResource1"
                        Text="Following are the instructions of Test No " ForeColor="#10a4ec" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTestNo" class="test-sub-title" runat="server" meta:resourcekey="lblTestNoResource1"
                        Font-Bold="True" ForeColor="#555555"></asp:Label>
                </div>
                <div style="margin-top: 10px; text-align: justify; margin-bottom: 20px">
                    <asp:Label ID="pd_tst_ins_msg_lbl" class="test-body" runat="server" meta:resourcekey="pd_tst_ins_msg_lblResource1"
                        Text="The questionnaire you are about to complete is not a test. You cannot 'pass' or 'fail', and there are no right or wrong answers.&lt;br /&gt;&lt;br /&gt;Avoid trying to predict or infer what the questions are about, but be sure to consider all the alternatives fully before answering. If two or more statements describe you closely, take time to consider which one describes your current approach most accurately.&lt;br /&gt;&lt;br /&gt;While answering the questions, think about the ways you tend to act in a working situation.&lt;br /&gt;&lt;br /&gt;Work quickly through the questionnaire; as a guideline, it should take you no longer than ten minutes to complete.&lt;br /&gt;&lt;br /&gt;From each set of four phrases in the questionnaire, choose the phrase that describes you most closely, and the phrase that describes you least closely.&lt;br /&gt;&lt;br /&gt;Indicate your answers by checking the appropriate MOST and LEAST button, as shown in the example below.&lt;br /&gt;&lt;br /&gt;You must choose one button from each column in each question.&lt;br /&gt;&lt;/strong&gt;&lt;strong&gt;&lt;br /&gt;&lt;strong&gt;Example Q.&amp;nbsp;&lt;br /&gt; &lt;/strong&gt;"></asp:Label>
                    <div align="center">
                        <asp:Image class="visible-md visible-lg" style="height:130px;width:900px;" ID="Image4" runat="server" ImageUrl="~/images/TestImages/test_10_opt_en.png" meta:resourcekey="Image4Resource1" />
                        <asp:Image class="visible-sm visible-xs img-responsive" style="height:100px;width:270px;" ID="Image1" runat="server" ImageUrl="~/images/TestImages/test_10_opt_en_sm.png" meta:resourcekey="Image4Resource1" />
                    </div>

                    <br />
                    <asp:Label ID="pd_ins_msg_lbl" runat="server" meta:resourcekey="pd_ins_msg_lblResource1"
                        class="test-body" Text="Do not skip any questions." Font-Bold="True"></asp:Label>
                </div>
                <div style="margin-top: 10px; text-align: center; margin-bottom: 20px">
                    <asp:LinkButton ID="lbContinue" runat="server" class="btn btn-primary btn-lg test-body"
                        OnClick="lbContinue_Click" meta:resourcekey="lbContinueResource1" Text="Click to Continue...."></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
