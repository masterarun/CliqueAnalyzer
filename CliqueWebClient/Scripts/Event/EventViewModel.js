
var EventViewModel = function () {
    debugger;
    $("#menunav").find(".active").removeClass("active");
    $("#menunav").find("a")[2].className = "active";
    var self = this;

    self.eventList = ko.observableArray([]);
    self.pincode = ko.observable("");
    self.city = ko.observable("");

    self.WaitForIFrame = function () {        
        var item = $("#myIFrame").contents().find(".navbar-fixed-top");
        if (item.length == 0) {
           setTimeout(self.WaitForIFrame(), 200);
        } else {
            self.done();
        }
    };
    self.done = function () {
        var item = $("#myIFrame").contents().find(".navbar-fixed-top");
        item.html("<div class=\"row\">   <div class=\"col-md-12\"> <h3 class=\"text-center\" style=\"color:orange\": White>Location Events</h3> </div></div>");


        var filterItem = $("#myIFrame").contents().find("#geoFilterContainer");
        // filterItem.hide();
    };

    self.initialize = function () {

        self.WaitForIFrame();

    };

    self.getLocationEvent = function () {
        debugger;

        $("#myIFrame").attr('src', 'http://cliqueanalyzer.azurewebsites.net/proxy/' + self.city());
        
      //  $("#loadingImage").addClass("show");
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetEventRequestDetails?pinCode=" + self.pincode() //+ "&location=" + self.hashTagRequestModel().location() //+ "&fromDate=" + self.hashTagRequestModel().fromDate() + "&toDate=" + self.hashTagRequestModel().toDate()
        }).done(function (res) {
            debugger;
            self.initialize();
            if (self.table) {
                self.table.clear();
                self.table.destroy();
            }
            self.eventList(res.CliqueEventList);
            
            self.initialize(res);

            self.table = $("#eventsTable").DataTable({ responsive: true });
          //  $("#loadingImage").removeClass("show").addClass("hide");
            //$('#MapDetails').addClass('in');
        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };
    self.table = "";
    debugger;

   // self.initialize();



};

