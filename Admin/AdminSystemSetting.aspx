<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="AdminSystemSetting.aspx.cs" Inherits="Admin_AdminSystemSetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>     
         <div class="mid-container" style="margin: 0 0 0 205px;">
        <asp:GridView ID="gvSystem" runat="Server" Width="100%" Style="margin: 15px 0;" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CaptionAlign="Left"
            ForeColor="#D24D8A" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
            PagerSettings-NextPageText="Next" PagerSettings-Mode="NumericFirstLast" PagerSettings-PreviousPageText="Previous"
            GridLines="None" PageSize="15" SelectedIndex="0" ShowFooter="False" OnRowEditing="gvSystem_RowEditing"  OnRowUpdating="gvSystem_RowUpdating" OnRowCancelingEdit="gvSystem_RowCancelingEdit" 
             AutoGenerateEditButton="true"   EnableViewState="True">
            <RowStyle BackColor="#F7F6F3" ForeColor="#D24D8A" Font-Size="Small" Font-Bold="false"
                CssClass="grid" />
            <Columns>
                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                   <ItemTemplate>
                        <%# Eval("Name")%>
                    </ItemTemplate>
                    <EditItemTemplate>                       
                        <asp:TextBox ID="txtName" runat="Server"  Text='<%# Eval("Name") %>'></asp:TextBox>                        
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                        <%# Eval("Description")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="Server" Text='<%# Eval("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enabled" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("Enabled")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkEnable" Checked='<%# Eval("Enabled") %>' runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Numeric Val" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("NumVal")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNumeric" runat="Server" Text='<%# Eval("NumVal") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="String Val" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("StrVal")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtString" runat="Server" Text='<%# Eval("StrVal") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="">   
                    <ItemTemplate>
                       <asp:HiddenField ID="hdstate" Value='<%# Eval("SystemSettingID") %>' runat="server" />
                         <%--<asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick="return show_confirm();" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>--%>                     
                    </ItemTemplate>                 
                    
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
            <EditRowStyle BackColor="" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

