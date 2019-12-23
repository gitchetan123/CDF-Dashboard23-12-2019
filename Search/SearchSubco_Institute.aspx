<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="SearchSubco_Institute.aspx.cs" Inherits="Search_SearchSubco_Institute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding: 5px;
        }

        .panel {
            text-align: center;
            margin: 0 auto;
            padding-bottom: 20px;
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
                <h2>Search Career, Course, Subcourse & Institute</h2>
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
                <div class="panel panel-primary" style="max-width: 700px;">
                    <div class="panel-heading ">
                        <div class="panel-title ">
                            Search Career, Course, Subcourse & Institute
                        </div>
                    </div>
                    <div id="div_msg" runat="server" class="alert " style="text-align: center;">
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <label class="col-md-3 col-md-offset-1 control-label">
                            Career Name</label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="ddl_career" runat="server" SkinID="DropDown" class="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_career_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_career"
                    Display="Static" ErrorMessage="Please select Career Name" InitialValue="--Select--">*</asp:RequiredFieldValidator>      
                            --%>
                        </div>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="row">
                                    <label class="col-md-3 col-md-offset-1 control-label">
                                        Course Name</label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddl_course" runat="server" SkinID="DropDown" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_course_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_course"
                    Display="Static" ErrorMessage="Please select Course Name" InitialValue="--Select--">*</asp:RequiredFieldValidator>      
                                        --%>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_career" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="row">
                                    <label class="col-md-3 col-md-offset-1 control-label">
                                        Sub-Course Name</label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddl_subconame" runat="server" SkinID="DropDown" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_subconame_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_subconame"
                                            Display="Static" ErrorMessage="Please select Sub-Course Name" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_course" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_instname" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="row">
                                    <label class="col-md-3 col-md-offset-1 control-label">
                                        Specialization</label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddl_specialization" runat="server" SkinID="DropDown" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_specialization"
                    Display="Static" ErrorMessage="Please select Specialization" InitialValue="--Select--">*</asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_subconame" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-1 control-label">
                            State</label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="ddl_state" runat="server" SkinID="DropDown" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_specialization"
                    Display="Static" ErrorMessage="Please select Specialization" InitialValue="--Select--">*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 col-md-offset-1 control-label">
                            City</label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="ddl_city" runat="server" SkinID="DropDown" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="row">
                                    <label class="col-md-3 col-md-offset-1 control-label">
                                        Institute Name</label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddl_instname" runat="server" SkinID="DropDown" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_instname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txt_instname" placeholder="Name of Institute" class="form-control"
                    runat="server"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_subconame" EventName="SelectedIndexChanged" />
                        </Triggers>

                    </asp:UpdatePanel>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-4 col-md-offset-2">
                            <asp:Button ID="btn_preview" runat="server" class="btn btn-info btn-block" Text="Preview"
                                OnClick="btn_preview_Click" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btn_clear" runat="server" class="btn btn-info btn-block" Text="Clear"
                                ValidationGroup="clear" OnClick="btn_clear_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 ">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="table" CellPadding="0" DataKeyNames="inst_id" GridLines="Horizontal"
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                            Width="100%" BorderColor="#E0E0E0" BorderStyle="Solid" ForeColor="#333333" BorderWidth="1px"
                            PageSize="20">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Institute Name">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" width="45%" height="22">&nbsp;&nbsp;
                                        <asp:HyperLink ID="Instname" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Font-Underline="true" ForeColor="#404040" Font-Bold="true">[Instname]</asp:HyperLink>
                                                </td>
                                                <td align="left" width="10%" height="22">&nbsp;&nbsp;
                                        <asp:Label ID="lblCity" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Underline="False"
                                            ForeColor="#404040" Font-Bold="true">[lblCity]</asp:Label>
                                                </td>
                                                <td align="left" width="30%" height="22">&nbsp;&nbsp;
                                        <asp:Label ID="lblsubconame" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Font-Underline="False" ForeColor="#404040" Font-Bold="true">[lblsubconame]</asp:Label>
                                                </td>
                                                <td align="left" width="15%" height="22">&nbsp;&nbsp;
                                        <asp:Label ID="lblspecialization" runat="server" Font-Names="Verdana" Font-Size="10px"
                                            Font-Underline="False" ForeColor="#404040" Font-Bold="true">[lblspecialization]</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" width="45%" height="22">Institute Name
                                                </td>
                                                <td align="left" width="10%" height="22">City
                                                </td>
                                                <td align="left" width="30%" height="22">Sub-Course Name
                                                </td>
                                                <td align="left" width="15%" height="22">Specialization
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="inst_id" HeaderText="Inst_id" SortExpression="inst_id" />
                                <asp:BoundField DataField="inst_name" HeaderText="Institute Name" SortExpression="inst_name" />
                                <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                                <asp:BoundField DataField="specialization" HeaderText="Specialization" SortExpression="specialization" />
                                <asp:BoundField DataField="subco_name" HeaderText="Sub-Course Name" SortExpression="subco_name" />
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" VerticalAlign="Top" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" Font-Bold="True" CssClass="pagination-ys"
                                Wrap="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />

            </div>
        </div>

    </form>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>
