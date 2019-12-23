<%@ Page Title="" Language="C#" MasterPageFile="~/cdf-test/Candidatemaster.master" AutoEventWireup="true"
    CodeFile="Separate_personality_test_status.aspx.cs" Inherits="candidate_Separate_personality_test_status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .panel {
            max-width: 800PX;
            padding-bottom: 15px;
            text-align: center;
        }
        .style1
        {
            height: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="panel panel-primary">
            <div class="panel-heading ">
                <div class="panel-title ">
                    <asp:Label ID="lblHeading" class="test-title " runat="server" Text="Test Dashboard"
                        meta:resourcekey="lblHeading"></asp:Label>
                </div>
            </div>
            <div id="div_msg" runat="server" style="text-align: center; margin-bottom: 20px; margin-top: 20px">
                <asp:Label ID="ErrMsg" runat="server" Font-Bold="True" ForeColor="Red" meta:resourcekey="ErrMsgResource1"></asp:Label>
            </div>
            <table class="table table-responsive">
                <thead style="" class="text-primary ">
                    <tr>
                        <td class="style1"></td>
                        <td class="style1">
                            <asp:Label ID="lblpersn" runat="server" Text="Test Details" meta:resourcekey="lblpersn"
                                Font-Bold="True" CssClass="test-sub-title"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lbl_click" runat="server" Text="Click Here to Start" meta:resourcekey="lbl_click"
                                Font-Bold="True" CssClass="test-sub-title"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblstatus" runat="server" Text="Status" meta:resourcekey="lblstatus"
                                Font-Bold="True" CssClass="test-sub-title"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblno1" runat="server" Text="1." meta:resourcekey="lblno1"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:Label ID="lblknow" runat="server" Text="Know Your Self" meta:resourcekey="lblknow"
                                CssClass="test-body"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_KY" runat="server" ImageUrl="~/images/TestImages/start-icon.png"
                                Width="30px" ToolTip="Start test" OnClick="btn_interest_Click" meta:resourcekey="btn_SAResource1"
                                CssClass="out" Height="30px" 
                                OnClientClick="javascript:KY_test_window_open();" />
                        </td>
                        <td>
                            <asp:Label ID="sts_KY" runat="server" meta:resourcekey="sts_KYResource2" 
                                CssClass="test-body"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblno2" runat="server" Text="2." meta:resourcekey="lblno2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblpersonality" runat="server" Text="Personality Test" meta:resourcekey="lblpersonality"
                                CssClass="test-body"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_PT" runat="server" ImageUrl="~/images/TestImages/start-icon.png"
                                Width="30px" ToolTip="Start test" OnClick="btn_PD_Click" meta:resourcekey="btn_OSTResource1"
                                CssClass="out" Height="30px" OnClientClick="javascript:PD_test_window_open();" />
                        </td>
                        <td>
                            <asp:Label ID="sts_PD" runat="server" meta:resourcekey="sts_OSTResource2" CssClass="test-body"></asp:Label>
                        </td>
                    </tr>
                   <tr><td colspan="5">
                
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkbox-inline"
                    Text="Do you want audio base test" AutoPostBack="True" Visible="false"
                    oncheckedchanged="CheckBox1_CheckedChanged"/>

                    </ContentTemplate>
                </asp:UpdatePanel>
                </td></tr>
                </tbody>
            </table>
            <div style="margin-top: 10px; text-align: center; margin-bottom: 5px; padding: 15px">
                <asp:Label ID="lblNoteM" runat="server" class="test-sub-body" Text="Note :" ForeColor="Red"
                    meta:resourcekey="lblNoteM"></asp:Label>
                <asp:Label class="test-sub-body" ID="lblNote" runat="server" meta:resourcekey="lblNote"
                    Text=" This test is collection of two tests. These are also not time bound tests. So be true about yourself and enjoy tests."></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
