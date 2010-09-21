<%@ Page Title="" Language="C#" MasterPageFile="~/Views/LandingPage/LandingPage.Master" Inherits="System.Web.Mvc.ViewPage<GG.DonateAnywhere.Core.DonateAnywhereResult>" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
You have now landed on the selection page where we suggest a few things you might want to donate to<br />
<div style="float: left; width: 50%;">
<b>Suggested results</b><br />
    <ul>
    <%foreach (var result in Model.Results){%>
    <li><%:result.Title %> - <%=result.Description %> - <%:result.Url %></li>
    <%}%>
    </ul>
</div>
<div style="float: right; width: 50%;">
<b>You might also be interested in</b><br />
    <ul>
    <%foreach (var result in Model.RelatedResults){%>
    <li><%:result.Title %> - <%=result.Description %> - <%:result.Url %></li>
    <%}%>
    </ul>
</div>
<div style="clear: both;"></div>

</asp:Content>

