<%@ Page Title="Home Page" Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>OFS Connect</title>
    <!--Main CSS-->
    <link rel="stylesheet" type="text/css" href="css/main.css" />
</head>
<body>
    <div id="page">
        <!-- Header -->
        <div class="header">
            <div class="header-top">
                <div class="header-menu">
                    <ul>
                        <li><a href="#">Password Maint.</a></li>
                        <li>|</li>
                        <li><a href="#">Contacts</a></li>
                        <li>|</li>
                        <li><a href="#">Help</a></li>
                        <li>|</li>
                        <li><a href="../OSF/Admin/Logout.aspx">Log Out</a></li>
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
                            <li><a href="#">Home</a></li>
                            <%
                                string query = string.Format("SectionID={0}", (int)SectionTypeEnum.News);
                                System.Data.DataRow dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                {
                                  
                            %>
                            <li>|</li>
                            <li><a href="Contents/NewsorContents.aspx?SectionTypeID=17">News</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Calender);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                {
                                  
                            %>
                            <li>|</li>
                            <li><a href="Contents/NewsorContents.aspx?SectionTypeID=18">Calender</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Directory);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                {
                                  
                            %>
                            <li>|</li>
                            <li><a href="Admin/User.aspx">Directory</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.BulletinBoard);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                {
                                  
                            %>
                            <li>|</li>
                            <li><a href="Contents/NewsorContents.aspx?SectionTypeID=20">Bulletin Board</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Email);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Email</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Discusstion);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Discussion</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.ACCommon);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">A.C. Common</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.AreaChapter);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Area Chapter</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Committee);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Commetee</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.ChapterDirectives);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Chapter Directive</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Reference);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Reference</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Private);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Private</a></li>
                            <%
} %>
                            <%
                                query = string.Format("SectionID={0} AND IsSection=1", (int)SectionTypeEnum.Chapter2008);
                                dr = dtUSP.Select(query).FirstOrDefault();
                                if (Convert.ToBoolean(dr["HasPermission"]))
                                { %>
                            <li>|</li>
                            <li><a href="#">Chapter2014</a></li>
                            <%
} %>
                            <%                           
                          
                             
                             
                                if (Convert.ToBoolean(dtUP.IsGlobalAdmin || dtUP.IsGlobalContentAdmin))
                                {
                            %>
                            <li>|</li>
                            <li><a href="Admin/AdminIndex.aspx">Admin</a></li>
                            <%}
                                } %>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- End Navigation -->
        </div>
        <!-- End Header -->
        <!--Container-->
        <div class="container">
            <div class="wrapper">
                <div class="left-sidebar">
                    <div class="colomn">
                        <h3>
                            Check This Out!</h3>
                        <div class="check">
                            <ul>
                                <li><a href="#">Private Postings - Week of 7/7/14</a></li>
                                <li><a href="#">Advocacy Action of the Week</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="colomn">
                        <h3>
                            Sisters' Illnesses</h3>
                        <div class="illnesses">
                            <ul>
                                <li><a href="#">Sr. Pat Hinton</a></li>
                                <li><a href="#">Sr. Ann McFadden</a></li>
                                <li><a href="#">Sr. Dominica LoBianco</a></li>
                                <li><a href="#">Sr. Mary O'Mahony</a></li>
                                <li><a href="#">Sr. Mercedes Rojo</a></li>
                                <li><a href="#">Sr. Theresa Ulshafer</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="colomn">
                        <h3>
                            Death Notices</h3>
                        <div class="notices">
                            <ul>
                                <li><span>7/22/2014</span><a href="#">Elizabeth DeWaele</a></li>
                                <li><span>7/22/2014</span><a href="#">Santina Bengardino</a></li>
                                <li><span>7/22/2014</span><a href="#">Harry Bakow</a></li>
                                <li><span>7/22/2014</span><a href="#">Peter Gerry</a></li>
                                <li><span>7/22/2014</span><a href="#">Roger Harrell</a></li>
                                <li><span>7/22/2014</span><a href="#">Bernard Dangelmaier</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="colomn">
                        <h3>
                            Link</h3>
                        <div class="link">
                            <ul>
                                <li><a href="#">Congregation's Website</a></li>
                                <li><a href="#">Franciscan Spiritual Center - Aston</a></li>
                                <li><a href="#">Franciscan Spiritual Center-Milwaukie, OR</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="mid-container">
                    <h3>
                        Recent News at OSF</h3>
                    <div id="dynamicDiv" runat="server">
                    </div>
                </div>
                <div class="right-sidebar">
                    <div class="colomn">
                        <img alt="Community-News" src="images/community-news.jpg" />
                        <a class="community-news-link" href="#">Community News Archive</a>
                    </div>
                    <div class="colomn">
                        <h3>
                            Advocacy Alerts</h3>
                        <div class="alerts">
                            <ul>
                                <li><span>7/22/2014</span><a href="#">1000 Kids for Iowa</a></li>
                                <li><span>7/22/2014</span><a href="#">Take Action</a></li>
                                <li><span>7/22/2014</span><a href="#">Articles About the Border Refugees</a></li>
                                <li><span>7/22/2014</span><a href="#">Speaking Tips</a></li>
                                <li><span>7/22/2014</span><a href="#">Action Alert - Refugees</a></li>
                                <li><span>7/22/2014</span><a href="#">Just Action</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="colomn">
                        <h3>
                            Upcoming Events</h3>
                        <div class="events">
                            <ul>
                                <li><span>7/22/2014</span><a href="#">Road to Healthier Living Workshop</a></li>
                                <li><span>7/22/2014</span><a href="#">Road to Healthier Living Workshop</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="colomn">
                        <h3>
                            Prayer Requests</h3>
                        <div class="prayer">
                            <ul>
                                <li><span>7/22/2014</span><a href="#">1000 Kids for Iowa</a></li>
                                <li><span>7/22/2014</span><a href="#">Take Action</a></li>
                                <li><span>7/22/2014</span><a href="#">Articles About the Border Refugees</a></li>
                                <li><span>7/22/2014</span><a href="#">Speaking Tips</a></li>
                                <li><span>7/22/2014</span><a href="#">Action Alert - Refugees</a></li>
                                <li><span>7/22/2014</span><a href="#">Just Action</a></li>
                                <li class="archive"><a href="#">Archive</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--End container-->
        <!-- Footer -->
        <div class="footer">
            <p>
                <asp:Label ID="lblCopyright" runat="server" Text=""></asp:Label>
                </p>
        </div>
        <!-- End Footer -->
    </div>
</body>
</html>
