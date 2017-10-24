<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="AudienceSummary.aspx.cs" Inherits="analytcs_AudienceSummary" %>

<%@ Register Src="ctrlDataGrid.ascx" TagName="ctrlDataGrid" TagPrefix="uc1" %>
<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="false">
        <li>Audience <span class="divider"></span></li>
        <li>Games <span class="divider"></span></li>
        <li>Summary<span class="divider"></span></li>
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
            <div class="form-horizontal">
                <fieldset>
                    <!-- Form Name -->
                    <legend>Audience Summary</legend>
                    <table class="tablesorter" id="tbl_audiencesummary">
                        <thead>
                            <tr>
                                <th style="text-align: center;" width="5%">
                                    #
                                </th>
                                <th width="15%">
                                    First Seen Date
                                </th>
                                <th width="20%" class="filter-select filter-exact" data-placeholder="Pick a game">
                                    Game Title
                                </th>
                                <th width="15%">
                                    Total Visitors
                                </th>
                                <th width="15%">
                                    Unique Visitors
                                </th>
                                <th width="10%">
                                    Game Hosted
                                </th>
                                <th width="10%">
                                    Avg Time Played
                                </th>
                                <th width="10%">
                                    Bounce
                                </th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th colspan="8">
                                    <button type="button" class="btn first" id="exportPDF">
                                        PDF
                                    </button>
                                </th>
                        </tr>
                        </tfoot>
                        <%--<tfoot>
                            <tr>
                                <th colspan="7" class="pager form-horizontal">
                                    <button type="button" class="btn first">
                                        <i class="icon-step-backward"></i>
                                    </button>
                                    <button type="button" class="btn prev">
                                        <i class="icon-arrow-left"></i>
                                    </button>
                                    <span class="pagedisplay"></span>
                                    <!-- this can be any element, including an input -->
                                    <button type="button" class="btn next">
                                        <i class="icon-arrow-right"></i>
                                    </button>
                                    <button type="button" class="btn last">
                                        <i class="icon-step-forward"></i>
                                    </button>
                                    <select class="pagesize input-mini" title="Select page size">
                                        <option selected="selected" value="15">15</option>
                                        <option value="30">30</option>
                                        <option value="45">45</option>
                                        <option value="60">60</option>
                                    </select>
                                    <select class="pagenum input-mini" title="Select page number">
                                    </select>
                                </th>
                            </tr>
                        </tfoot>--%>
                        <tbody>                              
                        </tbody>
                    </table>
                </fieldset>
            </div>
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

	<!-- bootstrap widget theme -->
	<link rel="stylesheet" href="css/theme.bootstrap.css">
	<!-- tablesorter plugin -->
	<script src="js/table/jquery.tablesorter.js"></script>
	<!-- tablesorter widget file - loaded after the plugin -->
	<script src="js/table/jquery.tablesorter.widgets.js"></script>

	<!-- PDF -->
	<script type="text/javascript" src="js/jspdf/jspdf.js"></script>
	<script type="text/javascript" src="js/jspdf/libs/FileSaver.js/FileSaver.js"></script>
	<script type="text/javascript" src="js/jspdf/libs/Blob.js/Blob.js"></script>
	<script type="text/javascript" src="js/jspdf/libs/Blob.js/BlobBuilder.js"></script>

	<script type="text/javascript" src="js/jspdf/libs/Deflate/deflate.js"></script>
	<script type="text/javascript" src="js/jspdf/libs/Deflate/adler32cs.js"></script>

	<script type="text/javascript" src="js/jspdf/jspdf.plugin.addimage.js"></script>
	<script type="text/javascript" src="js/jspdf/jspdf.plugin.from_html.js"></script>
	<script type="text/javascript" src="js/jspdf/jspdf.plugin.ie_below_9_shim.js"></script>
	<script type="text/javascript" src="js/jspdf/jspdf.plugin.sillysvgrenderer.js"></script>
	<script type="text/javascript" src="js/jspdf/jspdf.plugin.split_text_to_size.js"></script>
	<script type="text/javascript" src="js/jspdf/jspdf.plugin.standard_fonts_metrics.js"></script>

    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSAudienceSummary.js'></script>
</asp:Content>
