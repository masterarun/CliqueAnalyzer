var LocationViewModel = function () {
    debugger;
    $("#menunav").find(".active").removeClass("active");
    $("#menunav").find("a")[1].className = "active";
    var self = this;
    
    self.tweetList = ko.observableArray([]);

    self.locationRequestModel = ko.observable(new LocationRequestModel());

    self.initialize = function (res) {

        var geocoder = new google.maps.Geocoder();
        var address = $("#pinCode").val();
        var lat = "";
        var long = "";
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                lat = (results[0].geometry.location.lat());
                long = (results[0].geometry.location.lng());

                var map = new google.maps.Map(document.getElementById('locationmap'), {
                    zoom: 18,
                    center: { lat: lat, lng: long },
                    mapTypeId: google.maps.MapTypeId.HYBRID,
                    heading: 30,
                    tilt: 30,
                });

            } else {
                alert("Geolocation api failed.");
            }
        });
        $("#Details").removeClass("hide").addClass("show");
    };

    self.getLocationTweet = function () {
        debugger;
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetLocationRequestDetails?pinCode=" + self.locationRequestModel().postalCode() //+ "&location=" + self.hashTagRequestModel().location() //+ "&fromDate=" + self.hashTagRequestModel().fromDate() + "&toDate=" + self.hashTagRequestModel().toDate()
        }).done(function (res) {
            debugger;
            if (self.table) {
                self.table.clear();
                self.table.destroy();
            }
            self.tweetList(res.CliqueTweetList);
            self.initialize(res);

            //self.table = $("#tweetTable").DataTable({ responsive: true });
            $("#Details").removeClass('hide').addClass('show');
            //$("#loadingImage").removeClass("show").addClass("hide");
            //$('#MapDetails').addClass('in');
        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };
    self.table = "";
};