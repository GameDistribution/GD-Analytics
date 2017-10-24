var GameId = $("#cmb_YourGames").val();
var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");

$(function () {

    $("#btnSetDefaultAccount").click(function () {
        var defacc = $("input[name='defaultAccount']:checked").val();
        fetchDoActionData("&act=SetPaymentAccount&accid=" + defacc, onDataReceived);
        function onDataReceived(recv) {
            if (recv.code == 0) {
                gotoURL("/");
                return;
            }
            if (recv.code == 200) {
                bootbox.alert("Default account is set.", function () {
                });
            } else {
                bootbox.alert("Not set!", function () {
                });
            }
        }
    });

    $(".accountname").click(function () {
        var accid = $(this).attr("accid");
        var tablerowId = $(this).parents('tr:first');
        var clickedElement = this;

        bootbox.dialog({
            message: "Are sure to delete?",
            title: "Delete Confirmation",
            buttons: {
                danger: {
                    label: "Yes!",
                    className: "btn-danger",
                    callback: function () {
                        fetchDoActionData("&act=DelPaymentAccount&accid=" + accid, onDataReceived);
                        function onDataReceived(recv) {
                            if (recv.code == 0) {
                                gotoURL("/");
                                return;
                            }
                            if (recv.code == 200) {
                                $(clickedElement).remove();
                                $(tablerowId).animate({ backgroundColor: 'yellow' }, 1000).fadeOut(1000, function () {
                                    $(tablerowId).remove();
                                });
                            } else {
                                bootbox.alert("Not deleted!", function () {
                                });
                            }
                        }
                    }
                },
                cancel: {
                    label: "Close",
                    className: "btn-success",
                    callback: function () {

                    }
                }
            }
        });

    });

    function fetchDoActionData(param, onDataReceived) {
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "/DoAction.ashx?t=" + fetchTimer + param,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

    }


});