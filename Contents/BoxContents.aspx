<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="BoxContents.aspx.cs" Inherits="Contents_BoxContents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/2R.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
//        function EditContent(xs1)
//        {
//            var xmlhttp1;
//            if (window.XMLHttpRequest)
//            {// code for IE7+, Firefox, Chrome, Opera, Safari
//                xmlhttp1 = new XMLHttpRequest();
//            }
//            else
//            {// code for IE6, IE5
//                xmlhttp1 = new ActiveXObject("Microsoft.XMLHTTP");
//            }
//            var url1 = "../Admin/AdminContent.aspx?Method=EditContent&ID=" + xs1;
//            window.location.href = url1;
//        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" id="dynamicDiv" runat="server" style="margin: 20px 0 0 0;
        width: 95%;">
    </div>

</asp:Content>
