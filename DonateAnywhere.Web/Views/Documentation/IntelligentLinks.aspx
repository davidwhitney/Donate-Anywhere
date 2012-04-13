<%@ Page Title="" Language="C#" MasterPageFile="Documentation.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

        <h1>Intelligent Links</h1>

        <h2>Usage examples</h2>

        <div>
        <h3>General purpose link that spiders the referring Url</h3>
        <p>&lt;a href="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>LandingPage"&gt;Donate now&lt;/a&gt;</p>
        <ul>
            <li><a href="<%=Url.Content("~/TestPages/Button/LandingPageReferrerTest.htm")%>">Landing Page Referrer Test</a></li>
        </ul>
        </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
