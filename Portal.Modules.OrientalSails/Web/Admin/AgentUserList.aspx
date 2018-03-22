<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="AgentUserList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgentUserList"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:Panel ID="panelContent" runat="server">
        <fieldset>
            <legend>
                <asp:Label ID="labelStatus" runat="server" Text="User"></asp:Label></legend>
            <div class="settinglist">
                <div class="basicinfo">
                    <asp:Label runat="server" ID="lblListOfUserIn"></asp:Label><asp:Label runat="server"
                        ID="lblRoleName"></asp:Label>
                </div>
                <div class="data_table">
                    <div class="data_grid">
                        <table cellspacing="0" cellpadding="0">
                            <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 150px;">
                                            <asp:HyperLink ID='UserEditLink' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'>
                                            </asp:HyperLink>
                                        </td>
                                        <td style="width: 150px;">
                                            <%# DataBinder.Eval(Container.DataItem, "FullName")%>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="lblDate"></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <tr>
                                        <th style="width: 150px;">
                                            Username
                                        </th>
                                        <th style="width: 150px;">
                                            Full name
                                        </th>
                                        <th style="width: 150px;">
                                            Registered day
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="pager">
                        <cc1:Pager ID="PagerUser" runat="server" HideWhenOnePage="true" ControlToPage="rptUsers"
                            OnPageChanged="PagerUser_PageChanged" />
                    </div>
                </div>
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
