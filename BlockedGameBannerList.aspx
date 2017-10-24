<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="BlockedGameBannerList.aspx.cs" Inherits="analytics_BlockedGameBannerList" %>

<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type="text/javascript" src="js/daterangepicker/date.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <!-- Tablesorter: required for bootstrap -->
    <link rel="stylesheet" href="css/theme.bootstrap.css">
    <script type='text/javascript' src="js/table/jquery.tablesorter.js"></script>
    <script type='text/javascript' src="js/table/jquery.tablesorter.widgets.js"></script>
    <!-- Tablesorter: optional -->
    <link rel="stylesheet" href="css/jquery.tablesorter.pager.css">
    <script type='text/javascript' src="js/table/jquery.tablesorter.pager.js"></script>

    <link rel="stylesheet" href="css/pick-a-color-1.1.8.min.css">
    <script type='text/javascript' src="js/pickcolor/tinycolor-0.9.15.min.js"></script>
    <script type='text/javascript' src="js/pickcolor/pick-a-color-1.1.8.min.js"></script>

    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSBlockedGamesBanner.js'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="true">
        <li>Manage Banner Blocked Sites <span class="divider"></span></li>
        <li>Games <span class="divider"></span></li>
        <li>List<span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
                <fieldset>

                    <!-- Form Name -->
                    <legend>Ads Settings</legend>

                    <div class="panel panel-default">
                        <div class="panel-body">
                            <!-- Multiple Checkboxes (inline) -->
                            <div class="form-group">
                                <label class="control-label col-lg-2">Enable</label>
                                <div class="col-lg-10">
                                    <input type="checkbox" name="bannerEnable" id="bannerEnable" value="1">
                                    enables banner
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerWidth">Banner Width</label>
                                <div class="col-lg-2">
                                    <input id="bannerWidth" name="bannerWidth" type="text" placeholder="500" class="form-control" required="">
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerHeight">Banner Height</label>
                                <div class="col-lg-2">
                                    <input id="bannerHeight" name="bannerHeight" type="text" placeholder="350" class="form-control" required="">
                                </div>
                            </div>

                            <!-- Multiple Checkboxes (inline) -->
                            <div class="form-group">
                                <label class="control-label col-lg-2">Autosize</label>
                                <div class="col-lg-10">
                                        <input type="checkbox" name="bannerAS" id="bannerAS" value="1">
                                        fits your banner to Stage
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerTimeOut">Timeout</label>
                                <div class="col-lg-2">
                                    <input id="bannerTimeOut" name="bannerTimeOut" type="text" placeholder="15" class="form-control" required="">
                                    <span class="help-block">banner timeout second</span>
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerBGColor">Bg Color</label>
                                <div class="col-lg-10">
                                    <input id="bannerBGColor" name="bannerBGColor" type="text" placeholder="000000"  class="pick-a-color" required="">                                    
                                </div>
                            </div>


                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerDate">Config Update</label>
                                <div class="col-lg-10">
                                    <input id="bannerDate" name="bannerDate" type="text" placeholder="" class="form-control">
                                </div>
                            </div>

                            <!-- Button -->
                            <div class="form-group">
                                <label class="control-label col-lg-2" for="bannerSave"></label>
                                <div class="col-lg-10">
                                    <a href="#"  id="btnBannerSave" class="btn btn-success">Save</a>
                                </div>
                            </div>
                        </div>
                    </div>


                    <table class="tablesorter" id="tbl_blocksites">
                        <thead>
                            <tr>
                                <th style="text-align: center;" width="5%">#
                                </th>
                                <th style="text-align: center;" width="15%">Added Date
                                </th>
                                <th width="20%">Game Title
                                </th>
                                <th width="55%">Web Site
                                </th>
                                <th style="text-align: center;" width="10%">Action
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="pull-right">
                                Changes will not be applied unless click apply button. <a href="#" id="btnApply" class="btn btn-primary">Apply Changes</a>
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
