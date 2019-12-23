<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="training-batch.aspx.cs" Inherits="Admin_training_batch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        .leftalign {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Training batch</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a></li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div class="container" align="center">
                    <div class="row">
                        <div class="row">
                            <div class="alert " id="div_msg" runat="server" style="text-align: center; max-width: 80%;">
                            </div>
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Batch Name:*
                                    <asp:RequiredFieldValidator ID="rfvBatchName" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_city" runat="server" ErrorMessage="Batch name is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                    <asp:TextBox class="form-control" ID="txt_btachName" runat="server" placeholder="Batch Name"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters,Numbers,Custom"
                                        ValidChars="-" TargetControlID="txt_btachName" />
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Training Date: (dd/mm/yyyy)*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" Display="Static"
                                        ControlToValidate="tbDate1" runat="server" ErrorMessage="Training date is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Static"
                                            ErrorMessage="Invalid Date." ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
                                            ControlToValidate="tbDate1" ForeColor="Red">*</asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox class="form-control" ID="tbDate1" runat="server" placeholder="Enter Training Date"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="/" TargetControlID="tbDate1" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">State:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_state" runat="server" ErrorMessage="State is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddl_state" runat="server" class="form-control"
                                        OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5 ">
                                <div class="form-group">
                                    <span class="leftalign">City:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_city" runat="server" ErrorMessage="City is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_city" runat="server" class="form-control"
                                                AutoPostBack="false">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_state" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Location:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_location" runat="server" ErrorMessage="Location is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                    <asp:TextBox ID="txt_location" runat="server" placeholder="Location" class="form-first-name form-control"
                                        MaxLength="200"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                        TargetControlID="txt_location" ValidChars=".,;() &quot;#:&" />
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Details:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_details" runat="server" ErrorMessage="Details is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txt_details" runat="server" placeholder="Details" class="form-first-name form-control"
                                        MaxLength="200"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                        ValidChars=".,;() &quot;#:&" TargetControlID="txt_details" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Trainer Name:</span>
                                    <asp:TextBox ID="txt_TrainerName" runat="server" placeholder="Teacher Name"
                                        MaxLength="50" class="form-first-name form-control"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                        TargetControlID="txt_TrainerName" ValidChars="., " />
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">CDF Count:</span>
                                    <asp:TextBox ID="txt_count" runat="server" placeholder="Total CDF"
                                        class="form-first-name form-control" MaxLength="3"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers"
                                        TargetControlID="txt_count" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">CDF Level:</span> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_cdfLevel" runat="server" ErrorMessage="CDF Level required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddl_cdfLevel"  CssClass="form-control" runat="server" InitialValue="--Select--"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 20px">
                        <div class="form-group">
                            <div class="col-md-2 col-md-offset-3 ">
                                <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    Text="Submit" OnClick="btn_submit_Click" />
                            </div>
                            <div class=" col-sm-2">
                                <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="Update" OnClick="btn_update_Click" />
                            </div>

                            <div class="col-md-2">
                                <asp:Button ID="btn_clear" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    ValidationGroup="clear" Text="Clear" OnClick="btn_clear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                            Width="100%" DataKeyNames="id"
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15"
                            AllowSorting="True" OnSorting="GridView1_Sorting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" />

                                <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" Visible="False" />

                                <asp:BoundField DataField="batchName" HeaderText="Batch Name" SortExpression="batchName">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="city" HeaderText="City" SortExpression="city">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>

                                <asp:BoundField DataField="location" HeaderText="location" SortExpression="location" />

                                <asp:BoundField DataField="date" HeaderText="Training Date" SortExpression="date"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Wrap="False" />

                                </asp:BoundField>
                                <asp:BoundField DataField="trainerName" HeaderText="Trainer Name" SortExpression="trainerName" />
                                <asp:BoundField DataField="cdfcount" HeaderText="Count" SortExpression="cdfcount" />

                                <asp:BoundField DataField="cityId" HeaderText="cityId" SortExpression="cityId">
                                    <ControlStyle Font-Size="0pt" />
                                    <HeaderStyle Wrap="False" Font-Size="0pt" />
                                    <ItemStyle Font-Size="0pt" />
                                </asp:BoundField>

                                <asp:BoundField DataField="stateId" HeaderText="stateId" SortExpression="stateId">
                                    <ControlStyle Font-Size="0pt" />
                                    <HeaderStyle Wrap="False" Font-Size="0pt" />
                                    <ItemStyle Font-Size="0pt" />
                                </asp:BoundField>

                                <asp:BoundField DataField="details" HeaderText="details" SortExpression="details">
                                    <ControlStyle Font-Names="0" />
                                    <HeaderStyle Wrap="False" Font-Size="0pt" />
                                    <ItemStyle Font-Size="0pt" />
                                </asp:BoundField>

                                <asp:BoundField DataField="cdflevel" HeaderText="CDF Level" SortExpression="cdflevel" Visible="true" />

                            </Columns>
                            <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:HiddenField ID="hf_id" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <!-- Custom Theme Scripts -->
        <script src="../js/custom.min.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script>
            $(function () {
                $("#ctl00_ContentPlaceHolder1_tbDate1").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "dd/mm/yy",
                    yearRange: "-90:+01"
                });
            });
        </script>
    </form>
</asp:Content>

