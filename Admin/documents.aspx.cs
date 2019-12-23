using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_documents : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);  
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            div_msg.Visible = false;
            div_parent.Visible = false;
            try
            {
                ddl_parent.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query1 = "SELECT id, doc_name FROM tblDocDirectory where p_id=0";
                    SqlDataAdapter da = new SqlDataAdapter(query1, con);
                    DataSet ds1 = new DataSet();
                    da.Fill(ds1);

                    ddl_parent.DataSource = ds1.Tables[0];
                    ddl_parent.DataValueField = ds1.Tables[0].Columns[0].ToString();
                    ddl_parent.DataTextField = ds1.Tables[0].Columns[1].ToString();
                    ddl_parent.DataBind();

                    ddl_parent.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
                    ddl_parent.Items.Add(new ListItem("Other", "Other"));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                string filename = "";
                string filename2 = "";
                if (fileUpload1.HasFile && fileUpload2.HasFile)
                {
                    string filePath = "";
                    string filePath2 = "";

                    filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                    filePath = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename));
                    if (File.Exists(filePath))
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Preview file is already exist";
                    }
                    else
                    {
                        filename2 = Path.GetFileName(fileUpload2.PostedFile.FileName);
                        filePath2 = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename2));
                        if (File.Exists(filePath2))
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "File (document download file) which you want to upload is already exist";
                        }
                        else
                        {
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {

                                fileUpload1.SaveAs(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename));
                                fileUpload2.SaveAs(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename2));

                                string dname = txt_docname.Text;
                                string pname = ddl_parent.SelectedValue.ToString();
                                string tooltip = txt_tooltip.Text;
                                string status = ddl_status.SelectedValue.ToString();

                                SqlCommand cmd = new SqlCommand("insert into tblDocDirectory(doc_name,p_id,path,tooltip,status,preview_path) values(@docname,@parentid,@Path,@Tooltip,@Status,@Preview)", con);
                                cmd.Parameters.AddWithValue("@docname", dname);
                                cmd.Parameters.AddWithValue("@parentid", pname);
                                cmd.Parameters.AddWithValue("@Tooltip", tooltip);
                                cmd.Parameters.AddWithValue("@Path", "" + filename);
                                cmd.Parameters.AddWithValue("@Status", status);
                                cmd.Parameters.AddWithValue("@Preview", "" + filename2);
                                con.Open();
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-success";
                                    div_msg.InnerText = "File successfully uploaded";

                                    grid_doc.DataBind();
                                }
                            }
                        }
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Please select file";
                }
                cleardata();
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Please enter valid data";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);

            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong";
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string filename = "";
                    string filename2 = "";
                    if (fileUpload1.HasFile && fileUpload2.HasFile)
                    {
                        filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                        fileUpload1.SaveAs(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename));

                        filename2 = Path.GetFileName(fileUpload2.PostedFile.FileName);
                        fileUpload2.SaveAs(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename2));

                        string dname = txt_docname.Text;
                        string pname = ddl_parent.SelectedValue.ToString();
                        string tooltip = txt_tooltip.Text;
                        string status = ddl_status.SelectedValue.ToString();
                        SqlCommand cmd = new SqlCommand("update tblDocDirectory set doc_name=@docname,p_id=@parentid,path=@Path,tooltip=@Tooltip,status=@Status,preview_path=@Preview where id=@id", con);
                        cmd.Parameters.AddWithValue("@docname", dname);
                        cmd.Parameters.AddWithValue("@parentid", pname);
                        cmd.Parameters.AddWithValue("@Tooltip", tooltip);
                        cmd.Parameters.AddWithValue("@Path", "" + filename);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@id", hf_id.Value);
                        cmd.Parameters.AddWithValue("@Preview", "" + filename2);
                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerText = "File and data successfully updated";
                            grid_doc.DataBind();
                        }
                    }
                    else
                    {
                        string dname = txt_docname.Text;
                        string pname = ddl_parent.SelectedValue.ToString();
                        string tooltip = txt_tooltip.Text;
                        string status = ddl_status.SelectedValue.ToString();
                        SqlCommand cmd = new SqlCommand("update tblDocDirectory set doc_name=@docname,p_id=@parentid,tooltip=@Tooltip,status=@Status where id=@id", con);
                        cmd.Parameters.AddWithValue("@docname", dname);
                        cmd.Parameters.AddWithValue("@parentid", pname);
                        cmd.Parameters.AddWithValue("@Tooltip", tooltip);
                        //cmd.Parameters.AddWithValue("@Path", "" + filename);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@id", hf_id.Value);
                        //cmd.Parameters.AddWithValue("@Preview", "" + filename2);
                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerText = "Update successfully updated";
                            grid_doc.DataBind();
                        }

                    }

                    cleardata();
                }
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Please enter valid data";

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong";
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        cleardata();
        div_msg.Visible = false;
    }
    protected void grid_doc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hf_id.Value = grid_doc.SelectedValue.ToString();
            txt_docname.Text = grid_doc.SelectedRow.Cells[2].Text;
            ddl_parent.SelectedValue = grid_doc.SelectedRow.Cells[3].Text;
            txt_tooltip.Text = grid_doc.SelectedRow.Cells[6].Text;
            ddl_status.SelectedValue = grid_doc.SelectedRow.Cells[7].Text;

            btnUpload.Enabled = false;
            btn_update.Enabled = true;
            btn_delete.Enabled = true;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    void cleardata()
    {
        txt_docname.Text = "";
        txt_tooltip.Text = "";
        ddl_parent.SelectedValue = "-- Select --";
        ddl_status.SelectedValue = "ACTIVE";
        btnUpload.Enabled = true;
        btn_update.Enabled = false;
        btn_delete.Enabled = false;
        div_parent.Visible = false;
    }
    protected void ddl_parent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_parent.SelectedItem.Text == "Other")
            {
                div_parent.Visible = true;
                div_rows.Visible = false;
                grid_doc.Visible = false;
            }
            else
            {
                div_parent.Visible = false;
                div_rows.Visible = true;
                grid_doc.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_addparent_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                string newparent = txt_newparent.Text;
                string status = ddl_status.SelectedValue.ToString();
                if (newparent != "")
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("insert into tblDocDirectory (doc_name,p_id,status) values(@docname,0,@Status)", con);
                        cmd.Parameters.AddWithValue("@docname", newparent);
                        cmd.Parameters.AddWithValue("@Status", status);
                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerText = "Parent Added successfully";
                        }

                        div_parent.Visible = false;
                        div_rows.Visible = true;
                        grid_doc.Visible = true;
                        Response.Redirect("documents.aspx");
                    }
                }
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Please enter valid data";

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong in adding parent";

        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string filename = "";
                string filename2 = "";

                string str = " select path, preview_path from tblDocDirectory where id=@id";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", hf_id.Value);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    filename = dr["path"].ToString();
                    filename2 = dr["preview_path"].ToString();
                }
                dr.Close();

                string filePath = "";
                string filePath2 = "";

                //filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                filePath = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // filename2 = Path.GetFileName(fileUpload2.PostedFile.FileName);
                filePath2 = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + filename2));
                if (File.Exists(filePath2))
                {
                    File.Delete(filePath2);
                }

                str = "delete tblDocDirectory where id=@id";
                SqlCommand cmd2 = new SqlCommand(str, con);
                cmd2.Parameters.AddWithValue("@id", hf_id.Value);
                if (cmd2.ExecuteNonQuery() > 0)
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-success";
                    div_msg.InnerText = "Successfully Deleted ...";

                    grid_doc.DataBind();
                    cleardata();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}