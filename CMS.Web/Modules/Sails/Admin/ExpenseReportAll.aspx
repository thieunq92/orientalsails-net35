<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="ExpenseReportAll.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ExpenseReportAll" Title="Untitled Page" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Expense report </legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td>
                        From</td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        To</td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarTo" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
            CssClass="button" />
        <div>
            <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
                class="tabbutton">
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
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                        OnItemCommand="rptBookingList_ItemCommand">
                        <HeaderTemplate>
                            <tr class="header">
                                <th style="width: 100px">
                                    Date</th>
                                <asp:Repeater ID="rptServices" runat="server">
                                    <ItemTemplate>
                                        <th style="width: 100px">
                                            <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <th style="width: 100px">
                                    Total
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td>
                                    <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink></td>
                                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
                                    <ItemTemplate>
                                        <td>
                                            <asp:Literal ID="litCost" runat="server"></asp:Literal>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td>
                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="item" style="background-color: #E9E9E9">
                                <td>
                                    <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink></td>
                                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
                                    <ItemTemplate>
                                        <td>
                                            <asp:Literal ID="litCost" runat="server"></asp:Literal>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td>
                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>
                                    TOTAL</td>
                                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServiceTotal_ItemDataBound">
                                    <ItemTemplate>
                                        <td>
                                            <asp:Literal ID="litCost" runat="server"></asp:Literal>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td>
                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </fieldset>
</asp:Content>