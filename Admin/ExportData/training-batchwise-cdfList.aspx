﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true" CodeFile="training-batchwise-cdfList.aspx.cs" Inherits="Admin_training_batchwise_cdfList" %>

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


        <!-- CDF Training Batch Details -->
        <div class="x_panel">
            <div class="x_title">
                <h2>Training Batchwise CDF Details</h2>
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
                        <div class="alert " id="div_msg" visible="false" runat="server" style="text-align: center; max-width: 80%;">
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">
                                        <asp:CheckBox ID="chk_all" runat="server" Text="" /></span>
                                    <span class="leftalign">Require all Details</span>
                                    <span class="leftalign">[CDF - Level1 Details - Level 2 Details - Level 3 Details]</span>
                                </div>
                                <br />
                            </div>
                        </div>
                        <br />

                        <div class="col-md-5 col-md-offset-1">
                            <div class="form-group">
                                <span class="leftalign">CDF Level:*</span>
                                <asp:DropDownList ID="ddlLevel" runat="server" class="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <span class="leftalign">CDF Batch</span>
                                <asp:UpdatePanel ID="UpdatePanelBatch" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlLevel" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlBatchName" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group" style="margin-left:-161px">
                                    <asp:Button ID="btn_preview" runat="server" class="btn btn-primary " Style="margin-bottom: 20px;"
                                        Text="Preview" OnClick="btn_preview_click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="col-md-2 col-md-offset-3">
                            <div class="">
                                <asp:Button ID="btnExport" Visible="false" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    Text="Export To PDF" OnClick="ExportToPDF" />
                                <asp:Button ID="btnExportToExcel" Visible="false" runat="server" class="btn btn-primary btn-block " Style="margin-bottom: 20px;"
                                    Text="Export To Excel" OnClick="ExportToExcel" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="div_batch" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group" style="text-align: left">
                                    <span class="leftalign">Batch Name &nbsp: </span>&nbsp
                                    <asp:Label runat="server" ID="lblBatchName"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group" style="text-align: left">
                                    <span class="leftalign">Batch Location &nbsp: </span>&nbsp
                                    <asp:Label runat="server" ID="lblBatchLoc"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group" style="text-align: left">
                                    <span class="leftalign">Date &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp: </span>&nbsp
                                    <asp:Label runat="server" ID="lblBatchdate"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group" style="text-align: left">
                                    <span class="leftalign">Trainer &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp: </span>&nbsp
                                    <asp:Label runat="server" ID="lblTrainerName"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <div style="height: 20px; width: 100%">
                            <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" CellPadding="4" ForeColor="#333333" GridLines="None"
                            CssClass="table table-responsive" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="40"
                            runat="server" AutoGenerateColumns="false" AllowPaging="true">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="uId" HeaderText="uId" ItemStyle-Width="150px" Visible="false" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="email" HeaderText="Email Id" />
                                <asp:BoundField DataField="dheyaEmail" HeaderText="Dheya Email Id" />
                                <asp:BoundField DataField="contactNo" HeaderText="Contact No" />
                                <asp:BoundField DataField="city" HeaderText="City" />
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
        </div>

        <!-- Custom Theme Scripts -->
        <script src="../../js/custom.min.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <%--<script>
            $(function () {
                $("#ctl00_ContentPlaceHolder1_tbDate1").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "dd/mm/yy",
                    yearRange: "-90:+01"
                });
            });
        </script>--%>
    </form>
</asp:Content>

