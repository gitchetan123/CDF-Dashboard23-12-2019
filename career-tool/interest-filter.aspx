<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="interest-filter.aspx.cs" Inherits="InterestFilter" %>

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
                <h2>Careers By Interest</h2>
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
                            Interest1</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_interest1" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>Administrative</asp:ListItem>
                                <asp:ListItem>Adventurous</asp:ListItem>
                                <asp:ListItem>Artistic</asp:ListItem>
                                <asp:ListItem>Business and Commercial</asp:ListItem>
                                <asp:ListItem>Clerical</asp:ListItem>
                                <asp:ListItem>Computational</asp:ListItem>
                                <asp:ListItem>Literary</asp:ListItem>
                                <asp:ListItem>Management</asp:ListItem>
                                <asp:ListItem>Mechanical</asp:ListItem>
                                <asp:ListItem>Musical</asp:ListItem>
                                <asp:ListItem>Outdoor</asp:ListItem>
                                <asp:ListItem>Persuasive</asp:ListItem>
                                <asp:ListItem>Scientific</asp:ListItem>
                                <asp:ListItem>Social Services</asp:ListItem>
                                <asp:ListItem>Teaching</asp:ListItem>
                                <asp:ListItem>Technical</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drop_interest1"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Interest2</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_interest2" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>Administrative</asp:ListItem>
                                <asp:ListItem>Adventurous</asp:ListItem>
                                <asp:ListItem>Artistic</asp:ListItem>
                                <asp:ListItem>Business and Commercial</asp:ListItem>
                                <asp:ListItem>Clerical</asp:ListItem>
                                <asp:ListItem>Computational</asp:ListItem>
                                <asp:ListItem>Literary</asp:ListItem>
                                <asp:ListItem>Management</asp:ListItem>
                                <asp:ListItem>Mechanical</asp:ListItem>
                                <asp:ListItem>Musical</asp:ListItem>
                                <asp:ListItem>Outdoor</asp:ListItem>
                                <asp:ListItem>Persuasive</asp:ListItem>
                                <asp:ListItem>Scientific</asp:ListItem>
                                <asp:ListItem>Social Services</asp:ListItem>
                                <asp:ListItem>Teaching</asp:ListItem>
                                <asp:ListItem>Technical</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drop_interest2"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
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
                <div class="panel panel-primary" id="filter" runat="server">
                    <table class="table text-info">
                        <thead>
                            <tr>
                                <td width="50%">Totally compatible career as per Interest &nbsp;=&nbsp;<asp:Label ID="Label1" runat="server"
                                    Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </thead>
                    </table>
                    <div class="row">
                        <div class="col-md-2 ">
                        </div>
                        <div class="col-md-8 text-info">
                            <h3>Career Category Wise Compatibility</h3>
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
                            <h3>Occupational Category Wise Compatibility</h3>
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
                            <h3>Interest Career</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GridView1_DataBound"
                                        Width="100%" DataKeyNames="ca_id" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Interest1" HeaderText="Interest 1" SortExpression="Interest1" />
                                            <asp:BoundField DataField="Interest2" HeaderText="Interest 2" SortExpression="Interest2" />
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category"
                                                SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />
                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("ca_id", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center"
                                            CssClass="pagination-ys" Wrap="True" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#top">Top</asp:HyperLink>
                    <div class="row">
                        <div class="col-md-10">
                            Note :1) Please Choose the searching options as per your preference.<br />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;2) Please Click on select link to preview the Details
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
