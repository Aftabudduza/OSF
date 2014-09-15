using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_AdminContent : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    int _categoryID = 0; string _message = "";
    //EditContent
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack && Request.QueryString["Method"] != null && Request.QueryString["Method"] == "EditContent")
        {
            CheckAdminPermission();
            LoadComboCategoryList();
            ContentObj cObj = new ContentObj(cmscon.CONNECTIONSTRING);

            if (Request.QueryString["ID"] != null)
            {
                int contentid = Convert.ToInt32(Request.QueryString["ID"]);
                cObj = cObj.getRecordFromID(contentid);
                DesignLabeslAndOthers(cObj.CategoryID);
                SetDataOnEdit(cObj);
                ddlCategoryList.SelectedValue =  cObj.CategoryID.ToString();
            }
        }
        else
        {
            if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                CheckAdminPermission();
            }
            if (!IsPostBack)
            {
                Session["FileName"] = null;
                LoadComboCategoryList();

            }
        }
    }
    #endregion
    
    #region Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            _message = "";
            if (this.ValidateObject().Length <= 0)
            {
                bool isEdit = false;
                if (Request.QueryString["Method"] != null && Request.QueryString["Method"] == "EditContent")
                {
                    isEdit = true;
                }

                if (!isEdit)
                {
                    ////////////////
                    if (!string.IsNullOrEmpty(this.uplProduct.FileName))
                    {
                        //read the file in
                        string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Content\\");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        string nFile = Path.Combine(filePath, "ContentFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName);
                        Session["FileName"] = nFile;
                        try
                        {
                            if (System.IO.File.Exists(nFile))
                            {
                                System.IO.File.Delete(nFile);
                            }

                        }
                        catch (Exception ex)
                        {
                        }
                        uplProduct.SaveAs(nFile);

                        ContentObj objContent = new ContentObj(cmscon.CONNECTIONSTRING);
                        this.SetData(objContent);
                        if (ddlCategoryList.SelectedValue != "-1")
                        {
                            objContent.CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());

                        }

                        if (objContent.insert())
                        {
                            Session["FileName"] = null;
                            DisplayAlert("Content Saved Successfully");
                            txtAuthor.Text = "";
                            txtDate.Text = "";
                            txtTitle.Text = "";
                            txtContent.Text = "";
                        }
                        else
                        {
                            DisplayAlert("Error");
                        }
                    }
                    else
                    {
                        DisplayAlert("You Must Select file To Import.");
                    }


                    /////////////
                }
                else //is eidt
                {
                    string nFile = "";
                    if (string.IsNullOrEmpty(this.uplProduct.FileName))
                    {
                        Session["FileName"] = nFile = lblURLofFile.Text;
                    }
                    //read the file in
                    else
                    {
                        string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Content\\");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        nFile = Path.Combine(filePath, "ContentFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName);
                        Session["FileName"] = nFile;
                        try
                        {
                            if (System.IO.File.Exists(nFile))
                            {
                                System.IO.File.Delete(nFile);
                            }

                        }
                        catch (Exception ex)
                        {
                        }
                        uplProduct.SaveAs(nFile);
                    }
                    ContentObj objContent = new ContentObj(cmscon.CONNECTIONSTRING);

                    this.SetData(objContent);
                    objContent.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    objContent.ModifiedOn = DateTime.UtcNow;



                    if (ddlCategoryList.SelectedValue != "-1")
                    {
                        objContent.CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());

                    }
                    DataTable dt = cmscon.getRows(string.Format("SELECT CategoryTypeID FROM Categories WHERE CategoryID ={0}", objContent.CategoryID));


                    int secID = Convert.ToInt32(dt.Rows[0][0]);
                    objContent.ContentID = Convert.ToInt32(Request.QueryString["ID"]);
                    if (objContent.update())
                    {
                        Session["FileName"] = null;
                        DisplayAlert("Content Saved Successfully");
                        if (secID == (int)SectionTypeEnum.News || secID == (int)SectionTypeEnum.Calender || secID == (int)SectionTypeEnum.BulletinBoard)
                            Response.Redirect(string.Format("../Contents/NewsorContents.aspx?SectionTypeID={0}", secID));
                        else
                            Response.Redirect(string.Format("../Contents/BoxContents.aspx?SectionTypeID={0}", secID));

                    }
                    else
                    {
                        DisplayAlert("Error");
                    }


                }
            }
            else
            {
                DisplayAlert(_message);
            }
        }
        catch (Exception ex)
        {
            //  lblMsg.Text = "Error Inloading:" + ex.Message;
            //  appGlobal.LogDataError("Error Inloading:" + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminIndex.aspx");
    }
    protected void ddlCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategoryList.SelectedValue != "-1")
        {
            int CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());
            DesignLabeslAndOthers(CategoryID);

        }
    }

    #endregion

    #region Method
    private void CheckAdminPermission()
    {
        Users objUser = (Users)Session["User"];
        UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
        System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
        if (!dtUP.IsGlobalContentAdmin)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }


    private string ValidateObject()
    {
        _message = "";

        if ((ddlCategoryList.SelectedValue.ToString() == "-1"))
        {
            _message += "Please select a Category Type" + Environment.NewLine;
        }

        if ((txtAuthor.Text == ""))
        {
            _message += "Please enter author Name" + Environment.NewLine;
        }

        if ((txtTitle.Text == ""))
        {
            _message += "Please enter title" + Environment.NewLine;
        }

        if ((txtContent.Text == ""))
        {
            _message += "Please enter content" + Environment.NewLine;
        }

        return _message;
    }

    private void LoadComboCategoryList()
    {
        ddlCategoryList.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            //DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + 0 + "' AND IsLeaf = '" + 0 + "' ");
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + 0 + "'");


            ddlCategoryList.AppendDataBoundItems = true;
            ddlCategoryList.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryList.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryList.SelectedValue = "-1";

            ddlCategoryList.SelectedValue = Request["CategoryID"].ToString();
        }
        catch (Exception ex)
        { }
    }

    private void SetData(ContentObj objContent)
    {
        try
        {
            objContent.Author = txtAuthor.Text;
            objContent.Date = Convert.ToDateTime(txtDate.Text);
            objContent.Title = txtTitle.Text;
            objContent.Content = txtContent.Text;
            objContent.CategoryID = _categoryID;
            objContent.ContentTypeID = 1;//Will change
            if (Session["FileName"] != null)
                objContent.URL = Session["FileName"].ToString();

            objContent.CreatedBy = Convert.ToInt32(Session["UserID"]);
            objContent.CreatedOn = Convert.ToDateTime(DateTime.UtcNow);
        }
        catch (Exception ex)
        { 
        
        
        }
    }

    private void DesignLabeslAndOthers(int categoryID)
    {
        _categoryID = categoryID;
        bool isNeedtoShowDefaultContent = true;
        if (Request.QueryString["Method"] != null && Request.QueryString["Method"] == "EditContent")
        {
            isNeedtoShowDefaultContent = false;
        }
        //Get Category Details to Load control categorywise
        CategoryDetails objCategoryDetail = new CategoryDetails(cmscon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = cmscon.getRows(string.Format("SELECT * FROM CategoryDetails WHERE CategoryID={0}",categoryID));
            if ((objDataTable.Rows[0]["TitleTitle"].ToString() != null))
            {
                lblTitleTitle.Text = objDataTable.Rows[0]["TitleTitle"].ToString();
            }
            if ((objDataTable.Rows[0]["AuthorTitle"].ToString() != null))
            {
                lblAuthorTitle.Text = objDataTable.Rows[0]["AuthorTitle"].ToString();
            }
            if ((objDataTable.Rows[0]["DateTitle"].ToString() != null))
            {
                lblDateTtile.Text = objDataTable.Rows[0]["DateTitle"].ToString();
            }
            if ((objDataTable.Rows[0]["ContentTitle"].ToString() != null))
            {
                lblContentTitle.Text = objDataTable.Rows[0]["ContentTitle"].ToString();
            }

            //Show Controls
            if (Convert.ToBoolean(objDataTable.Rows[0]["ShowTitle"]) != null)
            {
                bool showTitle = Convert.ToBoolean(objDataTable.Rows[0]["ShowTitle"]);
                lblTitleTitle.Visible = showTitle;
                txtTitle.Visible = showTitle;

                if (objDataTable.Rows[0]["DefaultTitle"] != null)
                {
                    if (Convert.ToBoolean(objDataTable.Rows[0]["ShowTitle"]) && isNeedtoShowDefaultContent)
                        txtTitle.Text = objDataTable.Rows[0]["DefaultTitle"].ToString();
                }

            }

            if (Convert.ToBoolean(objDataTable.Rows[0]["ShowAuthor"]) != null)
            {
                bool showTitle = Convert.ToBoolean(objDataTable.Rows[0]["ShowAuthor"]);
                lblAuthorTitle.Visible = showTitle;
                txtAuthor.Visible = showTitle;

                if (objDataTable.Rows[0]["DefaultAuthor"] != null)
                {
                    if (Convert.ToBoolean(objDataTable.Rows[0]["ShowAuthor"]) && isNeedtoShowDefaultContent)
                        txtAuthor.Text = objDataTable.Rows[0]["DefaultAuthor"].ToString();
                }


            }

            if (Convert.ToBoolean(objDataTable.Rows[0]["ShowDate"]) != null)
            {
                bool showTitle = Convert.ToBoolean(objDataTable.Rows[0]["ShowDate"]);
                lblDateTtile.Visible = showTitle;
                txtDate.Visible = showTitle;
                if (Convert.ToDateTime(objDataTable.Rows[0]["DefaultDate"]) != null)
                {
                    if (Convert.ToBoolean(objDataTable.Rows[0]["ShowDate"]) && isNeedtoShowDefaultContent)
                        txtDate.Text = Convert.ToDateTime(objDataTable.Rows[0]["DefaultDate"]).ToString("dd/MM/yyyy");
                }

            }

            if (Convert.ToBoolean(objDataTable.Rows[0]["ShowContent"]) != null)
            {
                bool showTitle = Convert.ToBoolean(objDataTable.Rows[0]["ShowContent"]);
                lblContentTitle.Visible = showTitle;
                txtContent.Visible = showTitle;
                if (objDataTable.Rows[0]["DefaultContent"] != null)
                {
                    if (Convert.ToBoolean(objDataTable.Rows[0]["ShowContent"]) && isNeedtoShowDefaultContent)
                        txtContent.Text = objDataTable.Rows[0]["DefaultContent"].ToString();
                }

            }



            //  objCategories.getRows("*", "UserName ='" + txtUserName.Text.ToString().Trim() + "'  and Password = '" + cmscon.base64Encode(txtPassword.Text.ToString()) + "' ");

        }

        catch (Exception ex)
        {

        }

    }

    private void SetDataOnEdit(ContentObj objContent)
    {
        try
        {
            txtAuthor.Text = objContent.Author;

            txtDate.Text = objContent.Date.ToString("dd/MM/yyyy");
            txtTitle.Text = objContent.Title;
            txtContent.Text =objContent.Content;
            _categoryID = objContent.CategoryID; 
            //objContent.ContentTypeID = 1;//Will change
            lblURLofFile.Text = objContent.URL;          

        }
        catch (Exception ex)
        {


        }
    }

    #endregion

}