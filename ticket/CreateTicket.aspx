<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true" CodeFile="CreateTicket.aspx.cs" Inherits="Ticket_CreateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row-space {
            padding: 5px;
        }

        .panel {
            padding-bottom: 15px;
            max-width: 550px;
            margin: 30px auto;
        }

        label {
            text-align: left;
        }
    </style>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="WebToLeadForm"
        method="post" name="WebToLeadForm">
        <%-- action="https://www.dheya.com/crm/index.php?entryPoint=WebToPersonCapture"--%>
        <div class="x_panel">
            <div class="x_title">
                <h2>Create Ticket</h2>
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
                    <input id="cb_cdf" type="hidden" runat="server" />
                    <input id="ticket_created_by_c" name="ticket_created_by_c" type="hidden" />

                    <div class="row row-space" style="margin-top: 15px;">

                        <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                            <div class="hidden">
                                <select class="form-control" id="state" name="state" tabindex="1">
                                    <option value="Open">Open</option>
                                </select>
                            </div>

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

                    <div class="row row-space">

                        <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                            <label class="control-label">
                                <span>Subject</span>
                            </label>
                            <span>
                                <input class="form-control" id="name" type="text" name="name" placeholder="Subject" /></span>

                        </div>

                    </div>
                    <div class="row row-space">
                        <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
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

                    <div class="row row-space">
                        <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                            <label class="control-label">
                                <span>Description</span> ("Please donot use apostrophe ('), and (&), asterik (*) in this field." These 3 characters are prohibited because of security reasons.)
                            </label>
                            <span>
                                <textarea class="form-control" id="description" name="description" onkeyup="this.value = this.value.replace(/[''&*<>]/g, '')">
                            </textarea>
                            </span>
                        </div>
                    </div>
                    <select class="form-control hidden" id="type" name="type" tabindex="1">
                        <option value="User">User</option>
                    </select>
                    <select class="form-control hidden" id="status" name="status" tabindex="1">
                        <option value="Open_New">New</option>
                    </select>

                    <div class="ln_solid"></div>
                    <div class="row row-space form-group">
                        <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4">
                            <%--<input class="btn btn-info btn-block" onclick="submit_form();" type="button" name="Submit" value="Submit" />--%>
                            <div class="row row-space center buttons" style="margin: 5px 0 20px 0">
                                <input id="myBtn" class="btn btn-primary btn-block" onclick="DisableButton();" name="Submit" type="submit" value="Submit" />
                                <%--; this.disabled = true; this.value = 'Submitting, please wait...'; this.form.submit();--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input id="Hidden1" type="hidden" name="campaign_id" value="e0510c1a-5f24-a41d-785c-589c3e08533d" />
            <input id="Hidden4" type="hidden" name="account_id" value="91500f39-3f02-efdb-4427-58afd9fb7e5b" />
            <input id="redirect_url" type="hidden" runat="server" name="redirect_url" value="<%$ AppSettings: ViewTicketsPath %>" />
            <%-- <input id="redirect_url" type="hidden" name="redirect_url" value="http://localhost:5968/ticket/ViewTickets.aspx" />--%>
            <input id="Hidden3" type="hidden" name="assigned_user_id" value="1" />
            <input name="moduleDir" id="moduleDir" type="hidden" value="Cases" />
        </div>
    </form>

    <script type="text/javascript">
        function submit_form() {
            debugger;

            document.getElementById('ticket_created_by_c').value = document.getElementById('<%=cb_cdf.ClientID %>').value;
            var url = "https://www.dheya.com/crm/index.php?entryPoint=WebToPersonCapture";
            window.location = url;
        }
        function validate() {
            debugger;
            //var name = $("[id*=name]").val();
            var name = document.getElementById('name').value;
            if (name == "" || name == null) {
                $("[id*=name]").focus();
                return false;
            }
            else { return true; }
        }


        function DisableButton() {
            debugger;
            //document.getElementById("myBtn").disabled = true;
            //document.getElementById("myBtn").value = "Submiting..."
            if (validate()) {
                debugger;
                submit_form();
                $("[id*=myBtn]").prop('disabled', true);
            }
            else { $("[id*=myBtn]").prop('disabled', false); }
        }
        //window.onbeforeunload = DisableButton;
    </script>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>

</asp:Content>

