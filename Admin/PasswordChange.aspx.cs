using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_PasswordChange : System.Web.UI.Page
{
    #region Global Variable & PageLoad
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Token"] == null)
            {
                if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
               
                txtOldPassword.Visible = true;
                lblOldPassword.Visible = true;
            }
            else
            {
                DataTable passReq = osfcon.getRows(string.Format(@"SELECT * FROM PasswordResetRequests WHERE ResetCode='{0}'", Request["Token"].ToString()));
                Users user = new Users(osfcon.CONNECTIONSTRING);
                user.getRecord(Convert.ToInt32(passReq.Rows[0][0]));
                Session["User"] = user;
                txtOldPassword.Visible = false;
                lblOldPassword.Visible = false;                

            }
        }
    }
    #endregion

    #region Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request["Token"] == null)
        {
            string message = "";
            message = ValidateObject();
            if (message.Length > 0)
            {
                DisplayAlert(message);

            }
            else
            {
                Users user = (Users)(Session["User"]);


                message = "";
                message = ValidatePassword(user.Password);
                if (message.Length > 0)
                {
                    DisplayAlert(message);
                    txtConfirmPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                }
                else
                {

                    SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
                    ss.getRecord((int)EnumSystemSettings.GraceLogins);
                    user.QueryExecute(string.Format("UPDATE Users SET Password='{0}',LastPasswordChange='{1}' WHERE UserID={2}", txtNewPassword.Text, DateTime.UtcNow, user.UserID));
                    user.QueryExecute(string.Format("DELETE FROM PasswordResetRequests WHERE UserID={0}",user.UserID));

                   // DisplayAlert(string.Format("Password Change successfull" + Environment.NewLine + "Your next password renewal date is:'{0}'", DateTime.UtcNow.AddDays(ss.NumVal)));

                    user.LastPasswordChange = DateTime.UtcNow;
                    Session["User"] = user;
                    Response.Redirect("../Default.aspx");

                }

            }
        }
        else
        {

            string message = "";
            message = ValidateObjectForForgetPassword();
            if (message.Length > 0)
            {
                DisplayAlert(message);

            }
            else
            {
                Users user = (Users)(Session["User"]);


                message = "";
                message = ValidatePasswordForForgetPassword();
                if (message.Length > 0)
                {
                    DisplayAlert(message);
                    txtConfirmPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                }
                else
                {

                    SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
                    ss.getRecord((int)EnumSystemSettings.GraceLogins);


                    user.QueryExecute(string.Format("UPDATE Users SET Password='{0}',LastPasswordChange='{1}' WHERE UserID={2}", txtNewPassword.Text, DateTime.UtcNow, user.UserID));
                    user.QueryExecute(string.Format("DELETE FROM PasswordResetRequests WHERE UserID={0}", user.UserID));

                    DisplayAlert(string.Format("Password Change successfull" + Environment.NewLine + "Your next password renewal date is:'{0}'", DateTime.UtcNow.AddDays(ss.NumVal)));
                    user.LastPasswordChange = DateTime.UtcNow;
                    
                    Session["User"] = null;
                    Response.Redirect("Login.aspx");

                }

            }


        }
    }
    #endregion

    #region Method
    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private string ValidateObject()
    {
        string _message = "";

        if ((txtOldPassword.Text.Length <= 0 ))
        {
            _message += "Old password can not be empty" + Environment.NewLine;
        }

        if ((txtNewPassword.Text.Length <= 0))
        {
            _message += "New password can not be empty" + Environment.NewLine;
        }

        if ((txtConfirmPassword.Text.Length <= 0))
        {
            _message += "Please confirm new password" + Environment.NewLine;
        }
      

        return _message;
    }

    private string ValidatePassword(string oldPassword)
    {
        string _message = "";

        if ((txtOldPassword.Text.Trim() != oldPassword))
        {
            _message += "Old password Doesn't match" + Environment.NewLine;
        }

        if ((txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim()))
        {
            _message += "New password Doesn't match" + Environment.NewLine;
        }

        return _message;
    }

    private string ValidateObjectForForgetPassword()
    {
        string _message = "";


        if ((txtNewPassword.Text.Length <= 0))
        {
            _message += "New password can not be empty" + Environment.NewLine;
        }

        if ((txtConfirmPassword.Text.Length <= 0))
        {
            _message += "Please confirm new password" + Environment.NewLine;
        }


        return _message;
    }

    private string ValidatePasswordForForgetPassword()
    {
        string _message = "";

        if ((txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim()))
        {
            _message += "New password Doesn't match" + Environment.NewLine;
        }

        return _message;
    }
    #endregion
}