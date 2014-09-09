<%@ Page Title="OSF::Chapter Directive" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="AdminChapterDirectivesTopic.aspx.cs" Inherits="Admin_AdminChapterDirectivesTopic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ButtonOSF
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
          <div class="mid-container" style="margin: 20px 0 0 205px; width:600px;padding-left:150px;">
        <h3 class="OFSh3">
            Add Content</h3>
        <div class="news-osf">
        <table cellspacing="0" cellpadding="2" border="0">
            <tbody>
                <tr>
                    <td style="height: 19px; width: 220px">
                        <span class="standardText" id="CategoryTitle">Level 1 Category:</span>
                    </td>
                    <td width="750" style="height: 19px">
                        <asp:DropDownList Width="200" ID="ddlCategoryListL1" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCategoryListL1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; width: 150px">
                        <span class="standardText" id="Span1">Level 2 Category:</span>
                    </td>
                    <td width="750" style="height: 19px">
                        <asp:DropDownList Width="150" ID="ddlCategoryListL2" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCategoryListL2_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button CssClass="ButtonOSF"   ID="btnLvl2Add" runat="server" Text="Add" OnClick="btnLvl2Add_Click"
                            AutoPostBack="true" />
                        &nbsp;
                        <asp:TextBox ID="txtLvl2" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:Button CssClass="ButtonOSF" ID="btnLvl2Save" runat="server" Text="Save" OnClick="btnLvl2Save_Click"
                            AutoPostBack="true" Width="40px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; width: 150px">
                        <span class="standardText" id="Span2">Level 3 Category:</span>
                    </td>
                    <td width="750" style="height: 19px">
                        <asp:DropDownList Width="150" ID="ddlCategoryListL3" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button CssClass="ButtonOSF" ID="btnLvl3Add" runat="server" Text="Add" OnClick="btnLvl3Add_Click" />
                        &nbsp;
                        <asp:TextBox ID="txtLvl3" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:Button CssClass="ButtonOSF"  ID="btnlvl3Save" runat="server" Text="Save" OnClick="btnlvl3Save_Click"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="standardText" id="UploadFileTitle">File Upload:</span>
                    </td>
                    <td>
                        <asp:FileUpload ID="uplProduct" runat="server" Width="350px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAuthorTitle" runat="server" Text="Posting Author"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtAuthor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDateTtile" runat="server" Text="Article Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitleTitle" runat="server" Text="Title"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr height="21">
                    <td width="75" valign="top">
                        <asp:Label ID="lblContentTitle" runat="server" Text="Content"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td height="100%">
                                        <table width="600px" height="100%" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td height="100%">
                                                        <asp:TextBox Width="90%" Height="200px" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <!-- ======= This page contains code that is Copyright RichTextBox.com 2002-2003 ====== -->
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;<asp:Button CssClass="ButtonOSF" ID="btnSubmit" runat="server" Text="Post" OnClick="btnSubmit_Click" />
                        &nbsp;<asp:Button CssClass="ButtonOSF"  ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <span class="goodStatus" id="SuccessLabel"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </div>
</asp:Content>
