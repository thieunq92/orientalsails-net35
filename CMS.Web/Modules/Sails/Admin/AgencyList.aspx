<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="AgencyList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyList"
    Title="Agency Manager Page - Oriental Sails Management Office" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <div class="settinglist" style="margin-top: 10px;">
            <div class="search_panel" style="border: none; background: none">
                <table>
                    <tr>
                        <td>
                            <%= base.GetText("textName") %>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="191px"></asp:TextBox>
                        </td>
                        <td>
                            <%= base.GetText("textRole") %>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoles" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <%= base.GetText("textSaleInCharge")%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSales" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contract status
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlContracts">
                                <asp:ListItem Value="-1">All contract status</asp:ListItem>
                                <asp:ListItem Value="0">No contract</asp:ListItem>
                                <asp:ListItem Value="4">Contract sent</asp:ListItem>
                                <asp:ListItem Value="2">Contract in valid</asp:ListItem>
                                <asp:ListItem Value="3">Expired in 30 days</asp:ListItem>
                                <asp:ListItem Value="1">Expired</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Location
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLocations" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button runat="server" ID="btnSearch" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="Search agency" Style="background-image: url('https://cdn2.iconfinder.com/data/icons/crystalproject/128x128/apps/search.png');"
                OnClick="btnSearch_Click" Text="Search agency"></asp:Button>
            <asp:Button runat="server" ID="btnReboundSale" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="Rebound sales" Style="background-image: url('https://cdn2.iconfinder.com/data/icons/large-svg-icons-part-3/512/chain_link_url_seo_arrow_web-512.png')" OnClick="btnReboundSale_Click" Text="Rebound sales">
            </asp:Button>
            <asp:Button runat="server" ID="btnRecheck" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="Recheck sales" Style="background-image: url('https://www.isc.org/wp-content/uploads/2013/05/iconmonstr-clipboard-8-icon-01.png')" OnClick="btnRecheck_Click" Text="Recheck sales">
            </asp:Button>
        </div>
        <table cellspacing="1" border="1 " style="border-collapse: collapse; width: 100%;
            margin-top: 10px;">
            <asp:Repeater ID="rptAgencies" runat="server" OnItemDataBound="rptAgencies_ItemDataBound"
                OnItemCommand="rptAgencies_ItemCommand">
                <HeaderTemplate>
                    <tr class="header">
                        <th style="text-align: center; padding: 10px; width:23%" colspan="2">
                            <%= base.GetText("textAgencyName") %>
                        </th>
                        <th style="text-align: center;width:13%">
                            <%= base.GetText("textPhone") %>
                        </th>
                        <th style="text-align: center;width:7%">
                            <%= base.GetText("textFax") %>
                        </th>
                        <th style="text-align: center;width:19%">
                            <%= base.GetText("textEmail") %>
                        </th>
                        <th style="text-align: center;width:7%">
                            Contract status
                        </th>
                        <th style="text-align: center;width:4%">
                            Payment
                        </th>
                        <th style="text-align: center">
                            <%= base.GetText("textSaleInCharge") %>
                        </th>
                        <th style="text-align: center">
                            <%= base.GetText("textRole") %>
                        </th>
                        <th style="text-align: center">
                            <%= base.GetText("textLastBooking") %>
                        </th>
                        <th style="text-align: center">
                            Price
                        </th>
                        <th style="text-align: center">
                            <%= base.GetText("textAction") %>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="trItem" runat="server" class="item">
                        <td>
                            <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplName" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Phone") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Fax") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Email") %>
                        </td>
                        <td>
                            <asp:Literal ID="litContract" runat="server"></asp:Literal>
                            <asp:HyperLink ID="hplContract" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litPayment"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litSale" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litRole" runat="server"></asp:Literal>
                        </td>
                        <td id="tdLastBooking" runat="server">
                        </td>
                        <td>
                            <asp:HyperLink ID="hplPriceSetting" runat="server">Setting
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplEdit" runat="server">
                                <asp:Image ID="imageEdit" runat="server" ImageAlign="AbsMiddle" AlternateText="Edit"
                                    CssClass="image_button16" ImageUrl="../Images/edit.gif" />
                            </asp:HyperLink>
                            <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="imageButtonDelete"
                                ConfirmText='<%# base.GetText("messageConfirmDelete") %>'>
                            </ajax:ConfirmButtonExtender>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pager">
            <asp:Button runat="server" ID="btnExportAgency" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="Export agency list" Style="background-image: url('https://cdn2.iconfinder.com/data/icons/icons-mega-pack-1-and-2/128/Excel.png');
                float: left" OnClick="btnExport_Click" Text="Export agency list"></asp:Button>
            <asp:HyperLink runat="server" ID="hplViewMeetings" ToolTip="View your meetings" CssClass="image_text_in_button"
                Style="background-image: url('https://cdn1.iconfinder.com/data/icons/IconsLandVistaPeopleIconsDemo/128/Group_Meeting_Light.png');
                float: left">View meetings</asp:HyperLink>
            <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptAgencies"
                OnPageChanged="pagerBookings_PageChanged" PageSize="20" />
        </div>
    </fieldset>
</asp:Content>
