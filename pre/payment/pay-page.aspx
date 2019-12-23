<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pay-page.aspx.cs" Inherits="pre_payment_pay_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- plugins:css -->
    <link rel="stylesheet" href="../../vendors/iconfonts/mdi/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="../../vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="../../vendors/css/vendor.bundle.addons.css" />
    <!-- endinject -->
    <!-- plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../../css/style.css" />
    <!-- endinject -->
    <link rel="shortcut icon" href="../../images/favicon.png" />
    <style type="text/css">
        .text_f {
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="container-scroller">
                <div class="container-fluid page-body-wrapper full-page-wrapper auth-page">
                    <div class="content-wrapper d-flex align-items-center auth auth-bg-1 theme-one">
                        <div class="row w-100">
                            <div class="col-lg-5 mx-auto">
                                <h3>OPTION 1</h3>
                                <div class="auto-form-wrapper">
                                    <h5>Make One Time Payment and get 1500 OFF</h5>
                                    <hr />
                                    <asp:Panel ID="Panel_full" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvCustomPay_full" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" CssClass="table table-responsive"
                                                Width="494px"
                                                PageSize="20" OnRowCommand="gvCustomPay_RowCommand" DataKeyNames="id">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="CDF Training"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                                        <HeaderStyle Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btn_pay_full" CssClass="btn btn-success btn-sm"
                                                                        runat="server" Text="click here to payment" CommandArgument='<%# Eval("Amount") + "," +  Eval("id")%>' CommandName="pay" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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
                                    </asp:Panel>
                                    <hr />
                                    <p class="text_f text-center">
                                        © Copyright
                                            <script>document.write(new Date().getFullYear());</script>
                                        &nbsp;| <a href="https://dheya.com" target="_blank">Dheya Career Mentors</a>
                                    </p>
                                </div>
                            </div>


                            <div class="col-lg-5 mx-auto">
                                <h3>OPTION 2</h3>
                                <div class="auto-form-wrapper">
                                    <%-- <form action="#">--%>
                                    <h5>Advance Payment to Confirm Participation</h5>
                                    <asp:Panel ID="Panel_Advance" runat="server">
                                        <div class="table-responsive">

                                            <asp:GridView ID="gvCustomPay_advance_fix" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" CssClass="table table-responsive"
                                                Width="494px"
                                                PageSize="20" OnRowCommand="gvCustomPay_RowCommand" DataKeyNames="id">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Advance Payment"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                                        <HeaderStyle Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btn_pay_advance_fix" CssClass="btn btn-success btn-sm"
                                                                        runat="server" Text="click here to payment" CommandArgument='<%# Eval("Amount") + "," +  Eval("id")%>' CommandName="pay" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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
                                            <br />
                                        </div>
                                    </asp:Panel>
                                    <hr />
                                    <h5>Balance Payment During Basic Training Program</h5>
                                    <asp:Panel ID="Panel_Balance" runat="server" Enabled="false">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvCustomPay_balance" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" CssClass="table table-responsive"
                                                Width="494px"
                                                PageSize="20" OnRowCommand="gvCustomPay_RowCommand" DataKeyNames="id">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Balance Payment"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                                        <HeaderStyle Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btn_pay_balance" CssClass="btn btn-success btn-sm"
                                                                        runat="server" Text="click here to payment" CommandArgument='<%# Eval("Amount") + "," +  Eval("id")%>' CommandName="pay" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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
                                            <hr />

                                            <p class="text_f text-center">
                                                © Copyright
                                            <script>document.write(new Date().getFullYear());</script>
                                                &nbsp;| <a href="https://dheya.com" target="_blank">Dheya Career Mentors</a>
                                            </p>

                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- content-wrapper ends -->
                </div>
                <!-- page-body-wrapper ends -->
            </div>
            <!-- container-scroller -->
            <!-- plugins:js -->
            <script src="../../vendors/js/vendor.bundle.base.js"></script>
            <script src="../../vendors/js/vendor.bundle.addons.js"></script>
            <!-- endinject -->
            <!-- inject:js -->
            <script src="../../js/off-canvas.js"></script>
            <script src="../../js/misc.js"></script>
            <!-- endinject -->
        </div>
    </form>
</body>
</html>
