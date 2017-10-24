<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="AddPartner.aspx.cs" Inherits="analytcs_AddPartner" %>
<%@ Register src="ctrlDataGrid.ascx" tagname="ctrlDataGrid" tagprefix="uc1" %>
<%@ Register src="ctrlYourGames.ascx" tagname="ctrlYourGames" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="false">
        <li>Manage Partners <span class="divider"></span></li>
        <li>Partners <span class="divider"></span></li>
        <li>Add New<span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
            <fieldset>

            <!-- Form Name -->
            <legend>Partner Form</legend>

            <!-- Text input-->
            <div class="form-group">
              <label class="control-label" for="partnername">Partner Name</label>
              <div class="controls">
                <input id="partnername" name="partnername" type="text" placeholder="fgs" class="form-control">
                <p class="help-block">name of your partner</p>
              </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
              <label class="control-label" for="email">Email</label>
              <div class="controls">
                <input id="email" name="email" type="text" placeholder="@" class="form-control">
                <p class="help-block">email address should be valid to activate partner account</p>
              </div>
            </div>

            <!-- Password input-->
            <div class="form-group">
              <label class="control-label" for="partnerpassword">Password</label>
              <div class="controls">
                <input id="partnerpassword" name="partnerpassword" type="password" placeholder="*" class="form-control">
    
              </div>
            </div>

            <!-- Password input-->
            <div class="form-group">
              <label class="control-label" for="repartnerpassword">Re-Password</label>
              <div class="controls">
                <input id="repartnerpassword" name="repartnerpassword" type="password" placeholder="*" class="form-control">
    
              </div>
            </div>

            <!-- Button -->
            <div class="form-group">
              <label class="control-label" for="addnewpartner"></label>
              <div class="controls">
                <button id="addnewpartner" name="addnewpartner" class="btn btn-danger">Add Partner</button>
              </div>
            </div>

            </fieldset>
            </div>

        </div>
        <!--/span-->
    </div>
    <!--/row-->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSPlaceHolder" runat="Server">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
</asp:Content>
