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
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            Users objUser = (Users)Session["User"];
            UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
            System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
            if (!dtUP.IsGlobalAdmin && !dtUP.IsGlobalContentAdmin)
            {
                Response.Redirect("../Default.aspx");
            }


        }
        if (!IsPostBack)
        {
            if (Session["UserPermission"] != null)
            {
                UserPermissions up = (UserPermissions)Session["UserPermission"];
                Session["IsGlobalContentAdmin"] = up.IsGlobalContentAdmin;
            }
        }
    }
}