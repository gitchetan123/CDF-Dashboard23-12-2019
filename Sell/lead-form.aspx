<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="lead-form.aspx.cs" Inherits="Sell_lead_form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        #dvPreview {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
            min-height: 400px;
            min-width: 400px;
            display: none;
        }

        .row {
            padding: 1px;
        }

        .panel {
            max-width: 750px;
        }

        .leftalign {
            float: left;
        }

        html, body {
            height: 100%; /* The html and body elements cannot have any padding or margin. */
        }

        /* Wrapper for page content to push down footer */
        #wrap {
            min-height: 100%;
            height: auto; /* Negative indent footer by its height */
            margin: 0 auto -60px; /* Pad bottom by footer height */
            padding: 0 0 60px;
        }

        /* Set the fixed height of the footer here */
        #footer {
            height: 60px;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal_Sale').modal('show');
            $('#myModal_Lead').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="wrap">
        <form id="form2" runat="server">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Sell Form</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <%--<div id="div_success" runat="server" class=" alert alert-success" style="text-align: center; margin-bottom: 25px">
                    <button type="button" class="close" data-dismiss="alert" style="color:#ffffff;">&times;</button>
                   <b>If you want to refer School, College, Corporate
                         <input type="button" value="Click Here" onclick="window.location.href ='http://localhost:3105/leads/referral1.aspx'" /></b>
                </div>--%>
                    <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                    <div class="container" align="center">
                        <div class="row">
                            <div class="row">
                                <div class="alert " id="div1" runat="server" style="text-align: center; max-width: 80%;">
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    
                                    <div class="form-group">
                                        
                                        <span class="leftalign">Lead Category:*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Validators" Display="Static"
                                            ControlToValidate="ddl_leadcategory" runat="server" ErrorMessage="Lead Category is required."
                                            InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_leadcategory" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_leadcategory_SelectedIndexChanged">
                                            <asp:ListItem>Select an Option</asp:ListItem>
                                            <asp:ListItem Value="5">Student</asp:ListItem>
                                            <asp:ListItem Value="4">Parent</asp:ListItem>
                                            <%--<asp:ListItem Value="6">Professional</asp:ListItem>
                                           <asp:ListItem Value="12">Business</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="School">School</asp:ListItem>
                                            <asp:ListItem Value="College">College</asp:ListItem>
                                            <asp:ListItem Value="Corporate">Corporate</asp:ListItem>--%>

                                            <%--<asp:ListItem Value="0">Institution</asp:ListItem>
                                            <asp:ListItem Value="2">CDF</asp:ListItem>
                                            <asp:ListItem Value="12">Business Associate</asp:ListItem>
                                            <asp:ListItem Value="0">NGO</asp:ListItem>
                                            <asp:ListItem Value="0">Others</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_leadcategory" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <span class="leftalign">Enquired For:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_product" runat="server" ErrorMessage="Lead Category is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                                <asp:DropDownList ID="ddl_product" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_product_SelectIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">First Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_firstname" runat="server" ErrorMessage="First name is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_firstname" runat="server" placeholder="First Name" class="form-first-name form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <span class="leftalign">Last Name:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_lastname" runat="server" ErrorMessage="Last name is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_lastname" runat="server" placeholder="Last Name" class="form-first-name form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">Email:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_email" runat="server" ErrorMessage="Email Id is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter valid Email-ID..!"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"
                                                meta:resourcekey="RegularExpressionValidator1Resource1" Display="Static" ForeColor="Red">*</asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txt_email" runat="server" class="form-control" placeholder="Your Email Id" ReadOnly="false"
                                            ValidationGroup="GO" MaxLength="100" Enabled="True"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <span class="leftalign">Contact No:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_contact" runat="server" ErrorMessage="Contact No. is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_contact" runat="server" placeholder="Contact Number" class="form-first-name form-control"
                                            MaxLength="10"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                            TargetControlID="txt_contact" />
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <span class="leftalign">City:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_city" runat="server" ErrorMessage="City is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_city" runat="server" placeholder="City Name" class="form-first-name form-control"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>

                            <br />
                            <hr />
                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <div class="col-md-4" style="margin-left: -14px">
                                            <a href="#" data-toggle="modal" title="Click here to send payment link" class="btn btn-success btn-sm btn-block" onclick="return GetProductAmount()" runat="server" id="A1" data-target="#myModal_Sale">Sale</a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" data-toggle="modal" title="Click here to send lead to dheya" class="btn btn-success btn-sm btn-block" runat="server" id="A2" data-target="#myModal_Lead">Lead</a>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="Button2" runat="server" class="btn btn-success btn-sm btn-block" Text="Clear" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div id="myModalPopup_lead" class="modal fade" role="dialog" style="display: none" onshow="true" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="span12">
                                                <h2 style="font-family: 'Times New Roman', Times, serif">Information About Lead</h2>
                                                <button type="button" class="close" data-dismiss="modal">
                                                    &times;</button>
                                                <h4 class="modal-title" runat="server" id="H1"></h4>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                You Know the Client, They also know you and You have discussed about Dheya Programs. They are waiting for Dheya's Call
                                            </p>

                                            <div class="modal-footer">

                                                <button type="button" class="btn btn-primary" data-dismiss="modal">
                                                    OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="myModalPopup_contact" class="modal fade" role="dialog" style="display: none" onshow="true" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="span12">
                                                <h2 style="font-family: 'Times New Roman', Times, serif">Information About Contact</h2>
                                                <button type="button" class="close" data-dismiss="modal">
                                                    &times;</button>
                                                <h4 class="modal-title" runat="server" id="H2"></h4>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                You Know the Client but have not discussed about Dheya Programs.
                                            </p>

                                            <div class="modal-footer">

                                                <button type="button" class="btn btn-primary" data-dismiss="modal">
                                                    OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="myModalPopup_case" class="modal fade" role="dialog" style="display: none" onshow="true" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="span12">
                                                <h2 style="font-family: 'Times New Roman', Times, serif">Information About Case</h2>
                                                <button type="button" class="close" data-dismiss="modal">
                                                    &times;</button>
                                                <h4 class="modal-title" runat="server" id="H3"></h4>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                You have discussed about dheya and they are ready to buy the product.
                                            </p>

                                            <div class="modal-footer">

                                                <button type="button" class="btn btn-primary" data-dismiss="modal">
                                                    OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row" runat="server" id="div_married">
                                        <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                            <div class="form-group">
                                                <%-- <span class="leftalign">Spouse Name:</span>
                                                <asp:TextBox ID="txt_spouse" runat="server" placeholder="Spouse Name" class="form-first-name form-control"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                                    TargetControlID="txt_spouse" ValidChars=" " />--%>
                                            </div>
                                        </div>
                                        <div class="col-md-5 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <%--<span class="leftalign">Do you have Children? if Yes what Age?:</span>
                                                <asp:TextBox ID="txt_children" runat="server" placeholder="Ex- 10, 12" class="form-first-name form-control"
                                                    MaxLength="20"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom"
                                                    ValidChars="," TargetControlID="txt_children" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_married_status" EventName="SelectedIndexChanged" />
                                </Triggers>--%>
                            </asp:UpdatePanel>

                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <%--<span class="leftalign">Field of Work:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_fieldOfWork" runat="server" ErrorMessage="Field of Work field is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_fieldOfWork" runat="server" CssClass="form-control">
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>

                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <%--<span class="leftalign">Mode Of Work:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_modeOfWork" runat="server" ErrorMessage="Mode Of Work field is required."
                                        InitialValue="Select an Option" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />

                                        <asp:DropDownList ID="ddl_modeOfWork" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select an Option">Select an Option</asp:ListItem>
                                            <asp:ListItem Value="Job">Job</asp:ListItem>
                                            <asp:ListItem Value="Self Employed">Self Employed</asp:ListItem>
                                            <asp:ListItem Value="Business">Business</asp:ListItem>
                                            <asp:ListItem Value="Entrepreneur">Entrepreneur</asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <%-- <span class="leftalign">Industry Sector:*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="Validators" Display="Static"
                                        ControlToValidate="ddl_industrySector" runat="server" ErrorMessage="Industry Sector field is required."
                                        InitialValue="--Select--" ForeColor="Red">*</asp:RequiredFieldValidator></span><br />
                                        <asp:DropDownList ID="ddl_industrySector" runat="server" CssClass="form-control">
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>

                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <%--<span class="leftalign">Pincode:*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_pincode" runat="server" ErrorMessage="Pincode is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_pincode" runat="server" placeholder="Pincode"
                                            class="form-first-name form-control" MaxLength="6"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                    TargetControlID="txt_pincode" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-sm-12 col-xs-12 col-md-offset-1">
                                    <div class="form-group">
                                        <%--<span class="leftalign">Brief your profile (1000 Characters):*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" CssClass="Validators" Display="Static"
                                        ControlToValidate="txt_profile" runat="server" ErrorMessage="Your Profile is required."
                                        ForeColor="Red">*</asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txt_profile" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;"
                                            placeholder="Your Profile" ></asp:TextBox>
                                       <asp:RegularExpressionValidator Style="float: left" ID="RegularExpressionValidator4"
                                            ControlToValidate="txt_profile" runat="server" ValidationExpression="^[\s\S]{0,1000}$"
                                            Text="1000 characters max"
                                            ErrorMessage="Profile details max 1000 characters" />
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=".,;() &quot;" TargetControlID="txt_profile" />
                                        <div id="counter" style="float: right; color: red;">
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md-3 col-sm-12 col-xs-12 col-md-offset-3 ">
                                        <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block"
                                            Text="Submit" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12">
                                        <asp:Button ID="btn_clear" runat="server" class="btn btn-primary btn-block"
                                            ValidationGroup="clear" Text="Clear" OnClick="btn_clear_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <asp:TextBox ID="txtEndDate" runat="server" Enabled="False" BorderStyle="None" Font-Size="0pt"
                    Height="0px" Width="0px"></asp:TextBox>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                    Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" AllowSorting="false">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="false" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Program" HeaderText="Program" SortExpression="Program" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="PinCode" HeaderText="Pin Code" SortExpression="PinCode" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeadCategory" HeaderText="Lead Category" SortExpression="LeadCategory" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReferByEmail" HeaderText="Refer By Email" SortExpression="ReferByEmail" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Top" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                <!-- Modal Sale Product-->
                <div class="modal fade" id="myModal_Sale" role="dialog">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>

                                <h4 class="modal-title"><a><i class="fa fa-credit-card"></i>&nbsp;&nbsp;Fill the Payment Details</a></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">

                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rblfull" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rbl5" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rbl10" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rbl15" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rbl20" EventName="CheckedChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-md-8 col-md-offset-2">
                                                <div class="form-group">
                                                    <span class="leftalign">Product Price :*</span>
                                                    <asp:TextBox ID="txt_amount" placeholder="" class="form-control" Text="" ReadOnly="true"
                                                        runat="server"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                        TargetControlID="txt_amount" ValidChars=". " />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-9 col-md-offset-2">
                                        <div class="form-group">
                                            <span class="leftalign">Any discount offers :*</span><br />
                                            <br />
                                            <asp:RadioButton ID="rblfull" runat="server" Text=" Full Fees" GroupName="discount" OnCheckedChanged="rblfull_CheckedChanged" AutoPostBack="true" />
                                            &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl5" runat="server" Text="  5 %" GroupName="discount" OnCheckedChanged="rbl5_CheckedChanged" AutoPostBack="true" />
                                            &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl10" runat="server" Text="  10 %" GroupName="discount" OnCheckedChanged="rbl10_CheckedChanged" AutoPostBack="true" />
                                            &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl15" runat="server" Text="  15 %" GroupName="discount" OnCheckedChanged="rbl15_CheckedChanged" AutoPostBack="true" />
                                            &nbsp; &nbsp; &nbsp;
                            <asp:RadioButton ID="rbl20" runat="server" Text="  20 %" GroupName="discount" OnCheckedChanged="rbl20_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="col-md-4 col-md-offset-2">
                                            <asp:Button ID="btn_payment" runat="server" CssClass="btn btn-primary btn-block btn1"
                                                Text="Send" ValidationGroup="payment" OnClick="btn_payment_Click" />
                                            

                                             <asp:HiddenField ID="FullFees" runat="server" />
                                             <asp:HiddenField ID="txt_prod_Id" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal Lead only-->
                <div class="modal fade" id="myModal_Lead" role="dialog">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>

                                <h4 class="modal-title"><a><i class="fa fa-credit-card"></i>&nbsp;&nbsp;Refree Client</a></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-9 col-md-offset-2">
                                        <div class="form-group">
                                            <span class="leftalign"></span>
                                            <br />
                                            <p>Need Dheya -- to Close the Deal.</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="col-md-4 col-md-offset-2">
                                            <asp:Button ID="btn_submit_lead" runat="server" CssClass="btn btn-primary btn-block btn1"
                                                Text="Submit" OnClick="btn_submit_lead_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <input id="redirect_url" type="hidden" runat="server" name="redirect_url" value="<%$ AppSettings: ReferralSuccessPath %>" />
                </div>
        </form>

    </div>

    <script src="../js/custom.min.js"></script>
    <script type="text/javascript">
        function GetProductAmount() {
            debugger;

            $.ajax({
                async: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "lead-form.aspx/Get_Product_Amount",
                // data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) {
                    debugger;
                    $("[id*=txt_amount]").val(data.d);

                },

                error: function (data) {
                    alert("Error please try again");
                }
            });
        }
    </script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../js/MaxLength.min.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_tbDate1").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });
            //$("[id*=txt_profile]").MaxLength({ MaxLength: 1000, CharacterCountControl: $('#counter') });
        });
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btn_submit.ClientID %>").disabled = true;
            document.getElementById("<%=btn_submit.ClientID %>").value = "Submiting..."
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        disableSelection(document.body) //disable text selection on entire body of page
    </script>
    <script type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
    
    
</asp:Content>

