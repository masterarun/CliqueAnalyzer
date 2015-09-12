ko.bindingHandlers.showModal = {
    init: function (element, valueAccessor) {
        $(element).modal({ backdrop: 'static', keyboard: true, show: false });
    },
    update: function (element, valueAccessor) {

        var value = valueAccessor();
        if (ko.utils.unwrapObservable(value)) {
            $(element).modal('show');
            $("input", element).focus();
        }
        else {

            $(element).modal('hide');
        }
    }
};

var DashboardViewModel = function () {
    $("#menunav").find(".active").removeClass("active");
    $("#menunav").find("a")[2].className = "active";
    var self = this;

    self.hashTagRequestList = ko.observableArray([]);
    self.locationRequestList = ko.observableArray([]);
    self.displayAddHashTagModal = ko.observable(false);
    self.displayAddLocationModal = ko.observable(false);
    self.hashTagRequestModel = ko.observable(new HashTagRequestModel());
    self.locationRequestModel = ko.observable(new LocationRequestModel());
    self.showTagRequestModal = function (item) {
        debugger;
        self.displayAddHashTagModal(true);
    };
    self.hideTagRequestModal = function () {
        self.displayAddHashTagModal(false);
    };
    self.showLocationRequestModal = function (item) {
        debugger;
        self.displayAddLocationModal(true);
    };
    self.hideLocationRequestModal = function () {
        self.displayAddLocationModal(false);
    };
    self.getHashTagRequest = function () {
        debugger;
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetAllHashTag"
        }).done(function (res) {
            debugger;
            self.hashTagRequestList(res);
            $("#tagTable").DataTable({ responsive: true });
        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };
    debugger;
    self.getHashTagRequest();
    debugger;
    self.addHashTagRequest = function () {
        debugger;
        var item = ko.toJSON(self.hashTagRequestModel());
        debugger;
        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: baseUrl + "api/CliqueAPI/AddHashTagRequest"
        }).done(function (res) {
            debugger;
            self.hideTagRequestModal();
            self.getHashTagRequest();

        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };

    self.getLocationRequest = function () {
        debugger;
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetAllLocationRequest"
        }).done(function (res) {
            debugger;
            self.locationRequestList(res);
            $("#locationTable").DataTable({ responsive: true });
        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };

    self.getLocationRequest();

    self.addLocationRequest = function () {
        debugger;
        var item = ko.toJSON(self.locationRequestModel());
        debugger;
        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: baseUrl + "api/CliqueAPI/AddLocationRequest"
        }).done(function (res) {
            debugger;
            self.hideLocationRequestModal();
            self.getLocationRequest();

        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };

};


