<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="BasicData.aspx.cs" Inherits="Admin_BasicData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="innerDivCat">
        <fieldset>
            <table class="FormTable1" width="100%" cellspacing="0" cellpadding="5">
                <tr class="w">
                    <td colspan="5" style="font-size: 18px; id="Td7">
                        Basic Data
                    </td>
<%--                    <td style="font-size: 18px; width: 30%;" id="Td8">
                        &nbsp;
                    </td>
                    <td style="font-size: 18px; width: 10%;" id="Td9">
                        &nbsp;
                    </td>
                    <td style="font-size: 18px; width: 30%;" id="Td10">
                        &nbsp;
                    </td>
                    <td style="font-size: 18px; width: 10%;" id="Td11">
                        &nbsp;
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="1">
                        Basic Data Type:
                    </td>
                    <td colspan="1">
                        <asp:DropDownList CssClass="OSFTextBox" ID="ddlSectionType" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlSectionType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td colspan="1">
                        <%--<asp:Button ID="btnAddType" runat="server" Text="Add" AutoPostBack="true" OnClick="btnAddType_Click" />--%>
                    </td>
                    <td colspan="1" id="Td1">
                        <%--<asp:TextBox CssClass="OSFTextBox" ID="txtType" runat="server"></asp:TextBox>--%>
                    </td>
                    <td>
                        <%--<asp:Button ID="btnSaveType" runat="server" Text="Save" AutoPostBack="true" OnClick="btnSaveType_Click" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        Name:
                    </td>
                    <td colspan="1">
                        <asp:TextBox CssClass="OSFTextBox" ID="txtBasicDataName" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="1">
                        &nbsp;
                    </td>
                    <td colspan="1" id="Td2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                                <tr>
                    <td colspan="1">
                        User Defined ID:
                    </td>
                    <td colspan="1">
                        <asp:TextBox CssClass="OSFTextBox" ID="txtUserDiefinedID" runat="server"></asp:TextBox>

                        <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck"
                                    Type="Integer" ControlToValidate="txtUserDiefinedID" ErrorMessage="Value must be an Integer" />
                    </td>
                    <td colspan="1">
                        &nbsp;
                    </td>
                    <td colspan="1" id="Td4">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="1">
                        &nbsp;
                    </td>
                    <td colspan="1">
                        <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" Text="Save" AutoPostBack="true" OnClick="btnSave_Click" />
                    </td>
                    <td colspan="1">
                        &nbsp;
                    </td>
                    <td colspan="1" id="Td3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>

            <asp:GridView ID="gvCategory" runat="Server" Width="100%" Style="margin: 15px 0;"
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

        </fieldset>
    </div>
</asp:Content>
