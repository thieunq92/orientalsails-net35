<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="BookingOnline.Master" CodeBehind="Confirmation.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BO.Confirmation" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <article id="booking" class="col-md-7 col-lg-8 col-md-push-2 content post-1847 page type-page status-publish hentry">
        <header class="header">
            <h1 class="title"><span class="line"></span>Booking Confirmation</h1>
        </header>
        <section class="post-content clearfix">
            <h4>Booking Infomation</h4>
            <table class="table table-strip">
                <tr>
                    <td width="23%"><b>Departure Date</b></td>
                    <td>
                        <asp:label id="lblStartDate" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td><b>Itinerary</b></td>
                    <td>
                        <asp:label id="lblItinerary" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>

                        <b>Cruise</b>
                    </td>
                    <td>
                        <asp:label id="lblCruise" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Room(s)</b>
                    </td>
                    <td>
                        <asp:label id="lblRoom" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Total Amount</b>
                    </td>
                    <td>
                        <asp:label id="lblTotalAmount" runat="server"></asp:label>
                    </td>
                </tr>
            </table>
            <h4>Your Infomation</h4>
            <table class="table table-strip">
                <tr>
                    <td width="23%"><b>Your Name</b></td>
                    <td>
                        <asp:label id="lblYourName" runat="server"></asp:label>
                    </td>

                </tr>
                <tr>
                    <td><b>Your Email</b></td>
                    <td>
                        <asp:label id="lblYourEmail" runat="server"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Your Phone</b>
                    </td>
                    <td>
                        <asp:label id="lblYourPhone" runat="server"></asp:label>
                    </td>
                </tr>
            </table>
            <h4>Payment Method</h4>
            <div class="row">
                <div class="col-md-12">
                    <input id="noidia" type="radio" name="paymentmethod" checked="checked" value="1" />
                    <label for="noidia">Vietnam local debit card</label>
                </div>
                <div class="col-md-12">
                    <img src="/images/noidia.png" />
                </div>
                <div class="col-md-12">
                    <input id="quocte" type="radio" name="paymentmethod" value="2" />
                    <label for="quocte">International Credit or Debit card</label>
                </div>
                <div class="col-md-12">
                    <img src="/images/quocte.png" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <img src="/images/onepay.png" width="100px" height />
                    </div>
                    <div class="col-md-10">
                        Your payment data is encrypted and secure.<br />
                        We use OnePay, one of Vietnam 's leading payment processor
                    </div>
                </div>
            </div>
            <div class="text-center">
                <asp:button id="btnPayment" runat="server" class="btn view-detail text-center text-uppercase" text="Confirm And Process Payment" onclick="btnPayment_Click" />
            </div>
        </section>
    </article>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Scripts">
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>
