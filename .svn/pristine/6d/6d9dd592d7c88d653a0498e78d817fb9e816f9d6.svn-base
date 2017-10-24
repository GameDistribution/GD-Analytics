<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="MonetizePaymentHistory.aspx.cs" Inherits="analytcs_MonetizePaymentSettings" %>

<%@ Register Src="ctrlDataGrid.ascx" TagName="ctrlDataGrid" TagPrefix="uc1" %>
<%@ Register Src="ctrlYourGames.ascx" TagName="ctrlYourGames" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="false">
        <li>Monetize <span class="divider"></span></li>
        <li>Payment <span class="divider"></span></li>
        <li>History <span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-lg-2">
            <div class="alert alert-dismissable alert-info">
                <h5>Last Payment</h5>
                <h4><strong><asp:Label runat="server" ID="earnings"></asp:Label></strong></h4>
                <asp:Label runat="server" ID="lastpaymentdate"></asp:Label>
            </div>
        </div>
        <div class="col-lg-2">
            <div class="alert alert-dismissable alert-warning">
                <h5>Default Account</h5>
                <h4><strong><asp:Label ID="accname" runat="server"></asp:Label></strong></h4>
                <asp:Label ID="accdetail" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
                <fieldset>

                    <!-- Form Name -->
                    <legend>Payment History</legend>

                    <table class="table table-hover table-bordered" id="tbl_mypartnerslist">
                        <thead>
                            <tr>
                                <th style="text-align: center;" width="5%">#
                                </th>
                                <th width="10%">Start Date
                                </th>
                                <th width="10%">End Date
                                </th>
                                <th width="30%">Detail
                                </th>
                                <th width="15%">Process Date
                                </th>
                                <th width="10%">Debit
                                </th>
                                <th width="10%">Credit
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptSharedGames" runat="server" OnItemCreated="rptMonetizePayments_Item_Created">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-right"><%# Container.ItemIndex + 1 %></td>
                                        <td class="text-center"><%# Utils.safeInt(Eval("ProcessStateId"))==1 ? Utils.safeStrDate(Eval("StartPeriod")) : "" %></td>
                                        <td class="text-center"><%# Utils.safeInt(Eval("ProcessStateId"))==1 ? Utils.safeStrDate(Eval("EndPeriod")) : "" %></td>
                                        <td><%# Eval("Process").ToString()%></td>
                                        <td class="text-center"><%# Utils.safeStrDate(Eval("ProcessDate"))%></td>
                                        <td class="text-right"><%# Utils.safeInt(Eval("ProcessStateId"))==1 ? String.Concat(Eval("Earnings").ToString()," ",Eval("Currency").ToString()):""%></td>
                                        <td class="text-right"><%# Utils.safeInt(Eval("ProcessStateId"))==3 ? String.Concat(Eval("Earnings").ToString()," ",Eval("Currency").ToString()):""%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
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
    <script type='text/javascript' src='js/FGSUtils.js'></script>
    <script type='text/javascript' src='js/FGSGlobals.js'></script>
    <script type='text/javascript' src='js/FGSMonetizePaymentSettings.js'></script>
</asp:Content>


