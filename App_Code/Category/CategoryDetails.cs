using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class CategoryDetails
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
    public short ContentTypeID;
    public string TitleTitle;
    public string AuthorTitle;
    public string DateTitle;
    public string ContentTitle;
    public bool ShowTitle;
    public bool ShowAuthor;
    public bool ShowDate;
    public bool ShowContent;
    public string DefaultTitle;
    public string DefaultAuthor;
    public DateTime DefaultDate;
    public string DefaultContent;
    public bool IsQuickLaunch;
    public int ItemsPerPage;
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
    public CategoryDetails()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("CategoryID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("ContentTypeID", "short", 6, true, true));
        colInfo.Add(new ColumnInfo("TitleTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("AuthorTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("DateTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("ContentTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("ShowTitle", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("ShowAuthor", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("ShowDate", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("ShowContent", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("DefaultTitle", "string", 300, true, true));
        colInfo.Add(new ColumnInfo("DefaultAuthor", "string", 300, true, true));
        colInfo.Add(new ColumnInfo("DefaultDate", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("DefaultContent", "string", 300, true, true));
        colInfo.Add(new ColumnInfo("IsQuickLaunch", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("ItemsPerPage", "int", 11, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public CategoryDetails(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public CategoryDetails(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a CategoryDetails table record</summary>
    public void blank()
        {
        CategoryID = 0;
        ContentTypeID = 0;
        TitleTitle = "";
        AuthorTitle = "";
        DateTitle = "";
        ContentTitle = "";
        ShowTitle = false;
        ShowAuthor = false;
        ShowDate = false;
        ShowContent = false;
        DefaultTitle = "";
        DefaultAuthor = "";
        DefaultDate = DateTime.Today;
        DefaultContent = "";
        IsQuickLaunch = false;
        ItemsPerPage = 0;
        valid = false;
        RID = 0;
        }

    /// <summary>Get rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">A list of column names in the CategoryDetails table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"CategoryDetails\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">A list of column names in the CategoryDetails table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">An array of column names in the CategoryDetails</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
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

    /// <summary>Get a rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">An array of column names in the CategoryDetails</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the CategoryDetails</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
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


    /// <summary>Get a rows from the CategoryDetails table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the CategoryDetails</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the CategoryDetails table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the CategoryDetails table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"CategoryDetails\" (CategoryID, ContentTypeID, TitleTitle, AuthorTitle, DateTitle, ContentTitle, ShowTitle, ShowAuthor, ShowDate, ShowContent, DefaultTitle, DefaultAuthor, DefaultDate, DefaultContent, IsQuickLaunch, ItemsPerPage) VALUES (@CategoryID, @ContentTypeID, @TitleTitle, @AuthorTitle, @DateTitle, @ContentTitle, @ShowTitle, @ShowAuthor, @ShowDate, @ShowContent, @DefaultTitle, @DefaultAuthor, @DefaultDate, @DefaultContent, @IsQuickLaunch, @ItemsPerPage)", conn);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@ContentTypeID", ContentTypeID);
            cmd.Parameters.AddWithValue("@TitleTitle", TitleTitle);
            cmd.Parameters.AddWithValue("@AuthorTitle", AuthorTitle);
            cmd.Parameters.AddWithValue("@DateTitle", DateTitle);
            cmd.Parameters.AddWithValue("@ContentTitle", ContentTitle);
            cmd.Parameters.AddWithValue("@ShowTitle", ShowTitle);
            cmd.Parameters.AddWithValue("@ShowAuthor", ShowAuthor);
            cmd.Parameters.AddWithValue("@ShowDate", ShowDate);
            cmd.Parameters.AddWithValue("@ShowContent", ShowContent);
            cmd.Parameters.AddWithValue("@DefaultTitle", DefaultTitle);
            cmd.Parameters.AddWithValue("@DefaultAuthor", DefaultAuthor);
            cmd.Parameters.AddWithValue("@DefaultDate", DefaultDate);
            cmd.Parameters.AddWithValue("@DefaultContent", DefaultContent);
            cmd.Parameters.AddWithValue("@IsQuickLaunch", IsQuickLaunch);
            cmd.Parameters.AddWithValue("@ItemsPerPage", ItemsPerPage);
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

    /// <summary>Insert a record in the CategoryDetails table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the CategoryDetails table</param>
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
            cmd = new SqlCommand("INSERT INTO \"CategoryDetails\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
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

    public bool delete(int vId)
    {
        SqlCommand cmd;

        try
        {
            connect();
            cmd = new SqlCommand("DELETE FROM \"CategoryDetails\" WHERE CategoryId = @CategoryID", conn);
            cmd.Parameters.AddWithValue("@CategoryID", vId);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }

        catch (Exception ex)
        {
            disconnect();

            return false;
        }

        return true;
    }


    public bool update()
    {
        SqlCommand cmd;


        try
        {
            connect();
            cmd = new SqlCommand("UPDATE \"CategoryDetails\" SET TitleTitle = @TitleTitle, AuthorTitle = @AuthorTitle, DateTitle = @DateTitle, ContentTitle = @ContentTitle, ShowTitle = @ShowTitle, ShowAuthor = @ShowAuthor, ShowDate = @ShowDate, ShowContent = @ShowContent, DefaultTitle = @DefaultTitle, DefaultAuthor = @DefaultAuthor, DefaultDate = @DefaultDate, DefaultContent = @DefaultContent, IsQuickLaunch = @IsQuickLaunch, ItemsPerPage = @ItemsPerPage WHERE CategoryID = @CategoryID", conn);
           
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@ContentTypeID", ContentTypeID);
            cmd.Parameters.AddWithValue("@TitleTitle", TitleTitle);
            cmd.Parameters.AddWithValue("@AuthorTitle", AuthorTitle);
            cmd.Parameters.AddWithValue("@DateTitle", DateTitle);
            cmd.Parameters.AddWithValue("@ContentTitle", ContentTitle);
            cmd.Parameters.AddWithValue("@ShowTitle", ShowTitle);
            cmd.Parameters.AddWithValue("@ShowAuthor", ShowAuthor);
            cmd.Parameters.AddWithValue("@ShowDate", ShowDate);
            cmd.Parameters.AddWithValue("@ShowContent", ShowContent);
            cmd.Parameters.AddWithValue("@DefaultTitle", DefaultTitle);
            cmd.Parameters.AddWithValue("@DefaultAuthor", DefaultAuthor);
            cmd.Parameters.AddWithValue("@DefaultDate", DefaultDate);
            cmd.Parameters.AddWithValue("@DefaultContent", DefaultContent);
            cmd.Parameters.AddWithValue("@IsQuickLaunch", IsQuickLaunch);
            cmd.Parameters.AddWithValue("@ItemsPerPage", ItemsPerPage);

            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }

        catch (Exception ex)
        {

            return false;
        }
        return true;
    }

    public bool QueryExecute(string query)
    {
        SqlCommand cmd;


        try
        {
            connect();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
            disconnect();
            return false;
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
            disconnect();
            return false;
        }
        return true;
    }

    /// <summary></summary>
    /// <returns>The column names from CategoryDetails table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(CategoryID) + ", ";
        p += Convert.ToString(ContentTypeID) + ", ";
        p += TitleTitle + ", ";
        p += AuthorTitle + ", ";
        p += DateTitle + ", ";
        p += ContentTitle + ", ";
        p += Convert.ToString(ShowTitle) + ", ";
        p += Convert.ToString(ShowAuthor) + ", ";
        p += Convert.ToString(ShowDate) + ", ";
        p += Convert.ToString(ShowContent) + ", ";
        p += DefaultTitle + ", ";
        p += DefaultAuthor + ", ";
        p += Convert.ToString(DefaultDate) + ", ";
        p += DefaultContent + ", ";
        p += Convert.ToString(IsQuickLaunch) + ", ";
        p += Convert.ToString(ItemsPerPage);
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
