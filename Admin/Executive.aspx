<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true"
    CodeFile="executive.aspx.cs" Inherits="requirement_Admin_executive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        .h {
            display: none;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.8.0.min.js"></script>
    <script type="text/javascript">

        $(function () {
            $("[id*=grid_exe]").find("[id*=btnStatus]").click(function () {
                //Reference the GridView Row.
                var row = $(this).closest("tr");

                var Status = row.find("td").eq(4).html();

                //  alert(Status);
                if (Status == 'ACTIVE') {
                    return confirm("Are you sure you want to Deactive this user?");
                }
                if (Status == 'DEACTIVE') {
                    return confirm("Are you sure you want to Active this user?");
                } else {
                    return false;
                }
            });

        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Dheya Executive</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li class="dropdown">
                        <a href="executive-report.aspx" role="button" aria-expanded="false"><i class="fa fa-bar-chart"></i></a>

                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Name :
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txt_name" runat="server" placeholder="Executive Name"
                            MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_level" runat="server" ErrorMessage="Enter Name" ControlToValidate="txt_name"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                            TargetControlID="txt_name" ValidChars=". " />
                    </div>
                </div>
                <div class="row">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Email Id :</label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txt_email" runat="server" placeholder="Email ID"
                            MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="Enter valid Email-ID" ControlToValidate="txt_email"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                            meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ValidationGroup="a">*</asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row ">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Status :</label>
                    <div class="col-sm-4">
                        <asp:DropDownList class="form-control" ID="drop_status" runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>ACTIVE</asp:ListItem>
                            <asp:ListItem>DEACTIVE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_status" runat="server" ErrorMessage="select Status" ControlToValidate="drop_status"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px">
                    <div class=" col-sm-offset-3 col-sm-2">
                        <asp:Button ID="btn_save" class="btn btn-primary btn-block" runat="server" Text="Create"
                            OnClick="btn_save_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="update"
                            OnClick="btn_update_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="clear"
                            OnClick="btn_clear_Click" />
                    </div>
                </div>

                <div align="center" style="margin-top: 20px;">
                    <asp:GridView ID="grid_exe" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnSelectedIndexChanged="grid_exe_SelectedIndexChanged" Width="80%" CssClass="table table-responsive"
                        OnPageIndexChanging="grid_exe_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                        AllowPaging="True" GridLines="None" PageSize="5" OnRowCommand="grid_exe_RowCommand" OnRowDataBound="grid_exe_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select Here" />
                            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                SortExpression="id" Visible="False" />
                            <asp:BoundField DataField="exeName" HeaderText="Executive Name" SortExpression="exeName" />
                            <asp:BoundField DataField="exeEmail" HeaderText="Email ID" SortExpression="exeEmail" />
                            <asp:BoundField DataField="dateOfReg" HeaderText="Registered Date" SortExpression="dateOfReg" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" ItemStyle-CssClass="h" HeaderStyle-CssClass="h" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Button ID="btnStatus" runat="server" Text='<%# Eval("status") %>' CommandArgument='<%# Eval("id")+","+ Eval("status") %>' CommandName="ChkStatus" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:HiddenField ID="hf_id" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>

</asp:Content>
