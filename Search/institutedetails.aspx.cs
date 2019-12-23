using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Search_institutedetails : System.Web.UI.Page
{
    dal clsdal = new dal();
   


    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //check the user type 
    //        if (Session.Count > 0 && Session["user_type"].ToString().Equals("Admin"))
    //            this.Page.MasterPageFile = "~/AdminMaster.master";
    //        else if (Session.Count > 0 && Session["user_type"].ToString().Equals("USER"))
    //            this.Page.MasterPageFile = "~/UserMaster.master";
    //        else
    //            this.Page.MasterPageFile = "~/StaffMasterPage.master";
    //    }
    //    catch (Exception ex)
    //    {
    //        this.Page.MasterPageFile = "~/AdminMaster.master";
    //    }

    //}
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
                div_msg.Visible = false;
                string strcmd = "select distinct inst_name,category, region,state, city,  website,Affiliation, emailid, contact_no, address FROM tbl_institute_master where inst_id= '" + Request.QueryString["id"].ToString() + "'";
               
                DataSet ds = clsdal.ExecDataSet11(strcmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl_instname.Text = ds.Tables[0].Rows[0][0].ToString();
                    lbl_catagory.Text = ds.Tables[0].Rows[0][1].ToString();
                    lbl_region.Text = ds.Tables[0].Rows[0][2].ToString();
                    lbl_state.Text = ds.Tables[0].Rows[0][3].ToString();
                    lbl_city.Text = ds.Tables[0].Rows[0][4].ToString();
                    //lbl_website.Text = ds.Tables[0].Rows[0][5].ToString();
                    hpl_website.NavigateUrl = ds.Tables[0].Rows[0][5].ToString();
                    hpl_website.Text = ds.Tables[0].Rows[0][5].ToString();
             
                    lbl_affil.Text = ds.Tables[0].Rows[0][6].ToString();
                    lbl_email.Text = ds.Tables[0].Rows[0][7].ToString();
                    lbl_contact.Text = ds.Tables[0].Rows[0][8].ToString();
                    lbl_address.Text = ds.Tables[0].Rows[0][9].ToString();
                }

                strcmd = "SELECT B.subco_name, B.category, B.stream, specialization, subco_duration, B.basic_req, B.descrip, inst_req, rank,indiatodayrank,businesstodayrank,hindustantimesrank, dheya_rank, A.entrance_id,entrance_name FROM  tbl_institute_subco_details as A  inner join tbl_subcourse_master as B on A.subco_id=B.subco_id inner join tbl_entrance_master as C on A.entrance_id=C.entrance_id  where A.inst_id='" + Request.QueryString["id"].ToString() + "' and A.subco_id='" + Request.QueryString["sid"].ToString() + "' and A.specialization='" + Request.QueryString["spe"].ToString() + "'";
                DataSet ds_subco = clsdal.ExecDataSet11(strcmd);
                if (ds_subco.Tables[0].Rows.Count > 0)
                {
                    lbl_subconame.Text = ds_subco.Tables[0].Rows[0][0].ToString();
                    lbl_subcocatagory.Text = ds_subco.Tables[0].Rows[0][1].ToString();
                    lbl_subcostream.Text = ds_subco.Tables[0].Rows[0][2].ToString();
                    lbl_spe.Text = ds_subco.Tables[0].Rows[0][3].ToString();
                    lbl_duration.Text = ds_subco.Tables[0].Rows[0][4].ToString();
                    lbl_req.Text = ds_subco.Tables[0].Rows[0][5].ToString();
                    lbl_descrip.Text = ds_subco.Tables[0].Rows[0][6].ToString();
                    lbl_instreq.Text = ds_subco.Tables[0].Rows[0][7].ToString();
                    lbl_rank.Text = ds_subco.Tables[0].Rows[0][8].ToString();
                    lbl_itrank.Text = ds_subco.Tables[0].Rows[0][9].ToString();
                    lbl_btrank.Text = ds_subco.Tables[0].Rows[0][10].ToString();                   
                    lbl_htrank.Text = ds_subco.Tables[0].Rows[0][11].ToString();
                    lbl_drank.Text = ds_subco.Tables[0].Rows[0][12].ToString();                  
                    entrancename.NavigateUrl = "entrancedetail.aspx?id=" + ds_subco.Tables[0].Rows[0][13].ToString();
                    entrancename.Text = ds_subco.Tables[0].Rows[0][14].ToString();
                }

                strcmd = "select distinct A.specialization FROM tbl_institute_subco_details as A inner join tbl_course_subcourse_bridge as B on A.subco_id=B.subco_id and A.specialization=B.specialization inner join tbl_newcourse_master as C on C.co_id=B.co_id where A.subco_id=" + Request.QueryString["sid"].ToString() + " and A.specialization not in('" + Request.QueryString["spe"].ToString() + "') and A.inst_id=" + Request.QueryString["id"].ToString();
                DataSet ds_spe = clsdal.ExecDataSet11(strcmd);
                string otherspe = "";
                if (ds_spe.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds_spe.Tables[0].Rows.Count; i++)
                    {
                        otherspe += ds_spe.Tables[0].Rows[i][0].ToString()+" , ";
                    }
                    otherspe.Substring(0,otherspe.LastIndexOf(','));
                }
                lbl_otherspe.Text = otherspe;
               


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong. Please Try again."+ex;
            }
        }
    }
}