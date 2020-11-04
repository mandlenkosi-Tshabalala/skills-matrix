
let placeSearch;
let autocomplete;
const componentForm = {
    street_number: "short_name",
    route: "long_name",
    locality: "long_name",
    administrative_area_level_1: "short_name",
    country: "long_name",
    postal_code: "short_name",
};

function initAutocomplete() {
    // Create the autocomplete object, restricting the search predictions to
    // geographical location types.

    var org = document.getElementById('autocomplete');

    // new window.google.maps.places.Autocomplete(org, options);

    autocomplete = new window.google.maps.places.Autocomplete(
        org,
        { types: ["geocode"] }



    );
    // Avoid paying for data that you don't need by restricting the set of
    // place fields that are returned to just the address components.
    autocomplete.setFields(["address_component"]);
    // When the user selects an address from the drop-down, populate the
    // address fields in the form.
    autocomplete.addListener("place_changed", fillInAddress);
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    const place = autocomplete.getPlace();

    for (const component in componentForm) {
        document.getElementById(component).value = "";
        document.getElementById(component).disabled = false;
    }

    // Get each component of the address from the place details,
    // and then fill-in the corresponding field on the form.
    for (const component of place.address_components) {
        const addressType = component.types[0];

        if (componentForm[addressType]) {
            const val = component[componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }

    const address = {};

    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            if (addressType == "street_number") {
                address.StreetNumber = place.address_components[i][componentForm[addressType]];
            }
            else if (addressType == "route") {
                address.StreetName = place.address_components[i][componentForm[addressType]];
            }
            else if (addressType == "locality") {
                address.City = place.address_components[i][componentForm[addressType]];
            }
            else if (addressType == "administrative_area_level_1") {
                address.State = place.address_components[i][componentForm[addressType]];
            }
            else if (addressType == "postal_code") {
                address.ZipCode = place.address_components[i][componentForm[addressType]];
            }
            else if (addressType == "country") {
                address.Country = place.address_components[i][componentForm[addressType]];
            }
        }
    }

    DotNet.invokeMethodAsync('SkillsMatrix.Web', 'UpdateAddressCaller', JSON.stringify(address));
}

// Bias the autocomplete object to the user's geographical location,
// as supplied by the browser's 'navigator.geolocation' object.
function geolocate() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
            const geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude,
            };
            const circle = new window.google.maps.Circle({
                center: geolocation,
                radius: position.coords.accuracy,
            });
            autocomplete.setBounds(circle.getBounds());
        });
    }
}

function DoScriptSetup() {
    // Create the script tag, set the appropriate attributes
    var script = document.createElement('script');
    script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyAGafzh7tV2fKKypcr6LoR-KDIF1-S0cdg&callback=initAutocomplete&libraries=places&v=weekly';

    // Append the 'script' element to 'head'
    document.head.appendChild(script);
}

