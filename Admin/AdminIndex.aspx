<%@ Page Title="OSF::Index" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    AutoEventWireup="true" CodeFile="AdminIndex.aspx.cs" Inherits="Admin_AdminIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="innerDivCat" style="margin: 20px 0 10px 118px;width:80%;">
        <h3 class="OFSh3">
            Intranet Administration</h3>
        <div class="news-osf">
            <table width="100%" align="left" cellspacing="10" cellpadding="5" border="1" class="MYtable">
                <tbody>
                    <tr>
                        <td class="standardText">
                           <span class="InternetAdministration_headerspan"><strong>Add content</strong></span>
                        </td>
                        <td class="standardText">
                           <span class="InternetAdministration_headerspan"> <strong>List</strong></span>
                        </td>   
                        <td class="standardText">
                           <span class="InternetAdministration_headerspan"> <strong>Reports</strong></span>
                        </td>
                        <td class="standardText">
                          <span class="InternetAdministration_headerspan"> <strong>Manage</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table cellspacing="10" cellpadding="5" border="0">
                                <tr>
                                    <td  class="standardText">
                                        <%
                                            if (Convert.ToBoolean(Session["IsGlobalContentAdmin"]))
                                            { %>
                                        <a href="AdminContent.aspx?CategoryID=2&amp;ContentScope=1" id="NewsLink">Content</a>
                                        <% 
                                            }
                           
                                        %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardText">
                                        <%--                     <a href="AdminContent.aspx?CategoryID=3&amp;ContentScope=1" id="IllnessLink">Sisters'
                            Illnesses</a>--%>
                                        <a href="adminChapterDirectivesTopic.aspx?CategoryID=4&amp" id="CDLink">Chapter Directives</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardText">
                                        <a href="adminBulletinBoard.aspx?CategoryID=6&amp" id="BBLink">Bulletin Board</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardtext">
                                        <a href="AdminReference.aspx" id="Hyperlink5">Reference</a>
                                        <%--<a href="AdminContent.aspx?CategoryID=33&amp;ContentScope=1" id="HelpLink">Help/Faqs</a>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="10" cellpadding="5" border="0">
                                <tr>
                                    <td class="standardText">
                                        <a href="BasicData.aspx?BType=Job&amp" id="JobsLink">Jobs List</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="BasicData.aspx?BType=Location&amp" id="LocationsLink">Locations List</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="BasicData.aspx?BType=Department&amp" id="DepartmentsLink">Departments List</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="10" cellpadding="5" border="0">
                                <tr>
                                    <td class="standardText">
                                        <a id="A5" href="UnderDev.aspx">View Active Users </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardText">
                                        <a id="A6" href="UnderDev.aspx">View InActive Users </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardText">
                                        <a id="A7" href="UnderDev.aspx">View Deleted Users </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="10" cellpadding="5" border="0">
                                <tr>
                                    <td class="standardText">
                                        <a href="ManageCategory.aspx?CategoryID=2&amp;ContentScope=1" id="A9">Category</a>
                                        <%--<a href="AdminEditIsps.aspx" id="ISPLink">Email Providers</a>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--         <a href="AdminSurveyIndex.aspx" id="SurveyLink">Surveys</a>--%>
                                        <a href="BasicData.aspx?CategoryID=2&amp;ContentScope=1" id="A10">Basic Data</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<a href="AdminCommunityNewsImage.aspx" id="CommNewsIconLink">Community News Icon</a>--%>
                                        <a id="A1" href="User.aspx?CategoryID=4&amp;ContentScope=1">Users</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="standardText">
                                        <a href="UnderDev.aspx" id="chapter2008">Chapter 2014</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="UnderDev.aspx" id="Hyperlink2">AC Common</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="UnderDev.aspx" id="Hyperlink3">Reset Password</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="UnderDev.aspx" id="Hyperlink4">Manage AC Hotlinks</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
