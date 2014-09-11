<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="UnderDev.aspx.cs" Inherits="Admin_UnderDev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        //        function AnotherFunction(x) {
        //            document.getElementById("element_to_pop_up").innerHTML = x;
        //           $('#element_to_pop_up').bPopup();
        //        }

        function GetServerDate(xs) {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            var url = "UnderDev.aspx?Method=GetServerDate&ID=" + xs;
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    document.getElementById("element_to_pop_up").innerHTML = xmlhttp.responseText;
                    $('#element_to_pop_up').bPopup();
                }
            }
            xmlhttp.open("Get", url, true);
            xmlhttp.send();
        }


    </script>
    <style type="text/css">
        #element_to_pop_up
        {
            background-color: #fff;
            border-radius: 15px;
            color: #000;
            display: none;
            padding: 20px;
            min-width: 400px;
            min-height: 180px;
        }
        .b-close
        {
            cursor: pointer;
            position: absolute;
            right: 10px;
            top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <div>
        <button id="my-button">
            POP IT UP</button>
        <!-- Element to pop up -->
        <div id="element_to_pop_up" class="right-15p-sidebar">
            <a class="b-close">x<a />
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:LinkButton OnClick="EditCustomer" ID="lbtnCustomerName" CommandArgument='343'
            CommandName="CustomerName" OnCommand="LinkButton_Command" Visible="true" runat="server"
            Text="rer" ToolTip="Click to edit customer.">

        </asp:LinkButton>
    </div>--%>
    <div id="element_to_pop_up" class="right-15p-sidebar">
        <a class="b-close">x<a />
    </div>
    <div>
        <input type="url" value="Show Server Time" onclick="GetServerDate(7)" />
             <input type="url" value="Show Server Time" onclick="GetServerDate(3)" />
    </div>
</a>
</asp:Content>
