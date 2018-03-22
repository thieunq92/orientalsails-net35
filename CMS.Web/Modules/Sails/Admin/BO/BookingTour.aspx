<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="BookingOnline.Master" CodeBehind="BookingTour.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BO.BookingTour" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <article id="booking" class="col-md-7 col-lg-8 col-md-push-2 content post-1847 page type-page status-publish hentry">
        <header class="header">
            <h1 class="title"><span class="line"></span>Booking</h1>
        </header>
        <section class="post-content clearfix">
            <h4>Trip Infomation</h4>
            <div class="row">
                <div class="col-xs-12 col-md-4">
                    <label>Departure Date</label>
                    <input class="form-control hasDatepicker" name="StartDate" id="StartDate" placeholder="Month/Date/Year" data-required="" value="" type="text" />
                    <span class="message"></span>
                </div>
                <div class="col-xs-12 col-md-8">
                    <label>Itinerary</label>
                    <select name="Itinerary" class="form-control" data-required="" id="Trip">
                        <option value="2">Oriental Sails 2 days 1 night</option>
                    </select>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="panel-checkingcruise" style="display: none">
                    <em>Click cruise name to select cruise</em>
                    <table id="t-cruise" class="table table-bordered">
                        <tr>
                            <th width="20%">Cruise Name</th>
                            <th width="20%">Avaiable Room</th>
                            <th></th>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="panel-pending" style="display: none">
                    <table id="t-pending" class="table table-bordered">
                        <tr>
                            <th width="20%">Cruise Name</th>
                            <th width="20%">Pending Room</th>
                            <th></th>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="panel-chooseroom" style="display: none">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="panel-pickoptionroom" style="display: none">
                </div>
            </div>
            <input type="hidden" name="Cruise" id="cruise" />
            <div id="roomvalue">
            </div>
            <div id="panel-infomation">
                <h4>YOUR INFOMATION</h4>
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <label>Your Name</label>
                        <input class="name form-control" name="YourName" data-required="" type="text" placeholder="Name" />
                        <span class="message"></span>
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <label>Your Email</label>
                        <input class="email form-control" name="YourEmail" data-required="" type="email" placeholder="Email" />
                        <span class="message"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <label>Your Phone</label>
                        <input name="YourPhone" class="form-control" data-required="" type="text" placeholder="Phone"></input>
                        <span class="message"></span>
                    </div>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnBook" runat="server" class="btn view-detail text-center text-uppercase" Text="Book Your Trip" OnClick="btnBook_Click" />
                </div>
            </div>
            <div id="sendrequest" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Send Request Pending Room</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Departure Date:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <span id="requestDD"></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Itinerary:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <span id="requestI"></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Cruise:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <span id="requestC"></span>
                                        </div>
                                    </div>
                                    <input id="requestCruiseId" type="hidden" />
                                    <div class="form-group">
                                        <label for="name">Your Name:</label>
                                        <input type="text" id="name" placeholder="Name" class="form-control" name="yn" />
                                    </div>
                                    <div class="form-group">
                                        <label for="email">Your Email:</label>
                                        <input type="text" id="email" placeholder="Email" class="form-control" name="ye" />
                                    </div>
                                    <div class="form-group">
                                        <label for="phone">Your Phone:</label>
                                        <input type="text" id="phone" placeholder="Phone" class="form-control" name="yp" />
                                    </div>
                                    <div class="form-group">
                                        <label for="request">Your Room Request:</label>
                                        <textarea id="request" placeholder="Room Request" class="form-control" name="yr"></textarea>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-4 col-md-8">
                                            <button type="button" class="btn btn-success" id="btn-sendrequest">Send Request</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="sendrequestinform" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <span id="contentinform"></span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Ok</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </article>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Scripts">
    <script src="/scripts/jquery.datetimepicker.full.min.js"></script>
    <script>
        $(document).ready(function () {
            function CheckRoom() {
                $.ajax({
                    type: "POST",
                    url: "BookingTour.aspx/CheckAvaiable",
                    data: '{sd: "' + $("#StartDate").val() + '",tid:"' + $("#Trip").val() + '", sid:15 }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        $("#panle-checkingcruise").hide();
                        $("#panel-pending").hide();
                        alert("Error.Try Again");
                    }
                });
            };

            function OnSuccess(response) {
                try {
                    $("#panel-checkingcruise").show();
                    $("#panel-pending").hide();
                    $("#panel-chooseroom").hide();
                    $(".detail").remove();
                    var data = $.parseJSON(response.d);
                    $(data).each(function (i, e) {
                        var bgcolor = "";

                        if (e.AvaiableRoomQuantity > 0) {
                            bgcolor = "#92D050";
                        }

                        if (e.AvaiableRoomQuantity == 0) {
                            bgcolor = "#ff7f7f";
                        }

                        var cruiseDetails =
                            "<tr class='detail' style='background-color:" + bgcolor + "'>" +
                            "<td><a href = 'javascript:void(0)' class = 'a-cruisename' data-cruiseid='" +
                            e.JCruise.CruiseId + "' >" + e.JCruise.Name + "</a>" +
                            "</td>" +
                            "<td>" + e.AvaiableRoomQuantity + "</td>" +
                            "<td>" + e.AvaiableRoomDetail + "</td>" +
                            "</tr>";

                        $("#t-cruise").append(cruiseDetails);

                        if (e.PendingRoomQuantity > 0) {
                            $("#panel-pending").show();
                            var pendingDetails =
                                "<tr class = 'detail' style='background-color:" + bgcolor + "'>" +
                                    "<td>" + e.JCruise.Name + "</td>" +
                                    "<td>" + e.PendingRoomQuantity + "</td>" +
                                    "<td>" + e.PendingRoomDetail + "</td>" +
                                    "<td><a href='javascript:void(0)' data-cruisename = '" + e.JCruise.Name + "' class='a-sendrequest' data-cruiseid = '" + e.JCruise.CruiseId + "' >Send Request</a></td>"
                            "</tr>";
                            $("#t-pending").append(pendingDetails);
                        }
                    });

                    HandlerClickCruiseName(data);

                    HandlerSendRequestPendingRoom();

                } catch (err) {
                    $("#panel-checkingcruise").hide();
                    $("#panel-pending").hide();
                    alert("Error.Try Again");
                }
            };

            $("#StartDate").datetimepicker({
                timepicker: false,
                format: "m/d/Y",
                minDate: new Date(),
                onChangeDateTime: function (current_time, $input) {
                    $("#StartDate").blur();
                    CheckRoom();
                    $("#roomvalue").empty();
                },
                scrollInput: false,
            })

            $(document).ajaxStart(function () {
                $(".overlay").show();
            }).ajaxStop(function () {
                $(".overlay").hide();
            });

            function HandlerClickCruiseName(data) {
                $(".a-cruisename").on("click", function () {
                    var choosenCruiseId = $(this).attr("data-cruiseid");
                    var lavaiableRoom = null;
                    var cruiseName = null;
                    $(data).each(function (i, e) {
                        var avaiableRoomInCruise = e;
                        if (avaiableRoomInCruise.JCruise.CruiseId == choosenCruiseId) {
                            lavaiableRoom = avaiableRoomInCruise.LJAvaiableRoom;
                            cruiseName = avaiableRoomInCruise.JCruise.Name;
                        }

                    })
                    generateChooseRoom(lavaiableRoom, cruiseName);
                    $("#cruise").val(choosenCruiseId);
                    $("#roomvalue").empty();
                });
            }

            function generateChooseRoom(lavaiableRoom, cruiseName) {
                $("#panel-chooseroom").empty();
                $("#panel-chooseroom").show();
                $("#panel-chooseroom").append("<em>You have selected cruise <b>" + cruiseName + "</b></em><br/>");
                $(lavaiableRoom).each(function (i, e) {
                    var avaiableRoom = e;

                    var htmlSelectRoom =
                        "<select class='form-control roomcontrol' data-roomclass='" + avaiableRoom.JRoomClass.RoomClassId + "' data-roomType='" + avaiableRoom.JRoomType.RoomTypeId +
                        "' data-id ='" + i +
                        "' data-roomclassname = '" + avaiableRoom.JRoomClass.Name + "' data-roomtypename = '" + avaiableRoom.JRoomType.Name + "'>";

                    for (var i = 0 ; i < avaiableRoom.LJRoom.length + 1; i++) {
                        htmlSelectRoom = htmlSelectRoom +
                            "<option value = '" + i + "'>" + i + " room(s)" + "</option>";
                    }
                    htmlSelectRoom = htmlSelectRoom + "</select>"
                    var htmlPanelRoom =
                    "<div class = 'row'>" +
                    "<div class='col-md-3'>" + avaiableRoom.JRoomClass.Name + avaiableRoom.JRoomType.Name + "</div>" +
                        "<div class='col-md-3' id='panel-selectroom'>" +
                        htmlSelectRoom +
                        "</div>" +
                        "<div class='col-md-2'>" +
                        "Single Room" +
                        "</div>" +
                        "<div class='col-md-1' data-name='panel-selectsingleroom' data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "' data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "'>" +
                        "</div>" +
                        "<div class='col-md-2'>" +
                        "Children" +
                        "</div>" +
                        "<div class='col-md-1' data-name='panel-selectchildren' data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "' data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "'>" +
                        "</div>" +
                        "</div>"
                    $("#panel-chooseroom").append(htmlPanelRoom);
                    GenerateSelectSingleRoom(avaiableRoom);
                    GenerateSelectChildren(avaiableRoom);
                    GeneratePanelRoomClassType(avaiableRoom.JRoomClass.RoomClassId, avaiableRoom.JRoomType.RoomTypeId);
                });
                HandlerSelectRoomControl();
            }

            function GenerateSelectSingleRoom(avaiableRoom) {
                var htmlSelectSingleRoom = "<select data-name='selectsingleroom' data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "' data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "'></select>"
                $("div[data-name='panel-selectsingleroom'][data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "'][data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "']").append(htmlSelectSingleRoom);

                var htmlOptionSingleRoom = ""
                for (var i = 0 ; i < avaiableRoom.LJRoom.length + 1; i++) {
                    htmlOptionSingleRoom = htmlOptionSingleRoom +
                        "<option value = '" + i + "'>" + i + "</option>";
                }

                $("select[data-name='selectsingleroom'][data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "'][data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "']").append(htmlOptionSingleRoom);
                HandlerSelectSingleRoom(avaiableRoom.JRoomClass.RoomClassId, avaiableRoom.JRoomType.RoomTypeId);
            }

            function GenerateSelectChildren(avaiableRoom) {
                var htmlSelectChildren = "<select data-name='selectchildren' data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "' data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "'></select>"
                $("div[data-name='panel-selectchildren'][data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "'][data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "']").append(htmlSelectChildren);

                var htmlOptionChildren = ""
                for (var i = 0 ; i < avaiableRoom.LJRoom.length + 1; i++) {
                    htmlOptionChildren = htmlOptionChildren +
                        "<option value = '" + i + "'>" + i + "</option>";
                }

                $("select[data-name='selectchildren'][data-roomclassid='" + avaiableRoom.JRoomClass.RoomClassId + "'][data-roomtypeid='" + avaiableRoom.JRoomType.RoomTypeId + "']").append(htmlOptionChildren);
                HandlerSelectChildren(avaiableRoom.JRoomClass.RoomClassId, avaiableRoom.JRoomType.RoomTypeId);
            }

            function HandlerSelectSingleRoom(roomClassId, roomTypeId) {
                $("select[data-name='selectsingleroom'][data-roomclassid='" + roomClassId + "'][data-roomtypeid='" + roomTypeId + "']").on("change", function () {
                    htmlSelectSingleRoomValue = "<input type='hidden' name = 'singleroom' data-name='selectsingleroomvalue' data-roomclass = '" + roomClassId + "' data-roomtypeid = '" + roomTypeId + "' value='[" + roomClassId + "," + roomTypeId + "," + $(this).val() + "]'>";
                    $("#roomvalue").append(htmlSelectSingleRoomValue);
                });
            }

            function HandlerSelectChildren(roomClassId, roomTypeId) {
                $("select[data-name='selectchildren'][data-roomclassid='" + roomClassId + "'][data-roomtypeid='" + roomTypeId + "']").on("change", function () {
                    htmlSelectChildrenValue = "<input type='hidden' name = 'children' data-name='selectchildrenvalue' data-roomclass = '" + roomClassId + "' data-roomtypeid = '" + roomTypeId + "' value='[" + roomClassId + "," + roomTypeId + "," + $(this).val() + "]'>";
                    $("#roomvalue").append(htmlSelectChildrenValue);
                });
            }

            var roomNumber = 0;
            function HandlerSelectRoomControl() {
                $(".roomcontrol").on("change", function () {
                    $("#roomvalue input[data-id='" + $(this).attr("data-id") + "']").remove();
                    var roomvalue = "";
                    roomvalue = roomvalue +
                        "<input type='hidden' data-id ='" + $(this).attr("data-id") +
                        "' name='rooms' value='[" + $(this).attr("data-roomclass") + "," + $(this).attr("data-roomtype") + "," + $(this).val() + "]'/>";
                    $("#roomvalue").append(roomvalue);

                    var panelRoomClassType = $("div[data-name = 'panel-roomclasstype'][data-roomclass='" + $(this).attr("data-roomclass") + "'][data-roomtype = '" + $(this).attr("data-roomtype") + "']");
                    var selectedRoom = $(this).val();
                    var currentRoom = $("div[data-name='panel-room'][data-roomclass = '" + $(this).attr("data-roomclass") + "'][data-roomtype='" + $(this).attr("data-roomtype") + "']").length;
                    var roomNeedAdd = selectedRoom - currentRoom;
                    if (roomNeedAdd > 0) {
                        for (var i = 0; i < roomNeedAdd; i++) {
                            var htmlPickOptionRoom =
                                "<div class='row' data-name = 'panel-room' data-roomclass='" + $(this).attr("data-roomclass") + "' data-roomtype='" + $(this).attr("data-roomtype") + "'>" +
                                "<div class='col-md-4' data-roomclass='" + $(this).attr("data-roomclass") + "' data-roomtype ='" + $(this).attr("data-roomtype") + "' >" + "<label>" + "Room" + (roomNumber + 1) + ": " + $(this).attr("data-roomclassname") + $(this).attr("data-roomtypename") + "</label>" + "</div>" +
                                "<div class='col-md-3'>" +
                                "<input type='checkbox' id='singleroom" + roomNumber + "' data-name='singleroom' data-roomid='" + roomNumber + "'></input><label for='singleroom" + roomNumber + "' style='font-weight:normal'>Single Room" +
                                "</label></div>" +
                                "<div class='col-md-5'><input type='checkbox' id='addchildren" + roomNumber + "' data-name='addchildren' data-roomid='" + roomNumber + "'></input><label for='addchildren" + roomNumber + "' style='font-weight:normal'>Add Child(6 - 11 years old)</div>" +
                                "<div class='col-md-11'>Number of passengers in the room : <span data-name='adult-passengers' data-roomid='" + roomNumber + "'>2 adults</span>&nbsp<span data-name='child-passenger' data-roomid='" + roomNumber + "'></span></div>" +
                                "<div class='col-md-12'>Room Price : <span data-name='price'>12$</span></div>"
                            "</div>";
                            panelRoomClassType.children("div").first().append(htmlPickOptionRoom);
                            roomNumber++;
                        }
                    }

                    if (roomNeedAdd < 0) {
                        var startRemoveIndex = selectedRoom;
                        $("div[data-name='panel-room'][data-roomclass='" + $(this).attr("data-roomclass") + "'][data-roomtype='" + $(this).attr("data-roomtype") + "']").slice(startRemoveIndex).remove();
                    }
                    HandlerSingleRoomCheckBox();
                    HandlerAddChildCheckBox();
                });
            }

            function HandlerSendRequestPendingRoom() {
                $(".a-sendrequest").on("click", function () {
                    $("#requestDD").empty().append($("#StartDate").val());
                    $("#requestI").empty().append($("#Trip").html());
                    $("#requestC").empty().append($(this).attr("data-cruisename"));
                    $("#sendrequest").modal("show");
                    $("#requestCruiseId").val($(this).attr("data-cruiseid"))
                    $("#sendrequest").show();

                });
            }

            $("#btn-sendrequest").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: "BookingTour.aspx/HandleRequestPendingRoom",
                    data: '{sid:15, sd: "' + $("#StartDate").val() + '",tid:"' + $("#Trip").val() + '",cid: "' + $("#requestCruiseId").val() + '",yn:"' + $("#name").val() + '",ye:"' + $("#email").val() + '",yp:"' + $("#phone").val() + '",yr:"' + $("#request").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#sendrequest").modal("hide");
                        $("#contentinform").empty().append("Success!");
                        $("#sendrequestinform").modal("show");
                    },
                    failure: function (response) {
                        $("#sendrequest").modal("hide");
                        $("#contentinform").empty().append("Failed! Try again");
                        $("#sendrequestinform").modal("show");
                    }
                })
            });

            function HandlerSingleRoomCheckBox() {
                $("input[data-name='singleroom']").on("change", function () {
                    if ($(this).is(":checked")) {
                        $("span[data-name='adult-passengers'][data-roomid='" + $(this).attr("data-roomid") + "']").empty().append("1 adult");
                    } else {
                        $("span[data-name='adult-passengers'][data-roomid='" + $(this).attr("data-roomid") + "']").empty().append("2 adults");
                    }
                });
            }

            function HandlerAddChildCheckBox() {
                $("input[data-name='addchildren']").on("change", function () {
                    if ($(this).is(":checked")) {
                        $("span[data-name='child-passenger'][data-roomid='" + $(this).attr("data-roomid") + "']").empty().append("1 child");
                    } else {
                        $("span[data-name='child-passenger'][data-roomid='" + $(this).attr("data-roomid") + "']").empty().append("");
                    }
                });
            }

            function GeneratePanelRoomClassType(roomClassId, roomTypeId) {
                var htmlPanelRoomClassType =
                    "<div class='row' data-name = 'panel-roomclasstype' data-roomclass='" + roomClassId + "' data-roomtype = '" + roomTypeId + "' >" +
                        "<div class='col-md-12'>" +
                        "</div>" +
                     "</div>";
                $("#panel-pickoptionroom").append(htmlPanelRoomClassType);
            }
        });
    </script>
</asp:Content>
