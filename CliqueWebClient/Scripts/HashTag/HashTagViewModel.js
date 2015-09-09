
var HashTagViewModel = function () {
    var self = this;

    self.tweetList = ko.observableArray([]);    

    self.hashTagRequestModel = ko.observable(new HashTagRequestModel());

 
    self.initialize = function (res) {
       // baseUrl = "http://localhost:4620/";
        // Create the map.
        debugger;
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 12,
            center: { lat: Number(res.Latitude), lng: Number(res.Longitude) },
            mapTypeId: google.maps.MapTypeId.HYBRID,
            heading: 90,
            //tilt: 30,
            //labels:true,
            //setOptions:{styles: styles},
        });

        // Construct the circle for each value in citymap.
        // Note: We scale the area of the circle based on the population.

        // Add the circle for this city to the map.
        var iconBase = baseUrl + "/Content/Images/log1.png";
        self.tweetList().forEach(function (element, index, array) {
            debugger;
            
            var marker = new google.maps.Marker({
                position: { lat: Number(element.Latitude), lng: Number(element.Longitude) },
                map: map,
                icon: iconBase
            });
        });
       
        //debugger;
        //for (var i = 0; i < 250; i++) {
           
        //}


    }

    self.getHashTagTweet = function () {
        debugger;

       

        $.ajax({

            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/CliqueAPI/GetHashTagTweets?tag=" + self.hashTagRequestModel().tag() + "&location=" + self.hashTagRequestModel().location() + "&fromDate=" + self.hashTagRequestModel().fromDate() + "&toDate=" + self.hashTagRequestModel().toDate()
        }).done(function (res) {
            debugger;
            self.tweetList(res.CliqueTweetList);
            self.initialize(res);
            $("#tweetTable").DataTable({
                responsive: true
            });
            $("#Details").removeClass('hide').addClass('show');
            //$('#MapDetails').addClass('in');
        }).error(function (ex) {
            debugger;
            alert("Error");
        });



    };



    debugger;

  //  self.getHashTagTweet();

   

};

