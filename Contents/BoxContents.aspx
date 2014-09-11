<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="BoxContents.aspx.cs" Inherits="Contents_BoxContents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <link href="../Styles/2R.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" id="dynamicDiv" runat="server" style="margin: 20px 0 0 0;
        width: 95%;">
    </div>
    <div id="element_to_pop_up" class="right-15p-sidebar">
   
    </div>

</asp:Content>
