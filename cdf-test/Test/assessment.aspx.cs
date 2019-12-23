using System;

public partial class Candidate_assessment : BaseClass
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPersonality_Click(object sender, EventArgs e)
    {
        Response.Redirect("Separate_personality_test_status.aspx");
    }
}