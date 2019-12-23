using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Reflection;
using WebChart;
using System.Drawing;
using System.Data;
using log4net;

public partial class MobileAppReports_ViewGrah_Scheduling : System.Web.UI.Page
{
    static string connStr = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
    string name = "", username = "", age = "";
    string strcmd;
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    int c_id = 0;
    // Declare parameters to use for assessment
    int BlackM;
    int BlackL;
    int BlueM;
    int BlueL;
    int RedM;
    int RedL;
    int GreenM;
    int GreenL;
    int Hole;
    int DiffB;
    int DiffR;
    int DiffBl;
    int DiffG;
    MobileDAL dal = new MobileDAL();
    int batid = 1;
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //check the user type 
    //        if (Session.Count > 0 && Session["user_type"].ToString().Equals("Admin"))
    //        {
    //            this.Page.MasterPageFile = "~/AdminMaster.master";
    //        }
    //        else
    //        {
    //            this.Page.MasterPageFile = "~/StaffMasterPage.master";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        this.Page.MasterPageFile = "~/AdminMaster.master";
    //    }

    //}
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {
              
                c_id = Convert.ToInt32(Request.QueryString["StudId"]);              

                if (c_id > 0)
                {
                  //  string connectionString =ConfigurationManager.ConnectionStrings["DBContext1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connStr))
                    {

                        //Get Personal Details
                         strcmd = "SELECT fname,lname,(DATEPART(yyyy,regDateTime)-DATEPART(yyyy,dob)) as age,email FROM tbl_Candidate_Master where Id=" + c_id;
                       // strcmd = "select fname, lname,(DATEPART(yyyy, regDateTime) - DATEPART(yyyy, dob)) as age, standard, regDateTime from tbl_candidate_master where Id =" + c_id;

                        SqlCommand cmd = new SqlCommand(strcmd, con);
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            name = sdr["fname"].ToString() + " " + sdr["lname"].ToString();
                            age = sdr["age"].ToString();
                            username = sdr["email"].ToString();

                            lbl_name.Text = name;
                            lbl_username.Text = "Username - " + username;
                            lbl_age.Text = "Age - " + age;

                            con.Close();

                            //lblCand_name.Text = Session["cand_name"].ToString();
                            Boolean flag = dal.get_values1(c_id, batid);
                            if (flag)
                            {
                                BlueM = dal.BLUEM;
                                BlueL = dal.BLUEL;
                                RedM = dal.REDM;
                                RedL = dal.REDL;
                                BlackM = dal.BLACKM;
                                BlackL = dal.BLACKL;
                                GreenM = dal.GREENM;
                                GreenL = dal.GREENL;
                                Hole = dal.HOLE;

                                DiffB = BlueM - BlueL;
                                DiffR = RedM - RedL;
                                DiffBl = BlackM - BlackL;
                                DiffG = GreenM - GreenL;

                                lblBM.Text = Convert.ToString(BlueM);
                                lblBL.Text = Convert.ToString(BlueL);
                                lblRM.Text = Convert.ToString(RedM);
                                lblRL.Text = Convert.ToString(RedL);
                                lblBlM.Text = Convert.ToString(BlackM);
                                lblBlL.Text = Convert.ToString(BlackL);
                                lblGM.Text = Convert.ToString(GreenM);
                                lblGL.Text = Convert.ToString(GreenL);
                                lblDiffB.Text = Convert.ToString(DiffB);
                                lblDiffR.Text = Convert.ToString(DiffR);
                                lblDiffBl.Text = Convert.ToString(DiffBl);
                                lblDiffG.Text = Convert.ToString(DiffG);
                                lblTotal.Text = Convert.ToString(Hole);

                                //  CreateChart1();
                                LineChart chart1 = new LineChart();
                                chart1.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                                chart1.Line.Color = Color.SteelBlue;
                                chart1.Line.Width = 2;

                                dal.set_values();
                                BlueM = dal.DM;
                                RedM = dal.IM;
                                BlackM = dal.SM;
                                GreenM = dal.CM;

                                chart1.Legend = "RAPD GRAPH 1 information";
                                chart1.Data.Add(new ChartPoint("R", BlueM));
                                chart1.Data.Add(new ChartPoint("A", RedM));
                                chart1.Data.Add(new ChartPoint("P", BlackM));
                                chart1.Data.Add(new ChartPoint("D", GreenM));


                                ConfigureColors1();

                                ChartControl1.Charts.Add(chart1);
                                ChartControl1.RedrawChart();

                                //  CreateChart2();

                                LineChart chart2 = new LineChart();
                                chart2.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                                chart2.Line.Color = Color.SteelBlue;
                                chart2.Line.Width = 2;

                                dal.set_values();
                                BlueL = dal.DL;
                                RedL = dal.IL;
                                BlackL = dal.SL;
                                GreenL = dal.CL;

                                chart2.Legend = "RAPD GRAPH 2 information";
                                chart2.Data.Add(new ChartPoint("R", BlueL));
                                chart2.Data.Add(new ChartPoint("A", RedL));
                                chart2.Data.Add(new ChartPoint("P", BlackL));
                                chart2.Data.Add(new ChartPoint("D", GreenL));

                                ConfigureColors2();
                                ChartControl2.Charts.Add(chart2);
                                ChartControl2.RedrawChart();

                                //CreateChart3();
                                LineChart chart3 = new LineChart();
                                chart3.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                                chart3.Line.Color = Color.SteelBlue;
                                chart3.Line.Width = 2;

                                dal.set_values();
                                DiffB = dal.DD;
                                DiffR = dal.ID;
                                DiffBl = dal.SD;
                                DiffG = dal.CD;

                                chart3.Legend = "RAPD GRAPH 3 information";
                                chart3.Data.Add(new ChartPoint("R", DiffB));
                                chart3.Data.Add(new ChartPoint("A", DiffR));
                                chart3.Data.Add(new ChartPoint("P", DiffBl));
                                chart3.Data.Add(new ChartPoint("D", DiffG));

                                ConfigureColors3();

                                ChartControl3.Charts.Add(chart3);
                                ChartControl3.RedrawChart();


                                strcmd = "SELECT * FROM tblCandRAPDScore1 WHERE c_id=" + c_id;
                                SqlCommand cmdRAPD = new SqlCommand(strcmd, con);
                                con.Open();
                                SqlDataReader sdrRAPD = cmdRAPD.ExecuteReader();
                                if (sdrRAPD.HasRows)
                                {
                                    sdrRAPD.Close();
                                    SqlCommand updateRAPD = new SqlCommand("update tblCandRAPDScore1 set Rscore=@Rscore,Ascore=@Ascore,Pscore=@Pscore,Dscore=@Dscore where c_id=@c_id", con);
                                    updateRAPD.Parameters.AddWithValue("@c_id", c_id);
                                    updateRAPD.Parameters.AddWithValue("@Rscore", DiffB);
                                    updateRAPD.Parameters.AddWithValue("@Ascore", DiffR);
                                    updateRAPD.Parameters.AddWithValue("@Pscore", DiffBl);
                                    updateRAPD.Parameters.AddWithValue("@Dscore", DiffG);
                                    int intEffectedRows = updateRAPD.ExecuteNonQuery();
                                    
                                }
                                else
                                {
                                    sdrRAPD.Close();

                                    SqlCommand updateRAPD = new SqlCommand("insert into tblCandRAPDScore1 (c_id,Rscore,Ascore,Pscore,Dscore) values(@c_id,@Rscore,@Ascore,@Pscore,@Dscore)", con);
                                    updateRAPD.Parameters.AddWithValue("@Rscore", DiffB);
                                    updateRAPD.Parameters.AddWithValue("@Ascore", DiffR);
                                    updateRAPD.Parameters.AddWithValue("@Pscore", DiffBl);
                                    updateRAPD.Parameters.AddWithValue("@Dscore", DiffG);
                                    updateRAPD.Parameters.AddWithValue("@c_id", c_id);
                                    int intEffectedRows = updateRAPD.ExecuteNonQuery();

                                }

                            }
                            else
                            {
                                Log.Warn("Something went wrong");
                                Response.Redirect("ErrorPage.aspx", false);
                            }

                        }
                        else
                        {
                            Log.Warn("User Not Registered");
                            Response.Redirect("ErrorPage.aspx", false);

                        }


                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write("Error - " + ex);
                Log.Error("" + ex);
                Response.Redirect("ErrorPage.aspx", false);
            }
        }
    }
    private void ConfigureColors1()
    {
        ChartControl1.Background.Color = Color.FromArgb(75, Color.SteelBlue);
        ChartControl1.Background.Type = InteriorType.LinearGradient;
        ChartControl1.Background.ForeColor = Color.SteelBlue;
        ChartControl1.Background.EndPoint = new Point(500, 350);
        ChartControl1.Legend.Position = LegendPosition.Bottom;
        ChartControl1.Legend.Width = 40;

        ChartControl1.YAxisFont.ForeColor = Color.SteelBlue;
        ChartControl1.XAxisFont.ForeColor = Color.SteelBlue;

        ChartControl1.ChartTitle.Text = "HOW OTHERS SEE YOU";
        ChartControl1.ChartTitle.ForeColor = Color.White;

        ChartControl1.Border.Color = Color.SteelBlue;
        ChartControl1.BorderStyle = BorderStyle.Ridge;


    }

    private void ConfigureColors2()
    {

        ChartControl2.Background.Color = Color.FromArgb(75, Color.SteelBlue);
        ChartControl2.Background.Type = InteriorType.LinearGradient;
        ChartControl2.Background.ForeColor = Color.SteelBlue;
        ChartControl2.Background.EndPoint = new Point(500, 350);
        ChartControl2.Legend.Position = LegendPosition.Bottom;
        ChartControl2.Legend.Width = 40;

        ChartControl2.YAxisFont.ForeColor = Color.SteelBlue;
        ChartControl2.XAxisFont.ForeColor = Color.SteelBlue;

        ChartControl2.ChartTitle.Text = "BEHAVIOUR UNDER PRESSURE";
        ChartControl2.ChartTitle.ForeColor = Color.White;

        ChartControl2.Border.Color = Color.SteelBlue;
        ChartControl2.BorderStyle = BorderStyle.Ridge;
    }

    private void ConfigureColors3()
    {
        ChartControl3.Background.Color = Color.FromArgb(75, Color.SteelBlue);
        ChartControl3.Background.Type = InteriorType.LinearGradient;
        ChartControl3.Background.ForeColor = Color.SteelBlue;
        ChartControl3.Background.EndPoint = new Point(500, 350);
        ChartControl3.Legend.Position = LegendPosition.Bottom;
        ChartControl3.Legend.Width = 40;

        ChartControl3.YAxisFont.ForeColor = Color.SteelBlue;
        ChartControl3.XAxisFont.ForeColor = Color.SteelBlue;

        ChartControl3.ChartTitle.Text = "HOW YOU SEE YOURSELF";
        ChartControl3.ChartTitle.ForeColor = Color.White;

        ChartControl3.Border.Color = Color.SteelBlue;
        ChartControl3.BorderStyle = BorderStyle.Ridge;
    }



}