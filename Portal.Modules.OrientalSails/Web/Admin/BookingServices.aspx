<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="BookingServices.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingServices" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="data_grid">
    <table>
    <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
        <HeaderTemplate>
            <tr>
                <th></th>
                <asp:Repeater ID="rptServices" runat="server">
                    <ItemTemplate>
                    <th><%# DataBinder.Eval(Container.DataItem, "Name") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "FullName") %>
                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                </td>
                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
                    <ItemTemplate>
                        <td>
                        <asp:CheckBox ID="chkService" runat="server" />
                        <asp:HiddenField ID="hiddenServiceId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>'/>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
    </div>
    <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click" />
</asp:Content>
