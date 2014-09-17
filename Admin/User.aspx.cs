using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Admin_User : System.Web.UI.Page
{

    #region Global Variable & PageLoad
    bool _isEdit = false;
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
            Session["UserIDEdit"] = "";
            this.FillGrid(0);
            if (Session["UserPermission"] != null)
            {
                UserPermissions up = (UserPermissions)Session["UserPermission"];
                btnAddUser.Visible = up.IsDirectoryAdmin;
            }
        }

    }
    #endregion

    #region Events
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(0);
        gvUser.PageIndex = e.NewPageIndex;
        gvUser.DataBind();
    }

    protected void lbtnEdit_Click(object sender, System.EventArgs e)
    {
        _isEdit = true;
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvUser.Rows[row.RowIndex].FindControl("hdId");
        Session["UserIDEdit"] = vId.Value;
        ImageButton btndetails = sender as ImageButton;
        Users uObj = new Users(cmscon.CONNECTIONSTRING);
        DataTable dt = cmscon.getRows(string.Format("SELECT * FROM users WHERE UserID={0}", Convert.ToInt32(vId.Value)));
        if (dt != null)
        {
            this.FillControls(dt);
            this.ModalPopupExtender1.Show();
        }
        else
        {
            DisplayAlert("No Details Data Fount");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string message = "";

        Users uObj = new Users(cmscon.CONNECTIONSTRING);


        message = ValidateObject();
        if (message.Length > 0)
        {
            DisplayAlert(message);
           // return;
        }

        else
        {
            this.MakeObject(uObj);
            if (Convert.ToString( Session["UserIDEdit"]) == "")
            {
                uObj.Password = uObj.Username = uObj.FirstName[0] + uObj.LastName;
                if (uObj.insert())
                {
                    DisplayAlert("User saved successfully");
                }
      
            }
            else
            {
                uObj.UserID = Convert.ToInt32(Session["UserIDEdit"] == "" ? 0 : Session["UserIDEdit"]);
                uObj.update();
                Session["UserIDEdit"] = null;

            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ValidateSearch().Length <= 0)
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
                sql += string.Format(" AND HomeCity = {0}", Convert.ToInt32(txtSHomeCity.Text));

            if (txtsMinistryTitle.Text != "")
                sql += string.Format(" AND MinistryTitle = {0}", Convert.ToInt32(txtsMinistryTitle.Text));

            if (txtSMinistryLocation.Text != "")
                sql += string.Format(" AND MinistryLocation = {0}", Convert.ToInt32(txtSMinistryLocation.Text));

            if (txtSMinistryCity.Text != "")
                sql += string.Format(" AND MinistryCity = {0}", Convert.ToInt32(txtSMinistryCity.Text));

            if (txtSMinistryState.Text != "")
                sql += string.Format(" AND MinistryState = {0}", Convert.ToInt32(txtSMinistryState.Text));

            if (txtsProfessionalYear.Text != "")
                sql += string.Format(" AND ProfessionalYear = {0}", Convert.ToInt32(txtsProfessionalYear.Text));

            Users uObj = new Users(cmscon.CONNECTIONSTRING);
            DataTable dt = cmscon.getRows(sql);
            if (dt != null)
            {
                gvUser.DataSource = dt;
                gvUser.DataBind();
            }

        }
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        _isEdit = false;
        Session["UserIDEdit"] = null;
        txtFirstName.Text ="";
        txtLastName.Text ="";
        //txtD.Text = dt.Rows[0]["DEPT***"].ToString();
        //txtHomePhone.Text = dt.Rows[0]["POS****"].ToString();
        txtChapter.Text ="";
        txtCompanionType.Text ="";
        txtProfessionalYear.Text="";
        txtMI.Text="";
        //txtMI.Text ="";

        txtHomeStreet1.Text ="";
        txtHomeStreet1.Text ="";
        txtHomeCity.Text ="";
        txtHomeState.Text ="";
        txtHomeZip.Text="";
        txtHomeEmail.Text ="";
        txtHomePhone.Text ="";

        //txtMinistryTitle.Text = dt.Rows[0]["MinistryTitle"].ToString();
        txtMinistryClassification.Text ="";
        txtMinistryLocation.Text="";
        txtMinistryStreet1.Text ="";
        txtMinistryStreet2.Text ="";
        txtMinistryCity.Text ="";
        txtMinistryCountry.Text ="";
        txtMinistryZip.Text ="";
        txtMinistryPhone.Text ="";
        txtMinistryFax.Text ="";
        txtMinistryEmail.Text ="";

        txtMinistry2Title.Text="";
        txtMinistry2Classification.Text="";
        txtMinistry2Location.Text ="";
        txtMinistry2Street1.Text ="";
        txtMinistry2Street2.Text ="";
        txtMinistry2City.Text ="";
        txtMinistry2Country.Text ="";
        txtMinistry2Zip.Text="";
        txtMinistry2Phone.Text ="";
        txtMinistry2Fax.Text ="";
        txtMinistry2Email.Text ="";
        //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //HiddenField vId = (HiddenField)gvUser.Rows[row.RowIndex].FindControl("hdId");
        //Session["UserIDEdit"] = vId.Value;
        this.ModalPopupExtender1.Show();
    }

    protected void lbtnPermission_Click(object sender, EventArgs e)
    {
        _isEdit = true;


        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvUser.Rows[row.RowIndex].FindControl("hdId");
        Session["UserIDEdit"] = vId.Value;
        ImageButton btndetails = sender as ImageButton;

        Users uObj = new Users(cmscon.CONNECTIONSTRING);
        DataTable userData = cmscon.getRows(string.Format("SELECT * FROM users WHERE UserID={0}", Convert.ToInt32(vId.Value)));

        if (userData != null)
        {
            //  this.FillControls(dt);
            lblUserNamePermission.Text = userData.Rows[0]["FirstName"].ToString() + " " + userData.Rows[0]["LastName"].ToString() + "'s Security Info";
            txtUserNamePermission.Text = userData.Rows[0]["UserName"].ToString();
            Session["UserName"] = userData.Rows[0]["UserName"].ToString();

            DataTable perMissionData = cmscon.getRows(string.Format("SELECT * FROM UserPermissions WHERE UserID={0}", Convert.ToInt32(vId.Value)));
            if (perMissionData == null || perMissionData.Rows.Count <=0)
            {                
                Session["IsNew"] = true;               
            }
            else
            {
                Session["IsNew"] = false;
                this.FillPermissionControls(perMissionData);               
            }
        }
        else
        {
            DisplayAlert("No Details Data Fount");
        }

        DataTable userSectionPermissionData = cmscon.getRows(string.Format("SELECT * FROM UserSectionPermission WHERE UserID={0}", Convert.ToInt32(vId.Value)));
        if (userSectionPermissionData != null)
        {
            if (userSectionPermissionData == null || userSectionPermissionData.Rows.Count <= 0)
            {
                GenerateTreeView();           
            }
            else
            {
                GenerateTreeViewEdit();
              //this.FillSectionPermissionControls(userSectionPermissionData);
                
            }
        }
        else
        {
            DisplayAlert("No Details Data Fount");
        }

        this.ModalPopupExtender2.Show();

    }

    protected void btnPermissionClose_Click(object sender, EventArgs e)
    {

    }

    protected void btnPermissionSave_Click(object sender, EventArgs e)
    {
        UserPermissions uObj = new UserPermissions(cmscon.CONNECTIONSTRING);
    }


    protected void btnAddPermission_Click(object sender, EventArgs e)
    {
        string message = "";
        int a =LinksTreeView.Nodes.Count;
        UserPermissions uObj = new UserPermissions(cmscon.CONNECTIONSTRING);
        this.MakePermissionObject(uObj);
        List<UserSectionPermission> usPermissions = new List<UserSectionPermission>();
        MakeSectionPermissionObject(usPermissions);

        if (Convert.ToBoolean(Session["IsNew"]) == true)
        {
            uObj.UserID = Convert.ToInt32(Session["UserIDEdit"]);

            if (uObj.insert())
            {
     
            }
            else
            {
                DisplayAlert("Error");
            }
        }
        else
        {

            uObj.UserID = Session["UserIDEdit"].ToString() == "" ? 0 : Convert.ToInt32(Session["UserIDEdit"].ToString());
            uObj.update();

            if (chkResetPassword.Checked)
            { //QueryExecute
                Users user = new Users(cmscon.CONNECTIONSTRING);
                user.QueryExecute(string.Format("UPDATE Users SET Password='{0}' WHERE UserID={1}",txtUserNamePermission.Text,uObj.UserID));
            }
        }

        UserSectionPermission usp = new UserSectionPermission(cmscon.CONNECTIONSTRING);
        int userID =  Convert.ToInt32(Session["UserIDEdit"] == "" ? 0 : Session["UserIDEdit"]);
        usp.QueryExecute(string.Format("DELETE  FROM UserSectionPermission WHERE UserID = {0}",userID));
        foreach (UserSectionPermission u in usPermissions)
        {
            usp.CategoryID = u.CategoryID;
            usp.SectionID = u.SectionID;
            usp.UserID = u.UserID;
            usp.IsSection = u.IsSection;
            usp.HasPermission = u.HasPermission;
            if (usp.CategoryID > 0)
            {
                usp.insert();
                //if (usp.HasPermission)
                //{
                //    usp.QueryExecute(string.Format("UPDATE UserSectionPermission SET HasPermission=1 WHERE SectionID={0} AND IsSection=1 AND UserID={1}",usp.SectionID,usp.UserID));
                //}
            }
            else
                usp.insertNoCategory();
        }
        Session["UserIDEdit"] = null;

    }
    protected void btnPermissionCancel_Click(object sender, EventArgs e)
    {

    }


    protected void lbtnView_Click(object sender, System.EventArgs e)
    {
        _isEdit = true;
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvUser.Rows[row.RowIndex].FindControl("hdId");
        Session["UserIDEdit"] = vId.Value;
        int nId = vId.Value == "" ? 0 : Convert.ToInt32(vId.Value);
        if (nId > 0)
        {
            ImageButton btndetails = sender as ImageButton;
            Users uObj = new Users(cmscon.CONNECTIONSTRING);
            DataTable dt = cmscon.getRows(string.Format("SELECT u.*, Job = (SELECT Name From BasicData WHERE BasicDataID=u.JobID), Department = (SELECT Name From BasicData WHERE BasicDataID=u.DepartmentID), Location = (SELECT Name From BasicData WHERE BasicDataID=u.LocationID) FROM users u WHERE u.UserID={0}", Convert.ToInt32(vId.Value)));
            if (dt != null)
            {
                this.FillControlsView(dt);
                this.ModalPopupExtender3.Show();
            }
            else
            {
                DisplayAlert("No Details Data Fount");
            }

        }


    }




    protected void LinksTreeView_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        //TreeNodeCollection ts = null;
        //if (e.Node.Parent == null)
        //{
        //    ts = ((TreeView)sender).Nodes;
        //}
        //else
        //{
        //    ts = e.Node.Parent.ChildNodes;
        //}
        //foreach (TreeNode node in ts)
        //{
        //    if (node.Value != e.Node.Value)
        //    {
        //        node.Collapse();
        //    }
        //}
    }

    protected void gvUser_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (Session["UserPermission"] != null)
        {
            UserPermissions up = (UserPermissions)Session["UserPermission"];
            if (!up.IsDirectoryAdmin)
            {
                ((DataControlField)gvUser.Columns
                    .Cast<DataControlField>()
                    .Where(fld => fld.HeaderText == "Edit")
                    .SingleOrDefault()).Visible = false;

                ((DataControlField)gvUser.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "Permission")
                .SingleOrDefault()).Visible = false;
            }
        }
    }
    protected void btnUserViewExit_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Method
    private void MakePermissionObject(UserPermissions uObj)
    {
        uObj.CanLogIn = chkCanLogin.Checked;
        uObj.IsSister = chkIsSister.Checked;
        uObj.IsStaff = chkIsStuff.Checked;
        uObj.IsCompanion = chkIsCompanion.Checked;
        uObj.IsHidden = chkIsHidden.Checked;
        uObj.IsLayPerson = chkIsCommitteMember.Checked;//Lay Person
        uObj.IsGlobalAdmin = chkIsGlobalAdmin.Checked;
        uObj.IsGlobalContentAdmin = chkIsGlobalContentAdmin.Checked;
        //uObj.IsChapterContentAdmin = chkIsACContentAdmin.Checked;
        //uObj.IsChapterForumAdmin = chkISACForumAdmin.Checked;
        //uObj.IsChapterDirectoryAdmin = chkIsACDirectoryAdmin.Checked;
        uObj.IsDirectoryAdmin = chkIsGlobalDirectoryAdmin.Checked;
        //uObj.IsChapterDirectivesAdmin = isChapterDirectiveAdmin.Checked;
        //uObj.IsCompanionAdmin = chkIsCompanionAdmin.Checked;
        uObj.IsDelete = chkCanLogin.Checked;

    }

    private void MakeSectionPermissionObject(List<UserSectionPermission> uObj)
    {

        foreach (ListItem li in chkHeaderList.Items)
        {
            UserSectionPermission up = new UserSectionPermission();
            up.UserID = Convert.ToInt32(Session["UserIDEdit"]);
            up.CategoryID = 0;
            up.SectionID = Convert.ToInt32(li.Value);
            up.HasPermission = li.Selected;
            up.IsSection = true;
            uObj.Add(up);
        }

        foreach (TreeNode node in LinksTreeView.Nodes)
        {
            UserSectionPermission up = new UserSectionPermission();

            if (node.Parent == null) //its parent Node
            {
                //up.UserID = Convert.ToInt32(Session["UserIDEdit"]);
                //up.CategoryID = 0;
                //up.SectionID =Convert.ToInt32(node.Value);
                //up.HasPermission = node.Checked;
                //up.IsSection = true;
                //uObj.Add(up);

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode cnode in node.ChildNodes)
                    {
                        up = new UserSectionPermission();
                        up.UserID = Convert.ToInt32(Session["UserIDEdit"]);
                        up.CategoryID = Convert.ToInt32(cnode.Value);
                        up.SectionID = Convert.ToInt32(node.Value);
                        up.HasPermission = cnode.Checked;
                        uObj.Add(up);
                    }
                }
            }
        }



    }

    private void FillGrid(int SectionTypeID)
    {

    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private string ValidateObject()
    {
      string  _message = "";

      if ((txtMI.Text.Length > 1 ))
      {
          _message += "MI will be only one lett" + Environment.NewLine;
      }
      if (!Regex.IsMatch(txtChapter.Text, @"^\d+$"))
      {
          _message += "Chapter will be only one letter" + Environment.NewLine;
      }

      if (!Regex.IsMatch(txtProfessionalYear.Text, @"^\d+$"))
      {
          _message += "ProfessionalYear will be only one letter" + Environment.NewLine;
      }


        //if ((txtCategoryName.Text == ""))
        //{
        //    _message += "Please enter category Name" + Environment.NewLine;
        //}

        //if ((txtOrderSeq.Text == ""))
        //{
        //    _message += "Please enter sort order" + Environment.NewLine;
        //}



        return _message;
    }

    private void MakeObject(Users uObj)
    {
        uObj.FirstName = txtFirstName.Text;
        uObj.LastName = txtLastName.Text;
        //txtD.Text = dt.Rows[0]["DEPT***"].ToString();
        //txtHomePhone.Text = dt.Rows[0]["POS****"].ToString();
        uObj.Chapter = Convert.ToInt32(txtChapter.Text);
        uObj.CompanionType = txtCompanionType.Text;
        uObj.ProfessionalYear = Convert.ToInt32(txtProfessionalYear.Text);
        uObj.MI = txtMI.Text;
        //txtMI.Text = dt.Rows[0]["Loc"].ToString();

        uObj.HomeStreet1 = txtHomeStreet1.Text;
        uObj.HomeStreet1 = txtHomeStreet1.Text;
        uObj.HomeCity = txtHomeCity.Text;
        uObj.HomeState = txtHomeState.Text;
        uObj.HomeZip = txtHomeZip.Text;
        uObj.HomeEmail = txtHomeEmail.Text;
        uObj.HomePhone = txtHomePhone.Text;

        //uObj.MinistryTitle = txtMinistryTitle.Text;
        uObj.MinistryClassification = txtMinistryClassification.Text;
        uObj.MinistryLocation = txtMinistryLocation.Text;
        uObj.MinistryStreet1 = txtMinistryStreet1.Text;
        uObj.MinistryStreet2 = txtMinistryStreet2.Text;
        uObj.MinistryCity = txtMinistryCity.Text;
        uObj.MinistryCountry = txtMinistryCountry.Text;
        uObj.MinistryZip = txtMinistryZip.Text;
        uObj.MinistryPhone = txtMinistryPhone.Text;
        uObj.MinistryFax = txtMinistryFax.Text;
        uObj.MinistryEmail = txtMinistryEmail.Text;

        uObj.Ministry2Title = txtMinistry2Title.Text;
        uObj.Ministry2Classification = txtMinistry2Classification.Text;
        uObj.Ministry2Location = txtMinistry2Location.Text;
        uObj.Ministry2Street1 = txtMinistry2Street1.Text;
        uObj.Ministry2Street2 = txtMinistry2Street2.Text;
        uObj.Ministry2City = txtMinistry2City.Text;
        uObj.Ministry2Country = txtMinistry2Country.Text;
        uObj.Ministry2Zip = txtMinistry2Zip.Text;
        uObj.Ministry2Phone = txtMinistry2Phone.Text;
        uObj.Ministry2Fax = txtMinistry2Fax.Text;
        uObj.Ministry2Email = txtMinistry2Email.Text;

        uObj.JobID = Convert.ToInt32(ddlPosition.SelectedValue.ToString());
        uObj.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());
        uObj.LocationID  = Convert.ToInt32(ddlLocation.SelectedValue.ToString());

        if (!string.IsNullOrEmpty(this.uplProduct.FileName))
        {
            //read the file in
            string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = "Users_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName;
            string nFile = Path.Combine(filePath, fileName);

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
            uObj.Picture = fileName;
        }
    }
    
    private void FillControls(DataTable dt)
    {
        if (dt.Rows[0]["FirstName"] != null)
            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();

        if (dt.Rows[0]["LastName"] != null)
            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
        //txtD.Text = dt.Rows[0]["DEPT***"].ToString();
        //txtHomePhone.Text = dt.Rows[0]["POS****"].ToString();
        if (dt.Rows[0]["Chapter"] != null)
            txtChapter.Text = dt.Rows[0]["Chapter"].ToString();

        if (dt.Rows[0]["CompanionType"] != null)
            txtCompanionType.Text = dt.Rows[0]["CompanionType"].ToString();

        if (dt.Rows[0]["ProfessionalYear"] != null)
            txtProfessionalYear.Text = dt.Rows[0]["ProfessionalYear"].ToString();

        if (dt.Rows[0]["MI"] != null)
            txtMI.Text = dt.Rows[0]["MI"].ToString();

        if (dt.Rows[0]["HomeStreet1"] != null)
            txtHomeStreet1.Text = dt.Rows[0]["HomeStreet1"].ToString();
        if (dt.Rows[0]["HomeStreet2"] != null)
            txtHomeStreet1.Text = dt.Rows[0]["HomeStreet2"].ToString();
        if (dt.Rows[0]["HomeCity"] != null)
            txtHomeCity.Text = dt.Rows[0]["HomeCity"].ToString();
        if (dt.Rows[0]["HomeState"] != null)
            txtHomeState.Text = dt.Rows[0]["HomeState"].ToString();

        if (dt.Rows[0]["HomeZip"] != null)
            txtHomeZip.Text = dt.Rows[0]["HomeZip"].ToString();

        if (dt.Rows[0]["HomeEmail"] != null)
            txtHomeEmail.Text = dt.Rows[0]["HomeEmail"].ToString();

        if (dt.Rows[0]["HomePhone"] != null)
            txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

        if (dt.Rows[0]["MinistryClassification"] != null)
            txtMinistryClassification.Text = dt.Rows[0]["MinistryClassification"].ToString();

        if (dt.Rows[0]["MinistryLocation"] != null)
            txtMinistryLocation.Text = dt.Rows[0]["MinistryLocation"].ToString();

        if (dt.Rows[0]["MinistryStreet1"] != null)
            txtMinistryStreet1.Text = dt.Rows[0]["MinistryStreet1"].ToString();

        if (dt.Rows[0]["MinistryStreet2"] != null)
            txtMinistryStreet2.Text = dt.Rows[0]["MinistryStreet2"].ToString();

        if (dt.Rows[0]["MinistryCity"] != null)
            txtMinistryCity.Text = dt.Rows[0]["MinistryCity"].ToString();

        if (dt.Rows[0]["MinistryCountry"] != null)
            txtMinistryCountry.Text = dt.Rows[0]["MinistryCountry"].ToString();

        if (dt.Rows[0]["MinistryZip"] != null)
            txtMinistryZip.Text = dt.Rows[0]["MinistryZip"].ToString();

        if (dt.Rows[0]["MinistryPhone"] != null)
            txtMinistryPhone.Text = dt.Rows[0]["MinistryPhone"].ToString();

        if (dt.Rows[0]["MinistryFax"] != null)
            txtMinistryFax.Text = dt.Rows[0]["MinistryFax"].ToString();

        if (dt.Rows[0]["MinistryEmail"] != null)
            txtMinistryEmail.Text = dt.Rows[0]["MinistryEmail"].ToString();

        if (dt.Rows[0]["Ministry2Title"] != null)
            txtMinistry2Title.Text = dt.Rows[0]["Ministry2Title"].ToString();

        if (dt.Rows[0]["Ministry2Classification"] != null)
            txtMinistry2Classification.Text = dt.Rows[0]["Ministry2Classification"].ToString();

        if (dt.Rows[0]["Ministry2Location"] != null)
            txtMinistry2Location.Text = dt.Rows[0]["Ministry2Location"].ToString();

        if (dt.Rows[0]["Ministry2Street1"] != null)
            txtMinistry2Street1.Text = dt.Rows[0]["Ministry2Street1"].ToString();

        if (dt.Rows[0]["Ministry2Street2"] != null)
            txtMinistry2Street2.Text = dt.Rows[0]["Ministry2Street2"].ToString();

        if (dt.Rows[0]["Ministry2City"] != null)
            txtMinistry2City.Text = dt.Rows[0]["Ministry2City"].ToString();

        if (dt.Rows[0]["Ministry2Country"] != null)
            txtMinistry2Country.Text = dt.Rows[0]["Ministry2Country"].ToString();

        if (dt.Rows[0]["Ministry2Zip"] != null)
            txtMinistry2Zip.Text = dt.Rows[0]["Ministry2Zip"].ToString();

        if (dt.Rows[0]["Ministry2Phone"] != null)
            txtMinistry2Phone.Text = dt.Rows[0]["Ministry2Phone"].ToString();

        if (dt.Rows[0]["Ministry2Fax"] != null)
            txtMinistry2Fax.Text = dt.Rows[0]["Ministry2Fax"].ToString();
        if (dt.Rows[0]["Ministry2Email"] != null)
            txtMinistry2Email.Text = dt.Rows[0]["Ministry2Email"].ToString();

        if (dt.Rows[0]["DepartmentID"] != null)
            ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();

        if (dt.Rows[0]["JobID"] != null)
            ddlPosition.SelectedValue = dt.Rows[0]["JobID"].ToString();

        if (dt.Rows[0]["LocationID"] != null)
            ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();

        if (dt.Rows[0]["DepartmentID"] != null)
            ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();

        if (dt.Rows[0]["JobID"] != null)
            ddlPosition.SelectedValue = dt.Rows[0]["JobID"].ToString();

        if (dt.Rows[0]["LocationID"] != null)
            ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();


        if (dt.Rows[0]["Picture"] != null && dt.Rows[0]["Picture"].ToString() != string.Empty)
        {
            string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");
            imgUserView.Attributes["src"] = filePath + dt.Rows[0]["Picture"].ToString();
            Session["UserImage"] = dt.Rows[0]["Picture"].ToString();

        }

    }

    private void FillControlsView(DataTable dt)
    {
        if (dt.Rows[0]["FirstName"] != null)
            lblFirstName.Text = dt.Rows[0]["FirstName"].ToString();

        if (dt.Rows[0]["LastName"] != null)
            lblLastName.Text = dt.Rows[0]["LastName"].ToString();
        //lblD.Text = dt.Rows[0]["DEPT***"].ToString();
        //lblHomePhone.Text = dt.Rows[0]["POS****"].ToString();
        if (dt.Rows[0]["Chapter"] != null)
            lblAreaChapter.Text = dt.Rows[0]["Chapter"].ToString();

        if (dt.Rows[0]["CompanionType"] != null)
            lblCompanionType.Text = dt.Rows[0]["CompanionType"].ToString();

        if (dt.Rows[0]["ProfessionalYear"] != null)
            lblProfessionYear.Text = dt.Rows[0]["ProfessionalYear"].ToString();

        if (dt.Rows[0]["CompanionType"] != null)
            lblMI.Text = dt.Rows[0]["CompanionType"].ToString();

        if (dt.Rows[0]["HomeStreet1"] != null)
            lblHomeStreet1.Text = dt.Rows[0]["HomeStreet1"].ToString();
        if (dt.Rows[0]["HomeStreet2"] != null)
            lblHomeStreet1.Text = dt.Rows[0]["HomeStreet2"].ToString();
        if (dt.Rows[0]["HomeCity"] != null)
            lblHomeCity.Text = dt.Rows[0]["HomeCity"].ToString();
        if (dt.Rows[0]["HomeState"] != null)
            lblHomeState.Text = dt.Rows[0]["HomeState"].ToString();

        if (dt.Rows[0]["HomeZip"] != null)
            lblHomeZip.Text = dt.Rows[0]["HomeZip"].ToString();

        if (dt.Rows[0]["HomeEmail"] != null)
            lblHomeEmail.Text = dt.Rows[0]["HomeEmail"].ToString();

        if (dt.Rows[0]["HomePhone"] != null)
            lblHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

        if (dt.Rows[0]["MinistryClassification"] != null)
            lblMinistry1Classification.Text = dt.Rows[0]["MinistryClassification"].ToString();

        if (dt.Rows[0]["MinistryLocation"] != null)
            lblMinistry1PlaceofEmployment.Text = dt.Rows[0]["MinistryLocation"].ToString();

        if (dt.Rows[0]["MinistryStreet1"] != null)
            lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet1"].ToString();

        if (dt.Rows[0]["MinistryStreet2"] != null)
            lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet2"].ToString();

        if (dt.Rows[0]["MinistryCity"] != null)
            lblMinistry1City.Text = dt.Rows[0]["MinistryCity"].ToString();

        if (dt.Rows[0]["MinistryCountry"] != null)
            lblMinistry1Country.Text = dt.Rows[0]["MinistryCountry"].ToString();

        if (dt.Rows[0]["MinistryZip"] != null)
            lblMinistry1Zip.Text = dt.Rows[0]["MinistryZip"].ToString();

        if (dt.Rows[0]["MinistryPhone"] != null)
            lblMinistry1Phone.Text = dt.Rows[0]["MinistryPhone"].ToString();

        if (dt.Rows[0]["MinistryFax"] != null)
            lblMinistry1Fax.Text = dt.Rows[0]["MinistryFax"].ToString();

        if (dt.Rows[0]["MinistryEmail"] != null)
            lblMinistry1Email.Text = dt.Rows[0]["MinistryEmail"].ToString();

        if (dt.Rows[0]["Ministry2Title"] != null)
            lblMinistry2Title.Text = dt.Rows[0]["Ministry2Title"].ToString();

        if (dt.Rows[0]["Ministry2Classification"] != null)
            lblMinistry2Classification2.Text = dt.Rows[0]["Ministry2Classification"].ToString();

        if (dt.Rows[0]["Ministry2Location"] != null)
            lblMinistry2PlaceofEmp.Text = dt.Rows[0]["Ministry2Location"].ToString();

        if (dt.Rows[0]["Ministry2Street1"] != null)
            lblMinistry2Street1.Text = dt.Rows[0]["Ministry2Street1"].ToString();

        if (dt.Rows[0]["Ministry2Street2"] != null)
            lblMinistry2street2.Text = dt.Rows[0]["Ministry2Street2"].ToString();

        if (dt.Rows[0]["Ministry2City"] != null)
            lblMinistry2City.Text = dt.Rows[0]["Ministry2City"].ToString();

        if (dt.Rows[0]["Ministry2Country"] != null)
            lblMinistry2Country.Text = dt.Rows[0]["Ministry2Country"].ToString();

        if (dt.Rows[0]["Ministry2Zip"] != null)
            lblMinistry2Zip.Text = dt.Rows[0]["Ministry2Zip"].ToString();

        if (dt.Rows[0]["Ministry2Phone"] != null)
            lblMinistry2Phone.Text = dt.Rows[0]["Ministry2Phone"].ToString();

        if (dt.Rows[0]["CompanionType"] != null)
            lblMinistry2Fax.Text = dt.Rows[0]["Ministry2Fax"].ToString();

        if (dt.Rows[0]["Ministry2Email"] != null)
            lblMinistry2Email.Text = dt.Rows[0]["Ministry2Email"].ToString();

        if (dt.Rows[0]["Job"] != null)
            lblPosition.Text = dt.Rows[0]["Job"].ToString();

        if (dt.Rows[0]["Department"] != null)
            lblDepartment.Text = dt.Rows[0]["Department"].ToString();

        if (dt.Rows[0]["Location"] != null)
            lblHomeLocation.Text = dt.Rows[0]["Location"].ToString();

        //ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();

        if (dt.Rows[0]["Picture"] != null && dt.Rows[0]["Picture"].ToString() != string.Empty)
        {
            string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

            imgUserView.Attributes["src"] = "../Images/Users/" + dt.Rows[0]["Picture"].ToString();
        }

    }

    private void FillPermissionControls(DataTable dt)
    {
        if (dt.Rows[0]["CanLogIn"] != null)
            chkCanLogin.Checked = Convert.ToBoolean(dt.Rows[0]["CanLogIn"]);

        if (dt.Rows[0]["IsSister"] != null)
            chkIsSister.Checked = Convert.ToBoolean(dt.Rows[0]["IsSister"]);

        if (dt.Rows[0]["IsStaff"] != null)
            chkIsStuff.Checked = Convert.ToBoolean(dt.Rows[0]["IsStaff"]);

        if (dt.Rows[0]["IsCompanion"] != null)
            chkIsCompanion.Checked = Convert.ToBoolean(dt.Rows[0]["IsCompanion"]);

        if (dt.Rows[0]["IsHidden"] != null)
            chkIsHidden.Checked = Convert.ToBoolean(dt.Rows[0]["IsHidden"]);

        if (dt.Rows[0]["IsLayPerson"] != null)
            chkIsCommitteMember.Checked = Convert.ToBoolean(dt.Rows[0]["IsLayPerson"]);

        if (dt.Rows[0]["IsGlobalAdmin"] != null)
            chkIsGlobalAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsGlobalAdmin"]);

        if (dt.Rows[0]["IsGlobalContentAdmin"] != null)
            chkIsGlobalContentAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsGlobalContentAdmin"]);

        if (dt.Rows[0]["IsDirectoryAdmin"] != null)
            chkIsGlobalDirectoryAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsDirectoryAdmin"]);

        if (dt.Rows[0]["IsDelete"] != null)
            chkIsDelete.Checked = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);


    }

    private void FillSectionPermissionControls(DataTable dt)
    {
        //txtFirstName.Text = Convert.ToBoolean(dt.Rows[0]["FirstName"]);

        //chkCanLogin.Checked = Convert.ToBoolean(dt.Rows[0]["CanLogIn"]);
        //chkIsSister.Checked = Convert.ToBoolean(dt.Rows[0]["IsSister"]);
        //chkIsStuff.Checked = Convert.ToBoolean(dt.Rows[0]["IsStaff"]);
        //chkIsCompanion.Checked = Convert.ToBoolean(dt.Rows[0]["IsCompanion"]);
        //chkIsHidden.Checked = Convert.ToBoolean(dt.Rows[0]["IsHidden"]);
        //chkIsCommitteMember.Checked = Convert.ToBoolean(dt.Rows[0]["IsLayPerson"]);
        //chkIsGlobalAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsGlobalAdmin"]);

        //chkIsGlobalForumAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsGlobalForumAdmin"]);

        //chkIsGlobalContentAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsGlobalContentAdmin"]);

        //chkIsACContentAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsChapterContentAdmin"]);

        //chkISACForumAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsChapterForumAdmin"]);

        //chkIsACDirectoryAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsChapterDirectoryAdmin"]);

        //chkIsGlobalDirectoryAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsDirectoryAdmin"]);

        //isChapterDirectiveAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsChapterDirectivesAdmin"]);

        //chkIsCompanionAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsCompanionAdmin"]);

        //chkCanLogin.Checked = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);


    }

    private void LoadComboDepartment()
    {
        ddlDepartment.Items.Clear();
        ddlSDepartment.Items.Clear();

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = cmscon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(SectionTypeEnum.Department)));


            ddlDepartment.AppendDataBoundItems = true;
            ddlDepartment.Items.Add(new ListItem("--Select Dept--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlDepartment.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }



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
        ddlPosition.Items.Clear();
        ddlSPost.Items.Clear();

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = cmscon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(SectionTypeEnum.Job)));


            ddlPosition.AppendDataBoundItems = true;
            ddlPosition.Items.Add(new ListItem("--Select Position--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlPosition.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }
    


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
        ddlLocation.Items.Clear();
        ddlSLocation.Items.Clear();

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = cmscon.getRows(string.Format("SELECT * FROM BasicData WHERE ParentID= {0}", Convert.ToInt32(SectionTypeEnum.Location)));


            ddlLocation.AppendDataBoundItems = true;
            ddlLocation.Items.Add(new ListItem("--Select Location--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlLocation.Items.Add(new ListItem(dr["Name"].ToString(), dr["BasicDataID"].ToString()));
            }



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

    private string ValidateSearch()
    {
        string _search = "";

        if (txtSChapter.Text != "" && !Regex.IsMatch(txtSChapter.Text, @"^\d+$"))
        {
            _search += "Chapter will be only one letter" + Environment.NewLine;
        }



        if (txtsProfessionalYear.Text != "" && !Regex.IsMatch(txtsProfessionalYear.Text, @"^\d+$"))
        {
            _search += "ProfessionalYear will be only one letter" + Environment.NewLine;
        }

        return _search;
    }

    private void GenerateTreeView()
    {

        //chkHeaderList.Items.Add(new ListItem("Hello1", "1" ));
        //chkHeaderList.Items.Add(new ListItem("Hello2", "2"));
        //chkHeaderList.Items.Add(new ListItem("Hello3", "3"));
        //chkHeaderList.Items.Add(new ListItem("Hello4", "4"));
        //chkHeaderList.Items.Add(new ListItem("Hello5", "5"));

        //chkHeaderList.RepeatDirection = RepeatDirection.Horizontal;
        LinksTreeView.Nodes.Clear();
        try
        {
            string sql = "Select * FROM SectionType WHERE IsCategory=1";
            DataTable ResultSet = cmscon.getRows(sql);
            chkHeaderList.Items.Clear();
            chkHeaderList.RepeatDirection = RepeatDirection.Horizontal;
            // Create the second-level nodes
            if (ResultSet != null)
            {
                if (ResultSet.Rows.Count > 0)
                {
                    foreach (DataRow row in ResultSet.Rows)
                    {
                        TreeNode ParentNode = new TreeNode();
                        ParentNode.Text = row["Description"].ToString();
                        ParentNode.Value = row["SectionTypeID"].ToString();
                        //ParentNode.SelectAction = TreeNodeSelectAction.Select;
                        ParentNode.ShowCheckBox = false;
                        LinksTreeView.Nodes.Add(ParentNode);
                        chkHeaderList.Items.Add(new ListItem(ParentNode.Text, ParentNode.Value));
                        // Add Child Node 
                        if (Convert.ToBoolean(row["IsCategory"]))
                        {
                            string newsql = string.Format("Select * From Categories Where CategoryTypeID={0}", Convert.ToInt32(row["SectionTypeID"]));// + row["category_id"].ToString();
                            DataTable newResultSet = cmscon.getRows(newsql);
                            if (newResultSet != null)
                            {
                                // Create the third-level nodes.
                                if (newResultSet.Rows.Count > 0)
                                {
                                    foreach (DataRow newrow in newResultSet.Rows)
                                    {

                                        // Create the new node.
                                        TreeNode childNode = new TreeNode();
                                        //childNode.Text = "<a style='text-decoration:none; color:#000;' href='PageContent.aspx?page_id=" + newrow["CategoryID"].ToString() + "'>" + newrow["Description"].ToString() + "</a>";
                                        childNode.Text = newrow["Description"].ToString();
                                        childNode.Value = newrow["CategoryID"].ToString();
                                        //childNode.SelectAction = TreeNodeSelectAction.Select;
                                        childNode.ShowCheckBox = true;

                                        ParentNode.ChildNodes.Add(childNode);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }



    }

    private void GenerateTreeViewEdit()
    {
        LinksTreeView.Nodes.Clear();
        try
        {
            int userid = Convert.ToInt32(Session["UserIDEdit"]);
           //old******** string sql = string.Format("SELECt SectionType.*,UserSectionPermission.* from SectionType LEft join  UserSectionPermission ON SectionType.SectionTypeID = UserSectionPermission.sectionid AND UserSectionPermission.issection = 1 AND UserSectionPermission.UserID  ={0} AND IsCategory=1", userid); 

            string sql = string.Format(@"                SELECt *  FROM
(select * from SectionType where IsCategory = 1) A LEFT JOIN  UserSectionPermission ON A.SectionTypeID = 
UserSectionPermission.sectionid AND UserSectionPermission.issection = 1 AND UserSectionPermission.UserID  ={0}", userid); 


            DataTable ResultSet = cmscon.getRows(sql);

            // Create the second-level nodes
            if (ResultSet != null)
            {
                if (ResultSet.Rows.Count > 0)
                {
                    chkHeaderList.Items.Clear();
                    chkHeaderList.RepeatDirection = RepeatDirection.Horizontal;

                    foreach (DataRow row in ResultSet.Rows)
                    {
                     
                            TreeNode ParentNode = new TreeNode();
                            ParentNode.Text = row["Description"].ToString();
                            ParentNode.Value = row["SectionTypeID"].ToString();
                           // ParentNode.Checked = Convert.ToBoolean(row["HasPermission"]);
                            ParentNode.ShowCheckBox = false;
                            LinksTreeView.Nodes.Add(ParentNode);

                            chkHeaderList.Items.Add(new ListItem(ParentNode.Text, ParentNode.Value));

                            foreach (ListItem li in chkHeaderList.Items)
                            {
                                if (li.Value == ParentNode.Value)
                                {
                                    li.Selected = Convert.ToBoolean(row["HasPermission"]);
                                }

                            }
                            // Add Child Node 
                            if (Convert.ToBoolean(row["IsCategory"]))
                            {

                            string newsql = string.Format("SELECT T.CategoryID,T.Description, U.* FROM (Select * From Categories Where CategoryTypeID={0} ) T LEFT JOIN UserSectionPermission U on T.CategoryID = U.CategoryID AND U.UserID = {1}", Convert.ToInt32(row["SectionTypeID"]),userid);// + row["category_id"].ToString();
                            DataTable newResultSet = cmscon.getRows(newsql);
                            if (newResultSet != null)
                            {
                                // Create the third-level nodes.
                                if (newResultSet.Rows.Count > 0)
                                {
                                    foreach (DataRow newrow in newResultSet.Rows)
                                    {

                                        // Create the new node.
                                        TreeNode childNode = new TreeNode();
                                        //childNode.Text = "<a style='text-decoration:none; color:#000;' href='PageContent.aspx?page_id=" + newrow["CategoryID"].ToString() + "'>" + newrow["Description"].ToString() + "</a>";
                                        childNode.Text = newrow["Description"].ToString();
                                        childNode.Value = newrow["CategoryID"].ToString();
                                        childNode.Checked = Convert.ToBoolean(newrow["HasPermission"]);
                                        childNode.ShowCheckBox = true;

                                        ParentNode.ChildNodes.Add(childNode);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }



    }

    public void LinksTreeView_TreeNodeExpanded(Object sender, TreeNodeEventArgs e)
  {
        TreeNodeCollection ts = null;
        if (e.Node.Parent == null)
        {
            ts = ((TreeView)sender).Nodes;
        }
        else
        {
            ts = e.Node.Parent.ChildNodes;
        }
        foreach (TreeNode node in ts)
        {
            if (node.Value != e.Node.Value)
            {
                node.Collapse();
            }
        }

    }

    #endregion




}