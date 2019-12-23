<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="executive-report.aspx.cs" Inherits="Admin_executive_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Executive Report</h2>
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
                <div id="div_Error" runat="server" class="alert alert-warning alert-dismissable" style="text-align: center; margin: 8px;">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>

                </div>


                <%-- <div class="row ">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Filter by Executive Status :</label>
                    <div class="col-sm-4">
                        <asp:DropDownList class="form-control" ID="drop_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_status_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>ACTIVE</asp:ListItem>
                            <asp:ListItem>DEACTIVE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_status" runat="server" ErrorMessage="select Status" ControlToValidate="drop_status"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-md-5 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">Filter by Executive Status :</span>
                            <asp:DropDownList class="form-control" ID="drop_status" runat="server">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>ACTIVE</asp:ListItem>
                                <asp:ListItem>DEACTIVE</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">

                            <span class="leftalign">Executive Name                                                          
                                
                            </span>
                            <asp:TextBox ID="txtExeName" placeholder="Executive Name" class="form-control" runat="server"></asp:TextBox>
                            <%-- <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                ValidChars="/" TargetControlID="txt_to" />--%>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-5 col-md-offset-1">
                        <div class="form-group">
                            <span class="leftalign">From                                                               
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from"
                                     ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                     ErrorMessage="Invalid date format.">*</asp:RegularExpressionValidator></span>
                            <asp:TextBox ID="txt_from" placeholder="(DD/MM/YYYY)" class="form-control" runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                ValidChars="/" TargetControlID="txt_from" />
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <span class="leftalign">To                                                              
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to"
                                    ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                    ErrorMessage="Invalid date format.">*</asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox ID="txt_to" placeholder="(DD/MM/YYYY)" class="form-control" runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                ValidChars="/" TargetControlID="txt_to" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row form-group ">
                    <div class=" col-sm-offset-2 col-sm-2">
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_preview" runat="server" CssClass="btn btn-primary btn-block btn1"
                            Text="Preview" OnClick="btn_preview_Click" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_clear" Text="Clear" runat="server" CssClass="btn btn-primary btn-block btn1"
                            OnClick="btn_clear_Click" />
                    </div>
                    <div class=" col-sm-2">
                        <%--<a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="click here to make filter" tabindex="5" data-target="#myModal_fields">Export to Excel</a>--%>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <asp:Label ID="lbl_rowcount" class="control-label col-md-8 " runat="server"></asp:Label>
                </div>
                <div class="row">
                    <%--<div class="col-lg-6 col-lg-offset-1">--%>
                    <%----%>
                    <%-- <asp:UpdatePanel ID="UpdateGird" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drop_status" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>--%>
                    <asp:GridView ID="gvExeReport" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        OnPageIndexChanging="gvExeReport_PageIndexChanging"
                        PageSize="15">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="exeName" HeaderText="Executive Name" SortExpression="exeName">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Final_CDF" HeaderText="Final CDF" SortExpression="Final_CDF">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TodaysTestSent" HeaderText="Today's Test Sent" SortExpression="TodaysTestSent">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalTestSent" HeaderText="Total Test Sent" SortExpression="TotalTestSent">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TestComplete" HeaderText="Test Complete" SortExpression="TestComplete">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TestCompletePercent" HeaderText="Test Complete (%)" SortExpression="TestCompletePercent">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PaymentComplete" HeaderText="Payment Complete" SortExpression="PaymentComplete">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PaymentCompletePercent" HeaderText="Payment Complete (%)" SortExpression="PaymentCompletePercent">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Reg_Complete" HeaderText="Test Registration Complete" SortExpression="Reg_Complete">
                                <HeaderStyle Wrap="true" />
                                <ItemStyle HorizontalAlign="Justify" />
                            </asp:BoundField>
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
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </form>

    <!-- Custom Theme Scripts -->
    <script src="../js/custom.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txt_from").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

            $("#ctl00_ContentPlaceHolder1_txt_to").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

            $("#ctl00_ContentPlaceHolder1_txt_DOB").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm",
                yearRange: "-00:+00"
            });

        });
    </script>
</asp:Content>

