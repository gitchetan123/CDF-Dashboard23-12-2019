using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class resources_doctree : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                DataTable dt = this.GetData("SELECT id, doc_name,tooltip FROM tblDocDirectory where status='ACTIVE' and p_id=0");
                this.PopulateTreeView(dt, 0, null);
                viewfile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
    {
        try
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["doc_name"].ToString(),
                    Value = row["id"].ToString(),
                    ToolTip = row["tooltip"].ToString(),
                };

                if (parentId == 0)
                {
                    TreeView1.Nodes.Add(child);
                    DataTable dtChild = this.GetData("SELECT id, doc_name,tooltip FROM tblDocDirectory where status='ACTIVE' and p_id= " + child.Value);
                    PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    DataTable dtChild = this.GetData("SELECT id, doc_name,tooltip FROM tblDocDirectory  where status='ACTIVE' and p_id= " + child.Value);
                    if (dtChild.Rows.Count > 0)
                    {
                        treeNode.ChildNodes.Add(child);
                        PopulateTreeView(dtChild, int.Parse(child.Value), child);
                    }
                    else
                    {
                        treeNode.ChildNodes.Add(child);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private DataTable GetData(string query)
    {
        try
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return null;
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            TreeView1.SelectedNode.Expand();

            int id = Convert.ToInt32(TreeView1.SelectedNode.Value.ToString());
            DataTable dt = this.GetData("SELECT ISNULL(preview_path,'NO') as path FROM tblDocDirectory where status='ACTIVE' and id=" + id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (!row["path"].ToString().Equals("NO"))
                {
                    string filepath = ConfigurationManager.AppSettings["docfolderpath"] + row["path"].ToString();
                    viewfile.Visible = true;
                    viewfile.Attributes.Add("src", filepath);
                }
                else
                    viewfile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            viewfile.Visible = false;
        }
    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(TreeView1.SelectedNode.Value.ToString());
            DataTable dt = this.GetData("SELECT ISNULL(path,'NO') as path FROM tblDocDirectory where status='ACTIVE' and id=" + id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (!row["path"].ToString().Equals("NO"))
                {
                    DownloadFile(row["path"].ToString(), true);
                }
                else
                    viewfile.Visible = false;
            }
        }

        catch (System.Threading.ThreadAbortException)
        { }

        catch (Exception ex)
        {
            Log.Error(ex);
            viewfile.Visible = false;
        }
    }

    private void DownloadFile(string fname, bool forceDownload)
    {
        string path = Server.MapPath("~/" + ConfigurationManager.AppSettings["docfoldername"] + fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "";
        // set known types based on file extension  
        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".htm":
                case ".html":
                    type = "text/HTML";
                    break;

                case ".txt":
                    type = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                    type = "Application/msword";
                    break;

                case ".pdf":
                    type = "Application/pdf";
                    break;
            }
        }
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition", "attachment; filename=" + name.Replace(' ', '_'));
        }
        if (type != "")
            Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
    }
}

