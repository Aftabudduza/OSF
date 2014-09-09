<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="BasicData.aspx.cs" Inherits="Admin_BasicData" %>

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
            Basic Data</h3>
        <div class="news-osf" style="width: 90%;">
            <asp:Label ID="lblUserNamePermission" runat="server" CssClass="form_header" Text="New Basic Data"></asp:Label>
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
                            <asp:Label ID="Label1" runat="server" Text="Basic Data Type:"></asp:Label>
                        </td>
                        <td width="750" style="height: 19px">
                            <asp:DropDownList CssClass="OSFTextBox" ID="ddlSectionType" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlSectionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="UserDetailHeader" align="right">
                            <span id="Span6" style="color: #FF0000">*</span>
                            <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="OSFTextBox" ID="txtBasicDataName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="UserDetailHeader" align="right">
                            <span id="Span2" style="color: #FF0000">*</span>
                            <asp:Label ID="lblAuthorTitle" runat="server" Text="User Defined ID:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="OSFTextBox" ID="txtUserDiefinedID" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck"
                                Type="Integer" ControlToValidate="txtUserDiefinedID" ErrorMessage="Value must be an Integer" />
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
                                GridLines="None" PageSize="4" SelectedIndex="0" OnPageIndexChanging="gvCategory_PageIndexChanging">
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
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="headerstyle" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
