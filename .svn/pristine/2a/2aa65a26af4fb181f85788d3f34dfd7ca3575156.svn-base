var ServerName = $("#cmb_YourGames option:selected").attr("sn");
var RegId = $("#cmb_YourGames option:selected").attr("reg");
var GameId = $("#cmb_YourGames").val();

$(document).ready(function () {

    $('#cmb_YourGames').change(function () {
        if ($("#cmb_YourGames").val() == "all") {
            gotoURL("RealTimeDashboard.aspx");
        } else {
            GameId = $("#cmb_YourGames").val();
            ServerName = $("#cmb_YourGames option:selected").attr("sn");
            RegId = $("#cmb_YourGames option:selected").attr("reg");
        }
    });

     $('#exportPDF').click(function () {
        var doc = new jsPDF();
        var specialElementHandlers = {
            '#editor': function(element,renderer) {
                    return true;
                }
        };

        doc.setProperties({
	        title: 'Title',
	        subject: 'This is the subject',		
	        author: 'James Hall',
	        keywords: 'generated, javascript, web 2.0, ajax',
	        creator: 'MEEE'
        });

	    doc.fromHTML($('#tbl_audiencesummary').get(0), 15, 15, {
	        'width': 200,
            'height': 2000,
            'elementHandlers': specialElementHandlers
        });

        // Output as Data URI
        doc.save('Audience Summary.pdf');
     });

    // Global Settings
    var fetchActions = "Summary";
    var startDate = DateTimetoUnix(Date.today().add({ days: -2 }));
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
            var $row, row="";

            if ($.inArray("Summary", _fetchAction) > -1) {
                i = 1;
                $("#tbl_audiencesummary > tbody").empty();
                for (myCols in recv.metrics.Summary) {
                    row += '<tr><td style="text-align: center;">' + i + '</td><td style="text-align: center;">' + recv.metrics.Summary[myCols].c8 + '</td><td><a href="AudienceOverview.aspx?gid=' + recv.metrics.Summary[myCols].c7 + '">' + recv.metrics.Summary[myCols].c1 + '</a></td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c2) + '</td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c3) + '</td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c4) + '</td><td style="text-align: center;">' + number2Date(recv.metrics.Summary[myCols].c5) + '</td><td style="text-align: right;">' + recv.metrics.Summary[myCols].c6.toFixed(2) + '%</td></tr>';
                    //$('#tbl_audiencesummary').append('<tr><td style="text-align: center;">' + i + '</td><td><a href="AudienceOverview.aspx?gid=' + recv.metrics.Summary[myCols].c7 + '">' + recv.metrics.Summary[myCols].c1 + '</a></td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c2) + '</td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c3) + '</td><td style="text-align: right;">' + addCommas(recv.metrics.Summary[myCols].c4) + '</td><td style="text-align: center;">' + number2Date(recv.metrics.Summary[myCols].c5) + '</td><td style="text-align: center;">' + recv.metrics.Summary[myCols].c6.toFixed(2) + '%</td></tr>');
                    i++;
                }
                $row = $(row);
                $("#tbl_audiencesummary tbody").append(row);
                $("#tbl_audiencesummary").trigger("update");
                /*
                $('#tbl_audiencesummary')
                            .find('tbody').append($row)
                            .trigger('addRows', [$row])
                            .trigger("update");
                            */

                //createTable();
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
    /*    
    .tablesorterPager({

        // target the pager markup - see the HTML block below
        container: $(".pager"),

        // target the pager page select dropdown - choose a page
        cssGoto: ".pagenum",

        // remove rows from the table to speed up the sort of large tables.
        // setting this to false, only hides the non-visible rows; needed if you plan to add/remove rows with the pager enabled.
        removeRows: false,

        // starting page of the pager (zero based index)
        page: 0,

        // Number of visible rows - default is 10
        size: 15,

        // apply disabled classname to the pager arrows when the rows at either extreme is visible - default is true
        updateArrows: true,

        // output string - default is '{page}/{totalPages}';
        // possible variables: {page}, {totalPages}, {filteredPages}, {startRow}, {endRow}, {filteredRows} and {totalRows}
        output: '{startRow} - {endRow} / {filteredRows} ({totalRows})'

    });
    */
}