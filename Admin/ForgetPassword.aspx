<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="Admin_ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function validateEmail() {
            var x = document.forms["form1"]["txtEmail"].value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
              //  alert("Not a valid e-mail address");
                return false;
            }
        }


        //Created / Generates the captcha function    
        function DrawCaptcha() {
            var a = Math.ceil(Math.random() * 10) + '';
            var b = Math.ceil(Math.random() * 10) + '';
            var c = Math.ceil(Math.random() * 10) + '';
            var d = Math.ceil(Math.random() * 10) + '';
 
            var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d;
            document.getElementById("txtCaptcha").value = code
        }

        // Validate the Entered input aganist the generated security code function
        function ValidCaptcha() {
            var str1 = removeSpaces(document.getElementById('txtCaptcha').value);
            var str2 = removeSpaces(document.getElementById('txtInput').value);
            if (str1 == str2) {
                var email = removeSpaces(document.getElementById('txtEmail').value);


                var msg = "";

                if (email == "")
                    msg = "Enter email";
//                if (!validateEmail())
//                    msg = "Not a valid e-mail address";

                if (msg.length > 0) {
                    alert(msg);
                }
                else {
                    var url1 = "../Admin/ForgetPassword.aspx?Email=" + email;
                    window.location.href = url1;
                }

            }
            else {
                alert("Invalid captcha");
                
            }

            DrawCaptcha();

        }

        // Remove the spaces from the entered and generated code
        function removeSpaces(string) {
            return string.split(' ').join('');
        }
    
 
    </script>
</head>
<body onload="DrawCaptcha();">
    <form id="form1" runat="server">
    <div class="mid-container" style="margin: 100px 0 0 205px;">
        <table width="400" align="center" cellpadding="5" border="0" id="LoginTable">
            <tbody>
                <tr>
                    <td align="center" colspan="2" class="tableHeader">
                        &nbsp;&nbsp;
                    </td>
                </tr>
         
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        <input type="text" id="txtCaptcha" style="background-image: url(1.jpg); text-align: left;
                            border: none; font-weight: bold; font-family: Modern;width:113px;font-size:23px;" />
                            
                            <input type="image" src="../Images/Refresh.png" style="width:20px;height:20px;" alt="Submit" onclick="DrawCaptcha();"></td>
                </tr>
         
                <tr>
                    <td align="right">
                      <span id="Span1" class="textboxCaption">Write Above text:</span></td>
                    <td>
                        <input type="text" id="txtInput" /></td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="textboxCaption" id="lblUsername">Email </span>&nbsp;</td>
                    <td>
                        <input type="email" id="txtEmail" />
                     
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;</td>
                </tr>

                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        <input id="Button1" type="button" value="Submit" onclick="ValidCaptcha();" /></td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
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
