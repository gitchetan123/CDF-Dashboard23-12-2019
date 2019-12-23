using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using dotnetCHARTING;

public partial class Corptest_CorpReport : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MobileDAL dal = new MobileDAL();
    Document doc = new Document(PageSize.A4, 30, 30, 30, 30);

    int BlackM;
    int BlackL;
    int BlueM;
    int BlueL;
    int RedM;
    int RedL;
    int GreenM;
    int GreenL;
    int DiffB;
    int DiffR;
    int DiffBl;
    int DiffG;
    int c_id;
    int batid = 0;
   
    string RScore, AScore, PScore, DScore;
    string candidate_name, c_name, age, dateof_reg, sqlquery;
    bool T1, T2, T3, TD1, TD2, TD3;
    string strsql, f_name, l_name, strcmd, emailid, contactno,gender,dob,city;
    string StrChartPath, StrChartPath1, StrChartPath2, StrChartPath3, StrChartPath4, StrChartPath5, StrChartPath6, StrChartPath7;
    
    protected void Page_Load(object sender, EventArgs e)
    {
         batid = Convert.ToInt32(Session["batid"]);
        load_rpt();
    }
    private void load_rpt()
    {
       
        int[] yy = { 10, 1, 10 };
        try
        {
            string cipherText = Request.QueryString["snm"].ToString();
            cipherText = HttpUtility.UrlDecode(cipherText);
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            c_id = Convert.ToInt32(cipherText);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong. Please try again.";
        }

        try
        {
            if (c_id > 0)
            {
                string connectionString =
                    //ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
                    ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    //   strcmd = "SELECT uId,fname,lname,gender, CONVERT(VARCHAR(15),dob,110) as dob,(DATEPART(yyyy,regDateTime)-DATEPART(yyyy,dob)) as age, contactNo,email,CONVERT(VARCHAR(15),regDateTime,110) as regDateTime,c.name as city FROM tblUserMaster as u left outer join tblCitiesMaster as c on u.cityid=c.id where uId=" + c_id;
                    strcmd = " SELECT cand.Id,cand.fname,cand.lname,cand.gender,CONVERT(VARCHAR(15), cand.dob, 110) as dob,(DATEPART(yyyy, cand.regDateTime) - DATEPART(yyyy, cand.dob)) as age, cand.contactNo,cand.email,CONVERT(VARCHAR(15), cand.regDateTime, 110) as regDateTime,c.name as city FROM tbl_Candidate_Master as cand left outer join tblCitiesMaster as c on cand.cityid = c.id where cand.Id =" + c_id;
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    SqlDataReader sdruserinfo = cmd.ExecuteReader();
                    if (sdruserinfo.HasRows)
                    {
                        sdruserinfo.Read();
                        f_name = sdruserinfo["fname"].ToString();
                        l_name = sdruserinfo["lname"].ToString();
                        age = sdruserinfo["age"].ToString();
                        gender = sdruserinfo["gender"].ToString();
                        dob = sdruserinfo["dob"].ToString();
                        dateof_reg = sdruserinfo["regDateTime"].ToString();
                        emailid = sdruserinfo["email"].ToString();
                        contactno = sdruserinfo["contactNo"].ToString();
                        city = sdruserinfo["city"].ToString();
                        c_name = f_name + "_" + l_name.ToString().Trim().Replace(' ', '_');

                        sdruserinfo.Close();
                        c_name = f_name + "_" + l_name.ToString().Trim();
                        iTextSharp.text.Image rveryhi = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/rveryhih.png"));
                        rveryhi.ScaleToFit(70f, 8f);
                        iTextSharp.text.Image rhigh = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/rhighh.png"));
                        rhigh.ScaleToFit(70f, 8f);
                        iTextSharp.text.Image rmodrate = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/rmodrateh.png"));
                        rmodrate.ScaleToFit(70f, 8f);
                        iTextSharp.text.Image rlow = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/rlowh.png"));
                        rlow.ScaleToFit(70f, 8f);
                        iTextSharp.text.Image rverylow = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/rverylowh.png"));
                        rverylow.ScaleToFit(70f, 8f);

                        int[] width = { 6, 1, 4 };

                        iTextSharp.text.Image veryhigh = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/veryhibar.png"));
                        iTextSharp.text.Image high = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/hibar.png"));
                        iTextSharp.text.Image middal = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/middelbar.png"));
                        iTextSharp.text.Image low = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/barlow.png"));
                        iTextSharp.text.Image verylow = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/verylow.png"));

                        iTextSharp.text.Table thirdTable1 = new iTextSharp.text.Table(5);
                        Cell thirdcell1 = new Cell();
                        thirdcell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        thirdcell1.Width = 2;
                        thirdcell1.BackgroundColor = new Color(225, 225, 0);
                        thirdcell1.BorderWidth = 4f;
                        thirdcell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell thirdcell2 = new Cell();
                        thirdcell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        thirdcell2.Width = 2;
                        thirdcell2.BackgroundColor = new Color(225, 225, 0);
                        thirdcell2.BorderWidth = 4f;
                        thirdcell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell thirdcell3 = new Cell();
                        thirdcell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        thirdcell3.Width = 2;
                        thirdcell3.BackgroundColor = new Color(225, 225, 0);
                        thirdcell3.BorderWidth = 4f;
                        thirdcell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell thirdcell4 = new Cell();
                        thirdcell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        thirdcell4.Width = 2;
                        thirdcell4.BackgroundColor = new Color(225, 225, 0);
                        thirdcell4.BorderWidth = 4f;
                        thirdcell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell thirdcell5 = new Cell();
                        thirdcell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        thirdcell5.Width = 2;
                        thirdcell5.BackgroundColor = new Color(225, 225, 0);
                        thirdcell5.BorderWidth = 4f;
                        thirdcell5.BorderColor = new Color(255, 255, 255);




                        Cell acell33 = new Cell();
                        acell33.Add(new Paragraph("\n"));
                        acell33.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        acell33.Border = 0;

                        iTextSharp.text.Image endblock = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/endblock.png"));
                        endblock.ScaleToFit(5f, 5f);
                        Chunk imageChunk1 = new Chunk(endblock, 0, 0);
                        Phrase limage = new Phrase();
                        limage.Add(imageChunk1);

                        PdfWriter.GetInstance(doc, new FileStream(Server.MapPath(".") + "/Reports_pdf/" + c_name.ToString().Trim() + "_Report.pdf", FileMode.Create));
                       
                        doc.Open();

                        candidate_name = c_name;
                        StrChartPath = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab1.png");
                        StrChartPath1 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab2.png");
                        StrChartPath2 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab3.png");
                        StrChartPath3 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab4.png");
                        StrChartPath4 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab5.png");
                        StrChartPath5 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab6.png");
                        StrChartPath6 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab7.png");
                        StrChartPath7 = Server.MapPath((".") + "/Reports_graph/" + candidate_name + "ab8.png");


                        //*****************************************************************************************************************
                        // code for disc report
                        //**************************************************************************************************************
                        // PdfContentByte cb = writer.DirectContent;
                        sqlquery = "select * from tblPersonalityCandAnswers where batid=" + batid + " and c_id =" + c_id;
                        DataSet ds_d = dal.ExecDataSet(sqlquery);
                        int n = ds_d.Tables[0].Rows.Count;
                        if (n == 24)
                        {
                            Boolean flag = dal.get_values(c_id, batid);
                            if (flag)
                            {
                                dal.set_values();
                                BlueM = dal.DM;
                                BlueL = dal.DL;
                                RedM = dal.IM;
                                RedL = dal.IL;
                                BlackM = dal.SM;
                                BlackL = dal.SL;
                                GreenM = dal.CM;
                                GreenL = dal.CL;

                                DiffB = dal.DD;
                                DiffR = dal.ID;
                                DiffBl = dal.SD;
                                DiffG = dal.CD;

                                if (DiffB > 0)
                                    RScore = "High"; //R Score
                                else
                                    RScore = "Low"; //R Score

                                if (DiffR > 0)
                                    AScore = "High"; //A Score
                                else
                                    AScore = "Low"; //A Score

                                if (DiffBl > 0)
                                    PScore = "High"; //P Score
                                else
                                    PScore = "Low"; //P Score

                                if (DiffG > 0)
                                    DScore = "High"; //D Score
                                else
                                    DScore = "Low"; //D Score


                                strcmd = "select * from tblPersonalityReportConditions where disc_report_type = 'Browser_Report' order by disc_condition_id";
                                DataSet ds4 = dal.ExecDataSet(strcmd);

                            }
                        }

                        //strcmd = "SELECT P_rating,rating,B.factor,A.factor_no FROM tblKYCandFactors as A,tblKYFactors as B where A.factor_no=B.factor_no and A.batid=" + batid + " and c_id = " + c_id + " order by P_rating desc";
                        //DataSet ds2 = dal.ExecDataSet(strcmd);

                        //strcmd = "SELECT P_rating,rating,B.factor,A.factor_no FROM tblKYCandFactors as A,tblKYFactors as B where A.factor_no=B.factor_no and A.batid=" + batid + " and c_id = " + c_id + " order by A.factor_no";
                        //DataSet dsss2 = dal.ExecDataSet(strcmd);

                        strcmd = "SELECT rating,P_rating,A.factor_no,B.factor FROM tblKYCandFactors1 as A,tblKYFactors as B where A.factor_no=B.factor_no and A.batid=" + batid + " and c_id = " + c_id + " order by P_rating desc";
                        DataSet ds1 = dal.ExecDataSet(strcmd);

                        strcmd = "SELECT rating,P_rating,A.factor_no,B.factor FROM tblKYCandFactors1 as A,tblKYFactors as B where A.factor_no=B.factor_no and A.batid=" + batid + " and c_id = " + c_id + " order by A.factor_no";
                        DataSet dsss1 = dal.ExecDataSet(strcmd);

                        doc.SetPageSize(new iTextSharp.text.Rectangle(PageSize.A4.Width, PageSize.A4.Height));

                        iTextSharp.text.Image dheyalogo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/Logo.png"));
                        dheyalogo.ScaleToFit(50f, 50f);
                        dheyalogo.SetAbsolutePosition(30, 25);

                        //Frist Page
                        doc.NewPage();
                        #region First Page
                        HeaderFooter footer1 = new HeaderFooter(new Phrase("                " + "                                                                                        " + "                                                                                          " + "PAGE: ", FontFactory.GetFont(FontFactory.HELVETICA, 8)), true);
                        footer1.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        footer1.BorderColor = new Color(255, 255, 255);
                        footer1.BorderWidth = 0f;
                        doc.Footer = footer1;

                        iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/corptestheader.png"));
                        jpeg.ScaleToFit(600f, 900f);
                        jpeg.SetAbsolutePosition(0, 0);
                        doc.Add(jpeg);
                        doc.Add(dheyalogo);

                        iTextSharp.text.Table adddddd = new iTextSharp.text.Table(1);
                        adddddd.Alignment = 0;
                        adddddd.DefaultCellBorder = 0;
                        adddddd.Border = 0;

                        Cell adddddd1 = new Cell();
                        adddddd1.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nPersonal Information\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 18, iTextSharp.text.Font.COURIER, new Color(0, 0, 0))));
                        adddddd.AddCell(adddddd1);
                        doc.Add(adddddd);

                        iTextSharp.text.Table TblStudinfo = new iTextSharp.text.Table(2);
                        TblStudinfo.Alignment = 0;
                        TblStudinfo.Width = 110;
                        TblStudinfo.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        TblStudinfo.DefaultCellBorder = 0;

                        TblStudinfo.Border = 0;

                        Cell CellStudname = new Cell();

                        CellStudname.Add(new Paragraph("Name : " + f_name + " " + l_name, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(CellStudname);

                        Cell CellRegdate = new Cell();
                        CellRegdate.Add(new Paragraph("Date : " + dateof_reg, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(CellRegdate);

                        Cell CellSchoolname = new Cell();
                        CellSchoolname.Add(new Paragraph("Contact No : " + contactno, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(CellSchoolname);

                        Cell CellEmail = new Cell();
                        CellEmail.Add(new Paragraph("Email ID: " + emailid, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(CellEmail);

                        Cell CellDOB = new Cell();
                        CellDOB.Add(new Paragraph("DOB: " + dob, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        CellDOB.Width = 20;
                        TblStudinfo.AddCell(CellDOB);

                        Cell CellAge = new Cell();
                        CellAge.Add(new Paragraph("Age: " + age + " Years", FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        CellAge.Width = 20;
                        TblStudinfo.AddCell(CellAge);

                        Cell Cellgender = new Cell();
                        Cellgender.Add(new Paragraph("Gender: " + gender, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(Cellgender);

                        Cell Cellcity = new Cell();
                        Cellcity.Add(new Paragraph("City: " + city, FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.COURIER)));
                        TblStudinfo.AddCell(Cellcity);

                        doc.Add(TblStudinfo);

                        #endregion

                        //second Page
                        doc.NewPage();
                        doc.Add(dheyalogo);

                        iTextSharp.text.Image sevenpagefirst1gg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/NCDAPPersonality.jpg"));
                        //jpeg.ScalePercent(35f);
                        sevenpagefirst1gg.ScaleToFit(600f, 550f);
                        sevenpagefirst1gg.SetAbsolutePosition(0, 600);
                        // jpeg.SpacingAfter = -50f;
                        doc.Add(sevenpagefirst1gg);

                        Paragraph p1gg = new Paragraph("\n\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(00, 00, 00)));
                        p1gg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(p1gg);

                        profile_conditions_pdf();

                        //third Page                       
                        doc.NewPage();
                        doc.Add(dheyalogo);
                        ps1();
                        // persnalitychart();

                        iTextSharp.text.Table PDTopTable999 = new iTextSharp.text.Table(3);
                        PDTopTable999.Alignment = 0;
                        PDTopTable999.Width = 100;
                        // PDTopTable9.Padding = 20f;
                        PDTopTable999.SetWidths(yy);
                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        PDTopTable999.DefaultCellBorder = 0;
                        PDTopTable999.Border = 0;

                        Cell PDTopCell25 = new Cell();

                        Paragraph pop1 = new Paragraph();
                        pop1.SetLeading(25.0f, 25.0f);
                        Phrase ph2 = new Phrase(18, new Chunk("SUMMARY OF YOUR PERSONALITY STRENGTHS ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase ph3 = new Phrase(18, new Chunk("Your personality from within is your identity. What you see from outside is just your behavior. However your behavior is driven by your attitude, your thinking, your values, your up-bringing etc. In the assessments we are assessing your personality based on 9 factors that define your behavior and that would make a difference on to your performance on a long term.We call this as your behavioral assets.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pop1.Add(ph2);
                        pop1.Add(ph3);
                        pop1.Add(limage);
                        PDTopCell25.Add(pop1);
                        PDTopCell25.Width = 80;
                        PDTopCell25.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;
                        PDTopTable999.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        PDTopTable999.AddCell(PDTopCell25);

                        PDTopTable999.AddCell(acell33);


                        //////////////////////////////////////add PERSONALITY STRENGTHS table#####################################################################
                        ///////////////////////////////////##################################/////////////////////////////////////////////////////////////////////

                        iTextSharp.text.Table PDTopTable11 = new iTextSharp.text.Table(4);
                        PDTopTable11.Alignment = 0;
                        PDTopTable11.Width = 100;

                        // PDTopTable10.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        //PDTopTable10.DefaultCellBorder = 0;
                        //PDTopTable10.Border = 0;

                        ////////////////////// Add table heading
                        Cell persnality_heading_cell1 = new Cell();
                        persnality_heading_cell1.Add(new Paragraph("Behavior", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD, new Color(255, 255, 255))));
                        persnality_heading_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        persnality_heading_cell1.Width = 80;

                        persnality_heading_cell1.BackgroundColor = new Color(28, 125, 206);
                        persnality_heading_cell1.BorderWidth = 2f;
                        persnality_heading_cell1.BorderColor = new Color(255, 255, 255);
                        persnality_heading_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        persnality_heading_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        PDTopTable11.AddCell(persnality_heading_cell1);

                        Cell persnality_heading_cell2 = new Cell();
                        persnality_heading_cell2.Add(new Paragraph("Rating", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD, new Color(255, 255, 255))));
                        persnality_heading_cell2.Width = 80;
                        persnality_heading_cell2.BackgroundColor = new Color(28, 125, 206);
                        persnality_heading_cell2.BorderWidth = 2f;
                        persnality_heading_cell2.BorderColor = new Color(255, 255, 255);
                        persnality_heading_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        persnality_heading_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        PDTopTable11.AddCell(persnality_heading_cell2);

                        Cell persnality_heading_cell3 = new Cell();
                        persnality_heading_cell3.Add(new Paragraph("Percentile", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD, new Color(255, 255, 255))));
                        persnality_heading_cell3.Width = 80;
                        persnality_heading_cell3.BackgroundColor = new Color(28, 125, 206);
                        persnality_heading_cell3.BorderWidth = 2f;
                        persnality_heading_cell3.BorderColor = new Color(255, 255, 255);
                        persnality_heading_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        persnality_heading_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        PDTopTable11.AddCell(persnality_heading_cell3);

                        Cell persnality_heading_cell4 = new Cell();
                        persnality_heading_cell4.Add(new Paragraph("Rating", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD, new Color(255, 255, 255))));
                        persnality_heading_cell4.Width = 80;
                        persnality_heading_cell4.BackgroundColor = new Color(28, 125, 206);
                        persnality_heading_cell4.BorderWidth = 2f;
                        persnality_heading_cell4.BorderColor = new Color(255, 255, 255);
                        persnality_heading_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        persnality_heading_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                        PDTopTable11.AddCell(persnality_heading_cell4);

                        ////////////////////// end table heading

                        /////////////////////// get marks of candidate 

                        //////////////////////// end get marks of candidate

                        ///////// table first row
                        Cell first_row_cell1 = new Cell();
                        String chart2lblname = "";
                        switch (ds1.Tables[0].Rows[0][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }


                        first_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        first_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        first_row_cell1.Width = 80;
                        first_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        first_row_cell1.BorderWidth = 2f;
                        first_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(first_row_cell1);

                        Cell first_row_cell2 = new Cell();


                        //////////////  rectangle of line 1
                        # region r11

                        iTextSharp.text.Table per1 = new iTextSharp.text.Table(5);
                        Cell per_thirdcell1 = new Cell();
                        per_thirdcell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_thirdcell1.Width = 2;
                        per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                        per_thirdcell1.BorderWidth = 4f;
                        per_thirdcell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_thirdcell2 = new Cell();
                        per_thirdcell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_thirdcell2.Width = 2;
                        per_thirdcell2.BackgroundColor = new Color(225, 225, 0);
                        per_thirdcell2.BorderWidth = 4f;
                        per_thirdcell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_thirdcell3 = new Cell();
                        per_thirdcell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_thirdcell3.Width = 2;
                        per_thirdcell3.BackgroundColor = new Color(225, 225, 0);
                        per_thirdcell3.BorderWidth = 4f;
                        per_thirdcell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_thirdcell4 = new Cell();
                        per_thirdcell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_thirdcell4.Width = 2;
                        per_thirdcell4.BackgroundColor = new Color(225, 225, 0);
                        per_thirdcell4.BorderWidth = 4f;
                        per_thirdcell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_thirdcell5 = new Cell();
                        per_thirdcell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_thirdcell5.Width = 2;
                        per_thirdcell5.BackgroundColor = new Color(225, 225, 0);
                        per_thirdcell5.BorderWidth = 4f;
                        per_thirdcell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[0][0].ToString())
                        {
                            case "Very Low":

                                per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell2.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell3.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell4.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell2.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell3.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell4.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell2.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell3.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell4.BackgroundColor = new Color(71, 65, 18);
                                per_thirdcell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell2.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell3.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell4.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_thirdcell1.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell2.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell3.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell4.BackgroundColor = new Color(225, 225, 0);
                                per_thirdcell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per1.AddCell(per_thirdcell1);
                        per1.AddCell(per_thirdcell2);
                        per1.AddCell(per_thirdcell3);
                        per1.AddCell(per_thirdcell4);
                        per1.AddCell(per_thirdcell5);
                        per1.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per1.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        ////////////////////////////////////////////////////


                        // first_row_cell2.Add(per1);
                        # endregion

                        switch (ds1.Tables[0].Rows[0][0].ToString())
                        {
                            case "Very Low":

                                first_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                first_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                first_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                first_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                first_row_cell2.Add(rveryhi);

                                break;
                        }



                        first_row_cell2.Width = 80;
                        first_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        first_row_cell2.BorderWidth = 2f;
                        first_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        first_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(first_row_cell2);

                        Cell first_row_cell3 = new Cell();

                        float fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[0][1].ToString());
                        int perf = (int)Math.Ceiling(fff);

                        first_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        first_row_cell3.Width = 80;
                        first_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        first_row_cell3.BorderWidth = 2f;
                        first_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(first_row_cell3);

                        Cell first_row_cell4 = new Cell();
                        first_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[0][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        first_row_cell4.Width = 80;
                        first_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        first_row_cell4.BorderWidth = 2f;
                        first_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        first_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(first_row_cell4);


                        /////////// table second row

                        Cell second_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[1][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }

                        second_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        second_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        second_row_cell1.Width = 80;
                        second_row_cell1.BorderWidth = 2f;
                        second_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(second_row_cell1);

                        Cell second_row_cell2 = new Cell();


                        //////////////  rectangle of line 2
                        # region r22
                        iTextSharp.text.Table per_line2rect = new iTextSharp.text.Table(5);
                        Cell per_line2rect_cell1 = new Cell();
                        per_line2rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line2rect_cell1.Width = 2;
                        per_line2rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line2rect_cell1.BorderWidth = 4f;
                        per_line2rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line2rect_cell2 = new Cell();
                        per_line2rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line2rect_cell2.Width = 2;
                        per_line2rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line2rect_cell2.BorderWidth = 4f;
                        per_line2rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line2rect_cell3 = new Cell();
                        per_line2rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line2rect_cell3.Width = 2;
                        per_line2rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line2rect_cell3.BorderWidth = 4f;
                        per_line2rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line2rect_cell4 = new Cell();
                        per_line2rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line2rect_cell4.Width = 2;
                        per_line2rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line2rect_cell4.BorderWidth = 4f;
                        per_line2rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line2rect_cell5 = new Cell();
                        per_line2rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line2rect_cell5.Width = 2;
                        per_line2rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line2rect_cell5.BorderWidth = 4f;
                        per_line2rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[1][0].ToString())
                        {
                            case "Very Low":

                                per_line2rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line2rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line2rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line2rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line2rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line2rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line2rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line2rect.AddCell(per_line2rect_cell1);
                        per_line2rect.AddCell(per_line2rect_cell2);
                        per_line2rect.AddCell(per_line2rect_cell3);
                        per_line2rect.AddCell(per_line2rect_cell4);
                        per_line2rect.AddCell(per_line2rect_cell5);
                        per_line2rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line2rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                        //second_row_cell2.Add(per_line2rect);

                        ////////////////////////////////////////////////////

                        # endregion

                        switch (ds1.Tables[0].Rows[1][0].ToString())
                        {
                            case "Very Low":

                                second_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                second_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                second_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                second_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                second_row_cell2.Add(rveryhi);

                                break;
                        }


                        second_row_cell2.Width = 80;
                        second_row_cell2.BorderWidth = 2f;
                        second_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        second_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(second_row_cell2);



                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[1][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        Cell second_row_cell3 = new Cell();
                        second_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        second_row_cell3.Width = 80;
                        second_row_cell3.BorderWidth = 2f;
                        second_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(second_row_cell3);

                        Cell second_row_cell4 = new Cell();
                        second_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[1][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        second_row_cell4.Width = 80;
                        second_row_cell4.BorderWidth = 2f;
                        second_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        second_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(second_row_cell4);


                        /////////////// table third row 

                        Cell third_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[2][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }


                        third_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        third_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        third_row_cell1.Width = 80;
                        third_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        third_row_cell1.BorderWidth = 2f;
                        third_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(third_row_cell1);

                        Cell third_row_cell2 = new Cell();




                        //////////////  rectangle of line 3

                        #region rr3


                        iTextSharp.text.Table per_line3rect = new iTextSharp.text.Table(5);
                        Cell per_line3rect_cell1 = new Cell();
                        per_line3rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line3rect_cell1.Width = 2;
                        per_line3rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line3rect_cell1.BorderWidth = 4f;
                        per_line3rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line3rect_cell2 = new Cell();
                        per_line3rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line3rect_cell2.Width = 2;
                        per_line3rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line3rect_cell2.BorderWidth = 4f;
                        per_line3rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line3rect_cell3 = new Cell();
                        per_line3rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line3rect_cell3.Width = 2;
                        per_line3rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line3rect_cell3.BorderWidth = 4f;
                        per_line3rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line3rect_cell4 = new Cell();
                        per_line3rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line3rect_cell4.Width = 2;
                        per_line3rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line3rect_cell4.BorderWidth = 4f;
                        per_line3rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line3rect_cell5 = new Cell();
                        per_line3rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line3rect_cell5.Width = 2;
                        per_line3rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line3rect_cell5.BorderWidth = 4f;
                        per_line3rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[2][0].ToString())
                        {
                            case "Very Low":

                                per_line3rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line3rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line3rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line3rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line3rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line3rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line3rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line3rect.AddCell(per_line3rect_cell1);
                        per_line3rect.AddCell(per_line3rect_cell2);
                        per_line3rect.AddCell(per_line3rect_cell3);
                        per_line3rect.AddCell(per_line3rect_cell4);
                        per_line3rect.AddCell(per_line3rect_cell5);
                        per_line3rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line3rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                        //third_row_cell2.Add(per_line3rect);

                        ////////////////////////////////////////////////////

                        # endregion



                        switch (ds1.Tables[0].Rows[2][0].ToString())
                        {
                            case "Very Low":

                                third_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                third_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                third_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                third_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                third_row_cell2.Add(rveryhi);

                                break;
                        }



                        third_row_cell2.Width = 80;
                        third_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        third_row_cell2.BorderWidth = 2f;
                        third_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        third_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(third_row_cell2);


                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[2][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        Cell third_row_cell3 = new Cell();
                        third_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        third_row_cell3.Width = 80;
                        third_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        third_row_cell3.BorderWidth = 2f;
                        third_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(third_row_cell3);

                        Cell third_row_cell4 = new Cell();
                        third_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[2][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        third_row_cell4.Width = 80;
                        third_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        third_row_cell4.BorderWidth = 2f;
                        third_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        third_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(third_row_cell4);


                        /////////////// table fourth row 

                        Cell fourth_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[3][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }



                        fourth_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fourth_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        fourth_row_cell1.Width = 80;
                        fourth_row_cell1.BorderWidth = 2f;
                        fourth_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fourth_row_cell1);

                        Cell fourth_row_cell2 = new Cell();


                        //////////////  rectangle of line 4

                        # region rr4


                        iTextSharp.text.Table per_line4rect = new iTextSharp.text.Table(5);
                        Cell per_line4rect_cell1 = new Cell();
                        per_line4rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line4rect_cell1.Width = 2;
                        per_line4rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line4rect_cell1.BorderWidth = 4f;
                        per_line4rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line4rect_cell2 = new Cell();
                        per_line4rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line4rect_cell2.Width = 2;
                        per_line4rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line4rect_cell2.BorderWidth = 4f;
                        per_line4rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line4rect_cell3 = new Cell();
                        per_line4rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line4rect_cell3.Width = 2;
                        per_line4rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line4rect_cell3.BorderWidth = 4f;
                        per_line4rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line4rect_cell4 = new Cell();
                        per_line4rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line4rect_cell4.Width = 2;
                        per_line4rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line4rect_cell4.BorderWidth = 4f;
                        per_line4rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line4rect_cell5 = new Cell();
                        per_line4rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line4rect_cell5.Width = 2;
                        per_line4rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line4rect_cell5.BorderWidth = 4f;
                        per_line4rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[3][0].ToString())
                        {
                            case "Very Low":

                                per_line4rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line4rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line4rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line4rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line4rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line4rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line4rect_cell5.BackgroundColor = new Color(225, 225, 0); ;

                                break;
                        }

                        per_line4rect.AddCell(per_line4rect_cell1);
                        per_line4rect.AddCell(per_line4rect_cell2);
                        per_line4rect.AddCell(per_line4rect_cell3);
                        per_line4rect.AddCell(per_line4rect_cell4);
                        per_line4rect.AddCell(per_line4rect_cell5);
                        per_line4rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line4rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                        //fourth_row_cell2.Add(per_line4rect);

                        ////////////////////////////////////////////////////

                        #endregion


                        switch (ds1.Tables[0].Rows[3][0].ToString())
                        {
                            case "Very Low":

                                fourth_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                fourth_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                fourth_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                fourth_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                fourth_row_cell2.Add(rveryhi);

                                break;
                        }



                        fourth_row_cell2.Width = 80;
                        fourth_row_cell2.BorderWidth = 2f;
                        fourth_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        fourth_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fourth_row_cell2);

                        Cell fourth_row_cell3 = new Cell();


                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[3][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        fourth_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fourth_row_cell3.Width = 80;
                        fourth_row_cell3.BorderWidth = 2f;
                        fourth_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fourth_row_cell3);

                        Cell fourth_row_cell4 = new Cell();
                        fourth_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[3][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fourth_row_cell4.Width = 80;
                        fourth_row_cell4.BorderWidth = 2f;
                        fourth_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fourth_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fourth_row_cell4);




                        /////////////// table fifth row 

                        Cell fifth_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[4][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }



                        fifth_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fifth_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        fifth_row_cell1.Width = 80;
                        fifth_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        fifth_row_cell1.BorderWidth = 2f;
                        fifth_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fifth_row_cell1);

                        Cell fifth_row_cell2 = new Cell();


                        //////////////  rectangle of line 5

                        # region r55


                        iTextSharp.text.Table per_line5rect = new iTextSharp.text.Table(5);
                        Cell per_line5rect_cell1 = new Cell();
                        per_line5rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line5rect_cell1.Width = 2;
                        per_line5rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line5rect_cell1.BorderWidth = 4f;
                        per_line5rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line5rect_cell2 = new Cell();
                        per_line5rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line5rect_cell2.Width = 2;
                        per_line5rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line5rect_cell2.BorderWidth = 4f;
                        per_line5rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line5rect_cell3 = new Cell();
                        per_line5rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line5rect_cell3.Width = 2;
                        per_line5rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line5rect_cell3.BorderWidth = 4f;
                        per_line5rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line5rect_cell4 = new Cell();
                        per_line5rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line5rect_cell4.Width = 2;
                        per_line5rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line5rect_cell4.BorderWidth = 4f;
                        per_line5rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line5rect_cell5 = new Cell();
                        per_line5rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line5rect_cell5.Width = 2;
                        per_line5rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line5rect_cell5.BorderWidth = 4f;
                        per_line5rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[4][0].ToString())
                        {
                            case "Very Low":

                                per_line5rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line5rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line5rect_cell1.BackgroundColor = new Color(225, 225, 0); ;
                                per_line5rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line5rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line5rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line5rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line5rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line5rect.AddCell(per_line5rect_cell1);
                        per_line5rect.AddCell(per_line5rect_cell2);
                        per_line5rect.AddCell(per_line5rect_cell3);
                        per_line5rect.AddCell(per_line5rect_cell4);
                        per_line5rect.AddCell(per_line5rect_cell5);
                        per_line5rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line5rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;


                        //fifth_row_cell2.Add(per_line5rect);
                        ////////////////////////////////////////////////////


                        # endregion

                        switch (ds1.Tables[0].Rows[4][0].ToString())
                        {
                            case "Very Low":

                                fifth_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                fifth_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                fifth_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                fifth_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                fifth_row_cell2.Add(rveryhi);

                                break;
                        }



                        //fifth_row_cell2.Add(per_line5rect);
                        fifth_row_cell2.Width = 80;
                        fifth_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        fifth_row_cell2.BorderWidth = 2f;
                        fifth_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        fifth_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fifth_row_cell2);

                        Cell fifth_row_cell3 = new Cell();


                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[4][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        fifth_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fifth_row_cell3.Width = 80;
                        fifth_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        fifth_row_cell3.BorderWidth = 2f;
                        fifth_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fifth_row_cell3);

                        Cell fifth_row_cell4 = new Cell();
                        fifth_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[4][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        fifth_row_cell4.Width = 80;
                        fifth_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        fifth_row_cell4.BorderWidth = 2f;
                        fifth_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        fifth_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(fifth_row_cell4);


                        /////////////// table sixth row 

                        Cell sixth_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[5][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }


                        sixth_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        sixth_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        sixth_row_cell1.Width = 80;
                        sixth_row_cell1.BorderWidth = 2f;
                        sixth_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(sixth_row_cell1);

                        Cell sixth_row_cell2 = new Cell();




                        ////////////  rectangle of line 6


                        # region rr6

                        iTextSharp.text.Table per_line6rect = new iTextSharp.text.Table(5);
                        Cell per_line6rect_cell1 = new Cell();
                        per_line6rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line6rect_cell1.Width = 2;
                        per_line6rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line6rect_cell1.BorderWidth = 4f;
                        per_line6rect_cell1.BorderColor = new Color(255, 255, 255);
                        thirdTable1.AddCell(thirdcell1);

                        Cell per_line6rect_cell2 = new Cell();
                        per_line6rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line6rect_cell2.Width = 2;
                        per_line6rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line6rect_cell2.BorderWidth = 4f;
                        per_line6rect_cell2.BorderColor = new Color(255, 255, 255);
                        thirdTable1.AddCell(thirdcell2);

                        Cell per_line6rect_cell3 = new Cell();
                        per_line6rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line6rect_cell3.Width = 2;
                        per_line6rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line6rect_cell3.BorderWidth = 4f;
                        per_line6rect_cell3.BorderColor = new Color(255, 255, 255);
                        thirdTable1.AddCell(thirdcell3);

                        Cell per_line6rect_cell4 = new Cell();
                        per_line6rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line6rect_cell4.Width = 2;
                        per_line6rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line6rect_cell4.BorderWidth = 4f;
                        per_line6rect_cell4.BorderColor = new Color(255, 255, 255);
                        thirdTable1.AddCell(thirdcell4);

                        Cell per_line6rect_cell5 = new Cell();
                        per_line6rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line6rect_cell5.Width = 2;
                        per_line6rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line6rect_cell5.BorderWidth = 4f;
                        per_line6rect_cell5.BorderColor = new Color(255, 255, 255);
                        thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[5][0].ToString())
                        {
                            case "Very Low":

                                per_line6rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line6rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line6rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line6rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line6rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line6rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line6rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line6rect.AddCell(per_line6rect_cell1);
                        per_line6rect.AddCell(per_line6rect_cell2);
                        per_line6rect.AddCell(per_line6rect_cell3);
                        per_line6rect.AddCell(per_line6rect_cell4);
                        per_line6rect.AddCell(per_line6rect_cell5);
                        per_line6rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line6rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;


                        //sixth_row_cell2.Add(per_line6rect);
                        //////////////////////////////////////////////////

                        # endregion
                        switch (ds1.Tables[0].Rows[5][0].ToString())
                        {
                            case "Very Low":

                                sixth_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                sixth_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                sixth_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                sixth_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                sixth_row_cell2.Add(rveryhi);

                                break;
                        }

                        // sixth_row_cell2.Add(per_line6rect);
                        sixth_row_cell2.Width = 80;
                        sixth_row_cell2.BorderWidth = 2f;
                        sixth_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        sixth_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(sixth_row_cell2);

                        Cell sixth_row_cell3 = new Cell();


                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[5][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        sixth_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        sixth_row_cell3.Width = 80;
                        sixth_row_cell3.BorderWidth = 2f;
                        sixth_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(sixth_row_cell3);

                        Cell sixth_row_cell4 = new Cell();
                        sixth_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[5][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        sixth_row_cell4.Width = 80;
                        sixth_row_cell4.BorderWidth = 2f;
                        sixth_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        sixth_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(sixth_row_cell4);

                        /////////////// table seventh row 

                        Cell seven_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[6][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }

                        seven_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        seven_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER)));
                        seven_row_cell1.Width = 80;
                        seven_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        seven_row_cell1.BorderWidth = 2f;
                        seven_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(seven_row_cell1);

                        Cell seven_row_cell2 = new Cell();
                        //////////////  rectangle of line 7
                        # region rr7

                        iTextSharp.text.Table per_line7rect = new iTextSharp.text.Table(5);
                        Cell per_line7rect_cell1 = new Cell();
                        per_line7rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line7rect_cell1.Width = 2;
                        per_line7rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line7rect_cell1.BorderWidth = 4f;
                        per_line7rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line7rect_cell2 = new Cell();
                        per_line7rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line7rect_cell2.Width = 2;
                        per_line7rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line7rect_cell2.BorderWidth = 4f;
                        per_line7rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line7rect_cell3 = new Cell();
                        per_line7rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line7rect_cell3.Width = 2;
                        per_line7rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line7rect_cell3.BorderWidth = 4f;
                        per_line7rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line7rect_cell4 = new Cell();
                        per_line7rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line7rect_cell4.Width = 2;
                        per_line7rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line7rect_cell4.BorderWidth = 4f;
                        per_line7rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line7rect_cell5 = new Cell();
                        per_line7rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line7rect_cell5.Width = 2;
                        per_line7rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line7rect_cell5.BorderWidth = 4f;
                        per_line7rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[6][0].ToString())
                        {
                            case "Very Low":

                                per_line7rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line7rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line7rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line7rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line7rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line7rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line7rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line7rect.AddCell(per_line7rect_cell1);
                        per_line7rect.AddCell(per_line7rect_cell2);
                        per_line7rect.AddCell(per_line7rect_cell3);
                        per_line7rect.AddCell(per_line7rect_cell4);
                        per_line7rect.AddCell(per_line7rect_cell5);
                        per_line7rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line7rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;


                        //seven_row_cell2.Add(per_line7rect);
                        ////////////////////////////////////////////////////

                        # endregion

                        switch (ds1.Tables[0].Rows[6][0].ToString())
                        {
                            case "Very Low":

                                seven_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                seven_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                seven_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                seven_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                seven_row_cell2.Add(rveryhi);

                                break;
                        }


                        // seven_row_cell2.Add(per_line7rect);
                        seven_row_cell2.Width = 80;
                        seven_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        seven_row_cell2.BorderWidth = 2f;
                        seven_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        seven_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(seven_row_cell2);

                        Cell seven_row_cell3 = new Cell();

                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[6][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        seven_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        seven_row_cell3.Width = 80;
                        seven_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        seven_row_cell3.BorderWidth = 2f;
                        seven_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(seven_row_cell3);

                        Cell seven_row_cell4 = new Cell();
                        seven_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[6][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        seven_row_cell4.Width = 80;
                        seven_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        seven_row_cell4.BorderWidth = 2f;
                        seven_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        seven_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(seven_row_cell4);





                        ///////////// table eight row 

                        Cell eight_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[7][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }


                        eight_row_cell1.Add(new Paragraph(chart2lblname.ToString() + "\n ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        eight_row_cell1.Width = 80;
                        //eight_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        eight_row_cell1.BorderWidth = 2f;
                        eight_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(eight_row_cell1);

                        Cell eight_row_cell2 = new Cell();

                        //////////////  rectangle of line 8
                        # region rr8

                        iTextSharp.text.Table per_line8rect = new iTextSharp.text.Table(5);
                        Cell per_line8rect_cell1 = new Cell();
                        per_line8rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line8rect_cell1.Width = 2;
                        per_line8rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line8rect_cell1.BorderWidth = 4f;
                        per_line8rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line8rect_cell2 = new Cell();
                        per_line8rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line8rect_cell2.Width = 2;
                        per_line8rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line8rect_cell2.BorderWidth = 4f;
                        per_line8rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line8rect_cell3 = new Cell();
                        per_line8rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line8rect_cell3.Width = 2;
                        per_line8rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line8rect_cell3.BorderWidth = 4f;
                        per_line8rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line8rect_cell4 = new Cell();
                        per_line8rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line8rect_cell4.Width = 2;
                        per_line8rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line8rect_cell4.BorderWidth = 4f;
                        per_line8rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line8rect_cell5 = new Cell();
                        per_line8rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line8rect_cell5.Width = 2;
                        per_line8rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line8rect_cell5.BorderWidth = 4f;
                        per_line8rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[7][0].ToString())
                        {
                            case "Very Low":

                                per_line8rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line8rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line8rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line8rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line8rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line8rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line8rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line8rect.AddCell(per_line8rect_cell1);
                        per_line8rect.AddCell(per_line8rect_cell2);
                        per_line8rect.AddCell(per_line8rect_cell3);
                        per_line8rect.AddCell(per_line8rect_cell4);
                        per_line8rect.AddCell(per_line8rect_cell5);
                        per_line8rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line8rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;


                        // eight_row_cell2.Add(per_line8rect);
                        ////////////////////////////////////////////////////

                        #endregion


                        switch (ds1.Tables[0].Rows[7][0].ToString())
                        {
                            case "Very Low":

                                eight_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                eight_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                eight_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                eight_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                eight_row_cell2.Add(rveryhi);

                                break;
                        }


                        // eight_row_cell2.Add(per_line8rect);
                        eight_row_cell2.Width = 80;
                        //eight_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        eight_row_cell2.BorderWidth = 2f;
                        eight_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        eight_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(eight_row_cell2);

                        Cell eight_row_cell3 = new Cell();

                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[7][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        eight_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        eight_row_cell3.Width = 80;
                        // eight_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        eight_row_cell3.BorderWidth = 2f;
                        eight_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(eight_row_cell3);

                        Cell eight_row_cell4 = new Cell();
                        eight_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[7][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        eight_row_cell4.Width = 80;
                        //eight_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        eight_row_cell4.BorderWidth = 2f;
                        eight_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        eight_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(eight_row_cell4);



                        ///////////// table nineth row 

                        Cell ninth_row_cell1 = new Cell();

                        switch (ds1.Tables[0].Rows[8][2].ToString())
                        {
                            case "1":
                                chart2lblname = "Relationships";
                                break;
                            case "2":
                                chart2lblname = "Emotional Stability";
                                break;
                            case "3":
                                chart2lblname = "Assertiveness";
                                break;
                            case "4":
                                chart2lblname = "Enthusiasm";
                                break;
                            case "5":
                                chart2lblname = "Conscientious";
                                break;
                            case "6":
                                chart2lblname = "Responsiveness";
                                break;
                            case "7":
                                chart2lblname = "Tough Minded";
                                break;
                            case "8":
                                chart2lblname = "Self Assurance";
                                break;
                            case "9":
                                chart2lblname = "Relaxed";
                                break;
                        }

                        ninth_row_cell1.Add(new Paragraph(chart2lblname.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        ninth_row_cell1.Add(new Paragraph("\n", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        ninth_row_cell1.Width = 80;
                        ninth_row_cell1.BackgroundColor = new Color(219, 219, 219);
                        ninth_row_cell1.BorderWidth = 2f;
                        ninth_row_cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell1.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(ninth_row_cell1);

                        Cell ninth_row_cell2 = new Cell();
                        //////////////  rectangle of line 9

                        # region rr9
                        iTextSharp.text.Table per_line9rect = new iTextSharp.text.Table(5);
                        Cell per_line9rect_cell1 = new Cell();
                        per_line9rect_cell1.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line9rect_cell1.Width = 2;
                        per_line9rect_cell1.BackgroundColor = new Color(204, 186, 60);
                        per_line9rect_cell1.BorderWidth = 4f;
                        per_line9rect_cell1.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell1);

                        Cell per_line9rect_cell2 = new Cell();
                        per_line9rect_cell2.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line9rect_cell2.Width = 2;
                        per_line9rect_cell2.BackgroundColor = new Color(204, 186, 60);
                        per_line9rect_cell2.BorderWidth = 4f;
                        per_line9rect_cell2.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell2);

                        Cell per_line9rect_cell3 = new Cell();
                        per_line9rect_cell3.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line9rect_cell3.Width = 2;
                        per_line9rect_cell3.BackgroundColor = new Color(204, 186, 60);
                        per_line9rect_cell3.BorderWidth = 4f;
                        per_line9rect_cell3.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell3);

                        Cell per_line9rect_cell4 = new Cell();
                        per_line9rect_cell4.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line9rect_cell4.Width = 2;
                        per_line9rect_cell4.BackgroundColor = new Color(204, 186, 60);
                        per_line9rect_cell4.BorderWidth = 4f;
                        per_line9rect_cell4.BorderColor = new Color(255, 255, 255);
                        //thirdTable1.AddCell(thirdcell4);

                        Cell per_line9rect_cell5 = new Cell();
                        per_line9rect_cell5.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.COURIER, Color.WHITE)));
                        per_line9rect_cell5.Width = 2;
                        per_line9rect_cell5.BackgroundColor = new Color(204, 186, 60);
                        per_line9rect_cell5.BorderWidth = 4f;
                        per_line9rect_cell5.BorderColor = new Color(255, 255, 255);
                        // thirdTable1.AddCell(thirdcell5);



                        switch (ds1.Tables[0].Rows[5][1].ToString())
                        {
                            case "Very Low":

                                per_line9rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell2.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Low":

                                per_line9rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell3.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Moderate":

                                per_line9rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell4.BackgroundColor = new Color(71, 65, 18);
                                per_line9rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "High":

                                per_line9rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell5.BackgroundColor = new Color(71, 65, 18);

                                break;
                            case "Very High":

                                per_line9rect_cell1.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell2.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell3.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell4.BackgroundColor = new Color(225, 225, 0);
                                per_line9rect_cell5.BackgroundColor = new Color(225, 225, 0);

                                break;
                        }

                        per_line9rect.AddCell(per_line9rect_cell1);
                        per_line9rect.AddCell(per_line9rect_cell2);
                        per_line9rect.AddCell(per_line9rect_cell3);
                        per_line9rect.AddCell(per_line9rect_cell4);
                        per_line9rect.AddCell(per_line9rect_cell5);
                        per_line9rect.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        per_line9rect.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                        // ninth_row_cell2.Add(per_line9rect);

                        ////////////////////////////////////////////////////

                        #endregion


                        switch (ds1.Tables[0].Rows[8][0].ToString())
                        {
                            case "Very Low":

                                ninth_row_cell2.Add(rverylow);

                                break;
                            case "Low":

                                ninth_row_cell2.Add(rlow);

                                break;
                            case "Moderate":

                                ninth_row_cell2.Add(rmodrate);

                                break;
                            case "High":

                                ninth_row_cell2.Add(rhigh);

                                break;
                            case "Very High":

                                ninth_row_cell2.Add(rveryhi);

                                break;
                        }
                        // ninth_row_cell2.Add(per_line9rect);
                        ninth_row_cell2.Width = 80;
                        ninth_row_cell2.BackgroundColor = new Color(219, 219, 219);
                        ninth_row_cell2.BorderWidth = 2f;
                        ninth_row_cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        ninth_row_cell2.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(ninth_row_cell2);

                        Cell ninth_row_cell3 = new Cell();

                        fff = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[8][1].ToString());
                        perf = (int)Math.Ceiling(fff);

                        ninth_row_cell3.Add(new Paragraph(perf.ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        ninth_row_cell3.Width = 80;
                        ninth_row_cell3.BackgroundColor = new Color(219, 219, 219);
                        ninth_row_cell3.BorderWidth = 2f;
                        ninth_row_cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell3.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(ninth_row_cell3);

                        Cell ninth_row_cell4 = new Cell();
                        ninth_row_cell4.Add(new Paragraph(ds1.Tables[0].Rows[8][0].ToString(), FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER)));
                        ninth_row_cell4.Width = 80;
                        ninth_row_cell4.BackgroundColor = new Color(219, 219, 219);
                        ninth_row_cell4.BorderWidth = 2f;
                        ninth_row_cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        ninth_row_cell4.BorderColor = new Color(255, 255, 255);
                        PDTopTable11.AddCell(ninth_row_cell4);

                        Cell PDTopCell26 = new Cell();
                        PDTopCell26.Add(PDTopTable11);
                        PDTopCell26.Width = 80;
                        PDTopTable999.AddCell(PDTopCell26);


                        //////////////////////////////////////////////////////////////////// end PERSONALITY STRENGTHS table


                        //Cell sapce1 = new Cell();
                        //sapce1.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                        //PDTopTable999.AddCell(sapce1);
                        //PDTopTable999.AddCell(acell33);
                        //Cell sapce2 = new Cell();
                        //sapce2.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                        //PDTopTable999.AddCell(sapce2);
                        doc.Add(PDTopTable999);


                        // doc.Add(dheyalogo);

                        //////////////////////////////////////////////////////////////////
                        //doc.Add(dheyalogo);
                        doc.NewPage(); doc.Add(dheyalogo);
                        iTextSharp.text.Image sevenpagefirst1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/sevenfirst1.png"));
                        //jpeg.ScalePercent(35f);
                        sevenpagefirst1.ScaleToFit(600f, 550f);
                        sevenpagefirst1.SetAbsolutePosition(0, 600);
                        // jpeg.SpacingAfter = -50f;
                        doc.Add(sevenpagefirst1);

                        iTextSharp.text.Image textcrop = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/textcrop.png"));
                        //jpeg.ScalePercent(35f);
                        textcrop.ScaleToFit(283f, 232f);
                        textcrop.SetAbsolutePosition(38, 135);
                        // jpeg.SpacingAfter = -50f;
                        //  doc.Add(textcrop);


                        iTextSharp.text.Table maintableb1 = new iTextSharp.text.Table(1);
                        maintableb1.Width = 100;
                        maintableb1.DefaultCellBorder = 0;
                        maintableb1.Border = 0;


                        Cell maintabl0 = new Cell();
                        maintabl0.Add(new Paragraph("\n\n\n\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        Cell maintabl1 = new Cell();
                        Cell maintabl2 = new Cell();
                        Cell maintabl3 = new Cell();
                        Cell maintabl4 = new Cell();
                        Cell maintabl5 = new Cell();
                        Cell maintabl6 = new Cell();
                        Cell maintabl7 = new Cell();
                        Cell maintabl8 = new Cell();
                        Cell maintabl9 = new Cell();




                        iTextSharp.text.Table tb1 = new iTextSharp.text.Table(3);
                        tb1.Alignment = 0;
                        tb1.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb1.DefaultCellBorder = 0;
                        tb1.Border = 0;

                        //iTextSharp.text.Image endblock = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/endblock.png"));
                        //endblock.ScaleToFit(10f, 10f);
                        //Chunk imageChunk1 = new Chunk(endblock, 0, 0);
                        //Phrase limage = new Phrase();
                        //limage.Add(imageChunk1);


                        Cell tbCelll119 = new Cell();
                        tbCelll119.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;



                        tbCelll119.Add(new Paragraph("\n\n\n\nRelationship Orientation", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph pp7 = new Paragraph();
                        Phrase RelationshipOrientation = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase RelationshipOrientation1 = new Phrase(18, new Chunk(" High scores indicate that the person has a positive outlook towards people, the team and the society. Also this person is cooperative, participative and outgoing. Extreme high scores indicate an easy-going temperament.   ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp7.Add(RelationshipOrientation);
                        pp7.Add(RelationshipOrientation1);
                        pp7.Add(limage);
                        tbCelll119.Add(pp7);
                        Paragraph pp77 = new Paragraph();
                        Phrase RelationshipOrientation2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase RelationshipOrientation3 = new Phrase(18, new Chunk(" Low scores indicate that this person is likely to be impersonal, aloof and not too friendly. This person prefers to remain isolated from the society/group. Also this person is cool, distant and reserved. Also a hesitant and overly formal behaviour is expected.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp77.Add(RelationshipOrientation2);
                        pp77.Add(RelationshipOrientation3);
                        pp77.Add(limage);
                        tbCelll119.Add(pp77);


                        tb1.AddCell(tbCelll119);
                        tb1.AddCell(acell33);
                        tb1.SetWidths(width);


                        Cell tbcellbb = new Cell();
                        tbcellbb.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[0][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb.Add(middal);
                                break;
                            case "High":

                                tbcellbb.Add(high);
                                break;
                            case "Very High":

                                tbcellbb.Add(veryhigh);
                                break;
                        }
                        tbcellbb.Add(new Paragraph(dsss1.Tables[0].Rows[0][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb1.AddCell(tbcellbb);
                        // doc.Add(PDTopTableee7);
                        maintabl1.Add(tb1);

                        //////////////////////second row

                        iTextSharp.text.Table tb2 = new iTextSharp.text.Table(3);
                        tb2.Alignment = 0;
                        tb2.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb2.DefaultCellBorder = 0;
                        tb2.Border = 0;




                        Cell tbCelll110 = new Cell();
                        tbCelll110.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;
                        //Paragraph ppp16 = new Paragraph();

                        //Phrase nMt = new Phrase(18, new Chunk("Management ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        //Phrase nMt1 = new Phrase(18, new Chunk(" Management is the process of getting activities completed and effectively with and through other people. It is the managerial process of forming a strategic vision, setting objectives, crafting a strategy and then implementing and executing the plan. You have an interest in management field means you like the activities where some management is needed. Like-managing deals, organize such events or activities, make plans for the growth, manage people & their activities etc.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        //ppp16.Add(nMt);
                        //ppp16.Add(nMt1);
                        //ppp16.Add(lm);
                        //tbCelll119.Add(ppp16);


                        tbCelll110.Add(new Paragraph("\nEmotionally Stable", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph pp8 = new Paragraph();
                        Phrase EmotionallyStable = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase EmotionallyStable1 = new Phrase(18, new Chunk(" High scores indicate that this person is in good control of personal emotions. This person has a greater tolerance to handle frustration and face reality calmly.This person doesn’t get stressed or anxious under pressure. This person tries to maintain poise and maturity most of the times. This person doesn’t worry and get disturbed over petty inconvenience.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp8.Add(EmotionallyStable);
                        pp8.Add(EmotionallyStable1);
                        pp8.Add(limage);
                        tbCelll110.Add(pp8);
                        Paragraph pp88 = new Paragraph();
                        Phrase EmotionallyStable2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase EmotionallyStable3 = new Phrase(18, new Chunk(" Low scores indicate that this person is likely to have problems controlling emotions. This person also is likely to over-react to situations. He or she tends to become disorganized under pressure.There exists a tendency to get frustrated too fast. This person gets irritated, tensed, restless & impatient by his/her own feelings/ emotions too often. This person finds it difficult to cope effectively with stress.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp88.Add(EmotionallyStable2);
                        pp88.Add(EmotionallyStable3);
                        pp88.Add(limage);
                        tbCelll110.Add(pp88);


                        tb2.AddCell(tbCelll110);
                        tb2.AddCell(acell33);
                        tb2.SetWidths(width);


                        Cell tbcellbb1 = new Cell();
                        tbcellbb1.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[1][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb1.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb1.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb1.Add(middal);
                                break;
                            case "High":

                                tbcellbb1.Add(high);
                                break;
                            case "Very High":

                                tbcellbb1.Add(veryhigh);
                                break;
                        }
                        tbcellbb1.Add(new Paragraph(dsss1.Tables[0].Rows[1][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb2.AddCell(tbcellbb1);
                        // doc.Add(PDTopTableee7);
                        maintabl2.Add(tb2);


                        //////////////////////third row



                        iTextSharp.text.Table tb3 = new iTextSharp.text.Table(3);
                        tb3.Alignment = 0;
                        tb3.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb3.DefaultCellBorder = 0;
                        tb3.Border = 0;



                        Cell tbCelll111 = new Cell();
                        tbCelll111.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;
                        //Paragraph ppp16 = new Paragraph();

                        //Phrase nMt = new Phrase(18, new Chunk("Management ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        //Phrase nMt1 = new Phrase(18, new Chunk(" Management is the process of getting activities completed and effectively with and through other people. It is the managerial process of forming a strategic vision, setting objectives, crafting a strategy and then implementing and executing the plan. You have an interest in management field means you like the activities where some management is needed. Like-managing deals, organize such events or activities, make plans for the growth, manage people & their activities etc.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        //ppp16.Add(nMt);
                        //ppp16.Add(nMt1);
                        //ppp16.Add(lm);
                        //tbCelll119.Add(ppp16);

                        // doc.Add(dheyalogo);
                        //doc.Add(lastpatch);

                        tbCelll111.Add(new Paragraph("\nAssertive", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph prp9 = new Paragraph();
                        Phrase Arssertive = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase Arssertive1 = new Phrase(18, new Chunk(" High  scores  indicate that  this  person  loves  competition  is competitive  by nature. This  person  is  likely  to  be  freeminded  and  independent.This  person  can  express  feelings  and  thoughts  with  greater  ease.This  person  knows  his/her  rights  and  tends  to  control  others  and  helps  them  know  their  rights. This  person  has  good  control  over  his/her anger  and  manages  to express  his/her  displeasure  in  a  logical  way.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        //prp9.SetLeading(50.0f, 50.0f);
                        prp9.Add(Arssertive);
                        prp9.Add(Arssertive1);
                        prp9.Add(limage);
                        tbCelll111.Add(prp9);


                        Paragraph pp99 = new Paragraph();
                        //Phrase thissss = new Phrase(18, new Chunk("\nThis person knows his/her rights and tends to control others and helps them know their rights. This person has good control over his/her anger and manages to express his/her displeasure in a logical way.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        Phrase Assertive2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase Assertive3 = new Phrase(18, new Chunk(" Low scores indicate that this person dislikes competition. This person also prefers a non demanding environment. This person has a non demanding disposition and has low decision needs.This person mostly avoids taking an independent decision or stance on issues. This person depends on others for a decision. He/she also finds it difficult to express his /her feelings, thoughts and desires.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        //prp9.SetLeading(50.0f, 50.0f);
                        // pp99.Add(thissss);
                        //pp99.Add(limage);
                        pp99.Add(Assertive2);
                        pp99.Add(Assertive3);
                        pp99.Add(limage);
                        // pp99.SetLeading(25.0f, 25.0f);
                        tbCelll111.Add(pp99);

                        tb3.AddCell(tbCelll111);
                        tb3.AddCell(acell33);
                        tb3.SetWidths(width);


                        Cell tbcellbb2 = new Cell();
                        tbcellbb2.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[2][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb2.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb2.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb2.Add(middal);
                                break;
                            case "High":

                                tbcellbb2.Add(high);
                                break;
                            case "Very High":

                                tbcellbb2.Add(veryhigh);
                                break;
                        }

                        tbcellbb2.Add(new Paragraph(dsss1.Tables[0].Rows[2][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb3.AddCell(tbcellbb2);
                        // doc.Add(PDTopTableee7);
                        maintabl3.Add(tb3);

                        //////////////////////fourth row

                        iTextSharp.text.Table tb4 = new iTextSharp.text.Table(3);
                        tb4.Alignment = 0;
                        tb4.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb4.DefaultCellBorder = 0;
                        tb4.Border = 0;




                        Cell tbCelll112 = new Cell();


                        tbCelll112.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;


                        tbCelll112.Add(new Paragraph("\n\n\nEnthusiasm and Energy ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph pp10 = new Paragraph();
                        Phrase EnthusiasmandEnergy = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase EnthusiasmandEnergy1 = new Phrase(18, new Chunk(" High scores indicate that this person would always be ready to take up and accept new and greater responsibilities. This person would like to take new challenges & handle critical situations with great alacrity. This person also displays high energy and willingness to experience new and unknown experience or situations. Also this person would willingly deal adequately with difficult conditions. This person displays a tendency to take lead in new varied activities and love adventures (exploring unchartered territories). Also this person is known as insightful and imaginative. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp10.Add(EnthusiasmandEnergy);
                        pp10.Add(EnthusiasmandEnergy1);
                        pp10.Add(limage);
                        tbCelll112.Add(pp10);
                        Paragraph pp100 = new Paragraph();

                        Phrase EnthusiasmandEnergy2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase EnthusiasmandEnergy3 = new Phrase(18, new Chunk(" Low scores indicate avoidance of new or increased responsibilities, activities and challenges. This person also prefers to stay away from critical situations. This person also doesn’t get involved in group event or team oriented activities. This person has a tendency to ignore dealing adequately with difficult conditions. This person is risk and change averse. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp100.Add(EnthusiasmandEnergy2);
                        pp100.Add(EnthusiasmandEnergy3);
                        pp100.Add(limage);
                        tbCelll112.Add(pp100);


                        tb4.AddCell(tbCelll112);
                        tb4.AddCell(acell33);
                        tb4.SetWidths(width);


                        Cell tbcellbb3 = new Cell();
                        tbcellbb3.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb3.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[3][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb3.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb3.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb3.Add(middal);
                                break;
                            case "High":

                                tbcellbb3.Add(high);
                                break;
                            case "Very High":

                                tbcellbb3.Add(veryhigh);
                                break;
                        }
                        tbcellbb3.Add(new Paragraph(dsss1.Tables[0].Rows[3][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb4.AddCell(tbcellbb3);
                        // doc.Add(PDTopTableee7);
                        maintabl4.Add(tb4);


                        //////////////////////fifth row

                        iTextSharp.text.Table tb5 = new iTextSharp.text.Table(3);
                        tb5.Alignment = 0;
                        tb5.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb5.DefaultCellBorder = 0;
                        tb5.Border = 0;




                        Cell tbCelll113 = new Cell();
                        tbCelll113.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;

                        tbCelll113.Add(new Paragraph("\nResponsibility and Conscientiousness ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));

                        Paragraph pp11 = new Paragraph();
                        Phrase ResponsibilityandConscientiousness = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase ResponsibilityandConscientiousness1 = new Phrase(18, new Chunk(" Person with high scores shows diligence, drive and domination by a sense of his/her duty. The person has a great regard for moral standards and ethics. You can expect honesty and sincerity towards his/her work. This person also displays strong sense of responsibility and orderliness. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp11.Add(ResponsibilityandConscientiousness);
                        pp11.Add(ResponsibilityandConscientiousness1);
                        pp11.Add(limage);
                        tbCelll113.Add(pp11);
                        Paragraph pp101 = new Paragraph();
                        Phrase ResponsibilityandConscientiousness2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase ResponsibilityandConscientiousness3 = new Phrase(18, new Chunk(" Person with low scores show low levels of persistence, drive & domination by a sense of his/her duty. Also this person may lack complete commitment towards the given task/work.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp101.Add(ResponsibilityandConscientiousness2);
                        pp101.Add(ResponsibilityandConscientiousness3);
                        pp101.Add(limage);
                        tbCelll113.Add(pp101);


                        tb5.AddCell(tbCelll113);
                        tb5.AddCell(acell33);
                        tb5.SetWidths(width);


                        Cell tbcellbb4 = new Cell();
                        tbcellbb4.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb4.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[4][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb4.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb4.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb4.Add(middal);
                                break;
                            case "High":

                                tbcellbb4.Add(high);
                                break;
                            case "Very High":

                                tbcellbb4.Add(veryhigh);
                                break;
                        }
                        tbcellbb4.Add(new Paragraph(dsss1.Tables[0].Rows[4][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb5.AddCell(tbcellbb4);
                        // doc.Add(PDTopTableee7);
                        maintabl5.Add(tb5);


                        //////////////////////sixth row

                        iTextSharp.text.Table tb6 = new iTextSharp.text.Table(3);
                        tb6.Alignment = 0;
                        tb6.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb6.DefaultCellBorder = 0;
                        tb6.Border = 0;




                        Cell tbCelll114 = new Cell();
                        tbCelll114.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;


                        tbCelll114.Add(new Paragraph("\nSocial Responsiveness ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));

                        Paragraph pp12 = new Paragraph();
                        Phrase SocialResponsiveness = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase SocialResponsiveness1 = new Phrase(18, new Chunk(" The person with high scores is socially precise. This person displays greater self-discipline and respects customs, social protocol, and also would have good manners. This person has a firm code of right and wrong, good and bad. Also, the person shows a strong sense of identity, awareness of “self”. This person also maintains a positive social reputation in the society.", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp12.Add(SocialResponsiveness);
                        pp12.Add(SocialResponsiveness1);
                        pp12.Add(limage);
                        tbCelll114.Add(pp12);
                        Paragraph pp102 = new Paragraph();
                        Phrase SocialResponsiveness2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase SocialResponsiveness3 = new Phrase(18, new Chunk(" Person with low scores considers himself/herself free from social constraints, norms and conventions. This person may not respect customs or even question them. By nature, this person is open minded and open by attitude and follows his/her own urges. He/she doesn’t persist with boring tasks. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp102.Add(SocialResponsiveness2);
                        pp102.Add(SocialResponsiveness3);
                        pp102.Add(limage);
                        tbCelll114.Add(pp102);


                        tb6.AddCell(tbCelll114);
                        tb6.AddCell(acell33);
                        tb6.SetWidths(width);


                        Cell tbcellbb5 = new Cell();
                        tbcellbb5.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb5.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[5][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb5.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb5.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb5.Add(middal);
                                break;
                            case "High":

                                tbcellbb5.Add(high);
                                break;
                            case "Very High":

                                tbcellbb5.Add(veryhigh);
                                break;
                        }
                        tbcellbb5.Add(new Paragraph(dsss1.Tables[0].Rows[5][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb6.AddCell(tbcellbb5);
                        // doc.Add(PDTopTableee7);
                        maintabl6.Add(tb6);


                        //////////////////////seventh row

                        iTextSharp.text.Table tb7 = new iTextSharp.text.Table(3);
                        tb7.Alignment = 0;
                        tb7.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb7.DefaultCellBorder = 0;
                        tb7.Border = 0;


                        // doc.Add(lastpatch);

                        Cell tbCelll115 = new Cell();
                        tbCelll115.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;


                        tbCelll115.Add(new Paragraph("\nTough Minded ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));

                        Paragraph pp13 = new Paragraph();
                        Phrase ToughMinded = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase ToughMinded1 = new Phrase(18, new Chunk(" The person with high scores is cool, emotionally detached and controlled. This person is capable of making rapid decisions when necessary. This person would be able to adjust quickly under pressure of time. Also this person by nature is alert and analytical with new information. This person is disciplined and intellectual in nature and rejects illusions. This person has a greater resilience and can take rejection or disapproval in a positive manner. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp13.Add(ToughMinded);
                        pp13.Add(ToughMinded1);
                        pp13.Add(limage);
                        tbCelll115.Add(pp13);
                        Paragraph pp103 = new Paragraph();
                        Phrase ToughMinded2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase ToughMinded3 = new Phrase(18, new Chunk(" Person with low scores is emotionally connected, involved and open. This person finds it difficult to take firm and independent decisions. This person displays an emotionally connected thinking and is driven more by emotions than practicality. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp103.Add(ToughMinded2);
                        pp103.Add(ToughMinded3);
                        pp103.Add(limage);
                        tbCelll115.Add(pp103);
                        // doc.Add(dheyalogo);

                        tb7.AddCell(tbCelll115);
                        tb7.AddCell(acell33);
                        tb7.SetWidths(width);


                        Cell tbcellbb6 = new Cell();
                        tbcellbb6.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb6.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[6][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb6.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb6.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb6.Add(middal);
                                break;
                            case "High":

                                tbcellbb6.Add(high);
                                break;
                            case "Very High":

                                tbcellbb6.Add(veryhigh);
                                break;
                        }
                        tbcellbb6.Add(new Paragraph(dsss1.Tables[0].Rows[6][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb7.AddCell(tbcellbb6);
                        // doc.Add(PDTopTableee7);
                        maintabl7.Add(tb7);


                        //////////////////////eight row

                        iTextSharp.text.Table tb8 = new iTextSharp.text.Table(3);
                        tb8.Alignment = 0;
                        tb8.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb8.DefaultCellBorder = 0;
                        tb8.Border = 0;




                        Cell tbCelll116 = new Cell();
                        tbCelll116.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;


                        tbCelll116.Add(new Paragraph("\n\n\n\n\n\n\n\n\nSelf-Assured ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph pp14 = new Paragraph();
                        Phrase SelfAssured = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase SelfAssured1 = new Phrase(18, new Chunk(" By nature the person with high scores in self assured behaviour is placid, secured, satisfied,confident and untroubled. This person is independent and can operate autonomously. This person prefers to take his/her own decisions and is capable and confident of facing any particular situation. This person has greater levels of self belief and higher confidence in his/her capabilities.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp14.Add(SelfAssured);
                        pp14.Add(SelfAssured1);
                        pp14.Add(limage);
                        tbCelll116.Add(pp14);
                        Paragraph pp104 = new Paragraph();
                        Phrase SelfAssured2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase SelfAssured3 = new Phrase(18, new Chunk(" By nature this person is dependent on others. This person is more group oriented and always prefers to work with a group. This person is more of a follower than a leader. This person is likely to display low levels of self-belief and confidence in matters of personal capability. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp104.Add(SelfAssured2);
                        pp104.Add(SelfAssured3);
                        pp104.Add(limage);
                        tbCelll116.Add(pp104);


                        tb8.AddCell(tbCelll116);
                        tb8.AddCell(acell33);
                        tb8.SetWidths(width);


                        Cell tbcellbb7 = new Cell();
                        tbcellbb7.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb7.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[7][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb7.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb7.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb7.Add(middal);
                                break;
                            case "High":

                                tbcellbb7.Add(high);
                                break;
                            case "Very High":

                                tbcellbb7.Add(veryhigh);
                                break;
                        }
                        tbcellbb7.Add(new Paragraph(dsss1.Tables[0].Rows[7][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb8.AddCell(tbcellbb7);
                        // doc.Add(PDTopTableee7);
                        maintabl8.Add(tb8);


                        //////////////////////ninth row

                        iTextSharp.text.Table tb9 = new iTextSharp.text.Table(3);
                        tb9.Alignment = 0;
                        tb9.Width = 100;

                        // PDTopTable3.BackgroundColor = new iTextSharp.text.Color(226, 226, 226);
                        tb9.DefaultCellBorder = 0;
                        tb9.Border = 0;




                        Cell tbCelll117 = new Cell();
                        tbCelll117.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED;




                        tbCelll117.Add(new Paragraph("\nRelaxed ", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Paragraph pp15 = new Paragraph();
                        Phrase Relaxed = new Phrase(18, new Chunk("HIGH", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase Relaxed1 = new Phrase(18, new Chunk(" The person with high scores in relaxed behaviour is cool, calm & relaxed by nature. This person doesn’t get frustrated or tensed even in the critical situations. This person is calm and displays thoughtfulness. This person on extreme levels tends to resist actions and changes. This person prefers a non-changing and constant/routine environment.  ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp15.Add(Relaxed);
                        pp15.Add(Relaxed1);
                        pp15.Add(limage);
                        tbCelll117.Add(pp15);
                        Paragraph pp105 = new Paragraph();
                        Phrase Relaxed2 = new Phrase(18, new Chunk("LOW", FontFactory.GetFont("Open Sans", 8, Font.BOLD, new Color(0, 0, 0))));
                        Phrase Relaxed3 = new Phrase(18, new Chunk(" This person gets anxious very soon.This person is restless, mobile and alert.This person displays impatience to complex issues or problems. This person is high paced and a sharp thinker. This person cannot work calmly and needs action and changing environment/work as a stimulant. ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.COURIER, new Color(104, 108, 114))));
                        pp105.Add(Relaxed2);
                        pp105.Add(Relaxed3);
                        pp105.Add(limage);
                        tbCelll117.Add(pp105);


                        tb9.AddCell(tbCelll117);
                        tb9.AddCell(acell33);
                        tb9.SetWidths(width);


                        Cell tbcellbb8 = new Cell();
                        tbcellbb8.BackgroundColor = new Color(45, 136, 204);
                        tbcellbb8.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;



                        switch (dsss1.Tables[0].Rows[8][0].ToString())
                        {
                            case "Very Low":

                                tbcellbb8.Add(verylow);

                                break;
                            case "Low":

                                tbcellbb8.Add(low);
                                break;
                            case "Moderate":

                                tbcellbb8.Add(middal);
                                break;
                            case "High":

                                tbcellbb8.Add(high);
                                break;
                            case "Very High":

                                tbcellbb8.Add(veryhigh);
                                break;
                        }
                        tbcellbb8.Add(new Paragraph(dsss1.Tables[0].Rows[8][0].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD, Color.WHITE)));
                        tbcellbb8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tbcellbb8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tb9.AddCell(tbcellbb8);
                        // doc.Add(PDTopTableee7);
                        maintabl9.Add(tb9);

                        maintableb1.AddCell(maintabl0);
                        maintableb1.AddCell(maintabl1);
                        maintableb1.AddCell(maintabl2);
                        maintableb1.AddCell(maintabl3);
                        maintableb1.AddCell(maintabl4);
                        maintableb1.AddCell(maintabl5);
                        maintableb1.AddCell(maintabl6);
                        maintableb1.AddCell(maintabl7);
                        maintableb1.AddCell(maintabl8);
                        maintableb1.AddCell(maintabl9);
                        doc.Add(maintableb1);


                        //last Page
                        doc.NewPage();

                        iTextSharp.text.Image DCLflow = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/ESATBack.jpg"));
                        DCLflow.ScaleToFit(600f, 900f);
                        DCLflow.SetAbsolutePosition(0, 0);
                        doc.Add(DCLflow);
                        doc.Close();
                        DownloadFile(c_name.ToString().Trim() + "_Report.pdf", true);
                    }
                    else
                    {
                        Log.Warn("User Not Registered");
                        divchart.Visible = false;
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "You are not registered User. Please register and take a test.";
                    }
                }
                }           
        }
        catch (System.Threading.ThreadAbortException )
        {
        }
        catch (Exception ex)
        {
            divchart.Visible = false;
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong. Please try again.";
            Log.Error("" + ex);
            doc.Close();
        }
    }
    private void DownloadFile(string fname, bool forceDownload)
    {
        string path = Server.MapPath("./Reports_pdf/" + fname);
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
            //int index = name.IndexOf(".");
            //string res = name.Substring(0, index);

            Response.AppendHeader("content-disposition",
                  "attachment; filename=" + name + ".pdf");

        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
    }
    private void profile_conditions_pdf()
    {
        // Different Patterns for Graph 3 
        doc.Add(new Paragraph("\n Profile Analysis :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));

        ////get image for end paragraph 

        iTextSharp.text.Image endblock = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ReportImages/endblockh.png"));
        endblock.ScaleToFit(2f, 2f);
        Chunk imageChunk1 = new Chunk(endblock, 0, 0);
        Phrase limage = new Phrase();
        limage.Add(imageChunk1);

        // 1. D is greater than all and in positive region
        //if ((DiffB < 10 && DiffB > -1) && (DiffR < -1 && DiffBl < -1 && DiffG < -1))
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1 && DiffBl <= -1 && DiffG <= -1))
        {

            Paragraph p = new Paragraph("You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p.Add(limage);
            p.SetAlignment("justify");
            doc.Add(p);

            doc.Add(new Paragraph("\nRelating to Others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p1 = new Paragraph("You tend to stress on achievement and success significantly, which affects your relations with other people. In extreme cases, you seem to treat other people simply as a means to an end, or a way of achieving your personal goals. Since you are not emotional, you tend not to place great importance on feelings, either your own or others'. The competitive nature in you leads to see challenges and opposition everywhere, and others sometimes find it difficult to break through this naturally suspicious, skeptical shell of yours.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);

            doc.Add(new Paragraph("\nCommon Abilities :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nMotivating Factors :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nDescriptive Traits :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation and Independence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            //string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'ambitious/ aggressive' , c_profile_name = 'Result Oriented Autocrat',c_suitability='Generalist',c_specialisation_I='Marketing',c_specialisation_II='Operations',c_specialisation_III='-' where c_id='" + c_id + "'";
            //int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 2. I is Greater than all and in positive region
        if ((DiffB <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1) && (DiffG <= -1))
        {

            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'High-I'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            // SqlDataReader dr = clsdb_Xaction.ExecDataReader(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);

                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;

            }

            if (SConditions == true)
            {

                Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You are best in relating to others. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);
            }

            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'optimistic/ charming' , c_profile_name = 'People Oriented Communicator',c_suitability='Generalist',c_specialisation_I='Human Resources',c_specialisation_II='Sales',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        // 3. S is Greater than all and in positive region
        if ((DiffB <= -1) && (DiffR <= -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= -1))
        {
            Paragraph p1 = new Paragraph("\t You show high degree of patience, calmness and gentle openness. You are generally amiable and warm-hearted, being sympathetic to others' points of view, and valuing positive interaction with others. You are not outgoing by nature; however, rely on other, more assertive, people to take the lead.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t As in your general lifestyle, you initiate relationships of any kind – yours solid, dependable outlook makes you far more suited to the maintenance of interpersonal relations than making initial contact. For this reason, your circle of friends and close acquaintances is often small but tightly-knit.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);


            doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You are simply 'supportive'. You are dependable and loyal, this combines with an emotional literacy to make you particularly effective listeners and counselors. You are also unusually persistent in approach, having the patience and restraint to work steadily at a task until it is achieved. This makes you unusually capable of dealing with laborious tasks that many other styles would simply not have the patience to complete.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);


            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t Patience is the root source of motivation in you. You need to feel that you have the support of those around you and, more importantly, time to adapt to new situations. You have an inherent dislike of change, and will prefer to maintain the status quo whenever possible; sudden alterations in your circumstances can be very difficult for you to deal with. Once embarked on a task, you will wish to concentrate closely on it and see it through. Interruptions and distractions of any kind can be particularly demotivating in these situations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);


            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Patience , Thoughtfulness and Persistence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'dependable/ empathetic listener' , c_profile_name = 'Routine Oriented Dependable Worker',c_suitability='Specialist',c_specialisation_I='Finance',c_specialisation_II='HR Backend',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 4. C is Greater than all and in positive region
        if ((DiffB <= -1) && (DiffR <= -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'High-C'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);
            }
            else
            {
                SConditions = true;

            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t Passive by nature, often reticent and aloof, you often tend to give an impression of coldness or disinterest. Much of this impassive style stems from your controlled nature, however, which makes you reluctant to reveal information about yourselves or your ideas unless absolutely necessary. In fact, you are often ambitious and have lofty goals, but your innate lack of assertiveness and unwillingness to become involved in confrontational situations makes it difficult for you to achieve these goals directly. Instead, you will tend to use existing structures and rules to accomplish your aims. You tend to follow rules, authority and logical argument to influence the actions of others.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You have much strength, but the ability to relate easily to other people is rarely in you. The combination of a passive social style with a certain innate suspiciousness makes it difficult for you to form or maintain close relationships, and this is especially true in a business sense. Your friendship or close acquaintances will normally be based on mutual interests or common aims, rather than emotional considerations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You are generally very self-reliant although this fact is often difficult to perceive. You have structured ways of thinking, and often show particular strengths when it comes to organizing facts or working with precise detail or sophisticated systems. You have a quick-thinking individual who will often have useful input, but your natural reticence means that you will rarely offer an opinion unless asked directly for your thoughts. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You need to feel completely sure of your position, and of others' expectations of you, before you are able to proceed. Because of this, you have a very strong aversion to risk, and will rarely take any action unless you can feel absolutely sure about its consequences. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are Cooperativeness, Accuracy and Sensitivity. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);
            }
            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'analytical/meticulous' , c_profile_name = 'Process Oriented Critic',c_suitability='Specialist',c_specialisation_I='Operations',c_specialisation_II='Finance',c_specialisation_III='-' where c_id='" + c_id + "'";
            //int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 5. D is Lower than all and in negative region
        if ((DiffB <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1))
        {
            Paragraph p1 = new Paragraph("\t You generally will try to achieve your ends through communication, using your persuasive abilities or your powers of rational discussion.You are not an ambitious type of person. You rarely set distinct goals for yourselves in life, but prefer instead simply to build strong relationships with others and pursue your personal interests or pastimes. You work particularly well as part of a team or group, being both friendly and co-operative in style, and ready to accept others' ideas.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You confer certain communicative strengths on this type of person. In combination, these three elements give styles of this kind a number of strengths in the field of relations with other people. You are an outgoing person with friendly personality style. You are patient with others and have good listening skills. Further, you are rational with regards to your personal approach, when necessary you can make cogent and coherent arguments. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t Your strong abilities lie in the field of personal communication and relationship management. You are a good team player and work well with other people, and appreciate their input into discussions. While you have the confidence to maintain a pro-active role, this does not equate to the direct assertiveness one may possess. You are capable of being outgoing and extrovert, and are also receptive to other people and sympathetic to other points of view. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You are not ambitious by nature, and rarely have a specific set of goals or aims in life. Motivation for you is more a matter of a general sense of happiness or contentment, and specifically this means the development of positive, warm relations with other people, time to adapt to changes in circumstance, and a sense of sureness about your position, especially (but not exclusively) in social terms.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Friendliness,Patience and Cooperativeness. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            // string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'conservative/ agreeable' , c_profile_name = 'Service Oriented Friendly',c_suitability='Specialist',c_specialisation_I='Operations',c_specialisation_II='Human Resources',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 6. I is lower than all and in negative region
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'Low-I'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {
                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You have a unique and not so common personality profile. The main distinguishing feature of your personality style is that you are a person who is shy and introverted  and work more around practicality and rational thought than emotional considerations, and you are generally reluctant to reveal information about themselves, their ideas or their feelings. In your case, more assertive and dominant behavior can be expected in antagonistic or difficult situations; while a more relaxed (but far less assertive) style can be anticipated in less pressurized circumstances.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t Relating to other people is not an area of particular emphasis for you. Where you do respond to others on more than a purely practical basis, you will tend to be rather passive in approach, reacting to comments or suggestions from other parties rather than offering direct input yourselves. As a situation becomes more difficult, your willingness to make direct input will increase dramatically, but your readiness to communicate on a personal level will reduce proportionately.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t The emphasis of this personality is on results and productivity. You work well with facts, and are at home with complex systems. You value effectiveness and efficiency, and will tend to embody these qualities in your approach to both your work and home lives. Although you have a clear view of your own personal aims in life, you are prepared to bide your time when necessary, and this thoughtful, patient approach helps you to avoid unnecessary risks or impulsive actions.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t In your case, motivating factors include the achievement of results, time to adapt to changing situations , a full understanding of fact and detail and an avoidance of risk. There will clearly be times when elements of this complex group of motivations conflict with one another.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Thoughtfulness and Accuracy. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }
            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'introvert/ critical' , c_profile_name = 'Introverted & Critical',c_suitability='Specialist',c_specialisation_I='Finance',c_specialisation_II='Operations',c_specialisation_III='-' where c_id='" + c_id + "'";
            //  int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 7. S is lower than all and in negative region
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
        {

            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'Low-S'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {
                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);
            }
            else
            {
                SConditions = true;

            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t Speed of response and a sense of urgency are the defining characteristics of your behavior. Your approach will be rooted in a dynamic, impatient style. This is a relatively self-controlled and ambitious style, but you also possess effective social abilities that can be expected to come to the fore in informal, open situations. While ambition and assertiveness are important elements of your style, you have an awareness of the needs of others and a sense of order that make you far less impulsive and unpredictable than many similarly extrovert types. While you will wish to achieve success in your own right, you also understand that the needs of an organization will from time to time require that you suppress your own ambitions for the good of the team.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t The ways in which you relate to other people will tend to vary according to your social situation, depending on the relative formality of your surroundings. In more social, casual circumstances, you will project a friendly and animated style, being open and enthusiastic in general approach.If your situation is more formal or closely regulated, however, a more direct and determined side to the personality will develop, being both assertive and self-controlled, and showing far less of the sociable, gregarious side associated with favourable situations. It is in formal situations of this kind that the more ambitious and driving elements of the personality will come to the fore, and it is likely that you will also adopt a somewhat plain-speaking, blunt aspect.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t From the above, it will be clear that you will display different abilities in different situations; you can be charming and enthusiastic, or direct and forthright, depending on your particular circumstances. From a manager's perspective, it will clearly be productive to adapt such a person's working environment, so far as is possible, to bring out the particular style that is most appropriate. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You have complex sets of motivating factors that may sometimes conflict with one another. In your case, motivation stems from the achievement of personal ambition, the acceptance and approval of other people, and certainty of your position.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are Self-motivation, , Enthusiasm and and Sensitivity ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }

            //string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'impatient/ restless' , c_profile_name = 'Speed & Sense of Urgency',c_suitability='Generalist',c_specialisation_I='Marketing',c_specialisation_II='Operations',c_specialisation_III='-' where c_id='" + c_id + "'";
            //int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        // 8. C is lower than all and in negative region
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= -1))
        {

            Paragraph p1 = new Paragraph("\t As you display independent behavioral style, you are dynamic and direct. You have clear idea about your goals in life and you equally have the strength to achieve it. You have a more single-minded and stubborn approach. You display remarkable tenacity and determination that help you to reach your goal in life. Though you mix easily with strangers, and are unafraid to initiate social contact, you have both an assertive and a confident behavioral style. This helps you to deal directly fearlessly with most situations. While you will typically prefer to keep matters on an open and friendly level, you are quite capable of adopting a more determined or confrontational stance where a situation calls for it.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You interact easily and skillfully with people around you, and rarely experience self-doubt, and you feel at ease in almost any social situation. You are unafraid to initiate social contact however you have a strong sense of independence and tend to maintain your own sense of identity, and protect and defend your own viewpoint.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You exhibit dependency and at the same time have strong social abilities. This makes you to take initiative in every activity with great confidence and assertiveness. You self- reliant style with a strong sense of personal responsibility makes you an effective facilitator. Being patient and persistent along with assertiveness helps you to achieve the results and also consider and weigh the options before arriving at a definite conclusion.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You tend to have control of own activities, and being persistent and assertive, like to do things you own way. While success and achievements are important to you, you also value positive relationships with other people. Under certain situation you might even delay towards achieving your goals if this conflicts with others' needs.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Independence, Self-confidence and Persistence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            // string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'independent/ self-willed' , c_profile_name = 'Determined and Assertive',c_suitability='Generalist',c_specialisation_I='Human Resources',c_specialisation_II='Marketing',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        #region Positive-DI_I>D
        // 9. D and I in positive region and I > D
        //if ((DiffB > -1 && DiffR > -1) && (DiffBl < -1 && DiffG < -1) && (DiffB < DiffR))        
        if ((DiffB > -1 && DiffR > -1) && (DiffBl <= -1 && DiffG <= -1))
        {

            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'D and I in positive region and I > D'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                if (ds4.Tables[0].Rows[0][8].ToString() != "")
                {
                    doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                    p8.Add(limage);
                    p8.SetAlignment("justify");
                    doc.Add(p8);
                }

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }


            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You are a highly assertive person, capable of both direct, dynamic action and charming sociability as a situation demands. In combination, these factors describe you as a person with clear goals in life with the determination and commitment to achieve them. You seek to maintain a position of dominance, not only in terms of personal authority and control, but also in a social sense –where you like to feel that you are not only respected by those working with you, but also genuinely liked.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You have strong social skills and a persuasive communication style. You are capable of great charm, but you will sometimes adopt a more demanding, overbearing style of behavior, especially if , you feel yourselves to be under pressure. The outgoing and quickly-paced approach of yours can be difficult to deal with, especially as you have no fear of confrontation and will address issues directly rather than prevaricate or evade.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t Challenge is a keyword for you where you thrive in situations that others would find impossibly stressful and difficult to deal with. Your need for achievement means that you are willing to undertake almost any task to achieve success or recognition, and this driving, motivated approach lends you an urgency and energy rarely seen in others.You are a classic ideal for direct sales work, where you have the ability to think and react quickly, adapt to challenging situations and use powers of both assertiveness and persuasion to motivate others to accept your own proposals.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t Challenge is a keyword for you where you thrive in situations that others would find impossibly stressful and difficult to deal with. Your need for achievement means that you are willing to undertake almost any task to achieve success or recognition, and this driving, motivated approach lends them an urgency and energy rarely seen in others.You are the classic ideal for direct sales work. This type of occupation characterizes you for having the ability to think and react quickly, adapt to challenging situations and use powers of both assertiveness and persuasion to motivate others to accept your own proposals.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub--traits of this type are Self-motivation, Independence, Enthusiasm and Self-confidence ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }


            // StrQuery = "UPDATE tbl_candidate_master set c_short_char = 'persuasive/ enthusiatic' , c_profile_name = 'Persuasive Leader',c_suitability='Generalist',c_specialisation_I='Marketing',c_specialisation_II='Human Resources',c_specialisation_III='Operations' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(StrQuery);
            return;

        }
        # endregion

        // 10. D and S in positive region and D > S
        //if ((DiffB < 10 && DiffB > -1) && (DiffR < -1 && DiffG < -1) && (DiffBl < 10 && DiffBl > -1) && (DiffB > DiffBl))
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1 && DiffG <= -1) && (DiffBl <= 10 && DiffBl > -1))
        {
            Paragraph p1 = new Paragraph("\t Your kind of profile is rather  rare. You have personality sets that are radically differing in values and motivations.  On one side you are forceful and assertive and on the other you are relaxed and calm. These  two factors are so opposing that together its  hard for them to effectively coexist in a single style.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t It is difficult to predict your likely approach to other people. On one hand you prefer to avoid revealing information to others, but on the other you like to maintain amiable and trusting relations with person with those around you. You adapt your social style to a particular situation, showing a friendlier side to your character if you feel that you can trust the people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You possess a single-minded and practical style, representing an individual who will follow a line of action through to the end, using concentration and determination to achieve your aims. You will try to complete tasks within realistic timescales, but you also value careful planning. The profile suggests that the more cautious, thoughtful side of the personality will appear under favorable conditions, while the more urgent, demanding aspect will be seen at times of pressure.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t The motivating factors associated with you are control and power, while the need for time and the avoidance of change also persists. You tend to exercise whatever authority you may have to preserve the status quo and avoid sudden change.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Independence , Thoughtfulness and Persistence. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);



            //string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'self-motivated/ focussed' , c_profile_name = 'Individualistic Motivated',c_suitability='Specialist',c_specialisation_I='Operations',c_specialisation_II='Finance',c_specialisation_III='HR Backend' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 11. D and C in positive region and D < C
        //if ((DiffB < 10 && DiffB > -1) && (DiffR < -1) && (DiffBl < -1) && (DiffG < 10 && DiffG > -1) && (DiffB < DiffG))
        if ((DiffB <= 10 && DiffB > -1) && (DiffR <= -1) && (DiffBl <= -1) && (DiffG <= 10 && DiffG > -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'D and C in positive region and D < C'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                if (ds4.Tables[0].Rows[0][8].ToString() != "")
                {
                    doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                    p8.Add(limage);
                    p8.SetAlignment("justify");
                    doc.Add(p8);
                }

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You are a highly formal and structured individual with a forceful and blunt style. You believe in getting things right, and is rarely afraid to state your mind robustly and directly. You represent the least forthcoming in personal or emotional matters; and tend to be remote and somewhat isolated, preferring to keep your own counsel.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t As we suggested above, relating to others (at least on a personal level) is not a high priority for you. When communication with others is essential, it tends to be brief and succinct, focusing on practical matters. You are quite distrustful of others, and will prefer to keep facts to yourselves unless absolutely necessary. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You are motivated by achievement and efficiency. However, this is modulated by the presence of a high Compliance factor in you, where you have interest in detail and precision. A noticeable element is your tendency to correct other people when they make errors, even to the point of highlighting mistakes that others might regard as trivial or unimportant. Nonetheless, this combination of efficiency and precision can be an effective one, and your bluntly. assertive style helps you to achieve difficult tasks by sheer force of character. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You have a desire for personal achievement and success, but you also like to feel that you are completing assignments or projects accurately and efficiently. The naturally unexpressive style of your personality can make it difficult to detect whether or not you are motivated in any particular set of circumstances.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation , Accuracy and Sensitivity. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }

            //   string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'efficeint/ accurate ' , c_profile_name = 'Diligent',c_suitability='Specialist',c_specialisation_I='Finance',c_specialisation_II='Operations',c_specialisation_III='HR Backend' where c_id='" + c_id + "'";
            //  int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        // 12. I and S in positive region and I > S
        //if ((DiffB < -1 && DiffG < -1) && (DiffR < 10 && DiffR > -1) && (DiffBl < 10 && DiffBl > -1) && (DiffR > DiffBl))
        if ((DiffB <= -1 && DiffG <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= 10 && DiffBl > -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'I and S in positive region and I > S'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You are more oriented towards feelings and emotions than hard facts and practicalities. In combination, you are oriented towards personal matters and the understanding of other people. You are confident, warm and friendly, but nonetheless you incorporate a sympathetic ear for others and a readiness to help with others' problems where possible. You could be described as a person having 'Counselor Profile.' ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You tend to be most effective at relating to other people, in an all round sense. You socialize easily and your gregarious nature allows you to feel at ease with people you do not know. You are often persuasive and charming, but you are also able to adopt a more open, relaxed approach when a situation demands, becoming less directly active and more passively receptive to the ideas and feelings of other people. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You have strong abilities in the areas of communication and understanding. You tend to fulfill supportive roles well, being understanding and sympathetic, but your more outgoing side allows you to operate effectively in a social or persuasive sense. It should be noted, however, that you tend to place less emphasis on matters of practicality. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t Antagonism, rejection and confrontation are all situations that you will try to avoid. To use your considerable communicative powers, you will need to feel that you are operating in a favorable environment, and that those around you are sympathetic and approving. To feel completely motivated, you need to feel that you are appreciated, respected and liked by the people around you, and will sometimes go to unusual lengths to attract positive attention.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are  Friendliness, Self-confidence, Patience and and Persistence ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }
            //   string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'friendly / confident' , c_profile_name = 'Friendly Counsellor',c_suitability='Generalist',c_specialisation_I='Human Resources',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //  int i = clsdal.ExecNonQuery(strcmd);
            return;
        }
        // 13. I and C in positive region and I > C
        //if ((DiffB <= -1 && DiffBl <= -1) && (DiffR < 10 && DiffR > -1) && (DiffG < 10 && DiffG > -1) && (DiffR > DiffG))
        if ((DiffB <= -1 && DiffBl <= -1) && (DiffR <= 10 && DiffR > -1) && (DiffG <= 10 && DiffG > -1))
        {
            Paragraph p1 = new Paragraph("\t In relaxed, open and favorable situation, you display excitement, enjoyment and extrovert impulsiveness; on the other hand in more formal or structured circumstances, you are a detailed person who carefully follows rules and possess precision. The differences in approach between these two factors are resolved in an unusual approach. Normally, two or more high personality factors will tend to reinforce each other's common points, and blend to make up the entire style. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t The way you relate to other people is highly dependent on the circumstances under which an encounter takes place. Within a close circle of colleagues of friends, you are capable of quite confident and extrovert behavior. In a more formal work environment, or pressurized atmosphere, such as an interview for a job, this confidence will often seem to evaporate, and you tend to get compliant.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph(" \t Your personality style combines the abilities of a pure friendly and influential and a structured process oriented, but, as we have noted above, these abilities will not all be apparent at the same time. Different environments will produce different responses. Hence there is a high need to ideally adapt to different working environments. You are basically confident and have a communicative behavior rather than being a reticent person and showing caution. You will display different abilities indifferent situations; can be charming and enthusiastic, or direct and forthright, or cool or distant depending on a  particular circumstances. From a manager's perspective, it will necessary to adapt your working environment, so far as is possible, to bring out the particular style that is most appropriate. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t Your  motivations are more complex than most, because of the opposing natures of your two main factors of personality. This means you are interested in the attention and approval of others, but you are  less likely to be demonstrate it  overtly, and will instead be more subtle and discreet. You will be looking for certainty and sureness, and also look for a clear idea of your position and the expectations of those around you. You seem to be interested in the attention and approval of others, but at the same time, will look for a clear idea of your position and the expectations of those around you. You don’t ask for explicit instructions directly. You have a complex sets of motivating factors that may sometimes conflict with one another. In his  case, motivation stems from the achievement of personal ambition, the acceptance and approval of other people, and certainty of your position. Where these are incompatible, as for example if the fulfilment of a goal requires a risk to be taken, and also face uncertainty. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Friendliness ,Enthusiasm, Co-operativeness and Sensitivity.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            // string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'enthusiatic/ self-confident' , c_profile_name = 'Enthusiastic Confident',c_suitability='Generalist',c_specialisation_I='Human Resources',c_specialisation_II='Marketing',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;

        }


        // 14. S and C High
        if ((DiffB <= -1 && DiffR <= -1) && (DiffG <= 10 && DiffG > -1) && (DiffBl <= 10 && DiffBl > -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'S and C High with C > S'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {


                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);


            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You are passive by nature, precise and a systematic  thinker. Intelectually sound you tend to focus on perfection and accuracy. A perfectionist by nature, you set high standards of quality and accuracy and strive to achieve them.  A process oriented individual, who prefers to design processes and set the rules and follow them too. Often seen as reticent and aloof, with your kind of approach you  give an impression of coldness or disinterest(which may always not be so). Much of your impassive style stems from your controlled nature, however, which makes you a reluctant communicatior. You speak only if necesary and only when it forms a part of your core interest of expertise.  In fact, you are surprisingly ambitious and has lofty goals.You  tend to use existing structures and rules to accomplish your aims. You are also extremely logical, structured and systematic in your approach towards work.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nRelating to Others :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You have many strengths, but the ability to relate easily to other people is rare. You have sound knowledge and intellect  and many a times finds yourself in a situation where your intellect is higher than the group. You prefer to relate to individuals on a one on one basis rather than connecting with them in a group. The combination of a passive social style with a certain innate suspiciousness makes it difficult for you to form or maintain close relationships, and this is especially true in a business sense. Your friendships or close acquaintances will normally be based on mutual interests or common aims, rather than emotional considerations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nCommon Abilities  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("  \t Your strengths can be summarised as 'diligent' , perfectionist and  self reliant. You are  dependable and loyal, this combines with an emotional literacy to make you particularly an effective listener and a counsellor.  You are  persistent in approach, having the patience and restraint to work steadily at a task until it is achieved. You are very self-reliant, with your structured way of thinking you often show your strength when it comes to organising facts or working with precise detail or sophisticated systems. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t Self reliant and perfectionism is the root source of motivation in you. You are detailed oriented, and plan with extreme details, however you believe that the goal is met if the process is ahered to. At times due to the need to be precise and detail oriented you would be seen as slow paced, however your thoughtful nature and deligence improves the quality of decisions you make. You brief others with extreme care, and resolve queries and gainsexpertise in your area of work. You are also  respected for your expertise and specialisation. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits in you are Persistence, Thoughtfulness and Accuracy.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }
            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'perfectionst/ sensitive' , c_profile_name = 'Perfectionist',c_suitability='Specialist',c_specialisation_I='Finance',c_specialisation_II='Operations',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }

        //HD,HS,HC high DSC
        if ((DiffB <= 10 & DiffB > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffG <= 10 && DiffG > -1) && (DiffR <= -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'High DSC - C Highest'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {

                Paragraph p1 = new Paragraph("\t As a person you are highly analytical and relatively uncommunicative. You are basically practical in nature and rational in your thought process. Emotions don’t bother you much. You are generally secretive by nature and cautious in communication, rarely revealing much about yourselves than a bare minimum. In challenging and stressful situations, you display an assertive and forceful behaviour, whereas in easy environment you are much relaxed.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t Your inclination to relate to people in general is usually found low. You have a passive approach towards reacting to others’ comments or ideas. You prefer not to provide any direct inputs to others unless you are asked for. When you are in certain difficult or demanding situations your willingness to communicate do increase significantly, but to communicate on a personal level is normally low. Being yourself precise and accurate, you tend to demand similar kind of behaviour from people around you. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(" \t You never act on impulse and take unnecessary risks. You put extreme emphasis on results and productivity, leading to a disciplined purposeful approach, where actions are carefully planned with caution and care. You are comfortable with complex systems and processes and work well with facts. You emphasize on efficiency and effectiveness, and that is reflected in both your professional and personal front. If you start something, you like to carry it through the end. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You like to solve your problems in your own way and achievement of results motivates you. You need opportunity to prove yourself. You prefer to get enough time to adapt to changing situations as you normally have affinity towards stable and familiar environment.  You have the need for time to plan and execute your work carefully and thereby delivering high quality job gives you immense satisfaction. You need full understandings of facts and specific knowledge of the job and tend to seek freedom from risk. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits in you are accuracy, efficiency and thoughtfulness.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


            }

            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'thoughtful/ disciplined' , c_profile_name = 'Thoughtful',c_suitability='Specialist',c_specialisation_I='Finance',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //  int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        //HI,HD,HC high DCI
        if ((DiffB <= 10 & DiffB > -1) && (DiffG <= 10 && DiffG > -1) && (DiffR <= 10 && DiffR > -1) && (DiffBl <= -1))
        {


            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'High DCI I>D>C'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p8.Add(limage);
                p8.SetAlignment("justify");
                doc.Add(p8);

                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }
            if (SConditions == true)
            {
                Paragraph p1 = new Paragraph("\t You are basically fast paced and have high sense of urgency. You are impatient with routines and love variety. You are self-confident and have challenging and competitive approach and at the same time you are socially poised and very outgoing by nature. Though you are extremely assertive and have high need to achieve success, you work in consensus and exhibit caution in dealing with situations. You normally do not show unpredictability in your approach.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You display effective social abilities, particularly evident in any informal open situations, where you exhibit a friendly, open and enthusiastic approach. In formal environment, however, you have self-controlled, driving attitude and display a less sociable style. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(" \t Depending on situations, you can be charming and enthusiastic, or direct and forthright. You are socially aware, and able to detect nuances in communication. In demanding situation, you display usual outgoing and expressive behaviour, but is bolstered by a more assertive approach. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t Your motivation stems from the achievement of personal ambition, recognition for own ideas, social acceptance and social recognition, and certainty of your position.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits in you are enthusiasm, self-motivation and sensitivity.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }
            //    string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'competitive/ mobile' , c_profile_name = 'Competitive',c_suitability='Generalist',c_specialisation_I='Marketing',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //   int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        //HI,HD,HS high DSI
        if ((DiffB <= 10 & DiffB > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffR <= 10 && DiffR > -1) && (DiffG <= -1))
        {

            Paragraph p1 = new Paragraph("\t As a person you are dynamic and direct and have an independent behavioral style. You have a clear idea about your goals in life and you equally have the strength to achieve it. You have a more single-minded and stubborn approach to situations. Your remarkable tenacity and determination help you to attain your goal in life. Though you mix easily with strangers, and are unafraid to initiate social contact, you have a powerful, persuasive, confident behavioral style. This helps you to deal directly and fearlessly with most situations. While you will typically prefer to keep matters on an open and friendly level, you are quite capable of adopting a more determined and confrontational stance where a situation calls for. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You interact easily and skillfully with people around you, and rarely experience self-doubt, and you feel at ease in almost any social situations. You are unafraid to initiate social contact however you have a strong sense of independence and tend to maintain your own sense of identity, and tend to protect and defend your own viewpoint.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph(" \t : You exhibit dependency and at the same time have strong social abilities. This helps you to take initiative in every activity with great confidence and assertiveness. Your self- reliance style with a strong sense of personal responsibility makes you an effective facilitator. Being patient and persistent along with assertiveness helps you to achieve the results. Further, you also consider and weigh the options before arriving at a definite conclusion. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You like to have control over your own state of affairs, and being persistent and assertive, like to do things in your own way. While success and achievements are important to you, you also value positive and friendly relationships with other people. Thereby, under certain situation you might even delay achieving your goals, if there is some sort of conflict existing with others' needs.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits in you are independence, self-confidence and persistence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'determined/dynamic' , c_profile_name = 'Determined and Dynamic',c_suitability='Generalist',c_specialisation_I='Marketing',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //   int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        //HI,HS,HC high ISC
        if ((DiffG <= 10 & DiffG > -1) && (DiffBl <= 10 && DiffBl > -1) && (DiffR <= 10 && DiffR > -1) && (DiffB <= -1))
        {
            bool SConditions = false;
            string StrQuery = "";
            StrQuery = "SELECT DISTINCT condition_id,personal_summary,other_relation,strengths,motivation,basic_descriptors,leadership_style,goal_setting,work_environment,creativity_area,work_delegation,communication_style,decision_style FROM tblSalesReportConditions WHERE report_type = 'Browser_Report' AND condition = 'High ISC with  C > S > I'";
            DataSet ds4 = dal.ExecDataSet(StrQuery);
            if (ds4.Tables[0].Rows.Count > 0)
            {

                Paragraph p1 = new Paragraph(ds4.Tables[0].Rows[0][1].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);

                doc.Add(new Paragraph("\nHow do you relate to others :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph(ds4.Tables[0].Rows[0][2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);


                doc.Add(new Paragraph("\nYour general areas of strengths  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(ds4.Tables[0].Rows[0][3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);


                doc.Add(new Paragraph("\nWhat motivates you  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph(ds4.Tables[0].Rows[0][4].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);


                doc.Add(new Paragraph("\nBasic descriptors  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph(ds4.Tables[0].Rows[0][5].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);


                doc.Add(new Paragraph("\nYour leadership style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p6 = new Paragraph(ds4.Tables[0].Rows[0][6].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p6.Add(limage);
                p6.SetAlignment("justify");
                doc.Add(p6);

                doc.Add(new Paragraph("\nYour goal setting behaviour   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p7 = new Paragraph(ds4.Tables[0].Rows[0][7].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p7.Add(limage);
                p7.SetAlignment("justify");
                doc.Add(p7);

                if (ds4.Tables[0].Rows[0][8].ToString() != "")
                {
                    doc.Add(new Paragraph("\nYour preferred work environment   :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    Paragraph p8 = new Paragraph(ds4.Tables[0].Rows[0][8].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                    p8.Add(limage);
                    p8.SetAlignment("justify");
                    doc.Add(p8);
                }
                doc.Add(new Paragraph("\nYour area of creativity  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p9 = new Paragraph(ds4.Tables[0].Rows[0][9].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p9.Add(limage);
                p9.SetAlignment("justify");
                doc.Add(p9);


                doc.Add(new Paragraph("\nHow do you delegate work  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p10 = new Paragraph(ds4.Tables[0].Rows[0][10].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p10.Add(limage);
                p10.SetAlignment("justify");
                doc.Add(p10);

                doc.Add(new Paragraph("\nYour communication style :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p11 = new Paragraph(ds4.Tables[0].Rows[0][11].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p11.Add(limage);
                p11.SetAlignment("justify");
                doc.Add(p11);


                doc.Add(new Paragraph("\nYour decision making style  :-", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p12 = new Paragraph(ds4.Tables[0].Rows[0][12].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p12.Add(limage);
                p12.SetAlignment("justify");
                doc.Add(p12);

            }
            else
            {
                SConditions = true;
            }

            if (SConditions == true)
            {


                Paragraph p1 = new Paragraph("\t You are basically warm, friendly and outgoing by nature. You are patient and generally a good listener. You work cooperatively with others and you love team oriented environment. You tend to avoid individual risk and tend to accept others’ ideas. You are normally not very assertive and dominant type and thereby you prefer to achieve your ends through communication, using your persuasive abilities and rationality. You usually distribute responsibility and tend to concentrate particularly on the details of a task. You are not very ambitious kind and are happy in building strong social relationships. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You work harmoniously with the group. Your outgoing friendly style along with your patience and accommodative nature makes you a good listener. Your strong analytical rationale helps you to present your ideas and arguments in convincing and logical manner.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph(" \t Your warm friendly, extroverted easy going nature helps you to be a good team player.  You usually entertain ideas from your team. You are receptive to other people and sympathetic to their point of view. With your patience and steadiness, you have high tolerance to repetitive work.  Your confidence helps you to maintain a proactive approach to the situation, though it generally lacks effectiveness due to your low assertiveness. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t Warm, friendly, extroverted environment makes you happy and content. You are not ambitious kind but supportive ‘family-like’ work team motivates you. You need time to adapt to new situations or changes and prefer a stable work environment. A sense of sureness about your position especially in social terms motivates you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits in you are cooperativeness, friendliness and patience.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

            }
            //      string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'extroverted/ cooperative' , c_profile_name = 'Extroverted & Friendly',c_suitability='Generalist',c_specialisation_I='Human Resources',c_specialisation_II='Marketing',c_specialisation_III='-' where c_id='" + c_id + "'";
            //      int i = clsdal.ExecNonQuery(strcmd);
            return;

        }

        // Basic Prosiles
        // flick - up 
        if (T1 == true || T2 == true || T3 == true)
        {
            Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You are best in Connecting to People. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph(" \t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            //    string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'independent/ firm' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //   int i = clsdal.ExecNonQuery(strcmd);
            return;


        }
        // flick - down
        if (TD1 == true || TD2 == true || TD3 == true)
        {
            Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You are best in Connecting to People. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            //   string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'rebellious/bold' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //   int i = clsdal.ExecNonQuery(strcmd);
            return;
        }
        // sweep - down
        if ((DiffB < 10 && DiffB >= -1) && (DiffR < 10 && DiffR >= -1) && (DiffBl > -11 && DiffBl <= -1) && (DiffG > -11 && DiffG <= -1))
        {
            if ((DiffB < DiffR) && (DiffBl - DiffG > 4))
            {
                Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You are best in Connecting to People. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

                //      string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'carefree attitude/ happy-go-lucky' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
                //     int i = clsdal.ExecNonQuery(strcmd);
                return;
            }
        }
        // 8-5 Pattern
        if ((DiffB >= -4 && DiffB <= 2) && (DiffR >= -4 && DiffR <= 2) && (DiffBl >= -4 && DiffBl <= 2) && (DiffG >= -4 && DiffG <= 2))
        {
            if ((DiffB >= -4 || DiffB <= -1) && (DiffR >= -1 && DiffR <= 2) && (DiffBl <= -1 && DiffBl >= -4) && (DiffG >= -1 && DiffG <= 2))
            {
                Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You are best in Connecting to People. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);



                //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'compressed profile/ artefactual ?' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
                //  int i = clsdal.ExecNonQuery(strcmd);
                return;
            }
        }
        // D=C 
        if (DiffB > -1 && DiffR <= -1 && DiffBl <= -1 && DiffG > -1)
        {
            if (DiffB == DiffR)
            {
                Paragraph p1 = new Paragraph("\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p1.Add(limage);
                p1.SetAlignment("justify");
                doc.Add(p1);


                doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p2 = new Paragraph("\t You tend to stress on achievement and success significantly, which affects your relations with other people. In extreme cases, you seem to treat other people simply as a means to an end, or a way of achieving your personal goals. Since you are not emotional, you tend not to place great importance on feelings, either your own or others'. The competitive nature in you leads to see challenges and opposition everywhere, and others sometimes find it difficult to break through this naturally suspicious, skeptical shell of yours. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p2.Add(limage);
                p2.SetAlignment("justify");
                doc.Add(p2);

                doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p3 = new Paragraph("\t You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p3.Add(limage);
                p3.SetAlignment("justify");
                doc.Add(p3);

                doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p4 = new Paragraph("\t You like to feel that you are in control, and seek opportunities to reinforce and emphasize your personal power. You measure your progress in life by your achievements and successes, and feel the need to maintain a sense of personal momentum. Being impatient and forthright, you intensely dislike situations that you are unable to directly resolve for yourselves - dependence on other people is anathema to you. You find these kinds of situation extremely frustrating, and can be driven to wild, impulsive actions in an attempt to relieve the pressure.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p4.Add(limage);
                p4.SetAlignment("justify");
                doc.Add(p4);

                doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation and Independence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
                p5.Add(limage);
                p5.SetAlignment("justify");
                doc.Add(p5);

                //      string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'indecisive' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
                //      int i = clsdal.ExecNonQuery(strcmd);
                return;
            }
        }
        // Overshift
        if (DiffB > -1 && DiffR > -1 && DiffBl > -1 && DiffG > -1)
        {
            Paragraph p1 = new Paragraph("\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You tend to stress on achievement and success significantly, which affects your relations with other people. In extreme cases, you seem to treat other people simply as a means to an end, or a way of achieving your personal goals. Since you are not emotional, you tend not to place great importance on feelings, either your own or others'. The competitive nature in you leads to see challenges and opposition everywhere, and others sometimes find it difficult to break through this naturally suspicious, skeptical shell of yours. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You like to feel that you are in control, and seek opportunities to reinforce and emphasize your personal power. You measure your progress in life by your achievements and successes, and feel the need to maintain a sense of personal momentum. Being impatient and forthright, you intensely dislike situations that you are unable to directly resolve for yourselves - dependence on other people is anathema to you. You find these kinds of situation extremely frustrating, and can be driven to wild, impulsive actions in an attempt to relieve the pressure.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation and Independence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);


            //   string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'please everybody' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            //  int i = clsdal.ExecNonQuery(strcmd);
            return;
        }
        // Undershift
        if (DiffB <= -1 && DiffR <= -1 && DiffBl <= -1 && DiffG <= -1)
        {

            Paragraph p1 = new Paragraph("\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You tend to stress on achievement and success significantly, which affects your relations with other people. In extreme cases, you seem to treat other people simply as a means to an end, or a way of achieving your personal goals. Since you are not emotional, you tend not to place great importance on feelings, either your own or others'. The competitive nature in you leads to see challenges and opposition everywhere, and others sometimes find it difficult to break through this naturally suspicious, skeptical shell of yours. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You like to feel that you are in control, and seek opportunities to reinforce and emphasize your personal power. You measure your progress in life by your achievements and successes, and feel the need to maintain a sense of personal momentum. Being impatient and forthright, you intensely dislike situations that you are unable to directly resolve for yourselves - dependence on other people is anathema to you. You find these kinds of situation extremely frustrating, and can be driven to wild, impulsive actions in an attempt to relieve the pressure.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation and Independence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            //  string strcmd = "UPDATE tbl_candidate_master set c_short_char = 'demoralised' , c_profile_name = '-',c_suitability='-',c_specialisation_I='-',c_specialisation_II='-',c_specialisation_III='-' where c_id='" + c_id + "'";
            // int i = clsdal.ExecNonQuery(strcmd);
            return;
        }
        // in grey zone
        if (DiffB > 10)
        {
            Paragraph p1 = new Paragraph("\t You are often described as the 'Autocrat', and for good reason. You display a high level of control and assertiveness, and remarkably domineering, and even overbearing at times.You have a very strong need to achieve, and because of this you are often ambitious and competitive, striving aggressively to achieve your goals. You are dynamic and adaptable, and show decisiveness and a capacity for direct leadership.   ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You tend to stress on achievement and success significantly, which affects your relations with other people. In extreme cases, you seem to treat other people simply as a means to an end, or a way of achieving your personal goals. Since you are not emotional, you tend not to place great importance on feelings, either your own or others'. The competitive nature in you leads to see challenges and opposition everywhere, and others sometimes find it difficult to break through this naturally suspicious, skeptical shell of yours. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You have qualities of command and leadership. It should be noted, however, that these abilities are based on your direct, demanding nature, and are more suited to structured, formal situations than those where close ties are required. You are a competent and confident decision-maker, able to reach a conclusion quickly from minimal information and act accordingly. You are well suited to situations that others would find unbearably stressful, as your desire for challenge and your enjoyment of success against the odds makes you unusually proficient in dealing with such situations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Factors  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You like to feel that you are in control, and seek opportunities to reinforce and emphasize your personal power. You measure your progress in life by your achievements and successes, and feel the need to maintain a sense of personal momentum. Being impatient and forthright, you intensely dislike situations that you are unable to directly resolve for yourselves - dependence on other people is anathema to you. You find these kinds of situation extremely frustrating, and can be driven to wild, impulsive actions in an attempt to relieve the pressure.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Efficiency, Self-motivation and Independence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            return;
        }
        if (DiffR > 10)
        {

            Paragraph p1 = new Paragraph("\t Communication is the key factor in you. You can communicate easily and fluently with others. You are often referred to as 'Communicator' profiles – you are confident, outgoing and gregarious individual who value contact with other people and the development of positive relations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You are best in Connecting to People. You are open to others and confident in your own social abilities, and interact positively in almost any situation. Your strong and evident confidence, coupled with your genuine interest in the ideas and especially feelings of other people, are often found charming by those around you. Your actions will often be designed to improve and extend relations, even to the extent of alienating people who are not part of your circle. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You distinct abilities lie in the area of communication and relationship building. You are not only a strong communicator, possessing the assertiveness to drive home a point of view, but also have the intuitive qualities to understand others' perspectives and adapt others to meet new situations.   ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Aspect  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You have a natural talent to connect to people. You have no inhibitions in getting connected to different type of people. This also stems from the fact that you have an inner confidence to build a long term relation. Specifically, you need to feel accepted by those around you, and react badly if you perceive yourselves to be rejected or disliked. Praise and approval make a strong impression on you, and you will sometimes go to great lengths to achieve this kind of reaction from other people.Especially important to you are the opinions and reactions of your particularly close circle. You believe in creating a positive environment that enhances the well being of people around you.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub--traits of this type are Friendliness , Enthusiasm and Self--confidence.", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            return;
        }
        if (DiffBl > 10)
        {

            Paragraph p1 = new Paragraph("\t You show high degree of patience, calmness and gentle openness. You are generally amiable and warm-hearted, being sympathetic to others' points of view, and valuing positive interaction with others. You are not outgoing by nature; however, rely on other, more assertive, people to take the lead.   ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t As in your general lifestyle, you initiate relationships of any kind – yours solid, dependable outlook makes you far more suited to the maintenance of interpersonal relations than making initial contact. For this reason, your circle of friends and close acquaintances is often small but tightly-knit. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You are simply 'supportive'. You are dependable and loyal, this combines with an emotional literacy to make you particularly effective listeners and counselors. You are also unusually persistent in approach, having the patience and restraint to work steadily at a task until it is achieved. This makes you unusually capable of dealing with laborious tasks that many other styles would simply not have the patience to complete.   ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Aspect  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t Patience is the root source of motivation in you. You need to feel that you have the support of those around you and, more importantly, time to adapt to new situations. You have an inherent dislike of change, and will prefer to maintain the status quo whenever possible; sudden alterations in your circumstances can be very difficult for you to deal with. Once embarked on a task, you will wish to concentrate closely on it and see it through. Interruptions and distractions of any kind can be particularly demotivating in these situations. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Patience , Thoughtfulness and Persistence..", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            return;
        }
        if (DiffG > 10)
        {

            Paragraph p1 = new Paragraph("\t Passive by nature, often reticent and aloof, you often tend to give an impression of coldness or disinterest. Much of this impassive style stems from your controlled nature, however, which makes you reluctant to reveal information about yourselves or your ideas unless absolutely necessary. In fact, you are often ambitious and have lofty goals, but your innate lack of assertiveness and unwillingness to become involved in confrontational situations makes it difficult for you to achieve these goals directly. Instead, you will tend to use existing structures and rules to accomplish your aims. You tend to follow rules, authority and logical argument to influence the actions of others.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p1.Add(limage);
            p1.SetAlignment("justify");
            doc.Add(p1);


            doc.Add(new Paragraph("\nConnecting to People :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p2 = new Paragraph("\t You have much strength, but the ability to relate easily to other people is rarely in you. The combination of a passive social style with a certain innate suspiciousness makes it difficult for you to form or maintain close relationships, and this is especially true in a business sense. Your friendship or close acquaintances will normally be based on mutual interests or common aims, rather than emotional considerations.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p2.Add(limage);
            p2.SetAlignment("justify");
            doc.Add(p2);

            doc.Add(new Paragraph("\nGeneral Faculty :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p3 = new Paragraph("\t You are generally very self-reliant although this fact is often difficult to perceive. You have structured ways of thinking, and often show particular strengths when it comes to organizing facts or working with precise detail or sophisticated systems. You have a quick-thinking individual who will often have useful input, but your natural reticence means that you will rarely offer an opinion unless asked directly for your thoughts.  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p3.Add(limage);
            p3.SetAlignment("justify");
            doc.Add(p3);

            doc.Add(new Paragraph("\nMotivating Aspect  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p4 = new Paragraph("\t You need to feel completely sure of your position, and of others' expectations of you, before you are able to proceed. Because of this, you have a very strong aversion to risk, and will rarely take any action unless you can feel absolutely sure about its consequences. ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p4.Add(limage);
            p4.SetAlignment("justify");
            doc.Add(p4);

            doc.Add(new Paragraph("\nDescriptive Traits  :- ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            Paragraph p5 = new Paragraph("\t The sub-traits of this type are Cooperativeness, Accuracy and Sensitivity..", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(104, 108, 114)));
            p5.Add(limage);
            p5.SetAlignment("justify");
            doc.Add(p5);

            return;
        }


    }
    private void ps1()
    {


        strcmd = "select rating,P_rating,factor_no from tblKYCandFactors1 where batid=" + batid + " and c_id = " + c_id + " order by P_rating";
        //strcmd = "select * from tbl_KY_cand_factors where c_id = '" + c_id + "'order by factor_no";
        DataSet ds1 = dal.ExecDataSet(strcmd);

        Chart1.TempDirectory = "~/Graph_Report/Reports_graph"; //Server.MapPath("~/images");
        Chart1.Debug = true;
        //Chart1.Palette = new Color[]{Color.FromArgb(49,255,49),Color.FromArgb(255,255,0),Color.FromArgb(255,99,49),Color.FromArgb(0,156,255)};

        Chart1.Type = ChartType.PiesNested;
        Chart1.Size = "1000x750";
        Chart1.Title = "";
        Chart1.Mentor = false;
        Chart1.DefaultChartArea.TitleBox.Visible = false; 
        Chart1.ChartArea.Visible = false;


        int a = 1;
        int b = 1;
        dotnetCHARTING.SeriesCollection SC = new dotnetCHARTING.SeriesCollection();

        /////// first series
        dotnetCHARTING.Series s = new dotnetCHARTING.Series("Series " + a.ToString());

        if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 11)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 21)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 31)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 41)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 51)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 61)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 71)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 81)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) < 91)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString()) > 90)
        {
            s.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213), System.Drawing.Color.FromArgb(9, 85, 213) };
        }


        //s.Palette = new System.Drawing.Color[] { System.Drawing.Color.DeepSkyBlue, System.Drawing.Color.White };
        dotnetCHARTING.Element e1 = new dotnetCHARTING.Element("Element " + b.ToString());
        //e1.Outline.Width = 0;
        e1.Outline.Color = System.Drawing.Color.White;
        //e1.YValue = Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString());
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element " + b.ToString() + "rr");
        //e1.Outline.Width = 0;
        e1.Outline.Color = System.Drawing.Color.White;
        // e1.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString());
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);


        e1 = new dotnetCHARTING.Element("Element rr1");
        //e1.Outline.Width = 0;
        e1.Outline.Color = System.Drawing.Color.White;
        // e1.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[8][1].ToString());
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr2");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr3");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr4");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr5");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr6");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr7");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);

        e1 = new dotnetCHARTING.Element("Element rr8");
        e1.Outline.Color = System.Drawing.Color.White;
        e1.YValue = 100 - Convert.ToDouble(10);
        s.Elements.Add(e1);


        SC.Add(s);


        /////// second series
        dotnetCHARTING.Series ss = new dotnetCHARTING.Series("Series " + "dd");


        if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 11)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 21)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 31)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 41)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 51)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 61)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 71)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 81)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) < 91)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString()) > 90)
        {
            ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222), System.Drawing.Color.FromArgb(43, 85, 222) };
        }




        // ss.Palette = new System.Drawing.Color[] { System.Drawing.Color.HotPink, System.Drawing.Color.White };

        dotnetCHARTING.Element e2 = new dotnetCHARTING.Element("Element " + "e1");
        //e2.Outline.Width = 0;
        e2.Outline.Color = System.Drawing.Color.White;
        // e2.YValue = Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString());
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);


        e2 = new dotnetCHARTING.Element("Element " + "r");
        //e2.Outline.Width = 0;
        e2.Outline.Color = System.Drawing.Color.White;
        //e2.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[7][1].ToString());
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r1");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r2");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r3");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r4");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r5");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r6");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r7");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        e2 = new dotnetCHARTING.Element("Element " + "r8");
        e2.Outline.Color = System.Drawing.Color.White;
        e2.YValue = Convert.ToDouble(10);
        ss.Elements.Add(e2);

        SC.Add(ss);

        /////// third series
        dotnetCHARTING.Series ss1 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 11)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 21)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 31)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 41)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 51)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 61)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 71)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 81)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) < 91)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString()) > 90)
        {
            ss1.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153), System.Drawing.Color.FromArgb(0, 76, 153) };
        }


        dotnetCHARTING.Element e3 = new dotnetCHARTING.Element("Element " + "e1");
        //e3.Outline.Width = 0;
        e3.Outline.Color = System.Drawing.Color.White;
        //e3.YValue = Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString());
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);


        e3 = new dotnetCHARTING.Element("Element " + "r");
        //e3.Outline.Width = 0;
        e3.Outline.Color = System.Drawing.Color.White;
        //e3.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[6][1].ToString());
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r1");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r2");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r3");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r4");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r5");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r6");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r7");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);

        e3 = new dotnetCHARTING.Element("Element " + "r8");
        e3.Outline.Color = System.Drawing.Color.White;
        e3.YValue = Convert.ToDouble(10);
        ss1.Elements.Add(e3);



        SC.Add(ss1);


        /////// fourth series
        dotnetCHARTING.Series ss2 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 11)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 21)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 31)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 41)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 51)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 61)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 71)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 81)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) < 91)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString()) > 90)
        {
            ss2.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204), System.Drawing.Color.FromArgb(0, 102, 204) };
        }

        dotnetCHARTING.Element e4 = new dotnetCHARTING.Element("Element " + "e1");
        //e4.Outline.Width = 0;
        e4.Outline.Color = System.Drawing.Color.White;
        //e4.YValue = Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString());
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);


        e4 = new dotnetCHARTING.Element("Element " + "r");
        //e4.Outline.Width = 0;
        e4.Outline.Color = System.Drawing.Color.White;
        //e4.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[5][1].ToString());
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r1");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r2");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r3");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r4");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r5");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r6");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r7");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);

        e4 = new dotnetCHARTING.Element("Element " + "r8");
        e4.Outline.Color = System.Drawing.Color.White;
        e4.YValue = Convert.ToDouble(10);
        ss2.Elements.Add(e4);


        SC.Add(ss2);


        /////// fifth series
        dotnetCHARTING.Series ss3 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 11)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 21)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 31)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 41)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 51)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 61)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 71)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 81)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) < 91)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString()) > 90)
        {
            ss3.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.SkyBlue, System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255), System.Drawing.Color.FromArgb(51, 153, 255) };
        }

        dotnetCHARTING.Element e5 = new dotnetCHARTING.Element("Element " + "e1");
        //e5.Outline.Width = 0;
        e5.Outline.Color = System.Drawing.Color.White;
        //e5.YValue = Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString());
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);


        e5 = new dotnetCHARTING.Element("Element " + "r");
        //e5.Outline.Width = 0;
        e5.Outline.Color = System.Drawing.Color.White;
        //e5.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[4][1].ToString());
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r1");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r2");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r3");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r4");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r5");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r6");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);


        e5 = new dotnetCHARTING.Element("Element " + "r7");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        e5 = new dotnetCHARTING.Element("Element " + "r8");
        e5.Outline.Color = System.Drawing.Color.White;
        e5.YValue = Convert.ToDouble(10);
        ss3.Elements.Add(e5);

        SC.Add(ss3);


        /////// sixth series
        dotnetCHARTING.Series ss4 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 11)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 21)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 31)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 41)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 51)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 61)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 71)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 81)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) < 91)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString()) > 90)
        {
            ss4.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255), System.Drawing.Color.FromArgb(153, 204, 255) };
        }


        dotnetCHARTING.Element e6 = new dotnetCHARTING.Element("Element " + "e1");
        //e6.Outline.Width = 0;
        e6.Outline.Color = System.Drawing.Color.White;
        //e6.YValue = Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString());
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);


        e6 = new dotnetCHARTING.Element("Element " + "r");
        //e6.Outline.Width = 0;
        e6.Outline.Color = System.Drawing.Color.White;
        //e6.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[3][1].ToString());
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r1");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r2");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r3");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r4");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r5");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r6");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r7");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);

        e6 = new dotnetCHARTING.Element("Element " + "r8");
        e6.Outline.Color = System.Drawing.Color.White;
        e6.YValue = Convert.ToDouble(10);
        ss4.Elements.Add(e6);



        SC.Add(ss4);

        /////// seventh series
        dotnetCHARTING.Series ss5 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 11)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 21)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 31)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 41)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 51)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 61)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 71)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 81)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) < 91)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightSteelBlue, System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString()) > 90)
        {
            ss5.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.LightSteelBlue, System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255), System.Drawing.Color.FromArgb(0, 128, 255) };
        }


        dotnetCHARTING.Element e7 = new dotnetCHARTING.Element("Element " + "e1");
        //e7.Outline.Width = 0;
        e7.Outline.Color = System.Drawing.Color.White;
        //        e7.YValue = Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString());
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);


        e7 = new dotnetCHARTING.Element("Element " + "r");
        //e7.Outline.Width = 0;
        e7.Outline.Color = System.Drawing.Color.White;
        //e7.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[2][1].ToString());
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r1");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r2");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r3");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r4");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r5");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r6");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r7");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        e7 = new dotnetCHARTING.Element("Element " + "r8");
        e7.Outline.Color = System.Drawing.Color.White;
        e7.YValue = Convert.ToDouble(10);
        ss5.Elements.Add(e7);

        SC.Add(ss5);

        /////// eightth series
        dotnetCHARTING.Series ss6 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 11)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 21)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 31)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 41)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 51)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 61)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 71)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 81)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) < 91)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString()) > 90)
        {
            ss6.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255), System.Drawing.Color.FromArgb(102, 178, 255) };
        }


        dotnetCHARTING.Element e8 = new dotnetCHARTING.Element("Element " + "e1");
        //e8.Outline.Width = 0;
        e8.Outline.Color = System.Drawing.Color.White;
        //e8.YValue = Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString());
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);


        e8 = new dotnetCHARTING.Element("Element " + "r");
        //e8.Outline.Width = 0;
        e8.Outline.Color = System.Drawing.Color.White;
        //e8.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[1][1].ToString());
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r1");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r2");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r3");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r4");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r5");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r6");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r7");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);

        e8 = new dotnetCHARTING.Element("Element " + "r8");
        e8.Outline.Color = System.Drawing.Color.White;
        e8.YValue = Convert.ToDouble(10);
        ss6.Elements.Add(e8);



        SC.Add(ss6);


        /////// ninth series
        dotnetCHARTING.Series ss7 = new dotnetCHARTING.Series("Series " + "dd");

        if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) >= 0 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 11)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 10 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 21)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 20 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 31)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 30 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 41)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 40 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 51)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 50 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 61)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 60 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 71)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 70 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 81)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray, System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 80 && Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) < 91)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.LightGray };
        }
        else if (Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString()) > 90)
        {
            ss7.Palette = new System.Drawing.Color[] { System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255), System.Drawing.Color.FromArgb(204, 229, 255) };
        }

        dotnetCHARTING.Element e9 = new dotnetCHARTING.Element("Element " + "e1");
        //e9.Outline.Width = 0;
        e9.Outline.Color = System.Drawing.Color.White;
        //e9.YValue = Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString());
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);


        e9 = new dotnetCHARTING.Element("Element " + "r");
        //e9.Outline.Width = 0;
        e9.Outline.Color = System.Drawing.Color.White;
        //e9.YValue = 100 - Convert.ToDouble(ds1.Tables[0].Rows[0][1].ToString());
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r1");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r2");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r3");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r4");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r5");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r6");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r7");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        e9 = new dotnetCHARTING.Element("Element " + "r8");
        e9.Outline.Color = System.Drawing.Color.White;
        e9.YValue = Convert.ToDouble(10);
        ss7.Elements.Add(e9);

        SC.Add(ss7);





        /////// eightth series
        dotnetCHARTING.Series ss8 = new dotnetCHARTING.Series("Series " + "dd");
        ss8.Palette = new System.Drawing.Color[] { System.Drawing.Color.White, System.Drawing.Color.White };

        dotnetCHARTING.Element e10 = new dotnetCHARTING.Element("Element " + "e1");
        e10.YValue = 100;
        ss8.Elements.Add(e10);


        e10 = new dotnetCHARTING.Element("Element " + "r");
        e10.YValue = 0;
        ss8.Elements.Add(e10);

        SC.Add(ss8);

        //add all series to Chart1
        Chart1.SeriesCollection.Add(SC);
        Chart1.ImageFormat = dotnetCHARTING.ImageFormat.Jpg;

        string fileName = Chart1.FileManager.SaveImage();

        iTextSharp.text.Image abcharr = iTextSharp.text.Image.GetInstance(Server.MapPath(fileName));
        //jpeg.ScalePercent(35f);
        abcharr.ScaleToFit(550f, 500f);
        // jpeg.SpacingAfter = -50f;
        abcharr.SetAbsolutePosition(-100, 400);
        doc.Add(abcharr);


        iTextSharp.text.Image patch = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/patch.png"));
        patch.ScaleToFit(730f, 630f);
        patch.SetAbsolutePosition(-45, 400);
        doc.Add(patch);



        iTextSharp.text.Image chart1lable = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/chart2lable.png"));
        chart1lable.ScaleToFit(340f, 340f);
        chart1lable.SetAbsolutePosition(190, 520);
        doc.Add(chart1lable);



        iTextSharp.text.Table chartvalue = new iTextSharp.text.Table(2);
        chartvalue.Alignment = 0;
        chartvalue.Width = 190;
        // PDTopTable3.BackgroundColor = new Color(226, 226, 226);
        chartvalue.DefaultCellBorder = 0;
        chartvalue.Border = 0;





        Cell chartvaluecell1 = new Cell();




        iTextSharp.text.Table abilitycode = new iTextSharp.text.Table(1);
        abilitycode.Alignment = 0;
        abilitycode.Width = 98;
        abilitycode.DefaultCellBorder = 0;
        abilitycode.Border = 0;

        Cell abilitycod = new Cell();
        abilitycod.Add(new Paragraph("\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(77, 184, 219))));
        // abilitycode.AddCell(abilitycod);

        Cell abilitycode1 = new Cell();
        String chart2lblname = "";
        switch (ds1.Tables[0].Rows[0][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }

        abilitycode1.Add(new Paragraph(chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode1.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
        abilitycode.AddCell(abilitycode1);

        Cell abilitycode2 = new Cell();
        switch (ds1.Tables[0].Rows[1][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }
        abilitycode2.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode2.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
        abilitycode.AddCell(abilitycode2);

        Cell abilitycode3 = new Cell();
        switch (ds1.Tables[0].Rows[2][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }
        abilitycode3.Add(new Paragraph(chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode3.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
        abilitycode.AddCell(abilitycode3);

        Cell abilitycode4 = new Cell();
        switch (ds1.Tables[0].Rows[3][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }
        abilitycode4.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode4.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
        abilitycode.AddCell(abilitycode4);

        Cell abilitycode5 = new Cell();
        switch (ds1.Tables[0].Rows[4][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }
        abilitycode5.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode5.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
        abilitycode.AddCell(abilitycode5);

        Cell abilitycode6 = new Cell();
        switch (ds1.Tables[0].Rows[5][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }


        abilitycode6.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode6.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        abilitycode.AddCell(abilitycode6);

        Cell abilitycode7 = new Cell();
        switch (ds1.Tables[0].Rows[6][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }


        abilitycode7.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode7.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        abilitycode.AddCell(abilitycode7);


        Cell abilitycode8 = new Cell();
        switch (ds1.Tables[0].Rows[7][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }


        abilitycode8.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode8.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        abilitycode.AddCell(abilitycode8);


        Cell abilitycode9 = new Cell();
        switch (ds1.Tables[0].Rows[8][2].ToString())
        {
            case "1":
                chart2lblname = "Relationships";
                break;
            case "2":
                chart2lblname = "Emotional Stability";
                break;
            case "3":
                chart2lblname = "Assertiveness";
                break;
            case "4":
                chart2lblname = "Enthusiasm";
                break;
            case "5":
                chart2lblname = "Conscientious";
                break;
            case "6":
                chart2lblname = "Responsiveness";
                break;
            case "7":
                chart2lblname = "Tough Minded";
                break;
            case "8":
                chart2lblname = "Self Assurance";
                break;
            case "9":
                chart2lblname = "Relaxed";
                break;
        }
        abilitycode9.Add(new Paragraph("\n" + chart2lblname, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.COURIER, new Color(0, 0, 0))));
        abilitycode9.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode9.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        abilitycode.AddCell(abilitycode9);


        Cell abilitycode10 = new Cell();
        abilitycode10.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.COURIER, new Color(0, 0, 0))));
        abilitycode10.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
        abilitycode10.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
        abilitycode.AddCell(abilitycode10);


        chartvaluecell1.Add(abilitycode);

        chartvalue.AddCell(chartvaluecell1);


        Cell chartvaluecell2 = new Cell();

        iTextSharp.text.Table chartvaluee = new iTextSharp.text.Table(1);
        chartvaluee.Alignment = 0;
        chartvaluee.Width = 100;
        // PDTopTable3.BackgroundColor = new Color(226, 226, 226);
        chartvaluee.DefaultCellBorder = 0;
        chartvaluee.Border = 0;


        float chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[0][1].ToString());
        int intchvalue = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell1 = new Cell();
        chartvalueecell1.Add(new Paragraph("" + intchvalue.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell1);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[1][1].ToString());
        int intchvalue1 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell2 = new Cell();
        chartvalueecell2.Add(new Paragraph(intchvalue1.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell2);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[2][1].ToString());
        int intchvalue2 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell3 = new Cell();
        chartvalueecell3.Add(new Paragraph(intchvalue2.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell3);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[3][1].ToString());
        int intchvalue3 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell4 = new Cell();
        chartvalueecell4.Add(new Paragraph(intchvalue3.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell4);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[4][1].ToString());
        int intchvalue4 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell5 = new Cell();
        chartvalueecell5.Add(new Paragraph(intchvalue4.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell5);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[5][1].ToString());
        int intchvalue5 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell6 = new Cell();
        chartvalueecell6.Add(new Paragraph(intchvalue5.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell6);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[6][1].ToString());
        int intchvalue6 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell7 = new Cell();
        chartvalueecell7.Add(new Paragraph(intchvalue6.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell7);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[7][1].ToString());
        int intchvalue7 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell8 = new Cell();
        chartvalueecell8.Add(new Paragraph(intchvalue7.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell8);

        chvalue = (float)System.Convert.ToSingle(ds1.Tables[0].Rows[8][1].ToString());
        int intchvalue8 = (int)Math.Ceiling(chvalue);
        Cell chartvalueecell9 = new Cell();
        chartvalueecell9.Add(new Paragraph(intchvalue8.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(77, 184, 219))));
        chartvaluee.AddCell(chartvalueecell9);

        chartvaluecell2.Add(chartvaluee);
        chartvalue.AddCell(chartvaluecell2);
        doc.Add(chartvalue);


        iTextSharp.text.Image charthead = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/chart2head.png"));
        charthead.ScaleToFit(600f, 550f);
        charthead.SetAbsolutePosition(0, 810);
        doc.Add(charthead);

        iTextSharp.text.Image dischart = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/dischart2.png"));
        dischart.ScaleToFit(600f, 550f);
        dischart.SetAbsolutePosition(0, 343);
        doc.Add(dischart);

    }
 
}