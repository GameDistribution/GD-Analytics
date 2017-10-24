var GameId = $("#cmb_YourGames").val();
var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");

$(function () {

	// Global Settings
	var fetchActions = "AddPaymentAccount";
	var holdername = "";
	var accdetail = "";
	var acctype = "";

	$("#acctype").change(function(e){
		if ($("#acctype").val()=="1") {
			$("#typeIBAN").show();
			$("#typeEmail").hide();
		} else {
			$("#typeEmail").show();
			$("#typeIBAN").hide();
		}
	});

	$("#btnSave").click(function (e) {
		e.preventDefault();

		holdername = $("#holdername").val();
		accdetail = $("#acctype").val() == 1 ? $("#iban").val() : $("#Email").val();
		acctype = $("#acctype").val();

		if (holdername.length > 5 && accdetail.length > 15) {
			$(this).attr("disabled", true);
			setTimeout(fetchData, 1000);
		} else {
			$(this).removeAttr("disabled");
			bootbox.alert("Check your fields!", function () {
			});
		}
	});

	// Fetch Data
	function fetchData() {
	    fetchDoActionData("&act=" + fetchActions + "&holdername=" + encodeURIComponent(holdername) + "&accdetail=" + accdetail + "&acctype=" + acctype, onDataReceived);
	    function onDataReceived(recv) {
	        if (recv.code == 0) {
	            gotoURL("/");
	            return;
	        }
	        if (recv.code == 200) {
	            gotoURL("/MonetizePaymentSettings.aspx");
	            return;
	        } else {
	            $("#btnSave").removeAttr("disabled");
	            bootbox.alert("Check your fields!", function () {
				});
			}
		}
	}

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