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
		show: false
	},
	xaxis: {
		mode: "categories",
		tickLength: 0
	},
	yaxis: {
		autoscaleMargin: 2
	}
};
var data = [[[0, 0]]];
var newuser_data = [
	{ label: "Returning Visitor", data: 1.0, color: "#2091CF" },
	{ label: "New Visitor", data: 1.0, color: "#68BC31" }
	];
var change_blue_data = [[0, 0], [1, 0], [2, 0], [3, 0], [4, 0], [5, 0], [6, 0], [7, 0], [8, 0], [9, 0]];
var change_red_data = [[0, 0], [1, 0], [2, 0], [3, 0], [4, 0], [5, 0], [6, 0], [7, 0], [8, 0], [9, 0]];
var fetchTimer = 0;
var plotCount = 1;
var realtime_plotCount = 10;
var ServerPath = "gamedistribution.com";


$(document).ready(function () {
	// Your Games
	$('#cmb_YourGames').change(function () {
		GameId = $("#cmb_YourGames").val();
		ServerName = $("#cmb_YourGames option:selected").attr("sn");
		RegId = $("#cmb_YourGames option:selected").attr("reg");
		if ($("#BreadCrumb").attr("page") == "Dashboard") {
			gotoURL("RealTimeOverview.aspx?gid=" + GameId);
		}
		if ($("#BreadCrumb").attr("autoupdate") != "false") {
			setBreadCrumb($("#cmb_YourGames option:selected").text());
			data = [[[0, 0]]];
			plotCount = 1;
		}
	});

	// Menu
	$('.menu-left').click(function (event) {
		// Prevent link opening        
		event.preventDefault();
		var url = $(event.currentTarget).attr("href");
		if (GameId == "all") {
			GameId = $("#cmb_YourGames option:eq(1)").val();
		}
		url = buildQueryString(url, "gid", GameId);
		gotoURL(url);
	});

	$('.menu-top').click(function (event) {
	    // Prevent link opening        
	    event.preventDefault();
	    GameId = $("#cmb_YourGames").val();
	    if (GameId == "all") {
	        bootbox.alert("Choose a game first!", function () {
	        });
	    } else {
	        var url = $(event.currentTarget).attr("href");
	        url = buildQueryString(url, "gid", GameId);
	        gotoURL(url);
	    }
	});

});