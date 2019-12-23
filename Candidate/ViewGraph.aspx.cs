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

public partial class Candidate_ViewGraph : System.Web.UI.Page
{
    db_Xaction clsXaction = new db_Xaction();
    dal clsdal = new dal();
    int cccc = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //try
        //{
        //    //check the user type 
        //    if (Session.Count > 0 && Session["user_type"].ToString().Equals("Admin"))
        //        this.Page.MasterPageFile = "~/Admin/MasterPage.master";
        //    else
        //        this.Page.MasterPageFile = "~/Admin/StaffMasterPage.master";
        //}
        //catch (Exception ex)
        //{
        //    this.Page.MasterPageFile = "~/Admin/MasterPage.master";
        //}

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] != null)
        {
            if (Session.Count > 0)
            {

                int c_id;

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
                string strcmd;


                //////////////// end new changes //////////////


                //c_id = Convert.ToInt32(dataall.Tables[0].Rows[i][0].ToString());

                c_id = Convert.ToInt32(Request.QueryString["c_id"]);

                strcmd = "select c_first_name,c_last_name,c_middle_Name from tbl_candidate_master where c_id = " + c_id;
                DataSet ds = clsXaction.ExecDataSet(strcmd);
                lbl_name.Text = ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][2].ToString() + " " + ds.Tables[0].Rows[0][1].ToString();

                //lblCand_name.Text = Session["cand_name"].ToString();
                int id = clsXaction.get_values(c_id);
                BlueM = clsXaction.BLUEM;
                BlueL = clsXaction.BLUEL;
                RedM = clsXaction.REDM;
                RedL = clsXaction.REDL;
                BlackM = clsXaction.BLACKM;
                BlackL = clsXaction.BLACKL;
                GreenM = clsXaction.GREENM;
                GreenL = clsXaction.GREENL;
                Hole = clsXaction.HOLE;

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
                /////////////////// het function in side///


                LineChart chart1 = new LineChart();
                chart1.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                chart1.Line.Color = Color.SteelBlue;
                chart1.Line.Width = 2;

                clsXaction.set_values();
                BlueM = clsXaction.DM;
                RedM = clsXaction.IM;
                BlackM = clsXaction.SM;
                GreenM = clsXaction.CM;

                chart1.Legend = "RAPD GRAPH 1 information";
                chart1.Data.Add(new ChartPoint("R", BlueM));
                chart1.Data.Add(new ChartPoint("A", RedM));
                chart1.Data.Add(new ChartPoint("P", BlackM));
                chart1.Data.Add(new ChartPoint("D", GreenM));


                ConfigureColors1();

                ChartControl1.Charts.Add(chart1);
                ChartControl1.RedrawChart();




                ////////////end function


                //  CreateChart2();
                /////////////////// inside function 2


                LineChart chart2 = new LineChart();
                chart2.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                chart2.Line.Color = Color.SteelBlue;
                chart2.Line.Width = 2;

                clsXaction.set_values();
                BlueL = clsXaction.DL;
                RedL = clsXaction.IL;
                BlackL = clsXaction.SL;
                GreenL = clsXaction.CL;

                chart2.Legend = "RAPD GRAPH 2 information";
                chart2.Data.Add(new ChartPoint("R", BlueL));
                chart2.Data.Add(new ChartPoint("A", RedL));
                chart2.Data.Add(new ChartPoint("P", BlackL));
                chart2.Data.Add(new ChartPoint("D", GreenL));

                ConfigureColors2();
                ChartControl2.Charts.Add(chart2);
                ChartControl2.RedrawChart();



                /////////////// end function

                //CreateChart3();

                ////////////////////////////// third function inside 




                LineChart chart3 = new LineChart();
                chart3.Fill.Color = Color.FromArgb(50, Color.SteelBlue);
                chart3.Line.Color = Color.SteelBlue;
                chart3.Line.Width = 2;

                clsXaction.set_values();
                DiffB = clsXaction.DD;
                DiffR = clsXaction.ID;
                DiffBl = clsXaction.SD;
                DiffG = clsXaction.CD;


                chart3.Legend = "RAPD GRAPH 3 information";
                chart3.Data.Add(new ChartPoint("R", DiffB));
                chart3.Data.Add(new ChartPoint("A", DiffR));
                chart3.Data.Add(new ChartPoint("P", DiffBl));
                chart3.Data.Add(new ChartPoint("D", DiffG));




                String StrSql = "";
                StrSql = "SELECT * FROM tbl_candidate_RAPD_Score WHERE c_id='" + c_id + "'";
                DataSet DsDup = clsdal.ExecDataSet(StrSql);
                if (DsDup.Tables[0].Rows.Count == 0)
                {
                    StrSql = "";
                    StrSql = "INSERT INTO tbl_candidate_RAPD_Score VALUES ( " + c_id + "," + DiffB + ", " + DiffR + "," + DiffBl + "," + DiffG + ")";
                    int ie = clsdal.ExecNonQuery(StrSql);
                    cccc++;
                }
                else
                {
                    StrSql = "update tbl_candidate_RAPD_Score set Rscore=" + DiffB + ", Ascore=" + DiffR + ",Pscore=" + DiffBl + ",Dscore=" + DiffG + " where c_id=" + c_id;
                    int ie = clsdal.ExecNonQuery(StrSql);
                }

                DsDup.Clear();
                DsDup.Dispose();

                ConfigureColors3();

                ChartControl3.Charts.Add(chart3);
                ChartControl3.RedrawChart();





            }
            if (Session.Count <= 0)
            {
                // Response.Redirect("default.aspx");
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
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