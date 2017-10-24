﻿google.load('visualization', '1', { 'packages': ['geochart'] });
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

$(document).ready(function () {
    // Set Inits
    setBreadCrumb($("#cmb_YourGames option:selected").text());

    $('#cmb_YourGames').change(function () {
        if ($("#cmb_YourGames").val() == "all") {
            gotoURL("RealTimeDashboard.aspx");
        } else {
            GameId = $("#cmb_YourGames").val();
            ServerName = $("#cmb_YourGames option:selected").attr("sn");
            RegId = $("#cmb_YourGames option:selected").attr("reg");
            setTimeout(fetchData, 1000);
        }
    });

    var placeholder = $("#audience-chart");

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
    $('#audience-chart').bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0].toFixed(2),
                            y = addCommas(item.datapoint[1].toFixed(0));

                showTooltip(item.pageX, item.pageY,
                            "Visitors: " + y + "<br/>Date: " + UnixToDate(x/1000));

            }
        }
        else {
            $("#tooltip").remove();
            previousPoint = null;
        }

    });
    // end tootips chart

    // Global Settings
    var fetchActions = "Total,Country,City";
    var arrayofCountry = [['Country', 'Popularity']];
    var startDate = DateTimetoUnix(Date.today().add({ days: -30 }));
    var endData = DateTimetoUnix(Date.today().add({ days: -1 }));
    var page = 0;
    var pageLimit = 20;

    $('#reportrange').daterangepicker(
                     {
                         ranges: {
                             'Today': [moment(), moment()],
                             'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                             'Last 7 Days': [moment().subtract(7, 'days'), moment()],
                             'Last 15 Days': [moment().subtract(15, 'days'), moment()],
                             'Last 30 Days': [moment().subtract(30, 'days'), moment()],
                             'This Month': [moment().startOf('month'), moment().endOf('month')],
                             'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                         },
                         opens: 'left',
                         format: 'MM/DD/YYYY',
                         separator: ' to ',
                         startDate: moment().subtract(29, 'days'),
                         endDate: moment().subtract(1, 'days'),
                         minDate: '01/01/2013',
                         maxDate: moment().subtract(1, 'days'),
                         buttonClasses: ['btn-small btn-default'],
                         applyClass: 'btn-small btn-primary',
                         cancelClass: 'btn-small',
                         locale: {
                             applyLabel: 'Submit',
                             fromLabel: 'From',
                             toLabel: 'To',
                             customRangeLabel: 'Custom Range',
                             daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                             monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                             firstDay: 1
                         },
                         showWeekNumbers: true,
                         dateLimit: false
                     },
                     function (start, end) {
                         $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                         startDate = DateTimetoUnix(start.format('MMMM D, YYYY'));
                         endData = DateTimetoUnix(end.format('MMMM D, YYYY'));
                         setTimeout(fetchData, 1000);
                     }
        );

    //Set the initial state of the picker label
    $('#reportrange span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));

    // Fecth Data
    function fetchData() {
        waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "http://" + getServerName(ServerName, ServerPath) + "/vdata1.0/Audience.aspx?rid=" + RegId + "&gid=" + GameId + "&act=" + fetchActions + "&dat=" + startDate + "," + endData + "&pag=" + page,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

        function onDataReceived(recv) {
            if (recv == false) {
                gotoURL("/");
                return;
            }

            var i = 1;
            var myCols;
            var _fetchAction = fetchActions.split(',');

            if ($.inArray("Country", _fetchAction) > -1) {
                var htmlBody = '';
                i = 1;
                $("#tbl_Country > tbody").empty();
                for (myCols in recv.metrics.Country) {
                    arrayofCountry.push([recv.metrics.Country[myCols].c1, parseInt(recv.metrics.Country[myCols].c2)]);
                    htmlBody += ('<tr><td>' + (i+page*pageLimit) + '</td><td>' + recv.metrics.Country[myCols].c1 + '</td><td style="text-align: center;">' + addCommas(recv.metrics.Country[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.Country[myCols].c3 + '%</td></tr>');
                    i++;
                }
                $('#tbl_Country').append(htmlBody);
                setPageNumber("#CountryPager", parseInt(recv.metrics.TotalPage), page, pageLimit);

                redrawRegionsMap(arrayofCountry);
            }
            if ($.inArray("Total", _fetchAction) > -1) {

                $("#TotalUsers").text("Total visitors " + addCommas(recv.metrics.Total.c1) + ", unique visitors " + addCommas(recv.metrics.Total.c2));
                var d1 = [];
                var d2 = [];
                //var plotTicks = [];
                var ymin = 0;
                var ymax = 0;
                i = 0;
                for (myCols in recv.metrics.TotalUsers) {
                    d1.push([parseInt(recv.metrics.TotalUsers[myCols].c1 + "000"), parseInt(recv.metrics.TotalUsers[myCols].c2)]);
                    d2.push([parseInt(recv.metrics.TotalUsers[myCols].c1 + "000"), parseInt(recv.metrics.TotalUsers[myCols].c3)]);
                    //d1.push([i, parseInt(recv.metrics.TotalUsers[myCols].c2)]);
                    //d2.push([i, parseInt(recv.metrics.TotalUsers[myCols].c3)]);
                    //plotTicks.push([i, recv.metrics.TotalUsers[myCols].c1]);
                    if (ymin > parseInt(recv.metrics.TotalUsers[myCols].c2)) ymin = parseInt(recv.metrics.TotalUsers[myCols].c2);
                    if (ymax < parseInt(recv.metrics.TotalUsers[myCols].c2)) ymax = parseInt(recv.metrics.TotalUsers[myCols].c2);
                    i++;
                }

                var options = {
                    series: {
                        lines: {
                            show: true,
                            fill: true
                        },
                        points: {
                            show: true
                        },
                        hoverable: true
                    },
                    grid: {
                        backgroundColor: '#FFFFFF',
                        borderWidth: 1,
                        borderColor: '#CDCDCD',
                        hoverable: true
                    },
                    legend: {
                        show: true
                    },
                    xaxis: {
                        mode: "time"
                        //ticks: plotTicks
                    },
                    yaxis: {
                        yaxis: { min: ymin, max: ymax }
                    }
                };

                $.plot(placeholder, [{ label: "Total Visitors", data: d1, lines: { show: true }
                }, { label: "Unique Visitors", data: d2, lines: { show: true }
                }], options);
            }

            drawBreadCrumb();
            waitDialog.hidePleaseWait();
        } //onDataReceived
    }
    setTimeout(fetchData, 1000);

    $(".pagerPre").click(function (event) {
        event.preventDefault();
        var ParentObj = event.currentTarget.parentElement.parentElement;
        var totalpage = Math.ceil($(ParentObj).attr("totalPage") / pageLimit);
        if (page > 0) {
            page--;
            $("#pageNumber").text("Page " + (page + 1) + "/" + totalpage);
            setTimeout(fetchData, 1000);
        }
    });
    $(".pagerNext").click(function (event) {
        event.preventDefault();
        var ParentObj = event.currentTarget.parentElement.parentElement;
        var totalpage = Math.ceil($(ParentObj).attr("totalPage") / pageLimit);
        if (page < totalpage - 1) {
            page++;
            $("#pageNumber").text("Page " + (page + 1) + "/" + totalpage);
            //fetchActions = "Country";
            setTimeout(fetchData, 1000);
        }
    });
    function setPageNumber(objId, value, page, pageLimit) {
        $(objId).attr("totalPage", value);
        $(objId).attr("page", page);
        $(objId).attr("pageLimit", pageLimit);
        var pageCount = Math.ceil(value / pageLimit);
        $("#pageNumber").text("Page " + (page + 1) + "/" + pageCount);
    }

});