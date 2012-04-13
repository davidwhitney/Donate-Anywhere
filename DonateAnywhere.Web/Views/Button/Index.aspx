﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Button/Button.Master" Inherits="System.Web.Mvc.ViewPage<GG.DonateAnywhere.Core.DonateAnywhereResult>" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if(Model.RequestContext.ShowResultsPage) {%>
<a href="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>LandingPage?Keywords=<%: string.Join(",", Model.Keywords) %>" target="_parent" class="donateAnywhereButton"><span>Make a difference</span><br /><span>Donate Now</span></a>
<%} %>
<% if(!Model.RequestContext.ShowResultsPage) {%>
<a  href="http://www.justgiving.com/donation/direct/charity/<%:Model.Results[0].CharityId%>" target="_parent" class="donateAnywhereButton"><span>Donate To <%:Model.Results[0].Title %></span></a>
<%} %>
</asp:Content>

