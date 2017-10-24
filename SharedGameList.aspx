<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="SharedGameList.aspx.cs" Inherits="analytcs_SharedGameList" %>
<%@ Register src="ctrlDataGrid.ascx" tagname="ctrlDataGrid" tagprefix="uc1" %>
<%@ Register src="ctrlYourGames.ascx" tagname="ctrlYourGames" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="YourGamesContentPlaceHolder" runat="Server">
    <uc2:ctrlYourGames ID="CtrlYourGames" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <ul class="breadcrumb" id="BreadCrumb" autoupdate="false">
        <li>Manage Shared Games <span class="divider"></span></li>
        <li>Games <span class="divider"></span></li>
        <li>List<span class="divider"></span></li>
    </ul>

    <div class="row-fluid">
        <div class="col-md-12">

            <div class="form-horizontal">
            <fieldset>

            <!-- Form Name -->
            <legend>Shared Games</legend>

                <table class="table table-hover table-bordered" id="tbl_mypartnerslist">
                    <thead>
                        <tr>
                            <th style="text-align: center;" width="5%">
                                #
                            </th>
                            <th style="text-align: center;" width="15%">
                                Added Date
                            </th>
                            <th width="20%">
                                Game Title
                            </th>
                            <th width="25%">
                                User Name
                            </th>
                            <th width="30%">
                                Email
                            </th>
                            <th style="text-align: center;" width="10%">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptSharedGames" runat="server" OnItemCreated="rptSharedGames_Item_Created">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right;"><%# Container.ItemIndex + 1 %></td><td style="text-align: center;"><%# Eval("AddedDate").ToString() %></td><td><%# Eval("Title").ToString()%></td><td><%# Eval("FullName").ToString()%></td><td><%# Eval("Email").ToString()%></td><td><a class="btn btn-xs sharedgames" href="#" gid="<%# Eval("GameId").ToString()%>" suid="<%# Eval("SharedUserId").ToString()%>"><i class="glyphicon glyphicon-trash"></i></a></td>
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
    <script type='text/javascript' src='js/FGSSharedGames.js'></script>
</asp:Content>


