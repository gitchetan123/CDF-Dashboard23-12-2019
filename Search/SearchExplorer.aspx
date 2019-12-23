<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="SearchExplorer.aspx.cs" Inherits="Search_SearchExplorer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
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
    <form id="Form1" role="form" class="form-horizontal" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Search Explorer Information</h2>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="row">
                                <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">
                                    Choose Your Current Education:</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCourses" runat="server" class="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
                                        <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="10th" Text="10th"></asp:ListItem>
                                        <asp:ListItem Value="12th" Text="12th"></asp:ListItem>
                                        <asp:ListItem Value="Graduation" Text="Graduation"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourses"
                                        Display="Dynamic" ErrorMessage="Choose Your Current Education" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">
                                    <asp:Label ID="lbl1" runat="server" Visible="false"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlNext1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNext1_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNext1"
                                        Display="Dynamic" ErrorMessage=" " InitialValue="--Select--">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="row">
                                <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">
                                    <asp:Label ID="lbl2" runat="server" Visible="false"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlNext2" runat="server" class="form-control" Visible="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNext2"
                                        Display="Dynamic" ErrorMessage=" " InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                </div>
                            </div>



                            <div class="ln_solid"></div>
                            <div class="row" style="margin-bottom: 20px;">
                                <div class="col-md-5 col-md-offset-4">
                                    <asp:Button ID="btn_preview" runat="server" class="btn btn-primary col-sm-8 col-sm-offset-2" Text="Search"
                                        OnClick="btn_preview_Click" />
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-8">

                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table" CellPadding="4" DataKeyNames="co_id"
                                    GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                    PageSize="20" Width="100%" ForeColor="#333333">
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Course Title">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" height="22" width="50%">&nbsp;&nbsp;
                                            <asp:HyperLink ID="hlCareername" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                Font-Size="11px" Font-Underline="False" ForeColor="#404040">[hlCoursename]</asp:HyperLink></td>
                                                        <td align="center" height="22" width="50%">&nbsp;&nbsp;
                                            <asp:Label ID="lblCategory" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                Font-Size="11px" Font-Underline="False" ForeColor="#404040">[lblCategory]</asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" height="22" width="50%">Course Title</td>
                                                        <td align="center" height="22" width="50%">Course Category</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="co_id" HeaderText="co_id" SortExpression="co_id" />
                                        <asp:BoundField DataField="course1" HeaderText="Career Title" SortExpression="course1" />
                                        <asp:BoundField DataField="course6" HeaderText="Course Category" SortExpression="course6" />
                                    </Columns>
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <!-- /form -->
   <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
