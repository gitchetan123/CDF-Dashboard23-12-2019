//********************************************************************************************
//PageName        : UpdateInfo   
//Description     : This page is Edit and update user own information 
//AddedBy         : Bahubali                   AddedOn   : **/**/2017
//UpdatedBy       : Bahubali                   UpdatedOn :
//Reason          : 
//*******************************************************************************************

using System;
using System.Data.SqlClient;
using log4net;
using System.Configuration;
using System.Data;

public partial class Admin_UpdateInfo : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {

        FilteredTextBoxExtender10.ValidChars = FilteredTextBoxExtender10.ValidChars + "\r\n";
        //rfDob.Enabled = false;
        //rfGender.Enabled = false;
        //rfState.Enabled = false;
        //rfCity.Enabled = false;
        if (!IsPostBack)
        {
            try
            {
                // New
                get_Dropdown();
                string Type = Session["type"].ToString();
                if (Type == "SuperAdmin")
                {
                    moreInfo.Visible = true;
                    txt_password.ReadOnly = false;
                }
                ///////
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (id != 0)
                {
                    hd_id.Value = id.ToString();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string StrQuery = "";
                        //Get all user details 
                        //StrQuery = "SELECT tblUserMaster.uId, tblUserMaster.fname, tblUserMaster.lname, tblUserMaster.contactNo, tblUserMaster.address, tblUserMaster.gender, tblUserMaster.dob, "
                        //+ "tblCountriesMaster.id AS countryId, tblStatesMaster.id AS stateId, tblUserMaster.cityid,tblUserMaster.email,tblUserMaster.cdfLevel, "
                        //+ "tblUserDetails.aboutSelf,tblUserDetails.refundStatus,tblUserDetails.refundAmount,tblUserDetails.classification,UB.accountHolderName,UB.accountNumber,UB.bankName,UB.branchName,UB.ifscNo FROM tblCitiesMaster right outer JOIN tblUserMaster INNER JOIN "
                        //+ "tblUserDetails ON tblUserMaster.uId = tblUserDetails.uId ON tblCitiesMaster.id = tblUserMaster.cityid full outer JOIN "
                        //+ "tblStatesMaster full outer JOIN tblCountriesMaster ON tblStatesMaster.countryId = tblCountriesMaster.id ON tblCitiesMaster.stateId = tblStatesMaster.id "
                        //+ "LEFT Outer Join tblUserBankDetails as UB ON tblUserMaster.uId = UB.uId WHERE (tblUserMaster.uId = '" + id + "')";

                        // Added fieldOfWork, modeOfWork, industrySector
                        StrQuery = "SELECT tblUserMaster.uId, tblUserMaster.fname, tblUserMaster.lname ,tblUserMaster.password, tblUserMaster.contactNo, tblUserMaster.address, "
                                    + " tblUserMaster.gender, tblUserMaster.dob, tblCountriesMaster.id AS countryId, tblStatesMaster.id AS stateId,  "
                                    + " tblUserMaster.cityid,tblUserMaster.email,tblUserMaster.cdfLevel, tblUserDetails.aboutSelf,tblUserDetails.refundStatus, "
                                    + " tblUserDetails.refundAmount,tblUserDetails.classification,UB.accountHolderName,UB.accountNumber,UB.bankName,UB.branchName,UB.ifscNo , "
                                    + " tblUserDetails.fieldOfWork,tblUserDetails.modeOfWork, tblUserDetails.industrySector "
                                    + " FROM tblCitiesMaster right outer JOIN tblUserMaster INNER JOIN tblUserDetails ON tblUserMaster.uId = tblUserDetails.uId ON "
                                    + " tblCitiesMaster.id = tblUserMaster.cityid full outer JOIN tblStatesMaster full outer JOIN tblCountriesMaster "
                                    + "  ON tblStatesMaster.countryId = tblCountriesMaster.id ON tblCitiesMaster.stateId = tblStatesMaster.id LEFT Outer Join tblUserBankDetails "
                                    + " as UB ON tblUserMaster.uId = UB.uId WHERE(tblUserMaster.uId = '" + id + "')";

                        //pass a datareder dr object to ExecDataReader with query
                        SqlCommand cmd = new SqlCommand(StrQuery, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        //  If user's data present in database
                        if (dr.HasRows)
                        {
                            dr.Read();
                            //fill details in 
                            txt_fname.Text = dr["fname"].ToString();
                            txt_lname.Text = dr["lname"].ToString();
                            txt_password.Text = dr["password"].ToString();
                            txt_contact.Text = dr["contactNo"].ToString();
                            txt_address.Text = dr["address"].ToString();
                            txt_refamt.Text = dr["refundAmount"].ToString();
                            ddl_refsts.SelectedValue = dr["refundStatus"].ToString();
                            txt_email.Text = dr["email"].ToString();
                            txt_accountHolderName.Text = dr["accountHolderName"].ToString();
                            txt_accountNumber.Text = dr["accountNumber"].ToString();
                            txt_bankName.Text = dr["bankName"].ToString();
                            txt_branchName.Text = dr["branchName"].ToString();
                            txt_ifscNo.Text = dr["ifscNo"].ToString();
                            ddl_classification.SelectedValue = dr["classification"].ToString();

                            ddl_fieldOfWork.SelectedValue = dr["fieldOfWork"].ToString(); // New
                            ddl_modeOfWork.SelectedValue = dr["modeOfWork"].ToString(); // New
                            ddl_industrySector.SelectedValue = dr["industrySector"].ToString(); // New

                            if (txt_email.Text == "")
                                txt_email.Text = "dummy@dummy.com";

                            txt_profile.Text = dr["aboutSelf"].ToString();


                            if (dr["cdfLevel"].ToString() != "" || dr["cdfLevel"] != null)
                            {
                                ddl_cdfLevel.SelectedValue = dr["cdfLevel"].ToString();
                            }

                            ddl_gender.SelectedValue = dr["gender"].ToString();

                            if (dr["dob"].ToString() != "")
                            {
                                DateTime dt = Convert.ToDateTime(dr["dob"]);
                                tbDate1.Text = dt.ToString("dd/MM/yyyy").Replace('-', '/');
                            }

                            dr["uId"].ToString();

                            string country = dr["countryId"].ToString();
                            string state = dr["stateId"].ToString();
                            string city = dr["cityid"].ToString();

                            if (city != "")
                            {
                                StrQuery = "";
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
                Log.Error(Convert.ToString(Session["user_email"]) + ex);
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong on form loading. Please Try again.";
            }
        }
    }

    private void get_Dropdown()
    {
        try
        {
            string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery2, ref ddl_state);
            ddl_city.Items.Clear();
            ddl_city.Items.Insert(0, "--Select--");

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        div_msg.InnerText = "";
        div_msg.Attributes["class"] = "alert";
        ////check user mode edit or update
        //if (btn_submit.Text == "Edit")
        //{
        //   // pan_info.Enabled = true;
        //   // btn_submit.Text = "Update";
        //    //rfDob.Enabled = true;
        //    //rfGender.Enabled = true;
        //    //rfState.Enabled = true;
        //    //rfCity.Enabled = true;
        //}
        //else
        //{
        //  //  pan_info.Enabled = false;
        //   // btn_submit.Text = "Edit";

        try
        {
            if (txt_email.Text != "dummy@dummy.com")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string date = dbContext.DateConvert(tbDate1.Text.Trim().ToString());
                    string refAmount = "0";
                    if (txt_refamt.Text != "" && ddl_refsts.SelectedValue == "Yes")
                    {
                        refAmount = txt_refamt.Text;
                    }

                    string StrQuery = "";
                    //update details in tblUserMaster table
                    StrQuery = "update tblUserMaster set fname='" + txt_fname.Text + "', lname='" + txt_lname.Text + "', password= '"+txt_password.Text+"',contactNo='" + txt_contact.Text + "',"
                    + "address='" + txt_address.Text + "',cityid='" + ddl_city.SelectedValue + "',gender='" + ddl_gender.SelectedValue + "',dob='" + date + "',email='" + txt_email.Text + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) ";
                    if (ddl_cdfLevel.Text != "Select")
                    {
                        StrQuery += ",cdfLevel='" + ddl_cdfLevel.SelectedValue + "' ";
                    }
                    StrQuery += "where uId='" + hd_id.Value + "'";
                    SqlCommand cmd = new SqlCommand(StrQuery, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    //update details in tblUserDetails table
                    StrQuery = "";
                    //StrQuery = "update tblUserDetails set aboutSelf='" + txt_profile.Text + "', refundStatus='" + ddl_refsts.SelectedValue + "', refundAmount= '"+ refAmount + "', classification= '"+ ddl_classification.SelectedValue + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + hd_id.Value + "'";
                    // New
                    StrQuery = "update tblUserDetails set fieldOfWork='" + ddl_fieldOfWork.SelectedItem.Text + "', modeOfWork='" + ddl_modeOfWork.SelectedItem.Text + "', industrySector='" + ddl_industrySector.SelectedItem.Text + "', aboutSelf='" + txt_profile.Text + "', refundStatus='" + ddl_refsts.SelectedValue + "', refundAmount= '" + refAmount + "', classification= '" + ddl_classification.SelectedValue + "',modifiedBy='" + Session["adminuser_id"] + "', dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) where uId='" + hd_id.Value + "'";
                    cmd = new SqlCommand(StrQuery, con);
                    int j = cmd.ExecuteNonQuery();

                    //if update success is show msg
                    if (i != 0 && j != 0)
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerText = "Updated Successfully.......";
                    }
                }
            }
            else
            {
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "dummy@dummy.com is not valid emailid. Please enter valid emailid.";
            }
        }
        catch (Exception ex)
        {
            Log.Error(Convert.ToString(Session["user_email"]) + ex);
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
        // }
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
            Log.Error(ex);
        }
    }



}