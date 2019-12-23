<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="EntranceExam.aspx.cs" Inherits="Search_EntranceExam" %>

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
                <h2>Entrance Exam Search</h2>
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
                            Entrance Exam Search
                        </div>
                    </div>
                    <div>
                        <br />
                        <div class="row" style="text-align: center; margin-left: 65px;">
                            <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control" Width="500">
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
                <div class="col-xs-12 table-responsive" style="border: 1px; font-size: small;">

                    <asp:GridView ID="gvExam" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="table table-striped" CellPadding="4" CellSpacing="0" DataKeyNames="entrance_ID" GridLines="Horizontal"
                        BorderColor="#E0E0E0" BorderStyle="Solid" ForeColor="#333333" BorderWidth="0px"
                        PageSize="5" OnPageIndexChanging="gvExam_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="entrance_ID" HeaderText="ID" />
                            <asp:BoundField DataField="entrance_name" HeaderText="Exam Name" />
                            <asp:BoundField DataField="exam_detail" HeaderText="Detail" />
                            <asp:BoundField DataField="exam_req" HeaderText="Requirement" />
                            <asp:BoundField DataField="exam_fee" HeaderText="Fee" />
                            <asp:BoundField DataField="exam_date" HeaderText="Exam Date" />
                            <asp:BoundField DataField="app_date" HeaderText="Application Date" />
                            <asp:HyperLinkField HeaderText="Application Link" Target="_blank"
                                DataTextField="app_link"
                                DataNavigateUrlFields="app_link"
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
        </div>

    </form>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
