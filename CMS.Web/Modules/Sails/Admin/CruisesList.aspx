<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CruisesList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CruisesList"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textBookingList") %>
        </legend>
        <div class="settinglist">
            <div class="search_panel">
            </div>
            <div class="data_table">
                <div class="data_grid">
                    <table cellspacing="1">
                        <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Code") %></td>
                                    <td><asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink></td>
                                    <td><asp:HyperLink ID="hplRoomClasses" runat="server">Room class</asp:HyperLink></td>
                                    <td><asp:HyperLink ID="hplRooms" runat="server">Rooms</asp:HyperLink></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
