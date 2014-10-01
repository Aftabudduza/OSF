<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="AdminContactLists.aspx.cs" Inherits="Admin_AdminContactLists" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function TestT() {
        alert(4);
    }
</script>
<style type="text/css" >
.gridPaging
{
 float:left;
 margin-top:10px;   
 }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="mid-container" style="margin: 20px 0 10px 205px;">
                <h3 class="OFSh3">
                    User Search</h3>
                <div class="news-osf">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td width="17">
                                                    &nbsp;
                                                </td>
                                                <td width="125px" style="background: Images\TitleBar\TitleLeftMiddleSpacer.gif">
                                                </td>
                                                <td width="17">
                                                    &nbsp;
                                                </td>
                                                <td width="" style="background: Images/TitleBar/TitleMiddleSpacer.gif">
                                                    <span class="TitleText">&nbsp;</span>
                                                </td>
                                                <td width="1">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    First Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSFirstName" MaxLength="20" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Last Name:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtSLastName" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Location:
                                                </td>
                                                <td>
                                                    &nbsp;<asp:DropDownList CssClass="OSFPopupTextBox" Height="20px" ID="ddlSLocation"
                                                        Width="205" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Area Chapter:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSChapter" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Home City:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSHomeCity" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Home State:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtsHomeState" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Ministry Title:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsMinistryTitle" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Place of Employment:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtSMinistryLocation" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Ministry City:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSMinistryCity" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Ministry State:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtSMinistryState" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsSSister" Text="Sister" runat="server" />
                                                    <asp:CheckBox ID="chkIsSStaff" Text="Stuff" runat="server" />
                                                    <asp:CheckBox ID="chkIsSCompanion" Text="Companion" runat="server" />
                                                    <asp:CheckBox ID="chkIsSCommittee" Text="Committee" runat="server" />
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Companion Type:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtSCompanionType" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Department:
                                                </td>
                                                <td>
                                                    &nbsp;<asp:DropDownList CssClass="OSFPopupTextBox" Height="20px" ID="ddlSDepartment"
                                                        Width="205" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Position:
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;<asp:DropDownList CssClass="OSFPopupTextBox" Height="20px" ID="ddlSPost" Width="205"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Records:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRecordCount" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtRecordCount"
                                                        ValidationExpression="^\d+$" Display="Static" ErrorMessage="Only numeric values can be entered."
                                                        EnableClientScript="true" runat="server" />
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    Profession Year:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtsProfessionalYear" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="UserDetailHeader">
                                                    Contact Type:
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="OSFPopupTextBox" Height="20px" ID="ddlContactType" Width="205"
                                                        runat="server" OnSelectedIndexChanged="ddlContactType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="UserDetailHeader">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Button CssClass="ButtonOSF" ID="btnSearch" Style="margin: 15px 20px 0 0;" runat="server"
                                                        OnClick="btnSearch_Click" Text="Search user" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="mid-container" style="margin: 0 0 0 205px;">
                <asp:GridView ID="gvUser" runat="Server" Width="100%" Style="margin: 15px 0;" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CaptionAlign="Left"
                    ForeColor="#D24D8A" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                    PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
                    GridLines="None" PageSize="15" SelectedIndex="0" OnPageIndexChanging="gvUser_PageIndexChanging"
                    OnRowCreated="gvUser_RowCreated">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#D24D8A" Font-Size="Small" Font-Bold="false"
                        CssClass="grid" />
                    <Columns>
                        <asp:TemplateField HeaderText="LastName" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("LastName")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="250px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FirstName" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("FirstName")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="250px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HomePhone" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("HomePhone")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HomeEmail" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("HomeEmail")%>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:CheckBox ID="chkSelect" Checked='<%# Convert.ToBoolean(Eval("IsSelected"))%>'
                                        runat="server"></asp:CheckBox></center>
                                <asp:HiddenField ID="uID" Value='<%# Eval("UserID1") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="uID" Value='<%# Eval("UserID1") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>--%>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                        NextPageText="Next" PreviousPageText="Previous" />
                    <PagerStyle CssClass="form-actions tag gridPaging" ForeColor="#000" HorizontalAlign="Center" />
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
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Button CssClass="ButtonOSF" ID="btnSave" Style="margin: 15px 20px 0 0;" runat="server"
                                    Text="Save" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
