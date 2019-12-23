<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dheya Career Mentors | CDF-Portal Login</title>
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-social.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/favicons.ico" type="image/x-icon" />
   
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <style type="text/css">
        .checkb {
            margin-left: 20px;
        }

        .fontc {
            font-size: 12px;
        }

        .backgrd {
            /*background: linear-gradient(rgba(255,255,255,.5), rgba(255,255,255,.5)),url(images/login2.jpg);*/
            background: url(images/login-new.jpg) no-repeat center fixed;
            background-size:cover;            
        }
       
    </style>
    <%--Favicon--%>
    <link rel="icon" href="images/fevicon.ico" type="image/x-icon" />
</head>
<body class="backgrd">
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 70px;">
            <div class="row">
                <div class="col-md-8 col-sm-12 col-xs-12">
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12">
                    <h1 class="text-center login-title" style="color:#000000;font-weight:bold;">Sign in to CDF Portal</h1>
                    <div class="account-wall" style="background: linear-gradient(rgba(255,255,255,.8), rgba(255,255,255,.8)) center; ">
                        <img class="profile-img" src="images/dheya-icon.png"
                            alt="" />
                        <div class="form-signin">
                            <div id="div_msg" runat="server" style="margin-top: 10px; text-align: center; margin-bottom: 25px">
                            </div>
                            <asp:TextBox ID="txt_username" type="text" runat="server" placeholder="Enter Email-Id"
                                class="form-control" TabIndex="1"></asp:TextBox>
                            <asp:TextBox ID="txt_password" type="password" runat="server" placeholder="Password"
                                class="form-control" TabIndex="2"></asp:TextBox>
                            <div style="text-align: center">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter valid Email-ID."
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_username"
                                    meta:resourcekey="RegularExpressionValidator1Resource1" Display="None"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfv_username" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_username" runat="server" ErrorMessage="Email-ID is required."></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv_password" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_password" runat="server" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vs_login" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <asp:LinkButton ID="btn_login" runat="server"
                                class="btn btn-lg btn-primary btn-block" Style="text-align: center"
                                OnClick="btn_login_Click" TabIndex="3">Login to CDF-Portal</asp:LinkButton>
                           <%-- <a  class="btn btn-lg btn-success btn-block" style="text-align: center" href="http://webmail.dheya.in">Login to Dheya Email</a>--%>
                            <a  class="btn btn-lg btn-success btn-block" style="text-align: center" target="_blank" href="https://webmail.migadu.com">Login to Dheya Email</a>
                            <br />
                            <label class="checkbox pull-left fontc row">
                                Remember me
                            </label>
                            <asp:CheckBox ID="chkRememberMe" CssClass="checkb" runat="server" />
                            <asp:LinkButton ID="changepassword" class="pull-right need-help fontc" ValidationGroup="a" runat="server"
                                OnClick="forgotpassword_Click" TabIndex="6"><strong>Forgot Password</strong></asp:LinkButton>
                            <span class="clearfix"></span>

                            <%-- <div class="row text-center fontc">
                                <u><a href="https://webmail.migadu.com" data-toggle="modal" title="Click here to Login Dheya Email." tabindex="5"  target="_blank" style="color: green;font-size:15px"><strong>Login to Dheya Email</strong></a></u>
                            </div>--%>
                            <br />
                            <div class="row text-center fontc">
                                <%--<u><a href="#" data-toggle="modal" title="Create ticket to report issues." tabindex="5" data-target="#myModal" style="color: Red;"><strong>Report Issues</strong></a></u>--%>
                                <div class="row" style="text-align: center; margin-top: 10px;font-weight:bold;"><u><a href="pre/email-verification.aspx" style="color:#000000;">Create New Account</a></u></div>
                            </div>
                        </div>
                    </div>
                    
                    <%--<div class="row" style="text-align: center; margin-top: 10px;font-weight:bold;"><u><a href="http://webmail.dheya.in" style="color:#000000;">Login to Dheya Email</a></u></div>--%>
                </div>
            </div>

        </div>
    </form>
    <!-- Modal -->   
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">New Ticket</h4>
                </div>
                <div class="modal-body">
                    <form id="WebToLeadForm" action="https://www.dheya.com/crm/index.php?entryPoint=WebToPersonCapture"
                        method="post" name="WebToLeadForm">
                        <div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label">
                                        <span>Dheya's Email-Id:</span></label>
                                    <input id="ticket_created_by_c" class="form-control" name="ticket_created_by_c" type="text"
                                        placeholder="Enter Your Dheya's Email-Id" required="" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label">
                                        <span>Priority</span>
                                    </label>
                                    <span>
                                        <select class="form-control" id="priority" name="priority" tabindex="1">
                                            <option value="P1">High</option>
                                            <option value="P2">Medium</option>
                                            <option value="P3">Low</option>
                                        </select>
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label">
                                        <span>Subject</span>
                                    </label>
                                    <span>
                                        <input class="form-control" id="name" type="text" name="name" placeholder="Subject"
                                            required="" /></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label">
                                        <span>Issue</span>
                                    </label>
                                    <span>
                                        <select class="form-control" id="issue_c" name="issue_c" tabindex="1">
                                            <option value="0">Select an Option</option>
                                            <option value="1">Knowledge Based</option>
                                            <option value="2">Technical</option>
                                            <option value="3">Customer Service</option>
                                            <option value="4">Billing</option>
                                            <option value="5">Sales</option>
                                            <option value="6">Other</option>
                                        </select>
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <label class="control-label">
                                        <span>Description</span>
                                    </label>
                                    <span>
                                        <textarea class="form-control" id="description" name="description" onkeyup="this.value = this.value.replace(/[''&*<>]/g, '')">
                                        </textarea> </span>
                                </div>
                            </div>
                            <select class="form-control hidden" id="type" name="type" tabindex="1">
                                <option value="User">User</option>
                            </select>
                            <div class="row" style="margin: 20px 0 20px 0;">
                                <div class="col-md-4 col-md-offset-4">
                                    <div class="row center buttons">
                                        <input class="btn btn-info btn-block" name="Submit" type="submit"
                                            value="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <input id="Hidden1" type="hidden" name="campaign_id" value="e0510c1a-5f24-a41d-785c-589c3e08533d" />
                        <input id="Hidden4" type="hidden" name="account_id" value="dc2a8a4e-fd7e-9ed0-3a2c-58b6bb78608c" />
                       <%-- <input id="redirect_url" type="hidden" name="redirect_url" value="https://www.dheya.com/cdf-dashboard/ticketsuccess.aspx" />--%>
                         <input id="redirect_url" runat="server" type="hidden" name="redirect_url" value="<%$ AppSettings: LoginPageTicketSuccessPath %>" />
                        <input id="Hidden3" type="hidden" name="assigned_user_id" value="1" />
                        <input name="moduleDir" id="moduleDir" type="hidden" value="Cases" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
