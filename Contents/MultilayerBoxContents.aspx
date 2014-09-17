<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="MultilayerBoxContents.aspx.cs" Inherits="Contents_MultilayerBoxContents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/2R.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">
      function RedirectTobox(categoryID) {


          var url1 = "MultilayerBoxContents.aspx?CategoryID=" +categoryID;
          
            window.location.href = url1;



      }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" id="dynamicDiv" runat="server" style="margin: 20px 0 0 0;
        width: 95%;">
    </div>

</asp:Content>
