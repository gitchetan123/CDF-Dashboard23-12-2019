<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="ticket_success.aspx.cs" Inherits="Candidate_referral_success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="panel panel-info">
            <div style="padding-top: 10px" class="panel-body">
                <div style="margin-top: 10px">
                    <div id="div_success" runat="server" class=" alert alert-success" style="text-align: center; margin-bottom: 25px">
                        <b>You have successfully generated ticket. Thank you.....!</b>
                    </div>
                    <div class="form-group text-info" align="center" style="margin-bottom: 25px">
                        <label class="control-label">
                            <h5>Do you want to create another ticket &nbsp;<asp:LinkButton ID="LinkButton1"
                                runat="server" OnClick="LinkButton1_Click">Click Here</asp:LinkButton></h5>
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </form>
     <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
