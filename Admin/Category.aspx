<%@ Page Title="OSF::Category" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="Category.aspx.cs" Inherits="Admin_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float:left;margin:10px 10px;">
<table class="FormTable1" width="100%" cellspacing="0" cellpadding="5">
        <tr class="w">
            <td style="font-size: 18px; width: 20%;" id="Td1">
                Add Category
            </td>
            <td style="font-size: 18px; width: 20%;" id="Td2">
                &nbsp;
            </td>
            <td style="font-size: 18px; width: 20%;" id="Td3">
                &nbsp;
            </td>
            <td style="font-size: 18px; width: 20%;" id="Td4">
                &nbsp;
            </td>
            <td style="font-size: 18px; width: 20%;" id="Td5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="1">
                Category Type :
            </td>
            <td colspan="1">
                <asp:DropDownList ID="ddlSectionType" Width="100%" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="1">
                Category Description :
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtCategoryName" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="1">
                &nbsp;
            </td>
            <td colspan="1">
                <asp:CheckBox ID="chkIsLeaf" runat="server" Text="Is Leaf" Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="1">
                Sort Order
            </td>
            <td colspan="1">
               <asp:TextBox ID="txtOrderSeq" Width="100%" runat="server"></asp:TextBox>
               <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" 
 ControlToValidate="txtOrderSeq" ErrorMessage="Value must be an Integer" />
            </td>
        </tr>

                <tr>
            <td colspan="1">
              
            </td>
            <td colspan="1">

                <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />

            </td>
        </tr>

        
                <tr>
            <td colspan="1">
              
            </td>
            <td colspan="1">

                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

            </td>
        </tr>

        <%--        <tr>
            <td colspan="2">
                Manufacturer's Name :
                </td>
            <td colspan="2">
                <asp:TextBox ID="txtManufactName" Width="240" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Manufacturer's Misspellings Name :
                </td>
            <td colspan="2">
                <asp:TextBox ID="txtManuMisspelling" Width="240" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Manufacturer's Alternative Name :
                </td>
            <td colspan="2">
                <asp:TextBox ID="txtManuAlterName" Width="240" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Active:
                </td>
            <td colspan="2">
                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
            </td>
        </tr>
        <tr class="tableHeader">
            <td colspan="4">
                <asp:Button ID="btnManufactur" CssClass="AddUserButton " runat="server" Text="Add Manufacturer" />
            </td>
        </tr>--%>
    </table>
</div>
    
</asp:Content>
