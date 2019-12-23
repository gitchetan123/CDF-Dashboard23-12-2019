using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Candidate_CandidateReportsAndGraph : System.Web.UI.Page
{
    dal clsdal = new dal();
    bool flag = true;
    string strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString_simsr"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IsAllowReport"].ToString() == "Yes")
            {
                if (!Page.IsPostBack)
                {
                    div_Error.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch(Exception ex)
        {
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            bindgrid();
        }
        catch (Exception ex)
        {
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }
    private void bindgrid()
    {
        try
        {
            string strcmd = "";
            //   datevalidate();
            if (flag == true)
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (ddltesttype.SelectedValue == "Corptest")
                    {

                        if (ddlCandTestStatus.SelectedValue == "Complete")
                        {
                            if (ddl_Type.SelectedItem.Text == "Report")
                            {
                                //strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='SIMSIR' and tbl_candidate_master.c_id IN ( select c_id from tbl_KY_cand_factors GROUP By c_id HAVING Count(c_id) = 9)AND tbl_candidate_master.c_id IN ( select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24)";
                                strcmd = "SELECT Distinct tbl_candidate_master.c_id ,tbl_candidate_master.c_username,tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city = city_codes_simsr.city_id inner join tbl_Candidate_Mentor_Relation on tbl_Candidate_Mentor_Relation.c_id = tbl_candidate_master.c_id and tbl_Candidate_Mentor_Relation.IsAllowReport = 1 where tbl_candidate_master.c_status = 'APR' AND tbl_candidate_master.c_user_type IN ('Professional-Early', 'Professional-Mid', 'Corporate') and tbl_candidate_master.c_id IN(select c_id from tbl_KY_cand_factors GROUP By c_id HAVING Count(c_id) = 9) AND tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24) ";
                            }
                            if (ddl_Type.SelectedItem.Text == "Graph")
                            {
                                strcmd = "SELECT Distinct tbl_candidate_master.c_id ,tbl_candidate_master.c_username,tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city = city_codes_simsr.city_id inner join tbl_Candidate_Mentor_Relation on tbl_Candidate_Mentor_Relation.c_id = tbl_candidate_master.c_id and tbl_Candidate_Mentor_Relation.IsAllowGraph = 1 where tbl_candidate_master.c_status = 'APR' AND tbl_candidate_master.c_user_type IN ('Professional-Early', 'Professional-Mid', 'Corporate') and tbl_candidate_master.c_id IN(select c_id from tbl_KY_cand_factors GROUP By c_id HAVING Count(c_id) = 9) AND tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24) ";
                            }
                        }
                        else
                        {
                            //strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND (tbl_candidate_master.c_id NOT IN ( select c_id from tbl_KY_cand_factors)OR tbl_candidate_master.c_id NOT IN ( select c_id from Cand_PD_Details))";
                            // strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id  inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='SIMSIR' AND (tbl_candidate_master.c_id NOT IN (select c_id from tbl_KY_cand_factors) OR tbl_candidate_master.c_id IN (select c_id from tbl_KY_cand_answers GROUP By c_id HAVING Count(c_id) < 90) OR tbl_candidate_master.c_id NOT IN (select c_id from Cand_PD_Details) OR tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) < 24))";


                            strcmd = " SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password,tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB,tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join tbl_Candidate_Mentor_Relation on tbl_Candidate_Mentor_Relation.c_id = tbl_candidate_master.c_id and tbl_Candidate_Mentor_Relation.IsAllowReport = 0 inner join city_codes_simsr on tbl_candidate_master.c_city = city_codes_simsr.city_id where tbl_candidate_master.c_status = 'APR' AND tbl_candidate_master.c_user_type IN ('Professional-Early', 'Professional-Mid', 'Corporate') AND(tbl_candidate_master.c_id NOT IN(select c_id from tbl_KY_cand_factors) OR tbl_candidate_master.c_id IN(select c_id from tbl_KY_cand_answers GROUP By c_id HAVING Count(c_id) < 90) OR tbl_candidate_master.c_id NOT IN(select c_id from Cand_PD_Details) OR tbl_candidate_master.c_id IN(select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) < 24))";



                        }
                        if (txt_name.Text != "")
                        {
                            strcmd += " AND (c_first_name like '%" + txt_name.Text + "%' or c_last_name like '%" + txt_name.Text + "%') ";
                        }
                        if (txt_username.Text != "")
                        {
                            strcmd += " AND c_username like '%" + txt_username.Text + "%' ";
                        }
                        if (txt_city.Text != "")
                        {
                            strcmd += " AND city_codes_simsr.District like '%" + txt_city.Text + "%' ";
                        }
                        if (tbDate1.Text != "" && tbDate2.Text != "")
                        {
                            strcmd += " AND (c_date_of_reg BETWEEN '" + clsdal.DateConvert(tbDate1.Text) + "' AND '" + clsdal.DateConvert(tbDate2.Text) + "')";
                        }
                        strcmd += " order by tbl_candidate_master.c_date_of_reg desc, tbl_candidate_master.c_id desc";

                    }
                    //if (ddltesttype.SelectedValue == "Teaching Test")
                    //{

                    //    if (ddlCandTestStatus.SelectedValue == "Complete")
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='ESAT' and tbl_candidate_master.c_id IN ( select c_id from tbl_KY_cand_factors GROUP By c_id HAVING Count(c_id) = 9)AND tbl_candidate_master.c_id IN ( select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24)";
                    //    }
                    //    else
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id  inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='ESAT' AND (tbl_candidate_master.c_id NOT IN (select c_id from tbl_KY_cand_factors) OR tbl_candidate_master.c_id IN (select c_id from tbl_KY_cand_answers GROUP By c_id HAVING Count(c_id) < 90) OR tbl_candidate_master.c_id NOT IN (select c_id from Cand_PD_Details) OR tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) < 24))";
                    //    }
                    //    if (txt_name.Text != "")
                    //    {
                    //        strcmd += " AND (c_first_name like '%" + txt_name.Text + "%' or c_last_name like '%" + txt_name.Text + "%') ";
                    //    }
                    //    if (txt_username.Text != "")
                    //    {
                    //        strcmd += " AND c_username like '%" + txt_username.Text + "%' ";
                    //    }
                    //    if (txt_city.Text != "")
                    //    {
                    //        strcmd += " AND city_codes_simsr.District like '%" + txt_city.Text + "%' ";
                    //    }
                    //    if (tbDate1.Text != "" && tbDate2.Text != "")
                    //    {
                    //        strcmd += " AND (c_date_of_reg BETWEEN '" + clsdal.DateConvert(tbDate1.Text) + "' AND '" + clsdal.DateConvert(tbDate2.Text) + "')";
                    //    }
                    //    strcmd += " order by tbl_candidate_master.c_date_of_reg desc, tbl_candidate_master.c_id desc";

                    //}


                    //else if (ddltesttype.SelectedValue == "Personality Test")
                    //{
                    //    if (ddlCandTestStatus.SelectedValue == "Complete")
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='PD' AND tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24) AND tbl_candidate_master.c_id IN (select c_id from tbl_QuotientCandAnswers GROUP BY c_id HAVING Count(c_id) = 24) ";
                    //    }
                    //    else
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id  inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='PD' AND (tbl_candidate_master.c_id NOT IN (select c_id from Cand_PD_Details) OR tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) < 24)) AND tbl_candidate_master.c_id NOT IN (select c_id from tbl_QuotientCandAnswers) or tbl_candidate_master.c_id IN (select c_id from tbl_QuotientCandAnswers GROUP BY c_id HAVING Count(c_id) < 24) ";
                    //    }
                    //    if (txt_name.Text != "")
                    //    {
                    //        strcmd += " AND c_first_name like '%" + txt_name.Text + "%' ";
                    //    }
                    //    if (txt_username.Text != "")
                    //    {
                    //        strcmd += " AND c_username like '%" + txt_username.Text + "%' ";
                    //    }
                    //    if (txt_city.Text != "")
                    //    {
                    //        strcmd += " AND city_codes_simsr.District like '%" + txt_city.Text + "%' ";
                    //    }
                    //    if (tbDate1.Text != "" && tbDate2.Text != "")
                    //    {
                    //        strcmd += " AND (c_date_of_reg BETWEEN '" + clsdal.DateConvert(tbDate1.Text) + "' AND '" + clsdal.DateConvert(tbDate2.Text) + "')";
                    //    }
                    //    strcmd += " order by tbl_candidate_master.c_date_of_reg desc, tbl_candidate_master.c_id desc";
                    //}
                    //else if (ddltesttype.SelectedValue == "Non-Teaching Test")
                    //{
                    //    if (ddlCandTestStatus.SelectedValue == "Complete")
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='ESAT-PD' AND tbl_candidate_master.c_id IN (select c_id from Cand_PD_Details GROUP BY c_id HAVING Count(c_id) = 24)  ";
                    //    }
                    //    else
                    //    {
                    //        strcmd = "SELECT Distinct tbl_candidate_master.c_id,tbl_candidate_master.c_username, tbl_candidate_master.c_password, tbl_candidate_master.c_first_name, tbl_candidate_master.c_last_name,tbl_candidate_master.c_age_years,tbl_candidate_master.c_DOB, tbl_candidate_master.c_gender, tbl_candidate_master.c_date_of_reg,tbl_candidate_promotional_code.promotional_code,city_codes_simsr.District as city FROM tbl_candidate_master INNER JOIN tbl_candidate_promotional_code ON tbl_candidate_master.c_id = tbl_candidate_promotional_code.c_id INNER JOIN tbl_promoter ON tbl_candidate_promotional_code.promoter_id = tbl_promoter.promoter_id  inner join city_codes_simsr on tbl_candidate_master.c_city=city_codes_simsr.city_id where tbl_candidate_master.c_status ='APR' AND tbl_candidate_master.c_user_type='ESAT-PD' AND (tbl_candidate_master.c_id NOT IN (select c_id from Cand_PD_Details))  ";
                    //    }
                    //    if (txt_name.Text != "")
                    //    {
                    //        strcmd += " AND c_first_name like '%" + txt_name.Text + "%' ";
                    //    }
                    //    if (txt_username.Text != "")
                    //    {
                    //        strcmd += " AND c_username like '%" + txt_username.Text + "%' ";
                    //    }
                    //    if (txt_city.Text != "")
                    //    {
                    //        strcmd += " AND city_codes_simsr.District like '%" + txt_city.Text + "%' ";
                    //    }
                    //    if (tbDate1.Text != "" && tbDate2.Text != "")
                    //    {
                    //        strcmd += " AND (c_date_of_reg BETWEEN '" + clsdal.DateConvert(tbDate1.Text) + "' AND '" + clsdal.DateConvert(tbDate2.Text) + "')";
                    //    }
                    //    strcmd += " order by tbl_candidate_master.c_date_of_reg desc, tbl_candidate_master.c_id desc";
                    //}

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int row_count = ds.Tables[0].Rows.Count;
                    lbl_rowcount.Text = "Total - " + row_count.ToString();

                    if (ddlCandTestStatus.SelectedValue == "Not Complete")
                    {
                        GridView1.Columns[9].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }
                    else
                    {
                        if(ddl_Type.SelectedItem.Text=="Report")
                        {
                            GridView1.Columns[9].Visible = false;
                            GridView1.Columns[10].Visible = true;
                        }
                        if (ddl_Type.SelectedItem.Text == "Graph")
                        {
                            GridView1.Columns[9].Visible = true;
                            GridView1.Columns[10].Visible = false;
                        }
                    }

                    if (ddltesttype.SelectedValue == "Personality Test" || ddltesttype.SelectedValue == "Non-Teaching Test")
                    {
                        GridView1.Columns[10].Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";

        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewTest")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string c_id = lnkView.CommandArgument;
            lnkView.PostBackUrl = ConfigurationManager.AppSettings["View"].ToString() + c_id;
        }
    }
}