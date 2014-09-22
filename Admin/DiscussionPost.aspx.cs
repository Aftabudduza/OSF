using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_DiscussionPost : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    string _message = "";
    int contentId = 0;
    string sTitle = "";
    string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                Session["ContentId"] = null;
                Session["LevelId"] = null;
                try
                {
                    contentId = Convert.ToInt32(Request.QueryString["ContentIdDiscussionDetail"].ToString());
                    Session["CategoryID"] = Convert.ToInt32( (osfcon.getRows(string.Format(@"SELECT CategoryID FROM Content WHERE ContentID={0}", contentId)).Rows[0][0]));
                }
                catch
                {
                    contentId = 0;
                }
                if (contentId > 0)
                {
                    Session["ContentId"] = contentId;
                }
                try
                {
                    sTitle = Request.QueryString["Title"].ToString();
                }
                catch
                {
                    sTitle = "";
                }
                if (sTitle != "" && !string.IsNullOrEmpty(sTitle))
                {
                    Session["Title"] = sTitle.ToString();
                }
                if (Session["ContentId"] != null && Session["Title"] != null)
                {
                    FillPost(Convert.ToInt32(Session["ContentId"].ToString()));
                    if (Session["Title"].ToString().Length > 20)
                    {
                        str = Session["Title"].ToString().Substring(0, 10) + "...";
                    }
                    else
                    {
                        str = Session["Title"].ToString();
                    }
                    discussionTitle.InnerHtml = "Post Topics : " + " " + "' " + str.ToString() + " '";
                }


                Session["LevelId"] = "0";
                // ControlsAppearence(false);
            }
        }
        catch
        { 
        
        }
       
    }

    #endregion Global Variable

    #region Events

    protected void btnSave_Click(object sender, EventArgs e)
    {      
        ContentObj obj = new ContentObj(osfcon.CONNECTIONSTRING);
     
        try
        {
            if (this.ValidateObject().Length > 0)
            {
                DisplayAlert(_message);
            }
            else
            {
              obj= this.RefreshObject(obj);              
                    
                    if ( obj.insert())
                    {
                        DisplayAlert("Post sucessfully");
                        txtSubject.Text = "";
                        txtMessage.Text="";
                        FillPost(Convert.ToInt32(Session["ContentId"].ToString()));
                    }
                    else
                    {
                        DisplayAlert("Post Failed !!");
                        FillPost(Convert.ToInt32(Session["ContentId"].ToString()));
                        txtSubject.Text = "";
                        txtMessage.Text="";
                        //lblMessage.Text = "Error";
                    }               
            }
            
        }
        catch
        {

        }

    }

    protected void btnShowPopup_Click(object sender, EventArgs e)
    {
        try
        {
            this.ModalPopupExtender1.Show();
        }
        catch
        { 
        
        }
    }    

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["CategoryID"].ToString()) != null && Session["Title"].ToString() != null)
            {
                Response.Redirect("DiscussionDetails.aspx?ContentIdDiscussion=" + Convert.ToInt32(Session["CategoryID"].ToString()) + "&Title=" + Session["Title"].ToString());
            }
            Session["CategoryID"] = null;
        }
        catch
        { 
        
        }


    }
    #endregion

    #region Method
    private void FillPost(int nCatID)
    {
        try
        {
            DataTable objDataTable = new DataTable();
            Categories obj = new Categories(osfcon.CONNECTIONSTRING);
            string sql = "";
            string html = "";
            sql = string.Format(" SELECT * FROM [Content] c WHERE c.RootThreadID=" + nCatID);
            if (sql.Length > 0)
            {
                objDataTable = osfcon.getRows(sql);
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    Session["CategoryID"] = Convert.ToInt32(objDataTable.Rows[0]["CategoryID"].ToString());
                    foreach (DataRow dr in objDataTable.Rows)
                    {

                        html += "<tr><td><hr/></td></tr>";
                        if (dr["Author"].ToString() != null)
                        {
                            html += "<tr><td style='background-color: #f5f5f5; float: left;  margin-bottom: 10px; margin-top: 10px; width: 100%'> <span style='float: left; font-size: 12px; font-weight: bold; margin-right: 7px;'>" + dr["Author"].ToString() + "</span> wrote the following at ";
                        }

                        if (dr["Date"].ToString() != null)
                        {
                            html += "" + dr["Date"].ToString() + "";
                        }

                        if (dr["Content"].ToString() != null)
                        {
                            html += "<tr><td style='float:left;margin-bottom:10px;'>" + HttpUtility.HtmlDecode(dr["Content"].ToString()) + "</td></tr>";
                        }

                        html += "<tr><td><hr/></td></tr>";
                        PostDiv.InnerHtml = html.ToString();
                    }

                }
                else
                {
                    this.ModalPopupExtender1.Show();
                }
            }
            else
            {
               
            }
           
        }
        catch
        {

        }

    }    

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private string ValidateObject()
    {
        _message = "";

        if ((txtSubject.Text == ""))
        {
            _message += "Please enter Topics Name" + Environment.NewLine;
        }

        return _message;
    }

    private ContentObj RefreshObject(ContentObj objnew)
    {
        try
        {
            if (Convert.ToInt32(Session["ContentId"]) != 0)
            {
                objnew.RootThreadID = Convert.ToInt32(Session["ContentId"]);
            }
            else
            {
                objnew.RootThreadID = 0;
            }

            if (Session["User"] != null)
            {
                objnew.Author = Session["User"].ToString();
            }
            else
            {
                objnew.Author = "";
            }

            if (txtSubject.Text != "" && !string.IsNullOrEmpty(txtSubject.Text.ToString()))
            {
                objnew.Title = txtSubject.Text.ToString();
            }
            else
            {
                objnew.Title = "";
            }
            if (Session["CategoryID"] != null)
            {
                objnew.CategoryID = Convert.ToInt32(Session["CategoryID"].ToString());
                Session["CategoryID"] = null;
            }
            else
            {
                objnew.CategoryID = 0;
            }
            if (!string.IsNullOrEmpty(txtMessage.Text) && txtMessage.Text != "")
            {
                objnew.Content = txtMessage.Text.ToString();
            }
            else
            {
                objnew.Content = "";
            }

            if (Session["UserID"] != null)
            {
                objnew.CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
            }
            else
            {
                objnew.CreatedBy = 0;
            }
            objnew.CreatedOn = DateTime.UtcNow;
            objnew.Date = DateTime.UtcNow;
        }
        catch
        { 
        
        }       

        return objnew;
    }

    #endregion
}