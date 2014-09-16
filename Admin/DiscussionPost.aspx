<%@ Page Title="Forums" Language="C#" MasterPageFile="~/MasterPages/Main.master"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="DiscussionPost.aspx.cs"
    Inherits="Admin_DiscussionPost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/tinymce/tinymce.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        tinymce.init({
            selector: "textarea",
            encoding: "xml",
            external_plugins: { "nanospell": "/Scripts/tinymce/nanospell/plugin.js" },
            nanospell_server: "asp.net",
            plugins: [
        "advlist autolink lists link image charmap print preview anchor",
        "searchreplace visualblocks code fullscreen",
        "insertdatetime media table contextmenu paste"
    ],
            toolbar: "insertlayer,moveforward,movebackward,absolute,insertimage,fontselect,forecolor,backcolor,fontsizeselect,insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="DisDetailsMainDiv">
        <div class="mid-container" style="margin: 20px 0 0; padding: 0 !important; width: 100%;">
            <div style="width: 99% !important;">
                <div class="h3class">
                    <span id="discussionTitle" runat="server"></span>
                </div>
                <span style="float: left;">
                    <img src="../App_Themes/images/colomn-bg4.png" alt="" /></span></div>
            <div class="news-osf" style="width: 99% !important; margin-bottom: 18px;">
                <div class="PostContainerDiv">
                    <table class="PostTable" width='97%' cellspacing='10' cellpadding='0' border='0'>
                        <tbody id="PostDiv" runat="server">
                        </tbody>
                    </table>
                </div>
            </div>
            <div style="text-align: center;">
                <asp:Button ID="btnBack" CssClass="ButtonOSF" runat="server" Text="Back" OnClick="btnBack_Click" /><asp:Button
                    CssClass="ButtonOSF" ID="btnShowPopup" Text="Reply" runat="server" OnClick="btnShowPopup_Click" /></div>
                                 <input type="button" value="Click to Details" onclick="GetPopupContent(34)" />
        </div>
        <asp:Button ID="btnShowPopup_1" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="PopupMainBg"
            runat="server" TargetControlID="btnShowPopup_1" Drag="true" PopupControlID="pnlpopup"
            CancelControlID="btnCancel">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" CssClass="pnlpopupclass_Dis" BackColor="White"
            Style="display: none;">
            <div class="clsDiv1">
                <h3 style="width: 99% !important; float: left;" class="OFSh3">
                </h3>
                <div style="width: 99% !important;">
                    <div class="h3class">
                        Add New Discussions</div>
                    <span style="float: left;">
                        <img src="../App_Themes/images/colomn-bg4.png" alt="" /></span></div>
                <div class="discussionPopupCss">
                    <table width="700px" cellspacing="5" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td class="UserDetailHeader">
                                    Subject :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" MaxLength="50" CssClass="OSFPopupTextBox" runat="server"
                                        Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="UserDetailHeader">
                                    Message:
                                </td>
                                <td>
                                    <div style="float: left; margin-left: 11px; margin-top: 10px; width: 670px;">
                                        <asp:TextBox ID="txtMessage" TextMode="MultiLine" Rows="20" Columns="50" Height="280"
                                            CssClass="DiscussionMessageBox " runat="server"></asp:TextBox></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="margin-bottom: 10px; margin-top: 20px; text-align: center; width: 100%;">
                                        <asp:Button CssClass="ButtonOSF" ID="btnSave" runat="server" OnClick="btnSave_Click"
                                            Text="Save" Width="55px" />
                                        <asp:Button CssClass="ButtonOSF" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                            Width="75px" Text="Cancel" /></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
