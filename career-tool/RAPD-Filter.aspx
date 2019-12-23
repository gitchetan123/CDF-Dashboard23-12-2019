<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="RAPD-Filter.aspx.cs" Inherits="career_tool_RAPD_Filter" %>

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
                <h2>Careers By RAPD</h2>
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
                            R Score</label>
                        <div class="col-md-5">

                            <asp:DropDownList ID="drop_rScore" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drop_rScore"
                                Display="Dynamic" ErrorMessage="Please Select R Score" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            A Score</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_aScore" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drop_aScore"
                                Display="Dynamic" ErrorMessage="Please Select A Score" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            P Score</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_pScore" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drop_pScore"
                                Display="Dynamic" ErrorMessage="Please Select P Score" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            D Score</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_dScore" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drop_dScore"
                                Display="Dynamic" ErrorMessage="Please Select D Score" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row" id="div_occupationCategory" runat="server" visible="false">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Occupation Category</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_occupationCategory" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drop_occupationCategory_SelectedIndexChanged">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                        </div>
                    </div>
                    <div class="row" id="div_carrerCategory" runat="server" visible="false">
                        <label for="name" class="col-md-3 col-md-offset-1 control-label">
                            Career Category</label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="drop_carrerCategory" runat="server" class="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                        </div>
                    </div>
                    <div class="ln_solid"></div>
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-5 col-md-offset-4">

                            <asp:Button ID="btn_preview" runat="server" class="btn btn-primary col-sm-8 col-sm-offset-2" Text="Preview"
                                OnClick="btn_preview_Click" />
                        </div>
                    </div>

                    <div class="panel panel-primary" id="filter" runat="server">

                        <div class="row">
                            <div class="col-md-2 "></div>
                            <div class="col-md-8 text-info">
                                <h3>Careers List</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 "></div>
                            <div class="col-md-8 text-info">
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" CssClass="table table-responsive"
                                            Width="100%" DataKeyNames="ca_id">

                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <%--<asp:BoundField DataField="rScore" HeaderText="R Score" SortExpression="rScore" />
                                                <asp:BoundField DataField="aScore" HeaderText="A Score" SortExpression="aScore" />
                                              <asp:BoundField DataField="pScore" HeaderText="P Score" SortExpression="pScore" />
                                                <asp:BoundField DataField="dScore" HeaderText="D Score" SortExpression="dScore" />--%>
                                                <asp:BoundField DataField="CareerCategory" HeaderText="Career Category" SortExpression="CareerCategory" />
                                                <asp:BoundField DataField="OccupationalCategory" HeaderText="Occupational Category" SortExpression="OccupationalCategory" />
                                                <asp:BoundField DataField="Career" HeaderText="Career Name" SortExpression="Career" />

                                                <asp:TemplateField HeaderText="View Career">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlView" runat="server" NavigateUrl='<%# Eval("ca_id", "~/Search/careerdetails.aspx?id={0}") %>'
                                                            CssClass="bodytext">View</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                            <PagerStyle BackColor="#284775" CssClass="pagination-ys" />
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
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

