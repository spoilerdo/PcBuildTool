$(document).ready(function() {
    $("#accountLink").hover(
        function() {
            $(".AccountPopUp").css("display", "block");
        },
        function() {
            $(".AccountPopUp").css("display", "none");
        }
    );
    $("#PopUp").hover(
        function() {
            $(".AccountPopUp").css("display", "block");
        },
        function() {
            $(".AccountPopUp").css("display", "none");
        }
    );

    $(".tabsection").slice(1).hide();

    $("#my-select").multiSelect();
});

function ChangeTabs(selectorname, tabname) {

    $(".tab").removeClass("selectedtab");
    $(document.getElementById(selectorname)).addClass("selectedtab");
    $(".tabsection").hide();
    $(document.getElementById(tabname)).show();
}