<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="BookingHistories.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingHistories"
    Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="uc" TagName="customer" Src="../Controls/CustomerInfoRowInput.ascx" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Booking historical data
        </legend>
        <div class="settinglist">
            
            <div class="basicinfo">
                <h4>Date</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptDates" OnItemDataBound="rptDates_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>            
            
            <div class="basicinfo">
                <h4>Trip</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptTrips" OnItemDataBound="rptTrips_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            
            <div class="basicinfo">
                <h4>Total value</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptTotals" OnItemDataBound="rptTotals_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            
            <div class="basicinfo">
                <h4>Status</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptStatus" OnItemDataBound="rptStatus_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            
            <div class="basicinfo">
                <h4>Agencies</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAgencies" OnItemDataBound="rptAgencies_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="basicinfo">
                <h4>Cabins</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptCabins" OnItemDataBound="rptCabins_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="basicinfo">
                <h4>Customers</h4>
                <table style="width: 870px;">
                    <tr>
                        <th style="width:120px;">Time</th>
                        <th style="width:150px;">User</th>
                        <th style="width:300px;">From</th>
                        <th style="width:300px;">To</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptCustomers" OnItemDataBound="rptCustomers_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </fieldset>
</asp:Content>