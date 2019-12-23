<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="all-filter.aspx.cs" Inherits="AllFilter" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="x_panel">
            <div class="x_title">
                <h2>Careers By Ability, Interest and Personality</h2>
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
                            Ability 1</label>
                        <div class="col-md-5">

                            <asp:DropDownList ID="drop_ability1" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Observation and Concept Formation">Observation and Concept Formation</asp:ListItem>
                                <asp:ListItem Value="Spatial Awareness">Spatial Awareness</asp:ListItem>
                                <asp:ListItem Value="Observation and Spatial Transformation">Observation and Spatial Transformation</asp:ListItem>
                                <asp:ListItem Value="Abstract Reasoning">Abstract Reasoning</asp:ListItem>
                                <asp:ListItem Value="Visualization">Visualization</asp:ListItem>
                                <asp:ListItem Value="Memory Recall">Memory Recall</asp:ListItem>
                                <asp:ListItem Value="Numerical Ability">Numerical Ability</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drop_ability1"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Ability 2</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_ability2" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Observation and Concept Formation">Observation and Concept Formation</asp:ListItem>
                                <asp:ListItem Value="Spatial Awareness">Spatial Awareness</asp:ListItem>
                                <asp:ListItem Value="Observation and Spatial Transformation">Observation and Spatial Transformation</asp:ListItem>
                                <asp:ListItem Value="Abstract Reasoning">Abstract Reasoning</asp:ListItem>
                                <asp:ListItem Value="Visualization">Visualization</asp:ListItem>
                                <asp:ListItem Value="Memory Recall">Memory Recall</asp:ListItem>
                                <asp:ListItem Value="Numerical Ability">Numerical Ability</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drop_ability2"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Ability 3</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_ability3" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Observation and Concept Formation">Observation and Concept Formation</asp:ListItem>
                                <asp:ListItem Value="Spatial Awareness">Spatial Awareness</asp:ListItem>
                                <asp:ListItem Value="Observation and Spatial Transformation">Observation and Spatial Transformation</asp:ListItem>
                                <asp:ListItem Value="Abstract Reasoning">Abstract Reasoning</asp:ListItem>
                                <asp:ListItem Value="Visualization">Visualization</asp:ListItem>
                                <asp:ListItem Value="Memory Recall">Memory Recall</asp:ListItem>
                                <asp:ListItem Value="Numerical Ability">Numerical Ability</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drop_ability3"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Interest 1</label>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drop_interest1"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Interest 2</label>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drop_interest2"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 1</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality1" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drop_personality1"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 2</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality2" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drop_personality2"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Personality 3</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_personality3" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="Warm Hearted">Warm Hearted(Relationships)</asp:ListItem>
                                <asp:ListItem Value="Emotionally Stable">Emotionally Stable</asp:ListItem>
                                <asp:ListItem Value="Assertive">Assertive</asp:ListItem>
                                <asp:ListItem Value="Enthusiastic">Enthusiastic</asp:ListItem>
                                <asp:ListItem Value="Conscientious">Conscientious</asp:ListItem>
                                <asp:ListItem Value="Controlled">Controlled (Responsiveness)</asp:ListItem>
                                <asp:ListItem Value="Tough Minded">Tough Minded</asp:ListItem>
                                <asp:ListItem Value="Self-Assured">Self-Assured</asp:ListItem>
                                <asp:ListItem Value="Relaxed">Relaxed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drop_personality3"
                                Display="Dynamic" ErrorMessage="Please Select Interest" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="ln_solid"></div>
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-5 col-md-offset-4">
                            <asp:Button ID="btn_preview" runat="server" class="btn btn-primary col-sm-8 col-sm-offset-2"
                                Text="Preview" OnClick="btn_preview_Click" />
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary" id="filter" runat="server">

                    <table class="table text-info">
                        <thead>
                            <tr>
                                <td width="50%">Totally compatible career as per ability,Personality And Interest  &nbsp;=&nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td width="50%">Partially compatible - totally compatible career as per ability,Personality And Interest&nbsp;=&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
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
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVAbilityCC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHAbilityCC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVInterestCC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHInterestCC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVPersonalityCC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHPersonalityCC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVCombinedCC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHCombinedCC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
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
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVAbilityOC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHAbilityOC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVInterestOC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" CssClass="pagination-ys" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHInterestOC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVPersonalityOC" CssClass="table" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" CssClass="pagination-ys" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHPersonalityOC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVCombinedOC" runat="server" AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table">
                                        <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" ForeColor="#284775" />
                                        <PagerStyle BackColor="#284775" CssClass="pagination-ys" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:Chart ID="CHCombinedOC" runat="server" BorderlineDashStyle="Solid"
                                BackImageWrapMode="Scaled" BorderlineColor="Black">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BackImageWrapMode="Scaled" BorderDashStyle="Solid"
                                    SkinStyle="Sunken" />
                            </asp:Chart>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2 "></div>
                        <div class="col-md-8 text-info">
                            <h3>Total Compatibility - Ability</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVAbility" CssClass="table" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" OnDataBound="GDVAbility_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVAbility_RowDataBound" OnPageIndexChanging="GDVAbility_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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

                                </ContentTemplate>
                            </asp:UpdatePanel>







                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 "></div>
                        <div class="col-md-8 text-info">
                            <h3>Total Compatibility - Interest</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVInterest" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>

                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="GDVInterest" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVInterest_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVInterest_RowDataBound" OnPageIndexChanging="GDVInterest_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 "></div>
                        <div class="col-md-8 text-info">
                            <h3>Total Compatibility - Personality</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVPersonality" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>

                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="GDVPersonality" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVPersonality_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVPersonality_RowDataBound" OnPageIndexChanging="GDVPersonality_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 "></div>
                        <div class="col-md-8 text-info">
                            <h3>Total Compatibility - Combined</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVCombined" runat="server" AllowPaging="True" PageSize="100" Width="100%" >
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>

                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="GDVCombined" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVCombined_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVCombined_RowDataBound" OnPageIndexChanging="GDVCombined_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 "></div>
                        <div class="col-md-8 text-info">
                            <h3>Partial Compatibility</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVAbilityPC" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>


                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVAbilityPC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVAbilityPC_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVAbilityPC_RowDataBound" OnPageIndexChanging="GDVAbilityPC_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%--  <asp:GridView ID="GDVInterestPC" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>

                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="GDVInterestPC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVInterestPC_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVInterestPC_RowDataBound" OnPageIndexChanging="GDVInterestPC_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVPersonalityPC" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>

                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GDVPersonalityPC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVPersonalityPC_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVPersonalityPC_RowDataBound" OnPageIndexChanging="GDVPersonalityPC_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ">
                            <%-- <asp:GridView ID="GDVCombinedPC" runat="server" AllowPaging="True" PageSize="100" Width="100%">
                                <AlternatingRowStyle HorizontalAlign="Left" />
                                <RowStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>--%>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="GDVCombinedPC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table" OnDataBound="GDVCombinedPC_DataBound"
                                        Width="100%" DataKeyNames="CareerID" OnRowDataBound="GDVCombinedPC_RowDataBound" OnPageIndexChanging="GDVCombinedPC_PageIndexChanging"
                                        PageSize="15">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                            <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                            <asp:BoundField DataField="Career" HeaderText="Career" SortExpression="Career" />

                                            <asp:TemplateField HeaderText="View Career">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("CareerID", "~/Search/careerdetails.aspx?id={0}") %>'
                                                        CssClass="bodytext">View</asp:HyperLink>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#top">Top</asp:HyperLink>
                    <div class="row">
                        <div class="col-md-10">
                            Note :1) Please Choose the searching options as per your preference.<br />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;2) Please Click on select link to preview the Details of the Career. 
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <asp:HiddenField ID="HFAbility" runat="server" />
                    <asp:HiddenField ID="HFInterest" runat="server" />
                    <asp:HiddenField ID="HFPersonality" runat="server" />
                    <asp:HiddenField ID="HFAbilityP" runat="server" />
                    <asp:HiddenField ID="HFInterestP" runat="server" />
                    <asp:HiddenField ID="HFPersonalityP" runat="server" />

                </div>
            </div>
        </div>
    </form>
    <!-- /form -->
      <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
