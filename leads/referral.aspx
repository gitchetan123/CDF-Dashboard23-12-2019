<%@ Page Title="" Language="C#" MasterPageFile="~/CDFMaster.master" AutoEventWireup="true"
    CodeFile="referral.aspx.cs" Inherits="Candidate_referral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .row-space {
            padding: 5px;
        }
    </style>
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
    <form id="WebToLeadForm" action="https://www.dheya.com/crm/index.php?entryPoint=WebToPersonCapture"
        method="post" name="WebToLeadForm">
        <div class="x_panel">
            <div class="x_title">
                <h2>Referral Form</h2>
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
                    <input id="hf_cdf" type="hidden" runat="server" />

                    <input id="refered_by" name="refered_by" type="hidden" />
                </div>
                <div class="row row-space" style="padding-top: 20px;">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Lead Category (Parent/school/institution/cdf):*</span>
                        </label>
                        <span>
                            <select class="form-control" id="lead_category_c" name="lead_category_c" tabindex="1">
                                <option selected="selected" value="0">Select an Option</option>
                                <option value="6">Student</option>
                                <option value="1">Parent</option>
                                <option value="9">Professional</option>
                                <option value="3">School</option>
                                <option value="2">Institution</option>
                                <option value="7">Corporate</option>
                                <option value="4">CDF</option>
                                <option value="5">Business Associate</option>
                                <option value="8">NGO</option>
                                <option value="10">Others</option>
                            </select></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Enquired For: </span>
                        </label>
                        <span>
                            <select class="form-control" id="enquired_for_c" name="enquired_for_c" tabindex="1">
                                <option selected="selected" value="0">Select an Option</option>
                                <option value="Assessment">Career Assessment - 5999/- [Suitable for college Student Age Between 18-25 Year]</option>
                                <option value="1">Career Guidance - 9999/-  [Suitable for Student Age Above 13]</option>
                                <option value="CP">Career Planning - 19999/- [Suitable for Student Age Above 13]</option>
                                <option value="Early_Career_Planning">Early Career Planning -15000/- [ Suitable for working professional Below 35]</option>
                                <option value="Mid_Career_Planning">Mid-Career Planning - 20000/- [ Suitable for working professional above 35]</option>
                                <%--<option value="Evolve_Program">Evolve Program</option>
                                <option value="Boot_Camp">Boot Camp</option>--%>
                                <option value="Dheya_Career_Labs">Dheya Career Labs  [Educational Institution Tie Up]</option>
                                <option value="School_Workshops">School Workshops  [Suitable for students and parents]</option>
                                <option value="Employability_Workshops">Employability Workshops  [Suitable for UG and PG Colleges]</option>
                                <option value="CDF">Career Development Facilitator [ Suitable for professional willing to join Community above 32]</option>
                                <option value="Employee_Assessment_Program">Employee Assessment Program [Only for corporate - To Recruit employee]</option>
                                <option value="Employee_workshop">Employee workshop [Only for corporate- Employee kids career assessment]</option>
                                <option value="Employee_engagement_program">Employee engagement program [Only for corporate- Career Upgradation Program]</option>
                            </select></span>
                    </div>
                </div>
                 <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Lead Type:*</span>
                        </label>
                        <span>
                            <select class="form-control" id="lead_type_c" name="lead_type_c" tabindex="1" required="">
                                <option value=""></option>
                                <option value="1">The person may find Dheya's services useful. (5% conversion fee)</option>
                                <option value="2">The person is looking for Career Guidance & interested in Dheya's Program. (2% conversion fee)</option>
                                <option value="3">The person wants to enroll for Dheya's Career Guidance. (0% Conversion fee)</option>                                
                            </select></span>
                    </div>
                </div>                
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>First Name:* </span>
                        </label>

                        <span>
                            <input class="form-control" id="first_name" type="text" name="first_name" required="" /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Last Name:*</span>
                        </label>
                        <span>
                            <input class="form-control" id="last_name" type="text" name="last_name" required="" /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Email ID:*</span>
                        </label>
                        <span>
                            <input class="form-control" id="email1" type="email" name="email1" required=""/></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>School/Institution/College: </span>
                        </label>
                        <span>
                            <input class="form-control" id="school_college_name_c" type="text" name="school_college_name_c" /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Contact No.:* </span>
                        </label>
                        <span>
                            <input class="form-control" id="phone_mobile" type="number"
                                name="phone_mobile" required="" /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>City:* </span>
                        </label>
                        <span>
                            <input class="form-control" id="city_c" type="text" name="city_c" required="" /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Pincode: </span>
                        </label>
                        <span>
                            <input class="form-control" id="pincode_c" type="text" maxlength="6"  /></span>
                    </div>
                </div>
                <div class="row row-space">
                    <div class="col-md-6 col-sm-12 col-xs-12 col-md-offset-3">
                        <label class="control-label">
                            <span>Comment: (apostrophe ('), and (&), asterik (*) are prohibited because of security reasons.)</span>
                        </label>
                        <span>
                            <textarea class="form-control" id="description" name="description" onkeyup="this.value = this.value.replace(/[''&*<>]/g, '')"></textarea>
                        </span>
                    </div>
                </div>
                <select id="mode_c" name="mode_c" tabindex="1" hidden="">
                    <option selected="selected" value="CDF-Dashboard-Referral">CDF-Dashboard-Referral</option>
                </select>
                <select id="lead_source" name="lead_source" tabindex="1" hidden="">
                    <option selected="selected" value="5">CDF Referral</option>
                </select>
                <div class="ln_solid"></div>
                <div class="row row-space form-group" style="margin: 15px 0 20px 0">
                    <div class="col-md-4 col-sm-12 col-xs-12 col-md-offset-4">
                        <input id="myBtn" class="btn btn-primary btn-block" onclick="submit_form()"  type="submit" name="Submit" value="Submit" />
                        <%-- <input id="myBtn" class="btn btn-primary btn-block" onclick="submit_form(); this.disabled = true; this.value = 'Submitting, please wait...'; this.form.submit();" type="submit" name="Submit" value="Submit" />--%>
                    </div>
                </div>
            </div>

            <input id="Hidden1" type="hidden" name="campaign_id" value="c644dc77-9354-bef2-8ebc-56d2fccc27ea" />
            <%--  <input id="redirect_url" type="hidden" runat="server" name="redirect_url" value="http://localhost:5968/leads/referral_success.aspx" />--%>
            <input id="redirect_url" type="hidden" runat="server" name="redirect_url" value="<%$ AppSettings: ReferralSuccessPath %>" />
            <input id="Hidden3" type="hidden" name="assigned_user_id" value="1" />
             
            <input id="req_id" type="hidden" name="req_id" value="first_name;last_name;email1;current_company_c;phone_mobile;city_c;pincode_c;" />
            <input name="moduleDir" id="moduleDir" type="hidden" value="Leads" />
        </div>       
    </form>

    <script type="text/javascript">

        function submit_form() {
            document.getElementById('refered_by').value = document.getElementById('<%=hf_cdf.ClientID %>').value;          
        }

        $("#pincode_c").keyup(function () {
            $("#pincode_c").val(this.value.match(/[0-9]*/));
        });
               //function DisableButton() {
        //    document.getElementById("myBtn").disabled = true;
        //    document.getElementById("myBtn").value = "Submitting..."
        //}
        //window.onbeforeunload = DisableButton;
        //document.getElementById("myBtn").onclick = function () {
        //    //disable
        //    this.disabled = true;

        //    //do some validation stuff
        //}

    </script>
    <!-- Custom Theme Scripts -->
    <script src="../js/custom.min.js"></script>

</asp:Content>
