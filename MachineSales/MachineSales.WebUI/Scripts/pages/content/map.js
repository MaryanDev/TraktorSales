﻿//google map section
function initMap() {
    var uluru = { lat: -25.363, lng: 131.044 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 4,
        center: uluru,
        scrollwheel: false
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}