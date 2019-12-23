<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="ScholarshipSearch.aspx.cs" Inherits="Search_ScholarshipSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
        }

        .panel {
            text-align: center;
            margin: 0 auto;
            padding-bottom: 20px;
        }

        .h {
            display: none;
        }
    </style>
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
    <form id="Form1" class="form-horizontal" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Scholarship Search</h2>
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
                <div class="panel panel-primary" style="max-width: 700px;">
                    <div class="panel-heading ">
                        <div class="panel-title ">
                            Scholarship Search
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="text-align: center; margin-left: 65px;">
                            <asp:DropDownList ID="ddlScholarship" runat="server" CssClass="form-control" Width="500">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="row">
                            <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="btn btn-primary" OnClick="btnPreview_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />
                        </div>
                    </div>
                    <div id="div_msg" runat="server" class="alert " style="text-align: center;">
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12 ">
                        <asp:GridView ID="gvScholarship" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="table table-striped" CellPadding="0" DataKeyNames="id" GridLines="Horizontal"
                            Width="100%" BorderColor="#E0E0E0" BorderStyle="Solid" ForeColor="#333333" BorderWidth="1px" OnPageIndexChanging="gvScholarship_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="scolarshipName" HeaderText="Scholarship Name" />
                                <asp:BoundField DataField="scolarshipFor" HeaderText="Scholarship For" />
                               <%-- <asp:BoundField DataField="websiteLink" HeaderText="Website Link" />--%>
                                <asp:HyperLinkField HeaderText="Website Link" Target="_blank"
                                    DataTextField="websiteLink"
                                    DataNavigateUrlFields="websiteLink"
                                    DataNavigateUrlFormatString="{0}" />
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" VerticalAlign="Top" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" Font-Bold="True" CssClass="pagination-ys"
                                Wrap="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />

            </div>
        </div>

    </form>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
