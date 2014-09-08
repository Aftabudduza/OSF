<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="AdminContent.aspx.cs" Inherits="Admin_AdminContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="mid-container" style="margin: 20px 0 0 205px; width:600px;padding-left:150px;">
        <h3 class="OFSh3">
            Add Content</h3>
        <div class="news-osf">
        <table>
            <tr style="height: 10px">
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <span class="standardTextBold" id="lblHeader">Content Administration</span>
        <table>
            <tr style="height: 20px">
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="2" border="0" style="margin-left:20px;">
            <tbody>
                <tr>
                    <td style="height: 19px; width: 100px">
                         <span  id="Span1" style="color: #FF0000">*</span>
                        <span class="standardText" id="CategoryTitle">Category:</span>
                    </td>
                    <td width="750" style="height: 19px">
                        <%--                    <select runat="server" class="..standardText" id="CategoryList" language="javascript"
                        onchange="__doPostBack('CategoryList','')" name="CategoryList">
                        <option value="-1"></option>
                        <option value="33">Help/Faqs</option>
                        <option value="1">Calendar Events</option>
                        <option value="2" selected="selected">News.</option>
                        <option value="3">Sisters Illnesses</option>
                        <option value="4">Death Notices</option>
                        <option value="29">Check This Out!</option>
                        <option value="30">Community News</option>
                        <option value="71">Help</option>
                        <option value="72">Links</option>
                        <option value="75">Hot Of The Presses</option>
                        <option value="78">Prayers</option>
                    </select>&nbsp;--%>
                        <asp:DropDownList CssClass="OSFTextBox" ID="ddlCategoryList" Width="220" AutoPostBack="true"
                            runat="server" onselectedindexchanged="ddlCategoryList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td> <span  id="Span6" style="color: #FF0000">*</span>
                        <span class="standardText" id="UploadFileTitle">File Upload:</span>
                    </td>
                    <td>
                        <asp:FileUpload ID="uplProduct" runat="server" Width="350px" />
                    </td>
                </tr>
                <tr>
                    <td>
                      <span  id="Span2" style="color: #FF0000">*</span>
                        <asp:Label ID="lblAuthorTitle" runat="server" Text="Author"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtAuthor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                      <span  id="Span3" style="color: #FF0000">*</span>
                        <asp:Label ID="lblDateTtile" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtDate" runat="server"></asp:TextBox>
                               <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck"
                                    Type="Date" ControlToValidate="txtDate" ErrorMessage="Date format MM/dd/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td>
                      <span  id="Span4" style="color: #FF0000">*</span>
                        <asp:Label ID="lblTitleTitle" runat="server" Text="Title"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr height="21">
                    <td width="75" valign="top">
                      <span  id="Span5" style="color: #FF0000">*</span>
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
                    <td align="left" colspan="2">
                        &nbsp;<asp:Button CssClass="ButtonOSF" ID="btnSubmit" runat="server" Text="Post" OnClick="btnSubmit_Click" />
                        &nbsp;<asp:Button CssClass="ButtonOSF" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
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
