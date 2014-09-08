<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="AdminIndex.aspx.cs" Inherits="Admin_AdminIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="innerDivCat">
    <table width="1315px" align="left" cellspacing="10" cellpadding="5" border="1" class="MYtable">
            <tbody>
                <tr>
                    <td align="left" colspan="5" class="standardTextBold">
                        <h3 class="OFSh3">
                            Intranet Administration</h3>
                    </td>
                </tr>
                <tr>
                    <td class="standardText">
                        <strong>Add content</strong>
                    </td>
                    <td class="standardText">
                        <strong>List</strong>
                    </td>
                    <td class="standardText">
                        <strong>Reports</strong>
                    </td>
                    <td colspan="2" class="standardText">
                        <strong>Manage</strong>
                    </td>
                </tr>
                <tr>
                
                    <td class="standardText">
                     <%
                         if(Convert.ToBoolean(Session["IsGlobalContentAdmin"] ))
                         { %> 
                        <a href="AdminContent.aspx?CategoryID=2&amp;ContentScope=1" id="NewsLink">Content</a>
                       <% 
                         }
                           
                     %>
                    </td>
                    <td class="standardText">
                        <a href="AdminJobs.aspx" id="JobsLink">Jobs List</a>
                    </td>
                    <td class="standardText">
                        <a id="A5" href="Reports.aspx?Mode=Active">View Active Users </a>
                    </td>
                    <td class="standardText">
                                          <a href="ManageCategory.aspx?CategoryID=2&amp;ContentScope=1" id="A9">Category</a>
                        <%--<a href="AdminEditIsps.aspx" id="ISPLink">Email Providers</a>--%>
                    </td>
                    <td class="standardText">
                        <a href="AdminChapter2008Members.aspx" id="chapter2008">Chapter 2014</a>
                    </td>
                </tr>
                <tr>
                    <td class="standardText">
   <%--                     <a href="AdminContent.aspx?CategoryID=3&amp;ContentScope=1" id="IllnessLink">Sisters'
                            Illnesses</a>--%>
                            <a href="adminChapterDirectivesTopic.aspx?CategoryID=5&amp" id="CDLink">Chapter Directives</a>
                    </td>
                    <td>
                        <a href="AdminLocations.aspx" id="LocationsLink">Locations List</a>
                    </td>
                    <td class="standardText">
                        <a id="A6" href="Reports.aspx?Mode=Inactive">View InActive Users </a>
                    </td>
                    <td>
               <%--         <a href="AdminSurveyIndex.aspx" id="SurveyLink">Surveys</a>--%>
                           <a href="BasicData.aspx?CategoryID=2&amp;ContentScope=1" id="A10">Basic Data</a>
                    </td>
                    <td>
                        <a href="AdminACCommonMembers.aspx" id="Hyperlink2">AC Common</a>
                    </td>
                </tr>
                <tr>
                    <td class="standardText">
                        <a href="adminBulletinBoard.aspx?CategoryID=6&amp" id="BBLink">Bulletin Board</a>
                    </td>
                    <td>
                        <a href="AdminDepartments.aspx" id="DepartmentsLink">Departments List</a>
                    </td>
                    <td class="standardText">
                        <a id="A7" href="InActiveUsers.aspx">View Deleted Users </a>
                    </td>
                    <td>
                        <%--<a href="AdminCommunityNewsImage.aspx" id="CommNewsIconLink">Community News Icon</a>--%>
                                                <a id="A1" href="User.aspx?CategoryID=4&amp;ContentScope=1">Users</a>
                    </td>
                    <td>
                        <a href="AdminResetPassword.aspx" id="Hyperlink3">Reset Password</a>
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                                            <a href="adminReferenceTopic.aspx?CategoryID=80&amp" id="Hyperlink5">Reference</a>
                        <%--<a href="AdminContent.aspx?CategoryID=33&amp;ContentScope=1" id="HelpLink">Help/Faqs</a>--%>
                    </td>
                    <td>
                    </td>
                    <td class="standardtext">
                    </td>
                    <td>
                        <%--<a href="AdminSystemSettings.aspx" id="SystemSettingsLink">System Settings</a>--%>
                    </td>
                    <td>
                        <a href="AdminEditAreaChapterHotLinks.aspx" id="Hyperlink4">Manage AC Hotlinks</a>
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
              <%--          <a href="AdminContent.aspx?CategoryID=30&amp;ContentScope=1" id="CommNewsLink">Community
                            News</a>--%>
                            &nbsp;
                    </td>
                    <td class="standardtext">
                    </td>
                    <td class="standardtext">
                    </td>
                    <td class="standardtext">
                        <%--<a href="AdminPrivateMembers.aspx" id="AdminPCLink">Private Admin</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        <%--<a href="AdminContent.aspx?CategoryID=72&amp;ContentScope=1" id="NewLink">New Link</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">
                    </td>
                    <td class="standardtext">
                        <a id="ActiveuserReport" href="Reports.aspx?Mode=Active"></a>
                    </td>
                    <td class="standardtext">
                  <%--      <a href="ManageAreaChapter.aspx" id="AreaChapter">Manage Area Chapter</a> <a id="deletedUsers"
                            href="Reports.aspx?Mode=Inactive"></a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        <%--<a id="A2" href="AdminContent.aspx?CategoryID=75&amp;ContentScope=1">HOTP</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">
                      <a href="Category.aspx?CategoryID=2&amp;ContentScope=1" id="A8"></a>

                    </td>
                    <td class="standardtext">
                        <a href="Reports.aspx?Mode=Inactive" id="A3"></a>
                    </td>
                    <td class="standardtext">
                        <%--<a href="ManageCategory.aspx" id="Userscategory">Manage Users Category</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                       <%-- <a href="AdminContent.aspx?CategoryID=1&amp;ContentScope=1" id="EventsLink">Events</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">

                    </td>
                    <td class="standardtext">
                        <a id="A4" href="InActiveUsers.aspx"></a>
                    </td>
                    <td class="standardtext">
                      <%--  <a href="StandardContent.aspx?CategoryID=72&amp;ContentScope=1" id="EditLinks">Manage
                            Links</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        <%--<a href="AdminContent.aspx?CategoryID=4&amp;ContentScope=1" id="NoticesLink">Death Notices</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">
         
                    </td>
                    <td class="standardtext">
                    </td>
                    <td class="standardtext">
                        <%--<a href="AdminCommitteeMembers.aspx" id="CommitteeLink">Committees</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        <%--<a href="AdminContent.aspx?CategoryID=33&amp;ContentScope=1" id="FAQLink">FAQs</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">

                    </td>
                    <td>
                    </td>
                    <td class="standardtext">
              <%--          <a href="AdminCategories.aspx" id="CategoriesLink">Category Admin</a><a id="lnkHotp"
                            href="AdminContent.aspx?CategoryID=75&amp;ContentScope=1"></a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        
                    &nbsp;</td>
                    <td class="standardtext">
                    </td>
                    <td>
                    </td>
                    <td class="standardtext">
                        <%--<a href="AdminContactLists.aspx" id="AdminContactLists">Congregational Contacts</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">
                        <%--<a href="AdminContent.aspx?CategoryID=78&amp;ContentScope=1" id="Hyperlink1">Prayers</a>--%>
                    &nbsp;</td>
                    <td class="standardtext">
                    </td>
                    <td>
                    </td>
                    <td class="standardtext">
                        <%--<a href="ManageHOTPContent.aspx" id="ManageHOTPLink">Manage HOTP</a>--%>
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
                <tr>
                    <td class="standardtext">

                    &nbsp;</td>
                    <td class="standardtext">
                    </td>
                    <td>
                    </td>
                    <td class="standardtext">
                    </td>
                    <td class="standardtext">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
        
</asp:Content>
