<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CruisePriceTableCharter.ascx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Controls.CruisePriceTableCharter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
        <tr>
            <th align="left">
                <asp:Label runat="server" ID="lblTrip"></asp:Label>
                <asp:Literal runat="server" ID="litOption"></asp:Literal>
            </th>
            <asp:Repeater runat="server" ID="rptRoomRange" OnItemDataBound="rptRoomRange_OnItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidCharterId" Value='<%#Eval("Id")%>'></asp:HiddenField>
                    <td>
                        <asp:TextBox runat="server" ID="textBoxPrice" Width="80" Text='<%#Convert.ToDouble(Eval("PriceUSD")).ToString("#,0.#")%>'>0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="textBoxPriceVND" Width="80" Text='<%#Convert.ToDouble(Eval("PriceVND")).ToString("#,0.#") %>'>0</asp:TextBox>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>

