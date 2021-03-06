﻿using System;
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
            btnSubmit.Visible = false;

            if (Request.QueryString["ID"] != null)
            {
                LoadComboCategoryTypeList();
               // LoadComboCategoryList();
                ContentObj cObj = new ContentObj(osfcon.CONNECTIONSTRING);


                int contentid = Convert.ToInt32(Request.QueryString["ID"]);
                cObj = cObj.getRecordFromID(contentid);
                DesignLabeslAndOthers(cObj.CategoryID);
                SetDataOnEdit(cObj);

            

                DataTable catDt =osfcon.getRows(string.Format("SELECT * FROM Categories WHERE CategoryID={0}",cObj.CategoryID));
                if (catDt != null && catDt.Rows.Count > 0)
                {
                    int sectiontypeID = Convert.ToInt32(catDt.Rows[0]["CategoryTypeID"]);
                    Session["SectionTypeIDAC"] = sectiontypeID;
                    ddlSectionType.SelectedValue = sectiontypeID.ToString();
                    this.LoadComboCategoryListWithSelectedIndex(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()),cObj.CategoryID);
           
                }


                //ddlCategoryList.SelectedValue =  cObj.CategoryID.ToString();
            }
        }
        else if (!IsPostBack && Request.QueryString["Method"] != null && Request.QueryString["Method"] == "DeleteContent")
        {
            UserSectionPermission usp = new UserSectionPermission(osfcon.CONNECTIONSTRING);
            usp.QueryExecute(string.Format("UPDATE Content SET IsActive = 0 WHERE ContentID= {0}", Convert.ToInt32(Request.QueryString["ID"])));

            DataTable dt = osfcon.getRows(string.Format("SELECT CategoryTypeID FROM Categories WHERE CategoryID ={0}", Convert.ToInt32(Request.QueryString["CatID"])));
            int secID = Convert.ToInt32(dt.Rows[0][0]);
            DisplayAlert("Content deleted Successfully");
            if (secID == (int)EnumSectionType.News || secID == (int)EnumSectionType.Calender || secID == (int)EnumSectionType.BulletinBoard)
                Response.Redirect(string.Format("../Contents/NewsorContents.aspx?SectionTypeID={0}", secID));
            else
                Response.Redirect(string.Format("../Contents/BoxContents.aspx?SectionTypeID={0}", secID));

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
                LoadComboCategoryTypeList();

                ContentObj cObj = new ContentObj(osfcon.CONNECTIONSTRING);
                int contentid = Convert.ToInt32(Request.QueryString["ID"]);
                cObj = cObj.getRecordFromID(contentid);
  
                DataTable catDt = osfcon.getRows(string.Format("SELECT * FROM Categories WHERE CategoryID={0}", cObj.CategoryID));
                if (catDt != null && catDt.Rows.Count > 0)
                {
                    int sectiontypeID = Convert.ToInt32(catDt.Rows[0]["CategoryTypeID"]);
                    Session["SectionTypeIDAC"] = sectiontypeID;
                    ddlSectionType.SelectedValue = sectiontypeID.ToString();
                }


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

                        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName;
                        string nFile = Path.Combine(filePath, fileName);
                        Session["FileName"] = fileName;
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

                        ContentObj objContent = new ContentObj(osfcon.CONNECTIONSTRING);
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
                        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName;
                         nFile = Path.Combine(filePath,  fileName);
                       // nFile = Path.Combine(filePath, "ContentFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName);
                         Session["FileName"] = fileName;
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
                    ContentObj objContent = new ContentObj(osfcon.CONNECTIONSTRING);

                    this.SetData(objContent);
                    objContent.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    objContent.ModifiedOn = DateTime.UtcNow;



                    if (ddlCategoryList.SelectedValue != "-1")
                    {
                        objContent.CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());

                    }
                    DataTable dt = osfcon.getRows(string.Format("SELECT CategoryTypeID FROM Categories WHERE CategoryID ={0}", objContent.CategoryID));


                    int secID = Convert.ToInt32(dt.Rows[0][0]);
                    objContent.ContentID = Convert.ToInt32(Request.QueryString["ID"]);
                    if (objContent.update())
                    {
                        Session["FileName"] = null;
                        DisplayAlert("Content Saved Successfully");
                        if (secID == (int)EnumSectionType.News || secID == (int)EnumSectionType.Calender || secID == (int)EnumSectionType.BulletinBoard)
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
        CheckPostPermission();
    }

    #endregion

    #region Method
    private void CheckPostPermission()
    {
              if (ddlCategoryList.SelectedValue != "-1")
        {
            int CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());
            DesignLabeslAndOthers(CategoryID);
            DataTable TempPerm = osfcon.getRows(string.Format("SELECT * FROM UserSectionPermission WHERE UserID ={0} AND CategoryID={1} AND IsContentAdmin = 1 AND IsContentAdmin is not null",Convert.ToInt32(Session["UserID"]), CategoryID));
            if (!(TempPerm != null && TempPerm.Rows.Count > 0))
            {
                DisplayAlert("Your are not permitted to post in this category");
                btnSubmit.Visible = false;
            
            }
            else
                btnSubmit.Visible = true;


        }
    }

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

        if ((txtDate.Text == ""))
        {
            _message += "Please enter Date" + Environment.NewLine;
        }

        return _message;
    }

    private void LoadComboCategoryList(int sectionTypeID)
    {
        ddlCategoryList.Items.Clear();


        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM Categories Where ParentID=0 AND CategoryTypeID ={0} Order by Description", sectionTypeID));

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

    private void LoadComboCategoryListWithSelectedIndex(int sectionTypeID, int catID)
    {
        ddlCategoryList.Items.Clear();


        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable dtCategory = osfcon.getRows(string.Format("SELECT * FROM Categories Where ParentID=0 AND CategoryTypeID ={0} Order by Description", sectionTypeID));

            ddlCategoryList.AppendDataBoundItems = true;
            ddlCategoryList.Items.Add(new ListItem("--Select Category--", "-1"));
            int i = 0, index =0;
            foreach (DataRow dr in dtCategory.Rows)
            {
                i++;

                this.ddlCategoryList.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
                if (dr["CategoryID"].ToString() == catID.ToString())
                    index = i;

            }
             ddlCategoryList.SelectedIndex = index;
            ddlCategoryList.SelectedValue = catID.ToString();
            CheckPostPermission();
        }
        catch (Exception ex)
        { }
    }

    private void SetData(ContentObj objContent)
    {
        try

        {
            //SEt Chapter ID when area chapter

 
            int sectionTypeID = 0;
            if (ddlSectionType.SelectedValue != "-1")
            {
                sectionTypeID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString());

            }


            if (sectionTypeID == (int)EnumSectionType.AreaChapter)
            {
                //    objContent.Chapter = 

                if (Session["User"] != null)
                {
                    Users u = (Users)Session["User"];
                    objContent.Chapter = u.Chapter;

                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }


            }
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
        CategoryDetails objCategoryDetail = new CategoryDetails(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM CategoryDetails WHERE CategoryID={0}",categoryID));
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
                        txtDate.Text = Convert.ToDateTime(objDataTable.Rows[0]["DefaultDate"]).ToString("MM/dd/yyyy");
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

            txtDate.Text = objContent.Date.ToString("MM/dd/yyyy");
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

    private void LoadComboCategoryTypeList()
    {
        ddlSectionType.Items.Clear();


        SectionType objSectionType = new SectionType(osfcon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = osfcon.getRows("SELECT * FROM SectionType WHERE IsCategory = 1 Order by Description");


            ddlSectionType.AppendDataBoundItems = true;
            ddlSectionType.Items.Add(new ListItem("--Category Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSectionType.Items.Add(new ListItem(dr["Description"].ToString(), dr["SectionTypeID"].ToString()));
            }
            //ddlSectionType.SelectedValue = "-1";


        }
        catch (Exception ex)
        { }
    }
    #endregion

    protected void ddlSectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadComboCategoryList(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
    }
}