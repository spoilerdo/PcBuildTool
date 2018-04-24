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


    var liked = $("#likeBtn").hasClass("btn-clicked");
    var disliked = $("#dislikeBtn").hasClass("btn-clicked");
    $("#likeBtn").click(function () {
        liked = $("#likeBtn").hasClass("btn-clicked");

        if (liked === true) {
            $(this).removeClass("btn-clicked");
        } else {
            $("#dislikeBtn").removeClass("btn-clicked");
            $(this).addClass("btn-clicked");
        }

        $("#Liked").remove();
        $("#Disliked").remove();
        $("#AjaxForm").append('<input value="true" data-val="true" data-val-required="The Liked field is required." id="Liked" name="Liked" type="hidden">');
    });
    $("#dislikeBtn").click(function () {
        disliked = $("#dislikeBtn").hasClass("btn-clicked");

        if (disliked === true) {
            $(this).removeClass("btn-clicked");
        } else {
            $("#likeBtn").removeClass("btn-clicked");
            $(this).addClass("btn-clicked");
        }

        $("#Liked").remove();
        $("#Disliked").remove();
        $('#AjaxForm').append('<input value="true" data-val="true" data-val-required="The Disliked field is required." id="Disliked" name="Disliked" type="hidden">');
    });
});

function ChangeTabs(selectorname, tabname) {

    $(".tab").removeClass("selectedtab");
    $(document.getElementById(selectorname)).addClass("selectedtab");
    $(".tabsection").hide();
    $(document.getElementById(tabname)).show();
}