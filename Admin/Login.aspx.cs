using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Users uInfo = new Users(osfcon.CONNECTIONSTRING);
        string userName = txtUserName.Text;
        Session["User"] = null;
        this.ValidateUser();
       

    }


    private void ValidateUser()
    {
        if (!String.IsNullOrEmpty(txtUserName.Text.Trim()) && !String.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            Users objUsers = new Users(osfcon.CONNECTIONSTRING);
            try
            {
                DataTable dtSysChk = osfcon.getRows(string.Format(@"select FailedLogins from Users where Username =  '{0}'", txtUserName.Text.Trim(), (int)EnumSystemSettings.BadPasswordLockout));
                SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
                
                
                bool isUserBlocked = false;
                if (ss.Enabled && dtSysChk != null & Convert.ToInt32(dtSysChk.Rows[0][0]) > 0)
                {
                    if (Convert.ToInt32(dtSysChk.Rows[0][0]) == ss.NumVal)
                        isUserBlocked = true;
                }

                if (!isUserBlocked)
                {
                    DataTable objUserDataTable = osfcon.getRows(string.Format("SELECT * FROM (SELECT * FROM Users  WHERE Username = '{0}' AND Password='{1}' ) U LEFT Join UserPermissions Up on U.UserID = up.UserID", txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString()));

                    if ((objUserDataTable != null) && objUserDataTable.Rows.Count > 0)
                    {
                        Utility.QueryExecute(string.Format("Update Users Set FailedLogins = 0 WHERE UserName='{0}'", txtUserName.Text.Trim()));

                        if ((objUserDataTable.Rows[0]["UserID"].ToString() != null) && Convert.ToInt32(objUserDataTable.Rows[0]["UserID"].ToString()) > 0)
                        {
                            if (objUserDataTable.Rows[0]["CanLogIn"] == DBNull.Value || !Convert.ToBoolean(objUserDataTable.Rows[0]["CanLogIn"]))
                            {
                                DisplayAlert("You are not permitted to login!");
                                return;
                            }

                            Session["UserID"] = Convert.ToInt32(objUserDataTable.Rows[0]["UserID"].ToString());
                            if (objUsers.getRecord(Convert.ToInt32(objUserDataTable.Rows[0]["UserID"])))
                            {
                                Session["User"] = objUsers;

                                UserPermissions up = new UserPermissions(osfcon.CONNECTIONSTRING);
                                up.getRecord(objUsers.UserID);
                                Session["UserPermission"] = up;
                                DataTable dtUSP = null;
                                dtUSP = osfcon.getRows(string.Format("SELECT * FROM UserSectionPermission WHERE UserID={0} AND IsSection=1", objUsers.UserID));
                                if (!(up.IsSister || up.IsStaff))
                                {
                                    if (up.IsLayPerson)
                                    {
                                        foreach (System.Data.DataRow drow in dtUSP.Rows)
                                        {
                                            if (Convert.ToBoolean(drow["IsSection"]))
                                                drow["HasPermission"] = false;
                                        }
                                    }
                                    if (up.IsCompanion)
                                    {
                                        foreach (System.Data.DataRow drow in dtUSP.Rows)
                                        {
                                            if (Convert.ToBoolean(drow["IsSection"]) && Convert.ToInt32(drow["SectionID"]) != (int)EnumSectionType.Calender)
                                                drow["HasPermission"] = false;
                                            if (Convert.ToBoolean(drow["IsSection"]) && Convert.ToInt32(drow["SectionID"]) == (int)EnumSectionType.Calender)
                                                drow["HasPermission"] = true;
                                        }
                                    }
                                }
                                //if (up.IsSister || up.IsStaff)
                                //{
                                //    foreach (System.Data.DataRow drow in dtUSP.Rows)
                                //    {
                                //        if (Convert.ToBoolean(drow["IsSection"]))
                                //            drow["HasPermission"] = true;
                                //    }
                                //}
                                Session["UserSectionPermission"] = dtUSP;
                            }

                        }

                        Response.Redirect("../Default.aspx");

                    }
                    else
                    {
                        DisplayAlert("Wrong credentials!");
                        Utility.QueryExecute(string.Format("Update Users Set FailedLogins = ISNULL(FailedLogins,0) +1 WHERE UserName='{0}'", txtUserName.Text.Trim()));

                    }

                }

                else
                {
                    DisplayAlert("User is locked out of the system. Please contact the system administrator for assistance.");


                }


            }
            catch (Exception ex)
            {
                DisplayAlert(ex.Message);
            }
        }
        else
        {
            DisplayAlert("User name and password can not be empty!");
        }


    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg), true);
    }
}