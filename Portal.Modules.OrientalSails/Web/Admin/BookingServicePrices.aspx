<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="BookingServicePrices.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingServicePrices" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
        <div class="data_grid">
    <table>
    <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
        <HeaderTemplate>
            <tr>
                <th>Dịch vụ</th>
                <th>Đơn giá</th                
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                </td>
                <td>
                    <asp:HiddenField ID="hiddenPriceId" runat="server" Value='-1'/>
                    <asp:TextBox ID="txtPrice" runat="server" Text="0"></asp:TextBox>
                </td>                
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
    </div>
    <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click" />
</asp:Content>
