<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="ExchangeRate.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ExchangeRate" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("titleExchangeRate") %>
        </legend>
        
        <div class="advancedinfo">
        <table>
            <tr>
                <th><%= base.GetText("textValidFrom") %></th>
                <th><%= base.GetText("textExchangeRate") %></th>
                <th></th>
            </tr>
        <asp:Repeater ID="rptExchangedRate" runat="server" OnItemDataBound="rptExchangedRate_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="litValidFrom" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="litExchangeRate" runat="server"></asp:Literal></td>
                    <td><asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                    OnClick="btnDelete_Click"/></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td><asp:TextBox ID="txtValidFrom" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtExchangeRate" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClick="btnAdd_Click"/></td>
        </tr>
        </table>
        </div>
    </fieldset>
</asp:Content>
