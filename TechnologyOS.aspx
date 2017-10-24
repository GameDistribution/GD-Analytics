﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="TechnologyOS.aspx.cs" Inherits="analytcs_TechnologyOS" %>

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
        <li>Technology <span class="divider"></span></li>
        <li>OS <span class="divider"></span></li>
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
                <div id="technology-chart" style="height: 300px;">
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
        <div class="col-md-12">
            <uc1:ctrlDataGrid ID="ctrlTopBrowsers" Title="Operation Systems" TableName="OS" TableColumnNames="OS,Total,Percent" runat="server" />
            <uc3:ctrlPager ID="ctrlPager" PagerName="OSPager" runat="server" />
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
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSTechnologyOS.js'></script>
</asp:Content>
