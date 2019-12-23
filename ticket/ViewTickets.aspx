<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="ViewTickets.aspx.cs" Inherits="Ticket_ViewTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row-space {
            padding: 15px;
        }

    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>View Ticket</h2>
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
                    <div id="div_Error" runat="server" class="alert alert-warning" style="text-align: center; margin: 8px;"></div>

                    <div class="row row-space" style="padding-top: 20px">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">
                            Subject</label>
                        <div class="col-md-5 col-sm-12 col-xs-12">
                            <asp:TextBox ID="txt_subject" placeholder="Subject" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                 <div class="ln_solid"></div>
                    <div class="row row-space form-group">
                        <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4">
                            <asp:Button ID="btn_preview" runat="server" class="btn btn-primary btn-block" Text="Preview"
                                OnClick="btn_preview_Click" />
                        </div>
                    </div>
                </div>

                <div class="">
                    <asp:Label ID="lbl_rowcount" class="control-label col-md-8 " runat="server"></asp:Label>
                    <div class="">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table"
                            Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="15" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>--%>
                                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="View Solution">

                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("id", "ViewSolution.aspx?id={0}") %>'
                                            CssClass="bodytext">View</asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>--%>
                            </Columns>
                           <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" Wrap="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
     
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>

</asp:Content>

