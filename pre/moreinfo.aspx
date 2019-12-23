<%@ Page Title="" Language="C#" MasterPageFile="~/pre/PreCDFMasterPage.master" AutoEventWireup="true" CodeFile="moreinfo.aspx.cs" Inherits="pre_moreinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row-space {
            padding: 5px;
        }  
    </style>
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
                <h2>More Infro Update and NDA</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div class="row">
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-left: 10px; margin-right: 10px">
                    </div>
                    <div id="div_info" runat="server">
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    <span>Linked In Profile:
                                        <asp:TextBox ID="txt_linkedin" runat="server" placeholder="" class="form-first-name form-control"
                                            MaxLength="300"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    <span>Facebook Profile:
                                        <asp:TextBox ID="txt_facebook" runat="server" placeholder="" class="form-first-name form-control"
                                            MaxLength="300"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    <span>Twitter Profile:
                                        <asp:TextBox ID="txt_twitter" runat="server" placeholder="" class="form-first-name form-control"
                                            MaxLength="300"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    <span>After the training your profile along with your Pic will be displayed on Dheya's
                                        CDF online portal. Do let us know if you do not wish to display the same:
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                            ControlToValidate="ddl_profilepic" runat="server" ErrorMessage="This field is required."
                                            InitialValue="Select an Option">This field is required.</asp:RequiredFieldValidator><br />
                                        <asp:DropDownList ID="ddl_profilepic" runat="server" CssClass="form-control" >
                                            <asp:ListItem>Select an Option</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row row-space" style="margin-bottom:10px;">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group" >
                                    Resume:
                               Resume/CV (Only .pdf & .doc files):*
                                    <asp:FileUpload ID="file_resume" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="Validators" Display="Dynamic"
                                        ControlToValidate="file_resume" runat="server" ErrorMessage="Resume is required."
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="([a-zA-Z0-9\s_\\.()@\-:])+(.doc|.docx|.pdf)$"
                                        ControlToValidate="file_resume" runat="server" ForeColor="Red" ErrorMessage="Please select a valid Word or PDF File."
                                        Display="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    Attache Photo (Passport size photo with plain background):
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
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                                <div class="form-group">
                                    Attache Photo (Casual photo):
                                <asp:FileUpload ID="file_image2" onchange="validateFileSize2();" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" CssClass="Validators" Display="Dynamic"
                                        ControlToValidate="file_image2" runat="server" ErrorMessage="Photo is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="(([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.bmp|.GIF|.JPG|.JPEG|.PNG|.BMP)$)$"
                                        ControlToValidate="file_image2" runat="server" ForeColor="Red" ErrorMessage="Please select a valid png,jpg,jpeg or bmp File."
                                        Display="Dynamic" /><br />
                                    <div id="dvMsg2" style="background-color: Red; color: White; width: 230px; padding: 3px; display: none;">
                                        Maximum file size allowed is 2 MB
                                    </div>
                                </div>
                            </div>
                        </div>
                        <center>
                            <div>
                                <a href="#" data-toggle="modal" data-target="#myModal" style="color: #000000; font-size: 13px;">
                                    View <strong>Non-Disclosure Agreement(NDA)</strong></a>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" Display="Dynamic"
                                    ControlToValidate="rbl_nda" runat="server" ErrorMessage="NDA selection is required."></asp:RequiredFieldValidator>
                            </div>
                        </center>
                        <div class="form-group">
                            <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4 ">
                                <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block"
                                    Style="margin-bottom: 50px; margin-top: 20px;" Text="Submit" OnClick="btn_submit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">NDA</h4>
                    </div>
                    <div class="modal-body" style="height: 420px;">
                        <embed id="em" type="text/html" runat="server" style="width: 100%; height: 100%;" />
                        <asp:RadioButtonList ID="rbl_nda" runat="server" Style="float: right; margin-top: 5px;font-size:15px;"
                            OnSelectedIndexChanged="rbl_nda_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem>&nbsp;&nbsp;Agree&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="modal-footer" style="margin-top:25px;">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Save changes</button>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid_name" runat="server" />
        <asp:HiddenField ID="hid_address" runat="server" />
        <asp:HiddenField ID="hid_fileName" runat="server" />

    </form>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btn_submit.ClientID %>").disabled = true;
            document.getElementById("<%=btn_submit.ClientID %>").value = "Submiting..."
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function validateFileSize() {
            var uploadControl = document.getElementById('<%= file_image.ClientID %>');
            if (uploadControl.files[0].size > 2097152) {
                document.getElementById('dvMsg').style.display = "block";
                return false;
            }
            else {
                document.getElementById('dvMsg').style.display = "none";
                return true;
            }
        }
        function validateFileSize2() {
            var uploadControl = document.getElementById('<%= file_image2.ClientID %>');
            if (uploadControl.files[0].size > 2097152) {
                document.getElementById('dvMsg2').style.display = "block";
                return false;
            }
            else {
                document.getElementById('dvMsg2').style.display = "none";
                return true;
            }
        }
    </script>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

