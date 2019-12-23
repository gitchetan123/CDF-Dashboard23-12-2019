using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_training_batch : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();
    string level;
    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GridView1.SelectedIndex = -1;

            if (!IsPostBack)
            {
                div_msg.Visible = false;
                try
                {
                    string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
                    dbContext.BindDropDownlist(StrQuery2, ref ddl_state);

                    // CDF Levels Added
                    string StrQueryCDFLevel = "select id, cdfLevel from tblCDFLevel";
                    dbContext.BindDropDownlist(StrQueryCDFLevel, ref ddl_cdfLevel);
                    

                    ddl_city.Items.Clear();
                    ddl_city.Items.Insert(0, "--Select--");
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    // if condition fails then user will get following message
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
                }

                clear();
                div_msg.Visible = false;
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
    }

    public void clear()
    {
        hf_id.Value = "";
        txt_btachName.Text = "";
        tbDate1.Text = "";
        ddl_state.SelectedIndex = 0;
        ddl_city.Items.Clear();
        ddl_city.Items.Insert(0, "--Select--");
        txt_location.Text = "";
        txt_details.Text = "";
        txt_TrainerName.Text = "";
        txt_count.Text = "";
        btn_submit.Enabled = true;
        btn_update.Enabled = false;
        ddl_cdfLevel.SelectedIndex = 0;
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //The city DropDownList contents 
            string StrQuery = "select id, name from tblCitiesMaster where stateId='" + ddl_state.SelectedValue + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_city);
        }
        catch (Exception)
        {
            ddl_city.Items.Clear();
            ddl_city.Items.Insert(0, "--Select--");
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        clear();
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {

                    try
                    {
                        string date = dataContext.DateConvert(tbDate1.Text.Trim().ToString());

                        // insert cdf training batch details
                        //string str = "insert into tblTrainingBatch (batchName,cityId,location,date,details,trainerName,cdfcount,createdBy,createdDate) " +
                        //    "values(@btachName,@cityid,@location,@date,@details,@trainerName,@cdfcount,@createdBy,@createdDate)";

                        // CDF Levels Added.
                        string str = "insert into tblTrainingBatch (batchName,cityId,location,date,details,trainerName,cdfcount,createdBy,createdDate,cdfLevel) " +
                                "values(@btachName,@cityid,@location,@date,@details,@trainerName,@cdfcount,@createdBy,@createdDate,@cdfLevel)";

                        SqlCommand cmd = new SqlCommand(str, connection);
                        cmd.Parameters.AddWithValue("@btachName", txt_btachName.Text.Trim());
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@cityid", ddl_city.SelectedValue);
                        cmd.Parameters.AddWithValue("@location", txt_location.Text);
                        cmd.Parameters.AddWithValue("@details", txt_details.Text);
                        cmd.Parameters.AddWithValue("@trainerName", txt_TrainerName.Text);
                        cmd.Parameters.AddWithValue("@cdfcount", txt_count.Text);
                        cmd.Parameters.AddWithValue("@createdBy", Convert.ToString(Session["adminuser_name"]));
                        cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@cdfLevel", ddl_cdfLevel.SelectedValue);

                        connection.Open();
                        int j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            clear();
                            GridView1.DataSource = BindGridView();
                            GridView1.DataBind();
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerText = "Successfully submitted.";
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Something went wrong...Please try again.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
    }

    private DataTable BindGridView()
    {
        try
        {
            //Select details id in tblUserMaster table
            string strcmd = "select b.*,c.stateId, c.name as city from tblTrainingBatch as b left outer join tblCitiesMaster as c on b.cityId=c.id order by b.date desc";

            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
            return null;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            GridView1.DataSource = sortedView;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
    }

    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }

  

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int id = Convert.ToInt32(GridView1.SelectedValue.ToString());
                string StrQuery1 = "select cdfLevel from tblTrainingBatch where id=" + id;
                SqlCommand cmd = new SqlCommand(StrQuery1, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    level = dr.GetValue(0).ToString();
                    ViewState["Level"] = level;
                }
                dr.Close();
                dr.Dispose();
            }

            div_msg.Visible = false;
            hf_id.Value = GridView1.SelectedValue.ToString();
            txt_btachName.Text = GridView1.SelectedRow.Cells[2].Text;
            txt_location.Text = GridView1.SelectedRow.Cells[4].Text;
            tbDate1.Text = GridView1.SelectedRow.Cells[5].Text;
            txt_TrainerName.Text = GridView1.SelectedRow.Cells[6].Text;
            txt_count.Text = GridView1.SelectedRow.Cells[7].Text;
            ddl_cdfLevel.SelectedItem.Text = ViewState["Level"].ToString();
            btn_submit.Enabled = false;
            btn_update.Enabled = true;

            string country = "101";

            string city = GridView1.SelectedRow.Cells[8].Text;
            string state = GridView1.SelectedRow.Cells[9].Text;
            txt_details.Text = GridView1.SelectedRow.Cells[10].Text;

            string StrQuery = "";
            //The state DropDownList contents 
            StrQuery = "select id,name from tblStatesMaster where countryId='" + country + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_state);

            StrQuery = "";
            //The city DropDownList contents 
            StrQuery = "select id, name from tblCitiesMaster where stateId='" + state + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_city);

            string StrQueryCDFLevel = "select id, cdfLevel from tblCDFLevel";
            dbContext.BindDropDownlist(StrQueryCDFLevel, ref ddl_cdfLevel);

            //ddl_country.Items.FindByValue(country).Selected = true;
            ddl_state.Items.FindByValue(state).Selected = true;
            ddl_city.Items.FindByValue(city).Selected = true;
            ddl_cdfLevel.Items.FindByValue(level).Selected = true;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    try
                    {
                        string date = dataContext.DateConvert(tbDate1.Text.Trim().ToString());

                        // insert cdf training batch details
                        //string str = "update tblTrainingBatch set batchName=@btachName,cityId=@cityid,location=@location,date=@date,details=@details,trainerName=@trainerName, " +
                        //    "cdfcount= @cdfcount, createdBy= @createdBy, createdDate=@createdDate where id=@batchid";

                        // CDF Levels Added.
                        string str = "update tblTrainingBatch set batchName=@btachName,cityId=@cityid,location=@location,date=@date,details=@details,trainerName=@trainerName, " +
                            "cdfcount= @cdfcount, createdBy= @createdBy, createdDate=@createdDate, cdfLevel=@cdfLevel where id=@batchid";

                        SqlCommand cmd = new SqlCommand(str, connection);
                        cmd.Parameters.AddWithValue("@btachName", txt_btachName.Text.Trim());
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@cityid", ddl_city.SelectedValue);
                        cmd.Parameters.AddWithValue("@location", txt_location.Text);
                        cmd.Parameters.AddWithValue("@details", txt_details.Text);
                        cmd.Parameters.AddWithValue("@trainerName", txt_TrainerName.Text);
                        cmd.Parameters.AddWithValue("@cdfcount", txt_count.Text);
                        cmd.Parameters.AddWithValue("@createdBy", Convert.ToString(Session["adminuser_name"]));
                        cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@batchid", hf_id.Value);
                        cmd.Parameters.AddWithValue("@cdfLevel", ddl_cdfLevel.SelectedValue);

                        connection.Open();
                        int j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            clear();
                            GridView1.DataSource = BindGridView();
                            GridView1.DataBind();
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerText = "Successfully updated.";
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Something went wrong...Please try again.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please try again.";
        }
        //
    }
}