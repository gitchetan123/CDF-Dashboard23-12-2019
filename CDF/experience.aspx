<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="experience.aspx.cs" Inherits="CDF_Experience_" Culture="en-GB"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        .filterspace {
            padding-left: 20px;
            padding-right: 20px;
        }
    </style>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

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
    <form id="loginform" class="form-horizontal" role="form" runat="server">
         <asp:ScriptManager runat="server" ID="sm">
                </asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Experience</h2>
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
                    <div class="form-group " style="padding-top: 20px;">
                        <label class="col-md-3 col-md-offset-1  control-label">
                            Company Name :</label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ID="txt_company" runat="server" placeholder="Company Name" MaxLength="50"></asp:TextBox>
                             <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_company" />
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="rfv_company" runat="server" ErrorMessage="*" ControlToValidate="txt_company" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label class="col-md-3 col-md-offset-1  control-label">
                            Position  :
                        </label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ID="txt_position" runat="server" placeholder="Position" MaxLength="50"></asp:TextBox>
                             <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_position" />
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="rfv_position" runat="server" ErrorMessage="*"
                                ControlToValidate="txt_position" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group ">
                        <label class="col-md-3 col-md-offset-1  control-label">
                            Job Start Date :</label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox ID="txt_jst" class="form-control" runat="server" placeholder="Job Start Date"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:CompareValidator ID="dateValidator" runat="server"
                                Type="Date" Operator="DataTypeCheck" ControlToValidate="txt_jst"
                                ErrorMessage="Invalid date." ValidationGroup="a"></asp:CompareValidator>
                        </div>
                    </div>

                    <div class="form-group ">
                        <label class="col-md-3 col-sm-offset-1  control-label">
                            Job End Date :</label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ID="txt_jet" runat="server" placeholder="Job End Date"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:CompareValidator ID="CompareValidator1" runat="server"
                                Type="Date" Operator="DataTypeCheck" ControlToValidate="txt_jet"
                                ErrorMessage="Invalid date." ValidationGroup="a"></asp:CompareValidator>
                        </div>
                    </div>

                    <div class="form-group ">
                        <label class="col-md-3 col-sm-offset-1  control-label">
                            Location :</label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ID="txt_location" runat="server" placeholder="Job Location" MaxLength="50"></asp:TextBox>
                             <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_location" />
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="rfv_location" runat="server" ErrorMessage="*"
                                ControlToValidate="txt_location" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group ">
                        <label class="col-md-3 col-sm-offset-1  control-label">
                            Description :</label>
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ID="txt_des" runat="server" TextMode="MultiLine" placeholder="Description"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_des" />
                        </div>
                    </div>
                    <div class="ln_solid"></div>
                    <div class="form-group ">

                        <div class=" col-md-offset-2 col-md-2 col-sm-12 col-xs-12">
                            <asp:Button ID="btn_save" class="btn btn-primary btn-block"
                                runat="server" Text="Save" OnClick="btn_save_Click" ValidationGroup="a" />
                        </div>
                        <div class=" col-md-2 col-sm-12 col-xs-12">
                            <asp:Button ID="btn_update" class="btn btn-primary btn-block"
                                runat="server" Text="Update" OnClick="btn_update_Click" ValidationGroup="a" />
                        </div>
                        <div class=" col-md-2 col-sm-12 col-xs-12">
                            <asp:Button ID="btn_delete" class="btn btn-primary btn-block"
                                runat="server" Text="Delete" OnClick="btn_delete_Click" />
                        </div>
                        <div class=" col-md-2 col-sm-12 col-xs-12">
                            <asp:Button ID="btn_clear" class="btn btn-primary btn-block"
                                runat="server" Text="Clear" OnClick="btn_clear_Click" />
                        </div>
                    </div>
                </div>

                <div style="margin-top: 30px;">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grid_exp" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" GridLines="None"
                            DataKeyNames="id" DataSourceID="SqlDataSource_Edu" CssClass="table" PageSize="15"
                            OnSelectedIndexChanged="grid_exp_SelectedIndexChanged" Width="100%" HeaderStyle-BackColor="#337ab7">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Select" ControlStyle-ForeColor="#337AB7">

                                    <ControlStyle ForeColor="#337AB7"></ControlStyle>
                                </asp:CommandField>

                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False"
                                    ReadOnly="True" SortExpression="id" Visible="False" />
                                <asp:BoundField DataField="cdf_company" HeaderText="Company Name"
                                    SortExpression="cdf_company" />
                                <asp:BoundField DataField="cdf_position" HeaderText="Position"
                                    SortExpression="cdf_position" />
                                <asp:BoundField DataField="cdf_job_start_date" HeaderText="Job Start Date"
                                    SortExpression="cdf_job_start_date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="cdf_job_end_date" HeaderText="Job End Date"
                                    SortExpression="cdf_job_end_date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="cdf_location" HeaderText="Location"
                                    SortExpression="cdf_location" />
                                <asp:BoundField DataField="cdf_position_discription"
                                    HeaderText="Position Description"
                                    SortExpression="cdf_position_discription" />
                                <asp:BoundField DataField="cdf_master_id" HeaderText="cdf_master_id"
                                    SortExpression="cdf_master_id" Visible="False" />
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" Wrap="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource_Edu" runat="server"
                            ConnectionString="<%$ ConnectionStrings:DBConnection %>"
                            SelectCommand="SELECT * FROM [tblExperience] WHERE ([uId] = @uId)">
                            <SelectParameters>
                                <asp:SessionParameter Name="uId" SessionField="uid" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:HiddenField ID="hf_id" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    

          <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txt_jst").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });
            $("#ctl00_ContentPlaceHolder1_txt_jet").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });
        });
    </script>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

