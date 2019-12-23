<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="find-nearby-cdf.aspx.cs" Inherits="Admin_find_nearby_cdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Find Near By CDF</h2>
                <ul class="nav navbar-right panel_toolbox">
                     <li class="dropdown">
                        <a href="executive-report.aspx"  role="button" aria-expanded="false"><i class="fa fa-bar-chart"></i></a>
                        
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
                        Latitude :
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txtLatitude" runat="server" placeholder="Latitude"
                            MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_level" runat="server" ErrorMessage="Latitude" ControlToValidate="txtLatitude"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                       
                    </div>
                </div>
                <div class="row">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Longitude :</label>
                    <div class="col-sm-4">
                        <asp:TextBox class="form-control" ID="txtLongitude" runat="server" placeholder="Longitude"
                            MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="Enter Longitude" ControlToValidate="txtLongitude"
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                <div class="row" style="margin-top: 20px">
                    <div class="col-md-4 col-md-offset-2">
                        <asp:Button ID="btn_preview" class="btn btn-primary btn-block" runat="server" Text="Preview"
                            OnClick="btn_preview_Click" ValidationGroup="a" />
                    </div>                  
                    <div class="col-md-4">
                        <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="clear"
                            OnClick="btn_clear_Click" />
                    </div>
                </div>
                  <div style="height: 20px; width: 100%">
                    <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                </div>
                <div align="center" style="margin-top:20px;">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        OnDataBound="GridView1_DataBound" Width="100%" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" OnRowCommand="GridView1_RowCommand"
                        AllowSorting="True" OnSorting="GridView1_Sorting">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="false" />
                            <asp:TemplateField HeaderText="More Details">
                                <ItemTemplate>
                                    <asp:HyperLink ID="Details" Target="_blank" runat="server" NavigateUrl='<%# Eval("id", "~/Admin/candidate-details.aspx?id={0}") %>'
                                        CssClass="bodytext">More Info...</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" Visible="False" />
                            <asp:BoundField DataField="fname" HeaderText="First Name" SortExpression="fname">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lname" HeaderText="Last Name" SortExpression="lname">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                            <asp:BoundField DataField="regDateTime" HeaderText="Registration Date" SortExpression="regDateTime"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>           
                            <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />  
                             <asp:BoundField DataField="distance" HeaderText="Distance (in KM)" SortExpression="distance" />             
                            <asp:BoundField DataField="userStatus" HeaderText="User Status" SortExpression="userStatus" />
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
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

