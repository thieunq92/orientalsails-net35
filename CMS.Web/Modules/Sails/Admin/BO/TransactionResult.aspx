<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="BookingOnline.Master" CodeBehind="TransactionResult.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BO.TransactionResult" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <article id="booking" class="col-md-7 col-lg-8 col-md-push-2 content post-1847 page type-page status-publish hentry">
        <header class="header">
            <h1 class="title"><span class="line"></span>Transaction Result</h1>
        </header>
        <section class="post-content clearfix">
            <h4>Transaction Infomation</h4>
            <table class="table table-strip">
                <tr>
                    <td width="30%"><b>Onepay Transaction ID</b></td>
                    <td>
                        <asp:Label ID="lblTransactionId" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Status</b></td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>Order Information</b></td>

                    <td>
                        <asp:Label ID="lblOrderInfomation" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <b>Total Amount</b>
                    </td>
                    <td>
                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

            <h4>Booking Information</h4>
            <table class="table table-strip">
                <tr>
                    <td width="30%"><b>Booking ID</b></td>
                    <td>
                        <asp:Label ID="lblBookingId" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>Departure Date</b></td>
                    <td>
                        <asp:Label ID="lblDepartureDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Itinerary</b></td>
                    <td>
                        <asp:Label ID="lblItinerary" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Cruise</b></td>
                    <td>
                        <asp:Label ID="lblCruise" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Room(s)</b></td>
                    <td>
                        <asp:Label ID="lblRoom" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </section>
    </article>
</asp:Content>
