$(function () {
    GameId = $("#cmb_YourGames").val();
    GameTitle = $("#cmb_YourGames option:selected").text();
    var ServerName = $("#cmb_YourGames option:selected").attr("sn");
    var RegId = $("#cmb_YourGames option:selected").attr("reg");

    setBreadCrumb($("#cmb_YourGames option:selected").text());

    $('#cmb_YourGames').change(function () {
        if ($("#cmb_YourGames").val() == "all") {
            gotoURL("RealTimeDashboard.aspx");
        } else {
            GameId = $("#cmb_YourGames").val();
            GameTitle = $("#cmb_YourGames option:selected").text();
            ServerName = $("#cmb_YourGames option:selected").attr("sn");
            RegId = $("#cmb_YourGames option:selected").attr("reg");
            setTimeout(fetchData, 1000);
        }
    });

    // Global Settings
    var fetchActions = "BannerFilter";
    function fetchData() {
        waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "http://" + getServerName(ServerName, ServerPath) + "/vdata1.0/Tools.aspx?t=" + fetchTimer + "&rid=" + RegId + "&gid=" + GameId + "&act=" + fetchActions,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

        function onDataReceived(recv) {
            $("#gameTitle").text(GameTitle);
            if (recv == false) {
                gotoURL("/");
                return;
            }

            var i = 0;
            var j = 0;
            var _fetchAction = fetchActions.split(',');
            var row = "";
            if ($.inArray("BannerFilter", _fetchAction) > -1) {
                // Country                
                $("#countries").empty();
                for (myCols in recv.metrics.BannerFilter.c1) {                    
                    row += '<li class="list-group-item" value="' + recv.metrics.BannerFilter.c1[myCols].c1 + '" ' + (recv.metrics.BannerFilter.c1[myCols].c3 ? 'data-checked="true"' : '') + '>' + recv.metrics.BannerFilter.c1[myCols].c2 + '</li>';
                    i++;
                    if (recv.metrics.BannerFilter.c1[myCols].c3) j++;
                }
                $("#countries").append(row);
                $("#countries").checkedList();
                $("#countries").on("click.checkedList", function (event) {
                    $("#countryCount").text( $("#countries").checkedList('selectedCount') );
                });

                $("#countryTotal").text(i);
                $("#countryCount").text(j);                

                // Domains
                row = "";
                for (myCols in recv.metrics.BannerFilter.c2) {
                    row += recv.metrics.BannerFilter.c2[myCols].c2 + '\r\n';
                }
                $("#domains").text(row);

                // My Games
                $("#mygames").checkedList();
                $("#mygames").on("click.checkedList", function (event) {
                    $("#gameCount").text($("#mygames").checkedList('selectedCount'));
                });
                $("#gameTotal").text($("#mygames").checkedList('count'));

                // States
                //$('input[id="countryIncExc"]').bootstrapSwitch('state', recv.metrics.BannerFilter.c3.c2, true);
                //$('input[id="domainIncExc"]').bootstrapSwitch('state', recv.metrics.BannerFilter.c3.c3, true);
                $('input[id="bannerEnable"]').bootstrapSwitch('state', recv.metrics.BannerFilter.c3.c4, true);
                $('input[id="preRoll"]').bootstrapSwitch('state', recv.metrics.BannerFilter.c3.c5, true);

                $('#showAfterTime').prop('selectedIndex', recv.metrics.BannerFilter.c3.c6);

                if (recv.metrics.BannerFilter.c3.c6 == 0) {
                    $('input[id="midRoll"]').bootstrapSwitch('state', false, true);
                    $('#showAfterTime').prop('disabled', true);
                    $('#showAfterTime').prop('selectedIndex', 0);
                } else {
                    $('input[id="midRoll"]').bootstrapSwitch('state', true, true);
                    $('#showAfterTime').prop('disabled', false);
                }
            }

            waitDialog.hidePleaseWait();
        } //onDataReceived
    }
    setTimeout(fetchData, 1000);

    $("[id='bannerEnable'],[id='preRoll'],[id='midRoll']").bootstrapSwitch();

    $("#midRoll").on('switchChange.bootstrapSwitch', function (event, state) {        
        $('#showAfterTime').prop('disabled', !state);
        $('#showAfterTime').prop('selectedIndex', 0);
    });

    $("#btnSelectAllCountries").click(function () {
        $("#countries").checkedList('selectAll');
        $("#countryCount").text($("#countries").checkedList('selectedCount'));
    });

    $("#btnDeselectAllCountries").click(function () {
        $("#countries").checkedList('deselectAll');
        $("#countryCount").text($("#countries").checkedList('selectedCount'));
    });

    $("#btnSelectAllGames").click(function () {
        $("#mygames").checkedList('selectAll');
        $("#gameCount").text($("#mygames").checkedList('selectedCount'));
    });

    $("#btnDeselectAllGames").click(function () {
        $("#mygames").checkedList('deselectAll');
        $("#gameCount").text($("#mygames").checkedList('selectedCount'));
    });

    $("#btnApply").click(function (e) {
        e.preventDefault();

        var selectedCountry = Array(), counter = 0;
        $("#countries").checkedList('selectedItems').each(function (idx, li) {
            selectedCountry.push($(li).val());
        });

        var selectedGames = Array();
        $("#mygames").checkedList('selectedItems').each(function (idx, li) {
            selectedGames.push($(li).attr("value"));
        });

        var selectedDomains = Array();        
        var lines = $('#domains').val().split(/\n/);
        for (var i = 0; i < lines.length; i++) {
            if (/\S/.test(lines[i])) {
                if (validateDomain($.trim(lines[i]))) {
                    selectedDomains.push($.trim(lines[i]));
                }
            }
        }

        var data = { countries: JSON.stringify(selectedCountry), domains: JSON.stringify(selectedDomains), games: JSON.stringify(selectedGames), countryState: $('#countryIncExc').is(":checked"), domainState: $('#domainIncExc').is(":checked"), bannerEnable: $('#bannerEnable').is(":checked"), preRoll: $('#preRoll').is(":checked"), showAfterTime: $('#showAfterTime').val() };

        fetchDoActionData("&act=SaveBannerFilter&gid=" + GameId, data,
            function onDataReceived(recv) {
                waitDialog.hidePleaseWait();
                if (recv.code == 0) {
                    gotoURL("/");
                    return;
                }
                bootbox.alert(recv.message, function () { });
            });

    });

    function fetchDoActionData(param, data, onDataReceived) {
        waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "/DoAction.ashx?t=" + fetchTimer + param,
            method: 'POST',
            dataType: 'json',
            data: data,
            success: onDataReceived
        });

    }

    function validateDomain(dom) {
        return /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/.test(dom) || /[-a-zA-Z0-9@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?/.test(dom);
    }
});
