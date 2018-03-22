<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="HaiPhongExpenseReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HaiPhongExpenseReport"
    Title="Báo cáo chi phí Hải Phong" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Báo cáo chi phí Hải Phong</legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td>From
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>To
                    </td>
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
                    <tr>
                        <th rowspan="2">Ngày
                        </th>
                        <th rowspan="2">SK đi
                        </th>
                        <th colspan="3">SK 2 ngày
                        </th>
                        <th colspan="3">SK 3 ngày
                        </th>
                        <th id="tdCruisePrice" runat="server" colspan="2">Tiền tàu
                        </th>
                        <th rowspan="2">Tổng cộng
                        </th>
                        <th rowspan="2" id="thExchangeRate" runat="server" visible="false">Tỷ giá
                        </th>
                        <th id="thTotalExpenseVND" runat="server" visible="false">Tổng cộng
                        </th>
                    </tr>
                    <tr>
                        <th>SK
                        </th>
                        <th>Đơn giá
                        </th>
                        <th>Thành tiền
                        </th>
                        <th>SK
                        </th>
                        <th>Đơn giá
                        </th>
                        <th>Thành tiền
                        </th>
                        <th id="thUnitPrice" runat="server">Đơn giá
                        </th>
                        <th>Thành tiền
                        </th>
                        <th id="tdVND" runat="server" visible="false">VNĐ
                        </th>
                    </tr>
                    <asp:Repeater ID="rptStartDate" runat="server" OnItemDataBound="rptStartDate_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td id="tdStartDate" runat="server">
                                    <asp:Literal ID="ltrStartDate" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotalCustomer" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal2dayCustomer" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltr2dayServiceExpenseUnitPrice" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal2dayServiceExpense" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal3dayCustomer" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltr3dayServiceExpenseUnitPrice" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal3dayServiceExpense" runat="server" />
                                </td>
                                <td id="tdTotalCruiseExpenseCustomer" runat="server">
                                    <asp:Literal ID="ltrTotalCruiseExpenseCustomer" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotalCruiseExpense" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotalExpense" runat="server" />
                                </td>
                                <td id="tdExchangeRate" runat="server" visible="false">
                                    <asp:Literal ID="ltrExchangeRate" runat="server" />
                                </td>
                                <td id="tdTotalExpenseVND" runat="server" visible="false">
                                    <asp:Literal ID="ltrTotalExpenseVND" runat="server" />
                                </td>
                            </tr>
                            <tr id="trInspection" runat="server">
                                <td>
                                    <asp:Literal ID="ltrTotalCustomerI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal2dayCustomerI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltr2dayServiceExpenseUnitPriceI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal2dayServiceExpenseI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal3dayCustomerI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltr3dayServiceExpenseUnitPriceI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotal3dayServiceExpenseI" runat="server" />
                                </td>
                                <td id="tdTotalCruiseExpenseCustomerI" runat="server">
                                    <asp:Literal ID="ltrTotalCruiseExpenseCustomerI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotalCruiseExpenseI" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrTotalExpenseI" runat="server" />
                                </td>
                                <td id="tdExchangeRateI" runat="server" visible="false">
                                    <asp:Literal ID="ltrExchangeRateI" runat="server" />
                                </td>
                                <td id="tdTotalExpenseVNDI" runat="server" visible="false">
                                    <asp:Literal ID="ltrTotalExpenseVNDI" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th></th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotalCustomer" runat="server" />
                                </th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotal2dayCustomer" runat="server" />
                                </th>
                                <th></th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotal2dayServiceExpense" runat="server" />
                                </th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotal3dayCustomer" runat="server" />
                                </th>
                                <th></th>
                                <th id="thFooterTotal3dayServiceExpense" runat="server">
                                    <asp:Literal ID="ltrTotalTotal3dayServiceExpense" runat="server" />
                                </th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotalCruiseExpenseCustomer" runat="server" />
                                </th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotalCruiseExpense" runat="server" />
                                </th>
                                <th>
                                    <asp:Literal ID="ltrTotalTotalExpense" runat="server" />
                                </th>
                                <th id="thFooterExchangeRate" runat="server" visible="false"></th>
                                <th id="thFooterTotalExpenseVND" runat="server" visible="false">
                                    <asp:Literal ID="ltrTotalTotalExpenseVND" runat="server" />
                                </th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <br />
            <asp:Button ID="btnExportExpense" runat="server" Text="Export" OnClick="btnExportExpense_Click"
                CssClass="button" />
        </div>
    </fieldset>
</asp:Content>
