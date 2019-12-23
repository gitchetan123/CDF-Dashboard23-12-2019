<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="PreviewCDFinfo.aspx.cs" Inherits="CDF_PreviewCDFinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Preview CDF</h2>
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
                        <div id="div_msg" runat="server" class="" style="text-align: center; padding-top: 10px;"></div>
                        <div class="row" style="padding-top: 10px;">
                            <label class="col-md-2 col-md-offset-2  control-label">&nbsp;Email Id</label>
                            <div class="col-md-4">
                                <asp:TextBox class="form-control" ID="txt_email" runat="server"
                                    placeholder="Email ID" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-2 col-md-offset-2  control-label">&nbsp;First Name</label>
                            <div class="col-md-4">
                                <asp:TextBox class="form-control" ID="txt_fname" runat="server"
                                    placeholder="First Name" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-2 col-md-offset-2  control-label">&nbsp;City</label>
                            <div class="col-md-4">
                                <asp:TextBox class="form-control" ID="txt_city" runat="server"
                                    placeholder="City" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-offset-3 col-md-3">
                                <asp:Button ID="btn_preview" class="btn btn-primary btn-block"
                                    runat="server" Text="Preview"
                                    ValidationGroup="a" OnClick="btn_preview_Click" />
                            </div>
                            <div class=" col-md-3">
                                <asp:Button ID="btn_clear" class="btn btn-primary btn-block"
                                    runat="server" Text="Clear" OnClick="btn_clear_Click" />
                            </div>
                        </div>

                    </div>
                </div>

                <div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GridView1_DataBound"
                                Width="100%" DataKeyNames="uId" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                                PageSize="15" AllowSorting="True">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="uId" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="uId" />
                                    <asp:BoundField DataField="fname" HeaderText="First Name" SortExpression="fname" />
                                    <asp:BoundField DataField="lname" HeaderText="Last Name" SortExpression="lname" />
                                    <asp:BoundField DataField="dheyaEmail" HeaderText="Email ID" SortExpression="dheyaEmail" />
                                    <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                                    <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
                                    <asp:BoundField DataField="cdfLevel" HeaderText="CDF Level" SortExpression="cdfLevel" />
                                    <asp:BoundField DataField="userStatus" HeaderText="Status" SortExpression="userStatus" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="testView" runat="server" NavigateUrl='<%# Eval("uId", "cdfedit.aspx?cdf_id={0}") %>'>View Info</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../../js/custom.min.js"></script>
</asp:Content>

