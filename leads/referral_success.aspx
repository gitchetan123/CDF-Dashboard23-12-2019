<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="referral_success.aspx.cs" Inherits="Candidate_referral_success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
     <%-- Prevent cut , copy paste start code--%>
    <script
        src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });
        });
    </script>
     <%-- Prevent cut , copy paste end code --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="x_panel" style="margin-top: 20px">

            <div class="x_content">
                <br />

                <div id="div_success" runat="server" class=" alert alert-success" style="text-align: center; margin-bottom: 25px">
                    <button type="button" class="close" data-dismiss="alert" style="color:#ffffff;">&times;</button>
                    <b>You have successfully refered a candidate. Thank you.....!</b>
                </div>
                <div class="form-group text-info" align="center" style="margin-bottom: 25px">
                    <label class="control-label">
                        <h5>Do you want to refer another candidate &nbsp;<asp:LinkButton ID="LinkButton1"
                            runat="server" OnClick="LinkButton1_Click">Click Here</asp:LinkButton></h5>
                    </label>
                </div>

            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>
