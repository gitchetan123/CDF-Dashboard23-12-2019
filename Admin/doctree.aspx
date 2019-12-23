<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="doctree.aspx.cs" Inherits="resources_doctree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Information</h2>
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
                    <h3>CDF Documents Details</h3>
                    <asp:Button ID="btn_download" CssClass="btn btn-primary" Style="float: right; margin-top: -40px;" runat="server" Text="Download" OnClick="btn_download_Click" />
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4" style="margin: 0 0 0 0; padding: 30px 20px 300px 35px;">
                                <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0"
                                    OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ImageSet="XPFileExplorer" NodeIndent="15">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                    <NodeStyle CssClass="Color" Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                                </asp:TreeView>
                            </div>
                            <div class="col-md-8" style="padding: 20px 10px 0 20px;">
                                <iframe id="viewfile" src="" width="100%" style="height: 100vh;" frameborder="0" runat="server"></iframe>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

