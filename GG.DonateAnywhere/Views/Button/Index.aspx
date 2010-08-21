<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Button/Button.Master" Inherits="System.Web.Mvc.ViewPage<GG.DonateAnywhere.Core.DonateAnywhereResult>" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if(Model.RequestContext.ShowResultsPage) {%>
<div class="donateAnywhere">
    <a class="donateViaLandingPage" href="/LandingPage?Keywords=<%: string.Join(",", Model.Keywords) %>" target="_parent">Donate using JGDonateAnywhere</a>
</div>
<%} %>
<% if(!Model.RequestContext.ShowResultsPage) {%>
<div class="donateAnywhere">
    <a class="donateDirectLink" href="<%=Model.Results[0].Url %>">Donate To <%:Model.Results[0].Title %> using JGDonateAnywhere</a>
</div>
<%} %>
</asp:Content>

