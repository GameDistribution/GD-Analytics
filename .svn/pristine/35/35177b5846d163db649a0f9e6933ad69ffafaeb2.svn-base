<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="RealTimeOverview.aspx.cs" Inherits="analitcs_Default" %>

<%@ Register src="ctrlDataGrid.ascx" tagname="ctrlDataGrid" tagprefix="uc1" %>
<%@ Register src="ctrlYourGames.ascx" tagname="ctrlYourGames" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb">
        <li>Real-Time <span class="divider"></span></li>
        <li>Overview <span class="divider"></span></li>
    </ul>
    <div class="row-fluid">
        <div class="col-md-3">
            <div style="display: table; height:340px; width:100%;">
                <div style="display: table-cell; vertical-align:middle;">
                    <div class="row-fluid">
                        <div class="row-fluid text-center">
                            <h4>
                                Online Gamers</h4>
                        </div>
                        <div class="row-fluid text-center">
                            <h1 style="font-size: 600%" id="OnlineUsers">0</h1>
                        </div>
                        <div class="row-fluid text-center">
                            <h5>
                                Active Users Playing Game</h5>
                        </div>
                    </div>
                    <!--/row-->
                    <div class="row-fluid">
                        <div class="progress" style="height:25px;" id="ActiveUsersBar">
                        </div>
                    </div>
                    <!--/row-->
                </div>
            </div>
        </div>
        <!--/span-->
        <div class="col-md-7">
            <div style="padding-top: 40px; padding-bottom: 40px;">
                <div id="realtime-chart" style="height: 300px;"></div>
            </div>
        </div>
        <!--/span-->
        <div class="col-md-2">
            <div style="padding-top: 40px; padding-bottom: 40px;">
                <div id="realtime-change" style="height: 282px;"></div>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-4">
            <div class="row-fluid">
                <div class="col-md-12">
                    <div class="thumbnail">
                        <table class="table table-hover table-striped" id="tbl_VisitState">
                            <caption class="text-center">
                                <h4>New or Returning Visitors</h4>
                            </caption>
                            <thead>
                                <tr>
                                    <th colspan="4">
                                        <div id="realtime_newusers" style="margin-left:auto; margin-right:auto; width: 300px; height: 300px; "></div>
                                    </th>
                                </tr>
                                <tr>
                                    <th width="5%">
                                        #
                                    </th>
                                    <th width="65%">
                                        Times
                                    </th>
                                    <th style="text-align: center;" width="10%">
                                        Visitors
                                    </th>
                                    <th style="text-align: center;" width="20%">
                                        Percent
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>                
                    </div>
                </div>
                <!--/span-->
            </div>
            <div class="row-fluid">
                <div class="col-md-12">
                </div>
                <!--/span-->
            </div>
            <div class="row-fluid">
                <div class="col-md-12">
                    <uc1:ctrlDataGrid ID="ctrlTopCountries" Title="Top Countries" TableName="Country" TableColumnNames="Country,Visitors,Percent" runat="server" />
                </div>
                <!--/span-->
            </div>
        </div>
        <!--/span-->
		<div class="col-md-8">
			<div class="row-fluid">
				<div class="col-md-12">
					<uc1:ctrlDataGrid ID="CtrlTopReferSites" Title="Top Referer Sites" TableName="WebRefer" TableColumnNames="Referer URL,Visitors,Percent" runat="server" />                
				</div>
				<!--/span-->
			</div>
			<div class="row-fluid">
				<div class="col-md-12">
				</div>
			</div>
			<div class="row-fluid">
				<div class="col-md-12">
					<div class="thumbnail">
						<div id="chart_div" style="height: 500px;"></div>
					</div>
				</div>
				<!--/span-->
			</div>
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
            <uc1:ctrlDataGrid ID="ctrlTopCities" Title="Top Cities" TableName="City" TableColumnNames="City,Visitors,Percent" runat="server" />            
        </div>
        <!--/span-->
    </div>
    <!--/row-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSRealTimeOverview.js'></script>
</asp:Content>
