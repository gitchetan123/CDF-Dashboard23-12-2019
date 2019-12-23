<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="news-feed.aspx.cs" Inherits="Admin_news_feed" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>News Feed</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Title :
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txt_title" runat="server" placeholder="News Title"
                            MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_title" runat="server" ErrorMessage="Title is required" ControlToValidate="txt_title"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>                        
                    </div>
                </div>
                <div class="row">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Description :</label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txt_description" TextMode="MultiLine" runat="server" placeholder="Description"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_description" runat="server" ErrorMessage="Description is required" ControlToValidate="txt_description"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="rfv_status" runat="server" ErrorMessage="Status is required" ControlToValidate="drop_status"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px">
                    <div class=" col-sm-offset-3 col-sm-2">
                        <asp:Button ID="btn_save" class="btn btn-primary btn-block" runat="server" Text="Create"
                            OnClick="btn_save_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="Update"
                            OnClick="btn_update_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="Clear"
                            OnClick="btn_clear_Click" />
                    </div>
                </div>

                <div align="center" style="margin-top:20px;">
                    <asp:GridView ID="grid_exe" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnSelectedIndexChanged="grid_exe_SelectedIndexChanged" Width="80%" CssClass="table table-responsive"
                        OnPageIndexChanging="grid_exe_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                        AllowPaging="True" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Update" HeaderText="Update Here" />
                           <%-- <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                SortExpression="id" Visible="False" />--%>
                             <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id">
                            <HeaderStyle Font-Size="0pt" />
                            <ItemStyle Font-Size="0pt" />
                            </asp:BoundField>
                            <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
                            <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                            <asp:BoundField DataField="dateCreated" HeaderText="Entered Date" SortExpression="dateCreated"
                                     DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
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

