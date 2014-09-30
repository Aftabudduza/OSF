using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Admin_AdminContactLists : System.Web.UI.Page
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
            LoadComboDepartment();
            LoadComboPosition();
            LoadComboLocation();
            LoadContactType();
            btnSave.Visible = false;
        }
    }
    #endregion


    #region Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string msg = ValidateSearch();
        if (msg.Length <= 0)
        {
            string sql = "";
            if (txtRecordCount.Text == "")

                sql = "SELECT * FROM Users WHERE UserID >= 1 ";

            else
            {
                sql = string.Format(" SELECT top {0} * FROM Users WHERE UserID >= 1 ", Convert.ToInt32(txtRecordCount.Text));
            }

            if (txtSLastName.Text != "")
                sql += string.Format(" AND LastName LIKE '%{0}%'", txtSLastName.Text);

            if (txtSFirstName.Text != "")
                sql += string.Format(" AND FirstName LIKE '%{0}%'", txtSFirstName.Text);

            if (txtSChapter.Text != "")
                sql += string.Format(" AND Chapter = {0}", Convert.ToInt32(txtSChapter.Text));


            if (txtSHomeCity.Text != "")
                sql += string.Format(" AND HomeCity  LIKE '%{0}%'", txtSHomeCity.Text);

            if (txtsMinistryTitle.Text != "")
                sql += string.Format(" AND MinistryTitle  LIKE '%{0}%'", txtsMinistryTitle.Text);

            if (txtSMinistryLocation.Text != "")
                sql += string.Format(" AND MinistryLocation LIKE '%{0}%'", txtSMinistryLocation.Text);

            if (txtSMinistryCity.Text != "")
                sql += string.Format(" AND MinistryCity LIKE '%{0}%'", txtSMinistryCity.Text);

            if (txtSMinistryState.Text != "")
                sql += string.Format(" AND MinistryState LIKE '%{0}%'", txtSMinistryState.Text);

            if (txtsProfessionalYear.Text != "")
                sql += string.Format(" AND ProfessionalYear = {0}", Convert.ToInt32(txtsProfessionalYear.Text));

            //if (ddlSectionType.SelectedIndex == 0)
            //{
            //    msg += "Please select a Type" + Environment.NewLine;
            //}
            //int parentID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString());

            if (ddlSDepartment.SelectedIndex > 0)
            {
                int deptID = Convert.ToInt32(ddlSDepartment.SelectedValue.ToString());
                sql += string.Format(" AND DepartmentID = {0}", deptID);
            }

            if (ddlSPost.SelectedIndex > 0)
            {
                int jobID = Convert.ToInt32(ddlSPost.SelectedValue.ToString());
                sql += string.Format(" AND JobID = {0}", jobID);
            }

            if (ddlSLocation.SelectedIndex > 0)
            {
                int locID = Convert.ToInt32(ddlSLocation.SelectedValue.ToString());
                sql += string.Format(" AND LocationID = {0}", locID);
            }

            if (chkIsSSister.Checked || chkIsSStaff.Checked || chkIsSCompanion.Checked || chkIsSCommittee.Checked)
            {
                string subSql = "";
                if (chkIsSSister.Checked)
                {
                    subSql += string.Format(" AND IsSister = 1");
                }
                if (chkIsSStaff.Checked)
                {
                    subSql += string.Format(" AND IsStaff = 1");
                }
                if (chkIsSCompanion.Checked)
                {
                    subSql += string.Format(" AND IsCompanion = 1");
                }
                if (chkIsSCommittee.Checked)
                {
                    subSql += string.Format(" AND IsLayPerson = 1");
                }


                string queryN = "";

                if (subSql.Length > 0)
                    queryN = string.Format(@"SELECT Users.* FROM ({0}) Users JOIN UserPermissions up on USErs.UserID = up.UserID  {1}", sql, subSql);
                else
                    queryN = sql;
                sql = queryN;
            }

            int basicDataID = Convert.ToInt32(ddlContactType.SelectedValue.ToString());

            sql = string.Format(@"SELECT ISNULL(clm.isSelected,0) IsSelected, clm.ContactListID, clm.UserID, clm.BasicDataID,us.* FROM ({0}) Us LEFT JOIN ContactListMembers clm on Us.UserID = clm.userid AND clm.BasicDataID={1}", sql, basicDataID);


            Users uObj = new Users(osfcon.CONNECTIONSTRING);
            DataTable dt = osfcon.getRows(sql);
            if (dt != null )
            {
                
                btnSave.Visible = (dt.Rows.Count > 0);
                Session["UserData"] = dt;
                gvUser.DataSource = dt;
                gvUser.DataBind();
            }

        }
        else
        {

            //string sds = string.Format(@"TestT();",msg);        
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", sds, true);
            DisplayAlertUpdatePanel(msg);
            
          

        }
    }
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if (Session["UserData"] != null)
        {
            gvUser.DataSource = (DataTable)Session["UserData"];
            gvUser.DataBind();
            gvUser.PageIndex = e.NewPageIndex;
            gvUser.DataBind();
        }

     
    
    }

    protected void gvUser_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (Session["UserPermission"] != null)
        //{
        //    UserPermissions up = (UserPermissions)Session["UserPermission"];
        //    if (!up.IsDirectoryAdmin)
        //    {
        //        ((DataControlField)gvUser.Columns
        //            .Cast<DataControlField>()
        //            .Where(fld => fld.HeaderText == "Edit")
        //            .SingleOrDefault()).Visible = false;

        //        ((DataControlField)gvUser.Columns
        //        .Cast<DataControlField>()
        //        .Where(fld => fld.HeaderText == "Permission")
        //        .SingleOrDefault()).Visible = false;
        //    }
        //}
    }
    #endregion

    #region Method

    private void DisplayAlertUpdatePanel(string msg)
    {
        string str = string.Format("alert('{0}');", msg);
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", str, true);
    }

    private string ValidateSearch()
    {
        string _search = "";

        if (txtSChapter.Text != "" && !Regex.IsMatch(txtSChapter.Text, @"^\d+$"))
        {
            _search += "Chapter will be only one letter" +"\\n";
        }



        if (txtsProfessionalYear.Text != "" && !Regex.IsMatch(txtsProfessionalYear.Text, @"^\d+$"))
        {
            _search += "ProfessionalYear will be only one letter" +"\\n";
        }

        if (ddlContactType.SelectedIndex == 0)
        {
            _search += "Please select a contact type" + "\\n";
        }

        return _search;
    }

    private void FillGrid(int SectionTypeID)
    {

    }


    private void LoadContactType()
    {

        ddlContactType.Items.Clear();

        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(EnumSectionType.ContactList)));

            ddlContactType.AppendDataBoundItems = true;
            ddlContactType.Items.Add(new ListItem("--Select Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlContactType.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }


        }
        catch (Exception ex)
        { }
    }

    private void LoadComboDepartment()
    {

        ddlSDepartment.Items.Clear();

        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(EnumSectionType.Department)));

            ddlSDepartment.AppendDataBoundItems = true;
            ddlSDepartment.Items.Add(new ListItem("--Select Dept--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSDepartment.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }


        }
        catch (Exception ex)
        { }
    }

    private void LoadComboPosition()
    {
        ddlSPost.Items.Clear();

        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(EnumSectionType.Job)));

            ddlSPost.AppendDataBoundItems = true;
            ddlSPost.Items.Add(new ListItem("--Select Position--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSPost.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }


        }
        catch (Exception ex)
        { }
    }

    private void LoadComboLocation()
    {
        ddlSLocation.Items.Clear();

        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(EnumSectionType.Location)));

            ddlSLocation.AppendDataBoundItems = true;
            ddlSLocation.Items.Add(new ListItem("--Select Location--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSLocation.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }


        }
        catch (Exception ex)
        { }
    }
    #endregion
    protected void ddlContactType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            int basicDataID = Convert.ToInt32(ddlContactType.SelectedValue.ToString());

        foreach (GridViewRow row in gvUser.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("chkSelect");
            HiddenField uID = (HiddenField)row.FindControl("uID");

            try
            {
                if (cb != null && cb.Checked)
                {
                    Utility.QueryExecuteExceptionHandled(string.Format(@"DELETE FROM ContactListMembers WHERE UserID={0} AND BasicDataID={1}", Convert.ToInt32(uID.Value == "" ? "0" : uID.Value), basicDataID));
                    Utility.QueryExecuteExceptionHandled(string.Format(@"INSERT INTO ContactListMembers ([UserID],[BasicDataID],[IsSelected]) VALUES({0},{1},{2})", Convert.ToInt32(uID.Value), basicDataID, 1));
                }
                DisplayAlertUpdatePanel("Saved successfully");
            }
            catch (Exception ex)
            {
                DisplayAlertUpdatePanel(string.Format("Error Occured due to: {0}",ex.Message));
            }
        }
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvUser.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gvUser.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSelect");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
}