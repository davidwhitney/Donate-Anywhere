<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Iframe.Master" Inherits="System.Web.Mvc.ViewPage<GG.DonateAnywhere.Core.DonateAnywhereResult>" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

Results:<br />

<% if(Model.RequestContext.ShowResultsPage) {%>
Here's a link to the landing page which is just like the request link!<br />
<a href="/LandingPage?Keywords=<%: string.Join(",", Model.Keywords) %>" target="_parent">Donate</a> using JGDonateAnywhere
<%} %>
<% if(!Model.RequestContext.ShowResultsPage) {%>
Skipping results page - here's a single link...<br />
<a href="<%=Model.Results[0].Url %>">Donate To <%:Model.Results[0].Title %></a> using JGDonateAnywhere
<%} %>

</asp:Content>

