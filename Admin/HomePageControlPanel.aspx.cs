using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_HomePageControlPanel : System.Web.UI.Page
{
    #region Global Variable & PageLoad

    int _categoryID = 0; string _message = "";
    //EditContent
    protected void Page_Load(object sender, EventArgs e)
    {

   
            CheckAdminPermission();

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
                DataTable dt = cmscon.getRows("SELECT Count(*) From HomePageSettings");

                if (dt == null || dt.Rows.Count <= 0 || Convert.ToInt32(dt.Rows[0][0]) <= 0)
                    FirstTimeFill();
                else
                {
                    FillforEdit();
                }
            }
      
    }
    #endregion
    
    #region Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        List<HomePageSettings> leftCol = new List<HomePageSettings>();
        foreach (TreeNode node in treeViewLeft.Nodes)
        {
            HomePageSettings up = new HomePageSettings();

            if (node.Parent == null) //its parent Node
            {
                up = new HomePageSettings();
                up.SectionID = Convert.ToInt32(node.Value);
                up.IsSection = true;
                up.CategoryID = 0;
                up.WillShow = false;
                up.HomePageColumnType = (int)HomePageColumnType.LeftColumn;
                leftCol.Add(up);

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode cnode in node.ChildNodes)
                    {
                        up = new HomePageSettings();
                        up.SectionID = Convert.ToInt32(cnode.Value);
                        up.IsSection = false;
                        up.CategoryID = Convert.ToInt32(cnode.Value);
                        up.WillShow = cnode.Checked;
                        up.HomePageColumnType = (int)HomePageColumnType.LeftColumn; 
                        leftCol.Add(up);
                    }
                }
            }
        }

        List<HomePageSettings> RightCol = new List<HomePageSettings>();
        foreach (TreeNode node in treeViewRight.Nodes)
        {
            HomePageSettings up = new HomePageSettings();
            if (node.Parent == null) //its parent Node
            {
                up = new HomePageSettings();
                up.SectionID = Convert.ToInt32(node.Value);
                up.IsSection = true;
                up.CategoryID = 0;
                up.WillShow = false;
                up.HomePageColumnType = (int)HomePageColumnType.RightColumn;
                RightCol.Add(up);

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode cnode in node.ChildNodes)
                    {
                        up = new HomePageSettings();
                        up.SectionID = Convert.ToInt32(cnode.Value);
                        up.IsSection = false;
                        up.CategoryID = Convert.ToInt32(cnode.Value);
                        up.WillShow = cnode.Checked;
                        up.HomePageColumnType = (int)HomePageColumnType.RightColumn;
                        RightCol.Add(up);
                    }
                }
            }
        }

        List<HomePageSettings> MiddleCol = new List<HomePageSettings>();
        foreach (TreeNode node in treeViewMidde.Nodes)
        {
            HomePageSettings up = new HomePageSettings();

            if (node.Parent == null) //its parent Node
            {
                up = new HomePageSettings();
                up.SectionID = Convert.ToInt32(node.Value);
                up.IsSection = true;
                up.CategoryID = 0;
                up.WillShow = false;
                up.HomePageColumnType = (int)HomePageColumnType.MiddleColumn;
                MiddleCol.Add(up);

                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode cnode in node.ChildNodes)
                    {
                        up = new HomePageSettings();
                        up.SectionID = Convert.ToInt32(cnode.Value);
                        up.IsSection = false;
                        up.CategoryID = Convert.ToInt32(cnode.Value);
                        up.WillShow = cnode.Checked;
                        up.HomePageColumnType = (int)HomePageColumnType.MiddleColumn;
                        MiddleCol.Add(up);
                    }
                }
            }
        }
        Utility.QueryExecute("DELETE FROM HomePageSettings");
        foreach (HomePageSettings hps in leftCol)
        {
            HomePageSettings h = new HomePageSettings(cmscon.CONNECTIONSTRING);
            h.CategoryID = hps.CategoryID;
            h.SectionID = hps.SectionID;
            h.IsSection = hps.IsSection;
            h.WillShow = hps.WillShow;
            h.HomePageColumnType = hps.HomePageColumnType;
            if (!h.IsSection)
                h.SectionID = 0;
            h.insert();
        }

        foreach (HomePageSettings hps in RightCol)
        {
            HomePageSettings h = new HomePageSettings(cmscon.CONNECTIONSTRING);
            h.CategoryID = hps.CategoryID;
             h.SectionID = hps.SectionID;
            h.IsSection = hps.IsSection;
            if (!h.IsSection)
                h.SectionID = 0;
            h.WillShow = hps.WillShow;
            h.HomePageColumnType = hps.HomePageColumnType;

            h.insert();
        }

        foreach (HomePageSettings hps in MiddleCol)
        {
            HomePageSettings h = new HomePageSettings(cmscon.CONNECTIONSTRING);
            h.CategoryID = hps.CategoryID;
            h.SectionID = hps.SectionID;
            h.IsSection = hps.IsSection;
            h.WillShow = hps.WillShow;
            h.HomePageColumnType = hps.HomePageColumnType;
            if (!h.IsSection)
                h.SectionID = 0;
            h.insert();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminIndex.aspx");
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

    private void GenerateLeftTreeView(DataTable ResultSet)
    {

        treeViewLeft.Nodes.Clear();
        //treeViewMidde.Nodes.Clear();
        //treeViewRight.Nodes.Clear();
        try
        {
       

            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {
               
                    foreach (DataRow row in ResultSet.Rows)
                    {
                        TreeNode ParentNode = new TreeNode();
                        ParentNode.Text = row["Description"].ToString();
                        ParentNode.Value = row["SectionTypeID"].ToString();
                        ParentNode.ShowCheckBox = false;
                        ParentNode.Collapse();
                        treeViewLeft.Nodes.Add(ParentNode);



                        // Add Child Node 
                        if (Convert.ToBoolean(row["IsCategory"]))
                        {
                            string newsql = string.Format("Select * From Categories Where CategoryTypeID={0}", Convert.ToInt32(row["SectionTypeID"]));// + row["category_id"].ToString();
                            DataTable newResultSet = cmscon.getRows(newsql);
                            if (newResultSet != null && newResultSet.Rows.Count > 0)
                            {
                                // Create the third-level nodes.
                          
                                    foreach (DataRow newrow in newResultSet.Rows)
                                    {

                                        // Create the new node.
                                        TreeNode childNode = new TreeNode();                                        
                                        childNode.Text = newrow["Description"].ToString();
                                        childNode.Value = newrow["CategoryID"].ToString();                                   
                                        childNode.ShowCheckBox = true;

                                        ParentNode.ChildNodes.Add(childNode);

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

    private void GenerateMiddleTreeView(DataTable ResultSet)
    {


        treeViewMidde.Nodes.Clear();
        //treeViewRight.Nodes.Clear();
        try
        {


            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {

                foreach (DataRow row in ResultSet.Rows)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = row["Description"].ToString();
                    ParentNode.Value = row["SectionTypeID"].ToString();
                    ParentNode.ShowCheckBox = false;

                    ParentNode.Collapse();
                    treeViewMidde.Nodes.Add(ParentNode);
                    //treeViewRight.Nodes.Add(ParentNode);



                    // Add Child Node 
                    if (Convert.ToBoolean(row["IsCategory"]))
                    {
                        string newsql = string.Format("Select * From Categories Where CategoryTypeID={0}", Convert.ToInt32(row["SectionTypeID"]));// + row["category_id"].ToString();
                        DataTable newResultSet = cmscon.getRows(newsql);
                        if (newResultSet != null && newResultSet.Rows.Count > 0)
                        {
                            // Create the third-level nodes.

                            foreach (DataRow newrow in newResultSet.Rows)
                            {

                                // Create the new node.
                                TreeNode childNode = new TreeNode();
                                childNode.Text = newrow["Description"].ToString();
                                childNode.Value = newrow["CategoryID"].ToString();
                                childNode.ShowCheckBox = true;

                                ParentNode.ChildNodes.Add(childNode);

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

    private void GenerateRightTreeView(DataTable ResultSet)
    {



        treeViewRight.Nodes.Clear();
        try
        {

            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {

                foreach (DataRow row in ResultSet.Rows)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = row["Description"].ToString();
                    ParentNode.Value = row["SectionTypeID"].ToString();
                    ParentNode.ShowCheckBox = false;
                    ParentNode.Collapse();


                    treeViewRight.Nodes.Add(ParentNode);



                    // Add Child Node 
                    if (Convert.ToBoolean(row["IsCategory"]))
                    {
                        string newsql = string.Format("Select * From Categories Where CategoryTypeID={0}", Convert.ToInt32(row["SectionTypeID"]));// + row["category_id"].ToString();
                        DataTable newResultSet = cmscon.getRows(newsql);
                        if (newResultSet != null && newResultSet.Rows.Count > 0)
                        {
                            // Create the third-level nodes.

                            foreach (DataRow newrow in newResultSet.Rows)
                            {

                                // Create the new node.
                                TreeNode childNode = new TreeNode();
                                childNode.Text = newrow["Description"].ToString();
                                childNode.Value = newrow["CategoryID"].ToString();
                                childNode.ShowCheckBox = true;

                                ParentNode.ChildNodes.Add(childNode);

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

    private void FirstTimeFill()
    {
        string sql = string.Format("Select * FROM SectionType WHERE IsCategory=1 AND SectionTypeID <>{0}", (int)SectionTypeEnum.Directory);
        DataTable ResultSet = cmscon.getRows(sql);

        GenerateLeftTreeView(ResultSet);
        GenerateRightTreeView(ResultSet);
        GenerateMiddleTreeView(ResultSet);
    
    }


    private void FillforEdit()
    {


        GenerateLeftTreeViewEdit();
        GenerateRightTreeViewEdit();
        GenerateMiddleTreeViewEdit();

    }

    private void GenerateLeftTreeViewEdit()
    {
        string sql = string.Format(@"SELECT * FROM (Select * FROM SectionType WHERE IsCategory=1 AND SectionTypeID <> {0} ) A
                                    LEFT JOIN
                                  ( SELECT * FROM HomePageSettings where IsSection =1 AND HomePageColumnType = {1} ) B on A.SectionTypeID = B.SectionID", (int)SectionTypeEnum.Directory, (int)HomePageColumnType.LeftColumn);
        DataTable ResultSet = cmscon.getRows(sql);

        treeViewLeft.Nodes.Clear();
  
        try
        {


            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {

                foreach (DataRow row in ResultSet.Rows)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = row["Description"].ToString();
                    ParentNode.Value = row["SectionTypeID"].ToString();
                    ParentNode.ShowCheckBox = false;
                    ParentNode.Collapse();
                    treeViewLeft.Nodes.Add(ParentNode);

                    string newsql = string.Format(@"SELECT * FROM (Select CategoryID, Description From Categories Where CategoryTypeID={0}) A LEFT JOIN 
                                                            (
                                                            SELECT * FROM HomePageSettings where IsSection =0 AND HomePageColumnType = {1} ) B 

                                                            On A.CategoryID = B.CategoryID", Convert.ToInt32(row["SectionTypeID"]), (int)HomePageColumnType.LeftColumn);// + row["category_id"].ToString();
                    DataTable newResultSet = cmscon.getRows(newsql);
                    if (newResultSet != null && newResultSet.Rows.Count > 0)
                    {

                        foreach (DataRow newrow in newResultSet.Rows)
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Text = newrow["Description"].ToString();
                            childNode.Value = newrow["CategoryID"].ToString();
                            childNode.ShowCheckBox = true;
                            childNode.Checked = Convert.ToBoolean(newrow["WillShow"].ToString());
                            ParentNode.ChildNodes.Add(childNode);

                        }

                    }

                }

            }
        }
        catch (Exception ex)
        {

        }



    }


    private void GenerateRightTreeViewEdit()
    {
        string sql = string.Format(@"SELECT * FROM (Select * FROM SectionType WHERE IsCategory=1 AND SectionTypeID <> {0} ) A
                                    LEFT JOIN
                                  ( SELECT * FROM HomePageSettings where IsSection =1 AND HomePageColumnType = {1} ) B on A.SectionTypeID = B.SectionID", (int)SectionTypeEnum.Directory, (int)HomePageColumnType.RightColumn);
        DataTable ResultSet = cmscon.getRows(sql);

        treeViewRight.Nodes.Clear();

        try
        {


            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {

                foreach (DataRow row in ResultSet.Rows)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = row["Description"].ToString();
                    ParentNode.Value = row["SectionTypeID"].ToString();
                    ParentNode.ShowCheckBox = false;
                    ParentNode.Collapse();
                    treeViewRight.Nodes.Add(ParentNode);

                    string newsql = string.Format(@"SELECT * FROM (Select CategoryID, Description From Categories Where CategoryTypeID={0}) A LEFT JOIN 
                                                            (
                                                            SELECT * FROM HomePageSettings where IsSection =0 AND HomePageColumnType = {1} ) B 

                                                            On A.CategoryID = B.CategoryID", Convert.ToInt32(row["SectionTypeID"]), (int)HomePageColumnType.RightColumn);// + row["category_id"].ToString();
                    DataTable newResultSet = cmscon.getRows(newsql);
                    if (newResultSet != null && newResultSet.Rows.Count > 0)
                    {

                        foreach (DataRow newrow in newResultSet.Rows)
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Text = newrow["Description"].ToString();
                            childNode.Value = newrow["CategoryID"].ToString();
                            childNode.ShowCheckBox = true;
                            childNode.Checked = Convert.ToBoolean(newrow["WillShow"].ToString());
                            ParentNode.ChildNodes.Add(childNode);

                        }

                    }

                }

            }
        }
        catch (Exception ex)
        {

        }



    }


    private void GenerateMiddleTreeViewEdit()
    {
        string sql = string.Format(@"SELECT * FROM (Select * FROM SectionType WHERE IsCategory=1 AND SectionTypeID <> {0} ) A
                                    LEFT JOIN
                                  ( SELECT * FROM HomePageSettings where IsSection =1 AND HomePageColumnType = {1} ) B on A.SectionTypeID = B.SectionID", (int)SectionTypeEnum.Directory, (int)HomePageColumnType.MiddleColumn);
        DataTable ResultSet = cmscon.getRows(sql);

        treeViewMidde.Nodes.Clear();

        try
        {


            if (ResultSet != null && ResultSet.Rows.Count > 0)
            {

                foreach (DataRow row in ResultSet.Rows)
                {
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Text = row["Description"].ToString();
                    ParentNode.Value = row["SectionTypeID"].ToString();
                    ParentNode.ShowCheckBox = false;
                    ParentNode.Collapse();
                    treeViewMidde.Nodes.Add(ParentNode);

                    string newsql = string.Format(@"SELECT * FROM (Select CategoryID, Description From Categories Where CategoryTypeID={0}) A LEFT JOIN 
                                                            (
                                                            SELECT * FROM HomePageSettings where IsSection =0 AND HomePageColumnType = {1} ) B 

                                                            On A.CategoryID = B.CategoryID", Convert.ToInt32(row["SectionTypeID"]), (int)HomePageColumnType.MiddleColumn);// + row["category_id"].ToString();
                    DataTable newResultSet = cmscon.getRows(newsql);
                    if (newResultSet != null && newResultSet.Rows.Count > 0)
                    {

                        foreach (DataRow newrow in newResultSet.Rows)
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Text = newrow["Description"].ToString();
                            childNode.Value = newrow["CategoryID"].ToString();
                            childNode.ShowCheckBox = true;
                            childNode.Checked = Convert.ToBoolean(newrow["WillShow"].ToString());
                            ParentNode.ChildNodes.Add(childNode);

                        }

                    }

                }

            }
        }
        catch (Exception ex)
        {

        }



    }
    #endregion


}