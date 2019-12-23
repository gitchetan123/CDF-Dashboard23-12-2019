<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="linkedin.aspx.cs" Inherits="linkedin" %>

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



    <div class="panel panel-info" align="center">
        <div style="padding-top: 30px" class="panel-body">

            <div style="margin-top: 10px">
                <div id="div_msg" runat="server" style="text-align: center; margin-bottom: 25px">
                </div>
                <form id="form1" runat="server">
                    <%--<asp:Button ID="Button1" class="btn btn-social btn-linkedin" Text="Connect with LinkedIn" runat="server" OnClick="Authorize" />--%>
                    <asp:LinkButton ID="Button1" class="btn btn-social btn-linkedin" runat="server" OnClick="Authorize"> <span class="fa fa-linkedin"></span>Connect with LinkedIn </asp:LinkButton>
                    <asp:Panel ID="pnlDetails" runat="server" Visible="false">

                        <asp:Image ID="imgPicture" runat="server" /><br />
                        Name:
                        <asp:Label ID="lblName" runat="server" /><br />
                                            LinkedInId:
                        <asp:Label ID="lblLinkedInId" runat="server" /><br />
                                            Location:
                        <asp:Label ID="lblLocation" runat="server" /><br />
                                            EmailAddress:
                        <asp:Label ID="lblEmailAddress" runat="server" /><br />
                                            Industry:
                        <asp:Label ID="lblIndustry" runat="server" /><br />
                                            Headline:
                        <asp:Label ID="lblHeadline" runat="server" /><br />
                                            Public-url:
                        <asp:Label ID="lblurl" runat="server" /><br />
                    </asp:Panel>
                </form>

            </div>
        </div>
    </div>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>

</asp:Content>


