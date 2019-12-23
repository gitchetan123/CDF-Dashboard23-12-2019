using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

public partial class pre_moreinfo : System.Web.UI.Page
{
    //create a object Db_context class for database connecton and database related operation
    db_context dc = new db_context();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        // session is less than zero then go to the login page    
        if (!IsPostBack)
        {
            try
            {
                //Check sessions of user
                if (Session["uid"] != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        //Check if user has completed test or not
                        string query = "SELECT COUNT(id) FROM tblUserProductMaster where uId='" + Session["uid"].ToString() + "' and prodid = " + 7 + " and teststatus = 'Complete'";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query, connection);
                        int count = Convert.ToInt32((cmd.ExecuteScalar()));
                        if (count == 0)
                        {
                            div_msg.Visible = true;
                            div_info.Visible = false;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "Please complete your test first";
                        }
                        else
                        {
                            div_msg.Visible = false;
                            //string query_select_cdf_details = "SELECT A.fname, A.lname, A.address, B.ndaStatus, A.status, A.contactNo FROM tblUserMaster as A INNER JOIN tblUserDetails as B ON A.uId = B.uId where A.uId='" + Session["uid"].ToString() + "'";
                            // New query for Agreed Amount.
                            string query_select_cdf_details = "SELECT A.fname, A.lname, A.address, B.ndaStatus, A.status, A.contactNo, C.agreedAmount FROM tblUserMaster as A INNER JOIN tblUserDetails as B ON A.uId = B.uId LEFT OUTER JOIN tblAgreedAmount AS C ON C.uId=A.uId where A.uId='" + Session["uid"].ToString() + "'";
                            cmd = new SqlCommand(query_select_cdf_details, connection);
                            SqlDataReader dr_cdf_details = cmd.ExecuteReader();
                            if (dr_cdf_details.HasRows)
                            {
                                dr_cdf_details.Read();

                                ViewState["fname"] = dr_cdf_details["fname"].ToString();
                                ViewState["contactNo"] = dr_cdf_details["contactNo"].ToString();

                                hid_name.Value = dr_cdf_details.GetString(0) + " " + dr_cdf_details.GetString(1);
                                hid_address.Value = dr_cdf_details.GetString(2);
                                if (dr_cdf_details["status"].ToString() == "APPROVED" && dr_cdf_details["agreedAmount"].ToString() != "")
                                {
                                    if (dr_cdf_details["ndaStatus"].ToString() == "Agree")
                                    {
                                        //check user has alredy agreed the redirect to payment page
                                        div_msg.Visible = true;
                                        div_msg.Attributes["class"] = "alert alert-success";
                                        div_msg.InnerText = "You already updated your Profile";
                                        div_info.Visible = false;
                                    }
                                    else
                                    {
                                        div_info.Visible = true;
                                        //create NDA Form
                                        load_nda();
                                        //Display NDA documnet in Pdf format
                                        em.Attributes["src"] = "../doc/NDA/" + hid_fileName.Value.ToString();
                                    }
                                }
                                
                                else
                                {
                                    if (dr_cdf_details["agreedAmount"].ToString() == "")
                                    {
                                        //check user has alredy agreed the redirect to payment page
                                        div_msg.Visible = true;
                                        div_msg.Attributes["class"] = "alert alert-warning";
                                        div_msg.InnerText = "Please wait until your agreed amount is to confirmed.";
                                        div_info.Visible = false;
                                    }
                                    else
                                    {
                                        div_msg.Visible = true;
                                        div_msg.Attributes["class"] = "alert alert-warning";
                                        div_msg.InnerText = "Please wait until your profile get approved.";
                                        div_info.Visible = false;
                                    }
                                }
                                dr_cdf_details.Close();
                            }
                        }
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
                Response.Redirect("~/login.aspx", false);
            }
        }
    }
    //use templete Data and add to userName 
    private string PopulateBody(string userName)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["NDAEmailTemplatePath"])))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);

        return body;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["uid"] != null)
        {
            try
            {

                if (file_image.PostedFile.ContentLength < 2097152 && file_image2.PostedFile.ContentLength < 2097152) // 2 MB 1024*KB of file size
                {

                    string email = Session["email"].ToString();

                    //Create a PDF Report 

                    /*
                    * Sent a email with PDF attach with attachment
                    * recepientEmail:- email id whoes sent  
                    * subject:- subject data
                    * string body:- Email content 
                    * attachment:- NDA PDF file attachment 
                    * String subject is fatch data to web.confing file
                    * 
                    */

                    string recepientEmail = Session["email"].ToString();
                    string subject = ConfigurationManager.AppSettings["NDAEmailSubject"];
                    //fill email body content to NDAEmailTemplent.htm file 
                    string body = this.PopulateBody(hid_name.Value.ToString());

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(recepientEmail));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = ConfigurationManager.AppSettings["Host"];
                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                        Attachment data = new Attachment(
                        Server.MapPath("~/doc/NDA/" + hid_fileName.Value.ToString()),
                        MediaTypeNames.Application.Octet);
                        mailMessage.Attachments.Add(data);
                        smtp.Send(mailMessage);
                        smtp.Dispose();
                    }

                    //send sms
                    if (ViewState["fname"] != null && ViewState["contactNo"] != null)
                    {
                        string SMSText = ConfigurationManager.AppSettings["NDASmsTemplate"].ToString();
                        SMSText = SMSText.Replace("{CDF}", ViewState["fname"].ToString());
                        dc.sendSms(ViewState["contactNo"].ToString(), SMSText);
                    }

                    //Resume Upload code with rename in user emailid
                    string res = file_resume.FileName;
                    res = res.Substring(res.LastIndexOf('.'));
                    string resfile = email + "_resume_" + Session["uid"].ToString() + res;
                    file_resume.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["resumePath"].ToString() + resfile));

                    //formal_Image Upload code with rename in user emailid+_formal_+id
                    string img = file_image.FileName;
                    img = img.Substring(img.LastIndexOf('.'));
                    string imgfile = email + "_formal_" + Session["uid"].ToString() + img;
                    file_image.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageFormalPath"].ToString() + imgfile));

                    //Casual_Image Upload code with rename in user emailid+_Casual_+id
                    string img2 = file_image2.FileName;
                    img2 = img2.Substring(img2.LastIndexOf('.'));
                    string imgfile2 = email + " _Casual_" + Session["uid"].ToString() + img2;
                    file_image2.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageCasualPath"].ToString() + imgfile2));

                    // update data in table tblUserDetails for CDF-PostRegistration
                    string str = "update tblUserDetails set linkedin ='" + txt_linkedin.Text.Replace("'", "").ToString() + "',facebook = '" + txt_facebook.Text.Replace("'", "").ToString() + "',twitter = '" + txt_twitter.Text.Replace("'", "").ToString() + "',profilePicDisplay = '" + ddl_profilepic.SelectedValue + "',formalImg = '" + imgfile.ToString() + "',casualImg = '" + imgfile2.ToString() + "',ndaStatus = '" + rbl_nda.SelectedValue.Trim() + "',resume = '" + resfile.ToString() + "' where uId = '" + Session["uid"].ToString() + "'";
                    int i = dc.ExecNonQuery(str);

                    div_msg.Visible = true;
                    div_info.Visible = false;
                    div_msg.Attributes["class"] = "alert alert-success";
                    div_msg.InnerText = "Successfully updated your profile";
                }

                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Selected image file size is more than 1 MB. So, please upload it again with less than 1 MB size.";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //hide division msg
        div_msg.Visible = false;
    }

    protected void rbl_nda_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //Create to PFD report code
    private void load_nda()
    {
        try
        {
            // Added new fuction to Read newly added tblAgreedAmount data for accessing CDF Agreed Amount.
            int id = Convert.ToInt32(Session["uid"].ToString());
            int agreedAmount=0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("select uId,agreedAmount,createdDate from tblAgreedAmount where uId=" + id, connection);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    agreedAmount = Convert.ToInt32(sdr.GetValue(1).ToString());

                }
            }

            // Orginal PDF templete pdf file
            string oldfile_name = "NDA.pdf";
            string name = hid_name.Value.ToString();
            string address = hid_address.Value.ToString();

            // create file name as "username + user_id + _NDA.pdf" this format
            hid_fileName.Value = name + Session["uid"].ToString() + "_NDA.pdf";

            //set Template folder path for set a pdf file
            PdfReader pdfReader = new PdfReader(Server.MapPath("~") + "/Templates/" + oldfile_name.ToString());
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(Server.MapPath("~") + "/doc/NDA/" + hid_fileName.Value.ToString(), FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            // set form pdfFormFields
            // The fill the form details 
            pdfFormFields.SetField("txt_date", DateTime.Now.ToString("dd-MMM-yyyy"));

            //set the user name, string one, address, string two as s string
            string s = name + " " + ConfigurationManager.AppSettings["string_one"].ToString() + " " + address + " " + ConfigurationManager.AppSettings["string_two"].ToString();

            pdfFormFields.SetField("txt_name_add", s);
            pdfFormFields.SetField("txt_name", name);
            //pdfFormFields.SetField("txt_name3", name);
            pdfFormFields.SetField("txt_footname", name);
            pdfFormFields.SetField("txt_agreedAmount", agreedAmount.ToString());

            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }


}