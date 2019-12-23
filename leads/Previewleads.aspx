<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="previewleads.aspx.cs" Inherits="Candidate_Previewleads" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row-space {
            padding: 15px;
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
    <form id="Form1" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Preview Referral Leads</h2>
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
                <div id="div_Error" runat="server" class="alert alert-warning alert-dismissable" style="text-align: center; margin: 8px;">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                </div>

                <div class="row row-space" style="padding-top: 20px">
                    <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">Referred By</label>
                    <div class="col-md-5 col-sm-12 col-xs-12">
                        <asp:TextBox ID="txt_name" placeholder="Lead Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="ln_solid"></div>
                <div class="row row-space">
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
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                        PageSize="15">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Contact Number" HeaderText="Contact Number" SortExpression="Contact Number">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email Address" HeaderText="Email Address" SortExpression="Email Address">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Date" HeaderText="Entered Date" SortExpression="Date"
                                DataFormatString="{0:d}">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Lead Status" HeaderText="Current Status" SortExpression="Lead Status">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Assigned_User" HeaderText="Assigned Executive" SortExpression="Assigned_User">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Number" HeaderText="Executive Number" SortExpression="Number">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
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
    <script src="../js/custom.js"></script>
</asp:Content>

