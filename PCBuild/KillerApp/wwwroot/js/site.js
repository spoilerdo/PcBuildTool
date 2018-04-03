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

    $('.tabsection').slice(1).hide();
});

function ChangeTabs(selectorname, tabname) {
    $('.tab').removeClass('selectedtab');
    $('#' + tabname).addClass('selectedtab');
    $('.tabsection').hide();
    $('#' + selectorname).show();
}