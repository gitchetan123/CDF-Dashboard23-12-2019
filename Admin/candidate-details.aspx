<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin-master.master" AutoEventWireup="true"
    CodeFile="candidate-details.aspx.cs" Inherits="candidatedetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }

        .auto-style1 {
            height: 32px;
        }

        .colOne {
            style ="width:25%;";
        }

        .colTwo {
            style ="width:75%;";
        }

        html, body {
            height: 100%; /* The html and body elements cannot have any padding or margin. */
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="row">
            <%--User status--%>
            <div class="col-sm-6">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>User Status</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpStatus"><i class="fa fa-question"></i></a>
                            </li>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div align="center">
                            <table class="table table-striped" style="width: 80%; margin-top: 40px">
                                <tr>
                                    <td>
                                        <label>
                                            User activity</label>
                                    </td>
                                    <td>
                                        <label>
                                            Current Status</label>
                                    </td>

                                    <td>
                                        <label>
                                            Change Status</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server">User Status</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_infoUserStatus1" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <a href="#" data-toggle="modal" title="Click here to change status of CDF" class="btn btn-success btn-sm btn-block" runat="server" id="A3" data-target="#myModal_UserStatus">Change Status</a>
                                        <asp:Label ID="btn_status1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr id="status" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="Label1" runat="server">User Status</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_infoUserStatus" runat="server" Text="Label"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Button ID="btn_status" runat="server" Text="Active" OnClick="btn_status_Click" OnClientClick="return User_status_click()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server">Test Reassign</asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btn_testReassing" runat="server" class="btn btn-danger btn-sm btn-block" Text="TEST REASSIGN" OnClick="btn_testReassing_Click" OnClientClick="return TestReassign()" Enabled="False" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server">Test Approval</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_infoApproveStatus" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_approve" runat="server" Text="Approve" OnClientClick="return test()" OnClick="btn_approve_Click" Enabled="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server">Agree Amount</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_agreedAmount" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <a href="#" data-toggle="modal" title="Click here to fix CDF Agree Amount" class="btn btn-success btn-sm btn-block" runat="server" id="A1" data-target="#myModal_agreedAmount">Set Amount</a>
                                        <asp:Label ID="lbl_NDAStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server">Dheya Email</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_dheyaEmailStatus" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <a href="#" data-toggle="modal" title="Set Dheya Email Id" class="btn btn-success btn-sm btn-block" runat="server" id="btn_setDheyaEmail" data-target="#myModal">SET EMAIL</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server">Profile Display Approval</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_profileDisplayApproval" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_profileDisplayApproval" runat="server" Text="Active" OnClick="btn_profileDisplayApproval_Click" OnClientClick="return ProfileDisplayApproval()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server">Profile Update Approval</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_profileUpdateApproval" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_profileUpdateApproval" runat="server" Text="Active" OnClick="btn_profileUpdateApproval_Click" OnClientClick="return ProfileUpdateApproval()" />
                                    </td>
                                </tr>
                                <%-- <tr id="tr_CandidateReportDownloadApproval" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="Label7" runat="server">Candidate Report Download Approval</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCandidate_Report_Download_Approval" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_CandidateReportDownloadApproval" runat="server" Text="" OnClick="btn_CandidateReportDownloadApproval_Click" OnClientClick="return CandidateReportDownloadApproval_Click()" />
                                    </td>
                                </tr>--%>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <%--User Report and graph--%>
            <div class="col-sm-6">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>User Report and Graph</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a href="#" data-toggle="modal" data-target="#myModalHelpReportGraph"><i class="fa fa-question"></i></a>
                            </li>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="form-horizontal" role="form" runat="server">
                            <%--Payment Details--%>
                            <div align="center">
                                <table id="testcompleted" class="table  table-striped" style="width: 80%; margin-top: 40px" runat="server">
                                    <tr>
                                        <td>
                                            <label>
                                                Download Report</label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hl_rpt" runat="server" Target="_blank">Download Report</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Report For Print</label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hl_rptPrint" runat="server" Target="_blank">Download Report</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Download Graph</label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hl_graph" runat="server" Target="_blank">Download Graph</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                                <div runat="server" id="testincompleted" visible="false" class="alert alert-danger" style="width: 80%; margin-top: 40px">Test Not Completed</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>CDF Remark</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a href="#" data-toggle="modal" data-target="#myModalHelpReportGraph"><i class="fa fa-question"></i></a>
                            </li>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="form-horizontal" role="form" runat="server">
                            <table>
                                <tr>
                                    <td><strong>History &nbsp;&nbsp;&nbsp;</strong></td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Width="400px" Height="100px" ScrollBars="Vertical">
                                            <asp:DataList ID="data_remark" runat="server">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_remark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                                    <asp:Label ID="lbl_date1" runat="server" Text="Date-Time" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("createdDateTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </asp:Panel>
                                    </td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td>
                                        <div style="margin-top: 10px;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><strong>Remark &nbsp;&nbsp;&nbsp;</strong></td>
                                    <td>
                                        <asp:TextBox ID="txt_remark" TextMode="MultiLine" CssClass="form-control" runat="server" Columns="50"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td>
                                        <div style="margin-top: 10px;">
                                            <asp:Button ID="linkBtnRemark" class="btn btn-success btn-sm"
                                                runat="server" Text="Save" OnClientClick="return Remark()"
                                                OnClick="linkBtnRemark_Click" />
                                            <%--<asp:LinkButton ID="linkBtnRemark" CssClass="btn btn-success btn-sm" runat="server" OnClick="linkBtnRemark_Click">Save</asp:LinkButton>--%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--User Information--%>
        <div class="x_panel">
            <div class="x_title">
                <h2>User Information</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li>
                        <%--<asp:LinkButton ID="btn_editInfo" runat="server" Text="Edit" Font-Bold="true" OnClientClick="return EditUserInfo()" OnClick="btn_editInfo_Click" />--%>
                        <asp:Button ID="btn_editInfo" Font-Bold="true" BackColor="White" BorderWidth="0px"
                            runat="server" Text="Edit" OnClientClick="return EditUserInfo()" OnClick="btn_editInfo_Click" />
                    </li>
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpInformation"><i class="fa fa-question"></i></a>
                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="form-horizontal" role="form" runat="server" style="padding: 0 180px 0 180px;">
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;">
                    </div>
                    <div align="center" class="x_panel">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <td>
                                        <label>Column Names</label></td>
                                    <td>
                                        <label>Details</label></td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>
                                            Name</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Email Id</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Dheya Email Id</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_dheyaemail" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Contact No</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_contact" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            City</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Gender</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            DOB</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Status</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_status" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">
                                        <label>
                                            User Status</label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lbl_userStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Password</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_password" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="colOne">
                                        <label>
                                            Address</label>
                                    </td>
                                    <td class="colTwo">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="colOne" style="width: 20%">
                                        <label>
                                            About Self</label>
                                    </td>
                                    <td class="colTwo" style="width: 80%">
                                        <asp:Label ID="lbl_aboutSelf" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Reg. Date</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_regDate" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <label>
                                            Image Formal</label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hl_formal" runat="server">Download</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Images Casual</label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hl_casual" runat="server">Download</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Resume</label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hl_resume" runat="server">Download</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            NDA</label>
                                    </td>
                                    <td>
                                        <div class=" col-md-offset-1 col-md-6" style="margin-left: -10px">
                                            <%--<a href="#" data-toggle="modal" title="click here to download NDA Document" class="" onclick="return GetAgreedAmount()" runat="server" id="A2" data-target="#myModal_agreedAmount_final">Generate & Download</a>--%>
                                            <asp:Button ID="hl_nda" runat="server" Text="Download" OnClick="hl_nda_click" OnClientClick="return download_nda()" Font-Bold="true" BackColor="White" BorderWidth="0px"/>
                                        </div>
                                        <%--<asp:LinkButton ID="hl_nda" runat="server" data-target="#myModal_agreedAmount_final">Download</asp:LinkButton>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Refund Status</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_refundStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Refund Amount</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_refundAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Field Of Work</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_fieldOfWork" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Mode Of Work</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_modeOfWork" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Industry Sector</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_industrySector" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Classification</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_classification" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Latitude</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_latitude" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Longitude</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_longitude" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Level</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_cdflevel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Google Business Listing</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_GBL" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <%--User Business--%>
        <div class="x_panel" id="div_business" runat="server" visible="false">
            <div class="x_title">
                <h2>User Business</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
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

                <div style="max-width: 100%">
                    <div class="row">
                        <%-- <div class="col-sm-3 col-sm-offset-2">
                            <asp:CheckBox ID="chk_GBL" Text="Gooble Business Listing" CssClass="checkbox-inline" runat="server" />
                        </div>--%>
                    </div>
                </div>
                <br />
                <div class="row">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_GBLStatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="col-sm-3 col-sm-offset-2" style="margin-top: 25px">
                                <label style="text-align: right;" class="control-label">
                                </label>
                                <asp:CheckBox ID="chk_GBL" Text="Gooble Business Listing" runat="server" />
                            </div>
                            <div class="col-sm-3">
                                <label class="control-label">
                                    Store Code:
                                </label>
                                <asp:TextBox ID="txt_StoreCode" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class=" control-label">
                            GBL Status
                        </label>
                        <asp:DropDownList ID="ddl_GBLStatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_GBLStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Published</asp:ListItem>
                            <asp:ListItem Value="2">Pending</asp:ListItem>
                            <asp:ListItem Value="3">Not Done</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class="control-label">
                            Comments
                        </label>
                        <asp:TextBox ID="text_comment" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <asp:Button ID="btn_saveBusinessList" class="btn btn-success btn-sm" runat="server" Text="Save" OnClick="btn_saveBusinessList_Click" OnClientClick="return SaveBusinessList()" />
                    </div>
                </div>
            </div>
        </div>

        <%--User Requirements--%>
        <div class="x_panel" id="div_req" runat="server">
            <div class="x_title">
                <h2>User Requirements</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
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

                <div style="max-width: 100%">
                    <div class="row">
                        <div class="col-sm-2 col-sm-offset-2">
                            <asp:CheckBox ID="chk_idcard" Text="Id Card" CssClass="checkbox-inline" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:CheckBox ID="chk_certificate" Text="Certificate" CssClass="checkbox-inline" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:CheckBox ID="chk_visitingCard" Text="Visiting Card" CssClass="checkbox-inline" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:CheckBox ID="chk_ndaCopy" Text="NDA Copy" CssClass="checkbox-inline" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2 col-sm-offset-2">
                            <asp:CheckBox ID="chk_childTest" Text="Child's Test" CssClass="checkbox-inline" runat="server" />
                        </div>
                        <div class="col-sm-2">
                            <asp:CheckBox ID="chk_childSession" Text="Child's Session" CssClass="checkbox-inline" runat="server" />
                        </div>
                        <div class="col-sm-2">
                            <asp:CheckBox ID="chk_spouseTest" Text="Spouse's Test" CssClass="checkbox-inline" runat="server" />
                        </div>
                    </div>
                </div>

                <%-- <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class=" control-label">
                            CDF Level
                        </label>
                        <asp:DropDownList ID="ddl_cdfLevel" CssClass="form-control" runat="server" ></asp:DropDownList>
                        
                    </div>
                </div>--%>



                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class=" control-label">
                            Training Batch
                        </label>

                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_cdfLevel" EventName="OnSelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>--%>
                        <asp:DropDownList ID="ddl_trainingBatch" CssClass="form-control" runat="server"></asp:DropDownList>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>

                <%-- <hr />
                <div style="max-width: 100%">
                    <div class="row">
                        <div class="col-sm-2 col-sm-offset-2">
                            <asp:Label ID="label_Level_1" Text="Level-1 Batch" runat="server"></asp:Label>
                        </div>

                        <div class="col-sm-2">
                            <asp:Label ID="lbl_Level_2" Text="Level-2 Batch" runat="server"></asp:Label>
                        </div>

                        <div class="col-sm-2">
                             <asp:Label ID="label_Level_3" Text="Level-3 Batch" runat="server"></asp:Label>
                        </div>

                       
                    </div>
                    <div class="row">
                        <div class="col-sm-2 col-sm-offset-2">
                             <asp:Label ID="lbl_L1" Text="Level-1 Batch n" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                             <asp:Label ID="lbl_L2" Text="Level-2 Batch n" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lbl_L3" Text="Level-3 Batch n" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <hr />--%>

                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class=" control-label">
                            Shadow Sessions
                        </label>
                        <asp:DropDownList ID="ddl_shadowSession" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <label style="text-align: right;" class="control-label">
                            Comments
                        </label>
                        <asp:TextBox ID="txt_comments" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-2">
                        <asp:Button ID="lbtn_traning" class="btn btn-success btn-sm"
                            runat="server" Text="Save" OnClientClick="return training()"
                            OnClick="lbtn_traning_Click" />
                        <%-- <asp:LinkButton ID="lbtn_traning" CssClass="btn btn-success btn-sm" runat="server" OnClientClick="return training()" OnClick="lbtn_traning_Click">Save</asp:LinkButton>--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Payment Details--%>
        <div class="x_panel">
            <div class="x_title">
                <h2>User's Payment Details</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-money"></i></a>
                        <ul class="dropdown-menu" role="menu">
                            <li>
                                <asp:Button ID="CustomPayment" runat="server" Text="Custom Payment" BackColor="White" BorderWidth="0px" OnClientClick="return payment()" OnClick="CustomPayment_Click" />
                                <%--<asp:LinkButton ID="CustomPayment" runat="server" OnClick="CustomPayment_Click" OnClientClick="return payment()">Custom Payment</asp:LinkButton>--%>
                            </li>
                            <li>
                                <asp:Button ID="UpdatePayment" runat="server" Text="Update Payment" BackColor="White" BorderWidth="0px" OnClientClick="return payment()" OnClick="UpdatePayment_Click" />
                                <%--<asp:LinkButton ID="UpdatePayment" runat="server" OnClick="UpdatePayment_Click" OnClientClick="return payment()">Update Payment</asp:LinkButton>--%>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpPayment"><i class="fa fa-question"></i></a>
                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div>
                    <div class="container centeralign">
                        <div style="text-align: center; color: #000000;">
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row" style="margin: 0px; display: none">
                                    <div class="col-sm-4 col-sm-offset-4">
                                        <asp:Label ID="lbl_tot" runat="server" Text="Total Paid Amount:- "></asp:Label>
                                        <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="padding: 5px">
                                    <div>
                                        <asp:Label ID="lbl_msg" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div align="center">
                                    <asp:GridView class="table table-responsive" ID="gv_paymentDetails" runat="server"
                                        Width="95%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--   User Bank Details Section--%>
        <div class="x_panel" id="div3" runat="server">
            <div class="x_title">
                <h2>User Bank Details</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="form-horizontal" role="form" runat="server" style="padding: 0 180px 0 180px;">
                    <div id="div2" runat="server" class="" style="text-align: center; margin-top: 10px;">
                    </div>
                    <div align="center" class="x_panel">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <td>
                                        <label>Column Names</label></td>
                                    <td>
                                        <label>Details</label></td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="width: 40%">
                                        <label>
                                            Account Holder Name</label>
                                    </td>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_accountHolderName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Account Number</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_accountNumber" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Bank Name</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_bankName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Branch Name</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_branchName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            IFSC Code</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ifscNo" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="x_panel" id="div4" runat="server">
            <div class="x_title">
                <h2>Latitude and Longitude of User</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
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
                <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                <br />
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm pull-left" runat="server" Text="Search" OnClientClick="return LLSearch()" OnClick="FindCoordinates" />

                        <br />
                        <br />
                        <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                            runat="server" AutoGenerateColumns="false" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <label style="text-align: right;" class=" control-label">
                                    Latitude:
                                </label>
                                <asp:TextBox ID="txtLatitude" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label style="text-align: right;" class=" control-label">
                                    Longitude:
                                </label>
                                <asp:TextBox ID="txtLongitude" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-sm-6">

                        <asp:Button ID="btnSaveLatLong" CssClass="btn btn-success btn-sm" runat="server" OnClick="btnSaveLatLong_Click" OnClientClick="return LLSave()" Text="Save"></asp:Button>

                    </div>
                </div>
                <div id="divErr" runat="server" class="" style="text-align: center; margin-top: 10px;">
                </div>
            </div>
        </div>

        <div class="x_panel" id="div8" runat="server" style="margin-bottom: 80px;">
            <div class="x_title">
                <h2>Dheya Updates</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpRequirements"><i class="fa fa-question"></i></a>
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
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtDheyaUpdate" CssClass="form-control" runat="server" TextMode="multiline"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6">

                        <asp:Button ID="btnDheyaUpdate" CssClass="btn btn-success btn-sm" runat="server" OnClientClick="return DheyaUpdate()" OnClick="btnDheyaUpdate_Click" Text="Save"></asp:Button>

                    </div>
                </div>
                <div id="divMsg" runat="server" class="" style="text-align: center; margin-top: 10px;">
                </div>
            </div>
        </div>
        <!-- Modal Providing Dheya Email Id-->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Providing Dheya Email Id</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div1" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Dheya Email Id :</label>
                                <div class="col-md-6">
                                    <asp:TextBox class="form-control" ID="txt_setDheyaEmail" runat="server"
                                        placeholder="Email ID" MaxLength="100"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="rfv_fname" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_setDheyaEmail" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-4   control-label">
                                    Level :
                                </label>
                                <div class="col-md-6">
                                    <asp:DropDownList class="form-control" ID="drop_setCdfLevel" runat="server">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="rfv_level" runat="server" ErrorMessage="*" InitialValue="--Select--"
                                        ControlToValidate="drop_setCdfLevel" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-offset-4 col-md-6">
                                    <asp:Button ID="btn_saveDheyaEmail" class="btn btn-primary btn-block"
                                        runat="server" Text="Create" OnClientClick="return Set_DheyaEmail()"
                                        ValidationGroup="a" OnClick="btn_saveDheyaEmail_Click" />
                                </div>
                                <div class=" col-md-1">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal UserStatus-->
        <div class="modal fade" id="myModal_UserStatus" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">User Status</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div7" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            <%--<div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Current Status :</label>
                                <div class="col-md-6">
                                    <asp:Label ID="lbl_Status1" Text="Status" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>--%>

                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Select Status :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddl_Status" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                        <asp:ListItem Value="2">DEACTIVE</asp:ListItem>
                                        <asp:ListItem Value="3">TERMINATED</asp:ListItem>
                                        <asp:ListItem Value="4">OPTOUT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-1">
                                    
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class=" col-md-offset-4 col-md-6">
                                    <asp:Button ID="btn_UserStatus" class="btn btn-primary btn-block"
                                        runat="server" Text="Submit" ValidationGroup="a" OnClientClick="return btn_Status_Click()" OnClick="btn_Status_Click"  />
                                </div>
                                <div class=" col-md-1">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Agreed Amount-->
        <div class="modal fade" id="myModal_agreedAmount" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">CDF Agree Amount</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div5" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;CDF Agree Amount :</label>
                                <div class="col-md-6">
                                    <asp:TextBox class="form-control" ID="txt_agreed_amount" runat="server"
                                        placeholder="Agree Amount"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                        TargetControlID="txt_agreed_amount" />
                                </div>

                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_agreed_amount" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Comment :</label>
                                <div class="col-md-6">
                                    <asp:TextBox class="form-control" ID="txt_comment" runat="server" TextMode="MultiLine"
                                        placeholder="Comment"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                </div>
                            </div>

                            <div class="row">
                                <div class=" col-md-offset-4 col-md-6">
                                    <asp:Button ID="btn_SaveAgreed_Amount" class="btn btn-primary btn-block"
                                        runat="server" Text="Submit" ValidationGroup="a"
                                        OnClientClick="return Agreed_Click()" OnClick="btn_Agreed_Amount_Click" />
                                </div>
                                <div class=" col-md-1">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Final Agreed Amount-->
        <div class="modal fade" id="myModal_agreedAmount_final" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">CDF Agreed Amount</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div id="div6" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Agreed Amount :</label>
                                <div class="col-md-6">
                                    <asp:TextBox class="form-control" ID="txt_Agreed_Amount_F" runat="server"
                                        ReadOnly="false" placeholder="Agreed Amount"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                        TargetControlID="txt_Agreed_Amount_F" />
                                </div>

                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_Agreed_Amount_F"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 10px;">
                                <label class="col-md-4  control-label">&nbsp;Comment :</label>
                                <div class="col-md-6">
                                    <asp:TextBox class="form-control" ID="txt_F_Comment" runat="server" TextMode="MultiLine"
                                        ReadOnly="false" placeholder="Comment"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                </div>
                            </div>

                            <div class="row" style="margin-top: 2px;">
                                <label class="col-md-4  control-label">&nbsp;</label>
                                <div class="col-md-6">
                                    <table class="table table-striped" style="width: 120%; margin-top: 20px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEdit" class="btn btn-primary btn-block"
                                                    runat="server" Text="Edit" OnClientClick="return Agreed_F_Click()" OnClick="btnEdit_Click1" />
                                                &nbsp;
                                            </td>
                                            <td style="margin-left: 10px">
                                                <asp:Button ID="btnNoChange" class="btn btn-primary btn-block"
                                                    OnClick="btnNoChange_Click" runat="server" Text="No Change" OnClientClick="return Agreed_F_Click()" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div class="col-md-1">
                                </div>
                            </div>

                            <div class="row">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Help Status -->
        <div class="modal fade" id="myModalHelpStatus" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Help</h4>
                    </div>
                    <div class="modal-body">
                        User Status:-<br />
                        Test Approval:-<br />
                        Dheya Email:-<br />
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Help Report Graph-->
        <div class="modal fade" id="myModalHelpReportGraph" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Help</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Help User Information-->
        <div class="modal fade" id="myModalHelpInformation" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Help</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Help User Requirements-->
        <div class="modal fade" id="myModalHelpRequirements" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Help</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Help User Payment-->
        <div class="modal fade" id="myModalHelpPayment" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Help</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid_fileName" runat="server" />


    </form>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>
    <script type="text/javascript">
        function btn_Status_Click()
        {

        }
        function UserAgreed() {

            return confirm("Are you sure you want to Agree with this amount?");
        }
        function UserApproved() {
            return confirm("Are you sure you want to Approved this user?");
        }
        function UserActive() {
            return confirm("Are you sure you want to Active this user?");
        }
        function UserDeactive() {
            return confirm("Are you sure you want to Deactive this user?");
        }
        function SaveBusinessList() {

        }
        function DheyaUpdate()
        { }
        function LLSave()
        { }
        function LLSearch()
        { }
        function TestReassign()
        { }
        function Remark() {

        }
        function EditUserInfo() {

        }
        function training() {

        }
        function Set_DheyaEmail() {

        }
        function test() {
            return confirm("Are you sure you want to Approved this user?");
        }

        function ProfileDisplayApproval() {

        }
        function ProfileUpdateApproval() {

        }
        function payment() {

        }
        function User_status_click() {

        }
        function CandidateReportDownloadApproval_Click()
        { }
        function download_nda()
        {}

        function GetAgreedAmount() {
            debugger;

            $.ajax({
                async: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "candidate-details.aspx/Get_Agreed_Amount",
                // data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) {
                    debugger;
                    $("[id*=txt_Agreed_Amount_F]").val(data.d);

                },

                error: function (data) {
                    alert("Error please try again");
                }
            });
        }

        function Agreed_Click() {
            debugger;
            var Amount = $.trim($('#<%=txt_agreed_amount.ClientID %>').val());
            if (Amount == "" || Amount == 0) {
                alert("Please enter valid Agreed amount");
                $('#<%=txt_agreed_amount.ClientID %>').focus();
                flag = false;
                return flag;
            }
            else {
                flag = true;
                return confirm("Are you sure you want to Agree with this amount?");
            }
        }
        function Agreed_F_Click() {
            debugger;
            var Amount_F = $.trim($('#<%=txt_Agreed_Amount_F.ClientID %>').val());
            if (Amount_F == "" || Amount_F == 0) {
                alert("Please enter valid Agreed amount");
                $('#<%=txt_Agreed_Amount_F.ClientID %>').focus();
                flag = false;
                return flag;
            }
            else {
                flag = true;
                return confirm("Are you sure you want to Agree with this amount?");
            }

        }
    </script>

</asp:Content>
