<%@ Page Title="OSF::Content" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="AdminContent.aspx.cs" Inherits="Admin_AdminContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
    .UserDetailHeader
    {
       width:150px;
       float:left;    
    }
 
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" style="margin: 20px 0 0 205px;">
        <h3 class="OFSh3">
            Add Content</h3>
        <div class="news-osf" style="width:90%;">
       
                <asp:Label ID="lblUserNamePermission" runat="server" CssClass="form_header" Text="Content Administration"></asp:Label>
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
                      <span style="color: #FF0000">*</span>
                       <asp:Label ID="Label1" runat="server" Text="Category:"></asp:Label>
                            </td>
                            <td width="750" style="height: 19px">

                                <asp:DropDownList CssClass="OSFTextBox" ID="ddlCategoryList" Width="207" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="UserDetailHeader" align="right">
                                <span id="Span6" style="color: #FF0000">*</span> 
  
                                 <asp:Label ID="Label2" runat="server" Text="File Upload:"></asp:Label>
                                
                            </td>
                            <td>
                                <asp:FileUpload ID="uplProduct" runat="server" Width="350px" />
                                <asp:Label ID="lblURLofFile" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                                 <td class="UserDetailHeader" align="right">
                                <span id="Span2" style="color: #FF0000">*</span>
                                <asp:Label ID="lblAuthorTitle" runat="server" Text="Author:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox CssClass="OSFTextBox" ID="txtAuthor" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                <td class="UserDetailHeader" align="right">
                                <span id="Span3" style="color: #FF0000">*</span>
                                <asp:Label ID="lblDateTtile" runat="server" Text="Date:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox CssClass="OSFTextBox" ID="txtDate" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck"
                                    Type="Date" ControlToValidate="txtDate" ErrorMessage="Date format MM/dd/yyyy" />
                            </td>
                        </tr>
                        <tr>
                               <td class="UserDetailHeader" align="right">
                                <span id="Span4" style="color: #FF0000">*</span>
                                <asp:Label ID="lblTitleTitle" runat="server" Text="Title:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox CssClass="OSFTextBox" ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr height="21">
                                <td class="UserDetailHeader" align="right">
                                    &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr height="21">
                                <td class="UserDetailHeader" align="right" valign="top">
                                <span id="Span5" style="color: #FF0000">*</span>
                                <asp:Label ID="lblContentTitle" runat="server" Text="Content:"></asp:Label>
                            </td>
                            <td>
                             <asp:TextBox Width="93%" Height="200px" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
 
                        <tr>
                            <td colspan="2">

                                <!-- ======= This page contains code that is Copyright RichTextBox.com 2002-2003 ====== -->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;<asp:Button CssClass="ButtonOSF" ID="btnSubmit" runat="server" Text="Post"
                                    OnClick="btnSubmit_Click" />
                                &nbsp;<asp:Button CssClass="ButtonOSF" ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;</td>
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
