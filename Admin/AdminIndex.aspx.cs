using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserPermissions up = (UserPermissions)Session["UserPermission"];
        Session["IsGlobalContentAdmin"] = up.IsGlobalContentAdmin;
    }
}