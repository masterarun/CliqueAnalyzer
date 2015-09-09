﻿ko.bindingHandlers.showModal = {
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
    var self = this;

    self.hashTagRequestList = ko.observableArray([]);

    self.displayAddHashTagModal = ko.observable(false);

    self.hashTagRequestModel = ko.observable(new HashTagRequestModel());

    self.showTagRequestModal = function (item) {
        debugger;
        
        self.displayAddHashTagModal(true);
        

    };

    self.hideTagRequestModal = function () {
        self.displayAddHashTagModal(false);
        

    };



    self.getHashTagRequest = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetAllHashTag"
        }).done(function (res) {
            debugger;
            if (self.table) {
                self.table.clear();
                self.table.destroy();
            }
            self.hashTagRequestList(res);
            self.table = $("#tagTable").DataTable({ responsive: true });
            
        }).error(function (ex) {
            debugger;
            alert("Error");
        });



    };



    debugger;

    self.getHashTagRequest();
    self.table = "";
   

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
    
};


