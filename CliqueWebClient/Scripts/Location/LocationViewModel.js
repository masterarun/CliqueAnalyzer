var LocationViewModel = function () {
    debugger;
    $("#menunav").find(".active").removeClass("active");
    $("#menunav").find("a")[0].className = "active";
    var self = this;
    
    self.tweetList = ko.observableArray([]);

    self.locationRequestModel = ko.observable(new LocationRequestModel());

    
    self.initialize = function (res) {
        // baseUrl = "http://localhost:4620/";
        // Create the map.
        var map = new google.maps.Map(document.getElementById('locationmap'), {
            zoom: 18,
            center: { lat: Number(res.Latitude), lng: Number(res.Longitude) },
            mapTypeId: google.maps.MapTypeId.HYBRID,
            heading: 30,
            tilt: 30,
            //labels:true,
            //setOptions:{styles: styles},
        });

        $("#Details").removeClass("hide").addClass("show");

    };

    //self.initialize = function (res) {
    //    debugger;
    //    var geocoder = new google.maps.Geocoder();
        
    //    geocoder.geocode({ 'address': address }, function (results, status) {
    //        if (status == google.maps.GeocoderStatus.OK) {
    //            lat = (results[0].geometry.location.lat());
    //            long = (results[0].geometry.location.lng());

    //            var map = new google.maps.Map(document.getElementById('locationmap'), {
    //                zoom: 18,
    //                center: { lat: lat, lng: long },
    //                mapTypeId: google.maps.MapTypeId.HYBRID,
    //                heading: 30,
    //                tilt: 30,
    //            });

    //        } else {
    //            alert("Geolocation api failed.");
    //        }
    //    });
        
    //};

    self.getLocationTweet = function () {
        debugger;
        $("#loadingImage").addClass("show");
        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetLocationRequestDetails?pinCode=" + self.locationRequestModel().pincode() //+ "&location=" + self.hashTagRequestModel().location() //+ "&fromDate=" + self.hashTagRequestModel().fromDate() + "&toDate=" + self.hashTagRequestModel().toDate()
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
            $("#loadingImage").removeClass("show").addClass("hide");
            //$('#MapDetails').addClass('in');
        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };
    self.table = "";
};