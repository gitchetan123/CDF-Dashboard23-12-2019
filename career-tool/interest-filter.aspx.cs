using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class InterestFilter : System.Web.UI.Page
{


    db_context_career dbContext = new db_context_career();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        try
        {
            if (!IsPostBack)
            {
                filter.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            Interest();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            string strcmd = "", StrQuery = "";
            filter.Visible = true;
            #region Total Compatibility

            strcmd = "SELECT distinct ca_id,basic_info4 as Interest1, basic_info5 as Interest2,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') ";
            strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            Label1.Text = ds.Tables[0].Rows.Count.ToString();
            GridView1.DataSource = ds;
            GridView1.DataBind();

            #endregion

            #region Partially compatible - totally compatible
            if (ds.Tables[0].Rows.Count == 0)
            {
                strcmd = "SELECT distinct basic_info4 as Interest1, basic_info5 as Interest2,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )";
                strcmd += " AND ca_id not in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)";


            }
            #endregion

            #region Career CategoryCount AND Chart

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                StrQuery = " SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT Career_category AS CareerCategory, COUNT(Career_category) AS CategoryCount" +
                           " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                           " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                           " or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL  GROUP BY Career_category ) AS E";

                DataSet dsCareersum = dbContext.ExecDataSet(StrQuery);

                strcmd = "SELECT Career_category AS CareerCategory, COUNT(Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Career_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility " +
                            " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                            " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') AND Career_category IS NOT NULL GROUP BY Career_category ORDER BY CategoryCount DESC";
            }
            else
            {
                StrQuery = " SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT Career_category AS CareerCategory, " +
                         " COUNT(Career_category) AS CategoryCount FROM tbl_career_master where ca_id in  (SELECT distinct ca_id FROM tbl_career_master " +
                         " where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') OR ca_id in " +
                         " (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                         " or basic_info5 = '" + drop_interest2.SelectedValue + "') ) AND ca_id not in  (SELECT distinct ca_id " +
                         " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                         " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                         " or basic_info5 = '" + drop_interest2.SelectedValue + "' )) AND Career_category IS NOT NULL GROUP BY Career_category ) As E";

                DataSet dsCareersum = dbContext.ExecDataSet(StrQuery);

                strcmd = "SELECT Career_category AS CareerCategory, COUNT(Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Career_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_master where ca_id in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )";
                strcmd += " AND ca_id not in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)AND Career_category IS NOT NULL  GROUP BY Career_category ORDER BY CategoryCount DESC";
            }
            DataSet dsCareerCount = dbContext.ExecDataSet(strcmd);
            GridView4.DataSource = dsCareerCount;
            GridView4.DataBind();

            GridView4.Visible = true;
            Chart1.Visible = true;



            DataTable myDataTable = dsCareerCount.Tables[0];

            Chart1.DataSource = myDataTable;

            //// Set Doughnut chart type
            Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

            // Set labels style
            Chart1.Series[0]["PieLabelStyle"] = "Outside";

            //// Set Doughnut radius percentage
            //Chart1.Series[0]["DoughnutRadius"] = "30";


            //Chart's title
            System.Web.UI.DataVisualization.Charting.Title ctitle = new System.Web.UI.DataVisualization.Charting.Title();
            ctitle.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
            ctitle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            ctitle.ShadowOffset = 3;
            ctitle.Text = "Career Category Wise Compatibility";
            ctitle.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

            Chart1.Titles.Add(ctitle);

            //Chart1.Legends.Add("Legent1");
            //Chart1.Legends["Legent1"].Enabled = true;
            //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
            //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
            //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Enable 3D
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.ChartAreas[0].Area3DStyle.Inclination = 45;
            Chart1.ChartAreas[0].Area3DStyle.Rotation = 45;

            //Set the Y-axel as Category Count 
            Chart1.Series[0].YValueMembers = "Compatibility";

            //Set the X-axle as Category value 
            Chart1.Series[0].XValueMember = "CareerCategory";

            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisY.Interval = 1;

            ////set an angle to the label
            Chart1.Width = 300;

            Chart1.Height = 300;

            Chart1.Series[0].BorderWidth = 1;
            Chart1.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            Chart1.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

            //Bind the Chart control with the setting above 
            Chart1.DataBind();


            #endregion

            #region Occupational CategoryCount AND Chart


            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                StrQuery = " SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT Occupational_category AS OccupationalCategory, COUNT(Occupational_category) AS CategoryCount" +
                           " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                           " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                           " or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Occupational_category IS NOT NULL  GROUP BY Occupational_category ) AS E";

                DataSet dsCareersum = dbContext.ExecDataSet(StrQuery);

                strcmd = "SELECT Occupational_category AS OccupationalCategory, COUNT(Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Occupational_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility " +
                            " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                            " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') AND Occupational_category IS NOT NULL GROUP BY Occupational_category ORDER BY CategoryCount DESC";
            }
            else
            {
                StrQuery = " SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT Occupational_category AS OccupationalCategory, " +
                         " COUNT(Occupational_category) AS CategoryCount FROM tbl_career_master where ca_id in  (SELECT distinct ca_id FROM tbl_career_master " +
                         " where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') OR ca_id in " +
                         " (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                         " or basic_info5 = '" + drop_interest2.SelectedValue + "') ) AND ca_id not in  (SELECT distinct ca_id " +
                         " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                         " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' " +
                         " or basic_info5 = '" + drop_interest2.SelectedValue + "' )) AND Occupational_category IS NOT NULL GROUP BY Occupational_category ) As E";

                DataSet dsCareersum = dbContext.ExecDataSet(StrQuery);

                strcmd = "SELECT Occupational_category AS OccupationalCategory, COUNT(Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Occupational_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_master where ca_id in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )";
                strcmd += " AND ca_id not in ";
                strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	) AND Occupational_category IS NOT NULL GROUP BY Occupational_category ORDER BY CategoryCount DESC";
            }


            DataSet dsOccupationalCount = dbContext.ExecDataSet(strcmd);
            GridView5.DataSource = dsOccupationalCount;
            GridView5.DataBind();


            GridView5.Visible = true;
            Chart2.Visible = true;


            DataTable myOccupationTable = dsOccupationalCount.Tables[0];

            Chart2.DataSource = myOccupationTable;

            //// Set Doughnut chart type
            Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

            // Set labels style
            Chart2.Series[0]["PieLabelStyle"] = "Outside";

            //Chart's title
            System.Web.UI.DataVisualization.Charting.Title ctitle1 = new System.Web.UI.DataVisualization.Charting.Title();
            ctitle1.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
            ctitle1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            ctitle1.ShadowOffset = 3;
            ctitle1.Text = "Occupational Category Wise Compatibility";
            ctitle1.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

            Chart2.Titles.Add(ctitle1);

            //// Set Doughnut radius percentage
            //Chart1.Series[0]["DoughnutRadius"] = "30";

            //Chart1.Legends.Add("Legent1");
            //Chart1.Legends["Legent1"].Enabled = true;
            //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
            //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
            //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Enable 3D
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas[0].Area3DStyle.Inclination = 45;
            Chart2.ChartAreas[0].Area3DStyle.Rotation = 45;

            //Set the Y-axel as Category Count 
            Chart2.Series[0].YValueMembers = "Compatibility";

            //Set the X-axle as Category value 
            Chart2.Series[0].XValueMember = "OccupationalCategory";

            Chart2.ChartAreas[0].AxisX.Interval = 1;
            Chart2.ChartAreas[0].AxisY.Interval = 1;

            ////set an angle to the label
            Chart2.Width = 300;

            Chart2.Height = 300;

            Chart2.Series[0].BorderWidth = 1;
            Chart2.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            //// Chart Tooltip
            Chart2.Series[0].ToolTip = "Occupational Category: #VALX | Compatibility: #VALY";

            //Bind the Chart control with the setting above 
            Chart2.DataBind();

            #endregion

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }


    private void Interest()
    {
        try
        {
            string strcmd = "";
            strcmd = "SELECT distinct ca_id,basic_info4 as Interest1, basic_info5 as Interest2,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') ";
            strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            Label1.Text = ds.Tables[0].Rows.Count.ToString();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
}