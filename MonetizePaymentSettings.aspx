<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="MonetizePaymentSettings.aspx.cs" Inherits="analytcs_MonetizePaymentSettings" %>
<%@ Register src="ctrlDataGrid.ascx" tagname="ctrlDataGrid" tagprefix="uc1" %>
<%@ Register src="ctrlYourGames.ascx" tagname="ctrlYourGames" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="false">
        <li>Monetize <span class="divider"></span></li>
        <li>Payment <span class="divider"></span></li>
        <li>Settings <span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
            <fieldset>

            <!-- Form Name -->
            <legend>Payment Accounts</legend>

                <table class="table table-hover table-bordered" id="tbl_mypartnerslist">
                    <thead>
                        <tr>
                            <th style="text-align: center;" width="5%">
                                #
                            </th>
                            <th style="text-align: center;" width="10%">
                                Added Date
                            </th>
                            <th width="10%">
                                Default Account
                            </th>
                            <th width="20%">
                                Account Name
                            </th>
                            <th width="30%">
                                Detail
                            </th>
                            <th width="20%">
                                Account Type
                            </th>
                            <th style="text-align: center;" width="5%">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptSharedGames" runat="server" OnItemCreated="rptMonetizePayments_Item_Created">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right;"><%# Container.ItemIndex + 1 %></td>
                                    <td style="text-align: center;"><%# Eval("AddDate").ToString() %></td>
                                    <td class="text-center"><input type="radio" name="defaultAccount" class="defacc" value="<%# Eval("Id").ToString()%>" <%# (bool)Eval("DefaultAccount")?"checked=\"checked\"":"" %>></td>
                                    <td><%# Eval("AccountName").ToString()%></td><td><%# Eval("AccountDetail").ToString()%></td>
                                    <td><%# Eval("AccountType").ToString()%></td>
                                    <td><a class="btn btn-xs accountname" href="#" accid="<%# Eval("Id").ToString()%>"><i class="glyphicon glyphicon-trash"></i></a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

            </fieldset>
            </div>

            <div class="form-group">
                <div class="col-lg-12 text-right">
                    <a href="MonetizePaymentAddAccount.aspx" id="btnAddNewAccount" class="btn btn-info">Add New Account</a>                    
                    <a href="#" id="btnSetDefaultAccount" class="btn btn-success">Set Default Account</a>
                </div>
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


