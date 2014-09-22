using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PasswordChange : System.Web.UI.Page
{

    #region Global Variable & PageLoad
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }
    #endregion


    #region Events
    protected void btnSubmit_Click(object sender, EventArgs e)
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
                DisplayAlert(string.Format("Password Change successfull" + Environment.NewLine + "Your next password renewal date is:'{0}'", DateTime.UtcNow.AddDays(ss.NumVal)));
                user.LastPasswordChange = DateTime.UtcNow;
                Session["User"] = user;
                Response.Redirect("../Default.aspx");

            }
            //if (user.Password == txtOldPassword.Text.Trim())
            //{
            //    user.QueryExecute(string.Format("UPDATE Users SET Password='{0}',LastPasswordChange='{1}' WHERE UserID={2}", txtNewPassword.Text, DateTime.UtcNow, userID));
            //}
            

            //else
            //{
            //    DisplayAlert("Old password doesnot match!");
            //}
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
    #endregion



}