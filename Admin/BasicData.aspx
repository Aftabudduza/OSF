﻿<%@ Page Title="OSF::Basic Data" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    AutoEventWireup="true" CodeFile="BasicData.aspx.cs" Inherits="Admin_BasicData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="mid-container" style="margin: 20px 0 0 205px;">
                <h3 class="OFSh3">
                    Basic Data</h3>
                <div class="news-osf" style="width: 90%;">
                    <asp:Label ID="lblUserNamePermission" runat="server" CssClass="form_header" Text=""></asp:Label>
                    <table>
                        <tr style="height: 20px">
                            <td>
                                &nbsp
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="2" border="0" style="margin-left: 20px;">
                        <tbody>
                            <tr>
                                <td class="UserDetailHeader" align="right">
                                    <asp:Label ID="Label1" runat="server" Text="* Basic Data Type:"></asp:Label>
                                </td>
                                <td width="750" style="height: 19px">
                                    <asp:DropDownList CssClass="OSFTextBox" Height="29px" Width="205px" ID="ddlSectionType"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSectionType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader" align="right">
                                    <asp:Label ID="lblName" runat="server" Text="* Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="OSFTextBox" ID="txtBasicDataName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="UserDetailHeader">
                                    <asp:Label ID="lblHomePage" runat="server" Text="* Home Page:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHomePage" runat="server" CssClass="OSFTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="UserDetailHeader">
                                    <asp:Label ID="lblEmailPage" runat="server" Text="* Email Page:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailPage" runat="server" CssClass="OSFTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" Text="Save" AutoPostBack="true"
                                        OnClick="btnSave_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvCategory" runat="Server" Width="90%" Style="margin: 15px 15px;"
                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4"
                                        CaptionAlign="Left" ForeColor="#333333" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
                                        GridLines="None" PageSize="12" SelectedIndex="0" OnPageIndexChanging="gvCategory_PageIndexChanging">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Font-Bold="false"
                                            CssClass="grid" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("Name")%>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Data Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Type")%>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdId" Value='<%# Eval("BasicDataID") %>' runat="server" />
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                                                    <%--          <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick="return show_confirm();"
                                                            OnClick="lbtnDelete_Click">Delete</asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
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
                                                <div>
                                                </div>
                                            </div>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#D24D8A" Font-Bold="True" ForeColor="White" CssClass="headerstyle" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
