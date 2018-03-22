<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="PriceConfiguration.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.PriceConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label runat="server" ID="labelTitle"></asp:Label>
        </legend>
        <div class="data_table">
            <div class="data_grid">
                <table style="width: 800px;">
                    <tr>
                        <th>Valid from</th>
                        <th>Valid to</th>
                        <th>Action</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPriceTables" OnItemDataBound="rptPriceTables_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litValidFrom"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litValidTo"></asp:Literal></td>
                                <td><asp:HyperLink runat="server" ID="hplEdit">Edit</asp:HyperLink></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click"/>
        </div>
    </fieldset>
</asp:Content>
