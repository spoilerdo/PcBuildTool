$(document).ready(function() {

    $('.builds').click(showBuilds);

    function showBuilds(event) {
        event.preventDefault();

        hideBuilds();
        var targetElement = $('#OverviewPartialviewBox');

        showPartialView(event, targetElement);
    }

    function hideBuilds() {
        $('#OverviewPartialviewBox').empty();
    }

    function showPartialView(event, targetElement) {
        targetElement.html('Loading...');

        var element = $(event.currentTarget);
        var url = element.attr('href');

        $.get(url).done(function(view) {
            targetElement.html(view);
        });
    }
});