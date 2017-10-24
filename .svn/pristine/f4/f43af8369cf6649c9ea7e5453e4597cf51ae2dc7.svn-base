<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="BlockedGameList.aspx.cs" Inherits="analytics_BlockedGameList" %>

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

    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSBlockedGames.js'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="true">
        <li>Manage Blocked Games <span class="divider"></span></li>
        <li>Games <span class="divider"></span></li>
        <li>List<span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
                <fieldset>

                    <!-- Form Name -->
                    <legend>Blocked Games</legend>

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

