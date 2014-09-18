<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Controls_Header" %>
<div class="header-top">
    <div class="header-menu">
        <ul>
            <li><a href="#">Password Maint.</a></li>
            <li>|</li>
            <li><a href="#">Contacts</a></li>
            <li>|</li>
            <li><a href="#">Help</a></li>
            <li>|</li>
            <li><a href="../Admin/Logout.aspx">Log Out</a></li>
        </ul>
    </div>
</div>
<div style="float: left; width: 100%">
    <!-- Search -->
    <div class="search-form">
        <form>
        <input type="text" value="" name="" placeholder="Search" />
        <input type="submit" value="" name="" />
        </form>
    </div>
    <!-- End Search -->
</div>
<!-- Navigation -->
<div id="navigation">
    <div class="wrapper">
        <div class="nav">
            <ul>
                <% 
                           
                    if (Session["User"] != null && Session["UserPermission"] != null && Session["UserSectionPermission"] != null)
                    {
                        Users objUser = (Users)Session["User"];
                        UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
                        System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
                        //   if(
                                  
                %>
                <li><a href="../Default.aspx">Home</a></li>
                <%
                    string query = string.Format("SectionID={0}", (int)SectionTypeEnum.News);
                    System.Data.DataRow dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    {
                                  
                %>
                <li>|</li>
                <li><a href="../Contents/NewsorContents.aspx?SectionTypeID=17">News</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Calender);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    {
                                  
                %>
                <li>|</li>
                <li><a href="../Contents/NewsorContents.aspx?SectionTypeID=18">Calender</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Directory);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    {
                                  
                %>
                <li>|</li>
                <li><a href="../Admin/User.aspx">Directory</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.BulletinBoard);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    {
                                  
                %>
                <li>|</li>
                <li><a href="../Contents/NewsorContents.aspx?SectionTypeID=20">Bulletin Board</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Email);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="#">Email</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Discusstion);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Admin/Discussion.aspx">Discussion</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.ACCommon);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/BoxContents.aspx?SectionTypeID=8">AC Common</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.AreaChapter);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/BoxContents.aspx?SectionTypeID=12">Area Chapter</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Committee);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/BoxContents.aspx?SectionTypeID=2">Committee</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.ChapterDirectives);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/MultilayerBoxContents.aspx?SectionTypeID=4">Chapter Directive</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Reference);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/MultilayerBoxContents.aspx?SectionTypeID=23"">Reference</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Private);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/BoxContents.aspx?SectionTypeID=10">Private</a></li>
                <%
} %>
                <%
                    query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Chapter2008);
                    dr = dtUSP.Select(query).FirstOrDefault();
                    if (dr != null && Convert.ToBoolean(dr["HasPermission"]))
                    { %>
                <li>|</li>
                <li><a href="../Contents/BoxContents.aspx?SectionTypeID=7">Chapter2014</a></li>
                <%
} %>
                <%                           
                          
                             
                             
                    if (Convert.ToBoolean(dtUP.IsGlobalAdmin || dtUP.IsGlobalContentAdmin))
                    {
                %>
                <li>|</li>
                <li><a href="../Admin/AdminIndex.aspx">Admin</a></li>
                <%}
                                } %>
            </ul>
        </div>
    </div>
</div>
<!-- End Navigation -->
