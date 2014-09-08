using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    public static bool IsDate(string inputDate)
    {
        DateTime dt;
        bool isdate = DateTime.TryParse(inputDate, out dt);
        return isdate;
    }

    //public static DisplayAlert(string msg)
    //{
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    //}
}