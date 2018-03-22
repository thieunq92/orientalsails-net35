<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="TrackingReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.TrackingReport" Title="Untitled Page" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
<fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textTrackingReport") %> </legend>
        <div>
            <table>
                <tr>
                    <td>
                        <%= base.GetText("textDateToView") %></td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarDate" runat="server" TargetControlID="txtDate"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                CssClass="button" />
        </div>
        
        <div class="data_table">
            <div class="data_grid">
                <table>                    
                    <asp:Repeater ID="rptBookings" runat="server" OnItemDataBound="rptBookings_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <th><%# base.GetText("textBookingCode") %></th>
                                <th><%# base.GetText("textAgencyCode")%></th>
                                <th><%# base.GetText("textChangedTime") %></th>
                                <th><%# base.GetText("textModifiedBy") %></th>
                                <th><%# base.GetText("textChangedContent") %></th>
                                <th><%# base.GetText("textChangedTotal") %></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink></td>
                                <td><asp:Literal ID="litTACode" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="litTime" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="litUser" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="litContent" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th colspan="5">
                                <%# base.GetText("textGrandTotal") %>
                                </th>
                                <th>
                                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                </th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click"
                CssClass="button" />
    </fieldset>
</asp:Content>
