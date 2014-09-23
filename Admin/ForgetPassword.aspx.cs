using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;

public partial class Admin_ForgetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = "", email = "";
        if (Request["UserName"] != null)
            userName = Request["UserName"].ToString();
        if (Request["Email"] != null)
            email = Request["Email"].ToString();


        DataTable userDT = null;
        if (userName.Length > 0)
            userDT = osfcon.getRows(string.Format("Select * from Users WHERE UserName='{0}'", userName));
        if (userDT == null && email.Length > 0)
            userDT = osfcon.getRows(string.Format("Select * from Users WHERE HomeEmail='{0}'", email));

        if (email.Length > 0 && userDT != null && userDT.Rows.Count > 0)
        {

            string toke = Utility.GetUniqueKey(14);

            Utility.QueryExecute(string.Format(@"INSERT INTO PasswordResetRequests ([UserID],[RequestDate],[ResetCode],[DaysValid]) VALUES ({0},'{1}','{2}',{3})", Convert.ToInt32(userDT.Rows[0]["UserID"]), DateTime.Today, toke, 2));

         
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("2rtestg@gmail.com", "rapid123");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("2rtestg@gmail.com");
            msg.To.Add(new MailAddress(email));

            msg.Subject = "This is a test Email subject";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>{0}</b></body>", "http://localhost:2519/Admin/PasswordChange.aspx?Token=" + toke);

            try
            {
                client.Send(msg);
                
                DisplayAlert("A varification mail is sent to your email. Please check your email for details");
        
            }
            catch (Exception ex)
            {
                
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", sds, true);
    }
    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }
}