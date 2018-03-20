$(document).ready(function() {
    $('#accountLink').hover(
        function() {
            $('.AccountPopUp').css("display", "block");
        },
        function() {
            $('.AccountPopUp').css("display", "none");
        }
    );
    $('#PopUp').hover(
        function() {
            $('.AccountPopUp').css("display", "block");
        },
        function() {
            $('.AccountPopUp').css("display", "none");
        }
    );
});