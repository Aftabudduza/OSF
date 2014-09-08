using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class Categories
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
    public int CategoryID;
    public string Description;
    public bool IsLeaf;
    public bool IsGlobal;
    public bool IsAreaChapter;
    public bool IsCommittee;
    public bool IsPrivate;
    public int ParentID;
    public int SortOrder;
    public short ContentTypeID_;
    public bool IsChapter2008;
    public bool IsACCommon;
    public bool IsElection2008;
    public int RefTabOrderID;
    public int SectionTypeID;
    public int CreatedBy;
    public DateTime CreatedDate;
    public int Modifiedby;
    public DateTime ModifiedDate;
    public int RID;

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
    public Categories()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("CategoryID", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("Description", "string", 255, true, true));
        colInfo.Add(new ColumnInfo("IsLeaf", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsGlobal", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsAreaChapter", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsCommittee", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsPrivate", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("ParentID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("SortOrder", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ContentTypeID_", "short", 6, true, true));
        colInfo.Add(new ColumnInfo("IsChapter2008", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsACCommon", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsElection2008", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("RefTabOrderID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("SectionTypeID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CreatedBy", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CreatedDate", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("Modifiedby", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ModifiedDate", "DateTime", 19, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public Categories(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public Categories(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    public Categories(SqlConnection _conn, int _CategoryID) : this()
        {
        conn = _conn;
        getRecord(_CategoryID);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    public Categories(string _connectionString, int _CategoryID) : this()
        {
        connectionString = _connectionString;
        getRecord(_CategoryID);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a Categories table record</summary>
    public void blank()
        {
        CategoryID = 0;
        Description = "";
        IsLeaf = false;
        IsGlobal = false;
        IsAreaChapter = false;
        IsCommittee = false;
        IsPrivate = false;
        ParentID = 0;
        SortOrder = 0;
        ContentTypeID_ = 0;
        IsChapter2008 = false;
        IsACCommon = false;
        IsElection2008 = false;
        RefTabOrderID = 0;
        SectionTypeID = 0;
        CreatedBy = 1;
        CreatedDate = new DateTime(0);
        Modifiedby = 0;
        ModifiedDate = new DateTime(0);
        valid = false;
        RID = 0;
        }

    /// <summary>Get a DataRow from the Categories table using the primary key</summary>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>A DataRow from the Categories table</returns>
    public DataRow getRow(int _CategoryID)
        {
        return getRow("*", _CategoryID);
        }

    /// <summary>Get a DataRow from the Categories table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the Categories table separated by commas</param>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>A DataRow from the Categories table</returns>
    public DataRow getRow(string columnList, int _CategoryID)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Categories\" WHERE CategoryID = @CategoryID", conn);
            cmd.Parameters.AddWithValue("@CategoryID", _CategoryID);
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

    /// <summary>Get a DataRow from the Categories table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the Categories</param>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>A DataRow from the Categories table</returns>
    public DataRow getRow(string[] columnList, int _CategoryID)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _CategoryID);
        }

    /// <summary>Get a DataRow from the Categories table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Categories</param>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>A DataRow from the Categories table</returns>
    public DataRow getRow(ArrayList columnList, int _CategoryID)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _CategoryID);
        }

    /// <summary>Get a record from the Categories table and populate the local variables</summary>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _CategoryID)
        {
        DataRow dr = getRow(_CategoryID);

        valid = false;
        if(dr != null)
            {
            CategoryID = (dr["CategoryID"] == DBNull.Value ? (int)0 : (int)dr["CategoryID"]);
            Description = (dr["Description"] == DBNull.Value ? (string)"" : (string)dr["Description"]);
            IsLeaf = (dr["IsLeaf"] == DBNull.Value ? (bool)false : (bool)dr["IsLeaf"]);
            IsGlobal = (dr["IsGlobal"] == DBNull.Value ? (bool)false : (bool)dr["IsGlobal"]);
            IsAreaChapter = (dr["IsAreaChapter"] == DBNull.Value ? (bool)false : (bool)dr["IsAreaChapter"]);
            IsCommittee = (dr["IsCommittee"] == DBNull.Value ? (bool)false : (bool)dr["IsCommittee"]);
            IsPrivate = (dr["IsPrivate"] == DBNull.Value ? (bool)false : (bool)dr["IsPrivate"]);
            ParentID = (dr["ParentID"] == DBNull.Value ? (int)0 : (int)dr["ParentID"]);
            SortOrder = (dr["SortOrder"] == DBNull.Value ? (int)0 : (int)dr["SortOrder"]);
            ContentTypeID_ = (dr["ContentTypeID_"] == DBNull.Value ? (short)0 : (short)dr["ContentTypeID_"]);
            IsChapter2008 = (dr["IsChapter2008"] == DBNull.Value ? (bool)false : (bool)dr["IsChapter2008"]);
            IsACCommon = (dr["IsACCommon"] == DBNull.Value ? (bool)false : (bool)dr["IsACCommon"]);
            IsElection2008 = (dr["IsElection2008"] == DBNull.Value ? (bool)false : (bool)dr["IsElection2008"]);
            RefTabOrderID = (dr["RefTabOrderID"] == DBNull.Value ? (int)0 : (int)dr["RefTabOrderID"]);
            SectionTypeID = (dr["SectionTypeID"] == DBNull.Value ? (int)0 : (int)dr["SectionTypeID"]);
            CreatedBy = (dr["CreatedBy"] == DBNull.Value ? (int)1 : (int)dr["CreatedBy"]);
            CreatedDate = (dr["CreatedDate"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["CreatedDate"]);
            Modifiedby = (dr["Modifiedby"] == DBNull.Value ? (int)0 : (int)dr["Modifiedby"]);
            ModifiedDate = (dr["ModifiedDate"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["ModifiedDate"]);
            valid = true;
            }
        return valid;
        }

    /// <summary>Delete a row from the Categories table</summary>
    /// <param name="_CategoryID">A primary key column in the Categories table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _CategoryID)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Categories\" WHERE CategoryID = @CategoryID", conn);
            cmd.Parameters.AddWithValue("@CategoryID", _CategoryID);
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

    /// <summary>Delete a row from the Categories table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the Categories table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Categories\" WHERE CategoryID = @CategoryID", conn);
            cmd.Parameters.AddWithValue("@CategoryID", row["CategoryID"]);
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

    /// <summary>Delete the current row from the Categories table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(CategoryID);
        }

    /// <summary>Get rows from the Categories table</summary>
    /// <param name="ColumnList">A list of column names in the Categories table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Categories table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Categories\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the Categories table</summary>
    /// <param name="ColumnList">A list of column names in the Categories table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Categories table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Categories table</summary>
    /// <param name="ColumnList">An array of column names in the Categories</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Categories table</returns>
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

    /// <summary>Get a rows from the Categories table</summary>
    /// <param name="ColumnList">An array of column names in the Categories</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Categories table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Categories table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Categories</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Categories table</returns>
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


    /// <summary>Get a rows from the Categories table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Categories</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Categories table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the Categories table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"Categories\" (Description, IsLeaf, IsGlobal, IsAreaChapter, IsCommittee, IsPrivate, ParentID, SortOrder, ContentTypeID_, IsChapter2008, IsACCommon, IsElection2008, RefTabOrderID, CategoryTypeID, CreatedBy, CreatedDate, Modifiedby, ModifiedDate) VALUES (@Description, @IsLeaf, @IsGlobal, @IsAreaChapter, @IsCommittee, @IsPrivate, @ParentID, @SortOrder, @ContentTypeID_, @IsChapter2008, @IsACCommon, @IsElection2008, @RefTabOrderID, @SectionTypeID, @CreatedBy, @CreatedDate, @Modifiedby, @ModifiedDate)", conn);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@IsLeaf", IsLeaf);
            cmd.Parameters.AddWithValue("@IsGlobal", IsGlobal);
            cmd.Parameters.AddWithValue("@IsAreaChapter", IsAreaChapter);
            cmd.Parameters.AddWithValue("@IsCommittee", IsCommittee);
            cmd.Parameters.AddWithValue("@IsPrivate", IsPrivate);
            cmd.Parameters.AddWithValue("@ParentID", ParentID);
            cmd.Parameters.AddWithValue("@SortOrder", SortOrder);
            cmd.Parameters.AddWithValue("@ContentTypeID_", ContentTypeID_);
            cmd.Parameters.AddWithValue("@IsChapter2008", IsChapter2008);
            cmd.Parameters.AddWithValue("@IsACCommon", IsACCommon);
            cmd.Parameters.AddWithValue("@IsElection2008", IsElection2008);
            cmd.Parameters.AddWithValue("@RefTabOrderID", RefTabOrderID);
            cmd.Parameters.AddWithValue("@SectionTypeID", SectionTypeID);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            cmd.Parameters.AddWithValue("@Modifiedby", Modifiedby);
            cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
            cmd.ExecuteScalar();
            cmd.Dispose();

            // Attempt to load the auto-generated Id. This is not fool-proof.
            SqlDataAdapter da;
            DataTable dt = new DataTable();

            cmd = new SqlCommand("SELECT TOP 1 CategoryID FROM \"Categories\" ORDER BY CategoryID DESC", conn);
            cmd.Parameters.AddWithValue("@Description", Description);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                CategoryID = (int)dt.Rows[0]["CategoryID"];
            }
            dt.Dispose();
            da.Dispose();
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

    /// <summary>Insert a record in the Categories table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the Categories table</param>
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
                    case "categoryid":
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
            cmd = new SqlCommand("INSERT INTO \"Categories\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "CategoryID":
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

    /// <summary>Update a record in the Categories table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;

        //if(!valid)
        //    {
        //    lastError = "A valid current record is needed to update";
        //    return false;
        //    }
        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"Categories\" SET Description = @Description, IsLeaf = @IsLeaf, IsGlobal = @IsGlobal, IsAreaChapter = @IsAreaChapter, IsCommittee = @IsCommittee, IsPrivate = @IsPrivate, ParentID = @ParentID, SortOrder = @SortOrder, ContentTypeID_ = @ContentTypeID_, IsChapter2008 = @IsChapter2008, IsACCommon = @IsACCommon, IsElection2008 = @IsElection2008, RefTabOrderID = @RefTabOrderID, CategoryTypeID = @SectionTypeID, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, Modifiedby = @Modifiedby, ModifiedDate = @ModifiedDate WHERE CategoryID = @CategoryID", conn);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@IsLeaf", IsLeaf);
            cmd.Parameters.AddWithValue("@IsGlobal", IsGlobal);
            cmd.Parameters.AddWithValue("@IsAreaChapter", IsAreaChapter);
            cmd.Parameters.AddWithValue("@IsCommittee", IsCommittee);
            cmd.Parameters.AddWithValue("@IsPrivate", IsPrivate);
            cmd.Parameters.AddWithValue("@ParentID", ParentID);
            cmd.Parameters.AddWithValue("@SortOrder", SortOrder);
            cmd.Parameters.AddWithValue("@ContentTypeID_", ContentTypeID_);
            cmd.Parameters.AddWithValue("@IsChapter2008", IsChapter2008);
            cmd.Parameters.AddWithValue("@IsACCommon", IsACCommon);
            cmd.Parameters.AddWithValue("@IsElection2008", IsElection2008);
            cmd.Parameters.AddWithValue("@RefTabOrderID", RefTabOrderID);
            cmd.Parameters.AddWithValue("@SectionTypeID", SectionTypeID);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            cmd.Parameters.AddWithValue("@Modifiedby", Modifiedby);
            cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
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

    /// <summary>Update the Categories table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the Categories table</param>
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
            cmd = new SqlCommand("UPDATE \"Categories\" SET " + setList + " WHERE CategoryID = @CategoryID", conn);
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
    /// <returns>The column names from Categories table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(CategoryID) + ", ";
        p += Description + ", ";
        p += Convert.ToString(IsLeaf) + ", ";
        p += Convert.ToString(IsGlobal) + ", ";
        p += Convert.ToString(IsAreaChapter) + ", ";
        p += Convert.ToString(IsCommittee) + ", ";
        p += Convert.ToString(IsPrivate) + ", ";
        p += Convert.ToString(ParentID) + ", ";
        p += Convert.ToString(SortOrder) + ", ";
        p += Convert.ToString(ContentTypeID_) + ", ";
        p += Convert.ToString(IsChapter2008) + ", ";
        p += Convert.ToString(IsACCommon) + ", ";
        p += Convert.ToString(IsElection2008) + ", ";
        p += Convert.ToString(RefTabOrderID) + ", ";
        p += Convert.ToString(SectionTypeID) + ", ";
        p += Convert.ToString(CreatedBy) + ", ";
        p += ((CreatedDate == null) ? "<null>" : CreatedDate.ToString()) + ", ";
        p += Convert.ToString(Modifiedby) + ", ";
        p += ((ModifiedDate == null) ? "<null>" : ModifiedDate.ToString());
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
