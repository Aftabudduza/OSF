<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="ManageCategory.aspx.cs" Inherits="Admin_ManageCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         <div class="mid-container" style="margin: 20px 0 0 0;width:95%;">
        <h3 class="OFSh3">
            Manage Category</h3>
        <div class="news-osf">
        <asp:ToolkitScriptManager ID="ScriptManager1" CombineScripts="true" runat="server">
        </asp:ToolkitScriptManager>
        <fieldset>
            <div style="margin-left: 10px;">
             
                <asp:GridView ID="gvCategory" runat="Server" Width="100%" Style="margin: 15px 0;"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4"
                    CaptionAlign="Left" ForeColor="#333333" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                    PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
                    GridLines="None" PageSize="4" SelectedIndex="0" OnPageIndexChanging="gvCategory_PageIndexChanging">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Font-Bold="false"
                        CssClass="grid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("Description")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sort Order" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("SortOrder")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="80px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title of Title" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("TitleTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Author Title" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("AuthorTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DateTitle" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("DateTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Content Title" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("ContentTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Title" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ShowTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Author" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ShowAuthor")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ShowDate")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Content" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ShowContent")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Title" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("DefaultTitle")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Author" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("DefaultAuthor")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Date" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("DefaultDate" ,"{0:MMM dd yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Content" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("DefaultContent")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Quick Launch" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("IsQuickLaunch")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Items Per Page" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ItemsPerPage")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdId" Value='<%# Eval("CID") %>' runat="server" />
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
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="headerstyle" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <table class="FormTable1" width="80%" cellspacing="0" cellpadding="5">
                    <tr class="w">
                        <td style="font-size: 18px; width: 20%;" id="Td7">
                            <span id="Span8" style="color: #FF0000">*</span> Category
                        </td>
                        <td style="font-size: 18px; width: 20%;" id="Td8">
                            &nbsp;
                        </td>
                        <td style="font-size: 18px; width: 20%;" id="Td9">
                            &nbsp;
                        </td>
                        <td style="font-size: 18px; width: 20%;" id="Td10">
                            &nbsp;
                        </td>
                        <td style="font-size: 18px; width: 20%;" id="Td11">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <span id="Span7" style="color: #FF0000">*</span> Category Type :
                        </td>
                        <td colspan="1">
                            <asp:DropDownList CssClass="OSFTextBox" ID="ddlSectionType" Width="100%" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSectionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <span id="Span6" style="color: #FF0000">*</span> Category Description :
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtCategoryName" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <%--<asp:CheckBox ID="chkIsLeaf" runat="server" Text="Is Leaf" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <span id="Span5" style="color: #FF0000">*</span>Sort Order
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtOrderSeq" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            &nbsp;
                            <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="DataTypeCheck"
                                Type="Integer" ControlToValidate="txtOrderSeq" ErrorMessage="Value must be an Integer" />
                        </td>
                    </tr>
                </table>
                <table class="FormTable1" width="80%" cellspacing="0" cellpadding="5">
                    <tr>
                        <td colspan="1" style="width: 15%">
                            &nbsp;
                        </td>
                        <td colspan="1" style="width: 25%">
                            &nbsp;
                        </td>
                        <td colspan="1" style="width: 20%">
                            &nbsp;
                        </td>
                        <td colspan="1" style="width: 15%">
                            &nbsp;
                        </td>
                        <td colspan="1" style="width: 25%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="w">
                        <td colspan="5" style="font-size: 18px;" id="Td1">
                            <asp:Label ID="lblEditCategory" runat="server" Text="Category Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <span id="Span2" style="color: #FF0000">*</span>
                            <asp:Label ID="lblTitleTitle" runat="server" Text=" Title Title :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtTitleTitle" Width="100%" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <span id="Span1" style="color: #FF0000">*</span>
                            <asp:Label ID="lblAuthorTitle" runat="server" Text="Author Title :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtAuthorTitle" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <span id="Span3" style="color: #FF0000">*</span>
                            <asp:Label ID="lblDateTitle" runat="server" Text="Date Title :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtDateTitle" Width="100%" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <span id="Span4" style="color: #FF0000">*</span>
                            <asp:Label ID="lblContentTitle" runat="server" Text="Content Title :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtContentTitle" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="CheckBoxShowTitle" runat="server" Text="ShowTitle" />
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="CheckBoxShowAuthor" runat="server" Text="ShowAuthor" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="CheckBoxShowDate" runat="server" Text="ShowDate" />
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="CheckBoxShowContent" runat="server" Text="ShowContent" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <asp:Label ID="lblDefaultTitle" runat="server" Text="Default Title :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtDefaultTitle" Width="100%" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <asp:Label ID="lblDefaultAuthor" runat="server" Text="Default Author :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtDefaultAuthor" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <asp:Label ID="lblDefaultDate" runat="server" Text="Default Date :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <%--                                <asp:TextBox CssClass="OSFTextBox" ID="txtDefaultDate" Width="100%" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck"
                                    Type="Date" ControlToValidate="txtDefaultDate" ErrorMessage="Date for mat MM/dd/yyyy" />--%>
                            <asp:TextBox runat="server" ID="txtDefaultDate" CssClass="OSFTextBox" Text=""></asp:TextBox><img
                                style="margin-left: 5px; margin-top: 0;" id="test" src="../App_Themes/images/calender.jpg"
                                width="25px" height="30px" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="test"
                                TargetControlID="txtDefaultDate">
                            </asp:CalendarExtender>
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <asp:Label ID="lblDefaultContent" runat="server" Text="Default Content :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtDefaultContent" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="CheckBoxIsQuickLaunch" runat="server" Text="IsQuickLaunch" />
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            <asp:Label ID="lblItemsPerPage" runat="server" Text="Items Per Page :"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:TextBox CssClass="OSFTextBox" ID="txtItemsPerPage" Width="100%" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck"
                                Type="Integer" ControlToValidate="txtItemsPerPage" ErrorMessage="Value must be an Integer" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                        </td>
                        <td colspan="1">
                            <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </td>
                        <td colspan="2">
                            &nbsp;
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        </div>
    </div>
</asp:Content>
