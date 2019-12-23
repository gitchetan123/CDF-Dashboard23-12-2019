<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="google-business-listing.aspx.cs" Inherits="Admin_google_business_listing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 1px;
        }

        .filterspace {
            padding-left: 20px;
            padding-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Google Business Listing</h2>
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
                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_name" placeholder="First Name or Last Name or Email or Contact No. or City or Address" class="form-control"
                                runat="server"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_storeCode" placeholder="Store Code" class="form-control"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By GBL Status :</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddl_GBLStatus" CssClass="form-control" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Published</asp:ListItem>
                                <asp:ListItem Value="2">Pending</asp:ListItem>
                                <asp:ListItem Value="3">Not Done</asp:ListItem>
                                <asp:ListItem Value="4">Not Interested</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-2">
                            <asp:Button ID="btn_preview" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Preview" OnClick="btn_preview_Click" OnClientClick="return preview()" />
                        </div>
                        <div class=" col-sm-2">
                            <asp:Button ID="btn_reset" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Reset" OnClick="btn_Reset_Click" OnClientClick="return reset()" />
                        </div>

                    </div>

                </div>

                <div style="height: 20px; width: 100%">
                    <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <%--<a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="click here to make filter" tabindex="5" data-target="#myModal">Advance Filter</a>--%>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        OnDataBound="GridView1_DataBound" Width="100%" DataKeyNames="id"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15"
                        AllowSorting="True" OnSorting="GridView1_Sorting" OnRowCommand="GridView1_RowCommand">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="View GBL Detail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnView" runat="server" CommandName="View" CommandArgument='<%# Eval("id") %>'  OnCommand="return view()">View</asp:LinkButton>--%>
                                    <asp:Button ID="lnView" runat="server" Text="View" BackColor="White" BorderWidth="0px" CommandName="View" CommandArgument='<%# Eval("id") %>' OnClientClick="return view()" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="false" />

                            <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" Visible="False" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                            <asp:BoundField DataField="contactno" HeaderText="Contact" SortExpression="contactno" />
                            <asp:BoundField DataField="dheyaEmail" HeaderText="Dheya Email" SortExpression="dheyaEmail" Visible="false" />
                            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                            <asp:BoundField DataField="regDateTime" HeaderText="Registration Date" SortExpression="regDateTime" Visible="false"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GBL" HeaderText="Google Business" SortExpression="GBL" Visible="false">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="GBL Status" SortExpression="status">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="store_code" HeaderText="Store Code" SortExpression="store_code">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
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
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Google Business Listing</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_GBLStatus1" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-md-5 col-md-offset-1" style="margin-top: 20px">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chk_GBL" Text="Gooble Business Listing" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-5 col-md-offset-1">
                                        <div class="form-group">
                                            <span class="leftalign">Store Code:</span>
                                            <asp:TextBox ID="txt_StoreCode1" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="rfv_StoreCode" runat="server" ErrorMessage=""
                                            ControlToValidate="txt_StoreCode1"></asp:RequiredFieldValidator>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">GBL Status</span>
                                    <asp:DropDownList ID="ddl_GBLStatus1" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_GBLStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Published</asp:ListItem>
                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                        <asp:ListItem Value="3">Not Done</asp:ListItem> 
                                        <asp:ListItem Value="4">Not Interested</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Comments</span>
                                    <asp:TextBox ID="text_comment" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <asp:Button ID="Save" class="btn btn-primary btn-block" runat="server" Text="Save" OnClientClick="return Save()" OnClick="btn_saveBusinessList_Click" />
                                </div>
                            </div>
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        function preview() { }
        function view() { }
        function reset() { }
        function Save() { }
    </script>
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

