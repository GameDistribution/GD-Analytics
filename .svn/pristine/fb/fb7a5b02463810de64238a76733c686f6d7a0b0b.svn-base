
var GameId = $("#cmb_YourGames").val();
var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");
var FirstCall = true;

$(function () {
    var oldOnlineUsers = 0;

    $('#cmb_YourGames').change(function () {
        if ($("#cmb_YourGames").val() == "all") {
            gotoURL("RealTimeDashboard.aspx");
        }
    });

    // Set Inits
    setBreadCrumb($("#cmb_YourGames option:selected").text());

    var placeholder = $("#realtime-chart");

    $.plot(placeholder, data, options);

    // tootips chart
    function showTooltip(x, y, contents) {
        $('<div id="tooltip" style="background-color:#000000; color:#ffffff;">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 5,
            border: '0px',
            padding: '2px 10px 2px 10px',
            opacity: 0.7,
            'font-size': '11px'
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $('#realtime-chart').bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0].toFixed(2),
                            y = addCommas(item.datapoint[1].toFixed(0));

                showTooltip(item.pageX, item.pageY,
                            "Active Visitor: " + addCommas(y));

            }
        }
        else {
            $("#tooltip").remove();
            previousPoint = null;
        }

    });
    // end tootips chart

    // Global Settings
    var fetchActions = "WebRefer,GameOnline";
    // Fecth Data
    function fetchData() {
        if (FirstCall) waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.support.cors = true;
        $.ajax({
            url: "http://"+getRealtimeServerName(ServerName, ServerPath) + "/OnlineUsers/?t=" + fetchTimer + "&rid=" + RegId +"&act=" + fetchActions + "&gid=" + GameId,
            method: 'GET',
            dataType: 'json',
            crossDomain: true,
            success: onDataReceived
        }).fail(function (err) {
            FirstCall = false;
            setTimeout(fetchData, 3000);
            console.log("URL is not reachable.. : ");
        });

        function onDataReceived(recv) {
            var i = 1;
            var myCols;
            var _fetchAction = fetchActions.split(',');
            var htmlBody = '';
            var htmlBar = "";
            if ($.inArray("WebRefer", _fetchAction) > -1) {
                var barPercent = 0;
                var otherBar = false;
                var arrayofWebRefer = [['WebRefer', 'Popularity']];
                i = 1;
                //$("#tbl_WebRefer > tbody").empty();
                //$("#ActiveUsersBar").empty();
                for (myCols in recv.metrics.WebRefer) {
                    arrayofWebRefer.push([recv.metrics.WebRefer[myCols].c1, parseInt(recv.metrics.WebRefer[myCols].c2)]);
                    htmlBody += ('<tr><td>' + i + '</td><td><a href="' + recv.metrics.WebRefer[myCols].c1 + '" target="_blank">' + (recv.metrics.WebRefer[myCols].c1).substring(0, 100) + '</a></td><td style="text-align: center;">' + addCommas(recv.metrics.WebRefer[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.WebRefer[myCols].c3 + '%</td></tr>');
                    if (i < 5) {
                        var url = recv.metrics.WebRefer[myCols].c1;
                        var domain = url.replace('http://','').replace('https://','').split(/[/?#]/)[0];
                        if (domain == "Other") {
                            recv.metrics.WebRefer[myCols].c3 = 100 - barPercent;
                            otherBar = true;
                        } else {
                            barPercent += recv.metrics.WebRefer[myCols].c3;
                        }
                        htmlBar += ('<div class="progress-bar progress-bar-' + getBarColor(i % 5) + '" style="width: ' + recv.metrics.WebRefer[myCols].c3 + '%;">' + domain + '</div>');
                    }
                    if (i == 5 && !otherBar) {
                        htmlBar += ('<div class="progress-bar progress-bar-' + getBarColor(i % 5) + '" style="width: ' + (100 - barPercent) + '%;">Other</div>');
                    }
                    i++;
                }
                $('#tbl_WebRefer > tbody').html(htmlBody);
                $('#ActiveUsersBar').html(htmlBar);
            }
            if ($.inArray("GameOnline", _fetchAction) > -1) {
                //$("#OnlineUsers").text(addCommas(recv.metrics.GameOnline));
                $({ numberValue: oldOnlineUsers }).animate({ numberValue: parseInt(recv.metrics.GameOnline) }, {
                    duration: 500,
                    easing: 'linear',
                    step: function () {
                        $('#OnlineUsers').text(addCommas(Math.ceil(this.numberValue)));
                    }
                });
                oldOnlineUsers = parseInt(recv.metrics.GameOnline);

                data[0].push([plotCount * 3, parseInt(recv.metrics.GameOnline)]);
                if (plotCount > 30) {
                    data[0].shift();
                }
                plotCount++;
                $.plot(placeholder, data, options);
            }

            drawBreadCrumb();
	        if (FirstCall) { waitDialog.hidePleaseWait(); FirstCall = false; }
            setTimeout(fetchData, 3000);

        } //onDataReceived
    }
    setTimeout(fetchData, 1000);

});