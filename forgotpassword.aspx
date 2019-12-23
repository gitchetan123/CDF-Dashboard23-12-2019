<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dheya Career Mentors | Forgot Password</title>
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
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
            <div id="resetbox" runat="server" style="margin-top: 50px;" class="">
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="text-center">
                                    <h3><i class="fa fa-lock fa-4x"></i></h3>
                                    <h2 class="text-center">Forgot Password?</h2>
                                    <p>You can reset your password here.</p>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="input-group" style="margin-bottom: 5px;">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-envelope color-blue"></i></span>
                                                <asp:TextBox ID="txt_emailid" type="text" required="" runat="server" placeholder="Enter your email address"
                                                    class="form-control"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="rfv_username" ControlToValidate="txt_emailid" runat="server"
                                                Text="Email address is required." Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="Please Enter valid Email-ID."
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_emailid"
                                                meta:resourcekey="RegularExpressionValidator1Resource1" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-lg btn-primary btn-block" Text="Reset Password"
                                                OnClick="btnSubmit_Click" />
                                        </div>

                                        <input type="hidden" class="hide" name="token" id="token" value="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="Div1" class="form-horizontal" role="form" runat="server" defaultbutton="btn_login">
                    <div id="div_msg" runat="server" class=" " style="text-align: center;">
                    </div>
                </div>
            </div>

            <div id="resetsuccess" runat="server" style="margin-top: 50px; text-align: center;" class="alert alert-success col-md-12 ">
                <h3>Your password has been reset. Please check your email for a password reset link.</h3>
            </div>

        </div>

    </form>
    <!-- Custom Theme Scripts -->
    <script src="js/custom.min.js"></script>
</body>
</html>

