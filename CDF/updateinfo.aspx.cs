using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class updateinfo : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string connectionString = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query_fieldOfWork = "Select distinct ISNULL(Career_category,'No') from tbl_career_master";

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query_fieldOfWork, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    ddl_fieldOfWork.DataSource = ds;
                    //ddl_fieldOfWork.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddl_fieldOfWork.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddl_fieldOfWork.DataBind();
                    ddl_fieldOfWork.Items.Insert(0, "--Select--");
                    ddl_fieldOfWork.Items.Remove("No");
                    ds.Tables.Clear();
                    ds.Dispose();
                    da.Dispose();

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


                    int k;
                    for(k=0;k<=50;k++)
                    {
                        ListItem ltItem = new ListItem();

                        ltItem.Text = k.ToString();
                        ltItem.Value = k.ToString();

                        ddl_yearsOfExperience.Items.Add(ltItem);

                    }
                }

                read_data();

            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        btn_edit_update.Visible = true;
        btn_edit.Visible = false;
        pan.Enabled = true;
    }
    protected void btn_edit_update_Click(object sender, EventArgs e)
    {
        //if (btn_edit_update.Text == "Update")
        //{
            
        //    pan.Enabled = false;
        //    btn_edit.Visible = true;
        //}
        //else
        //{

            if (IsValid)
            {
                try
                {
                   // string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string connectionString = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                    {

                        string date = dbContext.DateConvert(txt_dob.Text.Trim().ToString());

                        string StrQuery = "";
                    //update details in tblUserMaster table
                    StrQuery = "update tblUserMaster set fname='" + txt_fname.Text + "',lname='" + txt_lname.Text + "',contactNo='" + txt_contactno.Text + "', address='" + txtAddress.Text + "', "
                    + "cityid='" + ddl_city.SelectedValue + "',gender='" + ddl_gender.SelectedValue + "',dob='"+date+"',profileUpdateApproval = 0,profileDisplayApproval = 0,dateModified = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "',pinCode = '" + txt_pincode.Text + "' where uId='" + Session["uid"] + "'";

                    int i = dbContext.ExecNonQuery(StrQuery);

                    //StrQuery = "update tblUserMaster set fname='" + txt_fname.Text + "',lname='" + txt_lname.Text + "',contactNo='" + txt_contactno.Text + "', address='" + txtAddress.Text + "', "
                    //  + "cityid='" + ddl_city.SelectedValue + "',gender='" + ddl_gender.SelectedValue + "',dob='" + date + "',profileUpdateApproval = 0,profileDisplayApproval = 0,pinCode = '" + txt_pincode.Text + "' where uId='" + Session["uid"] + "'";
                    //int i = dbContext.ExecNonQuery(StrQuery);

                    //update details in tblUserDetails table
                    StrQuery = "";
                        StrQuery = "update tblUserDetails set qualification = '" + txt_qualification.Text + "',designation = '" + txt_designation.Text + "',yearsOfExperience='" + ddl_yearsOfExperience.SelectedValue + "', aboutSelf ='" + txt_desc.Text + "',fieldOfWork='" + ddl_fieldOfWork.SelectedValue + "',modeOfWork='" + ddl_modeOfWork.SelectedValue + "',industrySector='" + ddl_industrySector.SelectedValue + "' where uId='" + Session["uid"] + "'";
                        int j = dbContext.ExecNonQuery(StrQuery);

                        //if update success is show msg
                        if (i != 0 && j != 0)
                        {
                            pan.Enabled = false;
                            btn_edit_update.Text = "Edit";
                        }
                    }
              
                }
                catch (Exception ex)
                {
                    Log.Error("" + ex);
                }
            }
        //}
    }


    void read_data()
    {
        {
            try
            {
              //  string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string connectionString = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Get all user details 
                    string strcmd = "SELECT tblUserMaster.uId, tblUserMaster.fname, tblUserMaster.lname,tblUserMaster.contactNo, tblUserMaster.address, tblUserMaster.gender, tblUserMaster.dob, "
                    + "tblCountriesMaster.id AS countryId, tblStatesMaster.id AS stateId, tblUserMaster.cityid, "
                    + " tblUserDetails.aboutSelf,tblUserMaster.pincode,tblUserDetails.fieldOfWork,tblUserDetails.modeOfWork,tblUserDetails.industrySector,tblUserDetails.qualification,tblUserDetails.designation,tblUserDetails.yearsOfExperience FROM tblCitiesMaster right outer JOIN tblUserMaster INNER JOIN "
                    + "tblUserDetails ON tblUserMaster.uId = tblUserDetails.uId ON tblCitiesMaster.id = tblUserMaster.cityid full outer JOIN "
                    + "tblStatesMaster full outer JOIN tblCountriesMaster ON tblStatesMaster.countryId = tblCountriesMaster.id ON "
                    + "tblCitiesMaster.stateId = tblStatesMaster.id WHERE (tblUserMaster.uId = '" + Convert.ToInt32(Session["uid"]) + "')";

                   

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        {
                            txt_fname.Text = dr["fname"].ToString();
                            txt_lname.Text = dr["lname"].ToString();
                            if (dr["dob"] != DBNull.Value)
                            {
                                txt_dob.Text = Convert.ToDateTime(dr["dob"]).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                txt_dob.Text = "";
                            }
                            txt_contactno.Text = dr["contactNo"].ToString();
                            ddl_gender.SelectedValue = dr["gender"].ToString();
                            txt_desc.Text = dr["aboutSelf"].ToString();
                            txt_qualification.Text = dr["qualification"].ToString();
                            txt_designation.Text = dr["designation"].ToString();
                            ddl_yearsOfExperience.SelectedValue = dr["yearsOfExperience"].ToString();
                            //New Requirement display and update 
                            txt_pincode.Text = dr["pincode"].ToString();

                            ddl_fieldOfWork.SelectedValue = dr["fieldOfWork"].ToString();
                            ddl_modeOfWork.SelectedValue= dr["modeOfWork"].ToString();                          
                            ddl_industrySector.SelectedValue = dr["industrySector"].ToString();

                            txtAddress.Text = dr["address"].ToString();

                            string country = dr["countryId"].ToString();
                            string state = dr["stateId"].ToString();
                            string city = dr["cityid"].ToString();


                            if (city != "")
                            {
                                string StrQuery = "";
                                //The state DropDownList contents 
                                StrQuery = "select id,name from tblStatesMaster where countryId='" + country + "' ORDER BY name";
                                dbContext.BindDropDownlist(StrQuery, ref ddl_state);

                                StrQuery = "";
                                //The city DropDownList contents 
                                StrQuery = "select id, name from tblCitiesMaster where stateId='" + state + "' ORDER BY name";
                                dbContext.BindDropDownlist(StrQuery, ref ddl_city);
                                ddl_state.Items.FindByValue(state).Selected = true;
                                ddl_city.Items.FindByValue(city).Selected = true;
                            }
                            else
                            {
                                string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
                                dbContext.BindDropDownlist(StrQuery2, ref ddl_state);
                                ddl_city.Items.Clear();
                                ddl_city.Items.Insert(0, "--Select--");
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //The city DropDownList contents 
            string StrQuery = "select id, name from tblCitiesMaster where stateId='" + ddl_state.SelectedValue + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_city);

        }
        catch (Exception ex)
        {
            Log.Error(Convert.ToString(Session["uid"]) + ex);
        }
    }
}