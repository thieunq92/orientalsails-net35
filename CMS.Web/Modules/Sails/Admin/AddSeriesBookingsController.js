moduleAddSeriesBookings.controller("seriesController", ["$rootScope", "$scope", "$http", "seriesService", function ($rootScope, $scope, $http, seriesService) {
    $scope.init = function () {
        $scope.listAgencyContact = [];
        $scope.displaySelectBookerLoadingIcon = false;
        $scope.selectedAgencyContact = null;
        $scope.seriesCode = null;
        $scope.cutoffDate = null;
        $scope.noOfDays = null;
        $scope.displayCreateSeriesLoadingIcon = false;
        $scope.displaySuccess = false;
        $scope.displaySeriesInfoPanel = true;
        $scope.displaySeriesInfoSuccessPanel = false;
        $scope.series = null;
    }

    $scope.agencyContactGetAllByAgency = function () {
        $scope.displaySelectBookerLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/AgencyContactGetAllByAgency",
            data: { "ai": $("#ctl00_AdminContent_agencySelector").val() }
        }).then(function (response) {
            $scope.listAgencyContact = JSON.parse(response.data.d);
            $scope.displaySelectBookerLoadingIcon = false;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.displaySelectBookerLoadingIcon = false;
        })
    }

    $scope.createSeries = function () {
        $scope.displayCreateSeriesLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CreateSeries",
            data: {
                "ai": $("#ctl00_AdminContent_agencySelector").val(),
                "bi": $scope.selectedAgencyContact,
                "sc": $scope.seriesCode,
                "cd": $scope.cutoffDate,
                "nod": $scope.noOfDays,
            }
        }).then(function (response) {
            seriesService.setSeries(JSON.parse(response.data.d));
            $(".series.alert-success").show().append("<strong>Success!</strong> Tạo series thành công")
            $scope.displayCreateSeriesLoadingIcon = false;
            $scope.displaySeriesInfoPanel = false;
            $scope.displaySeriesInfoSuccessPanel = true;
            $rootScope.displayBookingPanel = true;
            $scope.series = seriesService.getSeries();
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.displayCreateSeriesLoadingIcon = false;
        })
    }
}]);

moduleAddSeriesBookings.controller("bookingController", ["$rootScope", "$scope", "$http", "seriesService", "listBookedService", function ($rootScope, $scope, $http, seriesService, listBookedService) {
    $rootScope.displayBookingPanel = false;
    $rootScope.NumberOfBooking = 1;
    $scope.init = function () {
        $scope.selectedTrip = null;
        $scope.startDate = null;
        $scope.checkRoomInit();
        $scope.getAvaiableRoomInit();
        $scope.saveBookingInit();
    }

    $scope.tripGetAll = function () {
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/TripGetAll",
            data: {}
        }).then(function (response) {
            $scope.listTrip = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }
    $scope.tripGetAll();

    $scope.checkRoomInit = function () {
        $scope.displayCheckLoadingIcon = false;
        $scope.listCheckRoomResult = [];
        $scope.displayCheckRoomPanel = false;
    }
    $scope.checkRoom = function () {
        $scope.displayCheckLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CheckRoom",
            data: {
                "sd": $scope.startDate,
                "ti": $scope.selectedTrip,
                "SectionId": 15,
                "NodeId": 1,
            }
        }).then(function (response) {
            $scope.listCheckRoomResult = JSON.parse(response.data.d);
            for (i = 0; i < $scope.listCheckRoomResult.length; i++) {
                $scope.listCheckRoomResult[i]["style"] = "success";
                if ($scope.listCheckRoomResult[i].NoOfRoomAvaiable == 0)
                    $scope.listCheckRoomResult[i]["style"] = "danger";
            }
            $scope.displayCheckLoadingIcon = false;
            $scope.displayCheckRoomPanel = true;
            $scope.getAvaiableRoomInit();
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.displayCheckLoadingIcon = false;
        })
    }

    $scope.getAvaiableRoomInit = function () {
        $scope.selectedCruise = null;
        $scope.selectedCruiseName = null;
        $scope.listAvaiableRoom = [];
        $scope.displaySelectRoomPanel = false;
    }
    $scope.getAvaiableRoom = function (cruiseId, cruiseName) {
        $scope.selectedCruise = cruiseId;
        $scope.selectedCruiseName = cruiseName;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/GetAvaiableRoom",
            data: {
                "ci": $scope.selectedCruise,
                "sd": $scope.startDate,
                "ti": $scope.selectedTrip,
                "SectionId": 15,
                "NodeId": 1,
            }
        }).then(function (response) {
            var listAvaiableRoom = JSON.parse(response.data.d)
            $scope.listAvaiableRoom = listAvaiableRoom;
            for (i = 0; i < listAvaiableRoom.length; i++) {
                $scope.listAvaiableRoom[i]["selectedRoom"] = null;
                $scope.listAvaiableRoom[i]["selectedChild"] = null;
                $scope.listAvaiableRoom[i]["selectedBaby"] = null;
            }
            $scope.displaySelectRoomPanel = true;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }

    $scope.saveBookingInit = function () {
        $scope.displaySaveBookingLoadingIcon = false;
    }
    $scope.saveBooking = function () {
        $scope.displaySaveBookingLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/SaveBooking",
            data: {
                "listAvaiableRoomDTO": $scope.listAvaiableRoom,
                "si": seriesService.getSeries().Id,
                "ci": $scope.selectedCruise,
                "sd": $scope.startDate,
                "ti": $scope.selectedTrip,
            }
        }).then(function (response) {
            $scope.displaySaveBookingLoadingIcon = false;
            $(".booking.alert-success").empty().show().append("<strong>Success!</strong> Tạo booking thành công")
            $scope.init();

            var booking = JSON.parse(response.data.d)
            var listBooked = listBookedService.getListBooked();
            listBooked.push(booking);
            listBookedService.setListBooked(listBooked)
            $rootScope.listBooked = listBookedService.getListBooked();
            $rootScope.NumberOfBooking = $rootScope.listBooked.length + 1;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.displaySaveBookingLoadingIcon = false;
        });
    }

    $scope.range = function (count) {
        var items = [];

        for (var i = 1; i <= count; i++) {
            items.push(i)
        }
        return items;
    }
}]);

moduleAddSeriesBookings.controller("bookedController", ["$rootScope", "$scope", "listBookedService", function ($rootScope, $scope, listBookedService) {
}])

