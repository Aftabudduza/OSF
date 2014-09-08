using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class WebSiteListing
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
    public int gridPageSize;
    public long gridTotalRecords;
    public bool gridShowProgress;
    public bool gridAllowAdd = true;
    public bool gridAllowEdit = true;
    public bool gridAllowDelete = true;
    public bool gridAllowUpdate = true;
    public int gridCurrentRow = 0;
    public long gridEndRow;
    public DataTable gridTable;

    // A variable for each column in the table.
    public int Id;
    public int CompanyId;
    public string SiteName;
    public string SiteTitle;
    public string SiteType;
    public string ImageRootPath;
    public string ThumbRootPath;
    public string LogoImageFileName;
    public int LargeImageWidth;
    public int MedImageWidth;
    public int ThumbImageWidth;
    public string ImageRootFilePath;
    public string ThumbRootFilePath;
    public string NotFoundFileName;
    public string EmailServer;
    public string EmailAccount;
    public string EmailPass;
    public int EmailPort;

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
    public WebSiteListing()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("Id", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("CompanyId", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("SiteName", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("SiteTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("SiteType", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("ImageRootPath", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("ThumbRootPath", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("LogoImageFileName", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("LargeImageWidth", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("MedImageWidth", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ThumbImageWidth", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ImageRootFilePath", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("ThumbRootFilePath", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("NotFoundFileName", "string", 200, true, true));
        colInfo.Add(new ColumnInfo("EmailServer", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("EmailAccount", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("EmailPass", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("EmailPort", "int", 11, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public WebSiteListing(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public WebSiteListing(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    public WebSiteListing(SqlConnection _conn, int _Id) : this()
        {
        conn = _conn;
        getRecord(_Id);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    public WebSiteListing(string _connectionString, int _Id) : this()
        {
        connectionString = _connectionString;
        getRecord(_Id);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a WebSite table record</summary>
    public void blank()
        {
        Id = 0;
        CompanyId = 0;
        SiteName = "";
        SiteTitle = "";
        SiteType = "B2C";
        ImageRootPath = "";
        ThumbRootPath = "";
        LogoImageFileName = "";
        LargeImageWidth = 600;
        MedImageWidth = 300;
        ThumbImageWidth = 150;
        ImageRootFilePath = "";
        ThumbRootFilePath = "";
        NotFoundFileName = "";
        EmailServer = "";
        EmailAccount = "";
        EmailPass = "";
        EmailPort = 0;
        valid = false;
        }

    /// <summary>Get a DataRow from the WebSite table using the primary key</summary>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>A DataRow from the WebSite table</returns>
    public DataRow getRow(int _Id)
        {
        return getRow("*", _Id);
        }

    /// <summary>Get a DataRow from the WebSite table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the WebSite table separated by commas</param>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>A DataRow from the WebSite table</returns>
    public DataRow getRow(string columnList, int _Id)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"WebSite\" WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", _Id);
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

    /// <summary>Get a DataRow from the WebSite table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the WebSite</param>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>A DataRow from the WebSite table</returns>
    public DataRow getRow(string[] columnList, int _Id)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _Id);
        }

    /// <summary>Get a DataRow from the WebSite table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the WebSite</param>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>A DataRow from the WebSite table</returns>
    public DataRow getRow(ArrayList columnList, int _Id)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _Id);
        }

    /// <summary>Get a record from the WebSite table and populate the local variables</summary>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _Id)
        {
        DataRow dr = getRow(_Id);

        valid = false;
        if(dr != null)
            {
            Id = (dr["Id"] == DBNull.Value ? (int)0 : (int)dr["Id"]);
            CompanyId = (dr["CompanyId"] == DBNull.Value ? (int)0 : (int)dr["CompanyId"]);
            SiteName = (dr["SiteName"] == DBNull.Value ? (string)"" : (string)dr["SiteName"]);
            SiteTitle = (dr["SiteTitle"] == DBNull.Value ? (string)"" : (string)dr["SiteTitle"]);
            SiteType = (dr["SiteType"] == DBNull.Value ? (string)"B2C" : (string)dr["SiteType"]);
            ImageRootPath = (dr["ImageRootPath"] == DBNull.Value ? (string)"" : (string)dr["ImageRootPath"]);
            ThumbRootPath = (dr["ThumbRootPath"] == DBNull.Value ? (string)"" : (string)dr["ThumbRootPath"]);
            LogoImageFileName = (dr["LogoImageFileName"] == DBNull.Value ? (string)"" : (string)dr["LogoImageFileName"]);
            LargeImageWidth = (dr["LargeImageWidth"] == DBNull.Value ? (int)600 : (int)dr["LargeImageWidth"]);
            MedImageWidth = (dr["MedImageWidth"] == DBNull.Value ? (int)300 : (int)dr["MedImageWidth"]);
            ThumbImageWidth = (dr["ThumbImageWidth"] == DBNull.Value ? (int)150 : (int)dr["ThumbImageWidth"]);
            ImageRootFilePath = (dr["ImageRootFilePath"] == DBNull.Value ? (string)"" : (string)dr["ImageRootFilePath"]);
            ThumbRootFilePath = (dr["ThumbRootFilePath"] == DBNull.Value ? (string)"" : (string)dr["ThumbRootFilePath"]);
            NotFoundFileName = (dr["NotFoundFileName"] == DBNull.Value ? (string)"" : (string)dr["NotFoundFileName"]);
            EmailServer = (dr["EmailServer"] == DBNull.Value ? (string)"" : (string)dr["EmailServer"]);
            EmailAccount = (dr["EmailAccount"] == DBNull.Value ? (string)"" : (string)dr["EmailAccount"]);
            EmailPass = (dr["EmailPass"] == DBNull.Value ? (string)"" : (string)dr["EmailPass"]);
            EmailPort = (dr["EmailPort"] == DBNull.Value ? (int)0 : (int)dr["EmailPort"]);
            valid = true;
            }
        return valid;
        }

    /// <summary>Delete a row from the WebSite table</summary>
    /// <param name="_Id">A primary key column in the WebSite table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _Id)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"WebSite\" WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", _Id);
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

    /// <summary>Delete a row from the WebSite table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the WebSite table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"WebSite\" WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", row["Id"]);
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

    /// <summary>Delete the current row from the WebSite table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(Id);
        }

    /// <summary>Get rows from the WebSite table</summary>
    /// <param name="ColumnList">A list of column names in the WebSite table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the WebSite table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"WebSite\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the WebSite table</summary>
    /// <param name="ColumnList">A list of column names in the WebSite table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the WebSite table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the WebSite table</summary>
    /// <param name="ColumnList">An array of column names in the WebSite</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the WebSite table</returns>
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

    /// <summary>Get a rows from the WebSite table</summary>
    /// <param name="ColumnList">An array of column names in the WebSite</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the WebSite table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the WebSite table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the WebSite</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the WebSite table</returns>
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

    public DataTable getRows()
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            connect();
            cmd = new SqlCommand("SELECT * FROM \"Website\" ", conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
        }
        return null;
    }
    public DataTable getRows(string sql)
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            connect();
            cmd = new SqlCommand(sql, conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
        }

        catch (Exception ex)
        {

        }
        return null;
    }

    /// <summary>Get a rows from the WebSite table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the WebSite</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the WebSite table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a page of rows from the WebSite table</summary>
    /// <param name="ColumnList">A list of column names in the WebSite table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="PageSize">The number of rows in the page</param>
    /// <param name="CalcTotal">Determines whether the total number of records should be calculated and saved in 'GridTotalRecords'</param>
    /// <returns>A DataTable from the WebSite table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getPage(string columnList, string whereClause, int pageSize, bool calcTotal)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt=null;
        int i;

        gridPageSize = pageSize;
        gridShowProgress = calcTotal;
        try
            {
            connect();
            for(i=0; i<2; i++)
                {
                cmd = new SqlCommand("SELECT " + columnList + " FROM (SELECT TOP " + pageSize + " * FROM (SELECT TOP " + (gridCurrentRow+pageSize) + " " + columnList + " FROM \"WebSite\" WHERE " + whereClause + " ORDER BY Id ASC) AS big ORDER BY Id DESC) AS small ORDER BY Id ASC", conn);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                if(dt.Rows.Count > 0 || gridCurrentRow == 0) break;
                // No more rows, go back to the beginning.
                gridCurrentRow = 0;
                }
            gridEndRow = gridCurrentRow + dt.Rows.Count;
            if(calcTotal)
                {
                DataTable ct = new DataTable();
                cmd = new SqlCommand("SELECT COUNT(*) FROM \"WebSite\" WHERE " + whereClause, conn);
                da = new SqlDataAdapter(cmd);
                da.Fill(ct);
                da.Dispose();
                gridTotalRecords = (int)ct.Rows[0][0];
                }
            disconnect();
            if(dt.Rows.Count > 0) return dt;
            lastError = "No records found";
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

    /// <summary>Insert a record in the WebSite table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"WebSite\" (CompanyId, SiteName, SiteTitle, SiteType, ImageRootPath, ThumbRootPath, LogoImageFileName, LargeImageWidth, MedImageWidth, ThumbImageWidth, ImageRootFilePath, ThumbRootFilePath, NotFoundFileName, EmailServer, EmailAccount, EmailPass, EmailPort) VALUES (@CompanyId, @SiteName, @SiteTitle, @SiteType, @ImageRootPath, @ThumbRootPath, @LogoImageFileName, @LargeImageWidth, @MedImageWidth, @ThumbImageWidth, @ImageRootFilePath, @ThumbRootFilePath, @NotFoundFileName, @EmailServer, @EmailAccount, @EmailPass, @EmailPort)", conn);
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@SiteName", SiteName);
            cmd.Parameters.AddWithValue("@SiteTitle", SiteTitle);
            cmd.Parameters.AddWithValue("@SiteType", SiteType);
            cmd.Parameters.AddWithValue("@ImageRootPath", ImageRootPath);
            cmd.Parameters.AddWithValue("@ThumbRootPath", ThumbRootPath);
            cmd.Parameters.AddWithValue("@LogoImageFileName", LogoImageFileName);
            cmd.Parameters.AddWithValue("@LargeImageWidth", LargeImageWidth);
            cmd.Parameters.AddWithValue("@MedImageWidth", MedImageWidth);
            cmd.Parameters.AddWithValue("@ThumbImageWidth", ThumbImageWidth);
            cmd.Parameters.AddWithValue("@ImageRootFilePath", ImageRootFilePath);
            cmd.Parameters.AddWithValue("@ThumbRootFilePath", ThumbRootFilePath);
            cmd.Parameters.AddWithValue("@NotFoundFileName", NotFoundFileName);
            cmd.Parameters.AddWithValue("@EmailServer", EmailServer);
            cmd.Parameters.AddWithValue("@EmailAccount", EmailAccount);
            cmd.Parameters.AddWithValue("@EmailPass", EmailPass);
            cmd.Parameters.AddWithValue("@EmailPort", EmailPort);
           
           LogoImageFileName = (string)cmd.ExecuteScalar();
           NotFoundFileName = (string)cmd.ExecuteScalar();
          
            cmd.Dispose();

            // Attempt to load the auto-generated Id. This is not fool-proof.
           
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

    /// <summary>Insert a record in the WebSite table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the WebSite table</param>
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
                    case "id":
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
            cmd = new SqlCommand("INSERT INTO \"WebSite\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "Id":
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

    /// <summary>Update a record in the WebSite table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;

     
        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"WebSite\" SET CompanyId = @CompanyId, SiteName = @SiteName, SiteTitle = @SiteTitle, SiteType = @SiteType, ImageRootPath = @ImageRootPath, ThumbRootPath = @ThumbRootPath, LogoImageFileName = @LogoImageFileName, LargeImageWidth = @LargeImageWidth, MedImageWidth = @MedImageWidth, ThumbImageWidth = @ThumbImageWidth, ImageRootFilePath = @ImageRootFilePath, ThumbRootFilePath = @ThumbRootFilePath, NotFoundFileName = @NotFoundFileName, EmailServer = @EmailServer, EmailAccount = @EmailAccount, EmailPass = @EmailPass, EmailPort = @EmailPort WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@SiteName", SiteName);
            cmd.Parameters.AddWithValue("@SiteTitle", SiteTitle);
            cmd.Parameters.AddWithValue("@SiteType", SiteType);
            cmd.Parameters.AddWithValue("@ImageRootPath", ImageRootPath);
            cmd.Parameters.AddWithValue("@ThumbRootPath", ThumbRootPath);
            cmd.Parameters.AddWithValue("@LogoImageFileName", LogoImageFileName);
            cmd.Parameters.AddWithValue("@LargeImageWidth", LargeImageWidth);
            cmd.Parameters.AddWithValue("@MedImageWidth", MedImageWidth);
            cmd.Parameters.AddWithValue("@ThumbImageWidth", ThumbImageWidth);
            cmd.Parameters.AddWithValue("@ImageRootFilePath", ImageRootFilePath);
            cmd.Parameters.AddWithValue("@ThumbRootFilePath", ThumbRootFilePath);
            cmd.Parameters.AddWithValue("@NotFoundFileName", NotFoundFileName);
            cmd.Parameters.AddWithValue("@EmailServer", EmailServer);
            cmd.Parameters.AddWithValue("@EmailAccount", EmailAccount);
            cmd.Parameters.AddWithValue("@EmailPass", EmailPass);
            cmd.Parameters.AddWithValue("@EmailPort", EmailPort);

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

    /// <summary>Update the WebSite table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the WebSite table</param>
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
            cmd = new SqlCommand("UPDATE \"WebSite\" SET " + setList + " WHERE Id = @Id", conn);
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
    /// <returns>The column names from WebSite table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(Id) + ", ";
        p += Convert.ToString(CompanyId) + ", ";
        p += SiteName + ", ";
        p += SiteTitle + ", ";
        p += SiteType + ", ";
        p += ImageRootPath + ", ";
        p += ThumbRootPath + ", ";
        p += LogoImageFileName + ", ";
        p += Convert.ToString(LargeImageWidth) + ", ";
        p += Convert.ToString(MedImageWidth) + ", ";
        p += Convert.ToString(ThumbImageWidth) + ", ";
        p += ImageRootFilePath + ", ";
        p += ThumbRootFilePath + ", ";
        p += NotFoundFileName + ", ";
        p += EmailServer + ", ";
        p += EmailAccount + ", ";
        p += EmailPass + ", ";
        p += Convert.ToString(EmailPort);
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
