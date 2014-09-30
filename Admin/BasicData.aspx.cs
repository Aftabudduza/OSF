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
            txtEmailPage.Visible = false;
            txtHomePage.Visible = false;
            lblEmailPage.Visible = false;
            lblHomePage.Visible = false;
            LoadBascDataTypeList();
          //  FillGrid(0);    
            if (Request.QueryString["BType"] != null)
            {
                string bType = Request.QueryString["BType"].ToString();
                if (bType == "Job")
                    ddlSectionType.SelectedIndex = 1;
                if (bType == "Location")
                    ddlSectionType.SelectedIndex = 3;
                if (bType == "Department")
                    ddlSectionType.SelectedIndex = 2;
                if (bType == "EmailIspMgt")
                    ddlSectionType.SelectedIndex = 4;

                if (bType == "ContactList")
                    ddlSectionType.SelectedIndex = 5;

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
  
        if (Session["BasicData"] != null)
        {
            gvCategory.DataSource = (DataTable)Session["BasicData"];
            gvCategory.DataBind();
            gvCategory.PageIndex = e.NewPageIndex;
            gvCategory.DataBind();
        }

     
    }

    protected void lbtnEdit_Click(object sender, System.EventArgs e)
    {
        lblUserNamePermission.Text = "Edit Basic Data";
        btnSave.Text = "Edit";
      
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
        BasicData objBasicData = new BasicData(osfcon.CONNECTIONSTRING);
        this.MakeBasicType(objBasicData); 


        if (objBasicData.insert())
        {
            DisplayAlert("Basic Data Type Saved Successfully");
            lblUserNamePermission.Text = "";
        }
        else
        {
            DisplayAlert("Error");
        }

        //txtType.Visible = false;
        //btnSaveType.Visible = false;

        //txtType.Text = String.Empty;
        this.LoadBascDataTypeList();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BasicData objBasicData = new BasicData(osfcon.CONNECTIONSTRING);

        if (ValidateInput().Length <= 0)
        {
            this.MakeBasic(objBasicData);
            if (Convert.ToInt32(Session["LevelId"]) == 0)
            {
                if (objBasicData.insert())
                {
                    DisplayAlert("Basic Data Saved Successfully");
                    this.FillGrid(Convert.ToInt32(ddlSectionType.SelectedValue.ToString()));
                    txtBasicDataName.Text = "";


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

                    btnSave.Text = "Save";
                    txtBasicDataName.Text = "";


                }

            }
            txtBasicDataName.Text = String.Empty;
            txtEmailPage.Text = string.Empty;
            txtHomePage.Text = string.Empty;
        }

    }

    #endregion

    #region Method
    private string ValidateInput()
    {
        string msg = "";
        if (ddlSectionType.SelectedIndex == 0)
        {
            msg += "Please select a Type" + Environment.NewLine;             
        }
        int parentID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString());
        if (parentID == (int)EnumSectionType.EmailIspMgt)
        {
            if (txtHomePage.Text == "")
            {
                msg += "Please Enter HomePage" + Environment.NewLine;  
            }

            if (txtEmailPage.Text == "")
            {
                msg += "Please Enter EmailPage" + Environment.NewLine;
            }
        }


        return msg;
    }

    private void FillControl(int basicDataID)
    {
        DataTable dtCategory = new DataTable();

        dtCategory = osfcon.getRows(string.Format(@"SELECT * FROM BasicData WHERE BasicDataID={0}", basicDataID));
        if (dtCategory != null && dtCategory.Rows.Count > 0)
        {

            if(dtCategory.Rows[0]["Name"] != null)
            txtBasicDataName.Text = dtCategory.Rows[0]["Name"].ToString();

            if (dtCategory.Rows[0]["ParentID"] != null)
            ddlSectionType.SelectedValue = dtCategory.Rows[0]["ParentID"].ToString();


            if (Convert.ToInt32(ddlSectionType.SelectedValue) == (int)EnumSectionType.EmailIspMgt)
            {
                if (dtCategory.Rows[0]["EmailPage"] != null)
                    txtEmailPage.Text = dtCategory.Rows[0]["EmailPage"].ToString();


                if (dtCategory.Rows[0]["HomePage"] != null)
                    txtHomePage.Text = dtCategory.Rows[0]["HomePage"].ToString();

            }

        }
      
    }

    private void LoadBascDataTypeList()
    {
        ddlSectionType.Items.Clear();

        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM SectionType WHERE SectionTypeID IN ({0},{1},{2},{3},{4})", (int)EnumSectionType.Job, (int)EnumSectionType.Department, (int)EnumSectionType.Location, (int)EnumSectionType.EmailIspMgt,(int)EnumSectionType.ContactList));
            ddlSectionType.AppendDataBoundItems = true;
            ddlSectionType.Items.Add(new ListItem("--Select Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSectionType.Items.Add(new ListItem(dr["Description"].ToString(), dr["SectionTypeID"].ToString()));
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
        if (objBasicData.ParentID == (int)EnumSectionType.EmailIspMgt)
        {
            objBasicData.EmailPage = txtEmailPage.Text;
            objBasicData.HomePage = txtHomePage.Text;

        }
    
    }

    private void FillGrid(int parentID)
    {
        if (parentID != (int)EnumSectionType.EmailIspMgt)
        {
            txtEmailPage.Visible = false;
            txtHomePage.Visible = false;
            lblEmailPage.Visible = false;
            lblHomePage.Visible = false;
        }
        else
        {
            txtEmailPage.Visible = true;
            txtHomePage.Visible = true;
            lblEmailPage.Visible = true;
            lblHomePage.Visible = true;
        }

        DataTable objDataTable = new DataTable();
        CategoryDetails obj = new CategoryDetails(osfcon.CONNECTIONSTRING);

        string sql = "";
        if (parentID == 0)
            sql = string.Format("SELECT DISTINCT pv1.*, ISNULL(PV2.Name,'Basic Type') Type FROM BasicData pv1      JOIN BasicData pv2     ON pv1.ParentID = pv2.BasicDataID");
        else
            sql = string.Format("SELECT BasicData.*,SectionType.Description Type FROM BasicData JOIN SectionType on BasicData.ParentID = SectionType.SectionTypeID AND BasicData.ParentID ={0}",parentID); 

        objDataTable = osfcon.getRows(sql);
        Session["BasicData"] = objDataTable;
        if (objDataTable != null)
        {
            gvCategory.DataSource = objDataTable;
            gvCategory.DataBind();
        }
    }


    #endregion  

}