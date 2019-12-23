<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="education.aspx.cs" Inherits="CDF_CDF_Edu" %>
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
        <div class="x_panel">
            <div class="x_title">
                <h2>CDF Education</h2>
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
                <asp:ScriptManager runat="server" ID="sm">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="form-group " style="padding-top: 20px;">
                                <label class="col-md-3 col-sm-offset-1  control-label">
                                    College Name :</label>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <asp:TextBox class="form-control" ID="txt_college" runat="server"
                                        placeholder="College Name" MaxLength="100"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_college" />
                                </div>

                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_college" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="form-group ">
                                <label class="col-md-3 col-md-offset-1  control-label">
                                    Degree Name :
                                </label>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <asp:TextBox class="form-control" ID="txt_degree" runat="server" placeholder="Degree" MaxLength="50"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_degree" />
                                </div>
                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="rfv_degree" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_degree" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label class="col-md-3 col-md-offset-1 control-label">
                                    Description :</label>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <asp:TextBox class="form-control" ID="txt_des" runat="server" placeholder="Description"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_des" />
                                </div>
                            </div>

                            <div class="form-group ">
                                <label class="col-md-3 col-md-offset-1  control-label">
                                    Grade :</label>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <asp:TextBox class="form-control" ID="txt_grade" runat="server" placeholder="Grade" MaxLength="50"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;()#: " TargetControlID="txt_grade" />
                                </div>
                            </div>

                            <div class="ln_solid"></div>
                            <div class="form-group">

                                <div class="col-md-offset-2 col-sm-12 col-xs-12 col-md-2">
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
                        <div class="row" style="margin-top: 30px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:GridView ID="grid_edu" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" GridLines="None"
                                    DataKeyNames="id" DataSourceID="SqlDataSource_Edu" CssClass="table" PageSize="15"
                                    OnSelectedIndexChanged="grid_edu_SelectedIndexChanged" Width="100%" HeaderStyle-BackColor="#337ab7">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" HeaderText="Select" ControlStyle-ForeColor="#337AB7">
                                            <ControlStyle ForeColor="#337AB7" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id" Visible="False" />
                                        <asp:BoundField DataField="cdf_college" HeaderText="College Name"
                                            SortExpression="cdf_college" />
                                        <asp:BoundField DataField="cdf_degree" HeaderText="Degree Name"
                                            SortExpression="cdf_degree" />
                                        <asp:BoundField DataField="cdf_description" HeaderText="Description"
                                            SortExpression="cdf_description" />
                                        <asp:BoundField DataField="cdf_grade" HeaderText="Grade / Marks"
                                            SortExpression="cdf_grade" />
                                        <asp:BoundField DataField="cdf_master_id" HeaderText="id"
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
                                    SelectCommand="SELECT * FROM [tblEducation] WHERE ([uId] = @uId)">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="uId" SessionField="uid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:HiddenField ID="hf_id" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>

</asp:Content>

