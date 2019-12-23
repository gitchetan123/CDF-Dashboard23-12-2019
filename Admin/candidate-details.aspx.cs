//********************************************************************************************
//PageName        : Details   
//Description     : This page is display User details
//AddedBy         : Bahubali                   AddedOn   : 25/10/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          :
//*******************************************************************************************

using System;
using System.Configuration;
using System.Data;
using log4net;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using iTextSharp.text.pdf;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.Services;
using System.Net.Mail;
using System.Net.Mime;

public partial class candidatedetails : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    db_context_simsr dbContext_simsr = new db_context_simsr();
    int id = 0;
    string fname, lname;
    //payment 
    int c_id;
    string userStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strcmd1 = "select remark, createdDateTime from tblRemark where uId='" + Convert.ToInt32(Request.QueryString["id"]) + "'";
        DataSet ds1 = dbContext.ExecDataSet(strcmd1);
        int row_count = ds1.Tables[0].Rows.Count;
        data_remark.DataSource = ds1;
        data_remark.DataBind();
        divErr.Visible = false;

        //payment
        try
        {
            if (Session["type"].ToString() == "SuperAdmin")
            {
                //tr_CandidateReportDownloadApproval.Visible = true;
                //div_business.Visible = true;
            }
            if (Session["type"].ToString() == "Admin")
            {
                //div_business.Visible = true;
            }

            // Get user in using QueryString 
            c_id = Convert.ToInt32(Request.QueryString["id"]);
            Session["CID"] = c_id;
            readBankDetailsData();

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //Print Error msg when query string is not correct format 
            lbl_msg.Text = ex.Message;
        }

        if (!Page.IsPostBack)
        {
            //Payment
            try
            {

                //lblCandidate_Report_Download_Approval.Text = "DENIED";
                //btn_CandidateReportDownloadApproval.Text = "ACTIVE";
                //btn_CandidateReportDownloadApproval.CssClass = "btn btn-success btn-sm btn-block";



                //  productInfo,txnId,payDate, amount,status,paymentgateway
                //select Query for dispay all Payment details for (Success,failure,pending and not Responding)
                string query_select = "select productInfo as [Product Name],upper(txnId) as [Transaction ID],payDate as [Payment Date], amount as Amount, status as Status,paymentgateway as [Payment Gateway] from tblPayment where uId='" + c_id + "'";
                DataSet ds = dbContext.ExecDataSet(query_select);

                //fill the blank cell on gried view.
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[i][j].ToString()))
                        {
                            //  Custom Code fill the string value
                            ds.Tables[0].Rows[i][j] = "Not Completed";
                        }
                    }
                }

                //If no record found then count zero and print no record found.
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lbl_msg.Text = "No Record Found...";
                }
                else
                {
                    lbl_msg.Text = "";
                }

                //set dataset to girdview
                gv_paymentDetails.DataSource = ds;
                gv_paymentDetails.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                lbl_msg.Text = ex.Message;
            }
        }

        else
        {

        }

        //user Details
        try
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            if (id != 0)
            {
                //Get all User information
                string strcmd = "SELECT A.uId as id,fname,lname,dob,contactNo,email,dheyaEmail,C.name as city,A.status,gender,password,address,regDateTime,B.dheyaUpdates as dheyaUpdates, " +
                    " userStatus,aboutSelf,idcard,certificate,visitingCard,ndaCopy,formalImg,casualImg,resume,batchId,comments,B.refundStatus,B.refundAmount,B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,B.shadowSession,A.profileDisplayApproval,A.profileUpdateApproval, "+
                    " B.fieldOfWork,B.modeOfWork,B.industrySector,B.remark,B.classification,B.latitude,B.longitude,cdfLevel, B.ndaStatus, AG.agreedAmount, AG.locked, B.batchId_L2, B.batchId_L3, A.cdfId, UB.GBL, UB.status as GBL_status, UB.store_code, UB.comment  FROM tblUserMaster as A left outer join tblUserDetails as B " +
                    " on A.uId = B.uId left outer join tblCitiesMaster as C on A.cityid=C.id left outer join tblAgreedAmount as AG on AG.uId=A.uId  left outer join tblUserBusiness as UB on A.uId=UB.uId  where userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' and A.uId='" + id + "'";
                DataSet ds = dbContext.ExecDataSet(strcmd);

                if (!IsPostBack)
                {
                    //download Report and Graph
                    string str = "select count(id) from tblUserProductMaster where prodid=7 and teststatus='Complete' and uId=" + id + "";
                    int count = Convert.ToInt32(dbContext.ExecScal(str));
                    if (count != 0)
                    {
                        hl_rpt.NavigateUrl = ConfigurationManager.AppSettings["TestReportlink"].ToString() + id.ToString();
                        hl_rptPrint.NavigateUrl = ConfigurationManager.AppSettings["ReportForPrintlink"].ToString() + id.ToString();
                        hl_graph.NavigateUrl = ConfigurationManager.AppSettings["Graphlink"].ToString() + id.ToString();
                    }
                    else
                    {

                        testcompleted.Visible = false;
                        testincompleted.Visible = true;
                    }

                    // DataSet ds = dbContext.ExecDataSet(strcmd); // this is moved from here . above !IsPostBack condition.

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbl_name.Text = ds.Tables[0].Rows[0]["fname"].ToString() + " " + ds.Tables[0].Rows[0]["lname"].ToString();

                        ViewState["FirstName"] = ds.Tables[0].Rows[0]["fname"].ToString();
                        ViewState["LastName"] = ds.Tables[0].Rows[0]["lname"].ToString();

                        lbl_email.Text = ds.Tables[0].Rows[0]["email"].ToString();
                        lbl_dheyaemail.Text = ds.Tables[0].Rows[0]["dheyaEmail"].ToString();

                        lbl_contact.Text = ds.Tables[0].Rows[0]["contactNo"].ToString();
                        lbl_city.Text = ds.Tables[0].Rows[0]["city"].ToString();
                        lbl_gender.Text = ds.Tables[0].Rows[0]["gender"].ToString();
                        if (ds.Tables[0].Rows[0]["dob"].ToString() != "")
                        {
                            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString());
                            lbl_dob.Text = dt.ToString("dd/MM/yyyy");
                        }
                        lbl_status.Text = ds.Tables[0].Rows[0]["status"].ToString();
                        lbl_userStatus.Text = ds.Tables[0].Rows[0]["userStatus"].ToString();
                        lbl_password.Text = ds.Tables[0].Rows[0]["password"].ToString();
                        lbl_address.Text = ds.Tables[0].Rows[0]["address"].ToString();
                        lbl_aboutSelf.Text = ds.Tables[0].Rows[0]["aboutSelf"].ToString();
                        lbl_regDate.Text = ds.Tables[0].Rows[0]["regDateTime"].ToString();
                        lbl_refundStatus.Text = ds.Tables[0].Rows[0]["refundStatus"].ToString();
                        lbl_refundAmount.Text = ds.Tables[0].Rows[0]["refundAmount"].ToString();
                        lbl_fieldOfWork.Text = ds.Tables[0].Rows[0]["fieldOfWork"].ToString();
                        lbl_modeOfWork.Text = ds.Tables[0].Rows[0]["modeOfWork"].ToString();
                        lbl_industrySector.Text = ds.Tables[0].Rows[0]["industrySector"].ToString();
                        txt_remark.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                        lbl_classification.Text = ds.Tables[0].Rows[0]["classification"].ToString();
                        lbl_latitude.Text = ds.Tables[0].Rows[0]["latitude"].ToString();
                        lbl_longitude.Text = ds.Tables[0].Rows[0]["longitude"].ToString();
                        lbl_cdflevel.Text = ds.Tables[0].Rows[0]["cdfLevel"].ToString();

                        txtDheyaUpdate.Text = ds.Tables[0].Rows[0]["dheyaUpdates"].ToString();
                        txtLocation.Text = ds.Tables[0].Rows[0]["address"].ToString();


                        if (ds.Tables[0].Rows[0]["agreedAmount"].ToString() != "")
                        {
                            lbl_agreedAmount.Text = ds.Tables[0].Rows[0]["agreedAmount"].ToString();
                            lbl_agreedAmount.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            btnNoChange.Visible = false;
                            lbl_agreedAmount.Text = "NA";
                            lbl_agreedAmount.ForeColor = System.Drawing.Color.Red;
                        }

                        if (ds.Tables[0].Rows[0]["locked"].ToString() == "1")
                        {
                            A1.Visible = false;
                            lbl_agreedAmount.Text = "Rs. " + ds.Tables[0].Rows[0]["agreedAmount"].ToString();
                            lbl_NDAStatus.Text = "NDA doc. printed";
                            lbl_agreedAmount.ForeColor = System.Drawing.Color.Green;
                            lbl_NDAStatus.ForeColor = System.Drawing.Color.Green;
                        }


                        if (ds.Tables[0].Rows[0]["formalImg"].ToString() != "")
                        {
                            hl_formal.NavigateUrl = "~/Admin/download.aspx?filename=" + ConfigurationManager.AppSettings["imageFormalPath"].ToString() + "" + ds.Tables[0].Rows[0]["formalImg"].ToString() + "";
                        }
                        else
                        {
                            hl_formal.Text = "Pending";
                            hl_formal.ForeColor = System.Drawing.Color.Red;
                        }
                        if (ds.Tables[0].Rows[0]["casualImg"].ToString() != "")
                        {
                            hl_casual.NavigateUrl = "~/Admin/download.aspx?filename=" + ConfigurationManager.AppSettings["imageCasualPath"].ToString() + "" + ds.Tables[0].Rows[0]["casualImg"].ToString() + "";
                        }
                        else
                        {
                            hl_casual.Text = "Pending";
                            hl_casual.ForeColor = System.Drawing.Color.Red;
                        }
                        if (ds.Tables[0].Rows[0]["resume"].ToString() != "")
                        {
                            hl_resume.NavigateUrl = "~/Admin/download.aspx?filename=" + ConfigurationManager.AppSettings["resumePath"].ToString() + "" + ds.Tables[0].Rows[0]["resume"].ToString() + "";
                        }
                        else
                        {
                            hl_resume.Text = "Pending";
                            hl_resume.ForeColor = System.Drawing.Color.Red;
                        }



                        //active and deactive users
                        lbl_infoUserStatus.Text = ds.Tables[0].Rows[0]["userStatus"].ToString();
                        if (ds.Tables[0].Rows[0]["userStatus"].ToString() == "ACTIVE")
                        {
                            btn_status.Text = "DEACTIVE";
                            btn_status.CssClass = "btn btn-danger btn-sm btn-block";
                        }
                        else
                        {
                            btn_status.Text = "ACTIVE";
                            btn_status.CssClass = "btn btn-success btn-sm btn-block";
                        }

                        //user Approval
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "Reg_Complete")
                        {
                            if (count != 0)
                            {
                                lbl_infoApproveStatus.Text = "Test Complete";
                                btn_approve.Text = "APPROVE";
                                btn_approve.CssClass = "btn btn-success btn-sm btn-block";
                                btn_approve.Enabled = true;
                                btn_testReassing.Enabled = true;
                                btn_setDheyaEmail.Disabled = false;
                                div_req.Visible = true;
                                div_business.Visible = true;
                            }
                            else
                            {
                                lbl_infoApproveStatus.Text = "Pending";
                                btn_approve.Text = "Wait for Test";
                                btn_approve.CssClass = "btn btn-danger btn-sm btn-block";
                                btn_setDheyaEmail.Disabled = true;
                                div_req.Visible = false;
                                div_business.Visible = false;
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["status"].ToString() == "APPROVED")
                        {
                            lbl_infoApproveStatus.Text = "APPROVED";
                            btn_approve.Text = "APPROVED";
                            btn_approve.CssClass = "btn btn-success btn-sm btn-block";
                        }
                        else
                        {
                            lbl_infoApproveStatus.Text = "Old CDF";
                            btn_approve.Text = "APPROVED";
                            btn_approve.CssClass = "btn btn-success btn-sm btn-block";
                        }

                        //Dheya Email
                        if (ds.Tables[0].Rows[0]["dheyaEmail"].ToString() == "")
                        {

                            lbl_dheyaEmailStatus.Text = "No";
                            if (btn_approve.Enabled == false)
                            {
                                btn_setDheyaEmail.Visible = true;
                            }
                            else
                            {
                                btn_setDheyaEmail.Visible = false;
                            }
                        }
                        else
                        {
                            lbl_dheyaEmailStatus.Text = "Yes";
                            btn_setDheyaEmail.Visible = false;
                        }

                        //active and deactive profileDisplayApproval
                        lbl_profileDisplayApproval.Text = ds.Tables[0].Rows[0]["profileDisplayApproval"].ToString();
                        if (lbl_profileDisplayApproval.Text == "True")
                        {
                            lbl_profileDisplayApproval.Text = "ACTIVE";
                        }
                        else
                        {
                            lbl_profileDisplayApproval.Text = "DEACTIVE";
                        }
                        if (lbl_profileDisplayApproval.Text == "ACTIVE")
                        {
                            btn_profileDisplayApproval.Text = "DEACTIVE";
                            btn_profileDisplayApproval.CssClass = "btn btn-danger btn-sm btn-block";
                        }
                        else
                        {
                            btn_profileDisplayApproval.Text = "ACTIVE";
                            btn_profileDisplayApproval.CssClass = "btn btn-success btn-sm btn-block";
                        }

                        //active and deactive profileUpdateApproval
                        lbl_profileUpdateApproval.Text = ds.Tables[0].Rows[0]["profileUpdateApproval"].ToString();
                        if (lbl_profileUpdateApproval.Text == "True")
                        {
                            lbl_profileUpdateApproval.Text = "ACTIVE";
                        }
                        else
                        {
                            lbl_profileUpdateApproval.Text = "DEACTIVE";
                        }

                        if (lbl_profileUpdateApproval.Text == "ACTIVE")
                        {
                            btn_profileUpdateApproval.Text = "DEACTIVE";
                            btn_profileUpdateApproval.CssClass = "btn btn-danger btn-sm btn-block";
                        }
                        else
                        {
                            btn_profileUpdateApproval.Text = "ACTIVE";
                            btn_profileUpdateApproval.CssClass = "btn btn-success btn-sm btn-block";
                        }

                        if (ds.Tables[0].Rows[0]["idcard"].ToString() == "True")
                        {
                            chk_idcard.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["certificate"].ToString() == "True")
                        {
                            chk_certificate.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["visitingCard"].ToString() == "True")
                        {
                            chk_visitingCard.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["ndaCopy"].ToString() == "True")
                        {
                            chk_ndaCopy.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["childTestStatus"].ToString() == "True")
                        {
                            chk_childTest.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["childSessionStatus"].ToString() == "True")
                        {
                            chk_childSession.Checked = true;
                        }
                        if (ds.Tables[0].Rows[0]["spouseTestStatus"].ToString() == "True")
                        {
                            chk_spouseTest.Checked = true;
                        }

                        txt_comments.Text = ds.Tables[0].Rows[0]["comments"].ToString();

                        if (ds.Tables[0].Rows[0]["shadowSession"].ToString() != "")
                        {
                            //ddl_shadowSession.Items.FindByValue(ds.Tables[0].Rows[0]["shadowSession"].ToString()).Selected = true;
                            ddl_shadowSession.SelectedValue = ds.Tables[0].Rows[0]["shadowSession"].ToString();
                        }

                        string cdfId = ds.Tables[0].Rows[0]["cdfId"].ToString();
                        ViewState["CDFID"] = cdfId;
                        //string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) order by id desc";
                        //dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);


                        if (lbl_cdflevel.Text == "1")
                        {
                            string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) and cdfLevel =1 order by id desc";
                            dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);
                            if (ds.Tables[0].Rows[0]["batchId"].ToString() != "")
                            {
                                ddl_trainingBatch.Items.FindByValue(ds.Tables[0].Rows[0]["batchId"].ToString()).Selected = true;
                            }
                        }
                        if (lbl_cdflevel.Text == "2")
                        {
                            string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) and cdfLevel =2 order by id desc";
                            dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);
                            if (ds.Tables[0].Rows[0]["batchId_L2"].ToString() != "")
                            {
                                ddl_trainingBatch.Items.FindByValue(ds.Tables[0].Rows[0]["batchId_L2"].ToString()).Selected = true;
                            }
                        }
                        if (lbl_cdflevel.Text == "3")
                        {
                            string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) and cdfLevel =3 order by id desc";
                            dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);
                            if (ds.Tables[0].Rows[0]["batchId_L3"].ToString() != "")
                            {
                                ddl_trainingBatch.Items.FindByValue(ds.Tables[0].Rows[0]["batchId_L3"].ToString()).Selected = true;
                            }
                        }
                        if (lbl_cdflevel.Text == "4")
                        {
                            string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) and cdfLevel =4 order by id desc";
                            dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);
                        }


                        //if (ds.Tables[0].Rows[0]["batchId"].ToString() != "")
                        //{
                        //    ddl_trainingBatch.Items.FindByValue(ds.Tables[0].Rows[0]["batchId"].ToString()).Selected = true;
                        //}

                        txt_StoreCode.Text = ds.Tables[0].Rows[0]["store_code"].ToString();
                        text_comment.Text = ds.Tables[0].Rows[0]["comment"].ToString();
                        lbl_GBL.Text= ds.Tables[0].Rows[0]["GBL_status"].ToString();
                        if (lbl_GBL.Text == "Published")
                        {
                            lbl_GBL.Text = ds.Tables[0].Rows[0]["GBL_status"].ToString();
                            lbl_GBL.ForeColor = System.Drawing.Color.Green;
                        }
                        if (lbl_GBL.Text == "Pending")
                        {
                            lbl_GBL.Text = ds.Tables[0].Rows[0]["GBL_status"].ToString();
                            lbl_GBL.ForeColor = System.Drawing.Color.Brown;
                        }
                        if (lbl_GBL.Text == "Not Done")
                        {
                            lbl_GBL.Text = ds.Tables[0].Rows[0]["GBL_status"].ToString();
                            lbl_GBL.ForeColor = System.Drawing.Color.Red;
                        }
                        if (lbl_GBL.Text == "")
                        {
                            lbl_GBL.Text = "Not Done";
                            lbl_GBL.ForeColor = System.Drawing.Color.Red;
                        }

                        if (ds.Tables[0].Rows[0]["GBL"].ToString() == "True")
                        {
                            chk_GBL.Checked = true;
                        }
                    }
                    else
                    {
                        //display when data is not fount        
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "No record found..!!!";
                        btn_editInfo.Visible = false;
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["userStatus"].ToString() != "")
                    {
                        lbl_infoUserStatus1.Text = ds.Tables[0].Rows[0]["userStatus"].ToString();
                        if (lbl_infoUserStatus1.Text == "ACTIVE")
                        {
                            lbl_infoUserStatus1.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (lbl_infoUserStatus1.Text == "DEACTIVE")
                        {
                            lbl_infoUserStatus1.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (lbl_infoUserStatus1.Text == "TERMINATED")
                        {
                            lbl_infoUserStatus1.ForeColor = System.Drawing.Color.Purple;
                        }
                        else
                        {
                            lbl_infoUserStatus1.ForeColor = System.Drawing.Color.RoyalBlue;
                        }
                    }
                    else
                    {
                        lbl_infoUserStatus1.Text = "";
                    }

                    if (ds.Tables[0].Rows[0]["agreedAmount"].ToString() != "")
                    {
                        lbl_agreedAmount.Text = ds.Tables[0].Rows[0]["agreedAmount"].ToString();
                        lbl_agreedAmount.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_agreedAmount.Text = "NA";
                        lbl_agreedAmount.ForeColor = System.Drawing.Color.Red;
                    }

                    if (ds.Tables[0].Rows[0]["locked"].ToString() == "1")
                    {
                        A1.Visible = false;
                        lbl_agreedAmount.Text = "Rs. " + ds.Tables[0].Rows[0]["agreedAmount"].ToString();
                        lbl_NDAStatus.Text = "NDA doc. printed";
                        lbl_agreedAmount.ForeColor = System.Drawing.Color.Green;
                        lbl_NDAStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong. Please Try again.";
            btn_editInfo.Visible = false;
        }
    }
    protected void btn_editInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Update_Info.aspx?id=" + id);
    }
    protected void btn_status_Click(object sender, EventArgs e)
    {
        string queryUpdateStatus;

        if (lbl_infoUserStatus.Text == "ACTIVE")
        {
            queryUpdateStatus = "update tblUserMaster set userStatus ='DEACTIVE',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryUpdateStatus);
        }
        else if (lbl_infoUserStatus.Text == "DEACTIVE")
        {
            //update User status 
            queryUpdateStatus = "update tblUserMaster set userStatus ='ACTIVE',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryUpdateStatus);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        string queryUpdate = "update tblUserMaster set status ='APPROVED', modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
        int i = dbContext.ExecNonQuery(queryUpdate);
        if (i > 0)
        {
            //call to Templete file for email body
            string body = this.PopulateBody(lbl_name.Text);

            //Send email
            var task = new Thread(() => datacontext.sendemail(lbl_email.Text, null, null, ConfigurationManager.AppSettings["CDFApprovalEmailSubject"], body));
            task.Start();
            if (lbl_contact.Text != "")
            {
                string SMSText = ConfigurationManager.AppSettings["CDFApprovalSmsTemplate"].ToString();
                SMSText = SMSText.Replace("{CDF}", lbl_name.Text);
                datacontext.sendSms(lbl_contact.Text, SMSText);
            }
        }
        Response.Redirect(Request.RawUrl);
    }
    private string PopulateBody(string userName)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFApprovalEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_saveDheyaEmail_Click(object sender, EventArgs e)
    {
        string queryUpdate = "update tblUserMaster set dheyaEmail ='" + txt_setDheyaEmail.Text + "',cdfLevel='" + drop_setCdfLevel.Text + "',cdfApproved='APPROVED',profileUpdateApproval=0,cdfrating='3.00',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
        int i = dbContext.ExecNonQuery(queryUpdate);
        if (i > 0)
        {
            //call to Templete file for email body
            string body = this.PopulateBodyWelcomeEmail(lbl_name.Text, txt_setDheyaEmail.Text, lbl_password.Text);
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Check if user has completed test or not
                string query = "Select email, password from tblUserMaster where uId='" + id + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(query, connection);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string cdfEmail = dr["email"].ToString();
                string cdfPassword = dr["password"].ToString();

                //Send email with file attchement
                string filePath = "~/doc/VALUE_PROPOSITION-CAREER_MENTOR.pdf";
                //var task = new Thread(() => datacontext.sendemail(txt_setDheyaEmail.Text, null, ConfigurationManager.AppSettings["BCCWelcomeEmail"].ToString(), ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body));
                var task = new Thread(() => this.sendEmailWithAttachment(txt_setDheyaEmail.Text, null, ConfigurationManager.AppSettings["BCCWelcomeEmail"].ToString(), ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body, filePath));
                task.Start();
                

                if (cdfEmail != "")
                {
                    var task2 = new Thread(() => datacontext.sendemail(cdfEmail, null, null, ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body));
                    task2.Start();
                }

                if (lbl_contact.Text != "")
                {
                    string SMSText = ConfigurationManager.AppSettings["WelcomeEmailSmsTemplate"].ToString();
                    SMSText = SMSText.Replace("{CDF}", lbl_name.Text);
                    datacontext.sendSms(lbl_contact.Text, SMSText);
                }
            }
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void linkBtnRemark_Click(object sender, EventArgs e)
    {
        try
        {
            string queryRemarkUpdate = "update tblUserDetails set remark ='" + txt_remark.Text + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId=" + c_id + "";
            int i = dbContext.ExecNonQuery(queryRemarkUpdate);
            if (i > 0)
            {
                string queryRemarkInsert = "insert into tblRemark (uid, remark, remarkBy) values ('" + c_id + "', '" + txt_remark.Text + "', '" + Session["adminuser_id"] + "')";
                int j = dbContext.ExecNonQuery(queryRemarkInsert);
                Response.Redirect(Request.RawUrl, false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private string PopulateBodyWelcomeEmail(string userName, string userEmail, string userPassword)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["WelcomeEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{userEmail}", userEmail);
            //body = body.Replace("{userPassword}", "Please use the password that you had created when you had signed up for the CDF program to take the assessment in the CDF dashboard. In case you donot remember the password please click 'Forgot password'. We will reset your password & send mail on your dheya mail id.");
            body = body.Replace("{userPassword}", userPassword);
            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CustomPayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/custom-payment.aspx?id=" + c_id + "", false);
    }
    protected void UpdatePayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/cash-payment.aspx?id=" + c_id + "", false);
    }
    protected void lbtn_traning_Click(object sender, EventArgs e)
    {
        try
        {
            //string str = "update tblUserDetails set idcard='" + chk_idcard.Checked + "',certificate ='" + chk_certificate.Checked + "', " +
            //             "visitingCard='" + chk_visitingCard.Checked + "',ndaCopy='" + chk_ndaCopy.Checked + "',comments='" + txt_comments.Text + "', " +
            //             "childTestStatus='" + chk_childTest.Checked + "',childSessionStatus='" + chk_childSession.Checked + "',spouseTestStatus='" + chk_spouseTest.Checked + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) ";

            // new query for Level-1, Level-2 and Level-3 wise data capture.
            string str = "update tblUserDetails set idcard='" + chk_idcard.Checked + "',certificate ='" + chk_certificate.Checked + "', " +
                         "visitingCard='" + chk_visitingCard.Checked + "',ndaCopy='" + chk_ndaCopy.Checked + "',comments='" + txt_comments.Text + "', " +
                         "childTestStatus='" + chk_childTest.Checked + "',childSessionStatus='" + chk_childSession.Checked + "',spouseTestStatus='" + chk_spouseTest.Checked + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) ";

            if (ddl_shadowSession.SelectedValue != "")
            {
                str += ", shadowSession = '" + ddl_shadowSession.SelectedValue + "' ";
            }


            if (ddl_trainingBatch.SelectedIndex != 0)
            {
                //str += ", batchId = '" + ddl_trainingBatch.SelectedValue + "'";
                //str = "update tblUserMaster set cdfId = '" + ddl_trainingBatch.SelectedValue + "'+ where uId=" + c_id + "";

                // new query for Level-1, Level-2 and Level-3 wise data capture.
                if (lbl_cdflevel.Text == "1")
                {
                    str += ", batchId = '" + ddl_trainingBatch.SelectedValue + "'";
                }
                if (lbl_cdflevel.Text == "2")
                {
                    str += ", batchId_L2 = '" + ddl_trainingBatch.SelectedValue + "'";
                }
                if (lbl_cdflevel.Text == "3")
                {
                    str += ", batchId_L3 = '" + ddl_trainingBatch.SelectedValue + "'";
                }
                if (lbl_cdflevel.Text == "4")
                {
                    str += ", batchId_L4 = '" + ddl_trainingBatch.SelectedValue + "'";
                }
            }

            str += " where uId=" + c_id + "";
            int i = dbContext.ExecNonQuery(str);
            
            if (ddl_trainingBatch.SelectedIndex != 0)
            {
                // ds.Tables[0].Rows[0]["cdfId"].ToString()
                //if (lbl_cdflevel.Text == "1")
                if (ViewState["CDFID"].ToString() == "")
                {
                    str = "";
                    str = "update tblUserMaster set " +
                          "cdfId = (select C.batchName + case when COUNT(A.uId) + 1 <= 9 then '0' + Convert(varchar(20), COUNT(A.uId) + 1) when COUNT(A.uId) + 1 > 9 then Convert(varchar(20), COUNT(A.uId) + 1) END as CDFCount from tblUserMaster as A " +
                          "inner join tblUserDetails as B on A.uId = B.uId " +
                          "inner join tblTrainingBatch as C on B.batchId = C.id where C.id = '" + ddl_trainingBatch.SelectedValue + "'" +
                          " group by batchName),modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))";
                          str += " where uId=" + c_id + "";

                    int j = dbContext.ExecNonQuery(str);
                    if (j > 0)
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_testReassing_Click(object sender, EventArgs e)
    {
        string ec_id = dbContext.EncryptData(c_id.ToString());
        Response.Redirect("test-reassign.aspx?id=" + ec_id + "", false);
    }
    protected void btn_profileDisplayApproval_Click(object sender, EventArgs e)
    {
        string queryprofileDisplayApproval;

        if (lbl_profileDisplayApproval.Text == "ACTIVE")
        {
            queryprofileDisplayApproval = "update tblUserMaster set profileDisplayApproval = 0,modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryprofileDisplayApproval);
        }
        else if (lbl_profileDisplayApproval.Text == "DEACTIVE")
        {
            //update User status 
            queryprofileDisplayApproval = "update tblUserMaster set profileDisplayApproval =1,modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryprofileDisplayApproval);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void btn_profileUpdateApproval_Click(object sender, EventArgs e)
    {
        string queryprofileUpdateApproval;

        if (lbl_profileUpdateApproval.Text == "ACTIVE")
        {
            queryprofileUpdateApproval = "update tblUserMaster set profileUpdateApproval = 0,profileDisplayApproval = 0,modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))  where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryprofileUpdateApproval);
            //btn_CandidateReportDownloadApproval.CssClass = "btn btn-danger btn-sm btn-block";
        }
        else if (lbl_profileUpdateApproval.Text == "DEACTIVE")
        {
            //update User status 
            queryprofileUpdateApproval = "update tblUserMaster set profileUpdateApproval = 1,profileDisplayApproval = 1,modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + c_id + "'";
            int j = dbContext.ExecNonQuery(queryprofileUpdateApproval);
            //btn_CandidateReportDownloadApproval.CssClass = "btn btn-success btn-sm btn-block";
        }
        Response.Redirect(Request.RawUrl);
    }
    public void read_data_by_id()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["career_portalConnectionString_simsr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string strcmd = "sp_Read_Mentor";
            SqlCommand cmd = new SqlCommand(strcmd, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uId", c_id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                {
                    userStatus = dr.GetValue(7).ToString();
                }
            }
            else
            {
                get_count();
            }
        }
    }
    private void get_count()
    {
        string s = "select count(uId) from tblUserMaster where uId=" + c_id;
        int i = dbContext_simsr.ExecScal(s, c_id);  // by uId
        if (i == 0)
        {
            int j = dbContext_simsr.ExecNonQuery("sp_Add_Mentor", c_id, 2, ViewState["FirstName"].ToString(), ViewState["LastName"].ToString(), lbl_email.Text, lbl_contact.Text, "ACTIVE");
            //lblCandidate_Report_Download_Approval.Text = "APPROVED";
            //btn_CandidateReportDownloadApproval.Text = "DEACTIVE";
            //userStatus = "";
        }
    }
    //protected void btn_CandidateReportDownloadApproval_Click(object sender, EventArgs e)
    //{
    //    read_data_by_id();
    //    string queryCandidateReportDownloadApproval = "sp_Update_Mentor";
    //    if (btn_CandidateReportDownloadApproval.Text == "DEACTIVE")
    //    {
    //        int i = dbContext_simsr.ExecNonQuery(queryCandidateReportDownloadApproval, c_id, "DEACTIVE");
    //        lblCandidate_Report_Download_Approval.Text = "APPROVED";
    //        btn_CandidateReportDownloadApproval.Text = "DEACTIVE";
    //    }
    //    if (btn_CandidateReportDownloadApproval.Text == "ACTIVE")
    //    {
    //        int i = dbContext_simsr.ExecNonQuery(queryCandidateReportDownloadApproval, c_id, "DEACTIVE");
    //        lblCandidate_Report_Download_Approval.Text = "DENIED";
    //        btn_CandidateReportDownloadApproval.Text = "ACTIVE";
    //    }
    //}
    void readBankDetailsData()
    {
        {
            try
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Get all user details 
                    string strcmd = "SELECT accountHolderName,accountNumber,bankName,branchName,ifscNo from tblUserBankDetails WHERE uId = " + id + "";

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        {
                            lbl_accountHolderName.Text = dr["accountHolderName"].ToString();
                            lbl_accountNumber.Text = dr["accountNumber"].ToString();
                            lbl_bankName.Text = dr["bankName"].ToString();
                            lbl_branchName.Text = dr["branchName"].ToString();
                            lbl_ifscNo.Text = dr["ifscNo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }



    //protected void btn_nda_download(object sender, EventArgs e)
    //{
    //    //create NDA Form
    //    load_nda();
    //    //Display NDA documnet in Pdf format
    //    //Response.Redirect("../doc/Admin-NDA/" + hid_fileName.Value.ToString());

    //    Response.Redirect("~/Admin/download.aspx?filename=" + ConfigurationManager.AppSettings["adminNDAPath"].ToString() + hid_fileName.Value.ToString() + "");
    //}

    protected void FindCoordinates(object sender, EventArgs e)
    {
        string url = "http://maps.google.com/maps/api/geocode/xml?address=" + txtLocation.Text + "&sensor=false";
        WebRequest request = WebRequest.Create(url);
        try
        {
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    DataTable dtCoordinates = new DataTable();
                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Latitude",typeof(string)),
                        new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    }
                    GridView1.DataSource = dtCoordinates;
                    GridView1.DataBind();
                    int id = Convert.ToInt32(GridView1.Rows[0].Cells[0].Text);
                    string latitude = GridView1.Rows[0].Cells[2].Text.ToString();
                    string longitude = GridView1.Rows[0].Cells[3].Text.ToString();

                    txtLatitude.Text = latitude;
                    txtLongitude.Text = longitude;


                    btnSearch.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            divErr.Visible = true;
            divErr.Attributes["class"] = "alert alert-danger";
            divErr.InnerText = "Please enter valid address (without flat no.) or clicked multipe times";
        }
    }
    protected void btnSaveLatLong_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "update tblUserDetails set latitude='" + txtLatitude.Text + "',longitude ='" + txtLongitude.Text + "', " +
                         " modifiedBy='" + Session["adminuser_id"] + "',dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId=" + c_id + "";
            int i = dbContext.ExecNonQuery(str);
            divErr.Visible = true;
            divErr.Attributes["class"] = "success alert-success";
            divErr.InnerText = "Details has been successfully updated";
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            divErr.Visible = true;
            divErr.Attributes["class"] = "alert alert-danger";
            divErr.InnerText = "Something went wrong!";
        }
    }
    protected void btnDheyaUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "update tblUserDetails set dheyaUpdates='" + txtDheyaUpdate.Text + "'  where uId=" + c_id + "";
            int i = dbContext.ExecNonQuery(str);
            divMsg.Visible = true;
            divMsg.Attributes["class"] = "success alert-success";
            divMsg.InnerText = "Data has been successfully updated";
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            divMsg.Visible = true;
            divMsg.Attributes["class"] = "alert alert-danger";
            divMsg.InnerText = "Something went wrong!";
        }
    }

    //Send Mail with File Attachment
    public Boolean sendEmailWithAttachment(string to, string cc, string bcc, string subject, string body, string attachmentFile)
    {
        try
        {
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                if (to != null)
                    mailMessage.To.Add(new MailAddress(to));
                if (cc != null)
                    mailMessage.CC.Add(new MailAddress(cc));
                if (bcc != null)
                    mailMessage.Bcc.Add(new MailAddress(bcc));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                Attachment data = new Attachment(Server.MapPath(attachmentFile), MediaTypeNames.Application.Octet);
                mailMessage.Attachments.Add(data);

                smtp.Send(mailMessage);
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return false;
        }
    }


    #region NDA Ducument
    // Changes done By Dhananjay Korde 8-12-2018. Purpose Agreed Amount on NDA Document.
    //Create to PFD report code
    private void load_nda()
    {
        try
        {
            id = Convert.ToInt32(Request.QueryString["id"]);

            // Added new fuction to Read newly added tblAgreedAmount data for accessing CDF Agreed Amount.
            //int id = Convert.ToInt32(Session["uid"].ToString());
            int agreedAmount = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("select uId,agreedAmount,createdDate from tblAgreedAmount where uId=" + id, connection);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    agreedAmount = Convert.ToInt32(sdr.GetValue(1).ToString());

                }
            }

            // Orginal PDF templete pdf file
            string oldfile_name = "NDA.pdf";
            string name = lbl_name.Text;
            string address = lbl_address.Text;

            // create file name as "username + user_id + _NDA.pdf" this format
            hid_fileName.Value = name + id + "_NDA.pdf";

            //set Template folder path for set a pdf file
            PdfReader pdfReader = new PdfReader(Server.MapPath("~") + "/Templates/" + oldfile_name.ToString());
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(Server.MapPath("~") + "/doc/Admin-NDA/" + hid_fileName.Value.ToString(), FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            // set form pdfFormFields
            // The fill the form details 
            pdfFormFields.SetField("txt_date", DateTime.Now.ToString("dd-MMM-yyyy"));

            //set the user name, string one, address, string two as s string
            string s = name + " " + ConfigurationManager.AppSettings["string_one"].ToString() + " " + address + " " + ConfigurationManager.AppSettings["string_two"].ToString();

            pdfFormFields.SetField("txt_name_add", s);
            pdfFormFields.SetField("txt_name", name);
            //pdfFormFields.SetField("txt_name3", name);
            pdfFormFields.SetField("txt_footname", name);
            pdfFormFields.SetField("txt_agreedAmount", agreedAmount.ToString());

            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private void Save_Agreed_Amount()
    {
        try
        {
            //string queryUpdate = "insert into tblAgreedAmount (uId, agreedAmount, createdDate, createdBy, comment) values ('" + c_id + "', '" + txt_agreed_amount.Text + "', (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')), '" + Session["adminuser_id"] + "','" + txt_comment.Text + "')";
            //int i = dbContext.ExecNonQuery(queryUpdate);

            int locked = 1;
            string queryUpdate = "sp_Insert_Agreed_Amount_locked";
            int agreed_amount = Convert.ToInt32(txt_Agreed_Amount_F.Text);
            int createdBy = Convert.ToInt32(Session["adminuser_id"]);
            int updatedBy = Convert.ToInt32(Session["adminuser_id"]);
            int i = dbContext.ExecNonQuery(queryUpdate, c_id, agreed_amount, locked, createdBy, updatedBy, txt_comment.Text);
            if (i == 1)
            {
                btnEdit.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_Agreed_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            string queryUpdate = "sp_Insert_Agreed_Amount";
            int agreed_amount = Convert.ToInt32(txt_agreed_amount.Text);
            int createdBy = Convert.ToInt32(Session["adminuser_id"]);
            int updatedBy = Convert.ToInt32(Session["adminuser_id"]);
            int i = dbContext.ExecNonQuery(queryUpdate, c_id, agreed_amount, createdBy, updatedBy, txt_comment.Text);
            Page_Load(sender, e);

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private void nda_download()
    {
        //create NDA Form
        load_nda();
        //Display NDA documnet in Pdf format
        Response.Redirect("~/Admin/download.aspx?filename=" + ConfigurationManager.AppSettings["adminNDAPath"].ToString() + hid_fileName.Value.ToString() + "");
    }
    protected void btnEdit_Click1(object sender, EventArgs e)
    {

    }
    protected void hl_nda_click(object sender, EventArgs e)
    {
        //Save_Agreed_Amount();
        nda_download();

    }
    protected void btnNoChange_Click(object sender, EventArgs e)
    {
        Save_Agreed_Amount();
        nda_download();
    }

    [WebMethod]
    public static string Get_Agreed_Amount()
    {
        int amount = 0;
        string comment = "";
        try
        {
            string c_id = System.Web.HttpContext.Current.Session["CID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("select uId,agreedAmount, comment  from tblAgreedAmount where uId=" + c_id, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        amount = Convert.ToInt32(dr["agreedAmount"]);
                        System.Web.HttpContext.Current.Session["AgreedAmount"] = amount;
                        comment = dr["comment"].ToString();
                        System.Web.HttpContext.Current.Session["COMMENT"] = comment;
                    }
                }
                return amount.ToString();
                //return comment.ToString();
            }

        }
        catch (Exception ex)
        {
            return amount.ToString();
            throw ex;
        }
    }

    #endregion NDA Ducument

    #region CDF Google Business Lising
    protected void btn_saveBusinessList_Click(object sender, EventArgs e)
    {
        try
        {
            if(ddl_GBLStatus.SelectedItem.Text=="--Select--")
            {
                
            }
            else
            {
                bool GBL = false;
                string queryUpdate = "sp_Save_CDF_Business";
                if (chk_GBL.Checked == true) { GBL = true; } else { GBL = false; }
                int createdBy = Convert.ToInt32(Session["adminuser_id"]);
                int updatedBy = Convert.ToInt32(Session["adminuser_id"]);
                int i = dbContext.ExecNonQuery(queryUpdate, c_id, GBL, ddl_GBLStatus.SelectedItem.Text, txt_StoreCode.Text, text_comment.Text, createdBy, updatedBy);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void ddl_GBLStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddl_GBLStatus.SelectedItem.Text== "Not Done")
        {
            chk_GBL.Checked = false;
            txt_StoreCode.Text = "";
        }
    }
    #endregion CDF Google Business Lising

    #region User Status
    protected void btn_Status_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Status.SelectedItem.Text == "--Select--")
            {

            }
            else
            {
                string queryUpdate = "update tblUserMaster set userStatus='" + ddl_Status.SelectedItem.Text + "', dateModified='" + System.DateTime.Now + "', modifiedBy='" + Convert.ToInt32(Session["adminuser_id"]) + "' where uId='" + c_id + "' ";
                int i = dbContext.ExecNonQuery(queryUpdate, c_id, ddl_Status.SelectedItem.Text, System.DateTime.Now, Convert.ToInt32(Session["adminuser_id"]));
                Page_Load(sender, e);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    #endregion User Status

    //protected void btn_Final_Agreed_Amount_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
    //        {
    //            connection.Open();
    //            // check table tblUserMaster email id is not exist 
    //            string str = "select * from tblAgreedAmount where uId=" + c_id;
    //            SqlCommand cmd1 = new SqlCommand(str, connection);
    //            SqlDataReader dr = cmd1.ExecuteReader();
    //            if (dr.HasRows)
    //            {
    //                dr.Read();
    //                //txt_agreedAmount.Text = dr["agreedAmount"].ToString();
    //                //txt_Comment_Final.Text= dr["comment"].ToString();
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(ex);
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod]




    //protected void ddl_cdfLevel_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string StrQuerytrainingBatch = "select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))  or (cdfLevel = '" + Convert.ToInt32(ddl_cdfLevel.SelectedValue) + "' or cdfLevel = null)  order by id desc";
    //    dbContext.BindDropDownlist(StrQuerytrainingBatch, ref ddl_trainingBatch);
    //}
    
}