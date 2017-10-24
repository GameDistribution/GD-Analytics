<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlYourGames.ascx.cs" Inherits="analitcs_ctrlYourGames" %>
              <li class="nav-header">Your Games</li>
              <li>
                <select id="cmb_YourGames" class="selectpicker show-tick" data-style="btn-primary">
                    <option og="-1" value="all" reg="<%=SessionObjects.SessionUserRegId %>" sn="s1">All Games</option>
                <optgroup label="Your Games">
                <asp:Repeater ID="rptYourGames" runat="server">
                    <ItemTemplate>
                      <option og="1" value="<%#Eval("GameId")%>" <%# (Eval("GameId").ToString()==Utils._GET("gid")?"selected='selected'":"") %> reg="<%# Eval("RegId").ToString() %>" sn="<%#Eval("ServerName")%>"><%#Eval("Title")%></option>
                    </ItemTemplate>
                </asp:Repeater>
                </optgroup>
                <optgroup label="Shared Games">
                <asp:Repeater ID="rptSharedGames" runat="server">
                    <ItemTemplate>
                      <option og="0" value="<%#Eval("GameMD5Id")%>" <%# (Eval("GameMD5Id").ToString()==Utils._GET("gid")?"selected='selected'":"") %> sn="<%#Eval("ServerName")%>" reg="<%# Eval("RegId").ToString() %>"><%#Eval("Title")%></option>
                    </ItemTemplate>
                </asp:Repeater>
                </optgroup>
                </select>
              </li>