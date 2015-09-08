function HashTagRequestModel() {

    var self = this;

    self.tag = ko.observable();
    self.fromDate = ko.observable();
    self.toDate = ko.observable();
    self.id = ko.observable();
    
    self.location = ko.observable();

    self.latitude = ko.observable();
    self.longitude = ko.observable();

    self.location.subscribe(function () {

        if (self.location().trim() == "") {
            self.latitude("");
            self.longitude("");
            return;
        }
        self.GetGeoLocation();
    });

    self.GetGeoLocation = function () {
        debugger;
        var geocoder = new google.maps.Geocoder();
        var address = self.location().trim();

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                self.latitude(results[0].geometry.location.lat());
                self.longitude(results[0].geometry.location.lng());
            } else {
                alert("Geolocation api failed.");
            }
        });
    };




}