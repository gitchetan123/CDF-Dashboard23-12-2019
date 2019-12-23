<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="docaudio.aspx.cs" Inherits="resources_docaudio" %>

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
            <h2>Knowledge Sharing Audio</h2>
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
            <div class="row text-left" style="font-size: 16px;">
                <ul>
                    <li>Visualization Audio For Navigator</li>
                        <audio controls controlsList="nodownload">
                            <source src="Sheyansh-%20Full%20Visualisation.mp3" type="audio/ogg"  /><%--<audio src="Sheyansh-%20Full%20Visualisation.mp3" controls="controls" />--%>
                            <!--<source src="horse.mp3" type="audio/mpeg">-->Audio
                        </audio>

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

