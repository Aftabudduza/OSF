<%@ Page Title="OSF::Content" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    AutoEventWireup="true" CodeFile="HomePageControlPanel.aspx.cs" Inherits="Admin_HomePageControlPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .UserDetailHeader
        {
            width: 150px;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" style="margin: 20px 0 0 205px;">
        <h3 class="OFSh3">
            Manage Homepage</h3>
        <div class="news-osf" style="width: 90%;">
            <table cellspacing="0" cellpadding="2" border="0" style="margin-left: 20px;" width="100%">
                <tbody>
                    <tr>
                        <td align="right" style="width: 33%;text-align:center;">
                            Left Column</td>
                        <td style="height: 19px; width: 33%;text-align:center;">
                            Middle Column</td>
                        <td style="height: 19px; width: 33%;text-align:center;">
                            Right Column</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 33%;">
                            <asp:TreeView ID="treeViewLeft" runat="server" EnableClientScript="true" Font-Name="Arial"
                                ForeColor="Black" 
                                PopulateNodesFromClient="true">
                            </asp:TreeView>
                        </td>
                        <td align="left" style="height: 19px; width: 33%;">
                            <asp:TreeView ID="treeViewMidde" runat="server" EnableClientScript="true" Font-Name="Arial"
                                ForeColor="Black" 
                                PopulateNodesFromClient="true">
                            </asp:TreeView>
                        </td>
                        <td align="left" style="height: 19px; width: 33%;">
                            <asp:TreeView ID="treeViewRight" runat="server" EnableClientScript="true" Font-Name="Arial"
                                ForeColor="Black"
                                PopulateNodesFromClient="true">
                            </asp:TreeView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 33%;">
                            &nbsp;
                        </td>
                        <td style="height: 19px; width: 33%;">
                            &nbsp;
                        </td>
                        <td style="height: 19px; width: 33%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 33%;">
                            &nbsp;</td>
                        <td style="height: 19px; width: 33%;text-align:center;" >
                            <asp:Button CssClass="ButtonOSF" ID="btnSubmit" runat="server" Text="Save" onclick="btnSubmit_Click"
                                   />&nbsp;
                                <asp:Button CssClass="ButtonOSF" ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                        </td>
                        <td style="height: 19px; width: 33%;">
                            &nbsp;</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
