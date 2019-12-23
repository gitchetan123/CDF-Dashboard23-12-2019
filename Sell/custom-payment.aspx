<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="custom-payment.aspx.cs" Inherits="Sale_custom_payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="loginform" class="form-horizontal" role="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Send Payment Link<small><strong><i class="fa fa-user"></i> <asp:Label ID="lbl_name" runat="server"></asp:Label></strong></small></h2>
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

                    
                    <div class="row form-group">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Product Price :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_amount" placeholder="" class="form-control" Text="" ReadOnly="true"
                                runat="server"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                TargetControlID="txt_amount" ValidChars=". " />
                        </div>
                        <div class="col-sm-1">
                            <%--<asp:RequiredFieldValidator ID="rvAmount" runat="server" ErrorMessage="Please Enter Amount" ControlToValidate="txt_amount" ValidationGroup="payment">*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                   

                     <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Any discount offers :*
                        </label>
                        <div class="col-sm-6">
                            <asp:RadioButton ID="rblfull" runat="server" Text=" Full Fees" GroupName="discount" OnCheckedChanged="rblfull_CheckedChanged" AutoPostBack="true" /> &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl5" runat="server" Text="  5 %" GroupName="discount" OnCheckedChanged="rbl5_CheckedChanged" AutoPostBack="true" /> &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl10" runat="server" Text="  10 %" GroupName="discount" OnCheckedChanged="rbl10_CheckedChanged" AutoPostBack="true"/> &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl15" runat="server" Text="  15 %" GroupName="discount" OnCheckedChanged="rbl15_CheckedChanged" AutoPostBack="true"/> &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl20" runat="server" Text="  20 %" GroupName="discount" OnCheckedChanged="rbl20_CheckedChanged" AutoPostBack="true"/>
                        </div>
                        <div class="col-sm-1">

                        </div>
                    </div>

                    

                   <%-- <div class="row form-group ">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Created By :
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_createdBy" placeholder="Enter your name" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                       
                        </div>
                        <div class="col-sm-1">
                             <asp:RequiredFieldValidator ID="rvCreateBy" runat="server" ErrorMessage="Please Enter your Name" ControlToValidate="txt_createdBy" ValidationGroup="payment">*</asp:RequiredFieldValidator>
                        </div>
                    </div>--%>

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-2 col-sm-2">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_payment" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Create" OnClick="btn_payment_Click" ValidationGroup="payment" />
                        </div>
                    </div>
                </div>
                <div>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grid_Pyment" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="id" AllowPaging="True" CssClass="table" PageSize="50"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grid_Pyment_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="uId" HeaderText="ID" InsertVisible="False"
                                            ReadOnly="True" SortExpression="uId" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                         <asp:BoundField DataField="approve" HeaderText="Approve" SortExpression="approve" />
                                        <asp:BoundField DataField="Created_By" HeaderText="Created By" SortExpression="Created_By" />
                                        <asp:BoundField DataField="Created_Date" HeaderText="Created Date" SortExpression="Created_Date" DataFormatString="{0:dd/MM/yyyy}" />
                                       <%-- <asp:BoundField DataField="updatedBy" HeaderText="Updated" SortExpression="updatedBy" />
                                        <asp:BoundField DataField="modifiedDate" HeaderText="Modified Date" SortExpression="modifiedDate" />--%>
                                        <asp:TemplateField HeaderText="Payment Active/Deactive">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="Button2" CssClass="btn btn-danger btn-sm" Visible='<%#  (Eval("status").ToString()) == "ACTIVE" %>'
                                                            runat="server" Text="Deactive" OnClientClick="if ( ! UserDeactive()) return false;"
                                                            CommandArgument='<%# Eval("id")%>'  CommandName="CustPayment" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" Wrap="True" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
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
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
</asp:Content>

