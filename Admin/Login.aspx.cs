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
        Users uInfo = new Users(cmscon.CONNECTIONSTRING);
        string userName = txtUserName.Text;
        this.ValidateUser();

    }


    private void ValidateUser()
    {
        if (!String.IsNullOrEmpty(txtUserName.Text.Trim()) && !String.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            Users objUsers = new Users(cmscon.CONNECTIONSTRING);
            try
            {
                DataTable objUserDataTable = cmscon.getRows(string.Format("SELECT * FROM (SELECT * FROM Users  WHERE Username = '{0}' AND Password='{1}' ) U LEFT Join UserPermissions Up on U.UserID = up.UserID", txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString()));
                
                if ((objUserDataTable != null) && objUserDataTable.Rows.Count > 0)
                {
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

                            UserPermissions up = new UserPermissions(cmscon.CONNECTIONSTRING);
                            up.getRecord(objUsers.UserID);
                            Session["UserPermission"] = up;
                            DataTable dtUSP = null;
                            dtUSP= cmscon.getRows(string.Format("SELECT * FROM UserSectionPermission WHERE UserID={0} AND IsSection=1", objUsers.UserID));
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
                                        if (Convert.ToBoolean(drow["IsSection"]) && Convert.ToInt32(drow["SectionID"]) != (int)SectionTypeEnum.Calender)
                                            drow["HasPermission"] = false;
                                        if (Convert.ToBoolean(drow["IsSection"]) && Convert.ToInt32(drow["SectionID"]) == (int)SectionTypeEnum.Calender)
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