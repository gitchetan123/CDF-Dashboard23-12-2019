<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="bank-details.aspx.cs" Inherits="CDF_bank_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <%-- <link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="../css/custom.min.css" rel="stylesheet" />

    <%-- Prevent cut , copy paste start code--%>
    <script
        src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });
        });

       
           
     
    </script>
    <%-- Prevent cut , copy paste end code --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Bank Details</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>

                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>

                     <div class="modal fade" id="myModalUpdate" role="dialog" data-backdrop="static" >
                        <div class="modal-dialog">         
                          <div class="modal-content">
                            <div class="modal-header">
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                              <h3 class="modal-title" style="text-align:center;">Alert !</h3>
                            </div>
                            <div class="modal-body" style="text-align:center;">
                              <h4 style="text-align:center; color:green;">Update Successful..!!</h4>           
                                <button id="btnbank_details" style=" min-width: 80px; max-width: 80px;" data-dismiss="modal"  type="button" class="btn btn-primary" >Ok</button>    
            
                              </div>  
                          </div>
                          </div>
                      </div>



                    <div class="x_content">
                        <br />
                        <div id="demo-form2" class="form-horizontal form-label-left">
                            <asp:Panel ID="pan" runat="server" Enabled="true">
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Account Holder Name:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_accountHolderName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="200"></asp:TextBox>
                                    </div>
                                  
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="rfv_accHoldname" runat="server" ErrorMessage="Account Holder Name is required." ControlToValidate="txt_accountHolderName"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Account Number:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_accountNumber" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_accountNumber" runat="server" ErrorMessage="Only Numbers allowed"
                                            ValidationExpression="\d+" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Bank Name:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_bankName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bank Name is required." ControlToValidate="txt_bankName"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Branch Name:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_branchName" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Branch Name is required." ControlToValidate="txt_branchName"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        IFSC Code:
                                    </label>
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txt_ifscNo" class="form-control col-md-7 col-xs-12" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="IFSC Code is required." ControlToValidate="txt_ifscNo"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="ln_solid"></div>

                            <div class="form-group ">
                                <asp:Button ID="btn_edit_update" class="btn btn-primary col-md-2 col-sm-12 col-xs-12 col-md-offset-5" runat="server" Text="Update" OnClick="btn_edit_update_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Custom Theme Scripts -->
    <script type="text/javascript" src="../js/custom.min.js"></script>
</asp:Content>

