<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="BannerFilter.aspx.cs" Inherits="analytics_BannerFilter" %>

<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type="text/javascript" src="js/daterangepicker/date.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    
    <link href="css/bootstrap-switch.css" rel="stylesheet" />
    <script src="js/switch/bootstrap-switch.min.js"></script>
    
    <link href="css/gd-checkedlist.css" rel="stylesheet" />
    <script src="js/checkedlist/gd-checkedlist.js"></script>

    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script src="js/FGSBannerFilter.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="true">
        <li>Banner<span class="divider"></span></li>
        <li>Filter<span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
                <fieldset>

                    <!-- Form Name -->
                    <legend><input id="bannerEnable" type="checkbox" checked data-on-text="Enable" data-off-text="Disable"> <span id="gameTitle"></span></legend>

                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <div class="col-md-12" style="height:33px;">
                                        <div class="pull-left">Countries (<span id="countryCount">0</span>/<span id="countryTotal">0</span>)</div>
                                        <div class="pull-right"><input id="countryIncExc" type="checkbox" class="pull-right hidden" data-on-text="Include" data-off-text="Exclude"></div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="well" style="max-height: 400px;overflow: auto;padding:0px;">
        		                            <ul class="list-group checked-list-box" id="countries">
                                            </ul>
                                        </div>
                                        <small>Choose the country here we don't want to display ads on.</small>
                                    </div>
                                    <div class="col-md-12">
                                        <a id="btnSelectAllCountries" class="btn btn-info">Select All</a>
                                        <a id="btnDeselectAllCountries" class="btn btn-info">Deselect All</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12" style="height:33px;">
                                        <div class="pull-left">Domains</div>
                                        <div class="pull-right"><input id="domainIncExc" type="checkbox" class="pull-right hidden" data-on-text="Include" data-off-text="Exclude"></div>
                                    </div>
                                    <div class="col-md-12">
                                        <textarea id="domains" class="form-control" style="height:400px;"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <small>Put the domains here we don't want to display ads on. Use a line for each domain (ex: domain.com)</small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12" style="height:33px;">                                  
                                        <div class="pull-left">Games (<span id="gameCount">0</span>/<span id="gameTotal">0</span>)</div>
                                        <div class="pull-right">Apply all changes to selected games</div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="well" style="max-height: 400px;overflow: auto;padding:0px;">
        		                            <ul class="list-group checked-list-box" id="mygames">
                                            <asp:Repeater ID="rptYourGames" runat="server">
                                                <ItemTemplate>
                                                  <li class="list-group-item" value="<%#Eval("GameId")%>"><%#Eval("Title")%></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                            <a id="btnSelectAllGames" class="btn btn-info">Select All</a>
                                            <a id="btnDeselectAllGames" class="btn btn-info">Deselect All</a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="preRoll">Show PreRoll Banner</label>
                                <div class="col-sm-10">
                                    <input id="preRoll" type="checkbox" class="pull-right" checked data-on-text="Enable" data-off-text="Disable">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="preRoll">Show MidRoll Banners</label>
                                <div class="col-sm-3">
                                    <input id="midRoll" type="checkbox" data-on-text="Enable" data-off-text="Disable">
                                    <select id="showAfterTime" class="form-control" style="display:inline;width:inherit">
                                        <option value="0">0</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select> minutes
                                    <span class="help-block">Help: 0 means, when you call ShowBanner(), banner will not be shown.</span>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="pull-right">
                                <a href="#" id="btnApply" class="btn btn-primary">Apply Changes</a> 
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>

        </div>
        <!--/span-->
    </div>
    <!--/row-->
</asp:Content>
