<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="AudienceOverview.aspx.cs" Inherits="analytcs_AudienceOverview" %>

<%@ Register Src="ctrlDataGrid.ascx" TagName="ctrlDataGrid" TagPrefix="uc1" %>
<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb">
        <li>Audience <span class="divider"></span></li>
        <li>Overview <span class="divider"></span></li>
    </ul>
    <div class="row-fluid">
        <div class="col-md-12">
            <div id="reportrange" class="pull-right" style="background: #fff; cursor: pointer;
                padding: 5px 10px; border: 1px solid #ccc">
                <i class="icon-calendar icon-large"></i><span></span><b class="caret" style="margin-top: 8px">
                </b>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-12">
            <div style="padding: 40px;">
                <div id="audience-chart" style="height: 300px;">
                </div>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-12">
            <div class="alert alert-info" id="TotalUsers">
                Calculating total and unique users...
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-8">
            <div class="row-fluid">
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>First Seen</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="FirstSeenDate" class="label label-warning"><small>Loading..</small></span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Visitors</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphVisitors"><small>Loading..</small></span> <span id="totalVisitors">
                            </span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Average Time (minutes)</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphTimePlayed"><small>Loading..</small></span> <span id="totalTimePlayed">
                            </span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Game Hosted by Sites</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphSitePlayed"><small>Loading..</small></span> <span id="totalSitePlayed">
                            </span>
                        </p>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Online Since</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="SinceOnline" class="label label-warning"><small>Loading..</small></span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Unique Visitors</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphUniqueVisitors"><small>Loading..</small></span> <span id="uniqueVisitors">
                            </span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>Avg Bounce (%)</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphBounce"><small>Loading..</small></span> <span id="bounceRate"></span>
                        </p>
                    </div>
                </div>
                <div class="col-md-3" style="border-right: 1px solid #ddd; margin-bottom: 15px;">
                    <div class="row-fluid text-center">
                        <small>New Visitors (%)</small>
                    </div>
                    <div class="row-fluid">
                        <p class="text-center">
                            <span id="graphNewVisitors"><small>Loading..</small></span> <span id="newVisitors">
                            </span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div id="audience_newusers" style="margin-left: auto; margin-right: auto; width: 250px;
                height: 210px;">
            </div>
            <!--/span-->
        </div>
        <div class="row-fluid">
            <div class="col-md-4">
                <uc1:ctrlDataGrid ID="ctrlTopCountries" Title="Top Countries" TableName="Country"
                    TableColumnNames="Country,Visitors,Percent" runat="server" />
            </div>
            <!--/span-->
            <div class="col-md-8">
                <uc1:ctrlDataGrid ID="CtrlTopReferSites" Title="Top Referer Sites" TableName="WebRefer"
                    TableColumnNames="Referer URL,Visitors,Percent" runat="server" />
            </div>
            <!--/span-->
        </div>
        <!--/row-->
        <div class="row-fluid">
            <div class="col-md-12">
            </div>
        </div>
        <div class="row-fluid">
            <div class="col-md-4">
                <uc1:ctrlDataGrid ID="ctrlTopCities" Title="Top Cities" TableName="City" TableColumnNames="City,Visitors,Percent"
                    runat="server" />
            </div>
            <!--/span-->
            <div class="col-md-8">
                <div class="thumbnail">
                    <div id="chart_div" style="height: 500px;">
                    </div>
                </div>
            </div>
            <!--/span-->
        </div>
        <!--/row-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type="text/javascript" src="js/daterangepicker/date.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="js/sparkline/jquery.sparkline.min.js"></script>
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSAudienceOverview.js'></script>
</asp:Content>
