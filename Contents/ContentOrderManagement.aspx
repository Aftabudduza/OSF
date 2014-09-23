<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="ContentOrderManagement.aspx.cs" Inherits="Contents_ContentOrderManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var MoveUp = "table[id*=gvdetails] input[id*='btnUp']";
        var MoveDown = "table[id*=gvdetails] input[id*='btnDown']";

        $(document).ready(function () {
            DisableLastFirstRow();
            $(MoveUp).click(function () {

                $(this).parents("tr").insertBefore($(this).parents("tr").prev());
                SetIndex();
                DisableLastFirstRow();
                SaveContentOrder($(this).parents("tr").find("td:eq(1)").text(), "Up");
            });
            $(MoveDown).click(function () {

                $(this).parents("tr").insertAfter($(this).parents("tr").next());
                SetIndex();
                DisableLastFirstRow();
                SaveContentOrder($(this).parents("tr").find("td:eq(1)").text(), "Down");
            });
            function DisableLastFirstRow() {
                $("#<%=gvdetails.ClientID%> tr:has(td) input[id*='btnUp']").attr("disabled", false);
                $("#<%=gvdetails.ClientID%> tr:has(td):first input[id*='btnUp']").attr("disabled", true);
                $("#<%=gvdetails.ClientID%> tr:has(td) input[id*='btnDown']").attr("disabled", false);
                $("#<%=gvdetails.ClientID%> tr:last input[id*='btnDown']").attr("disabled", true);
            }
            function SetIndex() {
                $("#<%=gvdetails.ClientID %> tr:has(td)").each(function (index) {
                    index++;
                    $(this).find("td:eq(0)").text(index);
                });
            }
        });
    </script>
    <script type="text/javascript">
        function SaveContentOrder(orderid, upordown) {

            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            var url = "ContentOrderManagement.aspx?OrderID=" + orderid + "&&UD=" + upordown;

            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    //  alert(232);

                }
            }
            xmlhttp.open("Get", url, true);
            xmlhttp.send();

            //              var url1 = "ContentOrderManagement.aspx?OrderID=" + orderid+"&&UD=9";

            //              window.location.href = url1;



        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mid-container" style="margin: 20px 0 0 0; width: 95%;">
        <h3 class="OFSh3">
            Content Order</h3>
        <div class="news-osf" style="padding-left:15px;padding-right:15px;padding-top:20px;">
            <table cellspacing="0" cellpadding="2" border="0" style="margin-left: 20px;">
                <tbody>
                    <tr>
                        <td class="UserDetailHeader" align="right">
                            <span style="color: #FF0000">*</span>
                            <asp:Label ID="Label1" runat="server" Text="Section Type:"></asp:Label>
                        </td>
                        <td width="750" style="height: 19px">
                            <asp:DropDownList CssClass="OSFTextBox" Height="28px" Width="205px" ID="ddlSection"
                                AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="UserDetailHeader" align="right">
                            <span id="Span6" style="color: #FF0000">*</span>
                            <asp:Label ID="Label2" runat="server" Text="File Upload:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList CssClass="OSFTextBox" Height="28px" Width="205px" ID="ddlCateGory"
                                AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCateGory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="UserDetailHeader" align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
         
 

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                            <span class="goodStatus" id="SuccessLabel"></span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="gvdetails" runat="Server" Width="100%" Style="margin: 15px 15px 15px 0;"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4"
                HeaderStyle-BackColor="#D24D8A" HeaderStyle-ForeColor="White" CaptionAlign="Left"
                ForeColor="#333333" onrowcreated="gvdetails_RowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                            <asp:HiddenField ID="hdId" Value='<%# Eval("HotpOrder") %>' runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Font-Size="12px" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="HotpOrder" HeaderText="NeedToHide" SortExpression="ID" 
                        ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Country" ItemStyle-HorizontalAlign="Center">
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="btnUp" type="button" value="&uArr;" style="color: Green;" />
                            <input id="btnDown" type="button" value="&dArr;" style="color: Green;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
