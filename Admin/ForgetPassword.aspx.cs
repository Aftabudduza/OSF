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

//                System.Text.StringBuilder tbl = new System.Text.StringBuilder();
//            msg.Body = tbl.Append(string.Format(@"<div style='direction:ltr;text-align:left;font-family:'Open sans','Arial',sans-serif;color:#444;background-color:white;
//            padding:1.5em;border-radius:1em;max-width:580px;margin:2% auto 0 auto'><table style='background:white;width:100%'><tbody><tr><td><div style='width:90px;min-height:54px;
//            margin:10px auto'><img width='90' height='34' alt='Google' src='https://ci6.googleusercontent.com/proxy/mHy83PrMlAFaUM8Wo88ZAayRwWbU4dSztIJR2Q3diu_jwSeKti5RospLAc0A4AtVpHO5DZ7J6d4y_uDHtyGjUoa1m9MepX86MnDK6dOdi5hdIvFXLlZd7hFfVqOMsnw=s0-d-e1-ft#https://services.google.com/fh/files/emails/google_logo_flat_90_color.png'>
//            </div><div style='width:90%;padding-bottom:10px;padding-left:15px'><p><img style='display:block;float:left;margin-top:4px;margin-right:5px' 
//            src='https://ci5.googleusercontent.com/proxy/k-jr2y4ttJldbimrj22ku5ZryffArgRHUk0Lvong7MmDTR0ZJdo0OllmbCWMiZB0Zjlj8hcD_9f1OAuJrkkhOaxpo4MzfRfG4tfzIFtWpDk3awIAX0m5KzCyEymN=s0-d-e1-ft#https://ssl.gstatic.com/accounts/services/mail/msa/gmail_icon_small.png' 
//            alt=''><span style='font-family:'Open sans','Arial',sans-serif;font-weight:bold;font-size:small;line-height:1.4em'>Hi Rapid</span></p><p><span style='font-family:'Open sans','Arial',sans-serif;font-size:2.08em'>
//            Gmail's inbox puts you in control</span><br></p></div><p></p><div style='clear:both;float:left;padding:0px 5px 10px 10px;vertical-align:top'><a class='playable' target='_blank' style='text-decoration:none;border:0'
//            href='https://www.youtube.com/watch?v=CFf7dlewJus'><img width='129' height='129' src='https://ci5.googleusercontent.com/proxy/rN7M6NzOZEMvbpdaXBAjiHDEiXpgI5Uh2pN5x87stBQLtFo9Wiu_HmTQfXucqjgQhLiRRZ3UOI17RKYVKITUooOTPJvP_DsvOnamdNoQrEhiIBKzYUqlcaP_Cw=s0-d-e1-ft#https://ssl.gstatic.com/mail/welcome/localized/en/welcome_video_2.png'
//            style='border:0' alt='Inbox video'></a></div><div style='float:left;vertical-align:middle;padding:10px;max-width:398px;float:left'><table style='vertical-align:middle'><tbody><tr><td style='font-family:'Open sans','Arial',sans-serif'><span style='font-size:20px'>Meet the inbox</span><br><br><span style='font-size:small;line-height:1.4em'>
//            Gmail's inbox sorts your email into categories so you can see what's new at a glance, decide which emails you want to read when and view similar types of emails together. <a target='_blank' style='text-decoration:none;color:#15c'
//            href='https://www.youtube.com/watch?v=CFf7dlewJus'>Watch the video</a></span></td></tr></tbody></table></div><div style='float:left;clear:both;padding:0px 5px 10px 10px'>
//            <img width='129' height='129' style='display:block' alt='Social tab' src='https://ci4.googleusercontent.com/proxy/pSy1ZK0SwUN6sSNd_u-dDvGeZ3T40nq6hIbVWvGPuuqjb4hMeioW9SCoVHcIDCCBg0i_EmCF5085udSjTgMEbn4X4A2RiaI2v_av0o9UslJelfEKSUfEqgI8nz-wVOE=s0-d-e1-ft#https://ssl.gstatic.com/mail/welcome/localized/en/welcome_inbox_tab_2.png'>
//            </div><div style='float:left;vertical-align:middle;padding:10px;max-width:398px;float:left'><table style='vertical-align:middle'><tbody><tr><td style='font-family:'Open sans','Arial',sans-serif'><span style='font-size:20px'>Choose your categories</span><br>
//            <br><span style='font-size:small;line-height:1.4em'>The Social and Promotions categories are on by default. Add categories like Updates and Forums or remove  categories to have 
//            those emails show up in your Primary inbox. <a target='_blank' style='text-decoration:none;color:#15c' href='https://support.google.com/mail/?hl=en&amp;p=inboxcategories_all'>
//            Learn how to choose categories</a></span></td></tr></tbody></table></div><div style='float:left;clear:both;padding:0px 5px 10px 10px'><img width='129' height='129' 
//            style='display:block' alt='Customize' src='https://ci5.googleusercontent.com/proxy/J8zpZGNLI2h4WybI_OyIruXOrqkMzwG0ZyekMG5qJ1zrghGQSXBbwqbRSeivH74fFOt0h0XGmusW8LyYjRCaj7axLX2-399LeCCowidlD5083OOD7cNbyfwlTTQxCtQ=s0-d-e1-ft#https://ssl.gstatic.com/mail/welcome/localized/en/welcome_customize_2.png'>
//            </div><div style='float:left;vertical-align:middle;padding:10px;max-width:398px;float:left'><table style='vertical-align:middle'><tbody><tr><td style='font-family:'Open sans','Arial',sans-serif'><span style='font-size:20px'>Customize your inbox
//            </span><br><br><span style='font-size:small;line-height:1.4em'>If you see a message you want in a different category, you can move it there. On mobile devices, you can even choose
//            which categories create a notification. <a target='_blank' style='text-decoration:none;color:#15c' href='https://support.google.com/mail/?hl=en&amp;p=inboxcategories_all'>More customization tips</a></span></td></tr>
//            </tbody></table></div><br><br><br><div style='clear:both;vertical-align:middle;padding-left:15px;line-height:1.4em;font-family:'Open sans','Arial',sans-serif;font-size:small;text-align:left'>
//            To learn more about Gmail's inbox, check out the <a target='_blank' style='text-decoration:none;color:#15c' href='https://support.google.com/mail/?hl=en&amp;p=inboxcategories_all'>
//            help center</a> or <a target='_blank' style='text-decoration:none;color:#15c' href='https://www.youtube.com/watch?v=CFf7dlewJus&amp;amp'>watch the video</a>.</div><br><br>
//            <div style='clear:both;padding-left:13px;min-height:6.8em'><table style='width:100%;border-collapse:collapse;border:0'><tbody><tr><td style='width:68px'><img width='49' height='37' style='display:block' 
//            src='https://ci6.googleusercontent.com/proxy/Otim0U1swAd_E8Rhn2qrJguf92sTatA7EMZ-rpNoF7_Fr2GW1DUtwS9Z0dt_ojHL3ifjAa19FUw6wIoLJI5LVMRyTAZ8g4cr6fxA-bv80LenFLgrNs0JpuPF6LX5=s0-d-e1-ft#https://ssl.gstatic.com/accounts/services/mail/msa/gmail_icon_large.png'
//            alt='Gmail icon'></td><td style='font-family:'Open sans','Arial',sans-serif;vertical-align:bottom'><span style='font-size:small'>Happy emailing,<br></span><span style='font-size:x-large;line-height:1'>The Gmail Team</span></td></tr></tbody></table></div>
//            </td></tr></tbody></table></div>")).ToString();

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