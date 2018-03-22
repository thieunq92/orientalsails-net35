<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="AgentList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgentList"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        $(document).ready(function() {
            var listToHide = ['Captain','Sales','Guides','Suppliers','Anonymous user','Authenticated user','Editor','Administrator','Agent $71','Agent $68','Agent $69','Agent $70','Agent $72','Agent $73','Agent $74','Agent $75'];
            for (var i = 0; i < listToHide.length; i++) {
                $('table td a').each(function(k,v) {
                    if ($(v).html() == listToHide[i]) {
                        $(v).parent().parent().css('display', 'none');
                    }
                });  
            }
        })
    </script>
    <asp:Panel ID="panelContent" runat="server">
        <fieldset>
            <div class="settinglist">
                <div class="data_table">
                    <div class="data_grid">
                        <table cellspacing="0" cellpadding="2">
                            <asp:Repeater ID="RepeaterArticles" runat="server"
                                OnItemDataBound="RepeaterArticles_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <th style="width: 70%;">
                                            Agency Role
                                        </th>
                                        <th style="width: 20%;">
                                            Number of users
                                        </th>
                                        <th>
                                            Edit policy
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID='AgentViewLink' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'>
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblUserNumber"></asp:Label></td>
                                        <td>
                                            <asp:HyperLink runat="server" ID="hplPriceTable" ToolTip="Price configuration">Price config</asp:HyperLink>
                                            <asp:HyperLink runat="server" ID="hplEdit" ToolTip='Edit'><img class="image_button16" align="absmiddle" src="../Images/edit.gif" alt="Edit"/></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
            
         <%--   <div class="basicinfo">
                <table>
                    <tr>
                        <td>Single supplement for all agency:</td>
                        <td><asp:TextBox ID="txtSingle" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </div>--%>
            <%--<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />--%>
        </fieldset>
    </asp:Panel>
</asp:Content>
