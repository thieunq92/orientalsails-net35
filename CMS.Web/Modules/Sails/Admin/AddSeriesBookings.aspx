<%@ Page Title="Add Series Bookings" Language="C#" MasterPageFile="MO.Master"
    AutoEventWireup="true"
    CodeBehind="AddSeriesBookings.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.AddSeriesBookings" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="alert alert-success series" role="alert" style="display: none">
            </div>
            <div class="alert alert-info series" role="alert" style="display: none">
            </div>
            <div class="alert alert-warning series" role="alert" style="display: none">
            </div>
            <div class="alert alert-danger series" role="alert" style="display: none">
            </div>
        </div>
    </div>
    <div ng-controller="seriesController" ng-init="init()">
        <div class="seriesinfo-panel" ng-show="displaySeriesInfoPanel">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Agency
                    </div>
                    <div class="col-xs-3">
                        <input type="text" id="ctl00_AdminContent_agencySelectornameid" class="form-control" readonly placeholder="Click to select agency" ng-blur="agencyContactGetAllByAgency()" />
                        <input id="ctl00_AdminContent_agencySelector" type="hidden" />
                    </div>
                    <div class="col-xs-1">
                        Booker
                    </div>
                    <div class="col-xs-2">
                        <select class="form-control inline-block width90percent" ng-model="selectedAgencyContact" ng-options="item.Id as item.Name for item in listAgencyContact">
                            <option value="">--Select booker--</option>
                        </select>
                        <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="displaySelectBookerLoadingIcon"></i>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Series code
                    </div>
                    <div class="col-xs-3">
                        <input id="txtSeriesCode" ng-model="seriesCode" placeholder="Series code" class="form-control" />
                    </div>
                    <div class="col-xs-1">
                        Cut-off date
                    </div>
                    <div class="col-xs-3">
                        <input id="txtCutoffDate" ng-model="cutoffDate" placeholder="Cut-off date (dd/MM/yyyy)" class="form-control" />
                    </div>
                    <div class="col-xs-1">
                        No of days
                    </div>
                    <div class="col-xs-3">
                        <input id="txtNoOfDays" ng-model="noOfDays" placeholder="No of days" class="form-control" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12">
                        <button class="btn btn-primary" type="button" ng-click="createSeries()">Save Series</button>
                        <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="displayCreateSeriesLoadingIcon"></i>
                    </div>
                </div>
            </div>
        </div>

        <div class="seriesinfosuccess-panel" ng-show="displaySeriesInfoSuccessPanel">

            <div class="row">
                <div class="col-xs-1">
                    Agency
                </div>
                <div class="col-xs-4">
                    {{series.Agency.Name}}
                </div>
                <div class="col-xs-1">
                    Booker
                </div>
                <div class="col-xs-2">
                    {{series.Booker.Name}}
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1">
                    Series code
                </div>
                <div class="col-xs-2">
                    {{series.SeriesCode}}
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1">
                    Cut-off date
                </div>
                <div class="col-xs-4">
                    {{series.CutoffDate}}
                </div>
                <div class="col-xs-1">
                    No of days
                </div>
                <div class="col-xs-3">
                    {{series.NoOfDays}}
                </div>
            </div>
        </div>
    </div>
    <div class="mutation-observer">
        <div class="booked-panel" ng-controller="bookedController" ng-repeat="item in listBooked">
            <hr />
            <div class="row">
                <div class="col-xs-1">
                    <label>Booking {{$index + 1}}</label>
                </div>
                <div class="col-xs-1">
                    Booking Code
                </div>
                <div class="col-xs-1">
                    {{item.BookingId}}
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-1">
                    Start Date
                </div>
                <div class="col-xs-1">
                    {{item.StartDate}}
                </div>
                <div class="col-xs-1">
                    Trip
                </div>
                <div class="col-xs-4">
                    {{item.TripName}}
                </div>
                <div class="col-xs-1">
                    Cruise
                </div>
                <div class="col-xs-3">
                    {{item.CruiseName}}
                </div>
            </div>

            <div class="row">
                <div class="col-xs-1"></div>
                <div class="col-xs-1">No of room</div>
                <div class="col-xs-1">{{item.NoOfRoom}}</div>
                <div class="col-xs-1">Room</div>
                <div class="col-xs-2 room">
                    {{item.Room}}
                </div>

            </div>
            <div class="row">
                <div class="col-xs-1"></div>
                <div class="col-xs-1">No of pax</div>
                <div class="col-xs-1">{{item.NoOfPax}}</div>
                <div class="col-xs-1">Pax</div>
                <div class="col-xs-2  pax">
                    {{item.Pax}}
                </div>
            </div>
        </div>
    </div>
    <div class="booking-panel" ng-controller="bookingController" ng-init="init()" ng-show="displayBookingPanel">
        <hr />
        <div class="row">
            <div class="col-xs-12">
                <div class="alert alert-success booking" role="alert" style="display: none">
                </div>
                <div class="alert alert-info booking" role="alert" style="display: none">
                </div>
                <div class="alert alert-warning booking" role="alert" style="display: none">
                </div>
                <div class="alert alert-danger booking" role="alert" style="display: none">
                </div>
            </div>
        </div>
        <div class="booking-block">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        <label>Booking {{NumberOfBooking}}</label>
                    </div>
                    <div class="col-xs-1">
                        Start date
                    </div>
                    <div class="col-xs-3">
                        <input id="txtStartDate" ng-model="startDate" placeholder="Start date (dd/MM/yyyy)" class="form-control" />
                    </div>
                    <div class="col-xs-1">
                        Trip
                    </div>
                    <div class="col-xs-3" style="width: 30%">
                        <select class="form-control inline-block" ng-model="selectedTrip" ng-options="item.Id as item.Name for item in listTrip">
                            <option value="">--Select Trip--</option>
                        </select>
                    </div>
                    <div class="col-xs-1">
                        <button class="btn btn-primary" type="button" ng-click="checkRoom()">Check</button>
                        <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="displayCheckLoadingIcon"></i>
                    </div>
                </div>
            </div>
            <div class="row" ng-show="displayCheckRoomPanel">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover">
                        <tr class="active">
                            <th>Cruise</th>
                            <th>Số phòng trống</th>
                            <th>Trong đó</th>
                        </tr>
                        <tr ng-repeat="item in listCheckRoomResult" class="{{item.style}}">
                            <td><a href="javascript:void(0)" ng-click="getAvaiableRoom(item.Cruise.Id, item.Cruise.Name)">{{item.Cruise.Name}}</a></td>
                            <td>{{item.NoOfRoomAvaiable}}</td>
                            <td>{{item.DetailRooms}}</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="selectroom-panel" ng-show="displaySelectRoomPanel">
                <div class="row">
                    <div class="col-xs-12" style="margin-bottom: 10px">
                        <span>Chọn tàu <strong>{{selectedCruiseName}} </strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row" ng-repeat="item in listAvaiableRoom">
                        <div class="col-xs-2">
                            {{item.KindOfRoom}}
                        </div>
                        <div class="col-xs-1 custom1-col-xs-1">
                            <select class="form-control" ng-model="item.selectedRoom" ng-options="adult as (adult  + ' ' + 'room(s)') for adult in range(item.NoOfAdult)">
                                <option value="">0 room(s)</option>
                            </select>
                        </div>
                        <div class="col-xs-1 custom1-col-xs-1">
                            <select class="form-control" ng-model="item.selectedChild" ng-options="child as (child + ' ' + 'child(s)') for child in range(item.NoOfChild)">
                                <option value="">0 child(s)</option>
                            </select>
                        </div>
                        <div class="col-xs-1 custom1-col-xs-1">
                            <select class="form-control" ng-model="item.selectedBaby" ng-options="baby as (baby + ' ' + 'baby(s)') for baby in range(item.NoOfBaby)">
                                <option value="">0 baby(s)</option>
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 float-right">
                            <button class="btn btn-primary" type="button" ng-click="saveBooking()">Save Booking</button>
                            <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="displaySaveBookingLoadingIcon"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/addseriesbookingsservice.js"></script>
    <script type="text/javascript" src="/modules/sails/admin/addseriesbookingscontroller.js"></script>
    <script>
        $("#ctl00_AdminContent_agencySelectornameid").click(function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId=1&SectionId=15&clientid=ctl00_AdminContent_agencySelector', 'Agencyselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        })
    </script>

    <script>
        $(function () {
            $("#txtCutoffDate").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput: false,
                scrollMonth: false,
            })

            $("#txtStartDate").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput: false,
                scrollMonth: false,
            })
        });
    </script>
    <script>
        var target = document.querySelector(".mutation-observer"),
        observer = new MutationObserver(mutationCallback),
        config = { childList: true, }
        function mutationCallback(mutations) {
            mutations.forEach(function (mutation) {
                $(".pax").each(function (e, v) {
                    var parser = new DOMParser();
                    var dom = parser.parseFromString($(v).html(), "text/html")
                    $(v).empty().append(dom.body.textContent).removeClass("pax");
                });
                $(".room").each(function (e, v) {
                    var parser = new DOMParser();
                    var dom = parser.parseFromString($(v).html(), "text/html")
                    $(v).empty().append(dom.body.textContent).removeClass("room");
                });
            });
        }
        observer.observe(target, config);
    </script>
</asp:Content>
