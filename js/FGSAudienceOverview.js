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
				var x = item.datapoint[0].toFixed(0),
					y = addCommas(item.datapoint[1].toFixed(0));

				showTooltip(item.pageX, item.pageY,
							"Visitors: " + y + "<br/>Date: " + UnixToDate(x / 1000));

			}
		}
		else {
			$("#tooltip").remove();
			previousPoint = null;
		}

	});
	// end tootips chart

	// Global Settings
	var fetchActions = "Overview";
	var startDate = DateTimetoUnix(Date.today().add({ days: -30 }));
	var endData = DateTimetoUnix(Date.today().add({ days: -1 }));

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
			url: "http://" + getServerName(ServerName, ServerPath) + "/vdata1.0/Audience.aspx?rid=" + RegId + "&gid=" + GameId + "&act=" + fetchActions + "&dat=" + startDate + "," + endData,
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

			if ($.inArray("Overview", _fetchAction) > -1) {
				var arrayofCountry = [['Country', 'Popularity']];
				var htmlBody = '';
				i = 1;
				$("#tbl_Country > tbody").empty();
				for (myCols in recv.metrics.Country) {
					arrayofCountry.push([recv.metrics.Country[myCols].c1, parseInt(recv.metrics.Country[myCols].c2)]);
					htmlBody += ('<tr><td>' + i + '</td><td>' + recv.metrics.Country[myCols].c1 + '</td><td style="text-align: center;">' + addCommas(recv.metrics.Country[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.Country[myCols].c3 + '%</td></tr>');
					i++;
				}
				$('#tbl_Country').append(htmlBody);
				redrawRegionsMap(arrayofCountry);

				i = 1;
				htmlBody = '';
				$("#tbl_City > tbody").empty();
				for (myCols in recv.metrics.City) {
					htmlBody += ('<tr><td>' + i + '</td><td>' + recv.metrics.City[myCols].c1 + '</td><td style="text-align: center;">' + addCommas(recv.metrics.City[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.City[myCols].c3 + '%</td></tr>');
					i++;
				}
				$('#tbl_City').append(htmlBody);

				i = 1;
				htmlBody = '';
				$("#tbl_WebRefer > tbody").empty();
				for (myCols in recv.metrics.WebRefer) {
					htmlBody += ('<tr><td>' + i + '</td><td><a href="' + recv.metrics.WebRefer[myCols].c1 + '" target="_blank">' + (recv.metrics.WebRefer[myCols].c1).substring(0, 100) + '</a></td><td style="text-align: center;">' + addCommas(recv.metrics.WebRefer[myCols].c2) + '</td><td style="text-align: center;">' + recv.metrics.WebRefer[myCols].c3 + '%</td></tr>');
					i++;
				}
				$('#tbl_WebRefer').append(htmlBody);

				$("#TotalUsers").text("Total visitors " + addCommas(recv.metrics.Total.c1) + ", unique visitors " + addCommas(recv.metrics.Total.c2));

				var FirstSeenDate = recv.metrics.Seen[0].c1.split(" ")[0].split(".");
				FirstSeenDate = FirstSeenDate[1] + "/" + FirstSeenDate[0] + "/" + FirstSeenDate[2];
				var timeDiff = Math.abs((new Date()).getTime() - (new Date(FirstSeenDate)).getTime());
				var sec = ((new Date()).getTime() / 1000.0) - ((new Date(FirstSeenDate)).getTime() / 1000.0);
				var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

				function TimeDifferenceCounter(sec) {
					var t = parseInt(sec);
					var years; var months; var days;
					if (t > 31556926) {
						years = parseInt(t / 31556926); t = t - (years * 31556926);
					}
					if (t > 2629743) {
						months = parseInt(t / 2629743); t = t - (months * 2629743);
					}
					if (t > 86400) {
						days = parseInt(t / 86400); t = t - (days * 86400);
					}
					var hours = parseInt(t / 3600);
					t = t - (hours * 3600);
					var minutes = parseInt(t / 60);
					t = t - (minutes * 60);

					return hours;
				}
				$("#FirstSeenDate").text(recv.metrics.Seen[0].c1);
				$("#SinceOnline").text(diffDays + " days " + TimeDifferenceCounter(sec) + " hours");


				var d1 = [];
				var d2 = [];
				//var plotTicks = [];
				var ymin = 0;
				var ymax = 0;
				var UniqueVisitors = [];
				var TotalVisitors = [];
				var NewVisitors = [];
				var SumOfTotalVisitors = 0;
				var SumOfUniqueVisitors = 0;
				var SumOfNewVisitors = 0;

				i = 0;
				for (myCols in recv.metrics.TotalUsers) {
					TotalVisitors.push(parseInt(recv.metrics.TotalUsers[myCols].c2));
					UniqueVisitors.push(parseInt(recv.metrics.TotalUsers[myCols].c3));
					NewVisitors.push(parseInt(recv.metrics.TotalUsers[myCols].c4));
					SumOfTotalVisitors += parseInt(recv.metrics.TotalUsers[myCols].c2);
					SumOfUniqueVisitors += parseInt(recv.metrics.TotalUsers[myCols].c3);
					SumOfNewVisitors += parseInt(recv.metrics.TotalUsers[myCols].c4);

					d1.push([parseInt(recv.metrics.TotalUsers[myCols].c1 + "000"), parseInt(recv.metrics.TotalUsers[myCols].c2)]);
					d2.push([parseInt(recv.metrics.TotalUsers[myCols].c1 + "000"), parseInt(recv.metrics.TotalUsers[myCols].c3)]);
					//d1.push([i, parseInt(recv.metrics.TotalUsers[myCols].c2)]);
					//d2.push([i, parseInt(recv.metrics.TotalUsers[myCols].c3)]);
					//plotTicks.push([i, parseInt(recv.metrics.TotalUsers[myCols].c1)]);
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
						//minTickSize: [1, "month"]
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

			/* Sparklines */
			var SitePlayedvalues = [];
			myCols = null;
			for (myCols in recv.metrics.SitePlayed) {
				SitePlayedvalues.push(recv.metrics.SitePlayed[myCols].c1);
			}
			if (!isNull(recv.metrics.SitePlayed) && !isNull(myCols)) {
				if (recv.metrics.SitePlayed.length == 1) {
					SitePlayedvalues.splice(0, 0, 0);
				}
				$("#totalSitePlayed").html('<span class="label label-success">' + addCommas((typeof myCols == 'undefined' ? 0 : recv.metrics.SitePlayed[myCols].c2)) + '</span>');
				$("#graphSitePlayed").sparkline(SitePlayedvalues, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#totalSitePlayed").html('<span class="label label-success">0</span>');
				$("#graphSitePlayed").html('No data yet');
			}

			var TimePlayedvalues = [];
			myCols = null;
			for (myCols in recv.metrics.AvgTimePlayed) {
				TimePlayedvalues.push(parseInt(recv.metrics.AvgTimePlayed[myCols].c1 / 60));
			}
			if (!isNull(recv.metrics.AvgTimePlayed) && !isNull(myCols)) {
				if (recv.metrics.AvgTimePlayed.length == 1) {
					TimePlayedvalues.splice(0, 0, 0);
				}
				$("#totalTimePlayed").html('<span class="label label-success">' + number2Date((typeof myCols == 'undefined' ? 0 : recv.metrics.AvgTimePlayed[myCols].c2)) + '</span>');
				$("#graphTimePlayed").sparkline(TimePlayedvalues, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#totalTimePlayed").html('<span class="label label-success">0</span>');
				$("#graphTimePlayed").html('No data yet');
			}

			if (!isNull(TotalVisitors)) {
				if (TotalVisitors.length == 1) {
					TotalVisitors.splice(0, 0, 0);
				}
				$("#totalVisitors").html('<span class="label label-success">' + addCommas(SumOfTotalVisitors) + '</span>');
				$("#graphVisitors").sparkline(TotalVisitors, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#totalVisitors").html('<span class="label label-success">0</span>');
				$("#graphVisitors").html('No data yet');
			}

			if (!isNull(UniqueVisitors)) {
				if (UniqueVisitors.length == 1) {
					UniqueVisitors.splice(0, 0, 0);
				}
				$("#uniqueVisitors").html('<span class="label label-success">' + addCommas(SumOfUniqueVisitors) + '</span>');
				$("#graphUniqueVisitors").sparkline(UniqueVisitors, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#uniqueVisitors").html('<span class="label label-success">0</span>');
				$("#graphUniqueVisitors").html('No data yet');
			}

			if (!isNull(NewVisitors)) {
				if (NewVisitors.length == 1) {
					NewVisitors.splice(0, 0, 0);
				}
				$("#newVisitors").html('<span class="label label-success">' + addCommas((SumOfNewVisitors / SumOfTotalVisitors * 100).toFixed(2)) + '</span>');
				$("#graphNewVisitors").sparkline(NewVisitors, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#uniqueVisitors").html('<span class="label label-success">0</span>');
				$("#graphUniqueVisitors").html('No data yet');
			}

			var Bouncevalues = [];
			myCols = null;
			for (myCols in recv.metrics.Bounce) {
				Bouncevalues.push(recv.metrics.Bounce[myCols].c1);
			}
			if (!isNull(recv.metrics.Bounce) && !isNull(myCols)) {
				if (recv.metrics.Bounce.length == 1) {
					Bouncevalues.splice(0, 0, 0);
				}
				$("#bounceRate").html('<span class="label label-success">' + addCommas((typeof myCols == 'undefined' ? 0 : recv.metrics.Bounce[myCols].c2.toFixed(2))) + '</span>');
				$("#graphBounce").sparkline(Bouncevalues, {
					type: 'line',
					width: '100px',
					height: '20px',
					drawNormalOnTop: false
				});
			} else {
				$("#bounceRate").html('<span class="label label-success">0</span>');
				$("#graphBounce").html('No data yet');
			}

			var newuser_data = [
				{ label: "Returning", data: (((SumOfTotalVisitors - SumOfNewVisitors) / SumOfTotalVisitors * 100) == 0 ? 0.0001 : ((SumOfTotalVisitors - SumOfNewVisitors) / SumOfTotalVisitors * 100)), color: "#2091CF" },
				{ label: "New", data: ((SumOfNewVisitors / SumOfTotalVisitors * 100) == 0 ? 0.0001 : (SumOfNewVisitors / SumOfTotalVisitors * 100)), color: "#68BC31" }
				];

			$.plot($("#audience_newusers"), newuser_data,
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

			drawBreadCrumb();
			waitDialog.hidePleaseWait();
		} //onDataReceived
	}
	setTimeout(fetchData, 1000);

});