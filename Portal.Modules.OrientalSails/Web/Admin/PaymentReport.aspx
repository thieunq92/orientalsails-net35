<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="PaymentReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.PaymentReport"
    Title="Receivable" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="search_panel">
        <div class="row">
            <div class="col-md-1">
                <label for="from">From</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From Date (dd/mm/yyyy)"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <label for="to">To</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To Date (dd/mm/yyyy)"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <label for="agency">Agency</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtAgency" runat="server" placeholder="Agency Name" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <label for="bookingcode">Booking Code</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtBookingCode" runat="server" CssClass="form-control" placeholder="Booking Code"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <label for="sic">Sales In Charge</label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <label for="services">Services</label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlTrips" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-11">
                <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                    CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <br />
    <svc:Popup ID="popupManager" runat="server">
    </svc:Popup>
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="btnExport" runat="server" Text="Export Receivable By Agency" OnClick="btnExport_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExportRevenue" runat="server" Text="Export Revenue By Cruise" OnClick="btnExportRevenue_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExportRevenueBySale" runat="server" Text="Export Revenue By Sales"
                OnClick="btnExportRevenueBySale_Click" CssClass="btn btn-primary" />
        </div>
    </div>
    <br />
    <div class="receivable-table">
        <div class="table-responsive">
            <table class="table table-bordered table-hover">
                <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound">
                    <HeaderTemplate>
                        <tr class="header custom-warning">
                            <th rowspan="2">Booking code
                            </th>
                            <th rowspan="2">TA Code
                            </th>
                            <th rowspan="2">Sale in charge
                            </th>
                            <th rowspan="2">Partner
                            </th>
                            <th rowspan="2">Cruise
                            </th>
                            <th rowspan="2">Service
                            </th>
                            <th rowspan="2">Date
                            </th>
                            <th colspan="3">No of pax
                            </th>
                            <th colspan="2">No of cabin
                            </th>
                            <th colspan="2">Transfer
                            </th>
                            <th colspan="2">Total
                            </th>
                            <th colspan="2">Paid
                            </th>
                            <th rowspan="2">Applied rate
                            </th>
                            <th rowspan="2">Receivables
                            </th>
                            <th rowspan="2">Action
                            </th>
                            <th rowspan="2">Paid date
                            </th>
                        </tr>
                        <tr class="custom-warning">
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>Infant
                            </th>
                            <th>Double
                            </th>
                            <th>Twin
                            </th>
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>USD
                            </th>
                            <th>VND
                            </th>
                            <th>USD
                            </th>
                            <th>VND
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="trItem" runat="server" class="item">
                            <td>
                                <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>">
                                    <%# DataBinder.Eval(Container.DataItem,"Id","OS{0:00000}") %></a>
                            </td>
                            <td>
                                <%# Eval("AgencyCode")%>
                            </td>
                            <td>
                                <asp:Label ID="lblSalesInCharge" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="hlAgency"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="hlCruise"></asp:HyperLink>
                            </td>
                            <td>
                                <%# Eval("Trip.TripCode") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfAdult" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfBaby" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfDoubleCabin" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfTwinCabin" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfTransferAdult" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfTransferChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_TotalPrice" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_TotalPriceVnd" runat="server"></asp:Label>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"Paid","{0:#,0.#}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"PaidBase","{0:#,0.#}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"CurrencyRate","{0:#,0.#}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"MoneyLeft","{0:#,0.#}") %>
                            </td>
                            <td>
                                <a class="payment" href='BookingPayment.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>'>Payment</a>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem,"PaidDate","{0:dd/MM/yyyy}") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class="item">
                            <td colspan="7">GRAND TOTAL
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfAdult" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfChild" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfBaby" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfDoubleCabin" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfTwinCabin" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfTransferAdult" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfTransferChild" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_TotalPrice" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_TotalPriceVnd" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Literal ID="litPaid" runat="server"></asp:Literal></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Literal ID="litPaidBase" runat="server"></asp:Literal></strong>
                            </td>
                            <td></td>
                            <td>
                                <strong>
                                    <asp:Literal ID="litReceivable" runat="server"></asp:Literal></strong>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var datetimePicker = {
            configInputFrom: function () {
                $("#<%=txtFrom.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                })
            },

            configInputTo: function () {
                $("#<%=txtTo.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                });
            }
        }

        var colorbox = {
            configPayment: function () {
                $(".payment").colorbox({
                    iframe: true,
                    width: 1200,
                    height: 600,
                })
            }
        }

        $(function () {
            datetimePicker.configInputFrom();
            datetimePicker.configInputTo();
            colorbox.configPayment();
        })
    </script>
</asp:Content>
