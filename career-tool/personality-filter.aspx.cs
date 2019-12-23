using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Filter_PersonalityFilter : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    db_context_career dbContext = new db_context_career();
    
    String Strcmd = "";
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

                bind_drop_personality1();
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

    #region bind_dropdown_dynamically

    // new function by Dhananjay Korde on 24-11-2018 /  Reason: bind dropdown dynamically and maintain depadency.
    public void bind_drop_personality1()
    {
        string strcmd = "select distinct factor,  CASE factor WHEN 'Warm Hearted' THEN 'Warm Hearted(Relationships)' WHEN 'Controlled' THEN 'Controlled (Responsiveness)' ELSE factor END as factorName, factor_no from tbl_KY_factors ";
        DataSet ds2 = new DataSet();
        ds2 = dbContext.ExecDataSet(strcmd);
        drop_personality1.DataSource = ds2.Tables[0];
        drop_personality1.DataTextField = ds2.Tables[0].Columns[1].ToString();
        drop_personality1.DataValueField = ds2.Tables[0].Columns[0].ToString();
        drop_personality1.DataBind();
        drop_personality1.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        drop_personality1.SelectedIndex = 0;

        
    }
    public void bind_drop_personality2()
    {
        string strcmd = "select distinct factor,  CASE factor WHEN 'Warm Hearted' THEN 'Warm Hearted(Relationships)' WHEN 'Controlled' THEN 'Controlled (Responsiveness)' ELSE factor END as factorName, factor_no from tbl_KY_factors where factor not in ('" + drop_personality1.SelectedValue + "')";
        DataSet ds2 = new DataSet();
        ds2 = dbContext.ExecDataSet(strcmd);
        drop_personality2.DataSource = ds2.Tables[0];
        drop_personality2.DataTextField = ds2.Tables[0].Columns[1].ToString();
        drop_personality2.DataValueField = ds2.Tables[0].Columns[0].ToString();
        drop_personality2.DataBind();
        drop_personality2.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        drop_personality2.SelectedIndex = 0;
    }
    public void bind_drop_personality3()
    {
        string strcmd= "select distinct factor,  CASE factor WHEN 'Warm Hearted' THEN 'Warm Hearted(Relationships)' WHEN 'Controlled' THEN 'Controlled (Responsiveness)' ELSE factor END as factorName, factor_no from tbl_KY_factors where factor not in ('" + drop_personality1.SelectedValue + "','"+ drop_personality2.SelectedValue + "')";
        DataSet ds2 = new DataSet();
        ds2 = dbContext.ExecDataSet(strcmd);
        drop_personality3.DataSource = ds2.Tables[0];
        drop_personality3.DataTextField = ds2.Tables[0].Columns[1].ToString();
        drop_personality3.DataValueField = ds2.Tables[0].Columns[0].ToString();
        drop_personality3.DataBind();
        drop_personality3.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        drop_personality3.SelectedIndex = 0;
    }
    protected void drop_personality1_SelectedIndexChanged(object sender, EventArgs e)
    {
        drop_personality2.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        drop_personality2.SelectedIndex = 0;

        drop_personality3.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        drop_personality3.SelectedIndex = 0;

        bind_drop_personality2();
    }
    protected void drop_personality2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_drop_personality3();
    }
    #endregion bind_dropdown_dynamically


    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {

            filter.Visible = true;
            #region Total Compatibility
            // Query For Personality
            Strcmd = "";
            Strcmd = "SELECT distinct A.personality1 as Personality1,A.personality2 as Personality2,A.personality3 as Personality3, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                     " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                     " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                     " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')) " +
                     " AND B.Career_category IS NOT NULL";


            DataSet dspersonality = dbContext.ExecDataSet(Strcmd);
            if (dspersonality.Tables[0].Rows.Count > 0)
            {
                Label1.Text = dspersonality.Tables[0].Rows.Count.ToString(); //Career Category Wise Count
                GridView1.DataSource = dspersonality;
                GridView1.DataBind();
            }
            else
            {

                Strcmd = "";
                Strcmd = "SELECT  distinct Top 20  A.personality1 as Personality1, A.personality2 As Personality2, A.personality3 As Personality3,B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career  FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')) AND A.ca_id not in(  SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') AND ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  )";

                DataSet dspersonality2 = dbContext.ExecDataSet(Strcmd);
                Label1.Text = dspersonality2.Tables[0].Rows.Count.ToString();
                GridView1.DataSource = dspersonality2;
                GridView1.DataBind();
            }


            #endregion

            #region Career CategoryCount AND Chart

            DataSet dsCareerCount = new DataSet();

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(D.Career_category) AS CategoryCount " +
                        " FROM tbl_career_personality_master AS C INNER JOIN tbl_career_master As D    ON C.ca_id = D.ca_id " +
                        " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') " +
                        " AND C.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND C.ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                        " or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  GROUP BY D.Career_category ) As E ";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);
                // Query For Personality
                Strcmd = "";
                Strcmd = "SELECT B.Career_category As CareerCategory,COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')) " +
                         " AND B.Career_category IS NOT NULL GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GridView4.DataSource = dsCareerCount;
                GridView4.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                Chart1.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                Chart1.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                Chart1.Titles.Add(ctitlePersonality);

                //// Charts Legends 
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

                ////set Charts Height And Width
                Chart1.Width = 300;

                Chart1.Height = 300;

                // Set Charts Border Color and Width
                Chart1.Series[0].BorderWidth = 1;
                Chart1.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                Chart1.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                Chart1.DataBind();

                #endregion

            }
            else
            {
                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(B.Career_category) AS CategoryCount " +
                        " FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                        " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master " +
                        " WHERE (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  AND ca_id in (SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality3 = '" + drop_personality3.SelectedValue + "')  ) AND A.ca_id not in (  SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "'" +
                        " OR personality3 = '" + drop_personality1.SelectedValue + "')  AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " or personality3 = '" + drop_personality3.SelectedValue + "') )  ) GROUP BY B.Career_category ) As E";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);

                // Query For Personality
                Strcmd = "";
                Strcmd = "SELECT B.Career_category As CareerCategory,COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                         " AND B.Career_category IS NOT NULL GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GridView4.DataSource = dsCareerCount;
                GridView4.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                Chart1.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                Chart1.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                Chart1.Titles.Add(ctitlePersonality);

                //// Charts Legends 
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

                ////set Charts Height And Width
                Chart1.Width = 300;

                Chart1.Height = 300;

                // Set Charts Border Color and Width
                Chart1.Series[0].BorderWidth = 1;
                Chart1.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                Chart1.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                Chart1.DataBind();

                #endregion

            }

            #endregion

            #region Occupational CategoryCount AND Chart


            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(D.Occupational_category) AS CategoryCount " +
                   " FROM tbl_career_personality_master AS C INNER JOIN tbl_career_master As D    ON C.ca_id = D.ca_id " +
                   " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') " +
                   " AND C.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                   " or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND C.ca_id in " +
                   " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                   " or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  GROUP BY D.Occupational_category ) As E ";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);

                // Query For Personality
                Strcmd = "";
                Strcmd = "SELECT B.Occupational_category As OccupationalCategory,COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                            " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                            " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                            " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                            " AND B.Occupational_category IS NOT NULL GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GridView5.DataSource = dsCareerCount;
                GridView5.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                Chart2.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                Chart2.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                Chart2.Titles.Add(ctitlePersonality);

                //// Charts Legends 
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

                ////set Charts Height And Width
                Chart2.Width = 300;

                Chart2.Height = 300;

                // Set Charts Border Color and Width
                Chart2.Series[0].BorderWidth = 1;
                Chart2.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                Chart2.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                Chart2.DataBind();

                #endregion

            }
            else
            {
                // Query For Personality


                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(B.Occupational_category) AS CategoryCount " +
                        " FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                        " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master " +
                        " WHERE (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  AND ca_id in (SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality3 = '" + drop_personality3.SelectedValue + "')  ) AND A.ca_id not in (  SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "'" +
                        " OR personality3 = '" + drop_personality1.SelectedValue + "')  AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " or personality3 = '" + drop_personality3.SelectedValue + "') )  ) GROUP BY B.Occupational_category ) As E";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);
                Strcmd = "";
                Strcmd = "SELECT B.Occupational_category As OccupationalCategory,COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                         " AND B.Occupational_category IS NOT NULL GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GridView5.DataSource = dsCareerCount;
                GridView5.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                Chart2.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                Chart2.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                Chart2.Titles.Add(ctitlePersonality);

                //// Charts Legends 
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

                ////set Charts Height And Width
                Chart2.Width = 300;

                Chart2.Height = 300;

                // Set Charts Border Color and Width
                Chart2.Series[0].BorderWidth = 1;
                Chart2.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                Chart2.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                Chart2.DataBind();

                #endregion


            }


            #endregion

            #region Data of Suggested Careers


            string StrString = "";
            String strQuery = "";

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                //Query For picking up the Sum of Category 
                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(D.Career_category) AS CategoryCount " +
                           " FROM tbl_career_personality_master AS C INNER JOIN tbl_career_master As D    ON C.ca_id = D.ca_id " +
                           " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') " +
                           " AND C.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                           " or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND C.ca_id in " +
                           " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                           " or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  GROUP BY D.Career_category ) As E ";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);

                //Query For Showing Suggested Careers
                strQuery = "SELECT Distinct K.Occupational_category ,K.Career_category AS CareerCategory,K.basic_info1 As Career,L.education10 As Required_Degree " +
                          " FROM tbl_career_personality_master AS M INNER JOIN tbl_career_master As K ON M.ca_id = K.ca_id INNER JOIN tbl_career_education AS L ON M.ca_id =L.ca_id" +
                          " WHERE  Career_category In(SELECT  CareerCategory FROM( " +
                          " SELECT top 2 B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, " +
                          " CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage " +
                          " FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id " +
                          " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                          " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master " +
                          " where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' " +
                          " or personality3 = '" + drop_personality2.SelectedValue + "') AND A.ca_id in " +
                          " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                          " or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  " +
                          " GROUP BY B.Career_category order by Percentage desc)As K) AND (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')" +
                          " AND M.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master " +
                          " where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' " +
                          " or personality3 = '" + drop_personality2.SelectedValue + "')AND M.ca_id in " +
                          " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                          " or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))";
                Strcmd = strQuery + "Order by CareerCategory";

            }
            else
            {
                //Query For picking up the Sum of Category 
                Strcmd = "";
                Strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(B.Career_category) AS CategoryCount " +
                        " FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                        " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master " +
                        " WHERE (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' " +
                        " OR personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  AND ca_id in (SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " OR personality3 = '" + drop_personality3.SelectedValue + "')  ) AND A.ca_id not in (  SELECT [ca_id] " +
                        " FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "'" +
                        " OR personality3 = '" + drop_personality1.SelectedValue + "')  AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where " +
                        " (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') AND ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' " +
                        " or personality3 = '" + drop_personality3.SelectedValue + "') )  ) GROUP BY B.Career_category ) As E";
                DataSet dsCareersum = dbContext.ExecDataSet(Strcmd);

                //Query For Showing Suggested Careers
                strQuery = "SELECT Distinct K.Occupational_category ,K.Career_category AS CareerCategory,K.basic_info1 As Career,L.education10 As Required_Degree " +
                       " FROM tbl_career_personality_master AS M INNER JOIN tbl_career_master As K ON M.ca_id = K.ca_id " +
                       " INNER JOIN tbl_career_education AS L ON M.ca_id =L.ca_id WHERE  Career_category In(SELECT  CareerCategory FROM(SELECT top 3 B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, " +
                       " CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage " +
                       " FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id" +
                       " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master " +
                       " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') " +
                       " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  " +
                       " UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "') " +
                       " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') " +
                       " UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')) " +
                       " AND A.ca_id not in (  SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')" +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')" +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  ) " +
                       " GROUP BY B.Career_category ORDER BY CategoryCount DESC)As K)AND M.ca_id in ( SELECT distinct ca_id FROM tbl_career_personality_master " +
                       " where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                       " AND M.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  " +
                       " UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                       " AND M.ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')  " +
                       " UNION SELECT distinct ca_id FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')  " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "')) " +
                       " AND M.ca_id not in (  SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality1.SelectedValue + "' or personality2 = '" + drop_personality1.SelectedValue + "' or personality3 = '" + drop_personality1.SelectedValue + "')  " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "') " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_personality_master where personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )  ) ";

                Strcmd = strQuery + "Order by CareerCategory";


            }

            DataSet dssug = dbContext.ExecDataSet(Strcmd);
            GridView6.DataSource = dssug;

            GridView6.DataBind();
            GenerateUniqueData(0);
            GenerateUniqueData(1);

            if (GridView6.Columns.Count > 0)
                GridView6.Columns[3].Visible = false;
            else
            {
                GridView6.HeaderRow.Cells[3].Visible = false;
                foreach (GridViewRow gvr in GridView6.Rows)
                {
                    gvr.Cells[3].Visible = false;
                }
            }

            //Query For Required Degree for the Suggested Careers

            StrString = "SELECT Distinct Required_Degree FROM (" + strQuery + ") AS H WHERE Required_Degree <> 'NULL' AND Required_Degree Not Like 'any%'  Order By Required_Degree";

            DataSet DsDegree = dbContext.ExecDataSet(StrString);
            GridView7.DataSource = DsDegree;
            GridView7.DataBind();

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
    private void GenerateUniqueData(int cellno)
    {
        try
        {
            string initialnamevalue = GridView6.Rows[0].Cells[cellno].Text;

            for (int i = 1; i < GridView6.Rows.Count; i++)
            {

                if (GridView6.Rows[i].Cells[cellno].Text == initialnamevalue)
                    GridView6.Rows[i].Cells[cellno].Text = string.Empty;
                else
                    initialnamevalue = GridView6.Rows[i].Cells[cellno].Text;
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
}