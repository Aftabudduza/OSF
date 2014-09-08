<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

    <form id="form1" runat="server">
<table width="400" align="Center" cellpadding="5" border="0" id="LoginTable">
	<tbody><tr>
		<td align="Center" colspan="2" class="tableHeader">
						Please enter your Username and Password
					</td>
	</tr><tr>
		<td align="Right"><span class="textboxCaption" id="lblUsername">Username:</span></td><td>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
	</tr><tr>
		<td align="Right"><span class="textboxCaption" id="lblPassword">Password:</span></td><td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
	</tr><tr>
		<td align="Center" colspan="2"> 
            <asp:Button ID="btnLogin" runat="server"  Text="Login" 
                onclick="btnLogin_Click" />
            </td>
	</tr><tr>
		<td align="Center" colspan="2"></td>
	</tr><tr>
		<td align="Center" colspan="2"><span id="Errors"><font color="Red"></font></span><span id="Success"><font color="Green"></font></span></td>
	</tr>
</tbody></table>
    </form>

</html>
