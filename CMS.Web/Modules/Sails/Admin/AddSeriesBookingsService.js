moduleAddSeriesBookings.service("seriesService", function () {
    var series = null;

    var setSeries = function (obj) {
        series = obj;
    }

    var getSeries = function (obj) {
        return series;
    }

    return {
        setSeries: setSeries,
        getSeries: getSeries
    }
})

moduleAddSeriesBookings.service("listBookedService", function () {
    var listBooked = [];

    var setListBooked = function (obj) {
        listBooked = obj;
    }

    var getListBooked = function (obj) {
        return listBooked;
    }

    return {
        setListBooked: setListBooked,
        getListBooked: getListBooked
    }
})