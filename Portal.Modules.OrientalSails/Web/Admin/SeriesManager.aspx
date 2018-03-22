<%@ Page Language="C#" Title="Series Bookings Manager" AutoEventWireup="true" CodeBehind="SeriesManager.aspx.cs" MasterPageFile="MO.Master"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.SeriesManager" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Partner
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtPartner" runat="server" CssClass="form-control" placeholder="Partner"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Series code
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtSeriesCode" runat="server" CssClass="form-control" placeholder="Series code"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button ID="buttonSearch" runat="server" OnClick="buttonSearch_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <div class="table-panel">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover">
                    <tr class="active">
                        <th>Series Code</th>
                        <th>Partner</th>
                        <th>Booker</th>
                        <th>Sale in charge</th>
                        <th>No of booking</th>
                        <th>Cutoff date</th>
                        <th>No of days</th>
                        <th></th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptListSeries" OnItemCommand="rptListSeries_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("SeriesCode") %></td>
                                <td><%# Eval("Agency.Name")%></td>
                                <td><%# Eval("Booker.Name")%></td>
                                <td><%# Eval("Agency.Sale.UserName")%></td>
                                <td><%# Eval("ListBooking.Count")%></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "CutoffDate", "{0:dd/MM/yyyy}")%></td>
                                <td><%# Eval("NoOfDays")%></td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" CommandName="cancel" CommandArgument='<%# Eval("Id") %>' Text="Cancel" class="btn btn-primary" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <nav arial-label="...">
        <div class="pager">
            <svc:Pager ID="pagerSeries" runat="server" HideWhenOnePage="true" ControlToPage="rptListSeries"
                PagerLinkMode="HyperLinkQueryString" PageSize="20" />
        </div>
    </nav>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
