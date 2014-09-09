<%@ Page Title="OSF::Bulletin Board" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="adminBulletinBoard.aspx.cs" Inherits="Admin_adminBulletinBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="mid-container" style="margin: 20px 0 0 205px; width:600px;padding-left:150px;">
        <h3 class="OFSh3">
            Add Content</h3>
        <div class="news-osf">

        <table cellspacing="0" cellpadding="2" border="0">
            <tbody>
                <tr>
                    <td style="height: 19px; width: 150px"> <span  id="Span2" style="color: #FF0000">*</span>
                        <span class="standardText" id="CategoryTitle">Category:</span>
                    </td>
                    <td width="600px" style="height: 19px">                 
                        <asp:DropDownList Width="200" ID="ddlCategoryList" runat="server">
                            <asp:ListItem Value="-1">option</asp:ListItem>
                            <asp:ListItem Value="1">Ministry Listings</asp:ListItem>
                            <asp:ListItem Value="2">Deceased Sisters List</asp:ListItem>
                            <asp:ListItem Value="3">Sisters Birthday List</asp:ListItem>
                            <asp:ListItem Value="1">Other</asp:ListItem>
                            <asp:ListItem Value="2">Leadership Letters</asp:ListItem>
                            <asp:ListItem Value="3">Forms and Info</asp:ListItem>
                            <asp:ListItem Value="1">Media</asp:ListItem>
                            <asp:ListItem Value="2">Missioning Update</asp:ListItem>
                            <asp:ListItem Value="3">Retreat Options</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td> <span  id="Span1" style="color: #FF0000">*</span>
                        <span class="standardText" id="UploadFileTitle">File Upload:</span>
                    </td>
                    <td>
                        <asp:FileUpload ID="uplProduct" runat="server" Width="350px" />
                    </td>
                </tr>
                <tr>
                    <td> <span  id="Span3" style="color: #FF0000">*</span>
                        <asp:Label ID="lblAuthorTitle" runat="server" Text="Posting Author"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td> <span  id="Span4" style="color: #FF0000">*</span>
                        <asp:Label ID="lblDateTtile" runat="server" Text="Article Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td> <span  id="Span5" style="color: #FF0000">*</span>
                        <asp:Label ID="lblTitleTitle" runat="server" Text="Title"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr height="21">
                    <td width="75" valign="top"> <span  id="Span6" style="color: #FF0000">*</span>
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
                                        <table width="600px" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td height="100%">
                                                        <asp:TextBox Width="90%" Height="300px" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                        &nbsp;<asp:Button CssClass="ButtonOSF"  ID="btnSubmit" runat="server" Text="Post" OnClick="btnSubmit_Click" />
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
