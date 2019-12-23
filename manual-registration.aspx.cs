using log4net;
using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class manual_registration : System.Web.UI.Page
{
    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FilteredTextBoxExtender10.ValidChars = FilteredTextBoxExtender10.ValidChars + "\r\n";
            if (Session["uid"] != null)
            {
                if (!IsPostBack)
                {
                    div_msg.Visible = false;
                    txtEndDate.Text = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
                    if (!IsPostBack)
                    {
                        div_msg.Visible = false;
                        try
                        {
                            string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
                            dbContext.BindDropDownlist(StrQuery2, ref ddl_state);
                            ddl_city.Items.Clear();
                            ddl_city.Items.Insert(0, "--Select--");

                            string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE'";
                            dbContext.BindDropDownlist(StrQueryExe, ref ddl_ename);

                            txt_email.Text = Session["email"].ToString();
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
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                if (Session["uid"] != null)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        SqlCommand command;
                        string str;
                        // check table tblUserMaster email id is not exist 
                        connection.Open();
                        string email1 = Session["dheyaEmail"].ToString();
                        //  If user's data not present in database then user will redirect to Reg.aspx page
                        command = connection.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = connection.BeginTransaction("RegisterTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {
                            string date = dataContext.DateConvert(tbDate1.Text.Trim().ToString());
                            str = "";

                            // Insert user's data into tblUserMaster table
                            str = "Update tblUserMaster set fname=@fname,lname=@lname,contactNo=@contactNo,email=@email,gender=@gender,dob=@dob,cityid=@cityid,address=@address,regDateTime=@regDateTime,status=@status,userStatus=@userStatus where uid=@uid";

                            command.Parameters.AddWithValue("@fname", txt_fname.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@lname", txt_lname.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@contactNo", txt_contact.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@email", txt_email.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@gender", ddl_gender.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@dob", date);
                            command.Parameters.AddWithValue("@cityid", ddl_city.SelectedValue);
                            command.Parameters.AddWithValue("@address", txt_address.Text.Trim().ToString());
                            command.Parameters.AddWithValue("@regDateTime", DateTime.Now);
                            command.Parameters.AddWithValue("@status", "manual_Reg_Complete");
                            command.Parameters.AddWithValue("@userStatus", "ACTIVE");
                            command.Parameters.AddWithValue("@uId", Session["uid"].ToString());

                            command.CommandText = str;
                            int i = command.ExecuteNonQuery();
                            if (i > 0)
                            {
                                //formal_Image Upload code with rename in user emailid+_formal_+id
                                string img = file_image.FileName;
                                img = img.Substring(img.LastIndexOf('.'));
                                string imgfile = email1 + "_formal_" + Session["uid"].ToString() + img;
                                file_image.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageFormalPath"].ToString() + imgfile));

                                //Casual_Image Upload code with rename in user emailid+_Casual_+id
                                string img2 = file_image2.FileName;
                                img2 = img2.Substring(img2.LastIndexOf('.'));
                                string imgfile2 = email1 + " _Casual_" + Session["uid"].ToString() + img2;
                                file_image2.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageCasualPath"].ToString() + imgfile2));

                                str = "";
                                // Insert user id & executive id into tblRelation table
                                string str1 = "update tblUserDetails set qualification=@qualification,whyThisOpp=@whyThisOpp,designation=@designation,maritalstatus=@maritalstatus,spouseName=@spouseName,childrenAge=@childrenAge,aboutSelf=@aboutSelf where uid=@uid";

                                command.Parameters.AddWithValue("@qualification", txt_qualification.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@whyThisOpp", txt_why_opportunity.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@designation", txt_designation.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@maritalstatus", ddl_married_status.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@spouseName", txt_spouse.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@childrenAge", txt_children.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@aboutSelf", txt_profile.Text.Trim().ToString());
                                command.Parameters.AddWithValue("@formalImg", imgfile.ToString());
                                command.Parameters.AddWithValue("@casualImg", imgfile2.ToString());

                                command.CommandText = str1;
                                int k = command.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    transaction.Commit();
                                    Session["userName"] = txt_fname.Text.Trim().ToString() + " " + txt_lname.Text.Trim().ToString();
                                    Session["email"] = txt_email.Text.Trim().ToString();
                                    Session["status"] = "manual_Reg_Complete";
                                    Session["formalImg"] = "~/doc/formalImg/" + imgfile.ToString();
                                    Session["casualImg"] = "~/doc/images/" + imgfile2.ToString();
                                    Response.Redirect("~/home.aspx",false);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";
                            }
                        }

                        catch (Exception ex)
                        {

                            Log.Error(ex);
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";

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
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Rollback Error";
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/login.aspx",false);
                }
            }
            //If user's data already present in database then user will redirect to NDA.aspx page
            else
            {
                // Response.Redirect("~/Login.aspx", false);
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Your emailid is already registered...";
                div_msg.Visible = true;
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
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        clear();
    }

    //Clear all field 
    public void clear()
    {
        txt_fname.Text = "";
        txt_lname.Text = "";
        txt_contact.Text = "";
        //txt_email.Text = "";
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