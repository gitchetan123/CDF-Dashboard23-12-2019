using System;
using System.Data;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Configuration;

public partial class PD_Test_PD_test_page : BaseClass
{

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context class for database connecton and database related operation
    db_context dbContext = new db_context();
    int testid = 10;
    int total_answers;
    int lang = 1;
    DataSet ds;


    protected void Page_Load(object sender, EventArgs e)
    {
        //string usertype = "";

        try
        {

            if (!IsPostBack)
            {
                lblTestNo.Text = "2";
                Hbatid.Value = Session["batid"].ToString();
                Hcid.Value = Session["uid"].ToString();
                Hname.Value = Session["userName"].ToString();
                Hqid.Value = (Convert.ToInt32(Session["qid"])).ToString();

                if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    lang = 3;
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    lang = 2;
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    lang = 2;
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    lang = 4;
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }
        try
        {
            if (Session.Count > 0)
            {

                string QueryData = "select count(c_id) as total_answers from tblPersonalityCandAnswers where c_id = " + Hcid.Value + " and batid=" + Hbatid.Value + "";
                ds = dbContext.ExecDataSet(QueryData);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    total_answers = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                ds.Clear();
                ds.Dispose();


                // if candidate is giving the test for the first time
                if (total_answers == 0)
                {
                    if (Convert.ToUInt32(Hqid.Value.ToString()) == 0)
                    {
                        Hqid.Value = "1";
                    }
                }
                else
                {
                    // if session expires or candidate log out then resume the test when he log in again 
                    if (total_answers < 24)
                    {
                        QueryData = "";
                        QueryData = "select q_id from tblPersonalityCandAnswers where c_id= " + Hcid.Value + "  and batid=" + Hbatid.Value + " order by q_id desc";
                        ds = dbContext.ExecDataSet(QueryData);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            // HwordId.Value = (Convert.ToInt32(ds.Tables[0].Rows[0][10]) + 4).ToString();
                            Hqid.Value = (Convert.ToInt32(ds.Tables[0].Rows[0][0]) + 1).ToString();
                        }
                        ds.Clear();
                        ds.Dispose();

                    }
                    else
                    {
                        dbContext.updatePDTestStatus(Convert.ToInt32(Hcid.Value), Convert.ToInt32(Hbatid.Value), testid);
                        Response.Redirect("PD_test_complete.aspx", false);
                    }
                }

                if (Convert.ToInt32(Hqid.Value.ToString()) > 24)
                {
                    dbContext.updatePDTestStatus(Convert.ToInt32(Hcid.Value), Convert.ToInt32(Hbatid.Value), testid);
                    Response.Redirect("PD_test_complete.aspx", false);
                }
                else
                {

                    lblQuestionNo.Text = Hqid.Value.ToString();

                    //below to Audio Test 

                    if (Session["Audio"] == "true")
                    {
                        divAudio.Visible = true;
                       
                        audioControl.Attributes["src"] = "../" + ConfigurationManager.AppSettings["audioFolderName"].ToString() + "/pd-test/" + Thread.CurrentThread.CurrentUICulture.ToString() + "/" + lblQuestionNo.Text + ConfigurationManager.AppSettings["fileExtension"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "playAudio()", true);
                    }
                    else
                    {
                        divAudio.Visible = false;
                    }


                    QueryData = "";
                    QueryData = "select qno,questext,m,l from tblPersonalityQuestions where q_id=" + Hqid.Value + " and langid=" + lang + "";
                    DataSet DsPDDetails = dbContext.ExecDataSet(QueryData);


                    if (DsPDDetails.Tables[0].Rows.Count > 0)
                    {
                        lblword.Text = DsPDDetails.Tables[0].Rows[0][1].ToString();
                        lblword1.Text = DsPDDetails.Tables[0].Rows[1][1].ToString();
                        lblword2.Text = DsPDDetails.Tables[0].Rows[2][1].ToString();
                        lblword3.Text = DsPDDetails.Tables[0].Rows[3][1].ToString();

                        RadioButtonList1.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[0][2].ToString(), DsPDDetails.Tables[0].Rows[0][0].ToString()));
                        RadioButtonList1.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[1][2].ToString(), DsPDDetails.Tables[0].Rows[1][0].ToString()));
                        RadioButtonList1.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[2][2].ToString(), DsPDDetails.Tables[0].Rows[2][0].ToString()));
                        RadioButtonList1.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[3][2].ToString(), DsPDDetails.Tables[0].Rows[3][0].ToString()));

                        RadioButtonList2.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[0][3].ToString(), DsPDDetails.Tables[0].Rows[0][0].ToString()));
                        RadioButtonList2.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[1][3].ToString(), DsPDDetails.Tables[0].Rows[1][0].ToString()));
                        RadioButtonList2.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[2][3].ToString(), DsPDDetails.Tables[0].Rows[2][0].ToString()));
                        RadioButtonList2.Items.Add(new ListItem(DsPDDetails.Tables[0].Rows[3][3].ToString(), DsPDDetails.Tables[0].Rows[3][0].ToString()));
                    }
                    DsPDDetails.Clear();
                    DsPDDetails.Dispose();
                }

            }
            if (Session.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);

        }

    }
    protected void btnGiveTest_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {

                string QueryData = "select count(c_id) as total_answers from tblPersonalityCandAnswers where c_id = " + Hcid.Value + " and batid=" + Hbatid.Value + "";
                ds = dbContext.ExecDataSet(QueryData);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    total_answers = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                ds.Clear();
                ds.Dispose();

                if (total_answers < 24)
                {
                    // To get the code of the word entered as most

                    string s = " insert into tblPersonalityCandAnswers ( c_id,	q_id, most_qno,	least_qno,	most_code,	least_code,	most_status,	least_status,	batid) "
                    + " values (" + Hcid.Value + "," + Hqid.Value + "," + RadioButtonList1.SelectedValue + "," + RadioButtonList2.SelectedValue + ",'" + RadioButtonList1.SelectedItem.ToString() + "','" + RadioButtonList2.SelectedItem.ToString() + "','A','A'," + Hbatid.Value + ")";
                    int i = dbContext.ExecNonQuery(s);
                    if (i > 0)
                    {
                        //increment the word count and redirect to next question

                        Hqid.Value = (Convert.ToUInt32(Hqid.Value.ToString()) + 1).ToString();


                        if (Convert.ToUInt32(Hqid.Value.ToString()) > 24)
                        {
                            dbContext.updatePDTestStatus(Convert.ToInt32(Hcid.Value), Convert.ToInt32(Hbatid.Value), testid);
                            Response.Redirect("PD_test_complete.aspx", false);
                        }
                        else
                        {
                            Session["qid"] = Hqid.Value;
                            Response.Redirect("PD_test_page.aspx", false);
                        }

                    }
                    else
                    {
                        Label1.Visible = true;
                        //Label1.Text = "Error.please try again";
                        if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                        {
                            Label1.Text = "Error.please try again";
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                        {
                            Label1.Text = "त्रुटि. कृपया पुन: प्रयास करें.";
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.ToString() == "en")
                        {
                            Label1.Text = "Error.please try again";
                        }
                        else 
                        {
                            Label1.Text = "Error.please try again";
                        }
                    }

                    //set all session again

                    Session["c_id"] = Hcid.Value;
                    Session["qid"] = Hqid.Value;

                }
                else
                {
                    dbContext.updatePDTestStatus(Convert.ToInt32(Hcid.Value), Convert.ToInt32(Hbatid.Value), testid);
                    Response.Redirect("PD_test_complete.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
    }




}