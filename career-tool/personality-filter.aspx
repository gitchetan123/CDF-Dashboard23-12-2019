<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="personality-filter.aspx.cs" Inherits="Filter_PersonalityFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        .panel {
            text-align: center;
            margin: 0 auto;
            padding-bottom: 20px;
        }

        .filterspace {
            padding-left: 20px;
            padding-right: 20px;
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
    <form id="Form1" class="form-horizontal" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Careers By Personality</h2>
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
                <div>
                    <div class="row" style="padding-top: 20px;">
                        <div class="" id="div_msg" runat="server" style="text-align: center; margin-left: 70px; margin-right: 70px;">
                        </div>
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 1</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drop_personality1_SelectedIndexChanged">
                                <%--<asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drop_personality1"
                                Display="Dynamic" ErrorMessage="Please Select Ability" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 2</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality2" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drop_personality2_SelectedIndexChanged">
                               <%-- <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drop_personality2"
                                Display="Dynamic" ErrorMessage="Please Select Ability" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 3</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality3" runat="server" class="form-control">
                                <%--<asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drop_personality3"
                                Display="Dynamic" ErrorMessage="Please Select Ability" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="ln_solid"></div>
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-5 col-md-offset-4">
                            <asp:Button ID="btn_preview" runat="server" class="btn btn-primary col-sm-8 col-sm-offset-2" Text="Preview"
                                OnClick="btn_preview_Click" />
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary filterspace" id="filter" runat="server">
                    <table class="table text-info">
                        <thead>
                            <tr>
                                <td width="50%">Totally compatible career as per personality &nbsp;=&nbsp;<asp:Label ID="Label1" runat="server"
                                    Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </thead>
                    </table>
                    <div class="row">
                        <div class="col-md-2 ">
                        </div>
                        <div class="col-md-8 text-info">
                            <h3>Career Category Wise Count</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:GridView ID="GridView4" CssClass="table" runat="server" AllowPaging="True" PageSize="1000" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="Chart1" runat="server" BorderlineDashStyle="Solid" BackImageWrapMode="Scaled"
                                BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid" SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 ">
                        </div>
                        <div class="col-md-8 text-info">
                            <h3>Occupational Category Wise Count</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:GridView ID="GridView5" CssClass="table" runat="server" AllowPaging="True" PageSize="1000" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="Chart2" runat="server" BorderlineDashStyle="Solid" BackImageWrapMode="Scaled"
                                BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid" SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 ">
                        </div>
                        <div class="col-md-8 text-info">
                            <h3>Suggested Careers AND Related Degrees</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:GridView ID="GridView6" CssClass="table" runat="server" AllowPaging="True" PageSize="1000" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-6">
                            <asp:GridView ID="GridView7" CssClass="table" runat="server" AllowPaging="True" PageSize="50" PagerStyle-VerticalAlign="Top" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                <PagerStyle VerticalAlign="Top" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

                                <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 ">
                        </div>
                        <div class="col-md-8 text-info">
                            <h3>Personality Careers</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <asp:GridView ID="GridView1" CssClass="table" runat="server" AllowPaging="True" PageSize="1000" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>

                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#top">Top</asp:HyperLink>
                    <div class="row">
                        <div class="col-md-10">
                            Note 1) Please Choose the searching options as per your preference.<br />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;2) Please Click on select link to preview the details
                of the Career.
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

