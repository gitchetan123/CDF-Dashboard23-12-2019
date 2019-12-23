using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sale_sell_form : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();
    int j = 0;
    int i = 0;
    string uId = null, userSource = null;
    SqlCommand cmd = null;
    int isLead = 0, IsContact = 0, IsCase = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["email"] != null)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Redirect("login.aspx", false);
        }

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Product"] = ddl_product.SelectedItem.Text;
            //if (rblLead.Checked == true)
            //{ isLead = 1; IsContact = IsCase = 0; }
            //else if (rblContact.Checked == true)
            //{ IsContact = 1; isLead = IsCase = 0; }
            //else if (rblCase.Checked == true)
            //{ IsCase = 1; IsContact = isLead = 0; }
            IsCase = 1;

            //if (rblLead.Checked == true || rblContact.Checked == true || rblCase.Checked == true)
            //{

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd2 = null;
                con.Open();
                if (ddl_leadcategory.SelectedValue == "5" || ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "6")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where userTypeId in (4,5,6) and email='" + txt_email.Text + "'", con);
                }
                if (ddl_leadcategory.SelectedValue == "2")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=2 and email='" + txt_email.Text + "'", con);
                }
                if (ddl_leadcategory.SelectedValue == "12")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=12 and email='" + txt_email.Text + "'", con);
                }

                cmd2.CommandType = System.Data.CommandType.Text;
                int c = Convert.ToInt32(cmd2.ExecuteScalar());
                if (c == 0)
                {
                    //if (ddl_leadcategory.SelectedValue == "12")
                    //{
                    //    string str = "select count(id) from tblVerifyRegistration where email='" + txt_email.Text.Trim() + "'";
                    //    cmd2 = new SqlCommand(str, con);
                    //    int countuser = Convert.ToInt32(cmd2.ExecuteScalar());
                    //    if (countuser == 0)
                    //    {
                    //        string strcmd1 = "insert into tblVerifyRegistration (email,executiveId,createDate,status,userType)  values(@email, @executiveId, @createDate, @status,@userType)";
                    //        cmd2 = new SqlCommand(strcmd1, con);
                    //        cmd2.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                    //        cmd2.Parameters.AddWithValue("@executiveId", 1);
                    //        cmd2.Parameters.AddWithValue("@createDate", DateTime.Now);
                    //        cmd2.Parameters.AddWithValue("@status", "ACTIVE");
                    //        cmd2.Parameters.AddWithValue("@userType", 12);
                    //        int count = cmd2.ExecuteNonQuery();
                    //    }
                    //}
                    //else
                    //{
                    SqlCommand cmd1 = new SqlCommand("dsp_Insert_PreUser_tblUserMaster", con);
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ddl_leadcategory.SelectedValue));
                    cmd1.Parameters.AddWithValue("@fname", txt_firstname.Text);
                    cmd1.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd1.Parameters.AddWithValue("@contactNo", txt_contact.Text);
                    cmd1.Parameters.AddWithValue("@userStatus", "INACTIVE");
                    if (ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "5")
                    { userSource = "DHEYA-DASHBOARD"; }
                    if (ddl_leadcategory.SelectedValue == "6")
                    { userSource = "DHEYA-Professional"; }
                    if (ddl_leadcategory.SelectedValue == "12")
                    { userSource = "Business Partnet-Career Product Advisor"; }
                    cmd1.Parameters.AddWithValue("@userSource", userSource);
                    // Get Max Id (last created uId)
                    cmd1.Parameters.Add("@id", SqlDbType.VarChar, 50);
                    cmd1.Parameters["@id"].Direction = ParameterDirection.Output;

                    SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd1.Parameters.Add(parm);


                    int j = cmd1.ExecuteNonQuery();
                    if (j > 0)
                    {
                        int id = Convert.ToInt32(parm.Value);
                        uId = cmd1.Parameters["@id"].Value.ToString();
                    }
                    else
                    {
                        Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
                    }

                    //}

                    SqlCommand cmd = new SqlCommand("dsp_insert_referraldetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
                    cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@City", txt_city.Text);
                    cmd.Parameters.AddWithValue("@Contact", txt_contact.Text);
                    cmd.Parameters.AddWithValue("@Program", ddl_product.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@IsLead", isLead);
                    cmd.Parameters.AddWithValue("@IsContact", IsContact);
                    cmd.Parameters.AddWithValue("@IsCase", IsCase);
                    cmd.Parameters.AddWithValue("@PinCode", "");
                    cmd.Parameters.AddWithValue("@LeadCategory", ddl_leadcategory.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
                    cmd.Parameters.AddWithValue("@ReferByEmail", Session["email"].ToString());
                    cmd.Parameters.AddWithValue("@referAs", ddl_leadcategory.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Status", "");
                    cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(Session["product_amount"]));
                    cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ddl_product.SelectedValue));
                    cmd.Parameters.AddWithValue("@uId", Convert.ToInt32(uId));
                    cmd.Parameters.AddWithValue("@Discription", "");
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        string str2 = "INSERT INTO tblRelation (uId,referral_uId) VALUES ('" + Convert.ToInt32(uId) + "','" + Convert.ToInt32(Session["uid"]) + "')";
                        cmd = new SqlCommand(str2, con);
                        int m = cmd.ExecuteNonQuery();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerHtml = "Sell registered successfully.";
                        //Response.Redirect("referral_success.aspx", false);
                    }
                    else
                    {
                        Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
                    }

                    GridView1.DataSource = BindGridView();
                    GridView1.DataBind();
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "email already exist";

                }
            }
            //}
            //else
            //{
            //    Response.Write("<script type = 'text/javascript'>alert('Please select any of the referral type.');</script>");
            //}
        }
        catch (Exception ex)
        {
            Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
        }
    }

    //protected void btn_submit_Click(object sender, EventArgs e)
    //{
    //    Session["Product"] = ddl_product.SelectedItem.Text;
    //    int isLead, IsContact, IsCase ;
    //    isLead = IsContact = IsCase = 0;
    //    if (ddl_category.SelectedItem.Text == "Lead")
    //    { isLead = 1; IsContact = IsCase = 0; }
    //    else if (ddl_category.SelectedItem.Text == "Contact")
    //    { IsContact= 1; isLead = IsCase = 0; }
    //    else if (ddl_category.SelectedItem.Text == "Case")
    //    { IsCase = 1; IsContact = isLead = 0; }

    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(connectionString))
    //        {
    //            SqlCommand cmd = new SqlCommand("dsp_insert_referraldetails", con);
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

    //            cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
    //            cmd.Parameters.AddWithValue("@Email", txt_email.Text);
    //            cmd.Parameters.AddWithValue("@City", txt_city.Text);
    //            cmd.Parameters.AddWithValue("@Contact", txt_contact.Text);
    //            cmd.Parameters.AddWithValue("@Program", ddl_product.SelectedItem.Text);
    //            cmd.Parameters.AddWithValue("@IsLead", isLead);
    //            cmd.Parameters.AddWithValue("@IsContact", IsContact);
    //            cmd.Parameters.AddWithValue("@IsCase", IsCase);
    //            cmd.Parameters.AddWithValue("@PinCode", txt_pin.Text);
    //            cmd.Parameters.AddWithValue("@LeadCategory", ddl_leadcategory.SelectedItem.Text);
    //            cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
    //            cmd.Parameters.AddWithValue("@ReferByEmail", Session["email"].ToString());
    //            cmd.Parameters.AddWithValue("@referAs", ddl_leadcategory.SelectedItem.Text);
    //            cmd.Parameters.AddWithValue("@Status", "");
    //            cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(Session["product_amount"]));
    //            cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ddl_product.SelectedValue));

    //            con.Open();
    //            SqlCommand cmd2 = new SqlCommand("select count(uId) from tblUserMaster where email='"+ txt_email.Text + "'", con);
    //            cmd2.CommandType = System.Data.CommandType.Text;
    //            int c = Convert.ToInt32(cmd2.ExecuteScalar());
    //            if (c == 0)
    //            {
    //                int i = cmd.ExecuteNonQuery();
    //                if (i > 0)
    //                {
    //                    SqlCommand cmd1 = new SqlCommand("dsp_Insert_PreUser_tblUserMaster", con);
    //                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
    //                    cmd1.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ddl_leadcategory.SelectedValue));
    //                    cmd1.Parameters.AddWithValue("@fname", txt_firstname.Text);
    //                    cmd1.Parameters.AddWithValue("@email", Session["email"].ToString());
    //                    cmd1.Parameters.AddWithValue("@contactNo", txt_contact.Text);
    //                    cmd1.Parameters.AddWithValue("@userStatus", "INACTIVE");

    //                    // Get Max Id (last created uId)
    //                    cmd1.Parameters.Add("@id", SqlDbType.VarChar, 50);
    //                    cmd1.Parameters["@id"].Direction = ParameterDirection.Output;

    //                    SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
    //                    parm.Direction = ParameterDirection.ReturnValue;
    //                    cmd1.Parameters.Add(parm);


    //                    int j = cmd1.ExecuteNonQuery();
    //                    if (j > 0)
    //                    {
    //                        int id = Convert.ToInt32(parm.Value);
    //                        string uId = cmd1.Parameters["@id"].Value.ToString();

    //                        Response.Redirect("referral_success.aspx", false);
    //                    }
    //                    else
    //                    { Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>"); }
    //                }
    //                else
    //                { Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>"); }
    //                GridView1.DataSource = BindGridView();
    //                GridView1.DataBind();
    //            }
    //            else
    //            { Response.Write("<script type = 'text/javascript'>alert('email already exist');</script>"); }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
    //    }       
    //}

    protected DataTable BindGridView()
    {
        try
        {
            string strcmd = "SELECT [FirstName], [LastName] ,[City],[Contact],[Program],[Email],[PinCode]," +
                "CASE  WHEN IsLead = 1 AND IsCase = 0 AND IsContact = 0 Then 'Lead' WHEN IsLead = 0 AND IsCase = 1 AND IsContact = 0 Then 'Case' " +
                "WHEN IsLead = 0 AND IsCase = 0 AND IsContact = 1 Then 'Contact' END As 'Category'," +
                "[LeadCategory],[ReferByEmail] From [dbo].[tblReferralDetail] where ReferByEmail='" + Session["email"].ToString() + "' ";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.txt_firstname.Text = "";
        this.txt_firstname.Text = "";
        this.txt_email.Text = "";
        this.txt_contact.Text = "";
        this.txt_city.Text = "";
        //this.txt_pin.Text = "";

        this.ddl_product.SelectedValue = "Select an Option";
        this.ddl_leadcategory.SelectedValue = "Select an Option";
    }

    protected void ddl_product_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddl_product.SelectedItem.Text == "Dheya Discover (5999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Compass(9999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Nevigator (19999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Parent Workshop")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Early Career (15000) - [suitable for age 25-30]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Mid Career (20000) - [suitable for age above 35]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CDF Training (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CPA (15000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Center (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Cafe (300000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }

        //if (ddl_product.SelectedValue == "1")
        //{ Session["product_amount"] = 5999; }
        //if (ddl_product.SelectedValue == "2")
        //{ Session["product_amount"] = 9999; }
        //if (ddl_product.SelectedValue == "3")
        //{ Session["product_amount"] = 19999; }
        //if (ddl_product.SelectedValue == "4")
        //{ Session["product_amount"] = 20000; }
        //if (ddl_product.SelectedValue == "5")
        //{ Session["product_amount"] = 0; }
        //if (ddl_product.SelectedValue == "6")
        //{ Session["product_amount"] = 0; }
        //if (ddl_product.SelectedValue == "7")
        //{ Session["product_amount"] = 20000; }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = BindGridView();
        GridView1.DataBind();
    }

    //protected void rblLead_CheckedChanged(object sender, EventArgs e)
    //{
    //    div_msg.Visible = true;
    //    div_msg.Attributes["class"] = "alert alert-success";
    //    div_msg.InnerHtml = "You Know the Client, They also know you and You have discussed about Dheya Programs. They are waiting for Dheya's Call";
    //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_lead();", true);
    //}
    //protected void rblContact_CheckedChanged(object sender, EventArgs e)
    //{
    //    div_msg.Visible = true;
    //    div_msg.Attributes["class"] = "alert alert-success";
    //    div_msg.InnerHtml = "You Know the Client but have not discussed about Dheya Programs.";
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_contact();", true);
    //}
    //protected void rblCase_CheckedChanged(object sender, EventArgs e)
    //{
    //    div_msg.Visible = true;
    //    div_msg.Attributes["class"] = "alert alert-success";
    //    div_msg.InnerHtml = "You have discussed about dheya and they are ready to buy the product.";
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_case();", true);
    //}

    protected void ddl_leadcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_leadcategory.SelectedValue == "5")
        {
            string StrQueryExe = "select * from tblUserProductRelation where Student=1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "4")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Parent = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "6")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Professional = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "12")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Business = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "School")
        {
            Response.Redirect("referral1.aspx", false);
        }
        if (ddl_leadcategory.SelectedValue == "College")
        {
            Response.Redirect("referral1.aspx", false);
        }
        if (ddl_leadcategory.SelectedValue == "Corporate")
        {
            Response.Redirect("referral1.aspx", false);
        }
    }
}