<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="SailsPriceConfig.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SailsPriceConfig"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleSailsPriceConfig" runat="server"></asp:Label>
        </legend>
        <asp:Panel ID="panelContent" runat="server">
            <div class="settinglist">
                <div class="advancedinfo">
                    <table>
                        <tr>
                            <th>
                                Cruise
                            </th>
                            <th>
                                Valid from</th>
                            <th>
                                Valid to</th>
                            <th>
                            </th>
                        </tr>
                        <asp:Repeater ID="rptPriceTables" runat="server" OnItemDataBound="rptPriceTables_ItemDataBound"
                            OnItemCommand="rptPriceTables_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                  <td>
                                    <asp:Label ID="labelCruise" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelValidFrom" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="labelValidTo" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="hplEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>Edit</asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Button ID="btnAddNew" runat="server" CssClass="button" Text="Add new table"
                    OnClick="btnAddNew_Click" />
                <div class="advancedinfo">
                    <table>
                        <tr>
                            <td>
                                Valid From
                                <asp:TextBox ID="textBoxStartDate" runat="server"></asp:TextBox>
                                <ajax:CalendarExtender ID="calenderFrom" runat="server" TargetControlID="textBoxStartDate" Format="dd/MM/yyyy"></ajax:CalendarExtender></td>
                            <td>
                                Valid To
                                <asp:TextBox ID="textBoxEndDate" runat="server"></asp:TextBox>
                                <ajax:CalendarExtender ID="calenderTo" runat="server" TargetControlID="textBoxEndDate" Format="dd/MM/yyyy"></ajax:CalendarExtender></td>
                            <td>Apply for
                            <asp:DropDownList ID="ddlCruises" runat="server"></asp:DropDownList>
                            </td>                            
                        </tr>
                    </table>
                    <table>
                        <asp:Repeater runat="server" ID="rptRoomClass" OnItemDataBound="rptRoomClass_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <th>
                                    </th>
                                    <asp:Repeater runat="server" ID="rptRoomTypeHeader">
                                        <ItemTemplate>
                                            <th>
                                                <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th>Single supplement (+)</th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <th align="left">
                                        <asp:Label runat="server" ID="labelRoomClassId" Style="display: none;"></asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                    </th>
                                    <asp:Repeater runat="server" ID="rptRoomTypeCell">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Label runat="server" ID="labelRoomTypeId" Text='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                                                    Style="display: none;"></asp:Label>
                                                <asp:Label runat="server" ID="labelSailsPriceConfigId" Style="display: none;"></asp:Label>
                                                <asp:TextBox runat="server" ID="textBoxPrice" Width="80"></asp:TextBox>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td><asp:TextBox ID="txtSingle" runat="server"></asp:TextBox></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Button ID="buttonSubmit" runat="server" CssClass="button" OnClick="buttonSubmit_Click" />
                <asp:Button ID="buttonCancel" runat="server" CssClass="button" OnClick="buttonCancel_Click" />
            </div>
        </asp:Panel>
    </fieldset>
</asp:Content>
