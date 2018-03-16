$(document).ready(function () {
    $('.pcselector').click(function () {
        var pcPart = {
            "EAN": $(this).attr('data-href')
        };
        console.log(pcPart);

        $.ajax({
            type: "POST",
            url: '/PCBuild/SelectPcPart',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(pcPart),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (result) {
                console.log(result);
            },
            error: function (result) {
                console.log(result);
            }
        });
    });

    $('.usernameField').change(function() {
        var userName = $(this).val();
        console.log(userName);

        $.ajax({
            type: "POST",
            url: '/Account/CheckUsername',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(userName),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (result) {
                console.log(result);
            },
            error: function (result) {
                console.log(result);
            }
        });
    });
    hideErrors();
    function hideErrors() {
        $('.usernameError').hide();
        $('.passwordError').hide();
    }
});