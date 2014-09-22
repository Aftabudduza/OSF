using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Header : System.Web.UI.UserControl
{

    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
     //   CheckPasswordValidity();
    }
    #endregion


    #region Events

    #endregion

    #region Method
    private void CheckPasswordValidity()
    {
        Users user = (Users)(Session["User"]);
        SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
        ss.getRecord((int)EnumSystemSettings.GraceLogins);

        if (user.LastPasswordChange.AddDays(ss.NumVal) < DateTime.UtcNow)
        {
            Response.Redirect("../Admin/ChangePasssword.aspx");
        }

    }

    #endregion


}