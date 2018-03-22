<%@ Page Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="BookingPayment.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingPayment" Title="Booking Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h1>Booking Payment</h1>
    </div>

    <asp:PlaceHolder ID="plhOneBooking" runat="server">
        <div class="row">
            <div class="col-md-2">
                <label for="bookingcode">Booking Code</label>
            </div>
            <div class="col-md-10">
                <asp:Literal ID="ltrBookingCode" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label for="agency">Agency</label>
            </div>
            <div class="col-md-10">
                <asp:Literal ID="ltrAgency" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label for="startdate">Start Date</label>
            </div>
            <div class="col-md-10">
                <asp:Literal ID="ltrStartDate" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label for="service">Service</label>
            </div>
            <div class="col-md-10">
                <asp:Literal ID="ltrService" runat="server"></asp:Literal>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhMultiAgency" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <asp:Literal ID="litBookings" runat="server"></asp:Literal><br />
            </div>
        </div>
    </asp:PlaceHolder>
    <div class="row">
        <div class="col-md-2">
            <label for="revenue">Revenue</label>
        </div>
        <div class="col-md-10">
            <asp:Literal ID="ltrRevenueUSD" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <label for="revenue">Remained</label>
        </div>
        <div class="col-md-10">
            <asp:Literal ID="ltrRemainUSD" runat="server"></asp:Literal>
            <asp:Literal ID="ltrRemainVND" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <label for="appliedrate">Applied rate</label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtAppliedRate" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row" id="trUsdPaid" runat="server">
        <div class="col-md-2">
            <label for="usdpaid">USD Paid</label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtPaid" CssClass="form-control" runat="server">0</asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <label for="vndpaid">VND Paid</label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtPaidBase" CssClass="form-control" runat="server">0</asp:TextBox>
        </div>
    </div>
    <div class="row" id="trNote" runat="server">
        <div class="col-md-2">
            <label for="Note">Note</label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-2 col-md-10">
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chkPaid" runat="server" />Mark As Paid
                </label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
        </div>
    </div>

    <asp:PlaceHolder ID="plhHistory" runat="server">
        <h2>Payment history</h2>
        <div class="paymenthistory-table">
            <div class="table-responsive">
                <table class="table table-bordered table-hover">
                    <tr>
                        <th rowspan="2">Time</th>
                        <th rowspan="2">Pay by</th>
                        <th colspan="2">Amount</th>
                        <th rowspan="2">Created by</th>
                        <th rowspan="2">Note</th>
                    </tr>
                    <tr>
                        <th>USD</th>
                        <th>VND</th>
                    </tr>
                    <asp:Repeater ID="rptPaymentHistory" runat="server" OnItemDataBound="rptPaymentHistory_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="litTime" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litPayBy" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litAmountUSD" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litAmountVND" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litCreatedBy" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litNote" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th colspan="2">Total</th>
                                <th>
                                    <asp:Literal ID="litTotalUSD" runat="server"></asp:Literal></th>
                                <th>
                                    <asp:Literal ID="litTotalVND" runat="server"></asp:Literal></th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function RefreshParentPage() {
            window.parent.location.href = window.parent.location.href; sedss
        }
    </script>
</asp:Content>
