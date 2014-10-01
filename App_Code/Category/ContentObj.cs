using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;

public class ContentObj
    {
    // If 'connectionString' is set by the constructor or externally, then the connection will be
    // opened and closed with each operation. If 'conn' is set, then the connection stays open.
    public SqlConnection conn = null;
    public string connectionString = "";
    public string booleanFalse = "N";
    public string booleanTrue = "Y";
    public string lastError = "";
    public bool valid = false;
    public ArrayList colInfo;

    // A variable for each column in the table.
    public int ContentID;
    public string Author;
    public DateTime Date;
    public string Title;
    public string Content;
    public Object BlobContent;
    public string Extension;
    public string URL;
    public int CategoryID;
    public short ContentTypeID;
    public int Chapter;
    public int CommitteeID;
    public int UpdatedByID;
    public DateTime LastUpdated;
    public int HotpOrder;
    public int PrivateCategoryID;
    public int Chapter2008CategoryID;
    public int ACCommonCategoryID;
    public int Election2008CategoryID;
    public int RootThreadID;

    public int CreatedBy;
    public DateTime CreatedOn;
    public int ModifiedBy;
    public DateTime ModifiedOn;

#region User Variables
    // Add your variables to this object here. Adding them here ensures
    // that they will be retained when the object is re-generated.
#endregion

#region Column Info
    // This class maintains information about each column in the table.
    public class ColumnInfo : IComparable
        {
        public string columnName;
        public string dataType;
        public int length;
        public bool allowInsert;
        public bool allowUpdate;

        public ColumnInfo(string _ColumnName, string _DataType, int _Length, bool _AllowInsert, bool _AllowUpdate)
            {
            columnName = _ColumnName;
            dataType = _DataType;
            length = _Length;
            allowInsert = _AllowInsert;
            allowUpdate = _AllowUpdate;
            }

        public int CompareTo(Object obj)
            {
            if(obj.GetType() == this.GetType())
                {
                ColumnInfo ci = (ColumnInfo)obj;
                return this.columnName.CompareTo(ci.columnName);
                }
            string s = (string)obj;
            return this.columnName.CompareTo(s);
            }
        }
#endregion

#region Constructors
    /// <summary></summary>
    public ContentObj()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("ContentID", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("Author", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Date", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("Title", "string", 300, true, true));
        colInfo.Add(new ColumnInfo("Content", "string", 4000, true, true));
        colInfo.Add(new ColumnInfo("BlobContent", "Object", -1, true, true));
        colInfo.Add(new ColumnInfo("Extension", "string", 5, true, true));
        colInfo.Add(new ColumnInfo("URL", "string", 255, true, true));
        colInfo.Add(new ColumnInfo("CategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ContentTypeID", "short", 6, true, true));
        colInfo.Add(new ColumnInfo("Chapter", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CommitteeID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("UpdatedByID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("LastUpdated", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("HotpOrder", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("PrivateCategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("Chapter2008CategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ACCommonCategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("Election2008CategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("RootThreadID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CreatedBy", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CreatedOn", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("ModifiedBy", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ModifiedOn", "DateTime", 19, true, true));

        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public ContentObj(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public ContentObj(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    public ContentObj(SqlConnection _conn, int _ContentID) : this()
        {
        conn = _conn;
        getRecord(_ContentID);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    public ContentObj(string _connectionString, int _ContentID) : this()
        {
        connectionString = _connectionString;
        getRecord(_ContentID);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a Content table record</summary>
    public void blank()
        {
        ContentID = 0;
        Author = "";
        Date = new DateTime(0);
        Title = "";
        Content = "";
        BlobContent = null;
        Extension = "";
        URL = "";
        CategoryID = 0;
        ContentTypeID = 0;
        Chapter = 0;
        CommitteeID = 0;
        UpdatedByID = 0;
        LastUpdated = new DateTime(0);
        HotpOrder = 0;
        PrivateCategoryID = 0;
        Chapter2008CategoryID = 0;
        ACCommonCategoryID = 0;
        Election2008CategoryID = 0;
        CreatedBy = 0;
        CreatedOn = new DateTime(0);
        ModifiedBy = 0;
        ModifiedOn = new DateTime(0);
        RootThreadID = 0;


        valid = false;

        }

    /// <summary>Get a DataRow from the Content table using the primary key</summary>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>A DataRow from the Content table</returns>
    public DataRow getRow(int _ContentID)
        {
        return getRow("*", _ContentID);
        }

    /// <summary>Get a DataRow from the Content table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the Content table separated by commas</param>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>A DataRow from the Content table</returns>
    public DataRow getRow(string columnList, int _ContentID)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Content\" WHERE ContentID = @ContentID", conn);
            cmd.Parameters.AddWithValue("@ContentID", _ContentID);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            valid = true;
            if(dt.Rows.Count > 0) return dt.Rows[0];
            lastError = "No record found";
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            }
        valid = false;
        return null;
        }

    /// <summary>Get a DataRow from the Content table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the Content</param>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>A DataRow from the Content table</returns>
    public DataRow getRow(string[] columnList, int _ContentID)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _ContentID);
        }

    /// <summary>Get a DataRow from the Content table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Content</param>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>A DataRow from the Content table</returns>
    public DataRow getRow(ArrayList columnList, int _ContentID)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _ContentID);
        }

    /// <summary>Get a record from the Content table and populate the local variables</summary>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _ContentID)
        {
        DataRow dr = getRow(_ContentID);

        valid = false;
        if(dr != null)
            {
            ContentID = (dr["ContentID"] == DBNull.Value ? (int)0 : (int)dr["ContentID"]);
            Author = (dr["Author"] == DBNull.Value ? (string)"" : (string)dr["Author"]);
            Date = (dr["Date"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["Date"]);
            Title = (dr["Title"] == DBNull.Value ? (string)"" : (string)dr["Title"]);
            Content = (dr["Content"] == DBNull.Value ? (string)"" : (string)dr["Content"]);
            BlobContent = (dr["BlobContent"] == DBNull.Value ? (Object)null : (Object)dr["BlobContent"]);
            Extension = (dr["Extension"] == DBNull.Value ? (string)"" : (string)dr["Extension"]);
            URL = (dr["URL"] == DBNull.Value ? (string)"" : (string)dr["URL"]);
            CategoryID = (dr["CategoryID"] == DBNull.Value ? (int)0 : (int)dr["CategoryID"]);
            ContentTypeID = (dr["ContentTypeID"] == DBNull.Value ? (short)0 : (short)dr["ContentTypeID"]);
            Chapter = (dr["Chapter"] == DBNull.Value ? (int)0 : (int)dr["Chapter"]);
            CommitteeID = (dr["CommitteeID"] == DBNull.Value ? (int)0 : (int)dr["CommitteeID"]);
            UpdatedByID = (dr["UpdatedByID"] == DBNull.Value ? (int)0 : (int)dr["UpdatedByID"]);
            LastUpdated = (dr["LastUpdated"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["LastUpdated"]);
            HotpOrder = (dr["HotpOrder"] == DBNull.Value ? (int)0 : (int)dr["HotpOrder"]);
            PrivateCategoryID = (dr["PrivateCategoryID"] == DBNull.Value ? (int)0 : (int)dr["PrivateCategoryID"]);
            Chapter2008CategoryID = (dr["Chapter2008CategoryID"] == DBNull.Value ? (int)0 : (int)dr["Chapter2008CategoryID"]);
            ACCommonCategoryID = (dr["ACCommonCategoryID"] == DBNull.Value ? (int)0 : (int)dr["ACCommonCategoryID"]);
            Election2008CategoryID = (dr["Election2008CategoryID"] == DBNull.Value ? (int)0 : (int)dr["Election2008CategoryID"]);
            RootThreadID = (dr["RootThreadID"] == DBNull.Value ? (int)0 : (int)dr["RootThreadID"]);

            CreatedBy = (dr["CreatedBy"] == DBNull.Value ? (int)0 : (int)dr["CreatedBy"]);
            CreatedOn = (dr["CreatedOn"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["CreatedOn"]);
            ModifiedBy = (dr["ModifiedBy"] == DBNull.Value ? (int)0 : (int)dr["ModifiedBy"]);
            ModifiedOn = (dr["ModifiedOn"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["ModifiedOn"]);


            valid = true;
            }
        return valid;
        }

    public ContentObj MakeRowToObject(DataRow dr)
    {

        ContentObj cObj = new ContentObj();
        valid = false;
        if (dr != null)
        {
            cObj.ContentID = (dr["ContentID"] == DBNull.Value ? (int)0 : (int)dr["ContentID"]);
            cObj.Author = (dr["Author"] == DBNull.Value ? (string)"" : (string)dr["Author"]);
            cObj.Date = (dr["Date"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["Date"]);
            cObj.Title = (dr["Title"] == DBNull.Value ? (string)"" : (string)dr["Title"]);
            cObj.Content = (dr["Content"] == DBNull.Value ? (string)"" : (string)dr["Content"]);
            cObj.BlobContent = (dr["BlobContent"] == DBNull.Value ? (Object)null : (Object)dr["BlobContent"]);
            cObj.Extension = (dr["Extension"] == DBNull.Value ? (string)"" : (string)dr["Extension"]);
            cObj.URL = (dr["URL"] == DBNull.Value ? (string)"" : (string)dr["URL"]);
            cObj.CategoryID = (dr["CategoryID"] == DBNull.Value ? (int)0 : (int)dr["CategoryID"]);
            cObj.ContentTypeID = (dr["ContentTypeID"] == DBNull.Value ? (short)0 : (short)dr["ContentTypeID"]);
            cObj.Chapter = (dr["Chapter"] == DBNull.Value ? (int)0 : (int)dr["Chapter"]);
            cObj.CommitteeID = (dr["CommitteeID"] == DBNull.Value ? (int)0 : (int)dr["CommitteeID"]);
            cObj.UpdatedByID = (dr["UpdatedByID"] == DBNull.Value ? (int)0 : (int)dr["UpdatedByID"]);
            cObj.LastUpdated = (dr["LastUpdated"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["LastUpdated"]);
            cObj.HotpOrder = (dr["HotpOrder"] == DBNull.Value ? (int)0 : (int)dr["HotpOrder"]);
            cObj.PrivateCategoryID = (dr["PrivateCategoryID"] == DBNull.Value ? (int)0 : (int)dr["PrivateCategoryID"]);
            cObj.Chapter2008CategoryID = (dr["Chapter2008CategoryID"] == DBNull.Value ? (int)0 : (int)dr["Chapter2008CategoryID"]);
            cObj.ACCommonCategoryID = (dr["ACCommonCategoryID"] == DBNull.Value ? (int)0 : (int)dr["ACCommonCategoryID"]);
            cObj.Election2008CategoryID = (dr["Election2008CategoryID"] == DBNull.Value ? (int)0 : (int)dr["Election2008CategoryID"]);
            cObj.RootThreadID = (dr["RootThreadID"] == DBNull.Value ? (int)0 : (int)dr["RootThreadID"]);

            cObj.CreatedBy = (dr["CreatedBy"] == DBNull.Value ? (int)0 : (int)dr["CreatedBy"]);
            cObj.CreatedOn = (dr["CreatedOn"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["CreatedOn"]);
            cObj.ModifiedBy = (dr["ModifiedBy"] == DBNull.Value ? (int)0 : (int)dr["ModifiedBy"]);
            cObj.ModifiedOn = (dr["ModifiedOn"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["ModifiedOn"]);


        }
        return cObj;
    }
    public List<ContentObj> getRecords(int categoryTypeID)
    {
        List<ContentObj> contentsList = new List<ContentObj>();
        DataTable dt = osfcon.getRows(string.Format("SELECT top 1000 * FROM Categories cc JOIN Content c On cc.CategoryID = c.CategoryID AND cc.CategoryTypeID= {0} AND c.IsActive=1 Order by c.HotpOrder desc", categoryTypeID));
        if(dt != null)
        foreach(DataRow dr in dt.Rows)
        {
          contentsList.Add( this.MakeRowToObject(dr));
        }
        return contentsList;
    }

    public List<ContentObj> getRecordsForNewsType(int categoryTypeID,  DateTime fromdate, DateTime todate)
    {
        if (fromdate == null || todate == null || fromdate == DateTime.MinValue || todate == DateTime.MinValue)
        {
            return this.getRecords(categoryTypeID);
        }
        else
        {
            List<ContentObj> contentsList = new List<ContentObj>();
            DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Categories cc JOIN Content c On cc.CategoryID = c.CategoryID AND cc.CategoryTypeID= {0} AND c.Date between '{1}' AND '{2}' AND c.IsActive=1 Order by c.HotpOrder", categoryTypeID, fromdate, todate));
            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    contentsList.Add(this.MakeRowToObject(dr));
                }
            return contentsList;
        }
    }
    public List<ContentObj> getRecordsWithPermission(int categoryTypeID, DateTime fromdate, DateTime todate, int userID)
    {
        if (fromdate == null || todate == null || fromdate == DateTime.MinValue || todate == DateTime.MinValue)
        {
            return this.getRecordsWithPermission(categoryTypeID, userID);
        }
        else
        {
            List<ContentObj> contentsList = new List<ContentObj>();
            DataTable dt = osfcon.getRows(string.Format(@"
                                            SELECT a.* FROM 
                                            (  select * from Content Con where Con.Date BETWEEN '{0}' AND '{1}' AND Con.CategoryID
                                            in(SELECT CategoryID from Categories where CategoryTypeID = {2})) A  join UserSectionPermission up on 
                                            A.CategoryID = up.CategoryID AND up.HasPermission = 1 and UP.UserID = {3}", fromdate, todate, categoryTypeID, userID));
            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    contentsList.Add(this.MakeRowToObject(dr));
                }
            return contentsList;
        }
    }

    public List<ContentObj> getRecordsWithPermission(int categoryTypeID, int userID)
    {

        List<ContentObj> contentsList = new List<ContentObj>();
        DataTable dt = osfcon.getRows(string.Format(@"
                                            SELECT a.* FROM 
                                            (  select * from Content Con where  Con.CategoryID
                                            in(SELECT CategoryID from Categories where CategoryTypeID = {0})) A  join UserSectionPermission up on 
                                            A.CategoryID = up.CategoryID AND up.HasPermission = 1 and UP.UserID = {1} Order by A.Date DESC", categoryTypeID, userID));
        if (dt != null)
            foreach (DataRow dr in dt.Rows)
            {
                contentsList.Add(this.MakeRowToObject(dr));
            }
        return contentsList;

    }

    public List<ContentObj> getRecordsbyCategoryTypeID(int categoryTypeID)
    {
        List<ContentObj> contentsList = new List<ContentObj>();
        DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Categories cc JOIN Content c On cc.CategoryID = c.CategoryID AND cc.CategoryTypeID= {0} AND c.IsActive=1 Order by c.Date DESC", categoryTypeID));
        if (dt != null)
            foreach (DataRow dr in dt.Rows)
            {
                contentsList.Add(this.MakeRowToObject(dr));
            }
        return contentsList;
    }

    public List<ContentObj> getRecordsbyCategoryID(int categoryID)
    {
        List<ContentObj> contentsList = new List<ContentObj>();
        DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Categories cc JOIN Content c On cc.CategoryID = c.CategoryID AND cc.CategoryID= {0} AND c.IsActive=1 Order by c.Date DESC", categoryID));
        if (dt != null && dt.Rows.Count > 0)
            foreach (DataRow dr in dt.Rows)
            {
                contentsList.Add(this.MakeRowToObject(dr));
            }
        return contentsList;
    }

    public List<ContentObj> getRecordsbyCategoryIDANDChapter(int categoryID, int chapterID)
    {
        List<ContentObj> contentsList = new List<ContentObj>();
        DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Categories cc JOIN Content c On cc.CategoryID = c.CategoryID AND cc.CategoryID= {0} AND c.IsActive=1 AND c.Chapter={1} Order by c.Date DESC", categoryID,chapterID));
        if (dt != null && dt.Rows.Count > 0)
            foreach (DataRow dr in dt.Rows)
            {
                contentsList.Add(this.MakeRowToObject(dr));
            }
        return contentsList;
    }

    public ContentObj getRecordFromID(int ContentID)
    {
        ContentObj content = new ContentObj();
        DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Content WHERE ContentID= {0}", ContentID));
        if (dt != null)
            foreach (DataRow dr in dt.Rows)
            {
                content = this.MakeRowToObject(dr);
            }
        return content;
    }



    /// <summary>Delete a row from the Content table</summary>
    /// <param name="_ContentID">A primary key column in the Content table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _ContentID)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Content\" WHERE ContentID = @ContentID", conn);
            cmd.Parameters.AddWithValue("@ContentID", _ContentID);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            disconnect();
            lastError = translateException(ex);
            return false;
            }
        catch(Exception ex)
            {
            disconnect();
            lastError = ex.Message;
            return false;
            }
        blank();
        return true;
        }

    /// <summary>Delete a row from the Content table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the Content table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Content\" WHERE ContentID = @ContentID", conn);
            cmd.Parameters.AddWithValue("@ContentID", row["ContentID"]);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Delete the current row from the Content table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(ContentID);
        }

    /// <summary>Get rows from the Content table</summary>
    /// <param name="ColumnList">A list of column names in the Content table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause, string orderBy)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        string ob = orderBy;

        try
            {
            connect();
            if(!orderBy.Equals("")) ob = " ORDER BY " + ob;
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Content\" WHERE " + whereClause + ob, conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            }
        return null;
        }

    /// <summary>Get rows from the Content table</summary>
    /// <param name="ColumnList">A list of column names in the Content table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Content table</summary>
    /// <param name="ColumnList">An array of column names in the Content</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause, string orderBy)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRows(cl, whereClause, orderBy);
        }

    /// <summary>Get a rows from the Content table</summary>
    /// <param name="ColumnList">An array of column names in the Content</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Content table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Content</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause, string orderBy)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRows(cl, whereClause, orderBy);
        }


    /// <summary>Get a rows from the Content table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Content</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Content table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the Content table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"Content\" (Author, Date, Title, Content,  Extension, URL, CategoryID, ContentTypeID,  CreatedBy, CreatedOn,RootThreadID,Chapter) VALUES (@Author, @Date, @Title, @Content, @Extension, @URL, @CategoryID, @ContentTypeID, @CreatedBy, @CreatedOn, @RootThreadID, @Chapter)", conn);
            cmd.Parameters.AddWithValue("@Author", Author);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Content", Content);
            //cmd.Parameters.AddWithValue("@BlobContent", BlobContent);
            cmd.Parameters.AddWithValue("@Extension", Extension);
            cmd.Parameters.AddWithValue("@URL", URL);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@ContentTypeID", ContentTypeID);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("@RootThreadID", RootThreadID);
            cmd.Parameters.AddWithValue("@Chapter", Chapter);
            cmd.ExecuteScalar();
            cmd.Dispose();
 
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }


    public int insert_Content()
    {
        SqlCommand cmd;
        int checkInsert = 0;
        try
        {
            connect();
            cmd = new SqlCommand("INSERT INTO \"Content\" (Author, Date, Title, Content,  Extension, URL, CategoryID, ContentTypeID,  CreatedBy, CreatedOn,RootThreadID) VALUES (@Author, @Date, @Title, @Content, @Extension, @URL, @CategoryID, @ContentTypeID, @CreatedBy, @CreatedOn, @RootThreadID)", conn);
            cmd.Parameters.AddWithValue("@Author", Author);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Content", Content);
            //cmd.Parameters.AddWithValue("@BlobContent", BlobContent);
            cmd.Parameters.AddWithValue("@Extension", Extension);
            cmd.Parameters.AddWithValue("@URL", URL);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@ContentTypeID", ContentTypeID);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("@RootThreadID", RootThreadID);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.CommandText = "Select @@Identity";
            checkInsert = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd.Dispose();


            disconnect();
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
            disconnect();
            return 0;
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
            disconnect();
            return 0;
        }
        return checkInsert;
    }


    /// <summary>Insert a record in the Content table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the Content table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool insert(DataRow row)
        {
        SqlCommand cmd;
        string setList = "";
        string valList = "";

        try
            {
            connect();
            // Build the set list
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "contentid":
                        break;
                    default:
                        if(!setList.Equals(""))
                            {
                            setList += ", ";
                            valList += ", ";
                            }
                        setList += col.ColumnName;
                        valList += " @" + col.ColumnName;
                        break;
                    }
                }
            cmd = new SqlCommand("INSERT INTO \"Content\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "ContentID":
                        break;
                    default:
                        cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col.ColumnName]);
                        break;
                    }
                }
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Update a record in the Content table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;
        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"Content\" SET Author = @Author, Date = @Date, Title = @Title, Content = @Content, Extension = @Extension, URL = @URL, CategoryID = @CategoryID, ContentTypeID = @ContentTypeID, ModifiedBy=@ModifiedBy, ModifiedOn=@ModifiedOn, RootThreadID=@RootThreadID  WHERE ContentID = @ContentID", conn);
            cmd.Parameters.AddWithValue("@ContentID", ContentID);
            cmd.Parameters.AddWithValue("@Author", Author);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Content", Content);

            cmd.Parameters.AddWithValue("@Extension", Extension);
            cmd.Parameters.AddWithValue("@URL", URL);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@ContentTypeID", ContentTypeID);
            cmd.Parameters.AddWithValue("@RootThreadID", RootThreadID);

            cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedOn", ModifiedOn);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Update the Content table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the Content table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool update(DataRow row)
        {
        SqlCommand cmd;
        String setList = "";

        try
            {
            connect();
            // Build the set list
            foreach(DataColumn col in row.Table.Columns)
                {
                if(!setList.Equals("")) setList += ", ";
                setList += col.ColumnName + " = @" + col.ColumnName;
                }
            cmd = new SqlCommand("UPDATE \"Content\" SET " + setList + " WHERE ContentID = @ContentID", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col.ColumnName]);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary></summary>
    /// <returns>The column names from Content table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(ContentID) + ", ";
        p += Author + ", ";
        p += ((Date == null) ? "<null>" : Date.ToString()) + ", ";
        p += Title + ", ";
        p += Content + ", ";
        p += ((BlobContent == null) ? "<null>" : BlobContent.ToString()) + ", ";
        p += Extension + ", ";
        p += URL + ", ";
        p += Convert.ToString(CategoryID) + ", ";
        p += Convert.ToString(ContentTypeID) + ", ";
        p += Convert.ToString(RootThreadID) + ", ";
        p += Convert.ToString(Chapter) + ", ";
        p += Convert.ToString(CommitteeID) + ", ";
        p += Convert.ToString(UpdatedByID) + ", ";
        p += ((LastUpdated == null) ? "<null>" : LastUpdated.ToString()) + ", ";
        p += Convert.ToString(HotpOrder) + ", ";
        p += Convert.ToString(PrivateCategoryID) + ", ";
        p += Convert.ToString(Chapter2008CategoryID) + ", ";
        p += Convert.ToString(ACCommonCategoryID) + ", ";
        p += Convert.ToString(Election2008CategoryID);
        return p;
        }

#endregion

#region Private Methods
    private void connect()
        {
        if(connectionString == "")
            {
            if(conn == null) throw new Exception("Database not connected");
            }
        else
            {
            conn = new SqlConnection(connectionString);
            conn.Open();
            }
        }

    private void disconnect()
        {
        if(!connectionString.Equals("") && conn != null)
           {
            try
                {
                conn.Close();
                conn.Dispose();
                conn = null;
                }
            catch(Exception ex) {}
            }
        }

    private string translateException(SqlException ex)
        {
        string p = "";
        foreach(SqlError er in ex.Errors)
            p += er.Message + "\r\n";
        return p;
        }
#endregion

#region User Code
#endregion
}
