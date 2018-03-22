<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CruisePriceTable.ascx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Controls.CruisePriceTable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%--    <tr>
        <th colspan="100">
            <asp:Literal runat="server" ID="litTripName"></asp:Literal>
        </th>
        <asp:Repeater runat="server" ID="rptRoomTypeHeader">
            <ItemTemplate>
                <th>
                    <%# DataBinder.Eval(Container.DataItem,"Name") %>
                </th>
            </ItemTemplate>
        </asp:Repeater>
        <th>
            Single supplement (+)
        </th>
    </tr>--%>
<asp:Repeater runat="server" ID="rptRoomClass" OnItemDataBound="rptRoomClass_ItemDataBound">
    <ItemTemplate>
        <tr>
            <th align="left">
                <asp:Label runat="server" ID="labelRoomClassId" Style="display: none;"></asp:Label>
                <asp:Label runat="server" ID="lblTrip"></asp:Label>
                <asp:Literal runat="server" ID="litOption"></asp:Literal>
                <asp:Literal runat="server" ID="litName"></asp:Literal>
            </th>
            <asp:Repeater runat="server" ID="rptRoomTypeCell">
                <ItemTemplate>
                    <td>
                        <asp:Label runat="server" ID="labelRoomTypeId" Text='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                            Style="display: none;"></asp:Label>
                        <asp:Label runat="server" ID="labelSailsPriceConfigId" Style="display: none;"></asp:Label>
                        <asp:TextBox runat="server" ID="textBoxPrice" Width="80">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="textBoxPriceVND" Width="80">0</asp:TextBox>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
            <td>
                <asp:TextBox ID="txtSingle" runat="server">0</asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtSingleVND" runat="server">0</asp:TextBox>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
