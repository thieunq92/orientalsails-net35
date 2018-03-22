<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="OrderReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.OrderReport"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="/Admin/Controls/UserSelector.ascx" TagPrefix="asp" TagName="UserSelector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            My pending booking await confirmation: </legend>
        <div class="search_panel">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <%= base.GetText("textCreated") %>
                    </td>
                    <td>
                        <asp:UserSelector ID="userBooked" runat="server">
                        </asp:UserSelector>
                    </td>
                    <td>
                        <%= base.GetText("textTrip") %>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTrips" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <%= base.GetText("textStartDate") %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarDate" runat="server" TargetControlID="txtDate"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
            CssClass="button" />
        <div class="data_table">
            <div class="pager">
                <svc:Mirror ID="mirrorPager" runat="server" ControlIDToMirror="pagerOrders" />
            </div>
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                        OnItemCommand="rptBookingList_ItemCommand">
                        <HeaderTemplate>
                            <tr class="header">
                                <th style="width: 60px;">
                                    Code
                                </th>
                                <th style="width: 80px;">
                                    <asp:HyperLink ID="hplStartDate" runat="server"><%# base.GetText("textStartDate") %></asp:HyperLink>
                                    <asp:Image runat="server" ID="imgStartDateOrder" Visible="false" />
                                </th>
                                <th>
                                    <asp:HyperLink ID="hplTrip" runat="server"><%# base.GetText("textTrip") %></asp:HyperLink>
                                    <asp:Image runat="server" ID="imgTripOrder" Visible="false" />
                                </th>
                                <th style="width: 180px;">
                                    <asp:HyperLink ID="hplAgency" runat="server"><%# base.GetText("textAgency") %></asp:HyperLink>
                                    <asp:Image runat="server" ID="imgAgencyOrder" Visible="false" />
                                </th>
                                <th style="width: 180px;">
                                    Sale in charge
                                </th>
                                <th style="width: 100px;">
                                    <%# base.GetText("textRoom") %>
                                </th>
                                <th style="width: 140px">
                                    <%# base.GetText("textNumberOfPax") %>
                                </th>
                                <th style="width: 100px;">
                                    Pending until
                                </th>
                                <th style="width: 50px">
                                    <%# base.GetText("textNotify") %>
                                </th>
                                <th style="width: 100px;">
                                    <%# base.GetText("labelAction") %>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td>
                                    <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Label ID="labelDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labelTrip" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labelPartner" runat="server"></asp:Label>
                                    <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litSale" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Label ID="labelRoom" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labelNumberOfPax" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Literal ID="litPendingUntil" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkNofity" runat="server" Checked="False" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtApprove" runat="server" Text='Approve' CommandName="approve"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:LinkButton>
                                    <asp:LinkButton ID="lbtCancel" runat="server" Text='Cancel' CommandName="cancel"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Pager ID="pagerOrders" runat="server" ControlToPage="rptBookingList" OnPageChanged="pagerOrders_PageChanged"
                    PageSize="20" />
            </div>
        </div>
        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click"
            Visible="False" />
    </fieldset>
</asp:Content>
