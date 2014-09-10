<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="BoxContents.aspx.cs" Inherits="Contents_BoxContents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="Left-85P-container">
            <h3>
                <asp:Label ID="lblRcentTitle" runat="server" Text="Label"></asp:Label></h3>
            <div id="dynamicDiv" runat="server">
            </div>
        </div>
</asp:Content>

