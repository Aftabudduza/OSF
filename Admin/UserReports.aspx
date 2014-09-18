<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="UserReports.aspx.cs" Inherits="Admin_UserReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.unobtrusive.min.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
    <style type="text/css">
        .input-validation-error
        {
            border: 1px solid #ff0000;
            background-color: #ffeeee;
        }
        .field-validation-error
        {
            color: #ff0000;
        }
        .btnBG
        {
            margin-left: 150px;
            float: left;
            display: inline;
        }
        .ctl00_ContentPlaceHolder1_ModalPopupExtender1_backgroundElement
        {
            background: #000 !important;
            opacity: 0.5;
        }
        .style1
        {
            height: 42px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="mid-container" style="margin: 20px 0 20px 205px; min-height: 0 !important;">
        <h3 class="OFSh3">
            Reports</h3>
        <div class="news-osf" style="padding-bottom:0px !important;">
            <table width="100%" cellspacing="5" cellpadding="7" border="0">
                <tbody>
                    <tr valign="middle">
                        <td align="right" class="UserDetailHeader">
                            Start Date :
                        </td>
                        <td>
                            <asp:TextBox CssClass="Reporttestboxcss" ID="txtStartDate" runat="server"></asp:TextBox>
                            <img style="margin-left: 5px; margin-top: 9px;" id="FromDateC" src="../App_Themes/images/calender.jpg"
                                width="25px" height="25px" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="FromDateC"
                                TargetControlID="txtStartDate">
                            </asp:CalendarExtender>
                        </td>
                        <td align="right" class="UserDetailHeader">
                            End Date :
                        </td>
                        <td>
                            <asp:TextBox CssClass="Reporttestboxcss" ID="txtEndDate" runat="server"></asp:TextBox>
                            <img style="margin-left: 5px; margin-top: 9px;" id="EndDate" src="../App_Themes/images/calender.jpg"
                                width="25px" height="25px" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="EndDate"
                                TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            Category :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" CssClass="Reporttestboxcss" Width="165"
                                runat="server">
                                <asp:ListItem Value="-1">---- Select Category ---</asp:ListItem>
                                <asp:ListItem Value="1">Sister</asp:ListItem>
                                <asp:ListItem Value="2">Staff</asp:ListItem>
                                <asp:ListItem Value="3">Companion</asp:ListItem>
                                <asp:ListItem Value="4">Comitee Member</asp:ListItem>
                                <asp:ListItem Value="5">All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnView" CssClass="ButtonOSF" runat="server" Text="View Report" OnClick="btnView_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="mid-container" style="margin: 0 0 0 205px;">
        <asp:GridView ID="gvReport" runat="Server" Width="100%" Style="margin: 15px 0;" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CaptionAlign="Left"
            ForeColor="#D24D8A" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
            PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
            GridLines="None" PageSize="15" SelectedIndex="0" OnPageIndexChanging="gvReport_PageIndexChanging"
            OnRowCreated="gvUser_RowCreated">
            <RowStyle BackColor="#F7F6F3" ForeColor="#D24D8A" Font-Size="Small" Font-Bold="false"
                CssClass="grid" />
            <Columns>
                <asp:TemplateField HeaderText="LastName" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("LastName")%>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="150px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FirstName" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("FirstName")%>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="150px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HomePhone" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("HomePhone")%>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HomeEmail" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("HomeEmail")%>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdId" Value='<%# Eval("UserID") %>' runat="server" />
                        <%--<asp:LinkButton ID="lbtnView" runat="server" OnClick="lbtnView_Click">View</asp:LinkButton>--%>
                        <asp:LinkButton ID="lbtnView1" runat="server" OnClick="lbtnView1_Click">View</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                NextPageText="Next" PreviousPageText="Previous" />
            <PagerStyle CssClass="form-actions tag" ForeColor="#000" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <div>
                    <div style="height: 20px">
                    </div>
                    <div style="font-size:14px;font-weight:bold;text-align:center;">
                    This Category User have no Data.
                    </div>
                </div>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#D24D8A" Font-Bold="True" ForeColor="White" CssClass="headerstyle" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:Button ID="btnreportpopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ReportPopUpNew" runat="server" TargetControlID="btnreportpopup"
            Drag="true" BackgroundCssClass="PopupMainBg" PopupControlID="pnlpopupReportUser"
            CancelControlID="btnUserViewExit">
        </asp:ModalPopupExtender>
        <%--<asp:Button ID="btnShowUser_Report" runat="server" Style="display: none" />--%>
        <asp:Panel ID="pnlpopupReportUser" runat="server" CssClass="pnlpopupclass_Report" BackColor="White"
            Style="display: none;">
            <div class="clsDiv1">
             <div style="width: 99% !important;">
                <div class="h3class">
                    User Details
                </div>
                <span style="float: left;">
                    <img src="../App_Themes/images/colomn-bg4.png" alt="" /></span></div>
              <%--  <h3 style="margin-bottom: 10px;">
                    User Details</h3>--%>
                <div class="clsPopUp news-osf" style="width: 99% !important;">
                    <table width="700px" cellspacing="5" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td class="UserDetailHeader">
                                    First Name:
                                </td>                               
                                <td>
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Last Name:
                                </td>
                                <td>
                                    <asp:Label ID="lblLastName" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Department:
                                </td>
                                <td>
                                    <asp:Label ID="lblDepartment" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Position:
                                </td>
                                <td>
                                    <asp:Label ID="lblPosition" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Area Chapter:
                                </td>
                                <td>
                                    <asp:Label ID="lblAreaChapter" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Companion Type:
                                </td>
                                <td>
                                    <asp:Label ID="lblCompanionType" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Profession Year:
                                </td>
                                <td>
                                    <asp:Label ID="lblProfessionYear" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    MI:
                                </td>
                                <td>
                                    <asp:Label ID="lblMI" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Home</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Location:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblHomeLocation" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeStreet1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeStreet2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeCity" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeState" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeCountry" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeZip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomePhone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeEmail" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Ministry</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Title:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Title" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Ministry Classification:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Classification" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Place of Employment:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblMinistry1PlaceofEmployment" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Street1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Stree2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1City" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1State" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Country" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Zip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Phone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Fax:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Fax" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Email" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Ministry (Secondary)</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Title:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Title" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Ministry Classification:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Classification2" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Place of Employment:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblMinistry2PlaceofEmp" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Street1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2street2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2City" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2State" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Country" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Zip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Phone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Fax:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Fax" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Email" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Picture:
                                </td>
                                <td colspan="3">
                                    <%-- <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />&nbsp;&nbsp;&nbsp;--%>
                                    <asp:Image ID="imgUserView" runat="server" Height="20px" ImageUrl="" Width="50px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 10px;">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnUserViewExit" runat="server" CssClass="ButtonOSF" OnClick="btnUserViewExit_Click"
                                        Text="Cancel" Width="75px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 20px">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>


    </div>
</asp:Content>
