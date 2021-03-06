﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_ManageCategory : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    string _message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
          
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            LoadComboCategoryList();
          //  this.FillGrid(0);
            Session["LevelId"] = "0";
            // ControlsAppearence(false);
        }
    }

    #endregion Global Variable

    #region Events
    protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
        gvCategory.PageIndex = e.NewPageIndex;
        gvCategory.DataBind();
    }

    protected void lbtnEdit_Click(object sender, System.EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvCategory.Rows[row.RowIndex].FindControl("hdId");
        if (Convert.ToInt32(vId.Value) > 0)
        {
            try
            {
                Session["LevelId"] = vId.Value;
                ControlsAppearence(true);
                Fill_Controls(Convert.ToInt32(vId.Value));
                btnSave.Focus();
            }
            catch (Exception ex)
            {

            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
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

                    CategoryDetails obj = new CategoryDetails(osfcon.CONNECTIONSTRING);
                    obj.CategoryID = objCategories.CategoryID;
                    this.RefreshObject(obj);

                    obj.CategoryID = objCategories.CategoryID;

                    if (obj.insert())
                    {

                        lblMessage.Text = "Content Saved Successfully";
                        Clear_Controls();
                        ControlsAppearence(false);
                        FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
                        Session["LevelId"] = 0;
                    }
                    else
                    {
                        DisplayAlert("Error");
                    }
                }
                else
                {
                    lblMessage.Text = "Error";
                }

            }
            else
            {
                objCategories.CategoryID = Convert.ToInt32(Convert.ToInt32(Session["LevelId"]));
                if (objCategories.update())
                {

                    CategoryDetails obj = new CategoryDetails(osfcon.CONNECTIONSTRING);
                    obj.CategoryID = objCategories.CategoryID;
                    this.RefreshObject(obj);
                    
                    obj.CategoryID = objCategories.CategoryID;
                    obj.QueryExecute(string.Format("Delete from CategoryDetails WHERE CategoryID={0}",obj.CategoryID));
                    if (obj.insert())
                    {

                        lblMessage.Text = "Content Saved Successfully";
                        Clear_Controls();
                        ControlsAppearence(false);
                        FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));

                    }
                    else
                    {
                        lblMessage.Text = "Error";
                    }
                }
                else
                {
                    lblMessage.Text = "Error";
                }


            }
        }

    }

    protected void ddlSectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
    }
    #endregion

    #region Method
    private void FillGrid(int SectionTypeID)
    {
        DataTable objDataTable = new DataTable();
        CategoryDetails obj = new CategoryDetails(osfcon.CONNECTIONSTRING);

        string sql = "";
        if (SectionTypeID <=0)
            sql = string.Format("SELECT CategoryDetails.*, Categories.Description, Categories.SortOrder, Categories.CategoryID CID  FROM Categories LEFT JOIN CategoryDetails ON Categories.CategoryID = CategoryDetails.CategoryID  Order By  Categories.Description");
        else
            sql = string.Format("SELECT * FROM (SELECT CategoryID CID,Description,SortOrder, ParentID   FROM Categories WHERE CategoryTypeID={0}) A LEFT JOIN CategoryDetails ON  A.CID = CategoryDetails.CategoryID Order By A.Description ", SectionTypeID);

        objDataTable = osfcon.getRows(sql);
        if (objDataTable != null)
        {
            gvCategory.DataSource = objDataTable;
            gvCategory.DataBind();
        }
    }

    private void LoadComboCategoryList()
    {
        ddlSectionType.Items.Clear();


        SectionType objSectionType = new SectionType(osfcon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = osfcon.getRows("SELECT * FROM SectionType WHERE IsCategory = 1 order by description");


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

    private void Fill_Controls(int categoryID)
    {
        DataTable objCategoryDetails = new DataTable();
        DataTable objCategory = new DataTable();

        objCategory = osfcon.getRows(string.Format(@"SELECT * FROM Categories WHERE CategoryID={0}", categoryID));

        objCategoryDetails = osfcon.getRows(string.Format(@"SELECT * FROM CategoryDetails WHERE CategoryID={0}", categoryID));

        if (objCategoryDetails.Rows.Count > 0)
        {
            if (objCategory.Rows[0]["CategoryTypeID"] != null)
                ddlSectionType.SelectedValue = objCategory.Rows[0]["CategoryTypeID"].ToString();

            if (objCategory.Rows[0]["Description"] != null)
                txtCategoryName.Text = objCategory.Rows[0]["Description"].ToString();

            if (objCategory.Rows[0]["SortOrder"] != null)
                txtOrderSeq.Text = objCategory.Rows[0]["SortOrder"].ToString();

            if (objCategoryDetails.Rows[0]["TitleTitle"] != null)
                txtTitleTitle.Text = objCategoryDetails.Rows[0]["TitleTitle"].ToString();

            if (objCategoryDetails.Rows[0]["AuthorTitle"] != null)
                txtAuthorTitle.Text = objCategoryDetails.Rows[0]["AuthorTitle"].ToString();

            if (objCategoryDetails.Rows[0]["DateTitle"] != null)
                txtDateTitle.Text = objCategoryDetails.Rows[0]["DateTitle"].ToString();

            if (objCategoryDetails.Rows[0]["ContentTitle"] != null)
                txtContentTitle.Text = objCategoryDetails.Rows[0]["ContentTitle"].ToString();

            if (objCategoryDetails.Rows[0]["ShowTitle"] != null)
                CheckBoxShowTitle.Checked = Convert.ToBoolean(objCategoryDetails.Rows[0]["ShowTitle"]);

            if (objCategoryDetails.Rows[0]["ShowAuthor"] != null)
                CheckBoxShowAuthor.Checked = Convert.ToBoolean(objCategoryDetails.Rows[0]["ShowAuthor"]);

            if (objCategoryDetails.Rows[0]["ShowDate"] != null)
                CheckBoxShowDate.Checked = Convert.ToBoolean(objCategoryDetails.Rows[0]["ShowDate"]);

            if (objCategoryDetails.Rows[0]["ShowContent"] != null)
                CheckBoxShowContent.Checked = Convert.ToBoolean(objCategoryDetails.Rows[0]["ShowContent"]);

            if (objCategoryDetails.Rows[0]["DefaultTitle"] != null)
                txtDefaultTitle.Text = objCategoryDetails.Rows[0]["DefaultTitle"].ToString();

            if (objCategoryDetails.Rows[0]["DefaultAuthor"] != null)
                txtDefaultAuthor.Text = objCategoryDetails.Rows[0]["DefaultAuthor"].ToString();

            if (objCategoryDetails.Rows[0]["DefaultDate"] != null)
            {
                DateTime defaultDate = Convert.ToDateTime(objCategoryDetails.Rows[0]["DefaultDate"]);

                txtDefaultDate.Text = defaultDate.ToString("MM/dd/yyyy");
            }
            if (objCategoryDetails.Rows[0]["DefaultContent"] != null)
                txtDefaultContent.Text = objCategoryDetails.Rows[0]["DefaultContent"].ToString();

            if (objCategory.Rows[0]["IsActive"] != null)
                chkIsActive.Checked = Convert.ToBoolean(objCategory.Rows[0]["IsActive"]);

            if (objCategoryDetails.Rows[0]["ItemsPerPage"] != null)
                txtItemsPerPage.Text = objCategoryDetails.Rows[0]["ItemsPerPage"].ToString();
        }



    }

    private void Clear_Controls()
    {
        ddlSectionType.SelectedIndex = -1;

        txtCategoryName.Text = "";
        txtOrderSeq.Text = "";

        txtTitleTitle.Text = "";

        txtAuthorTitle.Text = "";

        txtDateTitle.Text = "";

        txtContentTitle.Text = "";

        CheckBoxShowTitle.Checked = false;

        CheckBoxShowAuthor.Checked = false;

        CheckBoxShowDate.Checked = false;

        CheckBoxShowContent.Checked = false;

        txtDefaultTitle.Text = "";

        txtDefaultAuthor.Text = "";

        txtDefaultDate.Text = "";

        txtDefaultContent.Text = "";

        chkIsActive.Checked = false;

        txtItemsPerPage.Text = "0";

    }

    private void ControlsAppearence(Boolean show)
    {
        show = true;
        //lblEditCategory.Visible = show;

        txtTitleTitle.Visible = show;

        txtAuthorTitle.Visible = show;

        txtDateTitle.Visible = show;

        txtContentTitle.Visible = show;

        CheckBoxShowTitle.Visible = show;

        CheckBoxShowAuthor.Visible = show;

        CheckBoxShowDate.Visible = show;

        CheckBoxShowContent.Visible = show;

        txtDefaultTitle.Visible = show;

        txtDefaultAuthor.Visible = show;

        txtDefaultDate.Visible = show;

        txtDefaultContent.Visible = show;

        chkIsActive.Visible = show;

        txtItemsPerPage.Visible = show;

        lblAuthorTitle.Visible = show;

        lblContentTitle.Visible = show;

        lblDateTitle.Visible = show;

        lblDefaultAuthor.Visible = show;

        lblDefaultContent.Visible = show;

        lblDefaultDate.Visible = show;

        lblDefaultTitle.Visible = show;

        lblItemsPerPage.Visible = show;

        lblMessage.Visible = show;

        lblTitleTitle.Visible = show;

        btnSave.Visible = show;
    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private string ValidateObject()
    {
        _message = "";

        if ((ddlSectionType.SelectedValue.ToString() == "-1"))
        {
            _message += "Please select a Category Type" + Environment.NewLine;
        }

        if ((txtCategoryName.Text == ""))
        {
            _message += "Please enter category Name" + Environment.NewLine;
        }

        if ((txtOrderSeq.Text == ""))
        {
            _message += "Please enter sort order" + Environment.NewLine;
        }
        if ((txtTitleTitle.Text == ""))
        {
            _message += "Please Enter Title" + Environment.NewLine;
        }
        if ((txtAuthorTitle.Text == ""))
        {
            _message += "Please Author Title" + Environment.NewLine;
        }

        if ((txtDateTitle.Text == ""))
        {
            _message += "Please Date Title" + Environment.NewLine;
        }
        if ((txtContentTitle.Text == ""))
        {
            _message += "Please Content Title" + Environment.NewLine;
        }


        //if (txtDefaultDate.Text.ToString().Trim().Length == 0)
        //{
        //    _message += "Please enter date" + Environment.NewLine;
        //}
        else
        {
            if (txtDefaultDate.Text.ToString().Trim().Length != 0 && !Utility.IsDate(txtDefaultDate.Text.ToString()))
            {
                _message += "Please enter valid date" + Environment.NewLine;
            }
        }
        return _message;
    }

    private Categories RefreshObject(Categories cat)
    {


        cat.SectionTypeID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString());
        cat.Description = txtCategoryName.Text;
        cat.IsLeaf = false;
        cat.SortOrder = Convert.ToInt32(txtOrderSeq.Text);
        cat.CreatedBy = Convert.ToInt32(Session["UserID"]);
        cat.CreatedDate = DateTime.Today;
        cat.ModifiedDate = DateTime.Today;
        cat.IsActive = chkIsActive.Checked;
        return cat;
    }

    private void RefreshObject(CategoryDetails obj)
    {

        obj.TitleTitle = txtTitleTitle.Text;

        obj.AuthorTitle = txtAuthorTitle.Text;

        obj.DateTitle = txtDateTitle.Text;

        obj.ContentTitle = txtContentTitle.Text;

        obj.ShowTitle = CheckBoxShowTitle.Checked;

        obj.ShowAuthor = CheckBoxShowAuthor.Checked;

        obj.ShowDate = CheckBoxShowDate.Checked;

        obj.ShowContent = CheckBoxShowContent.Checked;

        obj.DefaultTitle = txtDefaultTitle.Text;

        obj.DefaultAuthor = txtDefaultAuthor.Text;
        if (txtDefaultDate.Text != "")
            obj.DefaultDate = Convert.ToDateTime(txtDefaultDate.Text);

        obj.DefaultContent = txtDefaultContent.Text;

        obj.IsQuickLaunch = chkIsActive.Checked;

        obj.ItemsPerPage = Convert.ToInt32(txtItemsPerPage.Text == "" ? "0" : txtItemsPerPage.Text);

        obj.CategoryID = Convert.ToInt32(Session["LevelId"]);
    }

    #endregion
}