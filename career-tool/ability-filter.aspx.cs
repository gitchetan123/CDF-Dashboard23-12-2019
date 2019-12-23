using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class AbilityFilter : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    db_context_career dbContext = new db_context_career();

    string strcmd = "";
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
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            Ability();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }


    private void Ability()
    {
        try
        {
            //Select career by ability from selected values by user
            strcmd = "SELECT B.ca_id,A.ability1 as Ability1, A.ability2 As Ability2, A.ability3 As Ability3,B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  " +
                       "  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
            strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
            strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
            strcmd += " order by A.ca_id,ability1, ability2, ability3";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            Label1.Text = ds.Tables[0].Rows.Count.ToString(); //Career Category Wise Count
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
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

            strcmd = "SELECT B.ca_id,A.ability1 as Ability1, A.ability2 As Ability2, A.ability3 As Ability3,B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  " +
                            "  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
            strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
            strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
            strcmd += " order by A.ca_id,ability1, ability2, ability3";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            Label1.Text = ds.Tables[0].Rows.Count.ToString(); //Career Category Wise Count
            GridView1.DataSource = ds;
            GridView1.DataBind();



            #endregion

            #region Partially compatible - totally compatible
            if (ds.Tables[0].Rows.Count == 0)
            {
                strcmd = "SELECT distinct  A.ability1 as Ability1, A.ability2 As Ability2, A.ability3 As Ability3,B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career  FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " ) AND A.ca_id not in ( ";
                strcmd += " SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                strcmd += " )";




            }
            #endregion

            #region Career CategoryCount AND Chart

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {
                strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(D.Career_category) AS CategoryCount " +
                        " FROM tbl_career_ability_master AS C INNER JOIN tbl_career_master As D    ON C.ca_id = D.ca_id " +
                        " where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                        " AND C.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND C.ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                        " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  GROUP BY D.Career_category ) As E ";
                DataSet dsCareersum = dbContext.ExecDataSet(strcmd);

                strcmd = "";
                strcmd = "SELECT B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/ " + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  " +
                         "  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                strcmd += " GROUP BY B.Career_category ORDER BY CategoryCount DESC";


            }
            else
            {

                strcmd = "";
                strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(B.Career_category) AS CategoryCount " +
                        " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                        " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_ability_master " +
                        " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_ability_master where " +
                        " (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  AND ca_id in (SELECT [ca_id] " +
                        " FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' " +
                        " OR ability3 = '" + drop_ability3.SelectedValue + "')  ) AND A.ca_id not in (  SELECT [ca_id] " +
                        " FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "'" +
                        " OR ability3 = '" + drop_ability1.SelectedValue + "')  AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where " +
                        " (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' " +
                        " or ability3 = '" + drop_ability3.SelectedValue + "') )  ) GROUP BY B.Career_category ) As E";
                DataSet dsCareersum = dbContext.ExecDataSet(strcmd);

                strcmd = "";
                strcmd = "SELECT B.Career_category AS CareerCategory, COUNT(B.Career_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Career_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " ) AND A.ca_id not in ( ";
                strcmd += " SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                strcmd += " ) GROUP BY B.Career_category ORDER BY CategoryCount DESC";
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
            ctitle.Text = "Career Category Wise Count";
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
            Chart1.Series[0].YValueMembers = "Percentage";

            //Set the X-axle as Category value 
            Chart1.Series[0].XValueMember = "CareerCategory";

            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisY.Interval = 1;

            ////set an angle to the label
            Chart1.Width = 400;

            Chart1.Height = 400;

            Chart1.Series[0].BorderWidth = 1;
            Chart1.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            Chart1.Series[0].ToolTip = "Career Category: #VALX | Compatibility: #VALY";

            //Bind the Chart control with the setting above 
            Chart1.DataBind();

            #endregion

            #region Occupational CategoryCount AND Chart

            if (Convert.ToInt32((Label1.Text)) >= 8)
            {

                strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(D.Occupational_category) AS CategoryCount " +
                        " FROM tbl_career_ability_master AS C INNER JOIN tbl_career_master As D    ON C.ca_id = D.ca_id " +
                        " where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') " +
                        " AND C.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND C.ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                        " or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') )  GROUP BY D.Occupational_category ) As E ";
                DataSet dsCareersum = dbContext.ExecDataSet(strcmd);

                strcmd = "";
                strcmd = "SELECT B.Occupational_category AS OccupationalCategory, COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B  " +
                                "  ON A.ca_id = B.ca_id where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                strcmd += " GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";


            }
            else
            {

                strcmd = "";
                strcmd = "SELECT SUM(E.CategoryCount) As CategorySum FROM (SELECT COUNT(B.Occupational_category) AS CategoryCount " +
                        " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                        " WHERE A.ca_id in ( SELECT distinct ca_id FROM tbl_career_ability_master " +
                        " WHERE (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_ability_master where " +
                        " (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "')  " +
                        " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "')  UNION " +
                        " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' " +
                        " OR ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')  AND ca_id in (SELECT [ca_id] " +
                        " FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' " +
                        " OR ability3 = '" + drop_ability3.SelectedValue + "')  ) AND A.ca_id not in (  SELECT [ca_id] " +
                        " FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "'" +
                        " OR ability3 = '" + drop_ability1.SelectedValue + "')  AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where " +
                        " (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') AND ca_id in " +
                        " (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' " +
                        " or ability3 = '" + drop_ability3.SelectedValue + "') )  ) GROUP BY B.Occupational_category ) As E";
                DataSet dsCareersum = dbContext.ExecDataSet(strcmd);

                strcmd = "";
                strcmd = "SELECT B.Occupational_category AS OccupationalCategory, COUNT(B.Occupational_category) AS CategoryCount, CONVERT(decimal(18, 2),convert (decimal,COUNT(B.Occupational_category))/" + dsCareersum.Tables[0].Rows[0].ItemArray[0].ToString() + ")*100 as Percentage FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in ( ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " UNION ";
                strcmd += "SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ";
                strcmd += " ) AND A.ca_id not in ( ";
                strcmd += " SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability1.SelectedValue + "' or ability2 = '" + drop_ability1.SelectedValue + "' or ability3 = '" + drop_ability1.SelectedValue + "') ";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + drop_ability2.SelectedValue + "' or ability2 = '" + drop_ability2.SelectedValue + "' or ability3 = '" + drop_ability2.SelectedValue + "')";
                strcmd += " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + drop_ability3.SelectedValue + "' or ability2 = '" + drop_ability3.SelectedValue + "' or ability3 = '" + drop_ability3.SelectedValue + "') ) ";
                strcmd += " ) GROUP BY B.Occupational_category ORDER BY CategoryCount DESC";
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
            ctitle1.Text = "Occupational Category Wise Count";
            ctitle1.ForeColor = System.Drawing.Color.FromArgb(26, 59, 105);

            Chart2.Titles.Add(ctitle1);

            //// Set Doughnut radius percentage
            //Chart2.Series[0]["DoughnutRadius"] = "30";

            //Chart2.Legends.Add("Legent1");
            //Chart2.Legends["Legent1"].Enabled = true;
            //Chart2.Series[0].LegendText = "#VALX #PERCENT{P2}";
            //Chart2.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
            //Chart2.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Enable 3D
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas[0].Area3DStyle.Inclination = 45;
            Chart2.ChartAreas[0].Area3DStyle.Rotation = 45;

            //Set the Y-axel as Category Count 
            Chart2.Series[0].YValueMembers = "Percentage";

            //Set the X-axle as Category value 
            Chart2.Series[0].XValueMember = "OccupationalCategory";

            Chart2.ChartAreas[0].AxisX.Interval = 1;
            Chart2.ChartAreas[0].AxisY.Interval = 1;

            ////set an angle to the label
            Chart2.Width = 400;

            Chart2.Height = 400;

            Chart2.Series[0].BorderWidth = 1;
            Chart2.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);

            //// Chart Tooltip
            Chart2.Series[0].ToolTip = "Occupational Category: #VALX | Compatibility: #VALY ";

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




}