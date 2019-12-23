<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dheya Career Mentors | CDF-Dashboard Admin Login</title>
        <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />   
    <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-social.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../images/fevicon.ico" type="image/x-icon" />


    <style type="text/css">
        .form-signin {
            max-width: 330px;
            padding: 15px;
            margin: 0 auto;
        }

            .form-signin .form-signin-heading, .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin .checkbox {
                font-weight: normal;
            }

            .form-signin .form-control {
                position: relative;
                font-size: 16px;
                height: auto;
                padding: 10px;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
            }

                .form-signin .form-control:focus {
                    z-index: 2;
                }

            .form-signin input[type="text"] {
                margin-bottom: -1px;
                border-bottom-left-radius: 0;
                border-bottom-right-radius: 0;
            }

            .form-signin input[type="password"] {
                margin-bottom: 10px;
                border-top-left-radius: 0;
                border-top-right-radius: 0;
            top: 0px;
            left: 0px;
        }

        .account-wall {
            margin-top: 20px;
            padding: 40px 0px 20px 0px;
            background-color: #f7f7f7;
            -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        }

        .login-title {
            color: #555;
            font-size: 18px;
            font-weight: 400;
            display: block;
        }

        .profile-img {
            width: 96px;
            height: 96px;
            margin: 0 auto 10px;
            display: block;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            border-radius: 50%;
        }

        .need-help {
            margin-top: 10px;
        }

        .new-account {
            display: block;
            margin-top: 10px;
        }
        .backgrd {
            background: linear-gradient(rgba(255,255,255,.1), rgba(255,255,255,.1)),url(../images/login1.jpg);
             background-size:cover;
        }
    </style>
</head>
<body class="backgrd">
    <form id="form1" runat="server" defaultbutton="btn_login">

        <div class="container" style="margin-top: 90px;">
            <div class="row">
                <div class="col-md-8 col-sm-8 col-xs-12">
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <h1 class="text-center login-title">Sign in to continue
                    </h1>
                    <div class="account-wall" style="padding:50px 0 50px 0;background: linear-gradient(rgba(255,255,255,.8), rgba(255,255,255,.8));">
                        <img class="profile-img" src="../images/dheya-icon.png"  alt="" />
                        <div class="form-signin">
                            <div id="div_msg" runat="server" style="margin-top: 10px; text-align: center; margin-bottom: 25px">
                            </div>
                            <asp:TextBox ID="txt_Ausername" type="text" runat="server" placeholder="Enter Username"
                                class="form-control" TabIndex="1"></asp:TextBox>
                            <asp:TextBox ID="txt_Apassword" type="password" runat="server" placeholder="Enter Password"
                                class="form-control" TabIndex="2"></asp:TextBox>
                            <div style="text-align: center">
                                <asp:RequiredFieldValidator ID="rfv_username" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_Ausername" runat="server" ErrorMessage="Email-ID is required."></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv_password" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_Apassword" runat="server" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vs_login" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <asp:LinkButton ID="btn_login" runat="server" class="btn btn-lg btn-primary btn-block"
                                Style="text-align: center" OnClick="btn_login_Click" TabIndex="3">Login</asp:LinkButton>
                            <span class="clearfix"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
