using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Search_CourseDetail : System.Web.UI.Page
{

    db_context_career clsdal = new db_context_career();
    //int c_id;
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if (Session["type"] == null)
        {
            this.MasterPageFile = "~/CDFMaster.master";
        }
        else
        {
            this.MasterPageFile = "~/Admin/admin-master.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string query = "select distinct zone from tbl_institute order by zone ASC";
                DataSet dsa = clsdal.ExecDataSet(query);
                DropDownList1.DataSource = dsa.Tables[0];
                DropDownList1.DataTextField = dsa.Tables[0].Columns[0].ToString();
                DropDownList1.DataValueField = dsa.Tables[0].Columns[0].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Select", "Null"));
                DropDownList1.SelectedIndex = 0;

                for (int i = 0; i <= DropDownList1.Items.Count - 1; i++)
                {
                    DropDownList1.Items[i].Attributes.Add("Title", DropDownList1.Items[i].Text);
                }


                //string strcmd = "SELECT * FROM tbl_course_master Where co_id = '" + Request.QueryString["id"].ToString() + "'";
                string strcmd = "SELECT * FROM tbl_newcourse_master Where co_id = '" + Request.QueryString["id"].ToString() + "'";
                DataSet ds = clsdal.ExecDataSet(strcmd);
                //lblCareerName.Text = ds.Tables[0].Rows[0][1].ToString();
                //// lblCareername2.Text = ds.Tables[0].Rows[0][1].ToString();
                //course2Label.Text = ds.Tables[0].Rows[0][2].ToString();
                //course3Label.Text = ds.Tables[0].Rows[0][3].ToString();
                //course4Label.Text = ds.Tables[0].Rows[0][4].ToString();
                //course5Label.Text = ds.Tables[0].Rows[0][5].ToString();
                //course6Label.Text = ds.Tables[0].Rows[0][6].ToString();
                //course7Label.Text = ds.Tables[0].Rows[0][7].ToString();

                //if (ds.Tables[0].Rows[0][8].ToString() == "Yes")
                //    course8Label.Text = "Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][8].ToString() == "No")
                //    course8Label.Text = "Not a Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][8].ToString() == "Either")
                //    course8Label.Text = "No Specific Requirement";

                //if (ds.Tables[0].Rows[0][9].ToString() == "Yes")
                //    course9Label.Text = "Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][9].ToString() == "No")
                //    course9Label.Text = "Not a Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][9].ToString() == "Either")
                //    course9Label.Text = "No Specific Requirement";

                //if (ds.Tables[0].Rows[0][10].ToString() == "Yes")
                //    course10Label.Text = "Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][10].ToString() == "No")
                //    course10Label.Text = "Not a Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][10].ToString() == "Either")
                //    course10Label.Text = "No Specific Requirement";

                //if (ds.Tables[0].Rows[0][11].ToString() == "Yes")
                //    course11Label.Text = "Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][11].ToString() == "No")
                //    course11Label.Text = "Not a Mandatory Requirement";
                //if (ds.Tables[0].Rows[0][11].ToString() == "Either")
                //    course11Label.Text = "No Specific Requirement";

                //if (ds.Tables[0].Rows[0][12].ToString() != "NULL" && ds.Tables[0].Rows[0][12].ToString() != "")
                //    course12Label.Text = ds.Tables[0].Rows[0][12].ToString();
                //else
                //    course12Label.Text = "No Specific Choice of Subjects.";

                //strcmd = "SELECT A.ca_id, A.basic_info1 FROM tbl_career_master A, tbl_career_course_bridge B ";
                //strcmd += " Where A.ca_id = B.ca_id AND B.co_id = '" + Request.QueryString["id"].ToString() + "'";
                //ds = clsdal.ExecDataSet(strcmd);

                //lblListOfCareers.Text = "";
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    lblListOfCareers.Text += "<a Class='main4' href='careerdetails.aspx?id=" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</a><br><br>";

                lblCareerName.Text = ds.Tables[0].Rows[0][1].ToString();
                // lblCareername2.Text = ds.Tables[0].Rows[0][1].ToString();
                course2Label.Text = ds.Tables[0].Rows[0][2].ToString();
                course3Label.Text = ds.Tables[0].Rows[0][3].ToString();
                course4Label.Text = ds.Tables[0].Rows[0][4].ToString();
                course5Label.Text = ds.Tables[0].Rows[0][5].ToString();
                course6Label.Text = ds.Tables[0].Rows[0][6].ToString();
                course7Label.Text = ds.Tables[0].Rows[0][8].ToString();

                if (ds.Tables[0].Rows[0][9].ToString() == "Yes")
                    course8Label.Text = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][9].ToString() == "No")
                    course8Label.Text = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][9].ToString() == "Either")
                    course8Label.Text = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][10].ToString() == "Yes")
                    course9Label.Text = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][10].ToString() == "No")
                    course9Label.Text = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][10].ToString() == "Either")
                    course9Label.Text = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][11].ToString() == "Yes")
                    course10Label.Text = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][11].ToString() == "No")
                    course10Label.Text = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][11].ToString() == "Either")
                    course10Label.Text = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][12].ToString() == "Yes")
                    course11Label.Text = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][12].ToString() == "No")
                    course11Label.Text = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][12].ToString() == "Either")
                    course11Label.Text = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][13].ToString() != "NULL" && ds.Tables[0].Rows[0][13].ToString() != "")
                    course12Label.Text = ds.Tables[0].Rows[0][13].ToString();
                else
                    course12Label.Text = "No Specific Choice of Subjects.";

                strcmd = "SELECT A.ca_id, A.basic_info1 FROM tbl_career_master A, tbl_career_course_bridge B ";
                strcmd += " Where A.ca_id = B.ca_id AND B.co_id = '" + Request.QueryString["id"].ToString() + "'";
                ds = clsdal.ExecDataSet(strcmd);

                lblListOfCareers.Text = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    lblListOfCareers.Text += "<a Class='main4' href='careerdetails.aspx?id=" + ds.Tables[0].Rows[i][0].ToString() + "'>" + ds.Tables[0].Rows[i][1].ToString() + "</a><br><br>";

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong on form loading. Please Try again.";
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string query = "select top 10 institute_id,insti_name,rank from tbl_institute where zone='" + DropDownList1.SelectedItem.Text + "' and category like '" + course6Label.Text + "' order by rank ASC";
            DataSet ds1 = clsdal.ExecDataSet(query);

            if (ds1.Tables[0].Rows.Count != 0)
            {
                lblListOfInstitutes.Text = "";
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    lblListOfInstitutes.Text += "<a Class='main4' href='institute_info.aspx?id=" + ds1.Tables[0].Rows[i][0].ToString() + "'>" + ds1.Tables[0].Rows[i][1].ToString() + "</a><br><br>";

            }
            else
            {
                lblListOfInstitutes.Text = "No Information Available...";
            }

        }
        catch (Exception ex)
        {

            Log.Error(ex);
        }
    }
}