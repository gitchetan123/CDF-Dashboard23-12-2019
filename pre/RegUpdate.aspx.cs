using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


//********************************************************************************************************
//PageName        : Reg
//Description     : This page update gender dob city
//AddedBy         : Nitish                   AddedOn   : 

//********************************************************************************************************

public partial class RegUpdate : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        FilteredTextBoxExtender10.ValidChars = FilteredTextBoxExtender10.ValidChars + "\r\n";
        txtEndDate.Text = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
        if (Session["uid"] != null)
        {
            if (!IsPostBack)
            {
                div_msg.Visible = false;
                try
                {
                    string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
                    dbContext.BindDropDownlist(StrQuery2, ref ddl_state);
                    ddl_city.Items.Clear();
                    ddl_city.Items.Insert(0, "--Select--");

                    //Old dropdown code
                    //string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE'";
                    //dbContext.BindDropDownlist(StrQueryExe, ref ddl_ename);

                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query_exeId = "Select B.id, B.exeName from tblverifyRegistration as A Inner Join tblExecutive as B on A.executiveId = B.id where A.status ='ACTIVE' and A.email= '" + Session["email"].ToString() + "'";

                        SqlDataAdapter da = new SqlDataAdapter(query_exeId, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        ddl_ename.DataSource = ds;
                        ddl_ename.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddl_ename.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddl_ename.DataBind();
                        ds.Tables.Clear();
                        ds.Dispose();
                        da.Dispose();

                        string query_fieldOfWork = "Select distinct ISNULL(Career_category,'No') from tbl_career_master";

                        con.Open();
                        SqlDataAdapter da2 = new SqlDataAdapter(query_fieldOfWork, con);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        ddl_fieldOfWork.DataSource = ds2;
                        //ddl_fieldOfWork.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddl_fieldOfWork.DataValueField = ds2.Tables[0].Columns[0].ToString();
                        ddl_fieldOfWork.DataBind();
                        ddl_fieldOfWork.Items.Insert(0, "--Select--");
                        ddl_fieldOfWork.Items.Remove("No");
                        ds2.Tables.Clear();
                        ds2.Dispose();
                        da2.Dispose();

                        string query_industrySector = "Select distinct basic_info2 from tbl_career_master";

                        SqlDataAdapter da1 = new SqlDataAdapter(query_industrySector, con);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        ddl_industrySector.DataSource = ds1;
                        //ddl_fieldOfWork.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddl_industrySector.DataValueField = ds1.Tables[0].Columns[0].ToString();
                        ddl_industrySector.DataBind();
                        ddl_industrySector.Items.Insert(0, "--Select--");
                        ds1.Tables.Clear();
                        ds1.Dispose();
                        da1.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    // if condition fails then user will get following message
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
                }
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null)
            {
                if (IsValid)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();

                        SqlCommand cmd = connection.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = connection.BeginTransaction("RegisterTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;
                        try
                        {
                            string date = dataContext.DateConvert(tbDate1.Text.Trim().ToString());

                            // Insert remaining user's data into tblUserDetails table
                            string str = "update tblUserMaster set gender=@gender,dob=@dob,cityid=@cityid,address=@address,pincode=@pincode where uid=@uid";
                            cmd.Parameters.AddWithValue("@gender", ddl_gender.Text.Trim().ToString());
                            cmd.Parameters.AddWithValue("@dob", date);
                            cmd.Parameters.AddWithValue("@cityid", ddl_city.SelectedValue);
                            cmd.Parameters.AddWithValue("@address", txt_address.Text.Trim().ToString());
                            cmd.Parameters.AddWithValue("@uId", Session["uid"].ToString());
                            cmd.Parameters.AddWithValue("@pincode", txt_pincode.Text.Trim().ToString());
                            cmd.CommandText = str;
                            int j = cmd.ExecuteNonQuery();
                            if (j > 0)
                            {
                                string str1 = "update tblUserDetails set qualification=@qualification,whyThisOpp=@whyThisOpp,designation=@designation,maritalstatus=@maritalstatus,spouseName=@spouseName,childrenAge=@childrenAge,fieldOfWork=@fieldOfWork,modeOfWork=@modeOfWork,industrySector=@industrySector,aboutSelf=@aboutSelf where uid=@uid";
                                cmd.Parameters.AddWithValue("@qualification", txt_qualification.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@whyThisOpp", txt_why_opportunity.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@designation", txt_designation.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@maritalstatus", ddl_married_status.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@spouseName", txt_spouse.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@childrenAge", txt_children.Text.Trim().ToString());
                                cmd.Parameters.AddWithValue("@fieldOfWork", ddl_fieldOfWork.SelectedValue);
                                cmd.Parameters.AddWithValue("@modeOfWork", ddl_modeOfWork.SelectedValue);
                                cmd.Parameters.AddWithValue("@industrySector", ddl_industrySector.SelectedValue);
                                cmd.Parameters.AddWithValue("@aboutSelf", txt_profile.Text.Trim().ToString());
                                cmd.CommandText = str1;
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    // Insert user id & executive id into tblRelation table
                                    string str2 = "INSERT INTO tblRelation (uId,executiveId) VALUES ('" + Session["uid"].ToString() + "','" + ddl_ename.Text + "')";
                                    cmd.CommandText = str2;
                                    int m = cmd.ExecuteNonQuery();
                                    if (m > 0)
                                    {
                                        // Attempt to commit the transaction.
                                        transaction.Commit();
                                        Response.Redirect("~/pre/test.aspx", false);
                                    }
                                    else
                                    {
                                        Log.Warn("tblRelation inserting data fail");
                                        div_msg.Visible = true;
                                        div_msg.Attributes["class"] = "alert alert-danger";
                                        div_msg.InnerText = "Something Went Wrong!. Please Try again...";
                                    }
                                }
                                else
                                {
                                    Log.Warn("tbluserdetails updation fail");
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-danger";
                                    div_msg.InnerText = "Something Went Wrong!. Please Try again...";
                                }
                            }
                            else
                            {
                                Log.Warn("tblusermaster updation fail");
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Something Went Wrong!. Please Try again...";
                            }
                        }
                        catch (Exception ex1)
                        {
                            Log.Error(ex1);
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "CDF Requirement Form Not Successfully Submitted.....Please try again.";

                            // Attempt to roll back the transaction.
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                Log.Error(ex2);

                                // This catch block will handle any errors that may have occurred
                                // on the server that would cause the rollback to fail, such as
                                // a closed connection.                          
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Rollback Error";
                            }
                        }
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Invalid Data !. Please Try again...";
                }
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something Went Wrong!. Please Try again...";
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        clear();
    }

    //Clear all field 
    public void clear()
    {
        tbDate1.Text = "";
        ddl_gender.SelectedIndex = 0;
        ddl_state.SelectedIndex = 0;
        ddl_city.Items.Clear();
        ddl_city.Items.Insert(0, "--Select--");
        txt_qualification.Text = "";
        txt_address.Text = "";
        txt_designation.Text = "";
        txt_why_opportunity.Text = "";
        ddl_married_status.SelectedIndex = 0;
        txt_spouse.Text = "";
        txt_children.Text = "";
        txt_profile.Text = "";
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
    protected void ddl_married_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_married_status.SelectedValue == "Yes")
        {
            div_married.Visible = true;
        }
        else if (ddl_married_status.SelectedValue == "No")
        {
            div_married.Visible = false;
            txt_spouse.Text = "";
            txt_children.Text = "";
        }
    }
}