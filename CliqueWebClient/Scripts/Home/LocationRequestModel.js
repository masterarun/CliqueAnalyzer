function LocationRequestModel() {
    debugger;
    var self = this;

    self.address = ko.observable();
    self.street = ko.observable();
    self.city = ko.observable();
    self.state = ko.observable();
    self.id = ko.observable();

    self.postalCode = ko.observable();

    self.latitude = ko.observable();
    self.longitude = ko.observable();

    self.postalCode.subscribe(function () {
        debugger;
        if (self.postalCode().trim() == "") {
            self.latitude("");
            self.longitude("");
            return;
        }
        self.GetGeoLocation();
    });

    self.GetGeoLocation = function () {
        debugger;
        var geocoder = new google.maps.Geocoder();
        var address = self.postalCode().trim();

        geocoder.geocode({ 'address': address }, function (results, status) {
            debugger;
            if (status == google.maps.GeocoderStatus.OK) {
                self.latitude(results[0].geometry.location.lat());
                self.longitude(results[0].geometry.location.lng());
            } else {
                alert("Geolocation api failed.");
            }
        });
    };




}