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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text.RegularExpressions;


public partial class Candidate_DownloadGraph : System.Web.UI.Page
{
    db_Xaction clsXaction = new db_Xaction();
    dal clsdal = new dal();
    int cccc = 0;
    string name = "",username="",age="";
    Document doc = new Document();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] != null)
        {
            try
            {
                int c_id;

                c_id = Convert.ToInt32(Request.QueryString["c_id"]);
                if (c_id > 0)
                {
                    #region load Graph

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


                    strcmd = "select c_first_name,c_last_name,c_middle_Name,c_username,c_age_years from tbl_candidate_master where c_id = " + c_id;
                    DataSet ds = clsXaction.ExecDataSet(strcmd);
                    name = ds.Tables[0].Rows[0][0].ToString().Trim() + " " + ds.Tables[0].Rows[0][2].ToString().Trim() + " " + ds.Tables[0].Rows[0][1].ToString().Trim();
                    username = ds.Tables[0].Rows[0][3].ToString();
                    age = ds.Tables[0].Rows[0][4].ToString();
                    lbl_name.Text = name;

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
                    chart1.Fill.Color = System.Drawing.Color.FromArgb(50, System.Drawing.Color.SteelBlue);
                    chart1.Line.Color = System.Drawing.Color.SteelBlue;
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
                    chart2.Fill.Color = System.Drawing.Color.FromArgb(50, System.Drawing.Color.SteelBlue);
                    chart2.Line.Color = System.Drawing.Color.SteelBlue;
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
                    chart3.Fill.Color = System.Drawing.Color.FromArgb(50, System.Drawing.Color.SteelBlue);
                    chart3.Line.Color = System.Drawing.Color.SteelBlue;
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

                    ConfigureColors3();
                    ChartControl3.Charts.Add(chart3);
                    ChartControl3.RedrawChart();



                    String StrSql = "";
                    StrSql = "SELECT * FROM tbl_candidate_RAPD_Score WHERE c_id='" + c_id + "'";
                    DataSet DsDup = clsdal.ExecDataSet(StrSql);
                    StrSql = "";
                    if (DsDup.Tables[0].Rows.Count == 0)
                    {

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
                    #endregion

                    #region Graph_page

                    //cover page of docoment.
                    doc.NewPage();
                    PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~") + "/Candidate/Reports_pdf/Graph_" + name.Replace(' ', '_') + ".pdf", FileMode.Create));
                    doc.Open();

                    iTextSharp.text.Image dheyalogo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/LOGO-NEW.png"));
                    //jpeg.ScalePercent(35f);
                    dheyalogo.ScaleToFit(50f, 50f);
                    dheyalogo.SetAbsolutePosition(30, 25);
                    // jpeg.SpacingAfter = -50f;
                    doc.Add(dheyalogo);

                    Paragraph headname = new Paragraph(name, FontFactory.GetFont("Bookman Old Style", 18, iTextSharp.text.Color.BLACK));
                    headname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(headname);







                    headname = new Paragraph("Username :- " + username + "   Age :- " + age, FontFactory.GetFont("times", 14, iTextSharp.text.Color.BLACK));
                    headname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(headname);

                    headname = new Paragraph("Personality Test", FontFactory.GetFont("", 14));
                    headname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(headname);

                    headname = new Paragraph("Following is the result of Personality Test", FontFactory.GetFont("Bookman Old Style", 12, iTextSharp.text.Font.COURIER));
                    headname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(headname);


                    iTextSharp.text.Table PDTopTable2 = new iTextSharp.text.Table(4);
                    PDTopTable2.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    PDTopTable2.Width = 50;
                    PDTopTable2.Padding = 1;




                    PDTopTable2.AddCell(new Cell(new Paragraph(" ", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  M", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  L", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  DIFF", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  R", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblBM.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblBL.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblDiffB.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  A", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblRM.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblRL.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblDiffR.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  P", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblBlM.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblBlL.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblDiffBl.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  D", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblGM.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblGL.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + lblDiffG.Text, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  ", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  Total", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph("  " + Hole, FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));
                    PDTopTable2.AddCell(new Cell(new Paragraph(" ", FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.COURIER))));




                    doc.Add(PDTopTable2);
                    headname = new Paragraph(" Following are the resultant graphs of Personality Test", FontFactory.GetFont("Bookman Old Style", 12, iTextSharp.text.Font.COURIER));
                    headname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(headname);

                    iTextSharp.text.Table PDTopTable1 = new iTextSharp.text.Table(3);
                    PDTopTable1.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    PDTopTable1.Width = 100;

                    iTextSharp.text.Image graph1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/WebCharts/" + ChartControl1.ImageID + ".png"));
                    PDTopTable1.AddCell(new Cell(graph1));

                    iTextSharp.text.Image graph2 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/WebCharts/" + ChartControl2.ImageID + ".png"));
                    PDTopTable1.AddCell(new Cell(graph2));

                    iTextSharp.text.Image graph3 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/WebCharts/" + ChartControl3.ImageID + ".png"));
                    PDTopTable1.AddCell(new Cell(graph3));


                    doc.Add(PDTopTable1);
                    doc.Close();

                    DownloadFile("Graph_" + name.Replace(' ', '_') + ".pdf", true);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }

    private void DownloadFile(string fname, bool forceDownload)
    {
        string path = Server.MapPath("~/Candidate/Reports_pdf/" + fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "";
        // set known types based on file extension  
        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".htm":
                case ".html":
                    type = "text/HTML";
                    break;

                case ".txt":
                    type = "text/plain";
                    break;

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
   

    private void ConfigureColors1()
    {
        ChartControl1.Background.Color = System.Drawing.Color.FromArgb(75, System.Drawing.Color.SteelBlue);
        ChartControl1.Background.Type = InteriorType.LinearGradient;
        ChartControl1.Background.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl1.Background.EndPoint = new Point(500, 350);
        ChartControl1.Legend.Position = LegendPosition.Bottom;
        ChartControl1.Legend.Width = 40;

        ChartControl1.YAxisFont.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl1.XAxisFont.ForeColor = System.Drawing.Color.SteelBlue;

        ChartControl1.ChartTitle.Text = "HOW OTHERS SEE YOU";
        ChartControl1.ChartTitle.ForeColor = System.Drawing.Color.White;

        ChartControl1.Border.Color = System.Drawing.Color.SteelBlue;
        ChartControl1.BorderStyle = BorderStyle.Ridge;
        ChartControl1.EnableViewState=true;
     
    }

    private void ConfigureColors2()
    {

        ChartControl2.Background.Color = System.Drawing.Color.FromArgb(75, System.Drawing.Color.SteelBlue);
        ChartControl2.Background.Type = InteriorType.LinearGradient;
        ChartControl2.Background.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl2.Background.EndPoint = new Point(500, 350);
        ChartControl2.Legend.Position = LegendPosition.Bottom;
        ChartControl2.Legend.Width = 40;

        ChartControl2.YAxisFont.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl2.XAxisFont.ForeColor = System.Drawing.Color.SteelBlue;

        ChartControl2.ChartTitle.Text = "BEHAVIOUR UNDER PRESSURE";
        ChartControl2.ChartTitle.ForeColor = System.Drawing.Color.White;

        ChartControl2.Border.Color = System.Drawing.Color.SteelBlue;
        ChartControl2.BorderStyle = BorderStyle.Ridge;
    }

    private void ConfigureColors3()
    {
        ChartControl3.Background.Color = System.Drawing.Color.FromArgb(75, System.Drawing.Color.SteelBlue);
        ChartControl3.Background.Type = InteriorType.LinearGradient;
        ChartControl3.Background.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl3.Background.EndPoint = new Point(500, 350);
        ChartControl3.Legend.Position = LegendPosition.Bottom;
        ChartControl3.Legend.Width = 40;

        ChartControl3.YAxisFont.ForeColor = System.Drawing.Color.SteelBlue;
        ChartControl3.XAxisFont.ForeColor = System.Drawing.Color.SteelBlue;

        ChartControl3.ChartTitle.Text = "HOW YOU SEE YOURSELF";
        ChartControl3.ChartTitle.ForeColor = System.Drawing.Color.White;

        ChartControl3.Border.Color = System.Drawing.Color.SteelBlue;
        ChartControl3.BorderStyle = BorderStyle.Ridge;
    }
   
}