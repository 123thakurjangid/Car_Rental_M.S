
    let button = document.getElementById("get-location");
    let latText = document.getElementById("latitude");
    let longText = document.getElementById("longitude");

    button.addEventListener("click", () => {
        navigator.geolocation.getCurrentPosition((position) => {
            let lat = position.coords.latitude;
            let long = position.coords.longitude;
            latText.innerText = lat.toFixed(2);
            longText.innerText = long.toFixed(2);

            let lat1 = lat;
            let long1 = long;
            let lat2 = 26.9525;
            let long2 = 75.7105;

            document.getElementById("GettingLongitude").value = long;
            getDistanceFromLatLonInKm(lat1, long1, lat2, long2);
        });
    });

    function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
        var R = 6371; // Radius of the earth in km
        var dLat = deg2rad(lat2 - lat1);  // deg2rad below
        var dLon = deg2rad(lon2 - lon1);
        var a =
            Math.sin(dLat / 2) * Math.sin(dLat / 2) +
            Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
            Math.sin(dLon / 2) * Math.sin(dLon / 2)
            ;
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var d = R * c; // Distance in km

        console.log("latitude"+lat1);
        console.log("longtitude"+lon1);
        console.log("distanc= " + d);

        document.getElementById("GettingLatitude").value = lat1;
        document.getElementById('GettingDistance').value = Math.round(d);
        if (d != null && d <= 30) {
            document.getElementById('ans2').innerHTML = ("Service is available in your area,You are good to goo");
        } else {
            document.getElementById('ans2').innerHTML = ("Opps Service is not available in your area,very soon we try to provide service in your area");
        }
    }

    function deg2rad(deg) {
        return deg * (Math.PI / 180)
    }