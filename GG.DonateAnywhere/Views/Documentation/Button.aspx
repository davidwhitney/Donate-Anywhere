<%@ Page Title="" Language="C#" MasterPageFile="Documentation.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

        <h1>Dynamic Button Control</h1>
        <h2>Usage examples</h2>

        <h3>Button control which spiders the containing page</h3>
        <p>&lt;iframe src="/button" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>

        <h3>Button control which links to a landing page for specific keywords</h3>
        <p>&lt;iframe src="/button?Keywords=Bacon,Cheese" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>

        <h3>Button control which links directly to the top result</h3>
        <p>&lt;iframe src="/button?ShowResultsPage=false" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>

        <p>All usage examples can be combined.</p>

        <h3>Individual Examples</h3>
        <p>

        <a href="/TestPages/Button/Floods.htm">Floods test page</a><br />
        <a href="/TestPages/Button/KeywordOverload.htm">Keyword overloading example</a><br />
        <a href="/TestPages/Button/SimpleTest.htm">Simple test</a><br />
        <a href="/TestPages/Button/SkipResultsPage.htm">Skpping results page "I'm feeling lucky" style link</a><br />
        <a href="/TestPages/Button/BBC-Chernobyl.htm">BBC Chernobyl story test</a><br />
        <a href="/TestPages/Button/LandingPageReferrerTest.htm">Landing Page Referrer Test</a>

        </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

