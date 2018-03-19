$(document).ready(function () {
    $('.pcselector').click(function () {
        var pcPart = {
            "EAN": $(this).attr('data-href')
        };
        console.log(pcPart);

        ajaxPost('/PCBuild/SelectPcPart', pcPart);
    });

    $('.usernameField').change(function() {
        var userName = $(this).val();
        console.log(userName);

        var classNames = [".usernameError"];

        ajaxPost('/Account/CheckUsername', userName, classNames);
    });

    function ajaxPost(ul, dt, classNames) {
        $.ajax({
            type: "POST",
            url: ul,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(dt),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (result) {
                console.log('succes ' + result);
                if (result == false) {
                    for (var i = 0; i < classNames.length; i++) {
                        $(classNames[i]).show();
                    }
                } else {
                    for (var i = 0; i < classNames.length; i++) {
                        $(classNames[i]).hide();
                    }
                }
            },
            error: function (result) {
                console.log('error ' + result);
            }
        });
    }
    
    hideErrors();
    function hideErrors() {
        $('.usernameError').hide();
        $('.passwordError').hide();
    }
});