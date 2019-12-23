
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class home1 : System.Web.UI.Page
{
    string role = null;
    static List<int> BIdList = new List<int>();
    static List<int> DealIdList = new List<int>();
    //static string B = "";
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    static string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    static string connStr = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
    //static string connectionString_Test = ConfigurationManager.ConnectionStrings["DBConnection_Test"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack && Session["checkpageload"]==null)
        {
            bank_detailsCheck();
            Session["checkpageload"] = true;
        }
       
        lblGrid();        
        counter_session();      
       
        
        try
        {
            //Check sessions of uid and dheyaemail of user
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {

                check_token_available();
                // Referesh_token();
                //CDF own referrals month-wise

                //chart_bind();

                //All CDF referrals month-wise

                //chart_bind2();

                //CDF record count

                //bindgrid();

                //News Feed Method

                newsFeed();
                //New Updates from Dheya (individual)
                dheyaUpdates();

                //CDF own referrals month-wise - converted and created


                //chart_bind_both();

                // lead converted

               // bindgrid_lead_converted();

                // lead created

                //bindgrid_lead_created();

                // Rating
                //bind_rating();
                //get_count();

                #region Lead Details


                // hfRefEmail.Value = Session["dheyaEmail"].ToString();
                dummydata();
                GetDataFromOurDB();
                #endregion Lead Details


                string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    DataTable dt2 = new DataTable();
                    string strcmd1 = "SELECT count(id) FROM suitecrm.leads as A inner join suitecrm.leads_cstm as B on A.id=B.id_c where A.refered_by='" + Session["dheyaEmail"].ToString() + "' and deleted=0";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(strcmd1, con);
                    MySqlDataReader dr1 = cmd.ExecuteReader();
                    //Check if table has rows for required query
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        Int32 s = dr1.GetInt32(0);
                        //leads_count.Text = " " + s;
                        //session_count.Text = " " + s * 0;
                        // revenue_count.Text = "" + s * 0;
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Check payment status of respective user from tblPayment table
                    string query_visit = "  select COUNT(log_id) as count from tblLog where uId='" + Session["uid"].ToString() + "' and log_type = 'in'";
                    SqlCommand cmdvisit = new SqlCommand(query_visit, connection);
                    //visits_count.Text = cmdvisit.ExecuteScalar().ToString();


                    try
                    {
                        string queryLastVisit = " SELECT TOP 1 log_time FROM (select top 2 log_time, log_id from tblLog where log_type = 'in' and uId = '" + Session["uid"].ToString() + "' order by log_id desc)t ORDER BY log_id";
                        SqlCommand cmdLastVisit = new SqlCommand(queryLastVisit, connection);
                        string date1 = cmdLastVisit.ExecuteScalar().ToString();
                        DateTime dt = Convert.ToDateTime(date1);
                        //visits_count.ToolTip = "Last Vist: " + dt.ToString("dd'/'MM'/'yyyy HH:mm:ss tt");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                    }

                    int uid = Convert.ToInt32(HttpContext.Current.Session["uid"].ToString());


                    string str = ""
                        + "select count(uId) FROM tblUserMaster where uId=" + uid + ";"
                        + "select count(uId)FROM tblUserDetails where uId = " + uid + ";"
                        + "select count(DISTINCT uId) FROM tblEducation where uId = " + uid + ";"
                        + "select count(DISTINCT uId) FROM tblExperience where uId = " + uid + "";

                    SqlDataAdapter da = new SqlDataAdapter(str, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    double count = 0;
                    double total = 4;
                    double profileComplete = 0;
                    //profile complete status
                    for (int i = 0; i < total; i++)
                    {
                        count = count + Convert.ToInt32(ds.Tables[i].Rows[0][0]);
                    }
                    profileComplete = (count / total) * 100;
                    profilec.Text = profileComplete.ToString();
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
    public void lblGrid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                int uid11 = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                SqlCommand cmddd = new SqlCommand();
               // int session_id = Convert.ToInt32();
                cmddd.CommandText= "select Student_Id from tbl_Session where (CDF_Id='"+ uid11 + "' or Shadow_CDFId='" + uid11 + "')  and SessionStatus='Assigned'and Session_Date >= CONVERT(VARCHAR(10), getdate(), 111)";
                cmddd.Connection = con;
                con.Open();
                SqlDataReader rdr = cmddd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Session["stud"] = rdr["Student_Id"].ToString();
                    }
                }
                con.Close();
                int studID = Convert.ToInt32(Session["stud"]);
                string role_type = null;               

                SqlCommand cmdd = new SqlCommand();
                //cmdd.CommandText= "select case when Ses.Shadow_CDFId='"+ uid11 + "' then 'ShadowCDF' when Ses.CDF_Id = '"+ uid11 + "' then 'Conducting CDF' end as YourRole from[dbo].[tbl_Session] as Ses where Ses.CDF_Id = '"+ uid11 + "' and Session_Date >=CONVERT(VARCHAR(10), getdate(), 111)";
                cmdd.CommandText = "select case when Ses.Shadow_CDFId='" + uid11 + "' then 'ShadowCDF' when Ses.CDF_Id = '" + uid11 + "' then 'Conducting CDF' end as YourRole from[dbo].[tbl_Session] as Ses where (Ses.CDF_Id = '" + uid11 + "' or Ses.Shadow_CDFId = '" + uid11 + "') and Student_Id='"+ studID + "' and Session_Date >= CONVERT(VARCHAR(10), getdate(), 111)";
                cmdd.Connection = con;
                con.Open();
                //dat reader = cmdd.ExecuteReader();
                SqlDataAdapter sda11 = new SqlDataAdapter(cmdd);
                //DataSet ds11 = new DataSet();
                DataTable dt = new DataTable();
                sda11.Fill(dt);             
 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        role_type = dt.Rows[i]["YourRole"].ToString();

                        if (role_type == "Conducting CDF")
                        {
                            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);

                            cmd.Parameters.AddWithValue("@CDF_Id", uid);
                            cmd.Parameters.AddWithValue("@Type", "GetSessionDetails");
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            role = ds.Tables[0].Rows[0]["YourRole"].ToString();
                            string cdfacceptance = ds.Tables[0].Rows[0]["CDF_Acceptance"].ToString();
                            int sessionID = Convert.ToInt32(ds.Tables[0].Rows[0]["Session_Id"]);
                            if (role == "ShadowCDF")
                            {
                                if (cdfacceptance == "")//blank means null
                                {
                                    Gridmysession.Columns[3].Visible = false;
                                    Gridmysession.Columns[20].Visible = true;
                                    Gridmysession.Columns[21].Visible = false;
                                    Gridmysession.Columns[22].Visible = false;
                                }
                                else if (cdfacceptance == "True")//true means Accepted
                                {

                                    Gridmysession.Columns[25].Visible = true; //accept
                                    Gridmysession.Columns[24].Visible = false; //MSG
                                    Gridmysession.Columns[26].Visible = false;//reject

                                }
                                else if (cdfacceptance == "False")//false means Reject
                                {

                                    Gridmysession.Columns[3].Visible = false;
                                    Gridmysession.Columns[21].Visible = false;
                                    Gridmysession.Columns[22].Visible = false;
                                }
                            }
                            else if (role == "Conducting CDF")
                            {
                                Gridmysession.Columns[24].Visible = false;
                                Gridmysession.Columns[3].Visible = true;
                                Gridmysession.Columns[8].Visible = false;
                                Gridmysession.Columns[6].Visible = true;
                                Gridmysession.Columns[7].Visible = true;
                                Gridmysession.Columns[4].Visible = false;
                                Gridmysession.Columns[5].Visible = false;

                        }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Gridmysession.DataSource = null;
                                Gridmysession.DataSource = ds;
                                Gridmysession.DataBind();
                            }
                            else
                            {
                                Gridmysession.DataSource = null;
                                Gridmysession.DataBind();
                            }

                        }
                        //else
                        else if(role_type=="ShadowCDF")
                        {
                            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);

                            cmd.Parameters.AddWithValue("@CDF_Id", uid);
                            cmd.Parameters.AddWithValue("@Type", "GetSessionDetails_ShadowCDF");
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            role = ds.Tables[0].Rows[0]["YourRole"].ToString();
                            string cdfacceptance = ds.Tables[0].Rows[0]["CDF_Acceptance"].ToString();
                            int sessionID = Convert.ToInt32(ds.Tables[0].Rows[0]["Session_Id"]);
                            if (role == "ShadowCDF")
                               
                            {
                                Gridmysession.Columns[3].Visible = false;
                                Gridmysession.Columns[6].Visible = false;
                                Gridmysession.Columns[7].Visible = false;
                            if (cdfacceptance == "")
                                {
                                    Gridmysession.Columns[6].Visible = false;
                                    Gridmysession.Columns[7].Visible = false;
                                    Gridmysession.Columns[24].Visible = true;
                                    Gridmysession.Columns[25].Visible = false;
                                    Gridmysession.Columns[26].Visible = false;
                                }
                                else if (cdfacceptance == "True")
                                {
                                //bool flg = true;

                                //bool flg = true;
                                //    e.Row.Cells[21].Visible = false;
                                //Button btn = Gridmysession.FindControl("accptBtn") as Button;
                                //btn.Enabled = true;
                                //Button mybtn = (Button)NamingContainer.FindControl("accptBtn");
                                //mybtn.Visible = true;



                                // Gridmysession.Columns[21].Visible = false;
                                // Gridmysession.Columns[20].Visible = false;
                                Gridmysession.Columns[24].Visible = false;
                                Gridmysession.Columns[26].Visible = false;

                                   // (ButtonField)Gridmysession.Rows[0].Cells[21].Controls.Clear="";
                                    // Gridmysession.ShowHeaderWhenEmpty Columns[21].Visible = true;





                                }
                                else if (cdfacceptance == "False")
                                {

                                    Gridmysession.Columns[3].Visible = false;
                                    Gridmysession.Columns[25].Visible = false;
                                    Gridmysession.Columns[26].Visible = false;
                                }
                            }
                            else if (role == "Conducting CDF")
                            {
                                Gridmysession.Columns[24].Visible = false;
                                Gridmysession.Columns[3].Visible = true;
                                Gridmysession.Columns[4].Visible = false;

                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Gridmysession.DataSource = null;
                                Gridmysession.DataSource = ds;
                                Gridmysession.DataBind();
                            }
                           
                            else
                            {
                                Gridmysession.DataSource = null;
                                Gridmysession.DataBind();
                            }
                        }
                       
                    }
                
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    //Commented for seprate cdf and shadow cdf session details dated on :15 Nov,2019
    //public void lblGrid()
    //{
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(connStr))
    //        { 
    //            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
    //            //int uid = 23;                
    //           //  int shadowID = Convert.ToInt32(Session["Shadow_CDFId"]);

    //            cmd.Parameters.AddWithValue("@CDF_Id", uid);
    //           // cmd.Parameters.AddWithValue("@Shadow_CDFId", shadowID);
    //            cmd.Parameters.AddWithValue("@Type", "GetSessionDetails");
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            DataSet ds = new DataSet();
    //            sda.Fill(ds);
    //             role = ds.Tables[0].Rows[0]["YourRole"].ToString();
    //            string cdfacceptance = ds.Tables[0].Rows[0]["CDF_Acceptance"].ToString();
    //           int sessionID = Convert.ToInt32(ds.Tables[0].Rows[0]["Session_Id"]);
    //            if(role=="ShadowCDF")
    //            {
    //                if (cdfacceptance == "")
    //                {
    //                    Gridmysession.Columns[3].Visible = false;
    //                    Gridmysession.Columns[20].Visible = true;
    //                    Gridmysession.Columns[21].Visible = false;
    //                    Gridmysession.Columns[22].Visible = false;
    //                }
    //                else if (cdfacceptance == "True")
    //                {

    //                    Gridmysession.Columns[21].Visible = true;
    //                    Gridmysession.Columns[20].Visible = false;
    //                    Gridmysession.Columns[22].Visible = false;

    //                }
    //                else if (cdfacceptance == "False")
    //                {

    //                    Gridmysession.Columns[3].Visible = false;
    //                    Gridmysession.Columns[21].Visible = false;
    //                    Gridmysession.Columns[22].Visible = false;
    //                }
    //            }
    //            else if(role=="Conducting CDF")
    //            {
    //                Gridmysession.Columns[20].Visible = false;
    //                Gridmysession.Columns[3].Visible = true;
    //                Gridmysession.Columns[4].Visible = false;

    //            }
    //            //else if(cdfacceptance==1.ToString())
    //            //{
    //            //    Gridmysession.Columns[20].Visible = false;
    //            //    Gridmysession.Columns[21].Visible = true;
    //            //}

    //            //int id =Convert.ToInt32(ds.Tables[0].Rows[0]["ShadowCDF"]);
    //            //Session["Shadow_CDFId"] =id;
    //            //int b = Convert.ToInt32(Session["Shadow_CDFId"]);

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                Gridmysession.DataSource = null;
    //                Gridmysession.DataSource = ds;
    //                Gridmysession.DataBind();
    //            }
    //            else
    //            {
    //                Gridmysession.DataSource = null;
    //                Gridmysession.DataBind();
    //            }



    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //    }
    //}

    public void counter_session()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                int stud = Convert.ToInt32(Session["stud"]);
                string role_type = null;

                SqlCommand cmdd = new SqlCommand();
                //cmdd.CommandText= "select case when Ses.Shadow_CDFId='"+ uid11 + "' then 'ShadowCDF' when Ses.CDF_Id = '"+ uid11 + "' then 'Conducting CDF' end as YourRole from[dbo].[tbl_Session] as Ses where Ses.CDF_Id = '"+ uid11 + "' and Session_Date >=CONVERT(VARCHAR(10), getdate(), 111)";
                cmdd.CommandText = "select case when Ses.Shadow_CDFId='" + uid + "' then 'ShadowCDF' when Ses.CDF_Id = '" + uid + "' then 'Conducting CDF' end as YourRole from[dbo].[tbl_Session] as Ses where (Ses.CDF_Id = '" + uid + "' or Ses.Shadow_CDFId = '" + uid + "')and Student_Id='" + stud + "'  and Session_Date >= CONVERT(VARCHAR(10), getdate(), 111)";
                cmdd.Connection = con;
                con.Open();
                //dat reader = cmdd.ExecuteReader();
                SqlDataAdapter sda11 = new SqlDataAdapter(cmdd);
                //DataSet ds11 = new DataSet();
                DataTable dt = new DataTable();
                sda11.Fill(dt);
                int row_cnt = dt.Rows.Count;
                if (row_cnt != 0)
                {
                    for (int i = 0; i < row_cnt; i++)
                    {
                        role_type = dt.Rows[i]["YourRole"].ToString();

                        if (role_type == "Conducting CDF")
                        {
                            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                            //int uid = 23;
                            cmd.Parameters.AddWithValue("@CDF_Id", uid);
                            cmd.Parameters.AddWithValue("@Type", "GetSessionCount");
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            int count = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]);
                            //lablcount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]).ToString();  
                            lblcount.Text = count.ToString();
                            if (count >= 1)
                            {
                                card.Visible = true;
                            }
                            else
                            {
                                card.Visible = false;
                            }

                        }
                        else if (role_type == "ShadowCDF")
                        {
                            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                            //int uid = 23;
                            cmd.Parameters.AddWithValue("@CDF_Id", uid);
                            cmd.Parameters.AddWithValue("@Type", "GetSessionCountforShadow");
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            int count = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]);
                            //lablcount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]).ToString();  
                            lblcount.Text = count.ToString();
                            if (count >= 1)
                            {
                                card.Visible = true;
                            }
                            else
                            {
                                card.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    card.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    public void bank_detailsCheck()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                int id = 0;
                string strcmd = "SELECT uId from tblUserBankDetails	WHERE uId = " + Session["uid"] + "";
                SqlCommand cmd1 = new SqlCommand(strcmd,con);
                con.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();               
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        id = Convert.ToInt32(dr1["uId"]);
                    }
                }
                con.Close();
                if (id > 0)
                {

                    Response.Redirect("home1.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Plz Update Bank details first');window.location ='/CDF/bank-details.aspx';", true);

                }
            }
        }

        catch (Exception ex)
        {
            ex.ToString();
        }
    }



    //private void get_count()
    //{
    //    string i = dbContext.ExecScal_local("select count(*) from tbl_leads");
    //    btn_Referred.Text =i;
    //    string j = dbContext.ExecScal_local("select count(*) from tbl_leads where lead_status= 'Progress'");
    //    btn_Progress.Text = j;
    //    string k = dbContext.ExecScal_local("select count(*) from tbl_leads where lead_status= 'Converted'");
    //    btn_Converted.Text = k;
    //    string l = dbContext.ExecScal_local("select count(*) from tbl_leads where lead_status= 'Junk'");
    //    btn_Junk.Text = l;
    //}

        //private void bind_rating()
        //{
        //    try
        //    {
        //        string rate = null;
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            con.Open();
        //            SqlDataReader dr;
        //            SqlCommand cmd = new SqlCommand("select uId, cdfrating from tblUserMaster where uId=" + Session["uid"], con);
        //            dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    rate = dr["cdfrating"].ToString();
        //                }
        //            }
        //        }
        //        switch (rate)
        //        {
        //            case "1.00":
        //                star1();
        //                break;
        //            case "1.05":
        //                star15();
        //                break;
        //            case "2.00":
        //                star2();
        //                break;
        //            case "2.05":
        //                star25();
        //                break;
        //            case "3.00":
        //                star3();
        //                break;
        //            case "3.05":
        //                star35();
        //                break;
        //            case "4.00":
        //                star4();
        //                break;
        //            case "4.05":
        //                star45();
        //                break;
        //            case "5.00":
        //                star5();
        //                break;
        //            default:
        //                star();
        //                break;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //    }
        //}

        //Get data for referrals (own)
    private DataTable GetData()
    {
        try
        {
            string constr1 = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(constr1))
            {
                DataTable dt = new DataTable();
                // conn.Open();
                string cmd = "Select count(id) as Count, MONTHNAME(date_entered) AS Month From suitecrm.leads where deleted = 0 and refered_by ='" + Session["dheyaEmail"].ToString() + "' AND Year(date_entered) = YEAR(CURDATE()) group by MONTHNAME(date_entered) order by MONTH(date_entered) asc";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conn);
                adp.Fill(dt);

                return dt;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return null;
        }
    }

    //Get data for referrals (own)
    private DataTable GetData_both()
    {
        try
        {
            string constr1 = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(constr1))
            {
                DataTable dt = new DataTable();
                // conn.Open();
                //string cmd = "Select MONTHNAME(date_entered)AS Month, sum(if (status = 'New', 1 , 0)) as StatusNew , sum(if (status = 'Call_Back', 1 , 0)) as StatusCallBack , count(id) as Count From suitecrm.leads where deleted = 0 and-- status = 'New' and refered_by = '" + Session["dheyaEmail"].ToString() + "' AND Year(date_entered) = YEAR(CURDATE())  group by MONTHNAME(date_entered) order by MONTH(date_entered) asc ";

                string cmd = "Select  MONTHNAME(date_entered) AS Month, "
                            + " sum(if (status = 'New', 1 , 0)) as StatusNew , "
                            + " sum(if (status = 'Call_Back', 1 , 0)) as StatusCallBack , "
                            + " count(id) as Count "

                            + " From suitecrm.leads "
                            + " where deleted = 0  "
                            + " and refered_by = '" + Session["dheyaEmail"].ToString() + "' AND Year(date_entered) = YEAR(CURDATE()) "
                            + " group by MONTHNAME(date_entered) order by MONTH(date_entered) asc ";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conn);
                adp.Fill(dt);

                return dt;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return null;
        }
    }
    //Get data for referrals (All)
    private DataTable GetData2()
    {
        try
        {
            string constr2 = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(constr2))
            {
                DataTable dt = new DataTable();
                // conn.Open();
                string cmd = "Select count(id) *5 as Count, MONTHNAME(date_entered) AS Month From suitecrm.leads where deleted = 0 and lead_source = '5' AND Year(date_entered) = YEAR(CURDATE()) group by MONTHNAME(date_entered) order by MONTH(date_entered) asc";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conn);
                adp.Fill(dt);

                return dt;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return null;
        }
    }
    //CDF own referrals month-wise
    private void chart_bind()
    {
        StringBuilder str = new StringBuilder();
        DataTable dt = new DataTable();
        try
        {
            dt = GetData();
            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*,*bar*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month');
            data.addColumn('number', 'Count');

            data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Month"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Count"].ToString() + ") ;");

            }
            str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
            str.Append("chart.draw(data, {width: '95%', height: 220, curveType: 'function',");
            str.Append("legend: 'none',");
            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}},fontSize: 12,");
            str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            //lt.Text = str.ToString().Replace('*', '"');
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    //CDF own referrals month-wise
    private void chart_bind_both()
    {
        StringBuilder str = new StringBuilder();
        DataTable dt = new DataTable();
        try
        {
            dt = GetData_both();
            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*,*bar*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month');
            data.addColumn('number', 'StatusNew');
            data.addColumn('number', 'StatusCallBack');

            data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Month"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["StatusNew"].ToString() + ") ;");
                //str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["StatusCallBack"].ToString() + ") ;");
            }
            str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('chart_div100'));");
            str.Append("chart.draw(data, {width: '95%', height: 220, curveType: 'function',");
            str.Append("legend: 'none',");
            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'red'}},fontSize: 12,");
            str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'red'}}");
            //str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'red'}}");
            str.Append("}); }");
            str.Append("</script>");
            //lt100.Text = str.ToString().Replace('*', '"');
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    //All CDF referrals month-wise
    private void chart_bind2()
    {
        StringBuilder str = new StringBuilder();
        DataTable dt = new DataTable();
        try
        {
            dt = GetData2();
            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*,*bar*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month');
            data.addColumn('number', 'Count');

            data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Month"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Count"].ToString() + ") ;");

            }
            str.Append("   var chart = new google.visualization.ColumnChart(document.getElementById('chart_div2'));");
            str.Append(" chart.draw(data, {width: '95%', height: 220, curveType: 'function',");
            str.Append("legend: 'none',");
            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}},fontSize: 12,");
            str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            //lt2.Text = str.ToString().Replace('*', '"');
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    //CDF records
    [WebMethod(EnableSession = true)]
    public static List<object> GetChartData()
    {
        try
        {
            if (HttpContext.Current.Session["uid"] != null && HttpContext.Current.Session["dheyaEmail"] != null)
            {
                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"].ToString());

                string connectionString1 = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString1))
                {

                    string str = ""
                        + "select count(uId) FROM tblUserMaster where uId=" + uid + ";"
                        + "select count(uId)FROM tblUserDetails where uId = " + uid + ";"
                        + "select count(DISTINCT uId) FROM tblEducation where uId = " + uid + ";"
                        + "select count(DISTINCT uId) FROM tblExperience where uId = " + uid + "";

                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    double count = 0;
                    double total = 4;
                    double profileComplete = 0;
                    double profileInComplete = 0;
                    for (int i = 0; i < total; i++)
                    {
                        count = count + Convert.ToInt32(ds.Tables[i].Rows[0][0]);
                    }

                    profileComplete = (count / total) * 100;
                    profileInComplete = 100 - profileComplete;

                    List<object> chartData = new List<object>();
                    chartData.Add(new object[]
                    {"Profile", "Status" });
                    chartData.Add(new object[]
                        {"Complete",profileComplete});
                    chartData.Add(new object[]
                       {"In Complete",profileInComplete});
                    return chartData;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/login.aspx", false);
                return null;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return null;
        }
    }
    //CDF record count
    private void bindgrid()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string strcmd = "select first_name as 'First Name',last_name as 'Last Name',phone_mobile as 'Contact Number',ea.email_address as 'Email Address',date_entered as 'Date',city_c as 'City',lead_source as 'Lead Source',status as 'Lead Status', cust.lead_category_c as 'Lead Category',lead_source_description as 'Description' FROM suitecrm.leads left join suitecrm.leads_cstm cust on cust.id_c=suitecrm.leads.id  LEFT JOIN suitecrm.email_addr_bean_rel eabl  ON eabl.bean_id = leads.id AND eabl.deleted=0 LEFT JOIN suitecrm.email_addresses ea ON (ea.id = eabl.email_address_id) and ea.deleted=0 where suitecrm.leads.refered_by='" + Session["dheyaEmail"].ToString() + "' and suitecrm.leads.deleted=0";
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //GridView1.DataSource = ds;
                    //GridView1.DataBind();
                    //div_leadstatus.Visible = false;
                }
                else
                {
                    //div_leadstatus.Visible = true;
                    //div_leadstatus.Attributes["class"] = "alert alert-warning";
                    //div_leadstatus.InnerText = "Currently you don't have any referral lead status";


                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("First Name");
                    //dt.Columns.Add("Last Name");
                    //dt.Columns.Add("Date");
                    //dt.Columns.Add("Lead Status");
                    //dt.Columns.Add("Description");
                    //ds.Tables[0].Rows.Add("", "", null, "","");

                    //GridView1.DataSource = ds;
                    //GridView1.DataBind();
                    //GridView1.DataSource = null;
                    //GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        // bindgrid();
    }


    private void bindgrid_lead_converted()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string strcmd = "select first_name as 'First Name',last_name as 'Last Name',phone_mobile as 'Contact Number',ea.email_address as 'Email Address',date_entered as 'Date',city_c as 'City',lead_source as 'Lead Source',status as 'Lead Status', cust.lead_category_c as 'Lead Category',lead_source_description as 'Description' FROM suitecrm.leads left join suitecrm.leads_cstm cust on cust.id_c=suitecrm.leads.id  LEFT JOIN suitecrm.email_addr_bean_rel eabl  ON eabl.bean_id = leads.id AND eabl.deleted=0 LEFT JOIN suitecrm.email_addresses ea ON (ea.id = eabl.email_address_id) and ea.deleted=0 where suitecrm.leads.refered_by='" + Session["dheyaEmail"].ToString() + "' and suitecrm.leads.deleted=0 and status = 'Converted' ";
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //lbl_converted.Text = ds.Tables[0].Rows.Count.ToString();
                    gv_lead_converted.DataSource = ds;
                    gv_lead_converted.DataBind();
                    div_leadstatus_converted.Visible = false;
                }
                else
                {
                    //lbl_converted.Text = ds.Tables[0].Rows.Count.ToString();
                    div_leadstatus_converted.Visible = true;
                    div_leadstatus_converted.Attributes["class"] = "alert alert-warning";
                    div_leadstatus_converted.InnerText = "Currently you don't have any referral lead status";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void gv_lead_converted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_lead_converted.PageIndex = e.NewPageIndex;
        bindgrid_lead_converted();
    }

    private void bindgrid_lead_created()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string strcmd = "select first_name as 'First Name',last_name as 'Last Name',phone_mobile as 'Contact Number',ea.email_address as 'Email Address',date_entered as 'Date',city_c as 'City',lead_source as 'Lead Source',status as 'Lead Status', cust.lead_category_c as 'Lead Category',lead_source_description as 'Description' FROM suitecrm.leads left join suitecrm.leads_cstm cust on cust.id_c=suitecrm.leads.id  LEFT JOIN suitecrm.email_addr_bean_rel eabl  ON eabl.bean_id = leads.id AND eabl.deleted=0 LEFT JOIN suitecrm.email_addresses ea ON (ea.id = eabl.email_address_id) and ea.deleted=0 where suitecrm.leads.refered_by='" + Session["dheyaEmail"].ToString() + "' and suitecrm.leads.deleted=0 ";
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //lbl_created.Text = ds.Tables[0].Rows.Count.ToString();
                    gv_lead_created.DataSource = ds;
                    gv_lead_created.DataBind();
                    div_leadstatus_created.Visible = true;
                }
                else
                {
                    //lbl_created.Text = ds.Tables[0].Rows.Count.ToString();
                    div_leadstatus_created.Visible = true;
                    div_leadstatus_created.Attributes["class"] = "alert alert-warning";
                    div_leadstatus_created.InnerText = "Currently you don't have any referral lead status";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void gv_lead_created_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_lead_created.PageIndex = e.NewPageIndex;
        bindgrid_lead_created();
    }


    private void newsFeed()
    {
        try
        {
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                string query_newsfeed = "Select top 3 title,description,DateDiff(day, dateCreated, (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))) as days From tblNewsFeed where status = 'ACTIVE' order by dateCreated desc";
                SqlCommand command = new SqlCommand(query_newsfeed, connection1);
                connection1.Open();
                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.HasRows)
                {
                    int i = 1;
                    while (sdr.Read())
                    {
                        if (i == 1)
                        {
                            lbl_title1.Text = sdr["title"].ToString();
                            lbl_content1.Text = sdr["description"].ToString();
                            lbl_hr1.Text = sdr["days"].ToString();
                            i++;
                        }
                        else if (i == 2)
                        {
                            lbl_title2.Text = sdr["title"].ToString();
                            lbl_content2.Text = sdr["description"].ToString();
                            lbl_hr2.Text = sdr["days"].ToString();
                            i++;
                        }
                        else
                        {
                            lbl_title3.Text = sdr["title"].ToString();
                            lbl_content3.Text = sdr["description"].ToString();
                            lbl_hr3.Text = sdr["days"].ToString();

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void dheyaUpdates()
    {
        try
        {
            lblDheyaUpdates.Attributes.Add("readonly", "readonly");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string querydheyaUpdates = "Select uId,dheyaUpdates From tblUserDetails where uId = " + Session["uid"] + "";
                SqlCommand command = new SqlCommand(querydheyaUpdates, con);
                con.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["dheyaUpdates"] != DBNull.Value)
                    {
                        lblDheyaUpdates.Text = dr["dheyaUpdates"].ToString();
                    }
                    else
                    {
                        lblDheyaUpdates.Text = "No updates found.";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void hl_policy_click(object sender, EventArgs e)
    {
        try
        {
            string fname = ConfigurationManager.AppSettings["policyDocumentPath"].ToString() + "new_policy.pdf";
            DownloadFile(fname, true);
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void DownloadFile(string fname, bool forceDownload)
    {
        string path = Server.MapPath(fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "";
        // set known types based on file extension  
        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".gif":
                    type = "image/GIF";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".docx":
                case ".doc":
                case ".rtf":
                    type = "Application/msword";
                    break;
                case ".pdf":
                    type = "Application/pdf";
                    break;
            }
        }
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
    }

    #region Star
    public void star1()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/empty.png";
        img_star3.ImageUrl = "~/images/empty.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star15()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/half.png";
        img_star3.ImageUrl = "~/images/empty.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star2()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/empty.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star25()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/half.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star3()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/full.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star35()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/full.png";
        img_star4.ImageUrl = "~/images/half.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star4()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/full.png";
        img_star4.ImageUrl = "~/images/full.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    public void star45()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/full.png";
        img_star4.ImageUrl = "~/images/full.png";
        img_star5.ImageUrl = "~/images/half.png";
    }
    public void star5()
    {
        img_star1.ImageUrl = "~/images/full.png";
        img_star2.ImageUrl = "~/images/full.png";
        img_star3.ImageUrl = "~/images/full.png";
        img_star4.ImageUrl = "~/images/full.png";
        img_star5.ImageUrl = "~/images/full.png";
    }
    public void star()
    {
        img_star1.ImageUrl = "~/images/empty.png";
        img_star2.ImageUrl = "~/images/empty.png";
        img_star3.ImageUrl = "~/images/empty.png";
        img_star4.ImageUrl = "~/images/empty.png";
        img_star5.ImageUrl = "~/images/empty.png";
    }
    #endregion Star

    protected void click_ref(object sender, EventArgs e)
    {
        Response.Redirect("https://dheya.bitrix24.in/pub/form/6_dheya_career_development_facilitator_a_proud_tag_to_wear/ah6w2j/");
    }

    public void ReferredLeads()               
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string strcmd = "select *, LeadStatus as Lead_Status from BitrixLeads where IsRemoved = 0 and LeadStatus = 'NEW' and  ReferedByEmail = '" + Session["dheyaEmail"].ToString() + "'";
                SqlCommand cmd = new SqlCommand("SP_BitrixLeadDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferedByEmail", Session["dheyaEmail"].ToString());
                cmd.Parameters.AddWithValue("@SP_Type", "ReferredLeads");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lbl_msg.Text = "No Records...";
                }
                else
                {
                    lbl_msg.Text = "";
                }
                Lead_Details.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                Leads.Focus();
                ViewState["grid"] = "Refered";
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void ProcessedLeads()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string strcmd = "select *, LeadStatus as Lead_Status from BitrixLeads where IsRemoved = 0 and LeadStatus = 'NEW' and  ReferedByEmail = '" + Session["dheyaEmail"].ToString() + "'";
                SqlCommand cmd = new SqlCommand("SP_BitrixLeadDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferedByEmail", Session["dheyaEmail"].ToString());
                // cmd.Parameters.AddWithValue("@SP_Type", "ProcessedLeads");
                cmd.Parameters.AddWithValue("@SP_Type", "InProcess");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lbl_msg.Text = "No Records...";
                }
                else
                {
                    lbl_msg.Text = "";
                }
                Lead_Details.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                Leads.Focus();
                ViewState["grid"] = "Processed";
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void ConvertedLeads()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string strcmd = "select *, LeadStatus as Lead_Status from BitrixLeads where IsRemoved = 0 and LeadStatus = 'NEW' and  ReferedByEmail = '" + Session["dheyaEmail"].ToString() + "'";
                SqlCommand cmd = new SqlCommand("SP_BitrixLeadDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferedByEmail", Session["dheyaEmail"].ToString());
                //cmd.Parameters.AddWithValue("@SP_Type", "ConvertedLeads");
                cmd.Parameters.AddWithValue("@SP_Type", "Converted_Deal");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lbl_msg.Text = "No Records...";
                }
                else
                {
                    lbl_msg.Text = "";
                }
                Lead_Details.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                Leads.Focus();
                ViewState["grid"] = "Converted";
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void JunkLeads()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string strcmd = "select *, LeadStatus as Lead_Status from BitrixLeads where IsRemoved = 0 and LeadStatus = 'NEW' and  ReferedByEmail = '" + Session["dheyaEmail"].ToString() + "'";
                SqlCommand cmd = new SqlCommand("SP_BitrixLeadDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferedByEmail", Session["dheyaEmail"].ToString());
                cmd.Parameters.AddWithValue("@SP_Type", "JunkLeads");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lbl_msg.Text = "No Records...";
                }
                else
                {
                    lbl_msg.Text = "";
                }
                Lead_Details.Visible = true;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                Leads.Focus();
                ViewState["grid"] = "Junk";
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    #region Display Lead Detail

    [WebMethod]
    public static List<LeadsCount> ShowLeadsCount(string ReferedByEmail)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<LeadsCount> list = new List<LeadsCount>();
                SqlCommand cmd = new SqlCommand("SP_BitrixLeadCount_Status", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferedByEmail", ReferedByEmail);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LeadsCount cnt = new LeadsCount();
                        cnt.Refered = Convert.ToInt32(dr["Refered"]);
                        //   cnt.progress = Convert.ToInt32(dr["progress"]);
                        cnt.progress = Convert.ToInt32(dr["InProcess"]);
                        //cnt.converted = Convert.ToInt32(dr["converted"]);
                        cnt.converted = Convert.ToInt32(dr["Convert_Deal"]);
                        cnt.junk = Convert.ToInt32(dr["junk"]);
                        list.Add(cnt);
                    }
                    return list;
                }
                else { return null; }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }
    protected void btn_Referred_Click(object sender, EventArgs e)
    {
        ReferredLeads();
    }
    protected void btn_Progress_Click(object sender, EventArgs e)
    {
        ProcessedLeads();
    }
    protected void btn_Converted_Click(object sender, EventArgs e)
    {
        ConvertedLeads();
    }
    protected void btn_Junk_Click(object sender, EventArgs e)
    {
        JunkLeads();
    }
    #endregion

    #region Read Bitrix
    //public static void getBitrixCount()
    //{
    //    //dal clsdal = new dal();
    //    //string i = clsdal.ExecScal111("select count(*) from BitrixLeads where ReferedByEmail = 'dhananjay.korde@dheya.com'");
    //    using (SqlConnection con = new SqlConnection(connectionString_Test))
    //    {
    //        con.Open();
    //        SqlCommand cmd = new SqlCommand("select count(*) from BitrixLeads where ReferedByEmail = 'dhananjay.korde@dheya.com'", con);
    //        int cnt = Convert.ToInt32(cmd.ExecuteScalar());
    //        con.Close();
    //    }
    //}
    public void GetDataFromOurDB()
    {
        try
        {
            if (Session["dheyaEmail"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_BITRIX_LEADS_LIST", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReferedByEmail", Session["dheyaEmail"].ToString());
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        gvBitrix.DataSource = dr;
                        gvBitrix.DataBind();
                    }
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    [WebMethod]
    public static int BitrixDataInsert(int BitrixId, string FName, string LName, string ContactNo, string Email, string RegDate, string Status, string ReferedByEmail, string LeadType, string LeadSource, int AssignedBy)
    {
        int flag = 0; bool state = false;
        try
        {
            string d = RegDate;
            string[] d1 = d.Split('T');
            string d2 = d1[0].ToString();
            string T = d1[1].ToString();
            string[] T1 = T.Split('+');
            string T2 = T1[0].ToString();
            string DT = d2 + " " + T2;
            //  string status = txtStatus.Text;

            BIdList.Add(BitrixId);
            RemoveLead(BIdList);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string q = " select BitrixId from BitrixLeads where BitrixId = '" + BitrixId + "' ";
                SqlCommand c = new SqlCommand(q, con);
                c.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = c.ExecuteReader();
                if (dr.HasRows)
                {
                    state = true;
                }
                else { state = false; }
                con.Close();


                if (state == false)
                {
                    string query = "INSERT INTO [dbo].[BitrixLeads]([BitrixId] ,[FName],[LName],[ContactNo],[Email],[RegDate],[LeadType],[LeadSource],[LeadStatus],[ReferedByEmail],[ResponsiblePersonId],[IsRemoved],[AddedDate]) "
                                   + "VALUES('" + BitrixId + "','" + FName + "','" + LName + "','" + ContactNo + "','" + Email + "','" + DT + "','" + LeadType + "','" + LeadSource + "','" + Status + "','" + ReferedByEmail + "','" + AssignedBy + "'," + 0 + ",'" + DateTime.Now + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        flag = 1;

                    }
                    else { flag = 0; }
                }

                else
                {
                    string query = "update BitrixLeads set LeadStatus = '" + Status + "', ResponsiblePersonId = '" + AssignedBy + "' where BitrixId='" + BitrixId + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        flag = 1;

                    }
                }
                return flag;


            }
            //
        }
        catch (Exception ex)
        {
            ex.ToString();
            return 0;
        }
    }

    [WebMethod]
    public static int BitrixDealInsert(int DealId, int LeadId, string RegDate, string DealStatus, string ReferedByEmail)
    {
        int flag = 0; bool state = false;
        try
        {
            string d = RegDate;
            string[] d1 = d.Split('T');
            string d2 = d1[0].ToString();
            string T = d1[1].ToString();
            string[] T1 = T.Split('+');
            string T2 = T1[0].ToString();
            string DT = d2 + " " + T2;
            //  string status = txtStatus.Text;

            DealIdList.Add(DealId);
            // RemoveLead(BIdList);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string q = " select BitrixDealId from BitrixDeals where BitrixDealId = '" + DealId + "' ";
                SqlCommand c = new SqlCommand(q, con);
                c.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = c.ExecuteReader();
                if (dr.HasRows)
                {
                    state = true;
                }
                else { state = false; }
                con.Close();

                if (state == true)
                {
                    string query = " UPDATE BitrixDeals SET LeadStatus='" + DealStatus + "' WHERE BitrixDealId='" + DealId + "' and BitrixLeadId='" + LeadId + "' and ReferedByEmail='" + ReferedByEmail + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        flag = 1;

                    }
                    else { flag = 0; }
                }

                if (state == false)
                {
                    string query = " INSERT INTO [dbo].[BitrixDeals]([BitrixDealId],[BitrixLeadId],[LeadStatus],[ReferedByEmail],[RegDate],[AddedDate]) " +
                            " VALUES('" + DealId + "','" + LeadId + "','" + DealStatus + "','" + ReferedByEmail + "','" + DT + "','" + DateTime.Now + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        flag = 1;

                    }
                    else { flag = 0; }
                }
                //else
                //{
                //    string query = "update BitrixLeads set LeadStatus = '" + Status + "', ResponsiblePersonId = '" + AssignedBy + "' where BitrixId='" + BitrixId + "'";
                //    SqlCommand cmd = new SqlCommand(query, con);
                //    cmd.CommandType = System.Data.CommandType.Text;

                //    con.Open();
                //    int i = cmd.ExecuteNonQuery();
                //    if (i > 0)
                //    {
                //        flag = 1;

                //    }
                //}
                return flag;
            }
            //
        }
        catch (Exception ex)
        {
            ex.ToString();
            return 0;
        }
    }

    //public static void CheckRemoved(int BitrixId)
    //{
    //    try
    //    {

    //        BId.Add(BitrixId);
    //        RemoveLead(BId);
    //        //using (SqlConnection con = new SqlConnection(connectionString_Test))
    //        //{
    //        //    bool flag1 = false;
    //        //    string g = "select BitrixId from BitrixLeads";
    //        //    SqlCommand c = new SqlCommand(g, con);
    //        //    c.CommandType = CommandType.Text;
    //        //    con.Open();
    //        //    SqlDataReader dr = c.ExecuteReader();
    //        //    if (dr.HasRows)
    //        //    {
    //        //        while (dr.Read())
    //        //        {
    //        //            int b = Convert.ToInt32(dr["BitrixId"]);
    //        //            if (b == BitrixId)
    //        //            {
    //        //                flag1 = true;
    //        //            }
    //        //            else
    //        //            {
    //        //                flag1 = false;
    //        //                RemoveLead(b);
    //        //            }
    //        //            break;
    //        //        }
    //        //    }
    //        //    con.Close();
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //    }
    //}
    // This function will remove deleted Lead in the Bitrix [It is not delete permanantly only IsRemoved = false]
    public static void RemoveLead(List<int> BitrixId)
    {
        try
        {
            if (HttpContext.Current.Session["dheyaEmail"] != null)
            {
                string ReferedByEmail = HttpContext.Current.Session["dheyaEmail"].ToString();
                string B = "";
                if (BitrixId.Count == 0)
                {
                    B = "0";
                }
                else
                {
                    for (int i = 0; i < BitrixId.Count; i++)
                    {
                        if (i == 0)
                        {
                            B = BitrixId[i].ToString();
                        }
                        else
                        {
                            B = B + "," + BitrixId[i].ToString();
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string q = " update BitrixLeads set IsRemoved = 1 where BitrixId not in (" + B + ") and ReferedByEmail = '" + ReferedByEmail + "' ";
                    string q1 = " update BitrixLeads set IsRemoved = 0 where BitrixId in (" + B + ") and ReferedByEmail = '" + ReferedByEmail + "' ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlCommand cmd1 = new SqlCommand(q1, con);
                    cmd.CommandType = CommandType.Text;
                    cmd1.CommandType = CommandType.Text;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    int i1 = cmd1.ExecuteNonQuery();
                    if (i > 0)
                    {
                        // success ;
                    }
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    public void dummydata()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("BitrixId");
        dummy.Columns.Add("FName");
        dummy.Columns.Add("LName");
        dummy.Columns.Add("ContactNo");
        dummy.Columns.Add("Email");
        dummy.Columns.Add("RegDate");
        dummy.Columns.Add("Status");
        dummy.Columns.Add("ReferedByEmail");
        dummy.Rows.Add();
        gvLeads.DataSource = dummy;
        gvLeads.DataBind();
    }

    [WebMethod]
    public static void DeleteAllLeadByEmail(string Email)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string q = " update BitrixLeads set IsRemoved = 1 where ReferedByEmail = '" + Email + "' ";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    // success ;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    public class Bdata
    {
        public int BitrixId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        public string Status { get; set; }
        public string ReferedByEmail { get; set; }
    }
    #endregion Read Bitrix

    #region Get OAuth Token
    public void check_token_available()
    {
        string pure_token1 = "push token read from database";
        try
        {

            // Read Token from Database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string q = " select * from tblToken ";
                SqlCommand c = new SqlCommand(q, con);
                c.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = c.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    pure_token1 = dr["token"].ToString();
                }
                con.Close();
            }
            //ViewState["RefreshToken"] = pure_token1;
            var client = new WebClient();
            // check token using following list API
            string uri = "https://dheya.bitrix24.in/rest/crm.lead.list?auth=" + pure_token1;
            var text = client.DownloadString(uri);

            Session["TOKEN"] = pure_token1;
        }
        catch (Exception ex)
        {
            ex.ToString();
            string e = ex.ToString();
            Referesh_token(pure_token1);
            //CreateNew_Token();
        }
    }
    public void CreateNew_Token()
    {
        try
        {
            //  https://dheya.bitrix24.in/oauth/authorize/?response_type=code&client_id=local.5c9c74a54f4101.43211671&redirect_uri=app_URL
            //("https://dheya.com/democorptest/leads/CRM_Lead.aspx?code=57e0085d0038d01400326cd60000002f282303f233295f838c6d20f8c575909a0372b1&state=&domain=dheya.bitrix24.in&member_id=20997390ebdfcab53796b6c36a74fbef&scope=crm&server_domain=oauth.bitrix.info");
            String URLstring = "https://dheya.bitrix24.in/oauth/authorize/?response_type=code&client_id=local.5c9c74a54f4101.43211671&redirect_uri=app_URL";
            XmlTextReader reader = new XmlTextReader(URLstring);
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string a = url;
        }
        catch (Exception ex)
        {
            ex.ToString();
            string e = ex.ToString();
        }
    }
    public void Referesh_token(string token)
    {
        try
        {
            //  string OldToken = ViewState["RefreshToken"].ToString();
            var client = new WebClient();

            //string url = "https://dheya.bitrix24.in/oauth/token/?grant_type=refresh_token&client_id=local.5c9c74a54f4101.43211671&client_secret=55YDO6w5e11WvUzBf4rJmUKZQi4WAKm3VUzMvp7Y1KkVOPlrAC&refresh_token="+OldToken+"&scope=granted_permission&redirect_uri=app_URL";
            //var text = client.DownloadString(url);

            //var text = client.DownloadString("https://dheya.bitrix24.in/oauth/token/?grant_type=refresh_token&client_id=local.5c9c74a54f4101.43211671&client_secret=55YDO6w5e11WvUzBf4rJmUKZQi4WAKm3VUzMvp7Y1KkVOPlrAC&refresh_token=92b5705d0038d01400326cd60000002528230390b052e8bbdb038dd1d01d366be6fb04&scope=granted_permission&redirect_uri=app_URL");// Process to get Access Token

            // var text = client.DownloadString("https://dheya.bitrix24.in/oauth/token/?grant_type=refresh_token&client_id=local.5c9c74a54f4101.43211671&client_secret=55YDO6w5e11WvUzBf4rJmUKZQi4WAKm3VUzMvp7Y1KkVOPlrAC&refresh_token=f10aa05d0038d01400326cd6000000252823038c81763e002d18c766a8eb48114b0d3a&scope=granted_permission&redirect_uri=app_URL");// Process to get Access Token

            //var text = client.DownloadString("https://dheya.bitrix24.in/oauth/token/?grant_type=refresh_token&client_id=local.5c9c74a54f4101.43211671&client_secret=55YDO6w5e11WvUzBf4rJmUKZQi4WAKm3VUzMvp7Y1KkVOPlrAC&refresh_token=d44aa05d0038d01400326cd6000000252823039dae90ad041e1ebdebdae9e181b8b5ce&scope=granted_permission&redirect_uri=app_URL");// Process to get Access Token


            string str = "https://dheya.bitrix24.in/oauth/token/?client_id=local.5c9c74a54f4101.43211671&grant_type=refresh_token&client_secret=55YDO6w5e11WvUzBf4rJmUKZQi4WAKm3VUzMvp7Y1KkVOPlrAC&refresh_token=" + token + "&scope=granted_permission&redirect_uri=app_URL";

            var text = client.DownloadString(str);



            int mixed_token_index = text.IndexOf("access_token") + 15;  // get starting index of access_token value
            string pure_token = text.Substring(mixed_token_index);     // get full string starting from access_token value to end
            int index_expires = pure_token.IndexOf("expires");
            int lenght = (index_expires) - 3;
            string pure_token1 = pure_token.Substring(0, lenght);

            Session["TOKEN"] = pure_token1;

            int mixed_refresh_token_index = text.IndexOf("refresh_token") + 16;
            string pure_refresh_token = text.Substring(mixed_refresh_token_index);
            int refresh_token_length = pure_refresh_token.Length;
            int pure_refresh_token_length = refresh_token_length - 2;
            pure_refresh_token = text.Substring(mixed_refresh_token_index, pure_refresh_token_length);

            // Update token into Database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string query_add = "insert into tblAllToken (Token) values ('" + pure_token1 + "') ";
                //SqlCommand cmd_add = new SqlCommand(query_add, con);
                //cmd_add.CommandType = System.Data.CommandType.Text;

                string query = "update tblToken set token = '" + pure_refresh_token + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = System.Data.CommandType.Text;

                con.Open();
                int i = cmd.ExecuteNonQuery();
                //int j = cmd_add.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    //flag = 1;
                }
            }

            // Process to get Refresh code -- Note : Currently not in use.

        }
        catch (Exception ex)
        {
            ex.ToString();
            string e = ex.ToString();
        }
    }
    #endregion

    public class LeadsCount
    {
        public int Refered { get; set; }
        public int progress { get; set; }
        public int converted { get; set; }
        public int junk { get; set; }
    }

    protected void Gridmysession_RowCommand(object sender, GridViewCommandEventArgs e)

    {
        if (e.CommandName == "Accept")
        {

            bool flag = true;

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = Gridmysession.Rows[rowIndex];
            //Access Cell values.
            int studid = int.Parse(row.Cells[10].Text);

            
           
            string cdf_acce = null;
            
            string StudetnName, ContactNo;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                if (role == "ShadowCDF")
                {
                    SqlCommand cm = new SqlCommand("select CDF_Acceptance from tbl_Session where Student_Id='" + studid + "'", con);
                    con.Open();
                    SqlDataReader sdr1 = cm.ExecuteReader();
                    if (sdr1.HasRows)
                    {
                        while (sdr1.Read())
                        {
                            cdf_acce = sdr1["CDF_Acceptance"].ToString();
                        }
                    }
                    sdr1.Close();
                }
                else
                {
                    #region cdf accesptance check
                    SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                    //int uid = 23;
                    cmd.Parameters.AddWithValue("@Type", "GetAcceptStatus");
                    cmd.Parameters.AddWithValue("@Student_Id", studid);
                    cmd.Parameters.AddWithValue("@CDF_Id", uid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        //string query = " select fname,lname,contactNo,email from tbl_candidate_master where id='" + studid + "'";
                        string query = "select fname,lname,contactNo,email,Session_Date,Session_Time from tbl_candidate_master as cand inner join tbl_Session as ses on ses.Student_Id = cand.Id where id = '" + studid + "'";

                        SqlCommand cmd1 = new SqlCommand(query, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                StudetnName = ds.Tables[0].Rows[0]["fname"].ToString();
                                string sessiondate = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                                string sessionTime = ds.Tables[0].Rows[0]["Session_Time"].ToString();
                                sessiondate = sessiondate.Replace(" 00:00:00", "");
                             //   string sesdate = (sessiondate);
                                ContactNo = ds.Tables[0].Rows[0]["contactNo"].ToString();
                                int cdf = Convert.ToInt32(HttpContext.Current.Session["uid"]);

                                SqlCommand cm1 = new SqlCommand("select Shadow_CDFId from tbl_Session where Shadow_CDFId='" + cdf + "'and Student_Id='" + studid + "'", con);
                                SqlDataReader sdr = cm1.ExecuteReader();
                                if (!sdr.HasRows)
                                {
                                    //send OTP on User SMS
                                    string SMSText = ConfigurationManager.AppSettings["sessionAcceptmsg"].ToString();
                                    SMSText = SMSText.Replace("{Name}", "" + StudetnName);
                                    SMSText = SMSText.Replace("{sessiondate}", "" + sessiondate);
                                    SMSText = SMSText.Replace("{sessionTime}", "" + sessionTime);                               
                              
                                    sendSms(ContactNo, SMSText);
                                    ViewState["StudentId"] = studid;
                                }
                            }                           
                        }
                        else
                        {
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Accepted Succesfully!!');window.location ='home1.aspx';", true);

                        //lblmsg.Visible = true;
                        //lblmsg.Text = "Accepted";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "modalDialog", "CallJavaScriptFunction(" + RowIndex + ");", true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "validation();", true);
                        // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Accepted')", true);
                        //Response.Write("<script>alert('Accepted');</script>");
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Accepted');", true);  
                        //string message = "Accepted";
                        //string script = "window.onload = function(){ alert('";
                        //script += message;
                        //script += "')};";
                        //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                        //string script = "Accepted";
                        //Response.Write(script);
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Accepted')</script>");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Accepted')", true);
                        //Response.Write(@"<script language='javascript'>alert('Accepted')</script>");

                    }
                    else
                    {
                    }
                    #endregion
                }

                if (role == "ShadowCDF")
                {
                    if (cdf_acce == "True")
                    {
                        SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                        //int uid = 23;
                        cmd.Parameters.AddWithValue("@Type", "GetAcceptStatus");
                        cmd.Parameters.AddWithValue("@Student_Id", studid);
                        cmd.Parameters.AddWithValue("@CDF_Id", uid);
                        if(con.State!=System.Data.ConnectionState.Open)
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            //string query = " select fname,lname,contactNo,email from tbl_candidate_master where id='" + studid + "'";
                            string query = "select fname,lname,contactNo,email,Session_Date from tbl_candidate_master as cand inner join tbl_Session as ses on ses.Student_Id = cand.Id where id = '" + studid + "'";
                            SqlCommand cmd1 = new SqlCommand(query, con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd1);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    StudetnName = ds.Tables[0].Rows[0]["fname"].ToString();
                                    ContactNo = ds.Tables[0].Rows[0]["contactNo"].ToString();
                                    string sessiondate = ds.Tables[0].Rows[0]["Session_Date"].ToString();                                   
                                    int cdf = Convert.ToInt32(HttpContext.Current.Session["uid"]);

                                    SqlCommand cm1 = new SqlCommand("select Shadow_CDFId from tbl_Session where Shadow_CDFId='" + cdf + "'and Student_Id='" + studid + "'", con);
                                    SqlDataReader sdr = cm1.ExecuteReader();
                                    if (!sdr.HasRows)
                                    {
                                        //send OTP on User SMS
                                        string SMSText = ConfigurationManager.AppSettings["sessionAcceptmsg"].ToString();
                                        SMSText = SMSText.Replace("{Name}", "" + StudetnName);
                                        SMSText = SMSText.Replace("{sessiondate}", "" + sessiondate);                                      
                                        sendSms(ContactNo, SMSText);                                        
                                        ViewState["StudentId"] = studid;
                                    }
                                }
                            }
                            else
                            {

                            }

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Accepted Succesfully!!');window.location ='home1.aspx';", true);

                            //lblmsg.Visible = true;
                            //lblmsg.Text = "Accepted";

                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "modalDialog", "CallJavaScriptFunction(" + RowIndex + ");", true);
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "validation();", true);
                            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Accepted')", true);
                            //Response.Write("<script>alert('Accepted');</script>");
                            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Accepted');", true);  
                            //string message = "Accepted";
                            //string script = "window.onload = function(){ alert('";
                            //script += message;
                            //script += "')};";
                            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                            //string script = "Accepted";
                            //Response.Write(script);
                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Accepted')</script>");
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Accepted')", true);
                            //Response.Write(@"<script language='javascript'>alert('Accepted')</script>");

                        }

                        else
                        {


                        }
                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('Career mentor has not accepted the session request yet')</script>");
                    }
                }
            }
           

        }
        else
        {
            if (e.CommandName == "Reject")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row1 = Gridmysession.Rows[index];
                int stuid = int.Parse(row1.Cells[10].Text);
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                    //int uid = 23;
                    cmd.Parameters.AddWithValue("@CDF_Id", uid);
                    cmd.Parameters.AddWithValue("@Type", "GetRejectStatus");
                    cmd.Parameters.AddWithValue("@Student_Id", stuid);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i < 0)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Rejected Succesfully!!');window.location ='home1.aspx';", true);
                        //lblmsg.Visible = true;
                        //lblmsg.Text = "Rejected";
                    }
                    con.Close();
                }
                
                //Response.Redirect("~/home1.aspx");
            }
        }
        // Response.Redirect("~/home1.aspx");
        //catch(Exception ex)
        //{
        //    ex.ToString();
        //    Response.Redirect("~/home1.aspx");
        //}


    }

    protected void Gridmysession_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "Conducting CDF")
            {
                e.Row.Cells[25].Visible = true;
                e.Row.Cells[26].Visible = true;

            }

            else if (e.Row.Cells[1].Text == "ShadowCDF")
            {
                 if (e.Row.Cells[23].Text == "True")
                {
                     if (e.Row.Cells[24].Text == "Awaiting CDF Confirmation")
                    {
                        Gridmysession.Columns[25].Visible = true;
                        e.Row.Cells[24].Text = "CDF Accepted";

                        if(e.Row.Cells[24].Text== "CDF Accepted")
                        {
                            e.Row.Cells[24].ForeColor = Color.Green;
                        }
                        
                        e.Row.Cells[24].Visible = true;
                        e.Row.Cells[25].Visible = true;
                    }

                    e.Row.Cells[24].Visible = true;
                    e.Row.Cells[25].Visible = true;

                }
                else
                {
                    e.Row.Cells[24].Visible = true;
                    e.Row.Cells[25].Visible = false;
                }

            }



        }
    }
    public Boolean sendSms(string mob, string msg)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            string userid = ConfigurationManager.AppSettings["SMSUserId"].ToString();  //  "2000167436";
            string passwd = ConfigurationManager.AppSettings["SMSPassword"].ToString();  //  "xzreMXXv5";
            string url =
              "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" +
            mob + "&msg=" + msg + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";

            request = WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new System.IO.StreamReader(stream, ec);
            result = reader.ReadToEnd();
            Console.WriteLine(result);

            reader.Close();
            stream.Close();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ex.Message.ToString();
            return false;
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string grid = ViewState["grid"].ToString();
        if (grid == "Refered")
        {
            GridView2.PageIndex = e.NewPageIndex;
            ReferredLeads();
        }
        else if (grid == "Processed")
        {
            GridView2.PageIndex = e.NewPageIndex;
            ProcessedLeads();
        }
        else if (grid == "Converted")
        {
            GridView2.PageIndex = e.NewPageIndex;
            ConvertedLeads();
        }
        else if (grid == "Junk")
        {
            GridView2.PageIndex = e.NewPageIndex;
            JunkLeads();
        }

    }

}