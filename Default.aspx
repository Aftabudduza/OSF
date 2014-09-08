<%@ Page Title="Home Page" Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Controls/Header.ascx" TagPrefix="OSF" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="OSF" TagName="Footer" %>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>OFS Connect</title>
    <!--Main CSS-->
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
</head>
<body>
    <div id="page">
        <!-- Header -->
        <div class="header">
           <OSF:Header runat="server" ID="Header" />
           
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
            <OSF:Footer runat="server" ID="Footer" />
        </div>
        <!-- End Footer -->
    </div>
</body>
</html>
