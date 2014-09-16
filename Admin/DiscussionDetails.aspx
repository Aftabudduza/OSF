<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="DiscussionDetails.aspx.cs" Inherits="DiscussionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="DisDetailsMainDiv">
        <div class="mid-container" style="margin: 20px 0 0; padding: 0 !important; width: 100%;">
            <div style="width: 99% !important;">
                <div class="h3class">
                    <span id="discussionTitle" runat="server"></span>
                </div>
                <span style="float: left;">
                    <img src="../App_Themes/images/colomn-bg4.png" alt="" /></span></div>
            <div class="news-osf" style="width: 99% !important;">
                <fieldset>
                    <div style="margin-left: 10px; margin-right: 10px; margin-top: -4px;">
                        <asp:GridView ID="gvDiscussion" runat="Server" Width="100%" Style="margin: 15px 15px 15px 0;"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4"
                            CaptionAlign="Left" ForeColor="#333333" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                            PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
                            GridLines="None" PageSize="4" SelectedIndex="0" OnPageIndexChanging="gvDiscussion_PageIndexChanging">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#D24D8A" Font-Size="Small" Font-Bold="false"
                                CssClass="grid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Topics" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdId" Value='<%# Eval("ContentID") %>' runat="server" />
                                        <asp:HyperLink ID="hlkQuizans" runat="server" NavigateUrl='<%# "~/Admin/DiscussionPost.aspx?ContentIdDiscussionDetail=" + Eval("ContentID")+"&Title="+Eval("Title") %>'> <%#Eval("Title")%></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Author" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Eval("Author")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#Eval("Post")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Replay" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#Eval("LastUpdated")%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%-- <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick="return show_confirm();"
                                            OnClick="lbtnDelete_Click">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                NextPageText="Next" PreviousPageText="Previous" />
                            <PagerStyle CssClass="form-actions tag" ForeColor="#000" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <div>
                                    <div style="height: 20px">
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#D24D8A" Font-Bold="True" ForeColor="White" CssClass="headerstyle" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
        <div style="width: 99% !important; margin-top: 20px; float: left;">
            <div class="h3class">
                Add New Discussions</div>
            <span style="float: left;">
                <img src="../App_Themes/images/colomn-bg4.png" alt="" /></span></div>
        <div class="mid-container" style="width: 100%; padding: 0 !important;" id="MessageDiv"
            runat="server">
            <div class="news-osf" style="width: 99% !important;">
                <table width="40%" class="disTable" cellspacing="0" cellpadding="5" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <span id="Span1" style="color: #FF0000">*</span> Subject :
                            </td>
                            <td width="210px" style="float: left">
                                <asp:TextBox CssClass="OSFTextBox" ID="txtsubject" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button CssClass="ButtonOSF" ID="btnDiscussion" runat="server" Text="Discussion Topics"
                                    OnClick="btnDiscussion_Click" />
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
