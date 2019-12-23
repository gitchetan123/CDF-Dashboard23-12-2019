using log4net;
using System;
using System.Threading;
using System.Web.UI;
using System.Configuration;

public partial class candidate_Separate_personality_test_status : BaseClass
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    int c_id;
    int PDtestid = 10;

    int KYtestid = 9;

    //create a object db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string[] languages = ConfigurationManager.AppSettings["audioAvailable"].ToString().Split(',');
                bool lanavail = false;
                foreach (string item in languages)
                {
                    if (item.Equals(Thread.CurrentThread.CurrentUICulture.ToString()))
                    {
                        lanavail = true;
                    }
                }

                if (lanavail)
                {
                    if (Session["Audio"] == "true")
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }
                }
                else
                {
                    CheckBox1.Visible = false;
                    Session["Audio"] = "false";
                }

                if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    CheckBox1.Text = "तुम्हाला ऑडिओ बेस टेस्ट पाहिजे आहे का?";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    CheckBox1.Text = "क्या आप ऑडियो बेस परीक्षण चाहते हैं?";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "en")
                {
                    CheckBox1.Text = "Do you want audio base test";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    CheckBox1.Text = "શું તમે ઓડિયો બાસ પરીક્ષણ કરવા માંગો છો?";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "or")
                {
                    CheckBox1.Text = "Do you want audio base test";
                }
                else
                {
                    CheckBox1.Text = "Do you want audio base test";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Session.Clear();
                Response.Redirect("~/Default.aspx");
            }
        }

        ErrMsg.Text = "";
        string sts_pd = "", sts_ky = "";
        try
        {
            #region checking_session

            if (Session["uid"] != null)
                c_id = Convert.ToInt32(Session["uid"].ToString());
            else
            {
                Session.Clear();
                Response.Redirect("~/Default.aspx");
            }

            #endregion

            #region individual_Interest_test_sts

            //string strQuery = "SELECT count(id) FROM tblUserTestMaster WHERE batid=" + Session["batid"].ToString() + " and uId = " + c_id + " and testid=" + Itestid + " and testStatus='Complete' and factorStatus='Complete'";
            //int count = Convert.ToInt32(dbContext.ExecScal(strQuery));
            //if (count != 0)
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
            //    {
            //        sts_I.Text = "संपन्न";
            //    }
            //    else if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
            //    {
            //        sts_I.Text = "समाप्त ";
            //    }
            //    else
            //    {
            //        sts_I.Text = "Completed";
            //    }
            //    btn_Interest.ImageUrl = "~/images/TestImages/test-green.png";
            //    btn_Interest.ToolTip = "Completed";
            //    btn_Interest.Enabled = false;

            //    sts_interest = "done";
            //}

            #endregion

            #region individual_KY_test

            string strQuery = "SELECT count(id) FROM tblUserTestMaster WHERE batid=" + Session["batid"].ToString() + " and uId = " + c_id + " and testid=" + KYtestid + " and testStatus='Complete' and factorStatus='Complete'";
            int count = Convert.ToInt32(dbContext.ExecScal(strQuery));
            if (count != 0)
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    sts_KY.Text = "संपन्न";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    sts_KY.Text = "समाप्त ";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    sts_KY.Text = "પૂર્ણ";
                }
                else
                {
                    sts_KY.Text = "Completed";
                }
                btn_KY.ImageUrl = "~/images/TestImages/test-green.png";
                btn_KY.ToolTip = "Completed";
                btn_KY.Enabled = false;

                sts_ky = "done";
            }

            #endregion

            #region individual_PT_test_sts


            strQuery = "SELECT count(id) FROM tblUserTestMaster WHERE batid=" + Session["batid"].ToString() + " and uId = " + c_id + " and testid=" + PDtestid + " and testStatus='Complete' and factorStatus='Complete'";
            int countPD = Convert.ToInt32(dbContext.ExecScal(strQuery));
            if (countPD != 0)
            {

                if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    sts_PD.Text = "संपन्न";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    sts_PD.Text = "समाप्त ";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    sts_PD.Text = "પૂર્ણ";
                }
                   
                else
                {
                    sts_PD.Text = "Completed";
                }

                btn_PT.ImageUrl = "~/images/TestImages/test-green.png";
                btn_PT.ToolTip = "Completed";
                btn_PT.Enabled = false;

                sts_pd = "done";
            }
            #endregion


            //if (sts_interest == "done" && sts_pd == "done")
            //{
            //    Response.Redirect("all_test_complete.aspx", false);
            //}

            if (sts_ky == "done" && sts_pd == "done")
            {
                Response.Redirect("all_test_complete.aspx", false);
            }                                                                                                                                                                                                                                  

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void btn_PD_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("assessment.aspx");
    }
    protected void btn_interest_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("assessment.aspx");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            Session["Audio"] = "true";
        }
        else
        {
            Session["Audio"] = "false";
        }
    }
}