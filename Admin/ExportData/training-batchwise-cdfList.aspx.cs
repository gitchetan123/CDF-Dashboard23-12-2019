using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_training_batchwise_cdfList : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    int count;
    string file_name = null;
    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string StrQuery4 = "select id,cdfLevel from tblCDFLevel";
            dbContext.BindDropDownlist(StrQuery4, ref ddlLevel);
        }
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StrQuery3 = "Select id, batchName From tblTrainingBatch where cdfLevel='" + ddlLevel.SelectedValue + "' order by id desc";
        dbContext.BindDropDownlist(StrQuery3, ref ddlBatchName);
    }
    protected void btn_preview_click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        div_batch.Visible = true;

        string strcmd = "Select id, batchName,location + ' ' + details as 'Address',date,trainerName From tblTrainingBatch where id = " + ddlBatchName.SelectedValue.ToString();
        DataSet ds = dbContext.ExecDataSet(strcmd);
        lblBatchName.Text = ds.Tables[0].Rows[0]["batchName"].ToString();
        lblBatchLoc.Text = ds.Tables[0].Rows[0]["Address"].ToString();

        lblBatchdate.Text = ds.Tables[0].Rows[0]["date"].ToString();
        lblTrainerName.Text = ds.Tables[0].Rows[0]["trainerName"].ToString();

        //bind CDF Code 
        try
        {
            BindGrid_Export();
            //BindGrid();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    private void BindGrid()
    {
        file_name = "batchwise_cdf ";
        // bellow query has added stored procedure as "sp_batchwise_cdf_details"

        string str = " SELECT CONCAT(fname, '  ', lname) AS Name, A.email,dheyaEmail,contactNo,C.name as city ,A.cdfLevel as 'Current Level', A.regDateTime, ";

        if (ddlLevel.Text == "1")
        {
            str += "B.batchId as 'L1 Batch Id', L1.batchName as 'L1 BatchName',  L1.location as 'L1 Location', L1.date as 'L1 Date', L1.details as 'L1 Details', L1.trainerName as 'L1 Trainer', L1.cdfcount as 'L1 Count', ";
        }
        if (ddlLevel.Text == "2")
        {
            str += " B.batchId_L2 as 'L2 Batch Id', L2.batchName as 'L2 BatchName',  L2.location as 'L2 Location', L2.date as 'L2 Date', L2.details as 'L2 Details', L2.trainerName as 'L2 Trainer', L2.cdfcount as 'L2 Count', ";
        }
        if (ddlLevel.Text == "3")
        {
            str += " B.batchId_L3 as 'L3 Batch Id', L3.batchName as 'L3 BatchName',  L3.location as 'L3 Location', L3.date as 'L3 Date', L3.details as 'L3 Details', L3.trainerName as 'L3 Trainer', L3.cdfcount as 'L3 Count' ";
        }

        str += " ''  FROM "
                    + " (select u.uId, ISNULL(SUM(amount), 0) as TotalPayment from tblPayment as p "
                    + " Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource = 'DHEYA-CDF'  group by u.uId, u.userTypeId having u.userTypeId = '2') AS s "
                    + " Left Outer Join(select * from tblUserMaster  where userSource = 'DHEYA-CDF' and userTypeId = '2') AS A on A.uId = s.uId "
                    + " LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId "
                    + " join tblTrainingBatch L1 ON  B.batchId = L1.id "
                    + " Left join tblTrainingBatch L2 ON B.batchId_L2 = L2.id "
                    + " left join tblTrainingBatch L3 ON B.batchId_L3 = L3.id "
                    + " LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id "
                    + " LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId "
                    + " LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 "
                    + " left outer join(select vr.executiveId, e.exeName, vr.email from tblVerifyRegistration as vr join tblExecutive as e on vr.executiveId = e.id) as vf on vf.email = A.email "
                    + " where cdfApproved = 'APPROVED'  and userTypeId = '2'  and L1.batchName = '" + ddlBatchName.SelectedItem.Text + "' "
                    + " group by A.cdfLevel,dheyaEmail,contactNo,A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment,B.shadowSession,B.childTestStatus,B.childSessionStatus,  B.spouseTestStatus,  A.address,B.fieldOfWork,B.modeOfWork,B.industrySector ,A.email, vf.executiveId,vf.exeName, vf.email, "
                    + " B.batchId,    L1.batchName, L1.location, L1.date, L1.details, L1.trainerName, L1.cdfcount, "
                    + " B.batchId_L2, L2.batchName, L2.location, L2.date, L2.details, L2.trainerName, L2.cdfcount,  "
                    + " B.batchId_L3, L3.batchName, L3.location, L3.date, L3.details, L3.trainerName, L3.cdfcount "
                    + " order by A.uId desc ";

        file_name += "batchname=" + ddlBatchName.SelectedItem.Text + "-L" + ddlLevel.SelectedItem.Text;
        ViewState["FileName"] = file_name;
        ViewState["STRING"] = str;
        
        DataSet ds2 = dbContext.ExecDataSet(str);
        GridView1.DataSource = ds2;
        count = ds2.Tables[0].Rows.Count;
        GridView1.DataBind();

        //string strConnString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(strConnString))
        //{
        //    using (SqlCommand cmd = new SqlCommand(str))
        //    {
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {
        //                sda.Fill(dt);
        //                GridView1.DataSource = dt;
        //                GridView1.DataBind();
        //                count = dt.Rows.Count;
        //                //lbl_rowcount.Text = "Total : "+count.ToString();

        //            }
        //        }
        //    }
        //}
        if (count == 0)
        {
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-warning";
            div_msg.InnerText = "No Record Found...";
            btnExport.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            btnExportToExcel.Visible = true;
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    
    
    

    private void BindGrid_Export()
    {
        file_name = "batchwise_cdf ";
        // bellow query has added stored procedure as "sp_batchwise_cdf_details"

        string str = " SELECT CONCAT(fname, '  ', lname) AS Name, A.email,dheyaEmail,contactNo,C.name as city ,A.cdfLevel as 'Current Level', A.regDateTime, ";

        if (chk_all.Checked == true)
        {
            str += " B.batchId as 'L1 Batch Id', L1.batchName as 'L1 BatchName',  L1.location as 'L1 Location', L1.date as 'L1 Date', L1.details as 'L1 Details', L1.trainerName as 'L1 Trainer', L1.cdfcount as 'L1 Count', "
                    + " B.batchId_L2 as 'L2 Batch Id', L2.batchName as 'L2 BatchName',  L2.location as 'L2 Location', L2.date as 'L2 Date', L2.details as 'L2 Details', L2.trainerName as 'L2 Trainer', L2.cdfcount as 'L2 Count', "
                    + " B.batchId_L3 as 'L3 Batch Id', L3.batchName as 'L3 BatchName',  L3.location as 'L3 Location', L3.date as 'L3 Date', L3.details as 'L3 Details', L3.trainerName as 'L3 Trainer', L3.cdfcount as 'L3 Count', ";
        }
        if (ddlLevel.Text == "1" && chk_all.Checked == false)
        {
            str += "B.batchId as 'L1 Batch Id', L1.batchName as 'L1 BatchName',  L1.location as 'L1 Location', L1.date as 'L1 Date', L1.details as 'L1 Details', L1.trainerName as 'L1 Trainer', L1.cdfcount as 'L1 Count', ";
        }
        if (ddlLevel.Text == "2" && chk_all.Checked == false)
        {
            str += " B.batchId_L2 as 'L2 Batch Id', L2.batchName as 'L2 BatchName',  L2.location as 'L2 Location', L2.date as 'L2 Date', L2.details as 'L2 Details', L2.trainerName as 'L2 Trainer', L2.cdfcount as 'L2 Count', ";
        }
        if (ddlLevel.Text == "3" && chk_all.Checked == false)
        {
            str += " B.batchId_L3 as 'L3 Batch Id', L3.batchName as 'L3 BatchName',  L3.location as 'L3 Location', L3.date as 'L3 Date', L3.details as 'L3 Details', L3.trainerName as 'L3 Trainer', L3.cdfcount as 'L3 Count' ";
        }

        str += " ''  FROM "
                    + " (select u.uId, ISNULL(SUM(amount), 0) as TotalPayment from tblPayment as p "
                    + " Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource = 'DHEYA-CDF'  group by u.uId, u.userTypeId having u.userTypeId = '2') AS s "
                    + " Left Outer Join(select * from tblUserMaster  where userSource = 'DHEYA-CDF' and userTypeId = '2') AS A on A.uId = s.uId "
                    + " LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId "
                    + " join tblTrainingBatch L1 ON  B.batchId = L1.id "
                    + " Left join tblTrainingBatch L2 ON B.batchId_L2 = L2.id "
                    + " left join tblTrainingBatch L3 ON B.batchId_L3 = L3.id "
                    + " LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id "
                    + " LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId "
                    + " LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 "
                    + " left outer join(select vr.executiveId, e.exeName, vr.email from tblVerifyRegistration as vr join tblExecutive as e on vr.executiveId = e.id) as vf on vf.email = A.email "
                    + " where cdfApproved = 'APPROVED'  and userTypeId = '2'  and L1.batchName = '" + ddlBatchName.SelectedItem.Text + "' "
                    + " group by A.cdfLevel,dheyaEmail,contactNo,A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment,B.shadowSession,B.childTestStatus,B.childSessionStatus,  B.spouseTestStatus,  A.address,B.fieldOfWork,B.modeOfWork,B.industrySector ,A.email, vf.executiveId,vf.exeName, vf.email, "
                    + " B.batchId,    L1.batchName, L1.location, L1.date, L1.details, L1.trainerName, L1.cdfcount, "
                    + " B.batchId_L2, L2.batchName, L2.location, L2.date, L2.details, L2.trainerName, L2.cdfcount,  "
                    + " B.batchId_L3, L3.batchName, L3.location, L3.date, L3.details, L3.trainerName, L3.cdfcount "
                    + " order by A.uId desc ";


        //string str = "SELECT tb.batchName as 'Batch Name',tb.location,tb.date,tb.details,tb.trainerName as 'Trainer Name',tb.cdfcount, vf.exeName as 'Executive Name', CONCAT(fname, '  ', lname) AS 'CDF Name', A.email, A.contactno, dob,A.regDateTime,C.name as city,a.status,userStatus,  "
        //            + " ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment, ISNULL(B.shadowSession, '0'), "
        //            + " B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,A.address,B.fieldOfWork,B.modeOfWork, "
        //            + " B.industrySector FROM(select u.uId, ISNULL(SUM(amount), 0) as TotalPayment from tblPayment as p "

        //            + " Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource = 'DHEYA-CDF' group by u.uId, u.userTypeId having u.userTypeId = '2') AS s "
        //            + " Left Outer Join(select * from tblUserMaster where userSource = 'DHEYA-CDF' and userTypeId = '2') AS A on A.uId = s.uId "
        //            + " LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId "
        //            + " right outer join tblTrainingBatch as tb on tb.id = B.batchId and tb.batchName='" + ddlBatchName.SelectedItem.Text + "' "
        //            + " LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id "
        //            + " LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId "
        //            + " LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 "
        //            + " left outer join(select vr.executiveId, e.exeName, vr.email from tblVerifyRegistration as vr join tblExecutive as e on vr.executiveId = e.id) as vf on vf.email = A.email "

        //            + " where cdfApproved = 'APPROVED' and userTypeId = '2' "

        //            + " group by A.contactno,A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment,B.shadowSession,B.childTestStatus,B.childSessionStatus,B.spouseTestStatus, A.address,B.fieldOfWork,B.modeOfWork,B.industrySector ,A.email, vf.executiveId,vf.exeName, vf.email,  tb.batchName,tb.location,tb.date,tb.details,tb.trainerName,tb.cdfcount "
        //            + " order by A.uId desc ";

        file_name += "batchname=" + ddlBatchName.SelectedItem.Text + "-L" + ddlLevel.SelectedItem.Text;
        ViewState["FileName"] = file_name;
        ViewState["STRING"] = str;
        //DataSet ds2 = dbContext.ExecDataSet(str);
        //GridView1.DataSource = ds2;
        //GridView1.DataBind();

        string strConnString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(str))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        count = dt.Rows.Count;
                        //lbl_rowcount.Text = "Total : " + count.ToString();
                    }
                }
            }
        }
        if (count == 0)
        {
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-warning";
            div_msg.InnerText = "No Record Found...";
            btnExport.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            btnExportToExcel.Visible = true;
        }
    }

    protected void ExportToPDF(object sender, EventArgs e)
    {
        exportToPDFdoc();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        exp_to_excel();
    }

    private void exp_to_excel()
    {
        try
        {
            BindGrid_Export();
            //BindGrid();
            file_name = ViewState["FileName"].ToString();
            string strcmd = ViewState["STRING"].ToString();
            DataSet ds = dbContext.ExecDataSet(strcmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //Create a dummy GridView
                GridView G1 = new GridView();
                G1.AllowPaging = false;
                G1.DataSource = ds.Tables[0];
                G1.DataBind();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                 "attachment;filename=" + file_name + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                for (int i = 0; i < G1.Rows.Count; i++)
                {
                    //Apply text style to each Row
                    G1.Rows[i].Attributes.Add("class", "textmode");
                }
                G1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Save_export_data_tracking();
                Response.Flush();
                Response.End();
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerHtml = "No data found for export. Please try again......";
            }
        }

        catch (System.Threading.ThreadAbortException)
        { }

        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }
    private void Save_export_data_tracking()
    {
        try
        {
            string s_query = "sp_export_data_tracking";
            int createdBy = Convert.ToInt32(Session["adminuser_id"]);
            int i = dbContext.ExecNonQuery(s_query, file_name, count, createdBy);

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private void exportToPDFdoc()
    {
        Save_export_data_tracking();
        string fileName = "Batchwise_CDF_Details.pdf";
        HttpResponse Response = HttpContext.Current.Response;
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
        Document doc = new Document(PageSize.A4, 20, 30, 15, 20);
        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath(".") + "/Reports_pdf/Batchwise_CDF_Details.pdf", FileMode.Create));

        PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        doc.AddTitle(fileName);

        doc.Open();
        try
        {
            doc.NewPage();
            iTextSharp.text.Image dheyalogo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/LOGO-NEW.png"));
            dheyalogo.ScalePercent(35f);
            dheyalogo.ScaleToFit(50f, 50f);
            dheyalogo.SetAbsolutePosition(30, 25);
            // dheyalogo.SpacingAfter = -50f;
            doc.Add(dheyalogo);

            HeaderFooter footer1 = new HeaderFooter(new Phrase("                " + "                                                                                        " + "                                                                                          " + "PAGE: ", FontFactory.GetFont(FontFactory.HELVETICA, 8)), true);
            footer1.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            footer1.BorderColor = new Color(255, 255, 255);
            footer1.BorderWidth = 0f;
            doc.Footer = footer1;
            Paragraph p5 = new Paragraph("           \n");
            doc.Add(p5);
            doc.Add(p5);

            Paragraph p11 = new Paragraph();
            iTextSharp.text.Image jpeg16 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/logo.png"));
            jpeg16.ScalePercent(35f);
            jpeg16.ScaleToFit(200f, 200f);
            jpeg16.SetAbsolutePosition(200, 700);
            jpeg16.SpacingAfter = -50f;
            // p11.Add(new Chunk(jpeg16, 0, 0));
            p11.Add(new Paragraph("Training Batchwise CDF Details", FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, new Color(00, 00, 00))));
            p11.Alignment = Element.ALIGN_CENTER;
            doc.Add(p11);

            PdfPTable pdfhead = new PdfPTable(2);
            pdfhead.WidthPercentage = 100; // percentage
            // pdfhead.DefaultCell.Padding = 1;
            //pdfhead.DefaultCell.BorderWidth = -1;
            //pdfhead.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cell1 = new PdfPCell();
            Paragraph p1 = new Paragraph("\n Batch Name : " + lblBatchName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(00, 00, 00)));
            p1.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell1.AddElement(p1);
            cell1.BorderWidth = 0;
            cell1.PaddingTop = -2;
            //cell1.setPaddingTop(-3);
            cell1.VerticalAlignment = Element.ALIGN_TOP;
            pdfhead.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell();
            Paragraph p2 = new Paragraph("\n Batch Location : " + lblBatchLoc.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(00, 00, 00)));
            p2.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell2.AddElement(p2);
            cell2.BorderWidth = 0;
            cell2.PaddingTop = -2;
            cell2.VerticalAlignment = Element.ALIGN_TOP;
            pdfhead.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell();
            DateTime batchDate = Convert.ToDateTime(lblBatchdate.Text);
            Paragraph p3 = new Paragraph("\n Batch Date   : " + batchDate.ToString("dd-MMM-yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(00, 00, 00)));
            p3.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell3.AddElement(p3);
            cell3.BorderWidth = 0;
            cell3.PaddingTop = -2;
            cell3.VerticalAlignment = Element.ALIGN_TOP;
            pdfhead.AddCell(cell3);

            PdfPCell cell4 = new PdfPCell();
            Paragraph p4 = new Paragraph("\n Batch Trainer    : " + lblTrainerName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(00, 00, 00)));
            p4.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell4.AddElement(p4);
            cell4.BorderWidth = 0;
            cell4.PaddingTop = -2;
            cell4.VerticalAlignment = Element.ALIGN_TOP;
            pdfhead.AddCell(cell4);

            doc.Add(pdfhead);

            doc.Add(p5);
            //doc.Add(p5);

            //doc.Add(FormatPageHeaderPhrase("CDF LIST"));
            PdfPTable pdfTable = new PdfPTable(GridView1.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100; // percentage
            pdfTable.DefaultCell.BorderWidth = 2;
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                string ColumnName = "";
                if (i == 0)
                { ColumnName = "Sr. No."; }
                else { ColumnName = GridView1.Columns[i].HeaderText; }
                PdfPCell headerCell = new PdfPCell(new Paragraph(ColumnName, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(255, 255, 255))));
                headerCell.BackgroundColor = new Color(93, 123, 157);
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable.AddCell(headerCell);
            }

            pdfTable.HeaderRows = 1;  // this is the end of the table header
            pdfTable.DefaultCell.BorderWidth = 1;

            int rowNo = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                rowNo = rowNo + 1;
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    String cellText;
                    //String header = GridView1.Columns[i].HeaderText;
                    if (i == 0)
                    {
                        cellText = rowNo.ToString();
                    }
                    else { cellText = row.Cells[i].Text; }

                    PdfPCell rowCell = new PdfPCell(new Paragraph(cellText, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD, new Color(38, 38, 38))));

                    if (rowNo == 0 || rowNo % 2 == 0)
                    { rowCell.BackgroundColor = new Color(247, 246, 243); }
                    else { rowCell.BackgroundColor = new Color(225, 225, 225); }
                    rowCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    rowCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pdfTable.AddCell(rowCell);
                }
            }
            doc.Add(pdfTable);
            
            doc.Close();

            // DownloadFile("/Reports_pdf/Batchwise_CDF_Details.pdf", true);
        }
        catch (System.Threading.ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong. Please try again.";
            Log.Error("" + ex);
            doc.Close();
        }
    }



    





















































    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
    //       server control at run time. */
    //}

    //private void exp_to_excel()
    //{
    //    string strcmd = ViewState["STRING"].ToString();

    //    DataSet ds = dbContext.ExecDataSet(strcmd);

    //    Response.ClearContent();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Batchwise-CDFList.xls"));
    //    Response.ContentType = "application/ms-excel";
    //    DataTable dt = ds.Tables[0];
    //    string str = string.Empty;
    //    foreach (DataColumn dtcol in dt.Columns)
    //    {
    //        Response.Write(str + dtcol.ColumnName);
    //        str = "\t";
    //    }
    //    Response.Write("\n");
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        str = "";
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            Response.Write(str + Convert.ToString(dr[j]));
    //            str = "\t";
    //        }
    //        Response.Write("\n");
    //    }
    //    Response.End();
    //}


}
