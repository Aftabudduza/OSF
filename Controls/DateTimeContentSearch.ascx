<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTimeContentSearch.ascx.cs" Inherits="Controls_DateTimeContentSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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