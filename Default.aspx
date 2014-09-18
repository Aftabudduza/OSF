<%@ Page Title="OSF::Home" Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/Header.ascx" TagPrefix="OSF" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="OSF" TagName="Footer" %>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>OFS Connect</title>
    <!--Main CSS-->
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/popup.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
    <script type="text/jscript">

        function EditContent(cid, isBox) {

            var url1 = "../Admin/AdminContent.aspx?Method=EditContent&isBx=0&ID=" + cid;
            if (isBox == 1) {
                url1 = "../Admin/AdminContent.aspx?Method=EditContent&isBx=1&ID=" + cid;
            }
            window.location.href = url1;
        }

        //DeleteContent

        function DeleteContent(cid, catID) {

            var url1 = "../Admin/AdminContent.aspx?Method=DeleteContent&ID=" + cid + "&CatID=" + catID;
            window.location.href = url1;
        }

        function GetPopupContentDefaultPage(xs) {

            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            var url = "../Contents/BoxContents.aspx?Method=GetPopupContent&ID=" + xs;

            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    document.getElementById("element_to_pop_up2").innerHTML = xmlhttp.responseText;

                    $('#element_to_pop_up2').bPopup({
                        speed: 650,
                        transition: 'slideIn',
                        transitionClose: 'slideBack'
                    });

                }
            }
            xmlhttp.open("Get", url, true);
            xmlhttp.send();
        }
    </script>


    <style type="text/css">
        #element_to_pop_up2
        {
            background-color: #eff0e0;
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
</head>
<body>
    <div id="page">
        <!-- Header -->
        <div class="header">
            <OSF:Header runat="server" ID="Header" />
        </div>
        <!-- End Header -->
        <!--Container-->
        <div class="container">
            <div class="wrapper">
                <div class="left-sidebar" id="dynamicLeftDiv" runat="server">
                </div>
                <div class="mid-container">
                    <div id="dynamicMidDiv" runat="server">
                    </div>
                </div>
                <div class="right-sidebar" id="dynamicRightDiv" runat="server">
                </div>
            </div>
        </div>
        <div id="element_to_pop_up2" class="right-15p-sidebar">
        </div>
        <!--End container-->
        <!-- Footer -->
        <div class="footer">
            <OSF:Footer runat="server" ID="Footer" />
        </div>
        <!-- End Footer -->
    </div>
        <script type="text/javascript">
            function JSOSFPrint(contentSpan, authorspan, datespan, titlespan) {
                var contentControlToPrint = '#' + contentSpan;
                var prtContent = $(contentControlToPrint).html();
                prtContent = $(prtContent).html();  //removint <p> tag


                var authorControlToPrint = '#' + authorspan;
                var prtAuthor = $(authorControlToPrint).html();

                var TitleControlToPrint = '#' + titlespan;
                var prtTitle = $(TitleControlToPrint).html();

                var DateControlToPrint = '#' + datespan;
                var prtDate = $(DateControlToPrint).html();
                var oldPage = document.body.innerHTML;

                document.body.innerHTML = "<table > <tbody> <tr> <td> <span id='TitleTitle' class='standardTextBold'></span> <strong>Title: </strong><span id='TitleSpan'>" + prtTitle + "</span> </td> </tr> <tr> <td colspan='2'> <span id='PostDateTitle' class='standardTextBold'></span> <strong>Date: </strong><span id='DateSpan'>" + prtDate + "</span> </td>	</tr> <tr>	<td colspan='2'><span id='AuthorTitle' class='standardTextBold'></span>	<strong>Author: </strong><span id='AuthorSpan'>" + prtAuthor + "</span></td></tr>	<tr><td colspan='2'><strong>Content: </strong>  </td> </tr>	<tr><td colspan='2'><span id='ContentSpan'><p>" + prtContent + "....</p></span>  </td></tr></tbody></table>";


                window.print();


//                document.body.innerHTML = oldPage;
//                document.getElementById("element_to_pop_up2").style.display = 'none';



            }
    
    </script>

</body>
</html>
