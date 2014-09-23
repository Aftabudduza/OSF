using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPages_Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || Convert.ToInt32(Session["UserID"]) == 0)
        {
            Response.Redirect("../Admin/Login.aspx");
        }
       
        CheckPasswordValidity();
        
    }

    private void CheckPasswordValidity()
    {
        Users user = (Users)(Session["User"]);
        SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
        ss.getRecord((int)EnumSystemSettings.GraceLogins);

        if (ss.Enabled && user.LastPasswordChange.AddDays(ss.NumVal) < DateTime.UtcNow)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "GoPasswordChangePage()", true);
        }

    }
}
