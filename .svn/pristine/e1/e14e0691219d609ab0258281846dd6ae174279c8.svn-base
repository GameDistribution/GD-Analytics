var GameId = $("#cmb_YourGames").val();
var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");

$(document).ready(function () {
    // Set Inits
    setBreadCrumb($("#cmb_YourGames option:selected").text());

    $('#cmb_YourGames').change(function () {
        GameId = $("#cmb_YourGames").val();
        ServerName = $("#cmb_YourGames option:selected").attr("sn");
        RegId = $("#cmb_YourGames option:selected").attr("reg");
        setTimeout(fetchData, 1000);
    });

    var placeholder = $("#monetize-chart");

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
    $('#monetize-chart').bind("plothover", function (event, pos, item) {

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.series.data[item.dataIndex][0],
                    y = addCommas(item.series.data[item.dataIndex][1].toFixed(2)),
                    currency = item.series.data[item.dataIndex][2];
                showTooltip(item.pageX, item.pageY, "Earning: " + y + " " + currency + "<br/>Date: " + UnixToDateWithDay(x / 1000));
            }
        }
        else {
            $("#tooltip").remove();
            previousPoint = null;
        }

    });
    // end tootips chart

    // Global Settings
    var fetchActions = "GameIncomesTotal,GameIncomes";
    var arrayofEarn = [['Earn', 'Popularity']];
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
                         startDate: moment().subtract(30, 'days'),
                         endDate: moment().subtract(0, 'days'),
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
    $('#reportrange span').html(moment().subtract(30, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));

    // Fecth Data
    function fetchData() {
        waitDialog.showPleaseWait();
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "http://" + getServerName(ServerName, ServerPath) + "/vdata1.0/Monetize.aspx?rid=" + RegId + "&gid=" + GameId + "&act=" + fetchActions + "&dat=" + startDate + "," + endData + "&pag=" + page,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

        function onDataReceived(recv) {

            if (!recv) {
                gotoURL("/");
                return;
            }

            var i = 1;
            var myCols;
            var htmlBody = '';
            var _fetchAction = fetchActions.split(',');

            if ($.inArray("GameIncomes", _fetchAction) > -1) {

                var d1 = [];
                var ymin = 0;
                var ymax = 0;

                var monetize = { c1:0,c2:0,c3:0,c4:0,c5:0,c6:"",c7:0 };

                i = 0;
                $("#tbl_MonetizeIncome > tbody").empty();
                for (myCols in recv.metrics.GameIncomes) {
                    htmlBody += ('<tr><td>' + ((i + 1) + page * pageLimit) + '</td>'+
                        '<td>' + UnixToDateWithDay(recv.metrics.GameIncomes[myCols].c8) + '</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c1 + '</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c2 + '</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c3 + '%</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c4 + ' ' + recv.metrics.GameIncomes[myCols].c6 + '</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c5 + ' ' + recv.metrics.GameIncomes[myCols].c6 + '</td>' +
                        '<td style="text-align: center;">' + recv.metrics.GameIncomes[myCols].c7 + ' ' + recv.metrics.GameIncomes[myCols].c6 + '</td>' +
                        '</tr>');

                    monetize.c1 += recv.metrics.GameIncomes[myCols].c1;
                    monetize.c2 += recv.metrics.GameIncomes[myCols].c2;
                    monetize.c3 += recv.metrics.GameIncomes[myCols].c3;
                    monetize.c4 += recv.metrics.GameIncomes[myCols].c4;
                    monetize.c5 += recv.metrics.GameIncomes[myCols].c5;
                    monetize.c6  = recv.metrics.GameIncomes[myCols].c6;
                    monetize.c7 += recv.metrics.GameIncomes[myCols].c7;

                    d1.push([parseInt(recv.metrics.GameIncomes[myCols].c8 + "000"), parseFloat(recv.metrics.GameIncomes[myCols].c7), String(recv.metrics.GameIncomes[myCols].c6)]);
                    if (ymin > parseInt(recv.metrics.GameIncomes[myCols].c7)) ymin = parseInt(recv.metrics.GameIncomes[myCols].c7);
                    if (ymax < parseInt(recv.metrics.GameIncomes[myCols].c7)) ymax = parseInt(recv.metrics.GameIncomes[myCols].c7);

                    i++;
                }

                if (i > 1) {
                    i--;
                } else {
                    i = 1;
                }
                htmlBody += ('<tr class="success"><td></td>' +
                    '<td> <strong>Total</strong></td>' +
                    '<td style="text-align: center;"><strong>' + monetize.c1 + '</strong></td>' +
                    '<td style="text-align: center;"><strong>' + monetize.c2 + '</strong></td>' +
                    '<td style="text-align: center;"><strong>' + (monetize.c3 / (i)).toFixed(2) + '%</strong></td>' +
                    '<td style="text-align: center;"><strong>' + (monetize.c4 / (i)).toFixed(2) + ' ' + monetize.c6 + '</strong></td>' +
                    '<td style="text-align: center;"><strong>' + (monetize.c5 / (i)).toFixed(2) + ' ' + monetize.c6 + '</strong></td>' +
                    '<td style="text-align: center;"><strong>' + monetize.c7 + ' ' + monetize.c6 + '</strong></td>' +
                    '</tr>');

                $('#tbl_MonetizeIncome').append(htmlBody);

                setPageNumber("#MonetizeIncomePager", parseInt(recv.metrics.TotalPage), page, pageLimit);

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

                $.plot(placeholder, [
                {
                    label: "Earnings",
                    data: d1, lines: { show: true }
                }
                ], options);
            }

        
            if ($.inArray("GameIncomesTotal", _fetchAction) > -1) {
                if (recv.metrics.GameIncomesTotal.length>0) {
                    $("#BannerView").text(recv.metrics.GameIncomesTotal[0].c1);
                    $("#BannerClick").text(recv.metrics.GameIncomesTotal[0].c2);
                    $("#BannerCTR").text(recv.metrics.GameIncomesTotal[0].c3.toFixed(2) + '%');
                    $("#BannerCPC").text(recv.metrics.GameIncomesTotal[0].c4.toFixed(2) + ' ' + recv.metrics.GameIncomesTotal[0].c6);
                    $("#BannerRPM").text(recv.metrics.GameIncomesTotal[0].c5.toFixed(2) + ' ' + recv.metrics.GameIncomesTotal[0].c6);
                    $("#BannerEarn").text(recv.metrics.GameIncomesTotal[0].c7.toFixed(2) + ' ' + recv.metrics.GameIncomesTotal[0].c6);
                } else {
                    $("#BannerView").text(0);
                    $("#BannerClick").text(0);
                    $("#BannerCTR").text('0.00%');
                    $("#BannerCPC").text('0.00');
                    $("#BannerRPM").text('0.00');
                    $("#BannerEarn").text('0.00');
                }
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