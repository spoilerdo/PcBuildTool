$(document).ready(function () {
    $('.pcselector').click(function () {
        var pcPart = {
            "EAN": $(this).attr('data-href')
        };
        console.log(pcPart);

        ajaxPost('/PCBuild/SendPcPart', pcPart);
    });

    $("#userName").change(function() {
        var userName = $(this).val();
        console.log(userName);

        var classNames = [".usernameError"];

        ajaxPost('/Account/CheckUsername', userName);
    });

    $('#accountButton').click(function () {
        var account = {
            "UserName": $('#userName').val(),
            "Password": $('#password').val(),
            "ConfPassword": $('#confPassword').val()
        };

        ajaxPost('/Account/SendAccount', account);
    });

    $('#loginAccountButton').click(function() {
        var account = {
            "UserName": $('#loginUserName').val(),
            "Password": $('#loginPassword').val(),
            "ConfPassword": $('#loginPassword').val()
        };

        ajaxPost('/Account/LogIn', account);
    });

    function ajaxPost(ul, dt) {
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
            success: ajaxSucces,
            error: function (result) {
                console.log(result);
            }
        });
    }

    function ajaxSucces(result) {
        console.log('succes' + result);

        var obj = JSON.parse(result);
        if (obj.Result === "False") {
            $(obj.ClassName).show();
        } else {
            hideErrors();
        }
    }

    hideErrors();
    function hideErrors() {
        $('.usernameError').hide();
        $('.passwordError').hide();
        $('.confpasswordError').hide();
    }
});