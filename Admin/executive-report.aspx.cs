using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_executive_report : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["adminuser_name"] != null)
                {
                    div_Error.Visible = false;
                    //bindgrid();
                    bindgrid_default();
                }
                else
                {
                    Response.Redirect("~/Admin/Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            div_Error.Visible = true;
            div_Error.Attributes["class"] = "alert alert-danger";
            div_Error.InnerText = "Something wrong. Please Try again.";
        }
    }
    private void bindgrid_default()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strcmd = "SELECT distinct ex.exeName as exeName, ISNULL (rc.RegComplete,0) as Reg_Complete, ISNULL(fc.FinalCDF,0) as Final_CDF, "
                + " isnull(cvr.TodaysTestSent, 0) as TodaysTestSent,ISNULL(vr.TotalTestSent, 0) as TotalTestSent, "
                + " ISNULL(ups.TestComplete, 0) as TestComplete,CONVERT(decimal, ISNULL((TestComplete * 100.00 / TotalTestSent), 0)) as "
                + " TestCompletePercent,ISNULL(pm.PaymentComplete, 0) as PaymentComplete, "
                + " CONVERT(decimal, ISNULL((PaymentComplete * 100.00 / TestComplete), 0)) as PaymentCompletePercent FROM tblExecutive as ex "
                + " LEFT OUTER JOIN(select executiveId, count(executiveId) as TodaysTestSent from tblVerifyRegistration "
                + " WHERE createDate >= CAST((SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) AS DATE) "
                + " group by executiveId) as cvr "
                + " on ex.id = cvr.executiveId "
                + " LEFT OUTER JOIN(select executiveId, count(*) as TotalTestSent from tblVerifyRegistration "
                + " group by executiveId) as vr on ex.id = vr.executiveId left outer join tblVerifyRegistration as v on ex.id = v.executiveId "
                + " left outer join tblUserMaster as u on v.email = u.email left outer join(select executiveId, COUNT(upm.uId) as TestComplete "
                + " from tblVerifyRegistration as vrs inner join tblUserMaster as u on vrs.email = u.email inner join tblUserProductMaster as "
                + " upm on u.uId = upm.uId group by vrs.executiveId, upm.teststatus having upm.teststatus = 'Complete') as ups "
                + " on ex.id = ups.executiveId "
                + " left outer join(select executiveId, COUNT(u.status) as RegComplete from tblVerifyRegistration as vrs1 "
                + " inner join tblUserMaster as u on vrs1.email = u.email and u.status = 'Reg_Complete' group by vrs1.executiveId) "
                + " as rc on ex.id = rc.executiveId "
                + " left outer join(select executiveId, COUNT(u.uId) as FinalCDF from tblVerifyRegistration as vrs2 "
                + " inner join tblUserMaster as u on vrs2.email = u.email "
                + " and u.userSource = 'DHEYA-CDF' and u.cdfApproved = 'APPROVED' and u.userTypeId = '2' group by vrs2.executiveId) "
                + " as fc on ex.id = fc.executiveId "
                + " left outer join(select executiveId, COUNT(p.uId) as PaymentComplete from tblVerifyRegistration as vrs "
                + " inner join tblUserMaster as u on vrs.email = u.email inner join tblPayment as p on u.uId = p.uId group by vrs.executiveId) "
                + " as pm on ex.id = pm.executiveId where ex.forUserType<>0 and ex.status='ACTIVE' order by TestCompletePercent desc ";

                SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvExeReport.DataSource = ds;
                    gvExeReport.DataBind();
                    lbl_rowcount.Visible = true;
                    div_Error.Visible = false;
                    ViewState["grid"] = "default_grid";
                    lbl_rowcount.Text = "Total - " + ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    //lbl_rowcount.Visible = false;
                    div_Error.Visible = true;
                    div_Error.InnerText = "There are no records found on selected Referral Name...... ";
                    gvExeReport.DataSource = null;
                    gvExeReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }
    private void bindgrid()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //Show executive reports
                //string strcmd = "SELECT distinct ex.exeName as exeName,isnull(cvr.TodaysTestSent,0) as TodaysTestSent,ISNULL(vr.TotalTestSent,0)as TotalTestSent,ISNULL(ups.TestComplete,0) as TestComplete,CONVERT(decimal,ISNULL((TestComplete*100.00/TotalTestSent),0)) as TestCompletePercent,ISNULL(pm.PaymentComplete,0) as PaymentComplete,CONVERT(decimal,ISNULL((PaymentComplete*100.00/TestComplete),0)) as PaymentCompletePercent " +
                //"FROM tblExecutive as ex " +
                //"LEFT OUTER JOIN (select executiveId, count(executiveId) as TodaysTestSent from tblVerifyRegistration WHERE createDate >= CAST((SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) AS DATE) group by executiveId ) as cvr on ex.id = cvr.executiveId " +
                //"LEFT OUTER JOIN(select executiveId, count(*) as TotalTestSent from tblVerifyRegistration group by executiveId) as vr on ex.id = vr.executiveId " +
                //"left outer join tblVerifyRegistration as v on ex.id = v.executiveId " +
                //"left outer join tblUserMaster as u on v.email = u.email " +
                //"left outer join(select executiveId, COUNT(upm.uId) as TestComplete from tblVerifyRegistration as vrs inner join tblUserMaster as u on vrs.email = u.email " +
                //"inner join tblUserProductMaster as upm on u.uId = upm.uId group by vrs.executiveId, upm.teststatus having upm.teststatus = 'Complete') as ups on ex.id = ups.executiveId " +
                //"left outer join(select executiveId, COUNT(p.uId) as PaymentComplete from tblVerifyRegistration as vrs inner join tblUserMaster as u on vrs.email = u.email " +
                //"inner join tblPayment as p on u.uId = p.uId group by vrs.executiveId) as pm on ex.id=pm.executiveId order by TestCompletePercent desc";


                string strcmd = " SELECT distinct ex.exeName as exeName, ISNULL (rc.RegComplete,0) as Reg_Complete, ISNULL(fc.FinalCDF,0) as Final_CDF,  "
                + " isnull(cvr.TodaysTestSent, 0) as TodaysTestSent,ISNULL(vr.TotalTestSent, 0) as TotalTestSent,  ISNULL(ups.TestComplete, 0) as TestComplete, "
                + " CONVERT(decimal, ISNULL((TestComplete * 100.00 / TotalTestSent), 0)) as TestCompletePercent,ISNULL(pm.PaymentComplete, 0) as "
                + " PaymentComplete,  CONVERT(decimal, ISNULL((PaymentComplete * 100.00 / TestComplete), 0)) as PaymentCompletePercent FROM tblExecutive as ex "

                + " LEFT OUTER JOIN(select executiveId, count(executiveId) as TodaysTestSent from tblVerifyRegistration WHERE createDate >= CAST((SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) AS DATE) group by executiveId) as cvr  on ex.id = cvr.executiveId "

                + " LEFT OUTER JOIN(select executiveId, count(*) as TotalTestSent from tblVerifyRegistration ";

                //if both date are not empty then where condition will find data between date 
                //+" -- where createDate between '2018-11-26' and '2018-12-22' "
                if (txt_from.Text != "" && txt_to.Text != "")
                {
                    strcmd += " where createDate between '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "' ";
                }

                strcmd += " group by executiveId) as vr on ex.id = vr.executiveId "

                + " left outer join tblVerifyRegistration as v on ex.id = v.executiveId "
                + " left outer join tblUserMaster as u on v.email = u.email "
                + " left outer join(select executiveId, COUNT(upm.uId) as TestComplete  from tblVerifyRegistration as vrs "
                + " inner join tblUserMaster as u on vrs.email = u.email ";

                if (txt_from.Text != "" && txt_to.Text != "")
                {
                    strcmd += " and vrs.createDate between '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "' ";
                }

                strcmd += " inner join tblUserProductMaster as upm on u.uId = upm.uId group by vrs.executiveId, upm.teststatus having upm.teststatus = 'Complete') as ups  on ex.id = ups.executiveId "
                + " left outer join(select executiveId, COUNT(u.status) as RegComplete from tblVerifyRegistration as vrs1 "
                + " inner join tblUserMaster as u on vrs1.email = u.email and u.status = 'Reg_Complete' group by vrs1.executiveId) as rc on ex.id = rc.executiveId "
                + " left outer join(select executiveId, COUNT(u.uId) as FinalCDF from tblVerifyRegistration as vrs2 "
                + " inner join tblUserMaster as u on vrs2.email = u.email  and u.userSource = 'DHEYA-CDF' and u.cdfApproved = 'APPROVED' and u.userTypeId = '2' group by vrs2.executiveId) as fc on ex.id = fc.executiveId "
                + " left outer join(select executiveId, COUNT(p.uId) as PaymentComplete from tblVerifyRegistration as vrs "
                + " inner join tblUserMaster as u on vrs.email = u.email ";

                if (txt_from.Text != "" && txt_to.Text != "")
                {
                    strcmd += " and vrs.createDate between '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "' ";
                }

                strcmd += " inner join tblPayment as p on u.uId = p.uId group by vrs.executiveId) as pm on ex.id = pm.executiveId "

                + " where ex.forUserType <> 0 ";

                if (drop_status.SelectedItem.Text == "ACTIVE")
                {
                    strcmd += " and ex.status='ACTIVE'";
                }
                if (drop_status.SelectedItem.Text == "DEACTIVE")
                {
                    strcmd += " and ex.status='DEACTIVE'";
                }
                if (txtExeName.Text != "")
                {
                    strcmd += " and ex.exeName like '%" + txtExeName.Text + "%'";
                }

                strcmd += " order by TestCompletePercent desc ";

                SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvExeReport.DataSource = ds;
                        gvExeReport.DataBind();
                        lbl_rowcount.Visible = true;
                        div_Error.Visible = false;
                        ViewState["grid"] = "search_grid";
                        lbl_rowcount.Text = "Total - " + ds.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        lbl_rowcount.Visible = false;
                        div_Error.Visible = true;
                        div_Error.InnerText = "There are no records found on selected Executive Name...... ";
                        gvExeReport.DataSource = null;
                        gvExeReport.DataBind();
                    }
                }
                else
                {
                    //lbl_rowcount.Visible = false;
                    div_Error.Visible = true;
                    div_Error.InnerText = "There are no records found on selected Executive Name...... ";
                    gvExeReport.DataSource = null;
                    gvExeReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }
    protected void gvExeReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvExeReport.PageIndex = e.NewPageIndex;
            //if (drop_status.SelectedItem.Text == "ACTIVE" || drop_status.SelectedItem.Text == "DEACTIVE")
            //{
            //    bindgrid();
            //}
            //else
            //{
            //    bindgrid_default();
            //}
            string grid = ViewState["grid"].ToString();
            if (grid == "search_grid")
            {
                gvExeReport.PageIndex = e.NewPageIndex;
                bindgrid();
            }
            else {
                gvExeReport.PageIndex = e.NewPageIndex;
                bindgrid_default();
            }
        }
        catch (Exception ex)
        {
            div_Error.Visible = true;
            div_Error.Attributes["class"] = "alert alert-danger";
            div_Error.InnerText = "Something wrong. Please Try again.";
        }
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            string status = drop_status.SelectedItem.Text;
            bindgrid();
        }
        catch (Exception ex)
        {
            div_Error.Visible = true;
            div_Error.Attributes["class"] = "alert alert-danger";
            div_Error.InnerText = "Something wrong. Please Try again.";
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //txt_from.Text = "";
        //txt_to.Text = "";
        //drop_status.SelectedIndex = 0;
        Response.Redirect("executive-report.aspx", false);
    }
}