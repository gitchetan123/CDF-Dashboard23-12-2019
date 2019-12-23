<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="docvideo.aspx.cs" Inherits="resources_docvideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://f.vimeocdn.com/js/froogaloop2.min.js"></script>
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
    <div class="x_panel">
        <div class="x_title">
            <h2>Knowledge Sharing Videos</h2>
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
            <div class="row text-left" style="font-size:16px;">
                <ul>
                    <li><a href="https://dheya.wistia.com/medias/5d9rsf7n88?wtime=0" target="iframe_a">RAPD Webinar</a></li>
                    <li><a href="https://player.vimeo.com/video/210739157" target="iframe_a">Live Session with Student</a> (Password - Dheya@123)</li>
                    <li><a href="https://player.vimeo.com/video/234472035" target="iframe_a">Dheya: Chai Pe Charcha</a> (Password - #Dheya@123)</li>
                    <li><a href="https://player.vimeo.com/video/234493699" target="iframe_a">Brief on JV process- Pitch for Schools video</a> (Password - #Dheya@1234)</li>
<%--                    <li><a href="https://player.vimeo.com/video/244986376" target="iframe_a">Market outreach program</a></li>
                    <li><a href="https://player.vimeo.com/video/245323798" target="iframe_a">Institutional Connect: </a> (Password - Dheya@123)</li>--%>
                </ul>
            </div>
            <div class="row">
                <iframe id="Iframe1" src="" name="iframe_a" width="100%" style="height: 100vh;" frameborder="0" runat="server"></iframe>
            </div>
        </div>
    </div>

    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

