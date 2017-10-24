
$(function () {
    GameId = $("#cmb_YourGames").val();
    var ServerName = $("#cmb_YourGames option:selected").attr("sn");
    var RegId = $("#cmb_YourGames option:selected").attr("reg");

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
    
    $("#btnApply").click(function () {

        bootbox.dialog({
            message: "Are sure to apply?",
            title: "Apply Confirmation",
            buttons: {
                danger: {
                    label: "Yes!",
                    className: "btn-danger",
                    callback: function () {
                        fetchDoActionData("&act=ApplyBlockGameBanner&gid=" + GameId, onDataReceived);
                        function onDataReceived(recv) {
                            if (recv.code == 200) {
                            }
                            bootbox.alert(recv.message, function () {
                            });
                        }
                    }
                },
                cancel: {
                    label: "Close",
                    className: "btn-success",
                    callback: function () {

                    }
                }
            }
        });

    });   

    $("#btnBannerSave").click(function () {
        fetchDoActionData("&act=SaveBannerconfig&gid=" + GameId + "&width=" + $("#bannerWidth").val() + "&height="
            + $("#bannerHeight").val() + "&color=" + bgColor
            + "&timeout=" + $("#bannerTimeOut").val() + "&active=" + ($("#bannerEnable").is(':checked')?"1":"0") + "&autosize=" + ($("#bannerAS").is(':checked')?"1":"0"), onDataReceived);
        function onDataReceived(recv) {
            if (recv.code == 200) {
            }
            bootbox.alert(recv.message, function () {
            });
        }
    });
    var bgColor = "000000";
    $("#bannerBGColor").on("change", function () {
        bgColor = $(this).val();
    });

    function setButtonBlockedGames() {
        $(".blockedgames").click(function () {
            var gid = $(this).attr("gid");
            var website = $(this).attr("website");
            var tablerowId = $(this).parents('tr:first');
            var clickedElement = this;

            bootbox.dialog({
                message: "Are sure to delete?",
                title: "Delete Confirmation",
                buttons: {
                    danger: {
                        label: "Yes!",
                        className: "btn-danger",
                        callback: function () {
                            fetchDoActionData("&act=DelBlockedGameBanner&gid=" + gid + "&website=" + website, onDataReceived);
                            function onDataReceived(recv) {
                                if (recv == false) {
                                    gotoURL("/");
                                    return;
                                }

                                if (recv.code == 200) {
                                    $(clickedElement).remove();
                                }
                                bootbox.alert(recv.message, function () {
                                    $(tablerowId).animate({ backgroundColor: 'yellow' }, 1000).fadeOut(1000, function () {
                                        $(tablerowId).remove();
                                    });
                                });
                            }
                        }
                    },
                    cancel: {
                        label: "Close",
                        className: "btn-success",
                        callback: function () {

                        }
                    }
                }
            });

        });
    }

    function fetchDoActionData(param, onDataReceived) {
        fetchTimer = Math.round((new Date()).getTime() / 1000);
        $.ajax({
            url: "DoAction.ashx?t=" + fetchTimer + param,
            method: 'GET',
            dataType: 'json',
            success: onDataReceived
        });

    }

    // Global Settings
    var fetchActions = "BlockBannerSites,BannerConfig";
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
            if (recv == false) {
                gotoURL("/");
                return;
            }

            var i = 1;
            var myCols;
            var _fetchAction = fetchActions.split(',');
            var $row, row = "";

            if ($.inArray("BlockBannerSites", _fetchAction) > -1) {
                i = 1;
                for (myCols in recv.metrics.BlockSites) {
                    row += '<tr><td style="text-align: center;">' + i + '</td><td style="text-align: center;">' + recv.metrics.BlockSites[myCols].c4 + '</td><td>' + recv.metrics.BlockSites[myCols].c2 + '</a></td><td>' + recv.metrics.BlockSites[myCols].c3 + '</a></td><td><a class="btn btn-xs blockedgames" href="#" gid="' + recv.metrics.BlockSites[myCols].c1 + '" website="' + recv.metrics.BlockSites[myCols].c3 + '"><i class="glyphicon glyphicon-trash"></i></a></td></tr>';
                    i++;
                }
                $row = $(row);
                $("#tbl_blocksites > tbody").html(row);
                $("#tbl_blocksites").trigger("update");
                setButtonBlockedGames();
            }
            if ($.inArray("BannerConfig", _fetchAction) > -1) {
                $("#bannerBGColor").val(recv.metrics.BannerConfig.c2);
                bgColor = recv.metrics.BannerConfig.c2;
                $("#bannerEnable").attr("checked", (recv.metrics.BannerConfig.c7 == "True"));
                $("#bannerWidth").val(recv.metrics.BannerConfig.c3);
                $("#bannerHeight").val(recv.metrics.BannerConfig.c4);
                $("#bannerAS").attr("checked", (recv.metrics.BannerConfig.c6 == "True"));
                $("#bannerTimeOut").val(recv.metrics.BannerConfig.c5);
                $("#bannerDate").val(recv.metrics.BannerConfig.c9);
                $(".pick-a-color").pickAColor();
            }
            waitDialog.hidePleaseWait();
        } //onDataReceived
    }
    
    initTable();
    setTimeout(fetchData, 1000);
});


function initTable() {

    $.extend($.tablesorter.themes.bootstrap, {
        // these classes are added to the table. To see other table classes available,
        table: 'table table-bordered',
        header: 'bootstrap-header', // give the header a gradient background
        footerRow: '',
        footerCells: '',
        icons: '', // add "icon-white" to make them white; this icon class is added to the <i> in the header
        sortNone: 'bootstrap-icon-unsorted',
        sortAsc: 'icon-chevron-up',
        sortDesc: 'icon-chevron-down',
        active: '', // applied when column is sorted
        hover: '', // use custom css here - bootstrap class may not override it
        filterRow: '', // filter row class
        even: '', // odd row zebra striping
        odd: ''  // even row zebra striping
    });

    // call the tablesorter plugin and apply the uitheme widget
    $("table").tablesorter({
        // this will apply the bootstrap theme if "uitheme" widget is included
        // the widgetOptions.uitheme is no longer required to be set
        theme: "bootstrap",

        widthFixed: true,

        // initial sort order of the columns, example sortList: [[0,0],[1,0]],
        // [[columnIndex, sortDirection], ... ]
        sortList: [
        [2, 0]
        ],

        headerTemplate: '{content} {icon}', // new in v2.7. Needed to add the bootstrap icon!

        // widget code contained in the jquery.tablesorter.widgets.js file
        // use the zebra stripe widget if you plan on hiding any rows (filter widget)
        widgets: ["uitheme", "filter", "zebra"],

        widgetOptions: {
            // using the default zebra striping class name, so it actually isn't included in the theme variable above
            // this is ONLY needed for bootstrap theming if you are using the filter widget, because rows are hidden
            zebra: ["even", "odd"],

            // reset filters button
            filter_reset: ".reset"

            // set the uitheme widget to use the bootstrap theme class names
            // this is no longer required, if theme is set
            // ,uitheme : "bootstrap"

        }
    });
}