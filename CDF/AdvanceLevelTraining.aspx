<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="AdvanceLevelTraining.aspx.cs" Inherits="CDF_AdvanceLevelTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .img {
            border-radius: 100px;
            width: 160px;
            height: 160px;
        }

        .card_text {
            font-size: 18px;
            font-family: Poppins;
            font-weight: normal;
            color: darkslategrey;
        }

        .row {
            padding: 5px;
        }

        .panel {
            padding: 10px;
            text-align: center;
            max-width: 700px;
            padding: 5px;
            margin-top: 50px;
        }
    </style>
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="x_panel" style="height: 190px;">
                <div class="x_title">
                    <h2>Advance Level 3 Days Training Program</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="form-horizontal" role="form" runat="server">

                        <div class="x_content">
                                <div align="center">
                                    <asp:GridView ID="gvAdvanceLevel" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" CssClass="table table-responsive"
                                        Width="100%" DataKeyNames="pId"
                                        PageSize="20" OnRowCommand="gvAdvanceLevel_RowCommand" OnRowDataBound="gvAdvanceLevel_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="shortDescription" HeaderText="Name" SortExpression="Name">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="price" HeaderText="Amount" SortExpression="Amount">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btn_pay_Advance_fix" CssClass="btn btn-success btn-sm"
                                                                runat="server" Text="click here to payment" CommandArgument='<%# Eval("price") + "," +  Eval("pId")%>' CommandName="pay" />
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
                        </div>
                    </div>
                </div>
            </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</asp:Content>

