function getBarColor(i) {
    switch (i) {
        case 0: return "success";
            break;
        case 1: return "warning";
            break;
        case 2: return "danger";
            break;
        case 3: return "info";
            break;
        case 4: return "kivanc";
            break;
        default: return "success";
    }
}

var _BreadCrumbPath = "", _BreadCrumbPathIsSet = false;
function setBreadCrumb(pathName) {
    _BreadCrumbPath = pathName;
    _BreadCrumbPathIsSet = false;
}

function drawBreadCrumb() {
    if (!_BreadCrumbPathIsSet) {
        $("#BreadCrumb li:not(:first):not(:nth-child(2))").remove();
        $("#BreadCrumb").append('<li>' + _BreadCrumbPath + '</li>');
        _BreadCrumbPathIsSet = true;
    }
}

// ReBuild QueryString for Filter
function buildQueryString(url, changedKey, changedKeyValue) {
    //var sPageURL = window.location.search.substring(1);
    var _url = url.split('?');
    var tempQuery = _url[0] + "?";

    if (_url.count > 0) {
        _url = _url[1];
    } else {
        _url = "";
    }

    var sPageURL = _url;
    var sURLVariables = sPageURL.split('&');

    var i = 0;
    var keyFound = false;

    if (sPageURL.length > 0) {
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0].toLowerCase() == "page") {
                sParameterName[1] = "0";
            }
            tempQuery += sParameterName[0] + "=" + (sParameterName[0] == changedKey ? changedKeyValue : sParameterName[1]) + (i < sURLVariables.length - 1 ? "&" : "");
            if (sParameterName[0] == changedKey) {
                keyFound = true;
            }
            i++;
        }
        if (!keyFound) {
            tempQuery += "&" + changedKey + "=" + changedKeyValue;
        }
    }
    else {
        tempQuery += changedKey + "=" + changedKeyValue;
    }

    return tempQuery;
}

function gotoURL(url) {
    $(location).attr('href', url);
}

function prepareActions(fetchActions, fetchPages) {
    var _fetchActions = fetchActions.split(',');
    var i = 0;
    for (_fetchPages in fetchPages) {
        if (_fetchPages != "" || _fetchPages != null) {
            _fetchActions[i] += "|" + fetchPages[_fetchPages];
        }
        i++;
    }
    return _fetchActions.join();
}

function DateTimetoUnix(dt) {
    return Math.round((new Date(dt)).getTime() / 1000);
}

function GetGameName(GameId) {
    var result = "Unknown";
    $('#cmb_YourGames option').each(function () {
        if ($(this).attr('value') == GameId) {
            result = $(this).text();
            return;
        }
    });
    return result;
}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function UnixToDateTime(UNIX_timestamp) {
    var a = new Date(UNIX_timestamp * 1000);
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var year = a.getFullYear();
    var month = months[a.getMonth()];
    var date = a.getDate();
    var hour = a.getHours();
    var min = a.getMinutes();
    var sec = a.getSeconds();
    var time = date + ' ' + month + ' ' + year + ' ' + hour + ':' + min + ':' + sec;
    return time;
}

function UnixToDate(UNIX_timestamp) {
    var a = new Date(UNIX_timestamp * 1000);
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var year = a.getFullYear();
    var month = months[a.getMonth()];
    var date = a.getDate();
    var time = date + ' ' + month + ' ' + year;
    return time;
}

function UnixToDateWithDay(UNIX_timestamp) {
    var a = new Date(UNIX_timestamp * 1000);
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var days = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    var year = a.getFullYear();
    var month = months[a.getMonth()];
    var date = a.getDate();
    var time = days[a.getUTCDay()] + ', ' + date + ' ' + month + ' ' + year;
    return time;
}

function number2Date(unixtime) {
    var sec = unixtime % 60;
    var min = (unixtime / 60) % 60;
    var hour = (unixtime / 3600) % 60;
    return parseInt(hour)+":"+parseInt(min)+":"+parseInt(sec);
}

function isNull(val) {
    return (typeof val == 'undefined') || (val == null);
}

function getServerName(ServerName, ServerPath) {
    //return "www."+ServerName+"."+ServerPath;
    return "localhost:83";
}

function getRealtimeServerName() {
    //return "www."+ServerName+"."+ServerPath;
    //return "192.168.3.16:9801";
    return "www.s1.gamedistribution.com:8080";
}

var waitDialog;
waitDialog = waitDialog || (function () {
    var pleaseWaitDivTime; 
    var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h1 id="processingTitle">Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"></div></div></div></div></div></div>');
    return {
        showPleaseWait: function() {
            pleaseWaitDiv.modal();
            pleaseWaitDivTime = setTimeout(function(){/*$("#processingTitle").text("Time Out!");*/ setTimeout(function(){ pleaseWaitDiv.modal('hide'); },2000); }, 30000)
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
            clearTimeout(pleaseWaitDivTime);
        },

    };
})();


function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}
