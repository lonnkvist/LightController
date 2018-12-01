// Write your JavaScript code.

$(document).ready(function() {

    $(".sliderItem").on("slide", function (slideEvt) {
        alert(slideEvt.value);
    });

    $("#on").on("click", function () {
        $.get("/home/LightOn/");
    });

    $("#off").on("click", function () {
        $.get("/home/LightOff/");
    });

});