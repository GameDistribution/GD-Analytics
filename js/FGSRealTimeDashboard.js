google.load('visualization', '1', { 'packages': ['geochart'] });
google.setOnLoadCallback(drawRegionsMap);
var google_chart;
var google_options = {};
var google_data;

function drawRegionsMap() {
    google_data = google.visualization.arrayToDataTable([
                  ['Country', 'Popularity']
                ]);

    google_options = {};

    google_chart = new google.visualization.GeoChart(document.getElementById('chart_div'));
    google_chart.draw(google_data, google_options);

};

function redrawRegionsMap(arrayofVariables) {
    google_data = google.visualization.arrayToDataTable(arrayofVariables);
    google_options = {};
    google_chart.draw(google_data, google_options);
};

var GameId = $("#cmb_YourGames").val();
var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");
var FirstCall = true;

$(function () {
    var placeholder = $("#realtime-chart");
    var realtime_placeholder = $("#realtime-change");
    var realtime_newuser = $("#realtime_newusers");
    var oldOnlineUsers = 0;

    // Pie chart        
    $.plot(realtime_newuser, newuser_data,
	{
	    series: {
	        pie: {
	            show: true,
	            radius: 3 / 4,
	            label: {
	                show: true,
	                radius: 3 / 4,
	                formatter: function (label, series) {
	                    return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
	                },
	                background: {
	                    opacity: 0.5,
	                    color: '#000'
	                }
	            }
	        }
	    },
	    legend: {
	        show: false
	    }
	});

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
                            "Active Users: " + y);

            }
        }
        else {
            $("#tooltip").remove();
            previousPoint = null;
        }

    });
    // end tootips chart

    // Global Settings
    var fetchActions = "Whole,Country,WebRefer,TotalOnline,VisitState";
    // Fecth Data
    function fetchData() {
        if (FirstCall) waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.support.cors = true;
        $.ajax({
            url: "http://" + getRealtimeServerName(ServerName, ServerPath) + "/OnlineUsers/?t=" + fetchTimer + "&rid=" + RegId + "&act=" + fetchActions + "&gid=Whole",
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
            var htmlBody = '';
            var htmlBar = "";
            var i = 1;
            var myCols;
            var _fetchAction = fetchActions.split(',');

            if ($.inArray("Country", _fetchAction) > -1) {
                var barPercent = 0;
                var otherBar = false;
                var arrayofCountry = [['Country', 'Popularity']];
                i = 1;
                //$("#tbl_Country > tbody").empty();
                //$("#ActiveUsersBar").empty();
                for (myCols in recv.metrics.Country) {
                    arrayofCountry.push([recv.metrics.Country[myCols].c1, parseInt(recv.metrics.Country[myCols].c2)]);
                    htmlBody += ('<tr><td>' + i + '</td><td>' + recv.metrics.Country[myCols].c1 + '</td><td style="text-align: center;">' + addCommas(recv.metrics.Country[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.Country[myCols].c3 + '%</td></tr>');
                    if (i < 6) {
                        if (recv.metrics.Country[myCols].c1 == "Other") {
                            recv.metrics.Country[myCols].c3 = 100 - barPercent;
                            otherBar = true;
                        } else {
                            barPercent += recv.metrics.Country[myCols].c3;
                        }
                        htmlBar += ('<div class="progress-bar progress-bar-' + getBarColor(i % 5) + '" style="width: ' + recv.metrics.Country[myCols].c3 + '%;">' + recv.metrics.Country[myCols].c1 + '</div>');
                    }
                    if (i == 6 && !otherBar) {
                        htmlBar += ('<div class="progress-bar progress-bar-' + getBarColor(i % 5) + '" style="width: ' + (100 - barPercent) + '%;">Other</div>');
                    }
                    i++;
                }
                $('#ActiveUsersBar').html(htmlBar);
                $('#tbl_Country > tbody').html(htmlBody);
                redrawRegionsMap(arrayofCountry);
            }
            if ($.inArray("Whole", _fetchAction) > -1) {
                i = 1;
                htmlBody = '';
               // $("#tbl_Whole > tbody").empty();
                for (myCols in recv.metrics.Games) {
                    htmlBody += ('<tr><td>' + i + '</td><td><a href="RealTimeOverview.aspx?gid=' + recv.metrics.Games[myCols].c1 + '">' + GetGameName(recv.metrics.Games[myCols].c1) + '</a></td><td style="text-align: center;">' + addCommas(recv.metrics.Games[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.Games[myCols].c3 + '%</td></tr>');
                    i++;
                }
                $('#tbl_Whole > tbody').html(htmlBody);
            }
            if ($.inArray("WebRefer", _fetchAction) > -1) {
                i = 1;
                htmlBody = '';
                //$("#tbl_WebRefer > tbody").empty();
                for (myCols in recv.metrics.WebRefer) {
                    htmlBody+=('<tr><td>' + i + '</td><td>' + recv.metrics.WebRefer[myCols].c1 + '</td><td style="text-align: center;">' + addCommas(recv.metrics.WebRefer[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.WebRefer[myCols].c3 + '%</td></tr>');
                    i++;
                }
                $('#tbl_WebRefer > tbody').html(htmlBody);
            }
            if ($.inArray("TotalOnline", _fetchAction) > -1) {
                //$("#OnlineUsers").text(addCommas(recv.metrics.TotalOnline));
                var deltaoOfOnlineUsers = parseInt(recv.metrics.TotalOnline) - oldOnlineUsers;
                $({ numberValue: oldOnlineUsers }).animate({ numberValue: parseInt(recv.metrics.TotalOnline) }, {
                    duration: 2500,
                    easing: 'swing',
                    step: function () {
                        $('#OnlineUsers').text(addCommas(Math.ceil(this.numberValue)));
                    }
                });
                oldOnlineUsers = parseInt(recv.metrics.TotalOnline);
                data[0].push([plotCount * 3, parseInt(recv.metrics.TotalOnline)]);
                if (plotCount > 30) {
                    data[0].shift();
                }
                plotCount++;
                $.plot(placeholder, data, options);

                // RealTime Changes chart
                if (deltaoOfOnlineUsers >= 0) {
                    change_blue_data.push([realtime_plotCount, deltaoOfOnlineUsers]);
                    change_red_data.push([realtime_plotCount, 0]);
                }
                else {
                    change_blue_data.push([realtime_plotCount, 0]);
                    change_red_data.push([realtime_plotCount, deltaoOfOnlineUsers]);
                }
                if (realtime_plotCount > 10) {
                    change_blue_data.shift();
                    change_red_data.shift();
                }
                realtime_plotCount++;

                var realtime_data = [{ data: change_blue_data, label: "New Visitors", color: "#1D89B8" }, { data: change_red_data, label: "Leaving Visitors", color: "#DB0024"}];

                // plot it
                var plot = $.plot(realtime_placeholder, realtime_data, {
                    bars: { show: true, barWidth: 0.5, fill: 0.9 },
                    xaxis: { ticks: [], autoscaleMargin: 0.02 },
                    yaxis: { alignTicksWithAxis: 1, position: "right" },
                    grid: {
                        backgroundColor: '#FFFFFF',
                        borderWidth: 1,
                        borderColor: '#CDCDCD',
                        hoverable: true
                    }
                });

            }

            if ($.inArray("VisitState", _fetchAction) > -1) {
                var new_users = 0;
                htmlBody = '';
                i = 1;
                //$("#tbl_VisitState > tbody").empty();
                for (myCols in recv.metrics.VisitState) {
                    if (recv.metrics.VisitState[myCols].c1 == "0" || recv.metrics.VisitState[myCols].c1 == "1") {
                        new_users = parseInt(recv.metrics.VisitState[myCols].c2);
                    }
                    htmlBody += ('<tr><td>' + i + '</td><td>' + '<div class="progress" style="margin-bottom:0px;"><div class="progress-bar progress-bar-' + getBarColor(i % 5) + '" style="width: ' + recv.metrics.VisitState[myCols].c3 + '%;"></div></div><small>' + recv.metrics.VisitState[myCols].c1 + ' times' + '</small></td><td style="text-align: center;">' + addCommas(recv.metrics.VisitState[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.VisitState[myCols].c3 + '%</td></tr>');
                    i++;
                }
                $('#tbl_VisitState > tbody').html(htmlBody);

                var newuser_data = [
	            { label: "Returning Visitors", data: (oldOnlineUsers == 0 ? 0.0001 : ((oldOnlineUsers - new_users) == 0 ? 0.0001 : (oldOnlineUsers - new_users))), color: "#2091CF" },
	            { label: "New Visitors", data: (new_users == 0 ? 0.0001 : new_users), color: "#68BC31" }
                ];

                $.plot(realtime_newuser, newuser_data,
	            {
	                series: {
	                    pie: {
	                        show: true,
	                        radius: 3 / 4,
	                        label: {
	                            show: true,
	                            radius: 3 / 4,
	                            formatter: function (label, series) {
	                                return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
	                            },
	                            background: {
	                                opacity: 0.5,
	                                color: '#000'
	                            }
	                        }
	                    }
	                },
	                legend: {
	                    show: false
	                }
	            });

	        }

	        if (FirstCall) { waitDialog.hidePleaseWait(); FirstCall = false; }
            setTimeout(fetchData, 3000);
        } //onDataReceived
    }
    setTimeout(fetchData, 1000);

});