using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class AllFilter : System.Web.UI.Page
{
    db_context_career dbContext = new db_context_career();
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //String StrQuery = "",
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
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            filter.Visible = true;
            #region Total Compatibility

            // Query For Ability 

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                     " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                     " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND " +
                     " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                     " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL ";

            DataSet dsAbility = dbContext.ExecDataSet(Strcmd);
            HFAbility.Value = dsAbility.Tables[0].Rows.Count.ToString();
            GDVAbility.DataSource = dsAbility;
            GDVAbility.DataBind();
            dsAbility.Tables.Clear();
            dsAbility.Dispose();

            // Query For Interest

            Strcmd = "SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                     "  WHERE (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  " +
                     " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL";

            dsAbility = dbContext.ExecDataSet(Strcmd);
            HFInterest.Value = dsAbility.Tables[0].Rows.Count.ToString();
            GDVInterest.DataSource = dsAbility;
            GDVInterest.DataBind();
            dsAbility.Tables.Clear();
            dsAbility.Dispose();

            // Query For Personality

            Strcmd = "SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                     " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                     " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                     " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                     " AND B.Career_category IS NOT NULL";


            dsAbility = dbContext.ExecDataSet(Strcmd);
            HFPersonality.Value = dsAbility.Tables[0].Rows.Count.ToString();
            GDVPersonality.DataSource = dsAbility;
            GDVPersonality.DataBind();
            dsAbility.Tables.Clear();
            dsAbility.Dispose();

            //Query For Combined OF ABILITY Interest And Personality

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                   " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                   " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                   " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND " +
                   " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                   " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL " +
                   " UNION  " +
                   " (SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                   "  WHERE (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  " +
                   " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL)" +
                   " UNION " +
                   " (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                   " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                   " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                   " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                   " AND B.Career_category IS NOT NULL) order by B.Career_category";

            dsAbility = dbContext.ExecDataSet(Strcmd);
            Label1.Text = dsAbility.Tables[0].Rows.Count.ToString();
            GDVCombined.DataSource = dsAbility;
            GDVCombined.DataBind();
            dsAbility.Tables.Clear();
            dsAbility.Dispose();

            GDVAbility.Visible = true;
            GDVInterest.Visible = true;
            GDVPersonality.Visible = true;
            GDVCombined.Visible = true;

            #endregion

            #region Partially compatible - totally compatible

            //Query For Ability

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                     " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                     " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                     " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL" +
                     " UNION " +
                     " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                     " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL" +
                     " UNION " +
                     " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL) " +
                     " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')" +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                     " AND B.Career_category IS NOT NULL)";

            DataSet dsAbilityPC = dbContext.ExecDataSet(Strcmd);
            HFAbilityP.Value = dsAbilityPC.Tables[0].Rows.Count.ToString();
            GDVAbilityPC.DataSource = dsAbilityPC;
            GDVAbilityPC.DataBind();
            dsAbilityPC.Tables.Clear();
            dsAbilityPC.Dispose();

            //Query For Interest

            Strcmd = " SELECT distinct ca_id As CareerID ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                     " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                     " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )" +
                     " AND ca_id not in " +
                     " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                     " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)" +
                     " AND Career_category IS NOT NULL";


            dsAbilityPC = dbContext.ExecDataSet(Strcmd);
            HFInterestP.Value = dsAbilityPC.Tables[0].Rows.Count.ToString();
            GDVInterestPC.DataSource = dsAbilityPC;
            GDVInterestPC.DataBind();
            dsAbilityPC.Tables.Clear();
            dsAbilityPC.Dispose();

            //Query For Personality

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                     " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                     " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                     " AND A.ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                     " AND B.Career_category IS NOT NULL";

            dsAbilityPC = dbContext.ExecDataSet(Strcmd);
            HFPersonalityP.Value = dsAbilityPC.Tables[0].Rows.Count.ToString();
            GDVPersonalityPC.DataSource = dsAbilityPC;
            GDVPersonalityPC.DataBind();
            dsAbilityPC.Tables.Clear();
            dsAbilityPC.Dispose();


            //Query For Combined OF ABILITY Interest And Personality

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                   " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                   " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                   " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL" +
                   " UNION " +
                   " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                   " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL" +
                   " UNION " +
                   " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL) " +
                   " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')" +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                   " AND B.Career_category IS NOT NULL)" +
                   " UNION " +
                   "(SELECT distinct ca_id ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                   " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                   " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )" +
                   " AND ca_id not in " +
                   " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                   " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)" +
                   " AND Career_category IS NOT NULL)" +
                   " UNION " +
                   "(SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                   " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                   " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                   " AND A.ca_id in " +
                   " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                   " AND B.Career_category IS NOT NULL)";

            DataSet ds2 = dbContext.ExecDataSet(Strcmd);
            Label2.Text = ds2.Tables[0].Rows.Count.ToString();
            GDVCombinedPC.DataSource = ds2;
            GDVCombinedPC.DataBind();


            DataSet ds3 = dsAbility;
            ds3.Merge(ds2, true, MissingSchemaAction.Add);
            ds3.AcceptChanges();


            #endregion

            #region Career CategoryCount AND Chart

            DataSet dsCareerCount = new DataSet();

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {

                // Query For Ability 

                Strcmd = "";
                Strcmd = " SELECT B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + HFAbility.Value + ")*100 as Compatibility FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  " +
                         "  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')" +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                         " GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVAbilityCC.DataSource = dsCareerCount;
                GDVAbilityCC.DataBind();

                #region Ability Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTable = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHAbilityCC.DataSource = myDataTable;

                //// Set Doughnut chart type
                CHAbilityCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHAbilityCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitle = new System.Web.UI.DataVisualization.Charting.Title();
                ctitle.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitle.ShadowOffset = 3;
                ctitle.Text = "Career Category Wise Compatibility- Of Ability";
                ctitle.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHAbilityCC.Titles.Add(ctitle);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHAbilityCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHAbilityCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHAbilityCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHAbilityCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHAbilityCC.Series[0].XValueMember = "CareerCategory";

                CHAbilityCC.ChartAreas[0].AxisX.Interval = 1;
                CHAbilityCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHAbilityCC.Width = 300;

                CHAbilityCC.Height = 300;

                // Set Charts Border Color and Width
                CHAbilityCC.Series[0].BorderWidth = 1;
                CHAbilityCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHAbilityCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHAbilityCC.DataBind();

                #endregion

                // Query For Interst 

                Strcmd = "SELECT Career_category AS CareerCategory, COUNT(Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Career_category))/" + HFInterest.Value + ")*100 as Compatibility " +
                          " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                          " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') AND Career_category IS NOT NULL GROUP BY Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVInterestCC.DataSource = dsCareerCount;
                GDVInterestCC.DataBind();


                #region Interest Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTableInt = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHInterestCC.DataSource = myDataTableInt;

                //// Set Doughnut chart type
                CHInterestCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHInterestCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitleInt = new System.Web.UI.DataVisualization.Charting.Title();
                ctitleInt.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitleInt.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitleInt.ShadowOffset = 3;
                ctitleInt.Text = "Career Category Wise Compatibility- Of Interest";
                ctitleInt.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHInterestCC.Titles.Add(ctitleInt);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHInterestCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHInterestCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHInterestCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHInterestCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHInterestCC.Series[0].XValueMember = "CareerCategory";

                CHInterestCC.ChartAreas[0].AxisX.Interval = 1;
                CHInterestCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHInterestCC.Width = 300;

                CHInterestCC.Height = 300;

                // Set Charts Border Color and Width
                CHInterestCC.Series[0].BorderWidth = 1;
                CHInterestCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHInterestCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHInterestCC.DataBind();

                #endregion

                // Query For Personality

                Strcmd = "SELECT B.Career_category As CareerCategory,COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + HFPersonality.Value + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                         " AND B.Career_category IS NOT NULL GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVPersonalityCC.DataSource = dsCareerCount;
                GDVPersonalityCC.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHPersonalityCC.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                CHPersonalityCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHPersonalityCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHPersonalityCC.Titles.Add(ctitlePersonality);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHPersonalityCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHPersonalityCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHPersonalityCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHPersonalityCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHPersonalityCC.Series[0].XValueMember = "CareerCategory";

                CHPersonalityCC.ChartAreas[0].AxisX.Interval = 1;
                CHPersonalityCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHPersonalityCC.Width = 300;

                CHPersonalityCC.Height = 300;

                // Set Charts Border Color and Width
                CHPersonalityCC.Series[0].BorderWidth = 1;
                CHPersonalityCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHPersonalityCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHPersonalityCC.DataBind();

                #endregion

                //Query For Combined OF ABILITY Interest And Personality

                Strcmd = " SELECT CareerCategory,COUNT(CareerCategory) AS CategoryCount,CONVERT(decimal(18, 2), CONVERT(decimal, COUNT(CareerCategory)) /" + Label1.Text + ")*100 AS Compatibility " +
                     " FROM (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                     " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                     " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where  (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "'  or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "'  or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL " +
                     " UNION  (SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')   " +
                     " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL) " +
                     " UNION (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                     " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "')" +
                     " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                     " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                     " AND B.Career_category IS NOT NULL))AS G GROUP BY CareerCategory ORDER BY CategoryCount DESC";

            }
            else
            {

                // Query For Ability

                Strcmd = "";
                Strcmd = "SELECT B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/" + HFAbilityP.Value + ")*100 as Compatibility FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                Strcmd += " UNION ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                Strcmd += " UNION ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                Strcmd += " ) AND A.ca_id not in ( ";
                Strcmd += " SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                Strcmd += " ) GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVAbilityCC.DataSource = dsCareerCount;
                GDVAbilityCC.DataBind();

                #region Ability Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTable = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHAbilityCC.DataSource = myDataTable;

                //// Set Doughnut chart type
                CHAbilityCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHAbilityCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitle = new System.Web.UI.DataVisualization.Charting.Title();
                ctitle.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitle.ShadowOffset = 3;
                ctitle.Text = "Career Category Wise Compatibility- Of Ability";
                ctitle.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHAbilityCC.Titles.Add(ctitle);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHAbilityCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHAbilityCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHAbilityCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHAbilityCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHAbilityCC.Series[0].XValueMember = "CareerCategory";

                CHAbilityCC.ChartAreas[0].AxisX.Interval = 1;
                CHAbilityCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHAbilityCC.Width = 300;

                CHAbilityCC.Height = 300;

                // Set Charts Border Color and Width
                CHAbilityCC.Series[0].BorderWidth = 1;
                CHAbilityCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHAbilityCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHAbilityCC.DataBind();

                #endregion

                //Query For Interest

                Strcmd = "SELECT Career_category AS CareerCategory, COUNT(Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Career_category))/" + HFInterestP.Value + ")*100 as Compatibility FROM tbl_career_master where ca_id in ";
                Strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                Strcmd += " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )";
                Strcmd += " AND ca_id not in ";
                Strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                Strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)AND Career_category IS NOT NULL  GROUP BY Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVInterestCC.DataSource = dsCareerCount;
                GDVInterestCC.DataBind();

                #region Interest Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTableInt = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHInterestCC.DataSource = myDataTableInt;

                //// Set Doughnut chart type
                CHInterestCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHInterestCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitleInt = new System.Web.UI.DataVisualization.Charting.Title();
                ctitleInt.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitleInt.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitleInt.ShadowOffset = 3;
                ctitleInt.Text = "Career Category Wise Compatibility- Of Interest";
                ctitleInt.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHInterestCC.Titles.Add(ctitleInt);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHInterestCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHInterestCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHInterestCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHInterestCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHInterestCC.Series[0].XValueMember = "CareerCategory";

                CHInterestCC.ChartAreas[0].AxisX.Interval = 1;
                CHInterestCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHInterestCC.Width = 300;

                CHInterestCC.Height = 300;

                // Set Charts Border Color and Width
                CHInterestCC.Series[0].BorderWidth = 1;
                CHInterestCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHInterestCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHInterestCC.DataBind();

                #endregion

                // Query For Personality

                Strcmd = "SELECT B.Career_category As CareerCategory,COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + HFPersonalityP.Value + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                         " AND B.Career_category IS NOT NULL GROUP BY B.Career_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVPersonalityCC.DataSource = dsCareerCount;
                GDVPersonalityCC.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHPersonalityCC.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                CHPersonalityCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHPersonalityCC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHPersonalityCC.Titles.Add(ctitlePersonality);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHPersonalityCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHPersonalityCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHPersonalityCC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHPersonalityCC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHPersonalityCC.Series[0].XValueMember = "CareerCategory";

                CHPersonalityCC.ChartAreas[0].AxisX.Interval = 1;
                CHPersonalityCC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHPersonalityCC.Width = 300;

                CHPersonalityCC.Height = 300;

                // Set Charts Border Color and Width
                CHPersonalityCC.Series[0].BorderWidth = 1;
                CHPersonalityCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHPersonalityCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHPersonalityCC.DataBind();

                #endregion

                //Query For Combined OF ABILITY Interest And Personality

                Strcmd = " SELECT CareerCategory,COUNT(CareerCategory) AS CategoryCount,CONVERT(decimal(18, 2), CONVERT(decimal, COUNT(CareerCategory)) /" + Label2.Text + ")*100 AS Compatibility " +
                     " FROM (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As  OccupationalCategory,B.basic_info1 As Career  " +
                     " FROM tbl_career_ability_master AS A  INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in (SELECT distinct ca_id FROM tbl_career_ability_master " +
                     " where (ability1 = '" + drop_ability1.SelectedValue + "'  or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  " +
                     " AND B.Career_category IS NOT NULL  UNION  SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or  ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                     " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or  ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  AND B.Career_category IS NOT NULL " +
                     " UNION  SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "'  or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  AND B.Career_category IS NOT NULL)  " +
                     " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "'  or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') AND ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')" +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL) " +
                     " UNION (SELECT distinct ca_id ,Career_category As CareerCategory,Occupational_category As OccupationalCategory, basic_info1 As Career  FROM tbl_career_master where ca_id in  (SELECT distinct ca_id FROM tbl_career_master " +
                     " where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') OR ca_id in (SELECT distinct ca_id  FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') ) AND ca_id not in  " +
                     " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or " +
                     " basic_info5 = '" + drop_interest2.SelectedValue + "' )	) AND Career_category IS NOT NULL) UNION (SELECT distinct A.ca_id As CareerID,  B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                     " FROM tbl_career_personality_master AS A  INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "')  AND A.ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))  AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master where" +
                     " (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )AND B.Career_category IS NOT NULL))AS G GROUP BY CareerCategory ORDER BY CategoryCount DESC";
            }



            dsCareerCount = dbContext.ExecDataSet(Strcmd);
            GDVCombinedCC.DataSource = dsCareerCount;
            GDVCombinedCC.DataBind();

            GDVCombinedCC.Visible = true;
            CHAbilityCC.Visible = true;
            CHPersonalityCC.Visible = true;
            CHInterestCC.Visible = true;
            CHCombinedCC.Visible = true;

            #region Combined Chart Details

            //Converting Dataset to Datatable for displaying Chart
            DataTable myDataTableCombined = dsCareerCount.Tables[0];

            // Set DataSource to Chart
            CHCombinedCC.DataSource = myDataTableCombined;

            //// Set Doughnut chart type
            CHCombinedCC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

            // Set labels style for Showing Chart Labele Outside the Chart
            CHCombinedCC.Series[0]["PieLabelStyle"] = "Outside";

            //// Set Doughnut radius percentage
            //Chart1.Series[0]["DoughnutRadius"] = "30";


            //Set Chart's title
            System.Web.UI.DataVisualization.Charting.Title ctitle1 = new System.Web.UI.DataVisualization.Charting.Title();
            ctitle1.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
            ctitle1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            ctitle1.ShadowOffset = 3;
            ctitle1.Text = "Career Category Wise Compatibility";
            ctitle1.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

            CHCombinedCC.Titles.Add(ctitle1);

            //// Charts Legends 
            //Chart1.Legends.Add("Legent1");
            //Chart1.Legends["Legent1"].Enabled = true;
            //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
            //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
            //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Enable 3D
            CHCombinedCC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            CHCombinedCC.ChartAreas[0].Area3DStyle.Inclination = 45;
            CHCombinedCC.ChartAreas[0].Area3DStyle.Rotation = 45;

            //Set the Y-axel as Category Count 
            CHCombinedCC.Series[0].YValueMembers = "Compatibility";

            //Set the X-axle as Category value 
            CHCombinedCC.Series[0].XValueMember = "CareerCategory";

            CHCombinedCC.ChartAreas[0].AxisX.Interval = 1;
            CHCombinedCC.ChartAreas[0].AxisY.Interval = 1;

            ////set Charts Height And Width
            CHCombinedCC.Width = 300;

            CHCombinedCC.Height = 300;

            // Set Charts Border Color and Width
            CHCombinedCC.Series[0].BorderWidth = 1;
            CHCombinedCC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            // Set Chart Tooltip
            CHCombinedCC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

            //Bind the Chart control with the setting above 
            CHCombinedCC.DataBind();
            #endregion

            #endregion

            #region Occupational CategoryCount AND Chart


            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                //Query For Ability

                Strcmd = "";
                Strcmd = "SELECT B.Occupational_category AS OccupationalCategory, COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/" + HFAbility.Value + ")*100 as Compatibility " +
                         " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')" +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                         " GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVAbilityOC.DataSource = dsCareerCount;
                GDVAbilityOC.DataBind();

                #region Ability Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTable = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHAbilityOC.DataSource = myDataTable;

                //// Set Doughnut chart type
                CHAbilityOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHAbilityOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitle = new System.Web.UI.DataVisualization.Charting.Title();
                ctitle.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitle.ShadowOffset = 3;
                ctitle.Text = "Career Category Wise Compatibility- Of Ability";
                ctitle.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHAbilityOC.Titles.Add(ctitle);

                //// Charts Legends 
                //CHAbilityOC.Legends.Add("Legent1");
                //CHAbilityOC.Legends["Legent1"].Enabled = true;
                //CHAbilityOC.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //CHAbilityOC.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //CHAbilityOC.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHAbilityOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHAbilityOC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHAbilityOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHAbilityOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHAbilityOC.Series[0].XValueMember = "OccupationalCategory";

                CHAbilityOC.ChartAreas[0].AxisX.Interval = 1;
                CHAbilityOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHAbilityOC.Width = 300;

                CHAbilityOC.Height = 300;

                // Set Charts Border Color and Width
                CHAbilityOC.Series[0].BorderWidth = 1;
                CHAbilityOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHAbilityOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHAbilityOC.DataBind();

                #endregion

                //Query For Interest

                Strcmd = "SELECT Occupational_category AS OccupationalCategory, COUNT(Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Occupational_category))/" + HFInterest.Value + ")*100 as Compatibility " +
                         " FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') " +
                         " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') AND Occupational_category IS NOT NULL GROUP BY Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVInterestOC.DataSource = dsCareerCount;
                GDVInterestOC.DataBind();


                #region Interest Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTableInt = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHInterestOC.DataSource = myDataTableInt;

                //// Set Doughnut chart type
                CHInterestOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHInterestOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitleInt = new System.Web.UI.DataVisualization.Charting.Title();
                ctitleInt.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitleInt.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitleInt.ShadowOffset = 3;
                ctitleInt.Text = "Career Category Wise Compatibility- Of Interest";
                ctitleInt.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHInterestOC.Titles.Add(ctitleInt);

                //// Charts Legends 
                //CHInterestOC.Legends.Add("Legent1");
                //CHInterestOC.Legends["Legent1"].Enabled = true;
                //CHInterestOC.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //CHInterestOC.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //CHInterestOC.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHInterestOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHInterestCC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHInterestOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHInterestOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHInterestOC.Series[0].XValueMember = "OccupationalCategory";

                CHInterestOC.ChartAreas[0].AxisX.Interval = 1;
                CHInterestOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHInterestOC.Width = 300;

                CHInterestOC.Height = 300;

                // Set Charts Border Color and Width
                CHInterestOC.Series[0].BorderWidth = 1;
                CHInterestOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHInterestOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHInterestOC.DataBind();

                #endregion

                // Query For Personality

                Strcmd = "SELECT B.Occupational_category As OccupationalCategory,COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/ " + HFPersonality.Value + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                            " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                            " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                            " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                            " AND B.Occupational_category IS NOT NULL GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVPersonalityOC.DataSource = dsCareerCount;
                GDVPersonalityOC.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHPersonalityOC.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                CHPersonalityOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHPersonalityOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHPersonalityOC.Titles.Add(ctitlePersonality);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHPersonalityOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHPersonalityOC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHPersonalityOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHPersonalityOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHPersonalityOC.Series[0].XValueMember = "OccupationalCategory";

                CHPersonalityOC.ChartAreas[0].AxisX.Interval = 1;
                CHPersonalityOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHPersonalityOC.Width = 300;

                CHPersonalityOC.Height = 300;

                // Set Charts Border Color and Width
                CHPersonalityOC.Series[0].BorderWidth = 1;
                CHPersonalityOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHPersonalityOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHPersonalityOC.DataBind();

                #endregion

                //Query For Combined OF ABILITY Interest And Personality

                Strcmd = " SELECT OccupationalCategory,COUNT(OccupationalCategory) AS CategoryCount,CONVERT(decimal(18, 2), CONVERT(decimal, COUNT(OccupationalCategory)) /" + Label1.Text + ")*100 AS Compatibility " +
                    " FROM (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                    " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                    " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where  (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "'  or ability3 = '" + drop_ability2.SelectedValue + "') " +
                    " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "'  or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL " +
                    " UNION  (SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')   " +
                    " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL) " +
                    " UNION (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                    " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "')" +
                    " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                    " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                    " AND B.Career_category IS NOT NULL))AS G GROUP BY OccupationalCategory ORDER BY CategoryCount DESC";
            }
            else
            {

                // Query For Ability

                Strcmd = "";
                Strcmd = "SELECT B.Occupational_category AS OccupationalCategory, COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/" + HFAbilityP.Value + ")*100 as Compatibility FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                Strcmd += " UNION ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                Strcmd += " UNION ";
                Strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                Strcmd += " ) AND A.ca_id not in ( ";
                Strcmd += " SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                Strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                Strcmd += " ) GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVAbilityOC.DataSource = dsCareerCount;
                GDVAbilityOC.DataBind();

                #region Ability Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTable = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHAbilityOC.DataSource = myDataTable;

                //// Set Doughnut chart type
                CHAbilityOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHAbilityOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitle = new System.Web.UI.DataVisualization.Charting.Title();
                ctitle.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitle.ShadowOffset = 3;
                ctitle.Text = "Career Category Wise Compatibility- Of Ability";
                ctitle.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHAbilityOC.Titles.Add(ctitle);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHAbilityOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHAbilityOC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHAbilityOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHAbilityOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHAbilityOC.Series[0].XValueMember = "OccupationalCategory";

                CHAbilityOC.ChartAreas[0].AxisX.Interval = 1;
                CHAbilityOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHAbilityOC.Width = 300;

                CHAbilityOC.Height = 300;

                // Set Charts Border Color and Width
                CHAbilityOC.Series[0].BorderWidth = 1;
                CHAbilityOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHAbilityOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHAbilityOC.DataBind();

                #endregion

                //Query For Interest

                Strcmd = "SELECT Occupational_category AS OccupationalCategory, COUNT(Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(Occupational_category))/" + HFInterestP.Value + ")*100 as Compatibility FROM tbl_career_master where ca_id in ";
                Strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                Strcmd += " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )";
                Strcmd += " AND ca_id not in ";
                Strcmd += " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')";
                Strcmd += " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)AND Occupational_category IS NOT NULL  GROUP BY Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVInterestOC.DataSource = dsCareerCount;
                GDVInterestOC.DataBind();

                #region Interest Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTableInt = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHInterestOC.DataSource = myDataTableInt;

                //// Set Doughnut chart type
                CHInterestOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHInterestOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitleInt = new System.Web.UI.DataVisualization.Charting.Title();
                ctitleInt.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitleInt.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitleInt.ShadowOffset = 3;
                ctitleInt.Text = "Career Category Wise Compatibility- Of Interest";
                ctitleInt.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHInterestOC.Titles.Add(ctitleInt);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHInterestOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHInterestOC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHInterestOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHInterestOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHInterestOC.Series[0].XValueMember = "OccupationalCategory";

                CHInterestOC.ChartAreas[0].AxisX.Interval = 1;
                CHInterestOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHInterestOC.Width = 300;

                CHInterestOC.Height = 300;

                // Set Charts Border Color and Width
                CHInterestOC.Series[0].BorderWidth = 1;
                CHInterestOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHInterestOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHInterestOC.DataBind();

                #endregion

                // Query For Personality

                Strcmd = "SELECT B.Occupational_category As OccupationalCategory,COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/ " + HFPersonalityP.Value + ")*100 as Compatibility FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                         " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                         " AND B.Occupational_category IS NOT NULL GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";

                dsCareerCount = dbContext.ExecDataSet(Strcmd);
                GDVPersonalityOC.DataSource = dsCareerCount;
                GDVPersonalityOC.DataBind();

                #region Personality Chart Details

                //Converting Dataset to Datatable for displaying Chart
                DataTable myDataTablePersonality = dsCareerCount.Tables[0];

                // Set DataSource to Chart
                CHPersonalityOC.DataSource = myDataTablePersonality;

                //// Set Doughnut chart type
                CHPersonalityOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

                // Set labels style for Showing Chart Labele Outside the Chart
                CHPersonalityOC.Series[0]["PieLabelStyle"] = "Outside";

                //// Set Doughnut radius percentage
                //Chart1.Series[0]["DoughnutRadius"] = "30";


                //Set Chart's title
                System.Web.UI.DataVisualization.Charting.Title ctitlePersonality = new System.Web.UI.DataVisualization.Charting.Title();
                ctitlePersonality.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
                ctitlePersonality.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
                ctitlePersonality.ShadowOffset = 3;
                ctitlePersonality.Text = "Career Category Wise Compatibility- Of Personality";
                ctitlePersonality.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

                CHPersonalityOC.Titles.Add(ctitlePersonality);

                //// Charts Legends 
                //Chart1.Legends.Add("Legent1");
                //Chart1.Legends["Legent1"].Enabled = true;
                //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
                //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
                //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Enable 3D
                CHPersonalityOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                CHPersonalityOC.ChartAreas[0].Area3DStyle.Inclination = 45;
                CHPersonalityOC.ChartAreas[0].Area3DStyle.Rotation = 45;

                //Set the Y-axel as Category Count 
                CHPersonalityOC.Series[0].YValueMembers = "Compatibility";

                //Set the X-axle as Category value 
                CHPersonalityOC.Series[0].XValueMember = "OccupationalCategory";

                CHPersonalityOC.ChartAreas[0].AxisX.Interval = 1;
                CHPersonalityOC.ChartAreas[0].AxisY.Interval = 1;

                ////set Charts Height And Width
                CHPersonalityOC.Width = 300;

                CHPersonalityOC.Height = 300;

                // Set Charts Border Color and Width
                CHPersonalityOC.Series[0].BorderWidth = 1;
                CHPersonalityOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

                // Set Chart Tooltip
                CHPersonalityOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

                //Bind the Chart control with the setting above 
                CHPersonalityOC.DataBind();

                #endregion

                //Query For Combined OF ABILITY Interest And Personality

                Strcmd = " SELECT OccupationalCategory,COUNT(OccupationalCategory) AS CategoryCount,CONVERT(decimal(18, 2), CONVERT(decimal, COUNT(OccupationalCategory)) /" + Label2.Text + ")*100 AS Compatibility " +
                 " FROM (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As  OccupationalCategory,B.basic_info1 As Career  " +
                 " FROM tbl_career_ability_master AS A  INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in (SELECT distinct ca_id FROM tbl_career_ability_master " +
                 " where (ability1 = '" + drop_ability1.SelectedValue + "'  or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                 " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  " +
                 " AND B.Career_category IS NOT NULL  UNION  SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or  ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                 " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or  ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  AND B.Career_category IS NOT NULL " +
                 " UNION  SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                 " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "'  or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  AND B.Career_category IS NOT NULL)  " +
                 " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "'  or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') AND ca_id in " +
                 " (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')" +
                 " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL) " +
                 " UNION (SELECT distinct ca_id ,Career_category As CareerCategory,Occupational_category As OccupationalCategory, basic_info1 As Career  FROM tbl_career_master where ca_id in  (SELECT distinct ca_id FROM tbl_career_master " +
                 " where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "') OR ca_id in (SELECT distinct ca_id  FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') ) AND ca_id not in  " +
                 " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or " +
                 " basic_info5 = '" + drop_interest2.SelectedValue + "' )	) AND Career_category IS NOT NULL) UNION (SELECT distinct A.ca_id As CareerID,  B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                 " FROM tbl_career_personality_master AS A  INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "')  AND A.ca_id in " +
                 " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))  AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master where" +
                 " (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )AND B.Career_category IS NOT NULL))AS G GROUP BY OccupationalCategory ORDER BY CategoryCount DESC";
            }


            dsCareerCount = dbContext.ExecDataSet(Strcmd);
            GDVCombinedOC.DataSource = dsCareerCount;
            GDVCombinedOC.DataBind();

            GDVCombinedOC.Visible = true;
            CHAbilityOC.Visible = true;
            CHPersonalityOC.Visible = true;
            CHInterestOC.Visible = true;
            CHCombinedOC.Visible = true;

            #region Combined Chart Details

            //Converting Dataset to Datatable for displaying Chart
            DataTable myDataTableCombinedOC = dsCareerCount.Tables[0];

            // Set DataSource to Chart
            CHCombinedOC.DataSource = myDataTableCombinedOC;

            //// Set Doughnut chart type
            CHCombinedOC.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut;

            // Set labels style for Showing Chart Labele Outside the Chart
            CHCombinedOC.Series[0]["PieLabelStyle"] = "Outside";

            //// Set Doughnut radius percentage
            //Chart1.Series[0]["DoughnutRadius"] = "30";


            //Set Chart's title
            System.Web.UI.DataVisualization.Charting.Title ctitlecombined = new System.Web.UI.DataVisualization.Charting.Title();
            ctitlecombined.ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0);
            ctitlecombined.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            ctitlecombined.ShadowOffset = 3;
            ctitlecombined.Text = "Career Category Wise Compatibility";
            ctitlecombined.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

            CHCombinedOC.Titles.Add(ctitlecombined);

            //// Charts Legends 
            //Chart1.Legends.Add("Legent1");
            //Chart1.Legends["Legent1"].Enabled = true;
            //Chart1.Series[0].LegendText = "#VALX #PERCENT{P2}";
            //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
            //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Enable 3D
            CHCombinedOC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            CHCombinedOC.ChartAreas[0].Area3DStyle.Inclination = 45;
            CHCombinedOC.ChartAreas[0].Area3DStyle.Rotation = 45;

            //Set the Y-axel as Category Count 
            CHCombinedOC.Series[0].YValueMembers = "Compatibility";

            //Set the X-axle as Category value 
            CHCombinedOC.Series[0].XValueMember = "OccupationalCategory";

            CHCombinedOC.ChartAreas[0].AxisX.Interval = 1;
            CHCombinedOC.ChartAreas[0].AxisY.Interval = 1;

            ////set Charts Height And Width
            CHCombinedOC.Width = 300;

            CHCombinedOC.Height = 300;

            // Set Charts Border Color and Width
            CHCombinedOC.Series[0].BorderWidth = 1;
            CHCombinedOC.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            // Set Chart Tooltip
            CHCombinedOC.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

            //Bind the Chart control with the setting above 
            CHCombinedOC.DataBind();

            #endregion

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

    protected void GDVAbility_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVAbility_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVAbility.PageIndex = e.NewPageIndex;

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                    " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                    " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                    " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND " +
                    " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                    " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL ";

            DataSet dsAbility = dbContext.ExecDataSet(Strcmd);
            HFAbility.Value = dsAbility.Tables[0].Rows.Count.ToString();
            GDVAbility.DataSource = dsAbility;
            GDVAbility.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVAbility_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

    protected void GDVInterest_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVInterest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVInterest.PageIndex = e.NewPageIndex;


            Strcmd = "SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                  "  WHERE (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  " +
                  " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL";

            GDVInterest.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVInterest.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVInterest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVPersonality_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVPersonality_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVPersonality.PageIndex = e.NewPageIndex;

            Strcmd = "SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                    " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                    " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                    " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                    " AND B.Career_category IS NOT NULL";



            GDVPersonality.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVPersonality.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVPersonality_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVCombined_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVCombined_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVCombined.PageIndex = e.NewPageIndex;

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                  " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                  " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  AND A.ca_id in " +
                  " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + drop_ability2.SelectedValue + "'  or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND " +
                  " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                  " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  AND B.Career_category IS NOT NULL " +
                  " UNION  " +
                  " (SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                  "  WHERE (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')  " +
                  " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "')AND Career_category IS NOT NULL)" +
                  " UNION " +
                  " (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                  " where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                  " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "'))" +
                  " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "'))" +
                  " AND B.Career_category IS NOT NULL) order by B.Career_category";


            GDVCombined.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVCombined.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVCombined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVAbilityPC_DataBound(object sender, EventArgs e)
    {

    }
    protected void GDVAbilityPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVAbilityPC.PageIndex = e.NewPageIndex;

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                     " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                     " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                     " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL" +
                     " UNION " +
                     " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                     " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL" +
                     " UNION " +
                     " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                     " AND B.Career_category IS NOT NULL) " +
                     " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')" +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                     " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                     " AND B.Career_category IS NOT NULL)";


            GDVAbilityPC.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVAbilityPC.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVAbilityPC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

    protected void GDVInterestPC_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVInterestPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVInterestPC.PageIndex = e.NewPageIndex;

            Strcmd = " SELECT distinct ca_id As CareerID ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                     " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                     " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )" +
                     " AND ca_id not in " +
                     " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                     " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)" +
                     " AND Career_category IS NOT NULL";


            GDVInterestPC.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVInterestPC.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVInterestPC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVPersonalityPC_DataBound(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVPersonalityPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVPersonalityPC.PageIndex = e.NewPageIndex;

            Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                     " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                     " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                     " AND A.ca_id in " +
                     " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                     " AND B.Career_category IS NOT NULL";

            GDVPersonalityPC.DataSource = dbContext.ExecDataSet(Strcmd);
            GDVPersonalityPC.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVPersonalityPC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void GDVCombinedPC_DataBound(object sender, EventArgs e)
    {

    }
    protected void GDVCombinedPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GDVCombinedPC.PageIndex = e.NewPageIndex;


            //Query For Combined OF ABILITY Interest And Personality

            string Strcmd = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                   " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                   " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                   " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL" +
                   " UNION " +
                   " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                   " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL" +
                   " UNION " +
                   " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') " +
                   " AND B.Career_category IS NOT NULL) " +
                   " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')" +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') " +
                   " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) " +
                   " AND B.Career_category IS NOT NULL)" +
                   " UNION " +
                   "(SELECT distinct ca_id ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                   " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                   " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "') )" +
                   " AND ca_id not in " +
                   " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + drop_interest1.SelectedValue + "' or basic_info5 = '" + drop_interest1.SelectedValue + "')" +
                   " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + drop_interest2.SelectedValue + "' or basic_info5 = '" + drop_interest2.SelectedValue + "' )	)" +
                   " AND Career_category IS NOT NULL)" +
                   " UNION " +
                   "(SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                   " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + drop_personality1.SelectedValue + "' OR personality2='" + drop_personality1.SelectedValue + "' OR personality3='" + drop_personality1.SelectedValue + "') " +
                   " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality2.SelectedValue + "' or personality2 = '" + drop_personality2.SelectedValue + "' or personality3 = '" + drop_personality2.SelectedValue + "')) " +
                   " AND A.ca_id in " +
                   " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + drop_personality3.SelectedValue + "' or personality2 = '" + drop_personality3.SelectedValue + "' or personality3 = '" + drop_personality3.SelectedValue + "') )" +
                   " AND B.Career_category IS NOT NULL)";

            DataSet ds2 = dbContext.ExecDataSet(Strcmd);
            Label2.Text = ds2.Tables[0].Rows.Count.ToString();
            GDVCombinedPC.DataSource = ds2;
            GDVCombinedPC.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void GDVCombinedPC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

}