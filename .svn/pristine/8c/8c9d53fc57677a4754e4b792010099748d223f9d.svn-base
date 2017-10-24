<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="MonetizePaymentAddAccount.aspx.cs" Inherits="analytcs_MonetizePaymentAddAccount" %>

<%@ Register Src="ctrlDataGrid.ascx" TagName="ctrlDataGrid" TagPrefix="uc1" %>
<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>
<%@ Register Src="ctrlPager.ascx" TagName="ctrlPager" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb">
        <li>Monetize <span class="divider"></span></li>
        <li>Payment <span class="divider"></span></li>
        <li>Settings <span class="divider"></span></li>
        <li>Add Account <span class="divider"></span></li>
    </ul>

    <div class="form-horizontal">
        <fieldset>

            <!-- Form Name -->
            <legend>Add new payment method</legend>

            <div class="panel panel-default">
                <div class="panel-body">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="holdername" class="col-lg-3 control-label">Account Holder Name</label>
                            <div class="col-lg-4">
                                <input type="text" class="form-control" id="holdername" name="holdername" placeholder="Name">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="acctype" class="col-lg-3 control-label">Account Type</label>
                            <div class="col-lg-2">
                                <i class="icon-trash"></i>
                                <select class="form-control" id="acctype" name="acctype">
                                    <option value="1">Bank Account</option>
                                    <option value="2">Paypal Account</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group" id="typeIBAN">
                            <label for="iban" class="col-lg-3 control-label">IBAN</label>
                            <div class="col-lg-4">
                                <input type="text" class="form-control" id="iban" placeholder="IBAN">
                            </div>
                        </div>
                        <div class="form-group" style="display:none;" id="typeEmail">
                            <label for="iban" class="col-lg-3 control-label">Email</label>
                            <div class="col-lg-4">
                                <input type="text" class="form-control" id="Email" placeholder="Paypal account">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-3 col-lg-4">
                                <a href="#" id="btnSave" class="btn btn-success">Save</a>
                                <a href="MonetizePaymentSettings.aspx" id="btnCancel" class="btn btn-info">Cancel</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type="text/javascript" src="js/daterangepicker/date.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSMonetizePaymentAddAccount.js'></script>
</asp:Content>
