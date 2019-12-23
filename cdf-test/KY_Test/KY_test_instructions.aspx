<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/TestMaster.master" AutoEventWireup="true" CodeFile="KY_test_instructions.aspx.cs" Inherits="KY_Test_KY_test_instructions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
<div  style="margin-top:30px">
 
<div class="panel panel-primary">
     <div class="panel-heading "> 
            <div class="panel-title text-center "> 
   <asp:Label ID="lblassessment"  class="test-title" runat="server" Text="Assesment Portal" meta:resourcekey="lblassessment"></asp:Label>
        
                </div></div>
    <div style="padding-left:15px; padding-right:15px; padding-bottom:15px;">
    
<div style="margin-top: 10px; text-align: center;margin-bottom :20px">

<asp:Label ID="ky_tst_inst_hd_lbl"  class="test-sub-title" runat="server" meta:resourcekey="ky_tst_inst_hd_lblResource1"
                                                Text="Personal Asset Analyser Test Details"></asp:Label>
</div>


<div style="margin-top: 10px; text-align: left;margin-bottom :20px">
 <asp:Label ID="ky_folw_lbl" runat="server" meta:resourcekey="ky_folw_lblResource1"
                                                            Text="Following are the details of Test No." ForeColor="#10a4ec"  class="test-sub-title" Font-Bold="true"></asp:Label>
                                                        &nbsp;<asp:Label ID="lblTestNo" runat="server" meta:resourcekey="lblTestNoResource1"
                                                            Font-Bold="True"  class="test-sub-title"  ForeColor="#555555"></asp:Label>
                                                            </div>

                                                         

                                                            <div style="margin-top: 10px; text-align: left;margin-bottom :20px">
                                                             <asp:Label ID="ky_inst_lbl" runat="server" meta:resourcekey="ky_inst_lblResource1"
                                                             class="test-body"
                                                            Text="<strong>Personal Asset Analyser Test Instructions</strong><br />• This test comprises of 90 statements/ questions, each giving you 3 choices(A, B, C). <br />• Please see the example below for better understanding.<br />• There is no right or wrong answer, select the one which comes naturally to your mind. <br />• Each individual is different and has his/her own preferences and choices, hence choose the answer which you find most suitable. <br />• Though this is not a time-bound test, please do not take too much time to make your selection.<br /> Use the middle answer only when it is absolutely impossible to lean towards one of the other answer choices.<br />"></asp:Label>

                                                            </div>

                                                      <div style="margin-top: 10px; text-align: center;margin-bottom :20px">
                                                          <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary btn-lg test-body" 
                                                OnClick="lbContinue_Click" meta:resourcekey="LinkButton1Resource1" Text="Click to Continue...."></asp:LinkButton>&nbsp;
                                       
                                                           
    
                               

                                  </div>  
                            </div></div></div>
</asp:Content>

