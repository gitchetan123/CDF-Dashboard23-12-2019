<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="ViewSolution.aspx.cs" Inherits="Ticket_ViewSolution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <form id="Form1" role="form" runat="server">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>View Ticket Status</h2>
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
                        <asp:Label ID="lbl_solution" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_rowcount" class="control-label col-md-8 " runat="server"></asp:Label>
                        <div class="row" align="center">
                            <div class="">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" Width="100%"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnRowCommand="GridView1_RowCommand">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Solution" HeaderText="Solution" SortExpression="Solution">
                                            <HeaderStyle Wrap="False" Width="80%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Modified Date" HeaderText="Modified Date" SortExpression="Modified Date">
                                            <HeaderStyle Wrap="False" Width="20%" />
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="Solution" FooterText="hello">
                        <FooterTemplate>
                            <asp:TextBox ID="txt_solution" Style="color: #000000;" Width="50%" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_submit" CssClass="btn btn-info" runat="server" Text="Save" />
                        </FooterTemplate>
                    </asp:TemplateField>--%>
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
                </div>
            </form>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <form id="WebToLeadForm" action="https://www.dheya.com/crm/index.php?entryPoint=WebToPersonCapture" method="post" name="WebToLeadForm">
                <embed>
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Your Queries</h2>
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
                                <div class="col-md-10 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="row">
                                        <label style="margin-left: 20px;">Add More Queries</label>
                                    </div>
                                    <textarea id="name" class="form-control" name="name" type="text" required=""></textarea>
                                    <input id="hf_cdf" type="hidden" runat="server" />
                                    <input id="case_id" name="case_id" type="text" hidden="" />
                                    <input id="description" name="description" type="text" hidden="" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4">
                                    <input class="btn btn-primary btn-block" style="margin-top: 20px;" name="Submit" onclick="submit_form();" type="submit" value="Submit" />
                                </div>

                                <input id="account_id" type="hidden" name="account_id" value="91500f39-3f02-efdb-4427-58afd9fb7e5b" />
                                <input name="campaign_id" id="campaign_id" type="hidden" value="e0510c1a-5f24-a41d-785c-589c3e08533d" />
                                <%--<input id="redirect_url" type="hidden" name="redirect_url" value="http://localhost:5968/ticket/ViewTickets.aspx" />--%>
                                <input id="redirect_url" type="hidden" runat="server" name="redirect_url" value="<%$ AppSettings: ViewTicketsPath %>" />
                                <input name="assigned_user_id" id="assigned_user_id" type="hidden" value="1" /><input name="moduleDir" id="moduleDir" type="hidden" value="AOP_Case_Updates" />
                            </div>
                        </div>
                    </div>
                </embed>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        function submit_form() {

            document.getElementById('case_id').value = document.getElementById('<%=hf_cdf.ClientID %>').value;
            document.getElementById('description').value = document.getElementById('name').value;
        }

    </script>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
