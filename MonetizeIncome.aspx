<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="MonetizeIncome.aspx.cs" Inherits="analytcs_MonetizeIncome" %>

<%@ Register src="ctrlDataGrid.ascx" tagname="ctrlDataGrid" tagprefix="uc1" %>
<%@ Register src="ctrlYourGames.ascx" tagname="ctrlYourGames" tagprefix="uc2" %>
<%@ Register src="ctrlPager.ascx" tagname="ctrlPager" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb">
        <li>Monetize <span class="divider"></span></li>
        <li>Incomes <span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">
            <div id="reportrange" class="pull-right" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                <i class="icon-calendar icon-large"></i>
                <span></span> <b class="caret" style="margin-top: 8px"></b>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-12">
            <div style="padding: 40px;">
                <div id="monetize-chart" style="height: 300px;">
                </div>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-info">
              <h4>View</h4> <strong id="BannerView">Calculating...</strong>
            </div>
        </div>
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-warning">
              <h4>Click</h4> <strong id="BannerClick">Calculating...</strong>
            </div>
        </div>
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-info">
              <h4>CTR</h4> <strong id="BannerCTR">Calculating...</strong>
            </div>
        </div>
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-info">
              <h4>CPC</h4> <strong id="BannerCPC">Calculating...</strong>
            </div>
        </div>
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-info">
              <h4>RPM</h4> <strong id="BannerRPM">Calculating...</strong>
            </div>
        </div>
        <div class="col-md-2">
            <div class="alert alert-dismissable alert-success">
              <h4>Estimated Earnings</h4> <strong id="BannerEarn">Calculating...</strong>
            </div>
        </div>
        <!--/span-->
    </div>
    <div class="row-fluid">
        <div class="col-md-12">
            <uc1:ctrlDataGrid ID="ctrlMonetizeIncome" Title="Income" TableName="MonetizeIncome" TableColumnNames="Date,Views,Clicks,Click Through Rate (CTR),Cost Per Click (CPC),Rate Per Impression (RPM),Estimated Earnings" TableColumnWidths="13%,13%,13%,13%,13%,13%,13%" runat="server" />
            <uc3:ctrlPager ID="ctrlPager" PagerName="MonetizeIncomePager" runat="server" />
        </div>
        <!--/span-->
    </div>
    <!--/row-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type="text/javascript" src="js/daterangepicker/date.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSMonetizeIncomes.js'></script>
</asp:Content>
