using ASPSnippets.LinkedInAPI;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class linkedin : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

   
    SqlDataReader dr;
    string strcmd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkedInConnect.APIKey = "75ocntu5nsaxi3";
        LinkedInConnect.APISecret = "OsKtTt519mkX9ItU";
        LinkedInConnect.RedirectUrl = Request.Url.AbsoluteUri.Split('?')[0];
        if (LinkedInConnect.IsAuthorized)
        {
            Button1.Visible = false;

            try
            {
                pnlDetails.Visible = true;
                DataSet ds = LinkedInConnect.Fetch();
                imgPicture.ImageUrl = ds.Tables["person"].Rows[0]["picture-url"].ToString();
                lblName.Text = ds.Tables["person"].Rows[0]["first-name"].ToString();
                lblName.Text += " " + ds.Tables["person"].Rows[0]["last-name"].ToString();
                lblEmailAddress.Text = ds.Tables["person"].Rows[0]["email-address"].ToString();
                lblHeadline.Text = ds.Tables["person"].Rows[0]["headline"].ToString();
                lblIndustry.Text = ds.Tables["person"].Rows[0]["industry"].ToString();
                lblLinkedInId.Text = ds.Tables["person"].Rows[0]["id"].ToString();
                lblLocation.Text = ds.Tables["location"].Rows[0]["name"].ToString();
                imgPicture.ImageUrl = ds.Tables["person"].Rows[0]["picture-url"].ToString();
                lblurl.Text = ds.Tables["person"].Rows[0]["public-profile-url"].ToString();


                string strcmd1 = "select * from tblLinkedin where uid= '" + Convert.ToInt32(Session["uid"]) + "'";
                dr = dbContext.ExecDataReader(strcmd1);
                if (dr.HasRows)
                {
                    strcmd = "update cdf_linkedin set ";
                    strcmd += "link_id='" + ds.Tables["person"].Rows[0]["id"].ToString() + "',first_name='" + ds.Tables["person"].Rows[0]["first-name"].ToString() + "',";
                    strcmd += "last_name='" + ds.Tables["person"].Rows[0]["last-name"].ToString() + "',email_address='" + ds.Tables["person"].Rows[0]["email-address"].ToString() + "',industry='" + ds.Tables["person"].Rows[0]["industry"].ToString() + "',";
                    strcmd += "headline='" + ds.Tables["person"].Rows[0]["headline"].ToString() + "',person_id='" + ds.Tables["person"].Rows[0]["person_id"].ToString() + "',picture_url='" + ds.Tables["person"].Rows[0]["picture-url"].ToString() + "',";
                    strcmd += "public_profile_url='" + ds.Tables["person"].Rows[0]["public-profile-url"].ToString() + "' where uid='" + Convert.ToInt32(Session["uid"]) + "'";

                    div_msg.Attributes["class"] = "alert alert-info";
                    div_msg.InnerText = "Successfully updated Linkedin Profile...";
                }
                else
                {
                    strcmd = "insert into tblLinkedin (uid,link_id,first_name,last_name,email_address,industry,headline,person_id,picture_url,public_profile_url)values ";
                    strcmd += "('" + Convert.ToInt32(Session["uid"]) + "','" + ds.Tables["person"].Rows[0]["id"].ToString() + "','" + ds.Tables["person"].Rows[0]["first-name"].ToString() + "',";
                    strcmd += "'" + ds.Tables["person"].Rows[0]["last-name"].ToString() + "','" + ds.Tables["person"].Rows[0]["email-address"].ToString() + "','" + ds.Tables["person"].Rows[0]["industry"].ToString() + "',";
                    strcmd += "'" + ds.Tables["person"].Rows[0]["headline"].ToString() + "','" + ds.Tables["person"].Rows[0]["person_id"].ToString() + "','" + ds.Tables["person"].Rows[0]["picture-url"].ToString() + "',";
                    strcmd += "'" + ds.Tables["person"].Rows[0]["public-profile-url"].ToString() + "')";

                    div_msg.Attributes["class"] = "alert alert-info";
                    div_msg.InnerText = "Successfully connected with Linkedin...";

                }

                if (strcmd != "")
                {
                    int i = dbContext.ExecNonQuery(strcmd);
                }
                dr.Close();
                //dbContext.ConClose();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                //Response.Write(ex.ToString());
            }
        }
    }

    protected void Authorize(object sender, EventArgs e)
    {
        LinkedInConnect.Authorize();
    }
}