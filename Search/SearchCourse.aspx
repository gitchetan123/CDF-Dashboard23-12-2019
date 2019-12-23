<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="SearchCourse.aspx.cs" Inherits="Search_SearchCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <style type="text/css">
        .row {
            padding: 5px;
        }
        .panel {
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
            text-align: center;
            max-width: 550px;
            padding: 5px;
            margin: 0 auto;
        }
    </style>--%>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>

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
    <form id="Form1" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>Search Course Information</h2>
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
                <%--<div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8 text-info">
                <h3>
                    Search Course Information
                </h3>
            </div>
            <div class="col-sm-2">
            </div>
        </div>--%>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="" id="div_msg" runat="server" style="text-align: center; margin-left: 70px; margin-right: 70px;">
                            </div>
                            <label for="name" class="col-md-3 col-md-offset-1 control-label" style="text-align: right; padding-top: 5px;">
                                Course</label>
                            <div class="col-md-5">
                                <asp:DropDownList ID="drop_course" runat="server" class="form-control">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drop_course"
                                    Display="Dynamic" ErrorMessage="Please Select Candidate Test Status" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="ln_solid"></div>
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-5 col-md-offset-4">
                                <asp:Button ID="btn_preview" runat="server" class="btn btn-primary col-sm-8 col-sm-offset-2" Text="Preview"
                                    OnClick="btn_preview_Click" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-8">

                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                                    CellPadding="4" DataKeyNames="co_id" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%" ForeColor="#333333" PageSize="20">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Course Title">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" width="50%" height="22">&nbsp;&nbsp;
                                            <asp:HyperLink ID="hlCareername" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                Font-Underline="False" ForeColor="#404040" Font-Bold="true">[hlCoursename]</asp:HyperLink></td>
                                                        <td align="center" width="50%" height="22">&nbsp;&nbsp;
                                            <asp:Label ID="lblCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                Font-Underline="False" ForeColor="#404040" Font-Bold="true">[lblCategory]</asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" width="50%" height="22">Course Title</td>
                                                        <td align="center" width="50%" height="22">Course Category</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="co_id" HeaderText="co_id" SortExpression="co_id" />
                                        <asp:BoundField DataField="course_name" HeaderText="Course Title" SortExpression="course_name" />
                                        <asp:BoundField DataField="field_of_work" HeaderText="Course Category" SortExpression="field_of_work" />
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
