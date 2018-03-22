<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="BookingReportPeriodAll.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingReportPeriodAll"
    Title="Untitled Page" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script runat="server">
        protected new void btnDisplay_Click(object sender, EventArgs e)
        {
            DateTime from = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            PageRedirect(string.Format("BookingReportPeriodAll.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
            //GetDataSource();
            //BindBookings();
        }
    </script>
    <fieldset>
        <div class="search_panel" style="background: none; border: none; margin: 0">
            <%= base.GetText("textDateFrom") %>
            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
            <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </ajax:CalendarExtender>
            <%= base.GetText("textDateTo") %>
            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
            <ajax:CalendarExtender ID="calendarTo" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
            </ajax:CalendarExtender>
            <asp:Button runat="server" ID="btnDisplay" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="View meetings" Style="background-image: url('https://cdn0.iconfinder.com/data/icons/faticons-2/29/view6-128.png');
                height: 20px!important; margin-top: 10px" Text="View meetings" OnClick="btnDisplay_Click" />
        </div>
        <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;
            height: 20px;" class="tabbutton">
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
                                <asp:Repeater ID="rptCruisesRow" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                                    <ItemTemplate>
                                        <th id="thCruise" runat="server">
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptCruiseRoom" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                            <ItemTemplate>
                                                <th colspan="2">
                                                    <asp:Literal ID="litRoomName" runat="server"></asp:Literal>
                                                </th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="litTr" runat="server"></asp:Literal>
                            <td>
                                <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                            </td>
                            <asp:Repeater ID="rptCruiseRoom" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                            </td>
                                            <td id="tdAvail" runat="server">
                                                <asp:Literal ID="litAvail" runat="server"></asp:Literal>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <br />
        <asp:Button runat="server" ID="btnExportMeetings" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
            ToolTip="Export customer data" Style="background-image: url('https://cdn2.iconfinder.com/data/icons/icons-mega-pack-1-and-2/128/Excel.png');"
            Text="Export customer data" OnClick="btnExcel_OnClick"></asp:Button>
    </fieldset>
</asp:Content>
