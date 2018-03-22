<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="IncomeReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IncomeReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("titleIncomeReport") %>
        </legend>
        <div>
            <div class="search_panel">
                <table>
                    <tr>
                        <td>
                            <%= base.GetText("textFrom") %>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                            <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                                Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            <%= base.GetText("textTo") %>
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
            <div>
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
            </div>
            <div class="data_table">
                <div class="data_grid">
                    <table>
                        <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                            OnItemCommand="rptBookingList_ItemCommand">
                            <HeaderTemplate>
                                <tr class="header">
                                    <th>
                                        <%= base.GetText("textDate") %>
                                    </th>
                                    <th>
                                        <%= base.GetText("textCheckInPax") %>
                                    </th>
                                    <asp:Repeater ID="rptTrip" runat="server">
                                        <ItemTemplate>
                                            <th>
                                                <%# Eval("TripCode") %>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th>
                                        Total
                                    </th>
                                    <!--<th>
                                        Doanh thu quầy bar</th>-->
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </td>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptItemTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                    <!--<td>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></td>-->
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item" style="background-color: #E9E9E9">
                                    <td>
                                        <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </td>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptItemTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                    <!--<td>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></td>-->
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <th>
                                        GRAND TOTAL</th>
                                    <th>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </th>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptFooterTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <th>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></th>
                                    <!--<th>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></th>-->
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
