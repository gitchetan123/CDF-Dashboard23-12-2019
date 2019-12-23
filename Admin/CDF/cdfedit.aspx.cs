using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class CDF_cdfedit : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    int cdf_id = 0;
    string strcmd = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            div_msg.Visible = false;
            setdata();
        }

    }
    public void setdata()
    {
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    cdf_id = Convert.ToInt32(Request.QueryString["cdf_id"]);
                    if (cdf_id != 0)
                    {
                        strcmd = "SELECT uId, fname, lname, email, dheyaEmail, contactNo, dob, ci.name as city,s.name as state,co.name as country,gender,cdfLevel,userStatus,password FROM " +
                        "tblUserMaster as M left outer join tblCitiesMaster as ci on M.cityid = ci.id left outer join tblStatesMaster as s on ci.stateId = S.id " +
                        "left outer join tblCountriesMaster as co on s.countryId = co.id where uId=" + cdf_id;

                        SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lbl_name.Text = ds.Tables[0].Rows[0]["fname"].ToString() + " " + ds.Tables[0].Rows[0]["lname"].ToString();
                            lbl_email.Text = ds.Tables[0].Rows[0]["email"].ToString();
                            lbl_dheya_email.Text = ds.Tables[0].Rows[0]["dheyaEmail"].ToString();
                            lbl_contact.Text = ds.Tables[0].Rows[0]["contactNo"].ToString();
                            lbl_state.Text = ds.Tables[0].Rows[0]["state"].ToString();
                            lbl_city.Text = ds.Tables[0].Rows[0]["city"].ToString();
                            lbl_gender.Text = ds.Tables[0].Rows[0]["gender"].ToString();

                            if (ds.Tables[0].Rows[0]["dob"].ToString() != "")
                                lbl_dob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"]).ToString("dd/MM/yyyy");
                            lbl_level.Text = ds.Tables[0].Rows[0]["cdfLevel"].ToString();
                            lbl_status.Text = ds.Tables[0].Rows[0]["userStatus"].ToString();
                            lbl_password.Text = ds.Tables[0].Rows[0]["password"].ToString();
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "No record found..!!!";
                            Log.Warn("No record found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong. Please Try again.";
                Log.Error(ex);
            }
        }

    }
}