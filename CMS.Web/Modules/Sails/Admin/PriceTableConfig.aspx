<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="PriceTableConfig.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.PriceTableConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Controls/CruisePriceTable.ascx" TagPrefix="uc" TagName="PriceTable" %>
<%@ Register Src="../Controls/CruisePriceTableCharter.ascx" TagPrefix="uc" TagName="PriceTableCharter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <style>
        .table th, .table td
        {
            text-align: center;
        }
    </style>
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" runat = "server" id = "imgAvatar"/>
            <asp:Label ID="labelTitle" runat="server"></asp:Label>
        </legend>
        <asp:HyperLink runat="server" ID="hplBack">Back to list</asp:HyperLink>
        <table runat="server" id="tblDateTime">
            <tr>
                <td>
                    Valid from
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtValidFrom"></asp:TextBox>
                    <ajax:CalendarExtender ID="calenderFrom" runat="server" TargetControlID="txtValidFrom"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
                <td>
                    Valid to
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtValidTo"></asp:TextBox>
                    <ajax:CalendarExtender ID="calenderTo" runat="server" TargetControlID="txtValidTo"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label runat="server" ID="lblSailsPriceTableTemplate" Text="Lấy giá tham khảo từ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTemplatePriceTable" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGetTemplatePriceTable" OnClick="btnGetTemplatePriceTable_OnClick"
                        Text="Lấy Bảng Giá" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnBackSailPriceTable" OnClick="btnBackSailPriceTable_OnClick"
                        Text="Quay Lại Bảng Giá Cũ" />
                </td>
            </tr>
        </table>
        <div>
            <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
                class="tabbutton">
                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                    <ItemTemplate>
                        <li id="liMenu" runat="server">
                            <asp:HyperLink ID="hplCruises" runat="server"></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <table class="table table-bordered table-striped">
            <tr>
                <th>
                </th>
                <asp:Repeater runat="server" ID="rptRoomTypeHeader" OnItemDataBound="rptRoomTypeHeader_OnItemDataBound">
                    <ItemTemplate>
                        <th colspan="2">
                            <%# DataBinder.Eval(Container.DataItem,"Name") %>
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <th colspan="2">
                    Single supplement (+)
                </th>
            </tr>
            <tr>
                <th>
                </th>
                <asp:Literal runat="server" ID="ltrCurrency" />
                <th>
                    USD
                </th>
                <th>
                    VND
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rptTrips" OnItemDataBound="rptTrips_ItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hiddenTripId" Value='<%# DataBinder.Eval(Container.DataItem,"Trip.Id") %>' />
                    <asp:HiddenField runat="server" ID="hiddenTripOption" Value='<%# DataBinder.Eval(Container.DataItem,"Option") %>' />
                    <uc:PriceTable runat="server" ID="priceTable" />
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:PlaceHolder runat="server" ID="plhCharterTitle">
            <br />
            <hr />
            <h4>
                Charter Price Table</h4>
        </asp:PlaceHolder>
        <div id="roomrange">
            From
            <asp:DropDownList runat="server" ID="ddlRoomFrom">
            </asp:DropDownList>
            Room(s) To
            <asp:DropDownList runat="server" ID="ddlRoomTo">
            </asp:DropDownList>
            Room(s)
            <asp:Button runat="server" ID="btnAddRoomRange" Text="Add Room Range" OnClick="btnAddRoomRange_OnClick" />
            <br />
        </div>
        <asp:PlaceHolder runat="server" ID="plhCharter">
            <br />
            <table class="table table-bordered table-striped">
                <tr>
                    <th>
                    </th>
                    <asp:Repeater runat="server" ID="rptRoomRangeTable" OnItemDataBound="rptRoomRangeTable_OnItemDataBound">
                        <ItemTemplate>
                            <th colspan="2">
                                <%# Eval("RoomFrom") %> room(s)
                                -
                                <%# Eval("RoomTo")%> room(s)
                            </th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <tr>
                    <th>
                    </th>
                    <asp:Literal runat="server" ID="ltrCurrencyRoomRange" />
                </tr>
                <asp:Repeater runat="server" ID="rptTripsCharterTable" OnItemDataBound="rptTripsCharterTable_OnItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hiddenTripId" Value='<%# DataBinder.Eval(Container.DataItem,"Trip.Id") %>' />
                        <asp:HiddenField runat="server" ID="hiddenTripOption" Value='<%# DataBinder.Eval(Container.DataItem,"Option") %>' />
                        <uc:PriceTableCharter runat="server" ID="priceTable" />
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <br />
        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
