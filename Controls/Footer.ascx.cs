using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCopyright.Text = string.Format("Copyright &copy; OSFphila {0}.", DateTime.UtcNow.Year);
    }
}