using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DiscussionDetails : System.Web.UI.Page
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
                    contentId = Convert.ToInt32(Request.QueryString["ContentIdDiscussion"].ToString());
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
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                    if (Session["Title"].ToString().Length > 20)
                    {
                        str = Session["Title"].ToString().Substring(0, 10) + "...";
                    }
                    else
                    {
                        str = Session["Title"].ToString();
                    }

                    discussionTitle.InnerHtml = "Topics : " + " " + "' " + str.ToString() + " '";
                }


                Session["LevelId"] = "0";
            }
        }
        catch
        { 
        
        }
        
    }

    #endregion Global Variable

    #region Events
    protected void gvDiscussion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // FillGrid();
        gvDiscussion.PageIndex = e.NewPageIndex;
        gvDiscussion.DataBind();
    }
    protected void lbtnDelete_Click(object sender, System.EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvDiscussion.Rows[row.RowIndex].FindControl("hdId");
        ContentObj obj = new ContentObj(osfcon.CONNECTIONSTRING);
        if (Convert.ToInt32(vId.Value) > 0)
        {
            try
            {
                if (obj.delete(Convert.ToInt32(vId.Value)))
                {
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                    DisplayAlert("Topics Deteted Sucessefully");
                }
                else
                {
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                    DisplayAlert("Deteted Failed !!!");
                }               
                
            }
            catch (Exception ex)
            {

            }
        }

    }
    protected void btnDiscussion_Click(object sender, EventArgs e)
    {
        int checkInsert = 0;
        ContentObj objCaontent = new ContentObj(osfcon.CONNECTIONSTRING);
        try 
        { 
            if (this.ValidateObject().Length > 0)
                {
                    DisplayAlert(_message);
                }
        else
        {
            this.RefreshObject(objCaontent);

            if (Convert.ToInt32(Session["LevelId"]) == 0)
            {
                checkInsert = objCaontent.insert_Content();
                if (checkInsert >0)
                {
                    DisplayAlert("Saved sucessfully");
                    txtsubject.Text = "";
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                }
                else
                {
                    DisplayAlert("Saved Fail !!");
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                    txtsubject.Text = "";
                    //lblMessage.Text = "Error";
                }

            }
            else
            {
                objCaontent.ContentID = Convert.ToInt32(Convert.ToInt32(Session["LevelId"]));
                if (objCaontent.update())
                {
                    DisplayAlert("Update Successfully");
                    txtsubject.Text = "";
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                }
                else
                {
                    DisplayAlert("Update Failed !!!");
                    txtsubject.Text = "";
                    FillGrid(Convert.ToInt32(Session["ContentId"].ToString()));
                    //lblMessage.Text = "Error";
                }


            }
        }    

        }
        catch
        {
        
        }

}
    
    #endregion

    #region Method
    private void FillGrid(int nCatID)
    {
        try
        {
            DataTable objDataTable = new DataTable();
            Categories obj = new Categories(osfcon.CONNECTIONSTRING);
            string sql = "";
            //sql = string.Format(" SELECT distinct max(c.ContentID) ContentID,max(c.Author) Author,max(c.Content) Title,max(c.Date) LastUpdated , Post=ISNULL((SELECT COUNT( c3.ContentID) AS Total FROM [Content] c3, [Content] c4 WHERE c3.ContentID=c4.RootThreadID AND c3.CategoryID=" + nCatID + " AND c.ContentID=c3.ContentID GROUP BY c3.ContentID),0)  FROM [Content] c, [Content] c2 WHERE c.CategoryID= " + nCatID + " AND c.RootThreadID=0 GROUP BY c.ContentID ");            
            sql = string.Format(@"SELECT distinct c.ContentID ,max(c.Author) Author,max(c.Content) 
                                  Title, LastUpdated =(SELECT max(c5.Date) Date  FROM [Content] c5,[Content] c6 WHERE c5.CategoryID={0} AND 
                                  c5.RootThreadID=c.ContentID ), Post=ISNULL((SELECT COUNT( c3.ContentID) AS Total FROM [Content] c3, [Content] 
                                 c4 WHERE c3.ContentID=c4.RootThreadID AND c3.CategoryID={0} AND c.ContentID=c3.ContentID GROUP BY c3.ContentID),0) 
                                 FROM [Content] c, [Content] c2 WHERE c.CategoryID= {0} AND c.RootThreadID=0 GROUP BY c.ContentID",nCatID);


            objDataTable = osfcon.getRows(sql);
            if (objDataTable != null)
            {
                gvDiscussion.DataSource = objDataTable;
                gvDiscussion.DataBind();
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

        if ((txtsubject.Text == ""))
        {
            _message += "Please enter Topics Name" + Environment.NewLine;
        }

        return _message;
    }
    private ContentObj RefreshObject(ContentObj Content)
    {
        try
        {
            if (Convert.ToInt32(Session["ContentId"]) != 0)
            {
                Content.CategoryID = Convert.ToInt32(Session["ContentId"]);
            }
            else
            {
                Content.CategoryID = 0;
            }

            if (Session["User"] != null)
            {
                Content.Author = Session["User"].ToString();
            }
            else
            {
                Content.Author = "";
            }

            if (Session["Title"] != null && !string.IsNullOrEmpty(Session["Title"].ToString()))
            {
                Content.Title = Session["Title"].ToString();
            }
            else
            {
                Content.Title = "";

            }

            if (!string.IsNullOrEmpty(txtsubject.Text) && txtsubject.Text != "")
            {
                Content.Content = txtsubject.Text.ToString();
            }
            else
            {
                Content.Content = "";
            }

            if (Session["UserID"] != null)
            {
                Content.CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
            }
            else
            {
                Content.CreatedBy = 0;
            }
            Content.CreatedOn = DateTime.UtcNow;
            Content.RootThreadID = 0;
            Content.Date = DateTime.UtcNow;
            Content.LastUpdated = DateTime.UtcNow;
        }
        catch
        { 
        
        }        
       
        return Content;
    }
    #endregion
}