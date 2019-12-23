using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Net;
using System.Text;
using log4net;
using System.Threading;

public partial class session : System.Web.UI.Page
{
    string role = null;
    static string connStr = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
    db_context dbContext = new db_context();
    SqlConnection con11 = new SqlConnection(connStr);
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {      
            counter_session();
            lblGrid();
            history();
           
            
        }
       
    }

    //protected void graph_btn_Click(object sender, EventArgs e)
    //{
       


    //}


    public void counter_session()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
               
                SqlCommand cmd = new SqlCommand("sp_cdfdetails1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                //int uid = 23;
                cmd.Parameters.AddWithValue("@CDF_Id", uid);
                cmd.Parameters.AddWithValue("@Type", "GetCount");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]);
                //lablcnt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SesCount"]).ToString();
                //lablcnt.Text = count.ToString();
                if (count >= 1)
                {
                    //stddetails.Visible = true;
                }
                else
                {
                    //stddetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    //protected void show(object sender, EventArgs e)
    //{
    //    HyperLink hyp = new HyperLink();
    //    if (hyp.Visible == true)
    //        hyp.Visible = false;
    //    else
    //        hyp.Visible = false;
    //}

    public void lblGrid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_cdfdetails1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
               
                //int uid = 23;
                cmd.Parameters.AddWithValue("@CDF_Id", uid);
                cmd.Parameters.AddWithValue("@Type", "getCDFUpcomingSession");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                sda.Fill(ds);

                role = ds.Rows[0]["YourRole"].ToString();
               var shadowAcceptance = ds.Rows[0]["shadowacceptance"].ToString();
                Session["shadowAccept"] = shadowAcceptance;
                if (role == "Conducting CDF")
                {
                    Gridmysession.Columns[3].Visible = false;
                }
                else if (role == "ShadowCDF")
                {
                      if (shadowAcceptance == "")
                    {
                        Gridmysession.Columns[19].Visible = false;
                        Gridmysession.Columns[20].Visible = false;
                    }
                    if(shadowAcceptance == "1")
                    {
                        Gridmysession.Columns[19].Visible = true;
                        Gridmysession.Columns[20].Visible = true;

                    }
                    Gridmysession.Columns[2].Visible = false;
                   
                }



                
                
              

                // today changes---
                int studend = Convert.ToInt32(ds.Rows[0]["StudId"]);
                Session["Student_ID"] = studend;
                SqlCommand cm = new SqlCommand("select Shadow_CDFId from tbl_Session where Shadow_CDFId='" + uid + "'and Student_Id='" + studend + "' ",con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                if (sdr.HasRows)
                {
                    Gridmysession.Columns[16].Visible = false;
                    Gridmysession.Columns[18].Visible = false;
                }
                //Today chaneges end----

                DateTime date = DateTime.Now.Date;
                //Adding new column in existing datatable
                DataColumn newCol = new DataColumn("MatchDateTime", typeof(bool));
                newCol.AllowDBNull = true;
                ds.Columns.Add(newCol);
                //if you don't want to allow null-values' newCol.AllowDBNull = false;
                bool flag = false;
                //for (int i = 0; i < ds.Rows.Count; i++)
                foreach (DataRow row in ds.Rows)
                {
                    DateTime session_date = Convert.ToDateTime(row["sessiondate1"]);
                    if (session_date == date)
                    {
                        flag = true;
                        //string sestime = row["SesTime"].ToString();
                        //string[] splitTime = sestime.Split(':');
                        //string[] splitTimeAMPM = sestime.Split(' ');
                        //string systemTime = DateTime.Now.ToString("h:mm:ss tt");
                        //string[] SplitsystemTime = systemTime.Split(':');
                        //string[] splitSystemAMPM = systemTime.Split(' ');
                        //////  string a = Convert.ToInt32(splitTime[0]);
                        //string ah = splitTime[0].ToString();
                        //string[] b = splitTime[1].Split(' ');
                        //string bm = b[0].ToString();
                        //string AMPM = splitTimeAMPM[1].ToString();
                        //string sah = SplitsystemTime[0].ToString();
                        //string sbm = SplitsystemTime[1].ToString();
                        //string sysAMPM = splitSystemAMPM[1].ToString();

                       // if (Convert.ToInt32(sah) == Convert.ToInt32(ah))
                       // {
                            
                       //         if (Convert.ToInt32(sbm) == Convert.ToInt32(bm))
                       //        {
                               
                       //               if (AMPM == sysAMPM)
                       //              {
                       //                  flag = true;
                       //               }
                       //               else
                       //               {
                       //                   flag = false;
                       //              }
                       //     }
                       //     else
                       //     {
                       //         flag = false;
                       //     }
                       // }
                       //else
                       //{
                       //    flag = false;
                       // }

                    }

                    else
                    {

                        flag = false;
                    }
                    //Adding value to created new column
                    row["MatchDateTime"] = flag;
                }


                if (ds.Rows.Count > 0)
                {
                    Gridmysession.DataSource = ds;
                    Gridmysession.DataBind();
                }
                else
                {
                    NoSesFound.Text = "No Upcoming Session";
                    Gridmysession.DataSource = null;
                    Gridmysession.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    
    //protected void Gridmysession_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Accept")
    //    {

    //        int rowIndex = Convert.ToInt32(e.CommandArgument);
    //        //Reference the GridView Row.
    //        GridViewRow row = Gridmysession.Rows[rowIndex];

    //        //Access Cell values.
    //        int studid = int.Parse(row.Cells[2].Text);
    //        using (SqlConnection con = new SqlConnection(connStr))
    //        {
    //            SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
    //            //int uid = 23;
    //            cmd.Parameters.AddWithValue("@Type", "GetAcceptStatus");
    //            cmd.Parameters.AddWithValue("@Student_Id", studid);
    //            cmd.Parameters.AddWithValue("@CDF_Id", uid);
    //            con.Open();
    //            int i = cmd.ExecuteNonQuery();
    //            if (i > 0)
    //            {

    //                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Accepted Succesfully!!');window.location ='session.aspx';", true);                    
    //            }
    //            else
    //            {

    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (e.CommandName == "Reject")
    //        {

    //            int index = Convert.ToInt32(e.CommandArgument);
    //            GridViewRow row1 = Gridmysession.Rows[index];
    //            int stuid = int.Parse(row1.Cells[2].Text);
    //            using (SqlConnection con = new SqlConnection(connStr))
    //            {
    //                SqlCommand cmd = new SqlCommand("sp_MySessionDetaislForCDF", con);
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
    //                //int uid = 23;
    //                cmd.Parameters.AddWithValue("@CDF_Id", uid);
    //                cmd.Parameters.AddWithValue("@Type", "GetRejectStatus");
    //                cmd.Parameters.AddWithValue("@Student_Id", stuid);

    //                con.Open();
    //                int i = cmd.ExecuteNonQuery();
    //                if (i < 0)
    //                {

    //                }
    //                else
    //                {
    //                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Rejected Succesfully!!');window.location ='session.aspx';", true);

    //                }
    //                con.Close();
    //            }

    //        }
    //    }
    //}

  
      


    public void history()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_cdfdetails1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"]);
                var stud = Convert.ToInt32(Session["Student_ID"]);
                SqlCommand cmd1 = new SqlCommand("select * from tbl_Session where Student_Id='" + stud + "'", con);
                cmd.Parameters.AddWithValue("@CDF_Id", uid);
                cmd.Parameters.AddWithValue("@Type", "getCDFSessionhistory");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                role = ds.Tables[0].Rows[0]["YourRole"].ToString();

                if (role == "Conducting CDF")
                {
                    dtl.Columns[3].Visible = false;
                }
                else if (role == "ShadowCDF")
                {
                    dtl.Columns[2].Visible = false;
                }
                //var vs = ds.Tables[0].Rows[0]["SesDate"].ToString();
                //string[] data = vs.Split('-');
                //string d1 = data[0].ToString();
                //string m1= data[1].ToString();
                //string y1 = data[2].ToString();              


                //StringSplitOptions split = new StringSplitOptions();
                //string[] date = vs.Split('-');


                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtl.DataSource = ds;
                    dtl.DataBind();
                }
                else
                {
                    dtl.DataSource = null;
                    dtl.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void dtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[15].Text == "Accepted")
            {
                //e.Row.Cells[13].BackColor = Color.Green ;

                e.Row.Cells[15].ForeColor = Color.Green;
            }
            if (e.Row.Cells[15].Text == "Rejected")
            {
                //e.Row.Cells[13].BackColor = Color.White;
                e.Row.Cells[15].ForeColor = Color.Red;
                // (e.Row.FindControl("hlView") as HyperLink).Visible = false;
               
            }
        }
    }

    protected void Gridmysession_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          

            string stud1 = Session["Student_ID"].ToString();
            string shadowAcceptance = Session["shadowAccept"].ToString();
            string asd = e.Row.Cells[21].Text;

            //if(e.Row.Cells[5].Text==stud1)
            //{
            //    Gridmysession.Columns[19].Visible = true;
            //    e.Row.Cells[19].Visible = true;               

            //}   

            if (e.Row.Cells[17].Text == "Accepted")
            {
                //e.Row.Cells[13].BackColor = Color.Green ;

                e.Row.Cells[17].ForeColor = Color.Green;
               // (e.Row.FindControl("hlView") as HyperLink).Visible = true; 
                // (e.Row.FindControl("report_btn") as Button).Visible = true;
              //  (e.Row.FindControl("report_btn") as Button).Visible = false;
                

                //int id=0;
                //if (int.TryParse(e.Row.Cells[2].Text, out id))
                //{
                //    int studentId = Convert.ToInt32(Session["StudId"]);
                //    if (id == studentId)
                //    {
                //        Button btn1 = (Button)e.Row.FindControl("report_btn");
                //        btn1.Visible = true;
                //    }
                //}

                //HyperLink hper1 = (HyperLink)e.Row.FindControl("hlView");                               
                //Button btn1 = (Button)e.Row.FindControl("report_btn");
                //var rept = e.Row.FindControl("report_btn");                
                // hper1.Attributes.Add("onClick", "alert('Show Report')");
                //{
                //    (e.Row.FindControl("report_btn") as Button).Visible = true;
                //}


                //     hper1.Attributes.Add("onClick","fun()");  
                //    //  (e.Row.FindControl("report_btn") as Button).Visible = true;
                //    //hper1.Attributes.Add("onclick", "(e.Row.FindControl('report_btn') as Button).Visible;return false;");
                //    //hper1.Attributes.Add("onlclick", "(e.Row.FindControl('report_btn').Visible;false true");

                //}

                //DataRowView mydatarow = (DataRowView)e.Row.DataItem;
                //if (string.IsNullOrEmpty(mydatarow["report"].ToString()))
                //{
                //    Button btn2 = (Button)e.Row.FindControl("report_btn");
                //    if(btn2 !=null)
                //    {
                //        btn2.Visible = false;
                //    }

                //}

                // (e.Row.FindControl("report_btn") as Button).Visible = true;




            }
            if (e.Row.Cells[17].Text == "Rejected")
            {
                //e.Row.Cells[13].BackColor = Color.White;
                e.Row.Cells[17].ForeColor = Color.Red;
               // (e.Row.FindControl("hlView") as HyperLink).Visible = false;
                (e.Row.FindControl("report_btn") as Button).Visible = false;
                (e.Row.FindControl("graph_btn") as Button).Visible = false;
            }

            //if(shadowAcceptance=="")
            //{
            //    e.Row.Cells[19].Visible = false;
            //    e.Row.Cells[20].Visible = false;  
            //}
            //if(shadowAcceptance=="1")
            //{                
            //    e.Row.Cells[19].Visible = false;
            //    e.Row.Cells[20].Visible = false;
            //}


            
            //if (e.Row.Cells[13].Text != "Assigned")
            //{
            //    (e.Row.FindControl("hlView") as HyperLink).Visible = false;
            //}
            //else
            //{
            //    (e.Row.FindControl("hlView") as HyperLink).Visible = true;
            //}
                      
            //Session["StudId"] = Convert.ToInt32(e.Row.Cells[2].Text);
            //int stuid = Convert.ToInt32(HttpContext.Current.Session["StudId"]);
            //var StudId = Convert.ToInt32(e.Row.Cells[2].Text);
            //// var stud = Convert.ToInt32(Session["StudId"]).ToString();
            //if (Convert.ToInt32(Session["StudId"]) == StudId)
            //{
            //    HyperLink Hlnk = e.Row.FindControl("HyperLink1") as HyperLink;
            //    Hlnk.Visible = false;
            //}

            //else
            //{
            //    HyperLink Hlnk = e.Row.FindControl("hlView") as HyperLink;
            //    Hlnk.Visible = true;
            //}

            //e.Row.Cells[12].Attributes.Add("onClick", "alert('Cell Graph Clicked');");
            //if (e.Row.Cells[2].Text == search)
            //{
            //    var link = e.Row.FindControl("HyperLink1") as HyperLink;
            //    if (link != null)
            //        link.Visible = true;

            //}

          
        }
        
    }

   

    protected void Gridmysession_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "RequsetOTP")   
        switch (e.CommandName)
        {
            case "RequsetOTP":
               
                string a = e.CommandArgument.ToString();
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int studentId = Convert.ToInt32(commandArgs[2].ToString());

            Session["studentId"] = studentId;
            string command = commandArgs[0].ToString();
            string command1 = commandArgs[1].ToString();           
            if (command == "Accepted" && command1 == "True")
            { 
                string StudetnName, ContactNo;
                int OTP = GenerateRandomNo();
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd1 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Cid", studentId);
                    cmd1.Parameters.AddWithValue("@token", OTP);
                    cmd1.Parameters.AddWithValue("@SP_Type", "INSERT");
                    con.Open();
                    int i = cmd1.ExecuteNonQuery();
                    con.Close();
                        if (i > 0)
                        {
                            string query = " select fname,lname,contactNo,email from tbl_candidate_master where id='" + studentId + "'";
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Rows.Count > 0)
                                {
                                    StudetnName = ds.Rows[0]["fname"].ToString();
                                    ContactNo = ds.Rows[0]["contactNo"].ToString();

                                    //send OTP on User SMS
                                    string SMSText = ConfigurationManager.AppSettings["sessionCompleteOTP"].ToString();
                                    SMSText = SMSText.Replace("{Name}", "" + StudetnName);
                                    SMSText = SMSText.Replace("{OTP}", "" + OTP);
                                    sendSms(ContactNo, SMSText);

                                    ViewState["OTP"] = OTP;
                                    ViewState["studentId"] = studentId;
                                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                                   
                                    //Txtsubmit.Visible = true;
                                    //btnsumit.Visible = true;

                                }
                            }                       
                        
                    }
                }                  

                }

                break;
            case "report": 
                string b = e.CommandArgument.ToString();
                string[] commandArg = e.CommandArgument.ToString().Split(new char[] { ',' });
                //   int comd = Convert.ToInt32(e.CommandArgument);
                int productId = Convert.ToInt32(commandArg[0].ToString());
                int c_id = Convert.ToInt32(commandArg[1].ToString());
                //int c_id = Convert.ToInt32(Request.QueryString["StudId"]);
                Session["productId"] = productId;
                Session["c_id"] = c_id;

                //GetDataItem Batid from Query
                string stud1 = Session["c_id"].ToString();
                SqlCommand cm = new SqlCommand("select batid from tblKYCandFactors1 where c_id='" + stud1 + "'");
                cm.Connection = con11;
                if (con11.State == System.Data.ConnectionState.Closed)
                {
                    con11.Open();
                }
                SqlDataAdapter sd = new SqlDataAdapter(cm);
                DataSet ds1 = new DataSet();
                sd.Fill(ds1);
                int batid = Convert.ToInt32(ds1.Tables[0].Rows[0]["batid"]);
                Session["batid"] = batid;




                if (productId == 2 || productId == 3)
                {
                    Response.Redirect("~/Graph_Report/Half_Report_By_RAPD1_ForCDF.aspx?studtId=" + c_id + "");
                 

                }
                else if(productId==12 || productId==13)
                {
                    Response.Redirect("~/Graph_Report/ReportDownload.aspx?c_id=" + c_id);
                   
                }
                break;

            case "graph":

                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myModalloader", "$('#myModalloader').modal();", true);
               // ScriptManager.RegisterStartupScript(this, this.GetType(),"loaderPopup", "openloader();", true);
                Thread.Sleep(5000);

                string g = e.CommandArgument.ToString();
                string[] commandg = e.CommandArgument.ToString().Split(new char[] { ',' });
                //   int comd = Convert.ToInt32(e.CommandArgument);
                int studID = Convert.ToInt32(commandg[0].ToString());
                
                //int c_id = Convert.ToInt32(Request.QueryString["StudId"]);                
                Session["studID"] = studID;
                Response.Redirect("~/Graph_Report/ViewGrah_Scheduling.aspx?StudId=" + studID);
                break;

        }
    }

    protected void dtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "report":
                //Thread.Sleep(5000);
                string b = e.CommandArgument.ToString();
                string[] commandArg = e.CommandArgument.ToString().Split(new char[] { ',' });
                //   int comd = Convert.ToInt32(e.CommandArgument);
                int productId = Convert.ToInt32(commandArg[0].ToString());
                int c_id = Convert.ToInt32(commandArg[1].ToString());

                Session["studid"] = c_id;

                //GetDataItem Batid from Query
                string stud1 = Session["studid"].ToString();
                SqlCommand cm = new SqlCommand("select batid from tblKYCandFactors1 where c_id='" + stud1 + "'");
                cm.Connection = con11;
                if (con11.State == System.Data.ConnectionState.Closed)
                {
                    con11.Open();
                }
                SqlDataAdapter sd = new SqlDataAdapter(cm);
                DataSet ds1 = new DataSet();
                sd.Fill(ds1);
                int batid = Convert.ToInt32(ds1.Tables[0].Rows[0]["batid"]);
                Session["batid"] = batid;
             


                //int c_id = Convert.ToInt32(Request.QueryString["StudId"]);
                Session["productId"] = productId;
                Session["c_id"] = c_id;

                if (productId == 2 || productId == 3)
                {
                    Response.Redirect("~/Graph_Report/Half_Report_By_RAPD1_ForCDF.aspx?studtId=" + c_id );

                }
                else if (productId == 12 || productId == 13)
                {
                    Response.Redirect("~/Graph_Report/ReportDownload.aspx?c_id=" + c_id);
                }
                break;

            case "graph":
                Thread.Sleep(5000);
                string g = e.CommandArgument.ToString();
                string[] commandg = e.CommandArgument.ToString().Split(new char[] { ',' });
                //   int comd = Convert.ToInt32(e.CommandArgument);
                int studID = Convert.ToInt32(commandg[0].ToString());

                //int c_id = Convert.ToInt32(Request.QueryString["StudId"]);                
                Session["studID"] = studID;
                Response.Redirect("~/Graph_Report/ViewGrah_Scheduling.aspx?StudId=" + studID);
                break;

            case "feedback":
                string g1 = e.CommandArgument.ToString();
                string[] commandg1 = e.CommandArgument.ToString().Split(new char[] { ',' });
                int studiD = Convert.ToInt32(commandg1[0].ToString());
                // int studid = Convert.ToInt32(Session["studID"]);
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(@"select sfq.Ques_no as Quesno,sfq.Ques_text as QuesText,spfa.Ques_Answer as AnsText from tblStudentFeedbackQuestions as sfq inner join tblStudentProfessionalFeedbackAns as spfa on sfq.Ques_no=spfa.Ques_no where spfa.User_Id='" + studiD + "'", con);
                    con.Open();
                    // SqlDataReader sdr = cmd.ExecuteReader();

                    DataSet dt = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Tables[0].Rows.Count != 0)
                    {                       
                        feedbacklbl.Visible = false;
                        feedbackdiv.Visible = true;
                        //feedbackGrid.DataSource = dt;
                        //feedbackGrid.DataBind();
                        lblQno.Text = dt.Tables[0].Rows[0]["Quesno"].ToString();
                        lblQ.Text = dt.Tables[0].Rows[0]["QuesText"].ToString();
                        lblAns.Text = dt.Tables[0].Rows[0]["AnsText"].ToString();

                        lblQno1.Text = dt.Tables[0].Rows[1]["Quesno"].ToString();
                        lblQ1.Text = dt.Tables[0].Rows[1]["QuesText"].ToString();
                        lblAns1.Text = dt.Tables[0].Rows[1]["AnsText"].ToString();

                        lblQno2.Text = dt.Tables[0].Rows[2]["Quesno"].ToString();
                        lblQ2.Text = dt.Tables[0].Rows[2]["QuesText"].ToString();
                        lblAns2.Text = dt.Tables[0].Rows[2]["AnsText"].ToString();

                        lblQno3.Text = dt.Tables[0].Rows[3]["Quesno"].ToString();
                        lblQ3.Text = dt.Tables[0].Rows[3]["QuesText"].ToString();
                        lblAns3.Text = dt.Tables[0].Rows[3]["AnsText"].ToString();

                        lblQno4.Text = dt.Tables[0].Rows[4]["Quesno"].ToString();
                        lblQ4.Text = dt.Tables[0].Rows[4]["QuesText"].ToString();

                        if(dt.Tables[0].Rows[4]["AnsText"].ToString() == "2")
                        {
                            //lblAns4.Text += "<img src='http://localhost:1325/images/Star/2.jpg'>";
                        }
                       // lblAns4.Text = dt.Tables[0].Rows[4]["AnsText"].ToString();

                        lblQno5.Text = dt.Tables[0].Rows[5]["Quesno"].ToString();
                        lblQ5.Text = dt.Tables[0].Rows[5]["QuesText"].ToString();
                        lblAns5.Text = dt.Tables[0].Rows[5]["AnsText"].ToString();

                       
                    }
                    
                    else
                    {
                        
                        lblQno.Text = "";
                        lblQ.Text = "";
                        lblAns.Text = "";

                        lblQno1.Text = "";
                        lblQ1.Text = "";
                        lblAns1.Text = "";

                        lblQno2.Text = "";
                        lblQ2.Text = "";
                        lblAns2.Text = "";

                        lblQno3.Text = "";
                        lblQ3.Text = "";
                        lblAns3.Text = "";

                        lblQno4.Text = "";
                        lblQ4.Text = "";
                        lblAns4.Text = "";

                        lblQno5.Text = "";
                        lblQ5.Text = "";
                        lblAns5.Text = "";
                        feedbackdiv.Visible = false;
                        feedbacklbl.Visible = true;
                        feedbacklbl.Text = "Session Feedback Not Available";
                    }

                    //if (sdr.HasRows)
                    //{


                    //    while (sdr.Read())
                    //    {

                    //        lblQueNo.Text = sdr["Quesno"].ToString();
                    //        lblQus.Text = sdr["QuesText"].ToString();
                    //        lblAns.Text = sdr["AnsText"].ToString();

                    //    }
                    ScriptManager.RegisterStartupScript(this,GetType(), "Pop", "openpopup();", true);
                
                con.Close();
        }

        break;

    
        }

    }


    //Generate Random number
    public int GenerateRandomNo()
    {
        Random _rdm = new Random();
        return _rdm.Next(1000, 9999);
    }

    //Send SMS Function
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
            reader.Close();
            stream.Close();
            return true;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return false;
        }
    }
    //submit button
    protected void btn_sendOTP_Click(object sender, EventArgs e)
    {       
            string OTP = ViewState["OTP"].ToString();
            string studentId = ViewState["studentId"].ToString();
            bool flag = false;  
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                     if (OTP == txtReqOTP.Text)
                   // if (OTP == btn_sendOTP.Text)
                    {
                        string query = " select * from tbl_SessionCompletionOTP where status='ACTIVE' and Cid='" + studentId + "' and token='" + OTP + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            flag = true;
                        }
                        else { flag = false; }
                        con.Close();
                        if (flag == true)
                        {
                            SqlCommand cmd1 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@Cid", studentId);
                            cmd1.Parameters.AddWithValue("@token", OTP);
                            cmd1.Parameters.AddWithValue("@SP_Type", "UPDATE");
                            con.Open();
                            int i = cmd1.ExecuteNonQuery();
                            con.Close();
                            if (i > 0)
                            {
                                SqlCommand cmd2 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue("@Cid", studentId);
                                cmd2.Parameters.AddWithValue("@CDFid", Session["uid"]);
                                cmd2.Parameters.AddWithValue("@SP_Type", "UPDATE_SESSION_STATUS");
                                con.Open();
                                int j = cmd2.ExecuteNonQuery();
                                con.Close();
                                if (j > 0)
                                {
                                
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Session Completed Successfully...');window.location ='session.aspx';", true);

                                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('hiiii')", true);

                                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();window.location ='session.aspx'",  true);
                                //Page.Response.Redirect(Page.Request.Url.ToString(), false);
                                //Context.ApplicationInstance.CompleteRequest();

                                lblGrid();
                                //Txtsubmit.Visible = false;
                                //btnsumit.Visible = false;
                            }
                            }
                            else
                            {
                            string str = "<script>alert('Something went wrong...Resend OTP')</script>";
                                Response.Write(str);
                            }
                        }
                    }
                    //else if (Txtsubmit.Text == "")
                    //{
                    //    OTP_Erro.Visible = true;
                    //    OTP_Erro.Text = "Please Enter OTP";
                    //}

                    else
                    {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Wrong OTP...');", true);
                    OTP_Error.Visible = true;
                    OTP_Error.Text = "Wrong OTP...";

                    //OTP_Erro.Visible = true;
                    //OTP_Erro.Text = "Wrong OTP...";
                    //Response.Write("<script>alert('Wrong OTP')</script> ");

                    //   new Change  OTP Textbox
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccess", " OTP_Error.Text = ('Wrong OTP..')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),"worngOTP" , "< script > OTP_Error.Text('Wrong OTP') </ script >", true );
                }
                }

            }



            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        
        
    }

    protected void dtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dtl.PageIndex = e.NewPageIndex;
        history();

    }

    protected void Gridmysession_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridmysession.PageIndex = e.NewPageIndex;
        lblGrid();

    }
    //Resend OTP Button
    protected void btn_Resend_Click(object sender, EventArgs e)
    {
        OTP_Error.Visible = false;
        int studentId=Convert.ToInt32(Session["studentId"]);      
        string StudetnName, ContactNo;
        int OTP = GenerateRandomNo();
        using (SqlConnection con = new SqlConnection(connStr))
        {           
            SqlCommand cmd1 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Cid", studentId);
            cmd1.Parameters.AddWithValue("@token", OTP);
            cmd1.Parameters.AddWithValue("@SP_Type", "INSERT");
            con.Open();
            int i = cmd1.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                string query = " select fname,lname,contactNo,email from tbl_candidate_master where id='" + studentId + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
               
                if (ds != null)
                {                   
                    if (ds.Rows.Count > 0)
                    {
                        StudetnName = ds.Rows[0]["fname"].ToString();
                        ContactNo = ds.Rows[0]["contactNo"].ToString();

                        //send OTP on User SMS
                        string SMSText = ConfigurationManager.AppSettings["sessionCompleteOTP"].ToString();
                        SMSText = SMSText.Replace("{Name}", "" + StudetnName);
                        SMSText = SMSText.Replace("{OTP}", "" + OTP);
                        sendSms(ContactNo, SMSText);
                        ViewState["OTP"] = OTP;
                        ViewState["studentId"] = studentId;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                    }
                }
                else
                {

                }

            }


        }
    }

  
}
            
        
    
