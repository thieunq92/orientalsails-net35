<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BookingView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingView"
    Title="Booking View" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="uc" TagName="customer" Src="../Controls/CustomerInfoRowInput.ascx" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="buttonSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="screenCapture.capture();return false" />
            <asp:Button ID="button1" runat="server" CssClass="btn btn-primary hidden" OnClick="buttonSubmit_Click" />
            <a href="SendEmail.aspx?NodeId=1&SectionId=15&BookingId=<%= Booking.Id %>" class="btn btn-primary" id="sendemail">SendEmail</a>
            <a href="BookingHistories.aspx?NodeId=1&SectionId=15&BookingId=<%= Booking.Id %>" class="btn btn-primary">View History</a>
        </div>
    </div>
    <label id="screencapture_status" class="hidden"></label>
    <div class="bookinginfor-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 ">
                    <label for="bookingcode">BookingCode</label>
                </div>
                <div class="col-xs-1">
                    <asp:Label ID="lblBookingId" runat="server"></asp:Label>
                </div>
                <div class="col-xs-1">
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="chkInspection" runat="server">Inspection
                        </label>
                    </div>
                </div>
                <div class="col-xs-1">
                    <asp:UpdatePanel runat="server">
                        <contenttemplate>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" id="chkCharter" runat="server" onserverchange="chkCharter_OnCheckedChanged" onclick="submit()">Charter
                                </label>
                            </div>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-xs-1">
                    <label for="trip">Trip</label>
                </div>
                <div class="col-xs-4">
                    <asp:PlaceHolder runat="server" ID="plhTripReadonly" Visible="false">
                        <asp:Literal runat="server" ID="litTrip"></asp:Literal>
                        (contact accountant to change) </asp:PlaceHolder>
                    <asp:DropDownList ID="ddlTrips" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-control">
                        <asp:ListItem>Option 1</asp:ListItem>
                        <asp:ListItem>Option 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    <label for="cruise">Cruise</label>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlCruises" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="startdate">Start Date</label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Start Date (dd/mm/yyyy)" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <label for="status">Status</label>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlStatusType" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <div id="statusinfor-placeholder">
                        <asp:TextBox ID="txtDeadline" runat="server" placeholder="Deadline Pending" CssClass="form-control"></asp:TextBox>
                        <asp:TextBox ID="txtCutOffDays" runat="server" placeholder="CutOff Days" CssClass="form-control"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtCancelledReason" TextMode="MultiLine" placeholder="Lý do hủy booking" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-1 nopadding-right">
                    <label for="nop">Number Of Pax</label>
                </div>
                <div class="col-xs-1">
                    <asp:Literal ID="litPax" runat="server"></asp:Literal>
                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="<%= PaxGetDetails() %>"></i>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="agency">Agency</label>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlAgencies" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1 nopadding-left">
                    <asp:TextBox ID="txtAgencyCode" CssClass="form-control" placeholder="TA Code" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <label for="booker">Booker</label>
                </div>
                <div class="col-xs-2">
                    <svc:CascadingDropDown ID="cddlBooker" runat="server" CssClass="form-control">
                    </svc:CascadingDropDown>
                </div>
                <div class="col-xs-offset-2 col-xs-1 nopadding-right">
                    <label for="noc">Number Of Cabin</label>
                </div>
                <div class="col-xs-1">
                    <asp:Literal ID="litCabins" runat="server"></asp:Literal>
                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="<%= CabinGetDetails() %>"></i>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="total">Total</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtTotal" runat="server" placeholder="Total" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                </div>
                <div class="col-xs-1 nopadding-left">
                    <asp:DropDownList runat="server" ID="ddlCurrencies" CssClass="form-control">
                        <asp:ListItem Value="1">USD</asp:ListItem>
                        <asp:ListItem Value="0">VND</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-4">
                    <asp:Button ID="lbtCalculate" CssClass="btn btn-primary" runat="server" OnClick="lbtCalculate_Click" Text="Calculate"></asp:Button>
                    <asp:Button runat="server" ID="btnLockIncome" CssClass="btn btn-primary" Visible="false" Text="Lock this booking"
                        OnClick="btnLockIncome_Click" />
                    <asp:Button runat="server" ID="btnUnlockIncome" Visible="false" CssClass="btn btn-primary"
                        Text="Unlock" OnClick="btnUnlockIncome_Click" />
                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="<%= UserGetUserLockIncomeDetails() %>"></i>
                </div>

            </div>
        </div>
        <div class="form-group" style="display: none">
            <div class="row">
                <div class="col-xs-1">
                    <label for="commission">Commission</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtCommission" runat="server" placeholder="Commission" CssClass="form-control "></asp:TextBox>
                </div>
                <div class="col-xs-1 nopadding-left">
                    <asp:DropDownList runat="server" ID="ddlCommissionCurrencies" CssClass="form-control">
                        <asp:ListItem Value="1">USD</asp:ListItem>
                        <asp:ListItem Value="0">VND</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
        </div>
        <div class="form-group" style="display: none">
            <div class="row">
                <div class="col-xs-1 nopadding-right">
                    <label for="cancelpenalty">Cancel Penalty</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtPenalty" runat="server" Text="0" CssClass="form-control" placeholder="Cancel Penalty"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="vouchercode">
                        Voucher Code
                    </label>
                </div>
                <div class="col-xs-4">
                    <div class="input-group">
                        <asp:TextBox ID="txtAllVoucher" placeholder="Nhập tất cả mã voucher ngăn cách bởi dấu chấm phẩy" runat="server" CssClass="form-control"></asp:TextBox>

                        <span class="input-group-btn">
                            <input type="button" class="btn btn-primary" value="Check Code" id="checkvoucher" style="height: 25px" />
                        </span>
                    </div>
                </div>
                <div class="col-xs-7">
                    <div class="checkbox">
                        <label class="checkbox-inline">
                            <input runat="server" id="chkSpecial" type="checkbox" />Upgrade/Special price</label>
                        <label class="checkbox-inline">
                            <input type="checkbox" runat="server" id="chkInvoice">Invoice</label>
                        <label class="checkbox-inline">
                            <input id="chkIsPaymentNeeded" runat="server" type="checkbox" />
                            Pay Before Tour
                        </label>
                        <label class="checkbox-inline">
                            <input runat="server" id="chkEarlyBird" type="checkbox" />Early Bird
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="extraservices">Extra Services</label>
                </div>
                <div class="col-xs-3">
                    <asp:PlaceHolder ID="plhDetailService" runat="server">
                        <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound">
                            <ItemTemplate>
                                <div class="checkbox">
                                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%#Eval("Id") %>' />
                                    <label>
                                        <input id="chkService" runat="server" type="checkbox" /><%#Eval("Name") %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:PlaceHolder>
                </div>
                <div class="col-xs-offset-5 col-xs-3">
                    <asp:Literal runat="server" ID="litInform" Visible="true" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-offset-1 col-xs-10 custom-col-xs-offset-1-margin">
                <p>
                    <asp:Literal runat="server" ID="litCreated"></asp:Literal>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label for="pickupaddress">
                    Pickup Address
                </label>
            </div>
            <div class="col-xs-4">
                <label for="specialrequest">
                    Special Request
                </label>
            </div>
            <div class="col-xs-4">
                <label for="Customer Info">
                    Customer Info
                </label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <asp:TextBox ID="txtPickup" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Pickup Address"></asp:TextBox>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtSpecialRequest" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Special Request"></asp:TextBox>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtCustomerInfo" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Customer Info"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-12">
                <a href="RoomSelector.aspx?NodeId=1&SectionId=15&bookingid=<%= Booking.Id %>" class="btn btn-primary" id="roomorganizer">Room Organizer</a>
            </div>
        </div>
    </div>

    <div class="customerinfo-panel">
        <div class="panel panel-default">
            <asp:Repeater ID="rptRoomList" runat="server" OnItemDataBound="rptRoomList_ItemDataBound"
                OnItemCommand="rptRoomList_ItemCommand">
                <ItemTemplate>
                    <div class="roominfor-hiddenpanel">
                        <asp:HiddenField ID="hiddenBookingRoomId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        <asp:HiddenField ID="hiddenRoomClassId" runat="server" />
                        <asp:HiddenField ID="hiddenRoomTypeId" runat="server" />
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-2">
                                <label for="roomname">
                                    <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                                </label>
                            </div>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" placeholder="Room Number" title="Room Number">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5">
                                <div class="checkbox">
                                    <label class="checkbox-inline">
                                        <input id="checkBoxAddChild" runat="server" type="checkbox" />Add Child</label>
                                    <label class="checkbox-inline">
                                        <input id="checkBoxAddBaby" runat="server" type="checkbox" />Add Baby</label>
                                    <label class="checkbox-inline">
                                        <input id="checkBoxSingle" runat="server" type="checkbox" />Single</label>
                                </div>
                            </div>
                            <div class="col-xs-offset-1 col-xs-2 text-right">
                                <asp:Label ID="labelRoomTypes" runat="server" Text="change room type to"></asp:Label>
                            </div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlRoomTypes" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                    CommandName="delete" CssClass="btn btn-primary" Text="Delete this room" OnClientClick="return confirm('All unsaved customer data (included another rooms in this book) will be lost forever. Are you sure want to delete this room?')" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-1">
                                <label for="client1">Client 1</label>
                            </div>
                            <uc:customer ID="customer1" runat="server" ChildAllowed="true"></uc:customer>
                            <div>
                                <asp:Repeater ID="rptServices1" runat="server">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkService" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            CssClass="checkbox" /><asp:HiddenField ID="hiddenServiceId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:HiddenField ID="hiddenId" runat="server" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="row" id="trCustomer2" runat="server">
                            <div class="col-xs-1">
                                <label for="client1">Client 2</label>
                            </div>
                            <uc:customer ID="customer2" runat="server" ChildAllowed="true"></uc:customer>
                            <div>
                                <asp:Repeater ID="rptServices2" runat="server">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkService" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            CssClass="checkbox" /><asp:HiddenField ID="hiddenServiceId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:HiddenField ID="hiddenId" runat="server" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="row" id="trChild" runat="server">
                            <div class="col-xs-1">
                                <label for="client1">Child info</label>
                            </div>
                            <uc:customer ID="customerChild" runat="server"></uc:customer>
                            <div>
                                <asp:Repeater ID="rptServicesChild" runat="server">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkService" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            CssClass="checkbox" /><asp:HiddenField ID="hiddenServiceId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:HiddenField ID="hiddenId" runat="server" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="row" id="trBaby" runat="server">
                            <div class="col-xs-1">
                                <label for="client1">Baby info</label>
                            </div>
                            <uc:customer ID="customerBaby" runat="server"></uc:customer>
                            <div>
                                <asp:Repeater ID="rptServicesBaby" runat="server">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkService" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            CssClass="checkbox" /><asp:HiddenField ID="hiddenServiceId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:HiddenField ID="hiddenId" runat="server" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:PlaceHolder ID="plhAddRoom" runat="server">
        <div class="row">
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRoomTypes" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-10">
                <asp:Button ID="btnAddRoom" runat="server" OnClick="btnAddRoom_Click" Text="Add Room"
                    CssClass="btn btn-primary" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:HiddenField ID="ScreenCapture" runat="server"></asp:HiddenField>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= txtStartDate.ClientID%>").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput:false,
                scrollMonth:false
            });

            $("#<%= txtDeadline.ClientID%>").datetimepicker({
                format: 'd/m/Y H:i',
                scrollImput:false,
                scrollMonth:false
            });
        })
    </script>
    <script type="text/javascript">  
        $(function () {
            //workaround datetimepicker do not show date table when off mousedown and focusin and blank input value
            $("[data-control = 'datepicker']").each(function(i,e){
                if($(e).val()==""){
                    $(e).val("workaround");
                }
            })

            $("[data-control = 'datepicker']").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput:false,
                scrollMonth:false,
            })

            $("[data-control = 'datepicker']").each(function(i,e){
                if($(e).val()=="workaround"){
                    $(e).val("");
                }
            })

            $("[data-control = 'datepicker']").off("mousedown");
            $("[data-control = 'datepicker']").off("focusin");

            $(".fa-calendar").click(function(){
                $(this).siblings("input").datetimepicker("show");
            })
        });

    </script>

    <script>
        DropdownOptionSetVisible();
        $("#<%=ddlTrips.ClientID%>").change(DropdownOptionSetVisible);
        
        function DropdownOptionSetVisible(){
            $("#<%=ddlOptions.ClientID%>").hide();
            if($("#<%=ddlTrips.ClientID%> option:selected").attr("data-option-visible") == "true"){
                $("#<%=ddlOptions.ClientID%>").show();
            }
          
        }
    </script>

    <script>
        TACodeSetVisible();
        $("#<%=ddlAgencies.ClientID%>").change(TACodeSetVisible);

        function TACodeSetVisible(){
            $("#<%=txtAgencyCode.ClientID%>").hide();
            if(!$("#<%=ddlAgencies.ClientID%> option:selected").is($("#<%=ddlAgencies.ClientID%> option:first-child"))){
                $("#<%=txtAgencyCode.ClientID%>").show();
            }
        }
    </script>
    <script type="text/javascript">
        function toggleVisible(id) {
            item = document.getElementById(id);
            if (item.style.display == "") {
                item.style.display = "none";
            } else {
                item.style.display = "";
            }
        }
    </script>
    <script>
        function CheckVoucher() { 
            PopupCenter(url, 'Check Voucher', 400, 600);
        }
    </script>
    <script>
        function statusInforShow(){
            var txtCutOffDays = $("#<%= txtCutOffDays.ClientID%>");
            var txtDeadline = $("#<%= txtDeadline.ClientID%>");
            var txtCancelledReason = $("#<%= txtCancelledReason.ClientID%>");
            var selectedStatus =  $("#<%= ddlStatusType.ClientID%> option:selected");

            if (selectedStatus.html() === "Pending") {
                txtDeadline.show();
                txtCutOffDays.hide();
                txtCancelledReason.hide();
            }
            if (selectedStatus.html() === "CutOff") {
                txtDeadline.hide();
                txtCutOffDays.show();
                txtCancelledReason.hide();
            }
            if (selectedStatus.html() === "Cancelled") {
                txtDeadline.hide();
                txtCutOffDays.hide();
                txtCancelledReason.show();
            }
            if (selectedStatus.html() === "Approved") {
                txtDeadline.hide();
                txtCutOffDays.hide();
                txtCancelledReason.hide();
            }
        }
   
        statusInforShow();
        $("#<%= ddlStatusType.ClientID%>").change(statusInforShow);   
    </script>

    <script>
        function setPersonalInfomation(control, ui) {
            control.val(ui.item.Fullname);
            var divparentControl = control.parents(".row");
            if (ui.item.HasGenderValue === true) {
                if (ui.item.IsMale === false) {
                    divparentControl.find(".ddlGender").val("Female");
                } else {
                    divparentControl.find(".ddlGender").val("Male");
                }
            } else {
                divparentControl.find(".ddlGender").children(":first").prop("selected", "selected");
            }


            if (ui.item.HasBirthdayValue === true) {
                divparentControl.find(".txtBirthday").val(ui.item.Birthday);
            } else {
                divparentControl.find(".txtBirthday").val("");
            }

            if (ui.item.HasNationality === true) {
                divparentControl.find(".ddlNationality").val(ui.item.NationId);
            } else {
                divparentControl.find(".ddlNationality").children(":first").prop("selected", "selected");
            }

            divparentControl.find(".txtVisaNo").val(ui.item.VisaNo);
            divparentControl.find(".txtPassport").val(ui.item.Passport);

            if (ui.item.HasVisaExpiredValue === true) {
                divparentControl.find(".txtVisaExpired").val(ui.item.VisaExpired);
            }

            divparentControl.find(".txtNguyenQuan").val(ui.item.NguyenQuan);

            divparentControl.find(".chkVietKieu").children("input").prop("checked", "checked");

        }

    </script>
    <script>
        var fullNameArray = [];
        $(document).ready(function () {
            $.each($(".acomplete"), function (i, e) {
                if ($(e).val() === "") {
                    fullNameArray.push({ selected: false, originFullName: $(e).val(), control: $(e) });
                } else {
                    fullNameArray.push({ selected: true, originFullName: $(e).val(), control: $(e) });
                }
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $.each($(".acomplete"), function (i, e) {
                $(e).autocomplete({
                    source: "SearchCustomer.aspx?NodeId=1&SectionId=15",
                    select: function (event, ui) {
                        $.each(fullNameArray, function (index, element) {
                            if ($(e).is(element["control"])) {
                                element["originFullName"] = ui.item.Fullname;
                                element["selected"] = true;
                            }
                        });
                        setPersonalInfomation($(this), ui);
                        return false;
                    }
                }).autocomplete("instance")._renderItem = function (ul, item) {
                    var itemElement = "<a>" + item.Fullname + "<br>";
                    if (item.HasGenderValue === true) {
                        if (item.IsMale === false) {
                            itemElement = itemElement + "<b>Gender : Female</b> ";
                        } else {
                            itemElement = itemElement + "<b>Gender : Male</b> ";
                        }
                    }

                    if (item.HasBirthdayValue === true) {
                        itemElement = itemElement + "<b>Birthday : " + item.Birthday + "</b> ";
                    }

                    if (item.HasNationality === true) {
                        itemElement = itemElement + "<b>Nationality : " + item.Nationality + "</b> ";
                    }
                    itemElement = itemElement + "</a>";
                    return $("<li>").append(itemElement).appendTo(ul);
                };
            });
        });
    </script>
    <script>
        function resetPersonInformation(control) {
            control.siblings(".hiddenId").val("");
        }
    </script>
    <script>
        $(function () {
            $.each($(".acomplete"), function (i, e) {
                $(e).keyup(function () {
                    $.each(fullNameArray, function (index, element) {
                        if ($(e).is(element["control"])) {
                            if (element["selected"] === true) {
                                if ($(e).val() !== element["originFullName"]) {
                                    resetPersonInformation($(e));
                                    element["selected"] = false;
                                }
                            }
                        }
                    });
                });
            });
        })
    </script>
    <script>
        //change all selector nationality follow first selector
        $(function () {
            $(".ddlNationality:first").change(function () {
                $(".ddlNationality").val($(".ddlNationality:first").val());
            });
        });
    </script>
    <script>
        $("#sendemail").colorbox({
            iframe: true,
            width: 1200,
            height: 600,
        });

        if(GetParameterValues("confirm") == 1){
            $("#sendemail").colorbox({
                iframe: true,
                width: 1200,
                height: 600,
                open:true
            });    
        }
    </script>
    <script>
        $("#roomorganizer").colorbox({
            iframe: true,
            width: 1200,
            height: 600,
        });
    </script>
    <script>
        AgencyDropdownlistFillTitle();
        $("#<%=ddlAgencies.ClientID%>").change(function(){
            AgencyDropdownlistFillTitle();
        }); 
        function AgencyDropdownlistFillTitle(){
            $("#<%=ddlAgencies.ClientID%>").attr("title", $("#<%=ddlAgencies.ClientID%> option:selected").html());
        }
    </script>
    <script>
        $("#checkvoucher").click(function(){
            var code = document.getElementById('<%= txtAllVoucher.ClientID %>').value;
            var url = 'CheckVoucher.aspx?NodeId=1&SectionId=15&code=' + code + '&bookingid=' + <%= Request.QueryString["bi"] %>;
            $.colorbox({
                href:url,
                iframe:true,
                width:1200,
                height:600,
            })
        });
    </script>
    <script type="text/javascript">
        var screenCapture = {
            capture : function(){
             <%--   var allow = 0;
                $("#screencapture_status").html("Đang chụp ảnh màn hình");
                $("#screencapture_status").removeClass("hidden").addClass("show");
                html2canvas(document.body, {
                    onrendered: function(canvas) {                         
                        $("#<%= ScreenCapture.ClientID%>").val(canvas.toDataURL().replace('data:image/png;base64,', ''))
                        allow = 1;
                        $("#screencapture_status").html("Đã chụp xong. Tiến hành submit ");
                    }
                });  
                    
                var i = setInterval(function(){
                    if(allow === 1){--%>
                $("#<%= button1.ClientID%>").click();
                //        clearInterval(i);
                //    }
                //},1)          
            }
        }
    </script>
</asp:Content>
