using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BasicData : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
           
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            LoadComboCategoryList();
            FillGrid(0);    
            if (Request.QueryString["BType"] != null)
            {
                string bType = Request.QueryString["BType"].ToString();
                if (bType == "Job")
                    ddlSectionType.SelectedIndex = 1;
                if (bType == "Location")
                    ddlSectionType.SelectedIndex = 3;
                if (bType == "Department")
                    ddlSectionType.SelectedIndex = 2;

                //txtBasicDataName.Visible = false;
                //txtDate.Visible = false;
                //txtUserDiefinedID.Visible = false;

                //lblAuthorTitle.Visible = false;
                //lblDateTtile.Visible = false;
                //lblUserNamePermission.Visible = false;
                //lblName.Visible = false;
                //btnSave.Visible = false;

                this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
                ddlSectionType.Enabled = false;
            }
          
             
        }
    }

    #endregion

    #region Events


    protected void ddlSectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
    }

    protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
        gvCategory.PageIndex = e.NewPageIndex;
        gvCategory.DataBind();
    }

    protected void lbtnEdit_Click(object sender, System.EventArgs e)
    {
        lblUserNamePermission.Text = "Edit Basic Data";
        btnSave.Text = "Edit";
        txtUserDiefinedID.Enabled = false;
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvCategory.Rows[row.RowIndex].FindControl("hdId");
        if (Convert.ToInt32(vId.Value) > 0)
        {
            try
            {
                Session["LevelId"] = vId.Value;
                this.FillControl(Convert.ToInt32(vId.Value));
                btnSave.Focus();
            }
            catch (Exception ex)
            {

            }
        }

    }

    protected void btnAddType_Click(object sender, EventArgs e)
    {
        //txtType.Visible = true;
        //btnSaveType.Visible = true;
    }

    protected void btnSaveType_Click(object sender, EventArgs e)
    {
        BasicData objBasicData = new BasicData(cmscon.CONNECTIONSTRING);
        this.MakeBasicType(objBasicData); 


        if (objBasicData.insert())
        {
            DisplayAlert("Basic Data Type Saved Successfully");
            lblUserNamePermission.Text = "New Basic Data";
        }
        else
        {
            DisplayAlert("Error");
        }

        //txtType.Visible = false;
        //btnSaveType.Visible = false;

        //txtType.Text = String.Empty;
        this.LoadComboCategoryList();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BasicData objBasicData = new BasicData(cmscon.CONNECTIONSTRING);
        this.MakeBasic(objBasicData);
        if (objBasicData.ParentID == -1)
        {
            DisplayAlert("Please select a Type");
            return;
        }

        if (txtUserDiefinedID.Text  == "")
        {
            DisplayAlert("Please enter a user defined id");
            return;
        }

        if (Convert.ToInt32(Session["LevelId"]) == 0)
        {

            DataTable dt = cmscon.getRows(string.Format("SELECT  COUNT(*) FROM BasicData WHERE UserDefinedID={0}", objBasicData.UserDefinedID));
            if (Convert.ToInt32(dt.Rows[0][0]) >= 1)
            {
                DisplayAlert("Same Userdefined ID exists");
                return;
            }
            if (objBasicData.insert())
            {
                DisplayAlert("Basic Data Saved Successfully");
                this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
                txtBasicDataName.Text = "";
                txtUserDiefinedID.Text = "";
          
            }
            else
            {
                DisplayAlert("Error");
            }

        }
        else
        {
            objBasicData.BasicDataID = Convert.ToInt32(Convert.ToInt32(Session["LevelId"]));
            if (objBasicData.update())
            {
                Session["LevelId"] = 0;
                DisplayAlert("Basic Data Saved Successfully");
                this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
                txtUserDiefinedID.Enabled = true;
                btnSave.Text = "Save";
                txtBasicDataName.Text = "";
                txtUserDiefinedID.Text = "";
          
            }

        }
        txtBasicDataName.Text = String.Empty;


    }

    #endregion

    #region Method
    private void FillControl(int basicDataID)
    {
        DataTable objCategory = new DataTable();

        objCategory = cmscon.getRows(string.Format(@"SELECT * FROM BasicData WHERE BasicDataID={0}", basicDataID));
        if (objCategory != null)
        {
            txtUserDiefinedID.Text = objCategory.Rows[0]["UserDefinedID"].ToString();
            txtBasicDataName.Text = objCategory.Rows[0]["Name"].ToString();
            ddlSectionType.SelectedValue = objCategory.Rows[0]["ParentID"].ToString();
        }

  
      
    }

    private void LoadComboCategoryList()
    {
        ddlSectionType.Items.Clear();

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = cmscon.getRows("SELECT * FROM SectionType WHERE UserDefinedID IN (14,15,16)");
            ddlSectionType.AppendDataBoundItems = true;
            ddlSectionType.Items.Add(new ListItem("--Select Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSectionType.Items.Add(new ListItem(dr["Description"].ToString(), dr["UserDefinedID"].ToString()));
            }
            //ddlCategoryListL1.SelectedValue = "-1";


        }
        catch (Exception ex)
        { 
        
        }
    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private void MakeBasicType(BasicData objBasicData)
    {      
        objBasicData.ParentID = 0;
        //objBasicData.Name = txtType.Text.Trim();
    }

    private void MakeBasic(BasicData objBasicData)
    {

        objBasicData.ParentID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString());
        objBasicData.Name = txtBasicDataName.Text.Trim();
        objBasicData.UserDefinedID = Convert.ToInt32(txtUserDiefinedID.Text);
    }

    private void FillGrid(int parentID)
    {
        DataTable objDataTable = new DataTable();
        CategoryDetails obj = new CategoryDetails(cmscon.CONNECTIONSTRING);

        string sql = "";
        if (parentID == 0)
            sql = string.Format("SELECT DISTINCT pv1.*, ISNULL(PV2.Name,'Basic Type') Type FROM BasicData pv1      JOIN BasicData pv2     ON pv1.ParentID = pv2.BasicDataID");
        else
            sql = string.Format("SELECT BasicData.*,SectionType.Description Type FROM BasicData JOIN SectionType on BasicData.ParentID = SectionType.UserDefinedID AND BasicData.ParentID ={0}",parentID); 

        objDataTable = cmscon.getRows(sql);
        if (objDataTable != null)
        {
            gvCategory.DataSource = objDataTable;
            gvCategory.DataBind();
        }
    }


    #endregion  

}