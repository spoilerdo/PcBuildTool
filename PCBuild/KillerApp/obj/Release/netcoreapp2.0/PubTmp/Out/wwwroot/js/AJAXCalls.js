$(document).ready(function() {

    var propertieselection = [];
    $("#MultipleSelect > select").multiSelect({
        afterSelect: function(values) {
            var propertie = {
                "Id": parseInt(values)
            };
            propertieselection.push(propertie);
        }
    });

    $('#AjaxForm').on('submit', function (event) {
        event.preventDefault();
        getViewModel($(this), $(this).attr('controller-adres'));
    });

    $("#Account_UserName").change(function() {
        getViewModel($('#AccountForm'), '/Account/CheckUsername');
    });

    function getViewModel($this, adres) {
        var viewModel = $this.serialize();

        ajaxPost(adres, viewModel);
    }

    function ajaxPost(ul, dt) {
        $.ajax({
            type: "POST",
            url: ul,
            dataType: "json",
            data: dt,
            beforeSend: function(xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: ajaxSucces,
            error: function(result) {
                console.log(result);
            }
        });
    }

    function ajaxSucces(result) {
        console.log("succes" + result);

        var obj = JSON.parse(result);
        if (obj.Result === "False") {
            $(obj.ClassName).show();
        } else if (obj.Result === "true") {
            refresh();
            $(obj.ClassName).show();
        }else {
            hideErrors();
        }
    }

    hideErrors();

    function hideErrors() {
        $(".error-field").hide();
    }
});