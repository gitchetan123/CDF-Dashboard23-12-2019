<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="dheya-updates.aspx.cs" Inherits="CDF_dheya_updates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Dheya Updates</h2>
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
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <asp:TextBox ID="lblDheyaUpdates" runat="server" TextMode="MultiLine" width="1000px" Height="100px" wrap="true"></asp:TextBox>      
                                      
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

