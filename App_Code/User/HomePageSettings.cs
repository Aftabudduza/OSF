using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;

public class HomePageSettings
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
    public int RID;
    public int CategoryID;
    public int SectionID;
    public bool IsSection;
    public bool WillShow;
    public short HomePageColumnType;

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
    public HomePageSettings()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("RID", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("CategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("SectionID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("IsSection", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("WillShow", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("HomePageColumnType", "short", 6, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public HomePageSettings(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public HomePageSettings(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    public HomePageSettings(SqlConnection _conn, int _RID) : this()
        {
        conn = _conn;
        getRecord(_RID);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    public HomePageSettings(string _connectionString, int _RID) : this()
        {
        connectionString = _connectionString;
        getRecord(_RID);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a HomePageSettings table record</summary>
    public void blank()
        {
        RID = 0;
        CategoryID = 0;
        SectionID = 0;
        IsSection = false;
        WillShow = false;
        HomePageColumnType = 0;
        valid = false;
        }

    /// <summary>Get a DataRow from the HomePageSettings table using the primary key</summary>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>A DataRow from the HomePageSettings table</returns>
    public DataRow getRow(int _RID)
        {
        return getRow("*", _RID);
        }

    /// <summary>Get a DataRow from the HomePageSettings table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the HomePageSettings table separated by commas</param>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>A DataRow from the HomePageSettings table</returns>
    public DataRow getRow(string columnList, int _RID)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"HomePageSettings\" WHERE RID = @RID", conn);
            cmd.Parameters.AddWithValue("@RID", _RID);
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

    /// <summary>Get a DataRow from the HomePageSettings table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the HomePageSettings</param>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>A DataRow from the HomePageSettings table</returns>
    public DataRow getRow(string[] columnList, int _RID)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _RID);
        }

    /// <summary>Get a DataRow from the HomePageSettings table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the HomePageSettings</param>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>A DataRow from the HomePageSettings table</returns>
    public DataRow getRow(ArrayList columnList, int _RID)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _RID);
        }

    /// <summary>Get a record from the HomePageSettings table and populate the local variables</summary>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _RID)
        {
        DataRow dr = getRow(_RID);

        valid = false;
        if(dr != null)
            {
            RID = (dr["RID"] == DBNull.Value ? (int)0 : (int)dr["RID"]);
            CategoryID = (dr["CategoryID"] == DBNull.Value ? (int)0 : (int)dr["CategoryID"]);
            SectionID = (dr["SectionID"] == DBNull.Value ? (int)0 : (int)dr["SectionID"]);
            IsSection = (dr["IsSection"] == DBNull.Value ? (bool)false : (bool)dr["IsSection"]);
            WillShow = (dr["WillShow"] == DBNull.Value ? (bool)false : (bool)dr["WillShow"]);
            HomePageColumnType = (dr["HomePageColumnType"] == DBNull.Value ? (short)0 : (short)dr["HomePageColumnType"]);
            valid = true;
            }
        return valid;
        }

    public HomePageSettings MakeObjectFromRow(DataRow dr)
    {
        HomePageSettings obj = new HomePageSettings();
        if (dr != null)
        {
            obj.RID = (dr["RID"] == DBNull.Value ? (int)0 : (int)dr["RID"]);
            obj.CategoryID = (dr["CategoryID"] == DBNull.Value ? (int)0 : (int)dr["CategoryID"]);
            obj.SectionID = (dr["SectionID"] == DBNull.Value ? (int)0 : (int)dr["SectionID"]);
            obj.IsSection = (dr["IsSection"] == DBNull.Value ? (bool)false : (bool)dr["IsSection"]);
            obj.WillShow = (dr["WillShow"] == DBNull.Value ? (bool)false : (bool)dr["WillShow"]);
            obj.HomePageColumnType = (dr["HomePageColumnType"] == DBNull.Value ? (short)0 : (short)dr["HomePageColumnType"]);

        }
        return obj;
    }
    /// <summary>Delete a row from the HomePageSettings table</summary>
    /// <param name="_RID">A primary key column in the HomePageSettings table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _RID)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"HomePageSettings\" WHERE RID = @RID", conn);
            cmd.Parameters.AddWithValue("@RID", _RID);
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

    /// <summary>Delete a row from the HomePageSettings table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the HomePageSettings table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"HomePageSettings\" WHERE RID = @RID", conn);
            cmd.Parameters.AddWithValue("@RID", row["RID"]);
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

    /// <summary>Delete the current row from the HomePageSettings table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(RID);
        }

    public List<HomePageSettings> getRecordsbyQuery(string query)
    {
        List<HomePageSettings> contentsList = new List<HomePageSettings>();
        DataTable dt = osfcon.getRows(query);
        if (dt != null && dt.Rows.Count > 0)
            foreach (DataRow dr in dt.Rows)
            {
                contentsList.Add(this.MakeObjectFromRow(dr));
            }
        return contentsList;
    }

    /// <summary>Get rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">A list of column names in the HomePageSettings table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"HomePageSettings\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">A list of column names in the HomePageSettings table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">An array of column names in the HomePageSettings</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
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

    /// <summary>Get a rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">An array of column names in the HomePageSettings</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the HomePageSettings</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
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


    /// <summary>Get a rows from the HomePageSettings table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the HomePageSettings</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the HomePageSettings table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the HomePageSettings table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"HomePageSettings\" (CategoryID, SectionID, IsSection, WillShow, HomePageColumnType) VALUES (@CategoryID, @SectionID, @IsSection, @WillShow, @HomePageColumnType)", conn);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@IsSection", IsSection);
            cmd.Parameters.AddWithValue("@WillShow", WillShow);
            cmd.Parameters.AddWithValue("@HomePageColumnType", HomePageColumnType);
            cmd.ExecuteScalar();
            cmd.Dispose();

            // Attempt to load the auto-generated Id. This is not fool-proof.
            SqlDataAdapter da;
            DataTable dt = new DataTable();

            cmd = new SqlCommand("SELECT TOP 1 RID FROM \"HomePageSettings\" WHERE CategoryID = @CategoryIDSectionID = @SectionIDIsSection = @IsSectionWillShow = @WillShowHomePageColumnType = @HomePageColumnType ORDER BY RID DESC", conn);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@IsSection", IsSection);
            cmd.Parameters.AddWithValue("@WillShow", WillShow);
            cmd.Parameters.AddWithValue("@HomePageColumnType", HomePageColumnType);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if(dt.Rows.Count > 0)
                {
                RID = (int)dt.Rows[0]["RID"];
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

    /// <summary>Insert a record in the HomePageSettings table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the HomePageSettings table</param>
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
                    case "rid":
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
            cmd = new SqlCommand("INSERT INTO \"HomePageSettings\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "RID":
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

    /// <summary>Update a record in the HomePageSettings table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;

        if(!valid)
            {
            lastError = "A valid current record is needed to update";
            return false;
            }
        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"HomePageSettings\" SET CategoryID = @CategoryID, SectionID = @SectionID, IsSection = @IsSection, WillShow = @WillShow, HomePageColumnType = @HomePageColumnType WHERE RID = @RID", conn);
            cmd.Parameters.AddWithValue("@RID", RID);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@SectionID", SectionID);
            cmd.Parameters.AddWithValue("@IsSection", IsSection);
            cmd.Parameters.AddWithValue("@WillShow", WillShow);
            cmd.Parameters.AddWithValue("@HomePageColumnType", HomePageColumnType);
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

    /// <summary>Update the HomePageSettings table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the HomePageSettings table</param>
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
            cmd = new SqlCommand("UPDATE \"HomePageSettings\" SET " + setList + " WHERE RID = @RID", conn);
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
    /// <returns>The column names from HomePageSettings table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(RID) + ", ";
        p += Convert.ToString(CategoryID) + ", ";
        p += Convert.ToString(SectionID) + ", ";
        p += Convert.ToString(IsSection) + ", ";
        p += Convert.ToString(WillShow) + ", ";
        p += Convert.ToString(HomePageColumnType);
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
