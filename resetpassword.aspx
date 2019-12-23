<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resetpassword.aspx.cs" Inherits="resetpassword" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dheya Career Mentors | Forgot Password</title>
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-social.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/favicons.ico" type="image/x-icon" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <style type="text/css">
        .form-gap {
            padding-top: 70px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="form-gap"></div>
        <div class="container">
            <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 20px">
            </div>
            <div id="resetfail" runat="server" style="margin-top: 50px; text-align: center;" class="alert alert-danger col-md-12 ">
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div id="reset" runat="server" class="form-signin">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="text-center">
                                    <h3><i class="fa fa-unlock-alt fa-4x"></i></h3>
                                    <h2 class="form-signin-heading">Reset Password</h2>
                                    <asp:TextBox Style="margin: 20px 0 15px 0" class="form-control" runat="server" ID="txt_password"
                                        name="password" placeholder="Password" required="" TextMode="Password"></asp:TextBox>
                                    <asp:TextBox class="form-control" runat="server" ID="txt_cpassword"
                                        name="password" placeholder="Confirm Password" required="" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_password"
                                        Display="None" ErrorMessage="Please Enter the password"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Password length should be more than 6 and less than 15"
                                        ValidationExpression="(.{6,15})" ControlToValidate="txt_password" meta:resourcekey="RegularExpressionValidator1Resource1"
                                        Display="None"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_cpassword"
                                        Display="None" ErrorMessage="Please Enter the Confirm password"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_password"
                                        ControlToValidate="txt_cpassword" Display="None" ErrorMessage="Password not match"
                                        Operator="Equal" meta:resourcekey="CompareValidator1Resource1" CssClass="test-body"></asp:CompareValidator>
                                    <asp:Button Style="margin-top: 20px" class="btn btn-lg btn-primary btn-block" ID="btnLogIn"
                                        runat="server" Text="Reset password" OnClick="btnLogIn_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="resetsuccess" runat="server" style="margin-top: 50px; text-align: center;"
                class="alert alert-success col-md-12 " visible="False">
                <h3>Your password has been reset successfully.
                <br />
                    <br />
                    <a href="Login.aspx"><u>Click Here for login</u></a>
                </h3>
            </div>
        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script src="js/custom.min.js"></script>
</body>
</html>
