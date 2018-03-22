<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="AgencySelectorPage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencySelectorPage" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Agency list </legend>
        <div class="settinglist">
            <div class="search_panel">
                <table>
                    <tr>
                        <td>
                            Name</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                        <%--                        <td>Contact status</td>
                        <td><asp:DropDownList ID="ddlContractStatus" runat="server"></asp:DropDownList></td>--%>
                        <td>Role</td>
                        <td><asp:DropDownList ID="ddlRoles" runat="server"></asp:DropDownList></td>
                        <td><%= base.GetText("textSaleInCharge")%></td>
                        <td><asp:DropDownList ID="ddlSales" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Location</td>
                        <td><asp:DropDownList runat="server" ID="ddlLocations"/></td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                CssClass="button" />
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table cellspacing="1">
                    <asp:Repeater ID="rptAgencies" runat="server" OnItemDataBound="rptAgencies_ItemDataBound"
                        OnItemCommand="rptAgencies_ItemCommand">
                        <HeaderTemplate>
                            <tr class="header">
                                <th colspan="2">
                                    Agency name</th>
                                <th>
                                    Phone</th>
                                <th>
                                    Fax</th>
                                <th>
                                    Email</th>
                                <th>
                                    Contract</th>
                                <th>
                                    Sale in charge
                                </th>
                                <th>
                                    Role</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="trItem" runat="server" class="item">
                                <td>
                                    <asp:Literal ID="litIndex" runat="server"></asp:Literal></td>
                                <td>
                                    <a id="aName" runat="server"></a>                                    
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
                                    <asp:Literal ID="litSale" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litRole" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptAgencies"
                    OnPageChanged="pagerBookings_PageChanged" PageSize="20" />
            </div>
        </div>
        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" CssCalss="button"/>
    </fieldset>
</asp:Content>