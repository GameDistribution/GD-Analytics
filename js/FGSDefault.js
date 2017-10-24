$(function () {
    $("#contact").click(function () {
        $('#myContact').modal('show');
    });

    $('.selectpicker').selectpicker();

    $("#btnShareGame").click(function (e) {
        e.preventDefault();
        if ($("#cmb_YourGames option:selected").attr("og") == "1") {
            $("#shareGameTitle").text("Share " + $("#cmb_YourGames option:selected").text());
            $("#shareGameInfo").text("");
            $('#shareThisGame').modal('show');
        } else {
            bootbox.alert("You can share only your games!", function () { });
        }
    });
    $("#doShareGame").click(function (e) {
        e.preventDefault();
        fetchData("&act=AddSharedGame&gid=" + $("#cmb_YourGames option:selected").val() + "&suemail=" + $("#useremail").val(), onDataReceived);
        function onDataReceived(recv) {
            if (recv.code == 200) {
                gotoURL("SharedGameList.aspx");
            } else {
                $("#shareGameInfo").text(recv.message);
            }
        }
    });
    $("#btnBlockGame").click(function (e) {
        e.preventDefault();
        if ($("#cmb_YourGames option:selected").attr("og") == "1") {
            $("#blockGameTitle").text("Blocking " + $("#cmb_YourGames option:selected").text());
            $("#blockGameInfo").text("");
            $('#blockThisGame').modal('show');
        } else {
            bootbox.alert("This is not your games!", function () { });
        }
    });
    $("#btnBlockBanner").click(function (e) {
        e.preventDefault();
        if ($("#cmb_YourGames option:selected").attr("og") == "1") {
            $("#blockBannerTitle").text("Blocking Banner for " + $("#cmb_YourGames option:selected").text());
            $("#bannerGameInfo").text("");
            $('#blockBanner').modal('show');
        } else {
            bootbox.alert("This is not your games!", function () { });
        }
    });
    $("#doBlockGame").click(function (e) {
        e.preventDefault();
        fetchData("&act=AddBlockGame&gid=" + $("#cmb_YourGames option:selected").val() + "&website=" + $("#websitename").val(), onDataReceived);
        function onDataReceived(recv) {
            if (recv.code == 200) {
                gotoURL("BlockedGameList.aspx?gid="+ $("#cmb_YourGames option:selected").val());
            } else {
                $("#blockGameInfo").text(recv.message);
            }
        }
    });
    $("#doBlockBanner").click(function (e) {
        e.preventDefault();
        fetchData("&act=AddBlockGameBanner&gid=" + $("#cmb_YourGames option:selected").val() + "&website=" + $("#bannerWebsite").val(), onDataReceived);
        function onDataReceived(recv) {
            if (recv.code == 200) {
                gotoURL("BlockedGameBannerList.aspx?gid=" + $("#cmb_YourGames option:selected").val());
            } else {
                $("#bannerGameInfo").text(recv.message);
            }
        }
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
