<%@ Page Language="C#" AutoEventWireup="true" Title="OSF::Login" CodeFile="PasswordChange.aspx.cs"
    Inherits="Admin_PasswordChange" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background: #F3D6F4;">
    <form id="form1" runat="server">
    <div class="mid-container" style="margin: 100px 0 0 205px;">
    <table width="400" align="center" cellpadding="5" border="0" id="LoginTable">
            <tbody>
                <tr>
                    <td align="center" colspan="2" class="tableHeader">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOldPassword" runat="server" Text="Old Password:"></asp:Label>
                    &nbsp;</td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDateTtile3" runat="server" Text="New Password:"></asp:Label>
                    &nbsp;</td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="Right">
                        <asp:Label ID="lblDateTtile2" runat="server" Text="Confirm Password:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="OSFTextBox" ID="txtConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="Center" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="Center" colspan="2">
                        <asp:Button CssClass="ButtonOSF" ID="btnSubmit" runat="server" Text="Save"
                            OnClick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="Center" colspan="2">
                        <span id="Errors"><font color="Red"></font></span><span id="Success"><font color="Green">
                        </font></span>
                    </td>
                </tr>
            </tbody>
        </table>
        
    </div>
    </form>
</body>
</html>
