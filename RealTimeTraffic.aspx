<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="RealTimeTraffic.aspx.cs" Inherits="analitcs_RealTimeTraffic" %>

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
        <li>Traffic Sources <span class="divider"></span></li>
    </ul>
    <div class="row-fluid">
        <div class="col-md-4">
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
        <div class="col-md-8">
            <div style="padding: 40px;">
                <div id="realtime-chart" style="height: 300px;">
                </div>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-12">
            <uc1:ctrlDataGrid ID="ctrlTopWebReferals" Title="Top Referals" TableName="WebRefer" TableColumnNames="WebRefer,Visitors,Percent" runat="server" />
        </div>
        <!--/span-->
    </div>
    <!--/row-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSRealTimeTraffic.js'></script>
</asp:Content>

