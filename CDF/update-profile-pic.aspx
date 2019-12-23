<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="update-profile-pic.aspx.cs" Inherits="CDF_update_profile_pic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="CSS/uploadify.css" />

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
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Update Profile Picture</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div id="div_msg" runat="server" style="text-align: center; margin-left: 10px; margin-right: 10px">
                </div>

                <div class="row form-group">
                    <div class="col-md-6 col-sm-12 col-xs-12">
                        Upload New Photo (Passport size photo with plain background):
                        <asp:FileUpload ID="file_image" onchange="validateFileSize();" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="Validators" Display="Dynamic"
                            ControlToValidate="file_image" runat="server" ErrorMessage="Photo is required."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="(([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.bmp|.GIF|.JPG|.JPEG|.PNG|.BMP)$)$"
                            ControlToValidate="file_image" runat="server" ForeColor="Red" ErrorMessage="Please select a valid png,jpg,jpeg or bmp File."
                            Display="Dynamic" /><br />
                        <div id="dvMsg" style="background-color: Red; color: White; width: 230px; padding: 3px; display: none;">
                            Maximum file size allowed is 2 MB
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12" style="margin-top: 8px;">
                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="Button1_Click" />
                    </div>
                </div>

            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

