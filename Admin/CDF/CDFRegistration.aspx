<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="CDFRegistration.aspx.cs" Inherits="CDF_CDFRegistration" %>

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
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <h2>New CDF Registration</h2>
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
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                <div class="row" style="margin-top: 10px;">
                    <label class="col-md-2 col-md-offset-2  control-label">&nbsp;Dheya Email Id :</label>
                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txt_email" runat="server"
                            placeholder="Email ID" MaxLength="100"></asp:TextBox>
                    </div>
                   <%-- <div class="col-md-3">
                        <asp:Button ID="btn_search" class="btn btn-primary btn-block" runat="server"
                            Text="Search" OnClick="btn_search_Click" />
                    </div>--%>
                    <div class="col-md-1">
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                    meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red" ValidationGroup="a">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="Mandatory to fill all the field" Display="Dynamic"
                            ControlToValidate="txt_email" ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-2 col-md-offset-2  control-label">
                        Personal Email Id :
                    </label>
                    <div class="col-md-4">
                       <asp:TextBox class="form-control" ID="txt_personalEmail" runat="server"
                            placeholder="Personal Email ID" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_personalEmail"
                                    meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic" ForeColor="Red" ValidationGroup="a">*</asp:RegularExpressionValidator>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Mandatory to fill  Personal Email Id" Display="Dynamic"
                            ControlToValidate="txt_personalEmail" ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-2 col-md-offset-2  control-label">
                        Level :
                    </label>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="drop_level" runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="rfv_level" runat="server" ErrorMessage="Mandatory to fill Level" InitialValue="--Select--"
                            ControlToValidate="drop_level" ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-2 col-md-offset-2  control-label">
                        Status :</label>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="drop_status" runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>ACTIVE</asp:ListItem>
                            <asp:ListItem>DEACTIVE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="rfv_status" runat="server" ErrorMessage="Mandatory to fill Status" InitialValue="--Select--"
                            ControlToValidate="drop_status" ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" ValidationGroup="a" />
                   <div class="ln_solid"></div>
                <div class="row">
                    <div class=" col-md-offset-3 col-md-3">
                        <asp:Button ID="btn_save" class="btn btn-primary btn-block"
                            runat="server" Text="Create" OnClick="btn_save_Click"
                            ValidationGroup="a" />
                    </div>
                    <div class=" col-md-3">
                       <asp:Button ID="btn_clear" class="btn btn-primary btn-block"
                            runat="server" Text="Clear" OnClick="btn_clear_Click" />
                        
                    </div>
                   <%-- <div class=" col-md-3">
                         <asp:Button ID="btn_update" class="btn btn-primary btn-block"
                            runat="server" Text="Update" OnClick="btn_update_Click" ValidationGroup="a" />
                    </div>--%>
                </div>
                <div>
                    <div style="height: 20px; width: 100%;">
                        <asp:Label ID="lbl_rowcount" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grid_edu" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="uId" AllowPaging="True" CssClass="table" PageSize="50"
                                OnSelectedIndexChanged="grid_edu_SelectedIndexChanged" OnPageIndexChanging="grid_edu_PageIndexChanging" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="uId" HeaderText="ID" InsertVisible="False"
                                        ReadOnly="True" SortExpression="uId" />
                                    <asp:BoundField DataField="fname" HeaderText="First Name"
                                        SortExpression="fname" />
                                    <asp:BoundField DataField="lname" HeaderText="Last Name"
                                        SortExpression="lname" />
                                    <asp:BoundField DataField="dheyaEmail" HeaderText="Dheya Email ID"
                                        SortExpression="dheyaEmail" />
                                    <asp:BoundField DataField="cdfLevel" HeaderText="Level"
                                        SortExpression="cdfLevel" />
                                    <asp:BoundField DataField="userStatus" HeaderText="Status"
                                        SortExpression="userStatus" />
                                </Columns>
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />

                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            </asp:GridView>
                            <br />

                            <asp:HiddenField ID="hf_id" runat="server" />
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../../js/custom.min.js"></script>
</asp:Content>

