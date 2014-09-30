<%@ Page Title="OSF:User" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Admin_User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.unobtrusive.min.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
    <style type="text/css">
        .input-validation-error
        {
            border: 1px solid #ff0000;
            background-color: #ffeeee;
        }
        .field-validation-error
        {
            color: #ff0000;
        }
        .btnBG
        {
            margin-left: 150px;
            float: left;
            display: inline;
        }
        .ctl00_ContentPlaceHolder1_ModalPopupExtender1_backgroundElement
        {
            background: #000 !important;
            opacity: 0.5;
        }
        .style1
        {
            height: 42px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=LinksTreeView] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        })

        $(function () {
            $("[id*=treeViewCatConPerm] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        })
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
                                            <asp:Button CssClass="ButtonOSF" Style="margin: 15px 20px 0 0;" ID="btnAddUser" runat="server"
                                                Text="New User" OnClick="btnAddUser_Click" />
                                            <asp:Button CssClass="ButtonOSF" ID="btnSearch" Style="margin: 15px 20px 0 0; height: 26px;"
                                                runat="server" OnClick="btnSearch_Click" Text="Search" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            &nbsp;
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
                    <HeaderStyle Font-Size="12px" Width="150px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FirstName" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("FirstName")%>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="150px" HorizontalAlign="Left" />
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
                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdId" Value='<%# Eval("UserID") %>' runat="server" />
                        <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Permission" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdPermission" Value='<%# Eval("UserID") %>' runat="server" />
                        <asp:LinkButton ID="lbtnPermission" runat="server" OnClick="lbtnPermission_Click">Edit</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" OnClick="lbtnView_Click">View</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12px" Width="100px" HorizontalAlign="Center" />
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
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="PopupMainBg"
            runat="server" TargetControlID="btnShowPopup" Drag="true" PopupControlID="pnlpopup"
            CancelControlID="btnCancel">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" CssClass="pnlpopupclass" BackColor="White"
            Style="display: none;">
            <div class="clsDiv1">
                <h3>
                    User Details</h3>
                <div class="clsPopUp">
                    <table width="700px" cellspacing="5" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td class="ContentBlock">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    First Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFirstName" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Last Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLastName" MaxLength="30" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Department:
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="OSFPopupTextBox" ID="ddlDepartment" Height="20px" Width="205"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Position:
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList CssClass="OSFPopupTextBox" ID="ddlPosition" Height="20px" Width="205"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Area Chapter:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtChapter" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtChapter"
                                                        ValidationExpression="^\d+$" Display="Static" ErrorMessage="Only numeric values can be entered."
                                                        EnableClientScript="true" runat="server" />
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Companion Type:
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCompanionType" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Profession Year:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProfessionalYear" CssClass="OSFPopupTextBox" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    MI:
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtMI" MaxLength="1" runat="server" CssClass="OSFPopupTextBox" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <span class="popuptdspan"><b>Home</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Location:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList CssClass="OSFPopupTextBox" ID="ddlLocation" Height="20px" Width="205"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Street 1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeStreet1" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Street 2:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeStreet2" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    City:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeCity" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    State:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeState" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Country:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeCountry" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Zip:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeZip" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Phone:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomePhone" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Email:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHomeEmail" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4"
                                                        runat="server" ErrorMessage="Invalid Home Email" ControlToValidate="txtHomeEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <span class="popuptdspan"><b>Ministry</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Title:
                                                </td>
                                                <td>
                                                    <input type="text" class="OSFPopupTextBox" tabindex="18" id="MinistryTitle" name="" />
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Ministry Classification:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryClassification" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Place of Employment:
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtMinistryLocation" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Street 1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryStreet1" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Street 2:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryStreet2" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    City:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryCity" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    State:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryState" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Country:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryCountry" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Zip:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryZip" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Phone:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryPhone" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Fax:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryFax" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Email:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistryEmail" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <span class="popuptdspan"><b>Ministry (Secondary)</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Title:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Title" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Ministry Classification:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Classification" MaxLength="50" CssClass="OSFPopupTextBox"
                                                        runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Place of Employment:
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtMinistry2Location" MaxLength="50" CssClass="OSFPopupTextBox"
                                                        runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Street 1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Street1" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Street 2:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Street2" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    City:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2City" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    State:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2State" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Country:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Country" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Zip:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Zip" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Phone:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Phone" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td class="UserDetailHeader">
                                                    Fax:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Fax" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Email:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinistry2Email" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                                        Text=""></asp:TextBox>
                                                </td>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="UserDetailHeader">
                                                    Picture:
                                                </td>
                                                <td colspan="4">
                                                    <asp:FileUpload ID="uplProduct" runat="server" Width="350px" />&nbsp;&nbsp;&nbsp;
                                                    <%--<asp:Image ID="">--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 10px;" align="center">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" OnClick="btnSave_Click"
                                                        Text="Save" Width="55px" />
                                                    &nbsp;
                                                    <asp:Button CssClass="ButtonOSF" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                        Width="75px" Text="Cancel" />
                                                </td>
                                                <%-- <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                    </td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <span class="ErrorStatus" id="PhotoErrorLabel"></span><span class="goodStatus" id="PhotoSuccessLabel">
                                                    </span>
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
        </asp:Panel>
        <asp:Button ID="btnShowPopupPermission" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="PopupMainBg"
            TargetControlID="btnShowPopupPermission" Drag="true" PopupControlID="pnlpopupPermission"
            CancelControlID="btnCancel">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopupPermission" CssClass="pnlpopupclass" runat="server" BackColor="White"
            Style="display: block;">
            <div class="clsDiv1">
                <asp:Label ID="lblUserNamePermission" runat="server" CssClass="form_header" Text=""></asp:Label>
                <div class="clsPopUp">
                    <table width="700px" cellspacing="5" cellpadding="0" border="0">
                        <tbody>
                            <tr class="bg">
                                <td class="ContentBlock">
                                    <table>
                                        <tbody>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" class="form_label">
                                                    User Name:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtUserNamePermission" runat="server" CssClass="OSFPopupTextBox"
                                                        Text="" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="right" class="form_label">
                                                    Can Log In:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkCanLogin" runat="server" Text=" " Visible="true" />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Hidden:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsHidden" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Delete:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsDelete" runat="server" Text=" " />
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUserNamePermission"
                                                        Display="Static" EnableClientScript="true" ErrorMessage="Only alpha numeric values canbe entered."
                                                        ValidationExpression="^[a-zA-Z0-9_-]{1,16}$" />
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td colspan="3">
                                                    <asp:CheckBox ID="chkResetPassword" runat="server" Text="Reset Password" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    <b>Categorization</b>
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" class="form_label">
                                                    Sister:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsSister" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    staff:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsStuff" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Companion:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsCompanion" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Committee Member:
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsCommitteMember" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    <b>Global Permissions</b>
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="form_label">
                                                    Global Admin:
                                                </td>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkIsGlobalAdmin" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Global Directory Admin:
                                                </td>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkIsGlobalDirectoryAdmin" runat="server" Text=" " />
                                                </td>
                                                <td align="right" class="form_label">
                                                    Global Content Admin:
                                                </td>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkIsGlobalContentAdmin" runat="server" Text=" " />
                                                </td>
                                                <td class="style1">
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="style1">
                                                    <%--  Global Forum Admin:--%>
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" class="form_label">
                                                    <%--           Chapter Directives Admin:--%>
                                                </td>
                                                <td colspan="3">
                                                    <%--<asp:CheckBox ID="isChapterDirectiveAdmin" runat="server" Text=" " />--%>
                                                </td>
                                                <td align="right" class="form_label">
                                                    <%--Companion Admin:--%>
                                                </td>
                                                <td>
                                                    <%--<asp:CheckBox ID="chkIsCompanionAdmin" runat="server" Text=" " />--%>
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    <%--<b>Area Chapter Permissions</b>--%>
                                                    <b>Section Permissions</b>
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    <asp:CheckBoxList ID="chkHeaderList" runat="server" RepeatDirection="Horizontal"
                                                        RepeatColumns="7">
                                                    </asp:CheckBoxList>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <%--<tr class="bg">
                                                    <td class="UserDetailHeader" align="right">
                                                        AC Directory Admin:
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsACDirectoryAdmin" runat="server" Text=" " />
                                                    </td>
                                                    <td class="UserDetailHeader" align="right">
                                                        AC Content Admin:
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsACContentAdmin" runat="server" Text=" " />
                                                    </td>
                                                    <td class="UserDetailHeader" align="right">
                                                        AC Forum Admin:
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkISACForumAdmin" runat="server" Text=" " />
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                </tr>--%>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" colspan="4" style="vertical-align: top; text-align: left;">
                                                    <b>Category Content Admin Permissions</b>&nbsp;
                                                </td>
                                                <td align="left" colspan="5">
                                                    <b>Category Content View Permissions</b>&nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="right" colspan="1" style="vertical-align: top; text-align: left;">
                                                &nbsp;
                                                </td>
                                                <td align="right" colspan="4" style="vertical-align: top; text-align: left;">
                                                    &nbsp;
                                                    <asp:TreeView ID="treeViewCatConPerm" runat="server" EnableClientScript="true" Font-Name="Arial"
                                                        ForeColor="Black" PopulateNodesFromClient="true">
                                                    </asp:TreeView>
                                                </td>
                                                <td align="left" colspan="4">
                                                    <asp:TreeView ID="LinksTreeView" runat="server" EnableClientScript="true" Font-Name="Arial"
                                                        ForeColor="Black" OnTreeNodeCheckChanged="LinksTreeView_TreeNodeCheckChanged"
                                                        PopulateNodesFromClient="true">
                                                    </asp:TreeView>
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="9">
                                                    <asp:Button CssClass="ButtonOSF" ID="btnAddPermission" runat="server" OnClick="btnAddPermission_Click"
                                                        Text="Save" Width="55px" />
                                                    &nbsp;
                                                    <asp:Button CssClass="ButtonOSF" ID="btnPermissionCancel" runat="server" OnClick="btnPermissionCancel_Click"
                                                        Text="Cancel" Width="75px" />
                                                </td>
                                            </tr>
                                            <tr class="bg">
                                                <td align="center" colspan="10">
                                                    &nbsp;
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
        </asp:Panel>
        <asp:Button ID="btnShowUser" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowUser"
            Drag="true" BackgroundCssClass="PopupMainBg" PopupControlID="pnlpopupViewUser"
            CancelControlID="btnCancel">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopupViewUser" runat="server" CssClass="pnlpopupclass" BackColor="White"
            Style="display: none;">
            <div class="clsDiv1">
                <h3 style="margin-bottom: 10px;">
                    User Details</h3>
                <div class="clsPopUp">
                    <table width="700px" cellspacing="5" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td class="UserDetailHeader">
                                    First Name:
                                </td>
                                <td>
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Last Name:
                                </td>
                                <td>
                                    <asp:Label ID="lblLastName" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Department:
                                </td>
                                <td>
                                    <asp:Label ID="lblDepartment" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Position:
                                </td>
                                <td>
                                    <asp:Label ID="lblPosition" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Area Chapter:
                                </td>
                                <td>
                                    <asp:Label ID="lblAreaChapter" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Companion Type:
                                </td>
                                <td>
                                    <asp:Label ID="lblCompanionType" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Profession Year:
                                </td>
                                <td>
                                    <asp:Label ID="lblProfessionYear" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    MI:
                                </td>
                                <td>
                                    <asp:Label ID="lblMI" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Home</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Location:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblHomeLocation" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeStreet1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeStreet2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeCity" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeState" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeCountry" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeZip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomePhone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblHomeEmail" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Ministry</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Title:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Title" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Ministry Classification:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Classification" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Place of Employment:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblMinistry1PlaceofEmployment" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Street1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Stree2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1City" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1State" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Country" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Zip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Phone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Fax:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Fax" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry1Email" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <span class="popuptdspan"><b>Ministry (Secondary)</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Title:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Title" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Ministry Classification:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Classification2" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Place of Employment:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblMinistry2PlaceofEmp" runat="server" CssClass="UserDetailLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Street 1:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Street1" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Street 2:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2street2" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2City" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    State:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2State" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Country" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Zip:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Zip" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Phone" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td class="UserDetailHeader">
                                    Fax:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Fax" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblMinistry2Email" runat="server" CssClass="UserDetailLabel" Text=""></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Picture:
                                </td>
                                <td colspan="3">
                                    <%-- <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />&nbsp;&nbsp;&nbsp;--%>
                                    <asp:Image ID="imgUserView" runat="server" Height="20px" ImageUrl="" Width="50px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 10px;">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnUserViewExit" runat="server" CssClass="ButtonOSF" OnClick="btnUserViewExit_Click"
                                        Text="Cancel" Width="75px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 20px">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
