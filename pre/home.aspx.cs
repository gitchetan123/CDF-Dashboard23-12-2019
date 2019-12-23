
//********************************************************************************************
//PageName        : Home Page  
//Description     : This page is home page
//AddedBy         : Nitish                   AddedOn   : 05/10/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          :
//*******************************************************************************************

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Home_page : System.Web.UI.Page
{
    //create a object Db_context class for database connecton and database related operation
    db_context dc = new db_context();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    dal_simsr clsdal = new dal_simsr();
    db_Xaction clsdb_Xaction = new db_Xaction();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check sessions of user
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                div_msg.Visible = false;
                bindgrid();

                //profile_conditions_pdf();
                RowProfile.Visible = false;

                //Check if test is completed or not for respective user
                string query = "SELECT COUNT(id) FROM tblUserProductMaster where uId='" + Session["uid"].ToString() + "' and prodid = " + 7 + " and teststatus = 'Complete'";

                    int count = Convert.ToInt32((dc.ExecScal(query)));
                    if (count == 0)
                    {
                        test_status.Text = "No";
                        hiddendiv1.Visible = true;
                    }
                    else
                    {
                        test_status.Text = "Yes";
                        hiddendiv1.Visible = false;
                        // Response.Redirect("~/pre/test.aspx",false);
                    }
                // dr_cdf_details.Close();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Check payment status of respective user from tblPayment table
                    string query_paystatus = "Select status,prodid from tblPayment where uId='" + Session["uid"].ToString() + "' and prodid=" + 7 + "";
                    SqlCommand cmd = new SqlCommand(query_paystatus, connection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    //Check if table has rows for required query
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lbl_payStatus.Text = "Payment Done";
                    }
                    else
                    {
                        lbl_payStatus.Text = "Payment Pending";
                    }
                }             
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    //CDF record count
    private void bindgrid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string strcmd = "select B.name 'City',A.location 'Location',A.date 'Date' From tblTrainingBatch as A Inner Join tblCitiesMaster as B on A.cityId=B.id where A.Date > '" + DateTime.Now + "'";
                SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                  
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-warning";
                    div_msg.InnerText = "Currently dose not have any CDF training batch";                   
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //div_Error.Visible = true;
            //div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }

    private void profile_conditions_pdf()
    {
        try
        {
            int i, BlackM, BlackL, BlueM, BlueL, RedM, RedL, GreenM;
            int GreenL, Hole, DiffB, DiffR, DiffBl, DiffG;
            bool T1, T2, T3, TD1, TD2, TD3;
            int candId = Convert.ToInt32(Session["uid"].ToString());
            string aa = "Profile Analysis";
            string profile = "";

            i = BlackM = BlackL = BlueM = BlueL = RedM = RedL = GreenM = 0;
            GreenL = Hole = DiffB = DiffR = DiffBl = DiffG = 0;

            string sqlquery = "select * from Cand_PD_Details where c_id ='" + candId + "'";
            DataSet ds_d = clsdal.ExecDataSet(sqlquery);
            int n = ds_d.Tables[0].Rows.Count;
            if (n == 24)
            {
                //candidate_name = Convert.ToString(Session["cand_name"]);
                bool I;
                //lblname.Text = candidate_name;
                int id = clsdb_Xaction.get_values(candId);
                clsdb_Xaction.set_values();
                BlueM = clsdb_Xaction.DM;
                BlueL = clsdb_Xaction.DL;
                RedM = clsdb_Xaction.IM;
                RedL = clsdb_Xaction.IL;
                BlackM = clsdb_Xaction.SM;
                BlackL = clsdb_Xaction.SL;
                GreenM = clsdb_Xaction.CM;
                GreenL = clsdb_Xaction.CL;

                DiffB = clsdb_Xaction.DD;
                DiffR = clsdb_Xaction.ID;
                DiffBl = clsdb_Xaction.SD;
                DiffG = clsdb_Xaction.CD;

                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1 && DiffBl <= -1 && DiffG <= -1))
                {
                    profile = "You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ";
                }

                // 2. I is Greater than all and in positive region
                if ((DiffB <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1) && (DiffG <= -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'High-I'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    { SConditions = true; }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations.";
                    }
                }

                // 3. S is Greater than all and in positive region
                if ((DiffB <= -1) && (DiffR <= -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= -1))
                {
                    profile = "\t You show high degree of patience, calmness and gentle openness. You are generally amiable and warm-hearted, being sympathetic to others' points of view, and valuing positive interaction with others. You are not outgoing by nature; however, rely on other, more assertive, people to take the lead.";
                }

                // 4. C is Greater than all and in positive region
                if ((DiffB <= -1) && (DiffR <= -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'High-C'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t Passive by nature, often reticent and aloof, you often tend to give an impression of coldness or disinterest. Much of this impassive style stems from your controlled nature, however, which makes you reluctant to reveal information about yourselves or your ideas unless absolutely necessary. In fact, you are often ambitious and have lofty goals, but your innate lack of assertiveness and unwillingness to become involved in confrontational situations makes it difficult for you to achieve these goals directly. Instead, you will tend to use existing structures and rules to accomplish your aims. You tend to follow rules, authority and logical argument to influence the actions of others.";
                    }
                }

                // 5. D is Lower than all and in negative region
                if ((DiffB <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1))
                {
                    profile = "\t You generally will try to achieve your ends through communication, using your persuasive abilities or your powers of rational discussion.You are not an ambitious type of person. You rarely set distinct goals for yourselves in life, but prefer instead simply to build strong relationships with others and pursue your personal interests or pastimes. You work particularly well as part of a team or group, being both friendly and co-operative in style, and ready to accept others' ideas.";
                }

                // 6. I is lower than all and in negative region
                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'Low-I'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t You have a unique and not so common personality profile. The main distinguishing feature of your personality style is that you are a person who is shy and introverted  and work more around practicality and rational thought than emotional considerations, and you are generally reluctant to reveal information about themselves, their ideas or their feelings. In your case, more assertive and dominant behavior can be expected in antagonistic or difficult situations; while a more relaxed (but far less assertive) style can be anticipated in less pressurized circumstances.";
                    }
                }

                // 7. S is lower than all and in negative region
                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
                {

                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'Low-S'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t Speed of response and a sense of urgency are the defining characteristics of your behavior. Your approach will be rooted in a dynamic, impatient style. This is a relatively self-controlled and ambitious style, but you also possess effective social abilities that can be expected to come to the fore in informal, open situations. While ambition and assertiveness are important elements of your style, you have an awareness of the needs of others and a sense of order that make you far less impulsive and unpredictable than many similarly extrovert types. While you will wish to achieve success in your own right, you also understand that the needs of an organization will from time to time require that you suppress your own ambitions for the good of the team.";
                    }
                }

                // 8. C is lower than all and in negative region
                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= -1))
                {
                    profile = "\t As you display independent behavioral style, you are dynamic and direct. You have clear idea about your goals in life and you equally have the strength to achieve it. You have a more single-minded and stubborn approach. You display remarkable tenacity and determination that help you to reach your goal in life. Though you mix easily with strangers, and are unafraid to initiate social contact, you have both an assertive and a confident behavioral style. This helps you to deal directly fearlessly with most situations. While you will typically prefer to keep matters on an open and friendly level, you are quite capable of adopting a more determined or confrontational stance where a situation calls for it.";
                }

                #region Positive-DI_I>D
                // 9. D and I in positive region and I > D
                //if ((DiffB > -1 && DiffR > -1) && (DiffBl < -1 && DiffG < -1) && (DiffB < DiffR))        
                if ((DiffB > -1 && DiffR > -1) && (DiffBl <= -1 && DiffG <= -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'D and I in positive region and I > D'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }

                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t You are a highly assertive person, capable of both direct, dynamic action and charming sociability as a situation demands. In combination, these factors describe you as a person with clear goals in life with the determination and commitment to achieve them. You seek to maintain a position of dominance, not only in terms of personal authority and control, but also in a social sense –where you like to feel that you are not only respected by those working with you, but also genuinely liked.";
                    }
                }
                #endregion

                // 10. D and S in positive region and D > S
                //if ((DiffB < 10 && DiffB > -1) && (DiffR < -1 && DiffG < -1) && (DiffBl < 10 && DiffBl > -1) && (DiffB > DiffBl))
                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1 && DiffG <= -1) && (DiffBl <= 10 && DiffBl > -1))
                {
                    profile = "\t Your kind of profile is rather  rare. You have personality sets that are radically differing in values and motivations.  On one side you are forceful and assertive and on the other you are relaxed and calm. These  two factors are so opposing that together its  hard for them to effectively coexist in a single style.";
                }

                // 11. D and C in positive region and D < C
                //if ((DiffB < 10 && DiffB > -1) && (DiffR < -1) && (DiffBl < -1) && (DiffG < 10 && DiffG > -1) && (DiffB < DiffG))
                if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
                {


                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'D and C in positive region and D < C'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t You are a highly formal and structured individual with a forceful and blunt style. You believe in getting things right, and is rarely afraid to state your mind robustly and directly. You represent the least forthcoming in personal or emotional matters; and tend to be remote and somewhat isolated, preferring to keep your own counsel.";
                    }
                }

                // 12. I and S in positive region and I > S
                //if ((DiffB < -1 && DiffG < -1) && (DiffR < 10 && DiffR > -1) && (DiffBl < 10 && DiffBl > -1) && (DiffR > DiffBl))
                if ((DiffB <= -1 && DiffG <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'I and S in positive region and I > S'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    dr.Close();
                    dr.Dispose();

                    if (SConditions == true)
                    {
                        profile = "\t You are more oriented towards feelings and emotions than hard facts and practicalities. In combination, you are oriented towards personal matters and the understanding of other people. You are confident, warm and friendly, but nonetheless you incorporate a sympathetic ear for others and a readiness to help with others' problems where possible. You could be described as a person having 'Counselor Profile.' ";
                    }
                }
                // 13. I and C in positive region and I > C
                //if ((DiffB <= -1 && DiffBl <= -1) && (DiffR < 10 && DiffR > -1) && (DiffG < 10 && DiffG > -1) && (DiffR > DiffG))
                if ((DiffB <= -1 && DiffBl <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffG <= 10 && DiffG > -1))
                {
                    profile = "\t In relaxed, open and favorable situation, you display excitement, enjoyment and extrovert impulsiveness; on the other hand in more formal or structured circumstances, you are a detailed person who carefully follows rules and possess precision. The differences in approach between these two factors are resolved in an unusual approach. Normally, two or more high personality factors will tend to reinforce each other's common points, and blend to make up the entire style. ";
                }


                // 14. S and C High
                if ((DiffB <= -1 && DiffR <= -1) && (DiffG <= 10 && DiffG > -1) && (DiffBl <= 10 && DiffBl > -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'S and C High with C > S'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }

                    if (SConditions == true)
                    {
                        profile = "\t You are passive by nature, precise and a systematic  thinker. Intelectually sound you tend to focus on perfection and accuracy. A perfectionist by nature, you set high standards of quality and accuracy and strive to achieve them.  A process oriented individual, who prefers to design processes and set the rules and follow them too. Often seen as reticent and aloof, with your kind of approach you  give an impression of coldness or disinterest(which may always not be so). Much of your impassive style stems from your controlled nature, however, which makes you a reluctant communicatior. You speak only if necesary and only when it forms a part of your core interest of expertise.  In fact, you are surprisingly ambitious and has lofty goals.You  tend to use existing structures and rules to accomplish your aims. You are also extremely logical, structured and systematic in your approach towards work.  ";
                    }
                }

                //HD,HS,HC high DSC
                if ((DiffB <= 10 & DiffB > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1) && (DiffR <= -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'High DSC - C Highest'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        profile = dr.GetValue(1).ToString();
                    }
                    else
                    {
                        SConditions = true;
                    }
                    if (SConditions == true)
                    {
                        profile = "\t As a person you are highly analytical and relatively uncommunicative. You are basically practical in nature and rational in your thought process. Emotions don’t bother you much. You are generally secretive by nature and cautious in communication, rarely revealing much about yourselves than a bare minimum. In challenging and stressful situations, you display an assertive and forceful behaviour, whereas in easy environment you are much relaxed.  ";
                    }
                }

                //HI,HD,HC high DCI
                if ((DiffB <= 10 & DiffB > -1) && (DiffG <= 10 && DiffG > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'High DCI I>D>C'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        profile = dr.GetValue(1).ToString();
                    }
                    if (SConditions == true)
                    {
                        profile = "\t You are basically fast paced and have high sense of urgency. You are impatient with routines and love variety. You are self-confident and have challenging and competitive approach and at the same time you are socially poised and very outgoing by nature. Though you are extremely assertive and have high need to achieve success, you work in consensus and exhibit caution in dealing with situations. You normally do not show unpredictability in your approach.";
                    }
                }

                //HI,HD,HS high DSI
                if ((DiffB <= 10 & DiffB > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffR <= 10 && DiffR > -1) && (DiffG <= -1))
                {
                    profile = "\t As a person you are dynamic and direct and have an independent behavioral style. You have a clear idea about your goals in life and you equally have the strength to achieve it. You have a more single-minded and stubborn approach to situations. Your remarkable tenacity and determination help you to attain your goal in life. Though you mix easily with strangers, and are unafraid to initiate social contact, you have a powerful, persuasive, confident behavioral style. This helps you to deal directly and fearlessly with most situations. While you will typically prefer to keep matters on an open and friendly level, you are quite capable of adopting a more determined and confrontational stance where a situation calls for. ";
                }

                //HI,HS,HC high ISC
                if ((DiffG <= 10 & DiffG > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffR <= 10 && DiffR > -1) && (DiffB <= -1))
                {
                    bool SConditions = false;
                    string StrQuery = "";
                    StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tbl_sales_report_conditions WHERE report_type = 'Browser_Report' AND condition = 'High ISC with  C > S > I'";
                    DataSet ds4 = clsdb_Xaction.ExecDataSet(StrQuery);
                    SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
                    if (dr.HasRows)
                    {
                        //profile = dr.GetValue(1).ToString();
                        profile = ds4.Tables[0].Rows[0][1].ToString();
                    }
                    if (SConditions == true)
                    {
                        profile = "\t You are basically warm, friendly and outgoing by nature. You are patient and generally a good listener. You work cooperatively with others and you love team oriented environment. You tend to avoid individual risk and tend to accept others’ ideas. You are normally not very assertive and dominant type and thereby you prefer to achieve your ends through communication, using your persuasive abilities and rationality. You usually distribute responsibility and tend to concentrate particularly on the details of a task. You are not very ambitious kind and are happy in building strong social relationships. ";
                    }
                }

                // Basic Prosiles
                // flick - up 
                //if (T1 == true || T2 == true || T3 == true)
                //{
                //    profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ";
                //}
                //// flick - down
                //if (TD1 == true || TD2 == true || TD3 == true)
                //{
                //    profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ";
                //}
                // sweep - down
                if ((DiffB < 10 && DiffB >= -1) && (DiffR < 10 && DiffR >= -1) && (DiffBl > -11 && DiffBl <= -1) && (DiffG > -11 && DiffG <= -1))
                {
                    if ((DiffB < DiffR) && (DiffBl - DiffG > 4))
                    {
                        profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ";
                    }
                }
                // 8-5 Pattern
                if ((DiffB >= -4 && DiffB <= 2) && (DiffR >= -4 && DiffR <= 2) && (DiffBl >= -4 && DiffBl <= 2) && (DiffG >= -4 && DiffG <= 2))
                {
                    if ((DiffB >= -4 || DiffB <= -1) && (DiffR >= -1 && DiffR <= 2) && (DiffBl <= -1 && DiffBl >= -4) && (DiffG >= -1 && DiffG <= 2))
                    {
                        profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ";
                    }
                }
                // D=C 
                if (DiffB > -1 && DiffR <= -1 && DiffBl <= -1 && DiffG > -1)
                {
                    if (DiffB == DiffR)
                    {
                        profile = "\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership.  ";
                    }
                }
                // Overshift
                if (DiffB > -1 && DiffR > -1 && DiffBl > -1 && DiffG > -1)
                {
                    profile = "\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ";
                }
                // Undershift
                if (DiffB <= -1 && DiffR <= -1 && DiffBl <= -1 && DiffG <= -1)
                {
                    profile = "\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ";
                }
                // in grey zone
                if (DiffB > 10)
                {
                    profile = "\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership.   ";
                }
                if (DiffR > 10)
                {
                    profile = "\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ";
                }
                if (DiffBl > 10)
                {
                    profile = "\t You show high degree of patience, calmness and gentle openness. You are generally amiable and warm-hearted, being sympathetic to others' points of view, and valuing positive interaction with others. You are not outgoing by nature; however, rely on other, more assertive, people to take the lead.   ";
                }
                if (DiffG > 10)
                {
                    profile = "\t Passive by nature, often reticent and aloof, you often tend to give an impression of coldness or disinterest. Much of this impassive style stems from your controlled nature, however, which makes you reluctant to reveal information about yourselves or your ideas unless absolutely necessary. In fact, you are often ambitious and have lofty goals, but your innate lack of assertiveness and unwillingness to become involved in confrontational situations makes it difficult for you to achieve these goals directly. Instead, you will tend to use existing structures and rules to accomplish your aims. You tend to follow rules, authority and logical argument to influence the actions of others.  ";
                }

                lblProfilAnalysis.Text = profile.ToString();
                RowProfile.Visible = true;
            }//end of if condition for RAPD
            else { RowProfile.Visible = false; }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            RowProfile.Visible = false;
        }
    }
}