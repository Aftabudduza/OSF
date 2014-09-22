using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Discussion : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    string _message = "";
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
                FillGrid();
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
        FillGrid();
        gvDiscussion.PageIndex = e.NewPageIndex;
        gvDiscussion.DataBind();
    }
    protected void lbtnEdit_Click(object sender, System.EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvDiscussion.Rows[row.RowIndex].FindControl("hdId");
        if (Convert.ToInt32(vId.Value) > 0)
        {
            try
            {
                Session["LevelId"] = vId.Value;
                Fill_Controls(Convert.ToInt32(vId.Value));
                btnSave.Text = "Update";
            }
            catch (Exception ex)
            {

            }
        }

    }
    protected void lbtnDelete_Click(object sender, System.EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvDiscussion.Rows[row.RowIndex].FindControl("hdId");
        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        if (Convert.ToInt32(vId.Value) > 0)
        {
            try
            {
                if (objCategories.delete(Convert.ToInt32(vId.Value)))
                {
                    FillGrid();
                }
               
            }
            catch (Exception ex)
            {

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);

            if (this.ValidateObject().Length > 0)
            {
                DisplayAlert(_message);

            }
            else
            {
                this.RefreshObject(objCategories);

                if (Convert.ToInt32(Session["LevelId"]) == 0)
                {

                    if (objCategories.insert())
                    {

                        DisplayAlert("Saved sucessfully");
                        txtTopicsName.Text = "";
                        FillGrid();
                    }
                    else
                    {
                        DisplayAlert("Saved Fail !!");
                        FillGrid();
                        txtTopicsName.Text = "";
                        //lblMessage.Text = "Error";
                    }

                }
                else
                {
                    objCategories.CategoryID = Convert.ToInt32(Convert.ToInt32(Session["LevelId"]));
                    if (objCategories.update())
                    {

                        DisplayAlert("Update Successfully");
                        FillGrid();
                        btnSave.Text = "Save";
                        txtTopicsName.Text = "";
                    }
                    else
                    {
                        DisplayAlert("Update Failed !!!");
                        FillGrid();
                        btnSave.Text = "Save";
                        //lblMessage.Text = "Error";
                        txtTopicsName.Text = "";
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
    private void FillGrid()
    {
        try
        {
            DataTable objDataTable = new DataTable();
            Categories obj = new Categories(osfcon.CONNECTIONSTRING);
            string sql = "";
            sql = string.Format(@"SELECT DISTINCT cat.CategoryID, MAX(cat.[Description]) Description ,Discussions = ISNULL((SELECT COUNT(distinct(c2.ContentID)) Total  
                                FROM Categories cat1, [Content] c2  WHERE cat1.CategoryTypeID={0} AND cat1.CategoryID=c2.CategoryID  AND (c2.RootThreadID = 0 OR c2.RootThreadID IS NULL) AND cat1.CategoryID
                                = cat.CategoryID  GROUP BY cat1.CategoryID ),0) ,Post = ISNULL((SELECT COUNT(distinct(c4.ContentID)) Total  FROM Categories cat2, [Content] c4  WHERE cat2.CategoryTypeID={0}
                                AND cat2.CategoryID=c4.CategoryID AND c4.RootThreadID > 0  AND cat2.CategoryID = cat.CategoryID  GROUP BY cat2.CategoryID ),0) FROM (select * from Categories WHERE CategoryTypeID={0})
                                cat LEFT join [Content] c  on  cat.CategoryID=c.CategoryID  GROUP BY cat.CategoryID ", (int)EnumSectionType.Discusstion);
            //sql = string.Format("SELECT distinct cat.CategoryID, MAX(cat.[Description]) Description ,Discussions = ISNULL((SELECT COUNT(distinct(c2.ContentID)) Total  FROM Categories cat1, [Content] c2  WHERE cat1.CategoryTypeID={0} AND cat1.CategoryID=c2.CategoryID AND (c2.RootThreadID = 0 OR c2.RootThreadID IS NULL) AND cat1.CategoryID = cat.CategoryID    GROUP BY cat1.CategoryID ),0) ,Post = ISNULL((SELECT COUNT(distinct(c4.ContentID)) Total  FROM Categories cat2, [Content] c4  WHERE cat2.CategoryTypeID={0} AND cat2.CategoryID=c4.CategoryID AND c4.RootThreadID > 0 AND cat2.CategoryID = cat.CategoryID    GROUP BY cat2.CategoryID ),0) FROM Categories cat, [Content] c  WHERE cat.CategoryTypeID={0} AND cat.CategoryID=c.CategoryID  GROUP BY cat.CategoryID ORDER BY MAX(cat.[Description]) ASC ");

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

    private void Fill_Controls(int categoryID)
    {
        try
        {
            DataTable objCategoryDetails = new DataTable();
            DataTable objCategory = new DataTable();

            objCategory = osfcon.getRows(string.Format(@"SELECT * FROM Categories WHERE CategoryID={0}", categoryID));

            if (objCategory.Rows.Count > 0)
            {
                txtTopicsName.Text = objCategory.Rows[0]["Description"].ToString();

            }
        }
        catch
        { 
        
        }
       
    }
    private void Clear_Controls()
    {
        txtTopicsName.Text = "";

    }   

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }
    private string ValidateObject()
    {
        _message = "";        

        if ((txtTopicsName.Text == ""))
        {
            _message += "Please enter Topics Name" + Environment.NewLine;
        }       
        
        return _message;
    }

    private Categories RefreshObject(Categories cat)
    {

        try
        {
            cat.SectionTypeID = Convert.ToInt32(EnumSectionType.Discusstion);
            cat.Description = txtTopicsName.Text;
            cat.IsLeaf = false;
            cat.CreatedBy = Convert.ToInt32(Session["UserID"]);
            cat.CreatedDate = DateTime.Today;
            cat.ModifiedDate = DateTime.Today;
        }
        catch
        { 
        
        }
       
        return cat;
    }       

    #endregion
}