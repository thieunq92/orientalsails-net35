<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="BookingReportPeriod.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingReportPeriod"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textBookingByPeriod") %>
        </legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td>
                        <%= base.GetText("textDateFrom") %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        <%= base.GetText("textDateTo") %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarTo" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
            CssClass="button" />
        <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
            class="tabbutton">
            <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                <HeaderTemplate>
                    <li id="liMenu" runat="server">
                        <asp:HyperLink ID="hplCruises" runat="server" Text="All"></asp:HyperLink>
                    </li>
                </HeaderTemplate>
                <ItemTemplate>
                    <li id="liMenu" runat="server">
                        <asp:HyperLink ID="hplCruises" runat="server"></asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                        OnItemCommand="rptBookingList_ItemCommand">
                        <HeaderTemplate>
                            <tr class="header">
                                <th rowspan="2" style="width: 80px;">
                                    <%= base.GetText("textDate") %>
                                </th>
                                <th colspan="4" style="width: 100px;">
                                    <%= base.GetText("textNumberOfPax") %>
                                </th>
                                <asp:Repeater ID="rptTrips" runat="server">
                                    <ItemTemplate>
                                        <th rowspan="2">
                                            <%# DataBinder.Eval(Container.DataItem, "TripCode") %>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                    <ItemTemplate>
                                        <th colspan="2">
                                            <asp:Literal ID="litRoomName" runat="server"></asp:Literal>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <th rowspan="2">
                                Tổng
                                </th>
                                <th rowspan="2">
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    <%= base.GetText("textAdult") %>
                                </th>
                                <th>
                                    <%= base.GetText("textChild") %>
                                </th>
                                <th>
                                    <%= base.GetText("textBaby") %>
                                </th>
                                <th>
                                    <%= base.GetText("textTotalPax") %>
                                </th>
                                <asp:Repeater ID="rptRoomAvail" runat="server">
                                    <ItemTemplate>
                                        <th>
                                            Total</th>
                                        <th>
                                            Avail</th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="litTr" runat="server"></asp:Literal>
                            <td>
                                <asp:Panel CssClass="hover_content" ID="PopupMenu" runat="server">
                                    <asp:Literal ID="litNote" runat="server"></asp:Literal>
                                </asp:Panel>
                                <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal ID="litAdult" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Literal ID="litChild" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Literal ID="litBaby" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                            </td>
                            <asp:Repeater ID="rptTrips" runat="server" OnItemDataBound="rptTrips_ItemDataBound">
                                <ItemTemplate>
                                    <td>
                                        <asp:Literal ID="litPax" runat="server"></asp:Literal>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="rptRoomAvail" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                <ItemTemplate>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                    <td id="tdAvail" runat="server">
                                        <asp:Literal ID="litAvail" runat="server"></asp:Literal></td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td>
                                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtLock" runat="server" CommandName='lock'></asp:LinkButton>
                                <asp:Image ImageUrl="../Images/info.png" ID="imgNote" runat="server" />
                                <ajax:HoverMenuExtender ID="hmeNote" runat="Server" HoverCssClass="popupHover" PopupControlID="PopupMenu"
                                    PopupPosition="Left" TargetControlID="imgNote" PopDelay="25" />
                            </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <asp:Button ID="btnExcel" runat="server" Text="Export" OnClick="btnExcel_Click" CssClass="button" />
    </fieldset>
</asp:Content>
