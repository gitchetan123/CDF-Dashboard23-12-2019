using System;
using System.Data;

public partial class Search_CareerDetails : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    db_context_career dbContext = new db_context_career();
    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if (Session["type"] == null)
        {
            this.MasterPageFile = "~/CDFMaster.master";
        }
        else
        {
            this.MasterPageFile = "~/Admin/admin-master.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string strcmd = "SELECT * FROM tbl_career_master Where ca_id = '" + Request.QueryString["id"].ToString() + "'";
                DataSet ds = dbContext.ExecDataSet(strcmd);
                lblCareerName.Text = ds.Tables[0].Rows[0][1].ToString();
                //lblCareername2.Text = ds.Tables[0].Rows[0][1].ToString();
                
                Label2.Text = ds.Tables[0].Rows[0][2].ToString();
                Label3.Text = ds.Tables[0].Rows[0][3].ToString();
                Label4.Text = ds.Tables[0].Rows[0][4].ToString();
                Label5.Text = ds.Tables[0].Rows[0][5].ToString();
                Label6.Text = ds.Tables[0].Rows[0][6].ToString();
                Label7.Text = ds.Tables[0].Rows[0][7].ToString();
                lbl_career_scope.Text = ds.Tables[0].Rows[0][17].ToString();

                if (ds.Tables[0].Rows[0][8].ToString() != "NULL")
                    Label8.Text = ", " + ds.Tables[0].Rows[0][8].ToString();
                if (ds.Tables[0].Rows[0][9].ToString() != "NULL")
                    Label9.Text = ", " + ds.Tables[0].Rows[0][9].ToString();
                if (ds.Tables[0].Rows[0][10].ToString() != "NULL")
                    Label10.Text = ", " + ds.Tables[0].Rows[0][10].ToString();
                //if (Session.Count > 0)
                //{
                //c_id = Convert.ToInt32(Session["c_id"].ToString());
                //string strcmd1 = "select c_id from tbl_candidate_top3 where c_id = " + c_id;
                //DataSet ds1 = clsdal.ExecDataSet(strcmd1);
                //if (ds1.Tables[0].Rows.Count > 0)
                //{
                strcmd = " SELECT A.[ca_id], [education1], [education2], [education3], [education4], [education5], [education6], [education7], [education8], [education9], [education10], [education11], [education12], [education13], [education14], [education15], [education16], [education17], [education18], [education19], [education20], [education21] ,";
                strcmd += "	[market_scope1], [market_scope2], [career_advancement1], [career_advancement2], [career_advancement2_1], [career_advancement3], [career_advancement3_1], [career_advancement4], [career_advancement4_1], [career_advancement5], [career_advancement5_1], [work_environment1], [work_environment2], [work_environment3], [work_environment4], [work_environment5], [work_environment6], [work_environment7], [work_environment8], [work_environment9], [work_environment10], [work_context1], [work_context2], [work_context3], [work_context4], [work_context5], [work_context6], [work_context7], [work_context8], [work_context9], [work_context10], [work_context11], [work_context12], [work_context13], [work_comm_demands1], [work_comm_demands2], [work_comm_demands3], [work_comm_demands4], [work_comm_demands5], [mental_abilities1], [mental_abilities2], [mental_abilities3], [mental_abilities4], [mental_abilities5], [mental_abilities6], [mental_abilities7], [mental_abilities8], [mental_abilities9], [mental_abilities10], [mental_abilities11], [mental_abilities12], [mental_abilities13], [mental_abilities14], [physical_abilities1], [physical_abilities2], [physical_abilities3], [physical_abilities4], [physical_abilities5], [physical_abilities6], [physical_abilities7] , ";
                strcmd += "	[job_focus1], [job_focus2], [job_focus3], [job_focus4], [job_focus5], [job_focus6], [job_focus7], [job_focus8] ";
                strcmd += "	FROM [tbl_career_education] A ,[tbl_career_details] B ,[tbl_career_job_focus] C";
                strcmd += "	WHERE A.ca_id = B.ca_id AND A.ca_id = C.ca_id AND A.ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);

                job_focus1Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus1"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus2"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus2"].ToString() != "NULL"))
                    job_focus2Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus2"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus3"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus3"].ToString() != "NULL"))
                    job_focus3Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus3"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus4"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus4"].ToString() != "NULL"))
                    job_focus4Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus4"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus5"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus5"].ToString() != "NULL"))
                    job_focus5Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus5"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus6"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus6"].ToString() != "NULL"))
                    job_focus6Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus6"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus7"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus7"].ToString() != "NULL"))
                    job_focus7Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus7"].ToString() + "</li><br>&nbsp;";

                if ((ds.Tables[0].Rows[0]["job_focus8"].ToString() != "") && (ds.Tables[0].Rows[0]["job_focus8"].ToString() != "NULL"))
                    job_focus8Label.Text = "<li>" + ds.Tables[0].Rows[0]["job_focus8"].ToString() + "</li><br>&nbsp;";

                career_advancement1Label.Text = ds.Tables[0].Rows[0]["career_advancement1"].ToString();

                strcmd = "SELECT A.co_id, A.course1 FROM tbl_course_master A, tbl_career_course_bridge B ";
                strcmd += " Where A.co_id = B.co_id AND B.ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);

                lblListOfCourses.Text = "";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    lblListOfCourses.Text += "<a Class='main4' href='CourseDetail.aspx?id=" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</a><br><br>";

                ds.Clear();
                strcmd = "SELECT * FROM tbl_career_master Where ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);
                //lblCareerName.Text = ds.Tables[0].Rows[0][1].ToString();           

                strcmd = " SELECT A.[ca_id], [education1], [education2], [education3], [education4], [education5], [education6], [education7], [education8], [education9], [education10], [education11], [education12], [education13], [education14], [education15], [education16], [education17], [education18], [education19], [education20], [education21] ,";
                strcmd += "	[market_scope1], [market_scope2], [career_advancement1], [career_advancement2], [career_advancement2_1], [career_advancement3], [career_advancement3_1], [career_advancement4], [career_advancement4_1], [career_advancement5], [career_advancement5_1], [work_environment1], [work_environment2], [work_environment3], [work_environment4], [work_environment5], [work_environment6], [work_environment7], [work_environment8], [work_environment9], [work_environment10], [work_context1], [work_context2], [work_context3], [work_context4], [work_context5], [work_context6], [work_context7], [work_context8], [work_context9], [work_context10], [work_context11], [work_context12], [work_context13], [work_comm_demands1], [work_comm_demands2], [work_comm_demands3], [work_comm_demands4], [work_comm_demands5], [mental_abilities1], [mental_abilities2], [mental_abilities3], [mental_abilities4], [mental_abilities5], [mental_abilities6], [mental_abilities7], [mental_abilities8], [mental_abilities9], [mental_abilities10], [mental_abilities11], [mental_abilities12], [mental_abilities13], [mental_abilities14], [physical_abilities1], [physical_abilities2], [physical_abilities3], [physical_abilities4], [physical_abilities5], [physical_abilities6], [physical_abilities7] , ";
                strcmd += "	[job_focus1], [job_focus2], [job_focus3], [job_focus4], [job_focus5], [job_focus6], [job_focus7], [job_focus8] ";
                strcmd += "	FROM [tbl_career_education] A ,[tbl_career_details] B ,[tbl_career_job_focus] C";
                strcmd += "	WHERE A.ca_id = B.ca_id AND A.ca_id = C.ca_id AND A.ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);

                work_environment1Label.Text = ds.Tables[0].Rows[0]["work_environment1"].ToString();
                work_environment2Label.Text = ds.Tables[0].Rows[0]["work_environment2"].ToString();
                work_environment3Label.Text = ds.Tables[0].Rows[0]["work_environment3"].ToString();
                work_environment4Label.Text = ds.Tables[0].Rows[0]["work_environment4"].ToString();
                work_environment5Label.Text = ds.Tables[0].Rows[0]["work_environment5"].ToString();
                work_environment6Label.Text = ds.Tables[0].Rows[0]["work_environment6"].ToString();
                work_environment7Label.Text = ds.Tables[0].Rows[0]["work_environment7"].ToString();
                work_environment8Label.Text = ds.Tables[0].Rows[0]["work_environment8"].ToString();
                work_environment9Label.Text = ds.Tables[0].Rows[0]["work_environment9"].ToString();
                work_environment10Label.Text = ds.Tables[0].Rows[0]["work_environment10"].ToString();

                work_context1Label.Text = ds.Tables[0].Rows[0]["work_context1"].ToString();
                work_context2Label.Text = ds.Tables[0].Rows[0]["work_context2"].ToString();
                work_context3Label.Text = ds.Tables[0].Rows[0]["work_context3"].ToString();
                work_context4Label.Text = ds.Tables[0].Rows[0]["work_context4"].ToString();
                work_context5Label.Text = ds.Tables[0].Rows[0]["work_context5"].ToString();
                work_context6Label.Text = ds.Tables[0].Rows[0]["work_context6"].ToString();
                work_context7Label.Text = ds.Tables[0].Rows[0]["work_context7"].ToString();
                work_context8Label.Text = ds.Tables[0].Rows[0]["work_context8"].ToString();
                work_context9Label.Text = ds.Tables[0].Rows[0]["work_context9"].ToString();
                work_context10Label.Text = ds.Tables[0].Rows[0]["work_context10"].ToString();
                work_context11Label.Text = ds.Tables[0].Rows[0]["work_context11"].ToString();
                work_context12Label.Text = ds.Tables[0].Rows[0]["work_context12"].ToString();
                work_context13Label.Text = ds.Tables[0].Rows[0]["work_context13"].ToString();

                ds.Clear();
                strcmd = "SELECT * FROM tbl_career_master Where ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);
                //lblCareerName.Text = ds.Tables[0].Rows[0][1].ToString();

                strcmd = " SELECT A.[ca_id], [education1], [education2], [education3], [education4], [education5], [education6], [education7], [education8], [education9], [education10], [education11], [education12], [education13], [education14], [education15], [education16], [education17], [education18], [education19], [education20], [education21] ,";
                strcmd += "	[market_scope1], [market_scope2], [career_advancement1], [career_advancement2], [career_advancement2_1], [career_advancement3], [career_advancement3_1], [career_advancement4], [career_advancement4_1], [career_advancement5], [career_advancement5_1], [work_environment1], [work_environment2], [work_environment3], [work_environment4], [work_environment5], [work_environment6], [work_environment7], [work_environment8], [work_environment9], [work_environment10], [work_context1], [work_context2], [work_context3], [work_context4], [work_context5], [work_context6], [work_context7], [work_context8], [work_context9], [work_context10], [work_context11], [work_context12], [work_context13], [work_comm_demands1], [work_comm_demands2], [work_comm_demands3], [work_comm_demands4], [work_comm_demands5], [mental_abilities1], [mental_abilities2], [mental_abilities3], [mental_abilities4], [mental_abilities5], [mental_abilities6], [mental_abilities7], [mental_abilities8], [mental_abilities9], [mental_abilities10], [mental_abilities11], [mental_abilities12], [mental_abilities13], [mental_abilities14], [physical_abilities1], [physical_abilities2], [physical_abilities3], [physical_abilities4], [physical_abilities5], [physical_abilities6], [physical_abilities7] , ";
                strcmd += "	[job_focus1], [job_focus2], [job_focus3], [job_focus4], [job_focus5], [job_focus6], [job_focus7], [job_focus8] ";
                strcmd += "	FROM [tbl_career_education] A ,[tbl_career_details] B ,[tbl_career_job_focus] C";
                strcmd += "	WHERE A.ca_id = B.ca_id AND A.ca_id = C.ca_id AND A.ca_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = dbContext.ExecDataSet(strcmd);

                work_comm_demands1Label.Text = ds.Tables[0].Rows[0]["work_comm_demands1"].ToString();
                work_comm_demands2Label.Text = ds.Tables[0].Rows[0]["work_comm_demands2"].ToString();
                work_comm_demands3Label.Text = ds.Tables[0].Rows[0]["work_comm_demands3"].ToString();
                work_comm_demands4Label.Text = ds.Tables[0].Rows[0]["work_comm_demands4"].ToString();
                work_comm_demands5Label.Text = ds.Tables[0].Rows[0]["work_comm_demands5"].ToString();

                mental_abilities1Label.Text = ds.Tables[0].Rows[0]["mental_abilities1"].ToString();
                mental_abilities2Label.Text = ds.Tables[0].Rows[0]["mental_abilities2"].ToString();
                mental_abilities3Label.Text = ds.Tables[0].Rows[0]["mental_abilities3"].ToString();
                mental_abilities4Label.Text = ds.Tables[0].Rows[0]["mental_abilities4"].ToString();
                mental_abilities5Label.Text = ds.Tables[0].Rows[0]["mental_abilities5"].ToString();
                mental_abilities6Label.Text = ds.Tables[0].Rows[0]["mental_abilities6"].ToString();
                mental_abilities7Label.Text = ds.Tables[0].Rows[0]["mental_abilities7"].ToString();
                mental_abilities8Label.Text = ds.Tables[0].Rows[0]["mental_abilities8"].ToString();
                mental_abilities9Label.Text = ds.Tables[0].Rows[0]["mental_abilities9"].ToString();
                mental_abilities10Label.Text = ds.Tables[0].Rows[0]["mental_abilities10"].ToString();
                mental_abilities11Label.Text = ds.Tables[0].Rows[0]["mental_abilities11"].ToString();
                mental_abilities12Label.Text = ds.Tables[0].Rows[0]["mental_abilities12"].ToString();
                mental_abilities13Label.Text = ds.Tables[0].Rows[0]["mental_abilities13"].ToString();
                mental_abilities14Label.Text = ds.Tables[0].Rows[0]["mental_abilities14"].ToString();

                physical_abilities1Label.Text = ds.Tables[0].Rows[0]["physical_abilities1"].ToString();
                physical_abilities2Label.Text = ds.Tables[0].Rows[0]["physical_abilities2"].ToString();
                physical_abilities3Label.Text = ds.Tables[0].Rows[0]["physical_abilities3"].ToString();
                physical_abilities4Label.Text = ds.Tables[0].Rows[0]["physical_abilities4"].ToString();
                physical_abilities5Label.Text = ds.Tables[0].Rows[0]["physical_abilities5"].ToString();
                physical_abilities6Label.Text = ds.Tables[0].Rows[0]["physical_abilities6"].ToString();
                physical_abilities7Label.Text = ds.Tables[0].Rows[0]["physical_abilities7"].ToString();

                // New Fields Famous Personalities and Useful Links 
                strcmd = "select * from tbl_career_famousPersonalities Where ca_id = '" + Request.QueryString["id"].ToString() + "'";
                DataSet dso = dbContext.ExecDataSet(strcmd);
                int p = dso.Tables[0].Rows.Count;
                if (p == 0)
                {
                    lbl_FamousPersonalities.Text = "no data available...";
                }
                else
                {
                    DataList_famousPersonalities.DataSource = dso;
                    DataList_famousPersonalities.DataBind();
                }
                strcmd = "select * from tbl_career_usefulLinks Where ca_id = '" + Request.QueryString["id"].ToString() + "'";
                DataSet dso2 = dbContext.ExecDataSet(strcmd);
                int p2 = dso2.Tables[0].Rows.Count;
                if (p2 == 0)
                {
                    lbl_UsefulLinks.Text = "no data available...";
                }
                else
                {
                    DataList_usefulLinks.DataSource = dso2;
                    DataList_usefulLinks.DataBind();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

}