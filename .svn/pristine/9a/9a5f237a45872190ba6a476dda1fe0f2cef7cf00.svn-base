$(function () {

    var ServerName = $("#cmb_YourGames option:selected").attr("sn");

    $(".sharedgames").click(function () {
        var gid = $(this).attr("gid");
        var suid = $(this).attr("suid");
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
                        fetchData("&act=DelSharedGame&gid=" + gid + "&suid=" + suid, onDataReceived);
                        function onDataReceived(recv) {
                            if (recv == false) {
                                gotoURL("/");
                                return;
                            }

                            if (recv.code == 200) {
                                $(clickedElement).remove();
                            }
                            bootbox.alert(recv.message, function () {
                                $(tablerowId).animate({ backgroundColor: 'yellow' }, 1000).fadeOut(1000, function () {
                                    $(tablerowId).remove();
                                });
                            });
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

    function fetchData(param, onDataReceived) {
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "DoAction.ashx?t=" + fetchTimer + param,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

    }
});    