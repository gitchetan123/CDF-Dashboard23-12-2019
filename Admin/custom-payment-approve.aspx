<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="custom-payment-approve.aspx.cs" Inherits="customPaymentApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Approve Custom Payment</h2>
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
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_email" placeholder="First Name or Last Name or Email or Created By" class="form-control"
                                runat="server"></asp:TextBox>
                          
                        </div>
                        <div class="col-sm-1">
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Dynamic"
                                        ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                        ForeColor="Red" ValidationGroup="cdf">*</asp:RequiredFieldValidator>--%>
                                   </div>
                    </div>
                    

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_search" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Search" ValidationGroup="cdf" OnClick="btn_search_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="grid_Pyment" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="id" AllowPaging="True" CssClass="table"
                                            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grid_Pyment_RowCommand" OnPageIndexChanging="grid_Pyment_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField DataField="uId" HeaderText="ID" InsertVisible="False"
                                                    ReadOnly="True" SortExpression="uId" />
                                                <asp:BoundField DataField="fname" HeaderText="First Name" SortExpression="fname" />
                                                <asp:BoundField DataField="lname" HeaderText="Last Name" SortExpression="lname" />
                                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                                <asp:BoundField DataField="approve" HeaderText="Approve" SortExpression="approve" />
                                                <asp:BoundField DataField="createdBy" HeaderText="Created By" SortExpression="createdBy" />
                                                <asp:BoundField DataField="createdDate" HeaderText="Created Date" SortExpression="createdDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <%-- <asp:BoundField DataField="updatedBy" HeaderText="Updated" SortExpression="updatedBy" />
                                        <asp:BoundField DataField="modifiedDate" HeaderText="Modified Date" SortExpression="modifiedDate" />--%>
                                                <asp:TemplateField HeaderText="Active/Deactive">
                                                    <ItemTemplate>
                                                        <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>--%>
                                                        <asp:Button ID="Button2" CssClass="btn btn-success btn-sm" Visible='<%#  (Eval("approve").ToString()) == "Pending" %>'
                                                            runat="server" Text="Approve" OnClientClick="if ( ! UserDeactive()) return false;"
                                                            CommandArgument='<%# Eval("id") + "," +  Eval("uid")%>' CommandName="approve" />
                                                        <asp:Label ID="lbl_approve" CssClass="" Visible='<%#  (Eval("approve").ToString()) == "Approved" %>'
                                                             runat="server" Text="Approved"></asp:Label>
                                                        <%--   </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hf_id" runat="server" />

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

