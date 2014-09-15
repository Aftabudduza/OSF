<%@ Page Title="OSF::Contents" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    AutoEventWireup="true" CodeFile="NewsorContents.aspx.cs" Inherits="Contents_NewsorContents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
        #element_to_pop_up2
        {
            background-color: #fff;
            border-radius: 15px;
            color: #000;
            display: none;
            padding: 20px;
            min-width: 400px;
            min-height: 180px;
        }
               .b-close
        {
            cursor: pointer;
            position: absolute;
            right: 10px;
            top: 5px;
        }
</style>
    <script type="text/javascript" >
        function EditContentRow(xs1) {
            var xmlhttpNewsCon;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttpNewsCon = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttpNewsCon = new ActiveXObject("Microsoft.XMLHTTP");
            }
            var urlNewsCon = "../Admin/AdminContent.aspx?Method=EditContent&ID=" + xs1;
            window.location.href = urlNewsCon;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:TextBoxWatermarkExtender TextBtoxWatermarkExtender ID="TBWE2" runat="server"    TargetControlID="txtFromDate"    WatermarkText="MM/dd/yyyy"    WatermarkCssClass="watermarked" />--%>
    <div class="container">
        <asp:ToolkitScriptManager ID="ScriptManager1" CombineScripts="true" runat="server">
        </asp:ToolkitScriptManager>
        <div class="Left-85P-container">
            <h3>
                <asp:Label ID="lblRcentTitle" runat="server" Text="Label"></asp:Label></h3>
            <div id="dynamicDiv" runat="server">
            </div>
      
        </div>
        <div class="right-15p-sidebar">
            <div class="colomn">
                <img src="images/community-news.jpg" alt="Community-News">
                <a href="#" class="community-news-link">Community News Archive</a>
            </div>
            <div class="colomn">
                <h3>
                    Search</h3>
                <div class="alerts">
                    <table align="center">
                        <tbody>
                            <tr>
                                <td class="StandardTextBold">
                                    From:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="OSFDateWiseSearchTextBox" ID="txtFromDate" runat="server"></asp:TextBox>
                                    <img style="margin-left: 5px; margin-top: 0;" id="FromDateC" src="../App_Themes/images/calender.jpg"
                                        width="25px" height="30px" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="FromDateC"
                                        TargetControlID="txtFromDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="color: Red; display: none;" id="Rangevalidator2">Invalid Date.<br>
                                        Format like ##/##/##</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="StandardTextBold">
                                    To:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="OSFDateWiseSearchTextBox" ID="txtToDate" runat="server"></asp:TextBox>
                                    <img style="margin-left: 5px; margin-top: 0;" id="FromDateC2" src="../App_Themes/images/calender.jpg"
                                        width="25px" height="30px" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="FromDateC2"
                                        TargetControlID="txtToDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="color: Red; display: none;" id="Rangevalidator1">Invalid Date.<br>
                                        Format like ##/##/##</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="StandardTextBold">
                                    Keyword:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="OSFDateWiseSearchTextBox" ID="txtKeyWord" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
          <div id="element_to_pop_up2" class="right-15p-sidebar">
            </div>
</asp:Content>
