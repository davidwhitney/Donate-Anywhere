<%@ Page Title="" Language="C#" MasterPageFile="Documentation.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

        <h1>Dynamic Button Control</h1>
        <h2>Usage examples</h2>
        <p>All usage examples can be combined.</p>
        <hr />

        <div>
            <h3>Button control which spiders the containing page</h3>
            
            <div class="leftCol">
                <p>&lt;iframe src="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>button" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>               
                <ul>
                    <li><a href="<%=Url.Content("~/TestPages/Button/SimpleTest.htm") %>">Simple test</a></li>
                    <li><a href="<%=Url.Content("~/TestPages/Button/Floods.htm") %>">Floods test page</a></li>
                    <li><a href="<%=Url.Content("~/TestPages/Button/BBC-Chernobyl.htm") %>">BBC Chernobyl story test</a></li>
                </ul>
            </div>
            <div class="rightCol">
                <p>
                    <img src="<%=Url.Content("~/Content/Documentation/Images/regular-button-usage.png")%>" alt="Screenshot example of regular button usage" />
                </p>
            </div>
            <div class="clear"></div>
        </div>
        
        <hr />

        <div>
            <h3>Button control which links to a landing page for specific keywords</h3>
            <div class="leftCol">            
                <p>&lt;iframe src="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>button?Keywords=Bacon,Cheese" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>
                <ul>
                    <li><a href="<%=Url.Content("~/TestPages/Button/KeywordOverload.htm")%>">Keyword overloading example</a></li>
                </ul>
            </div>
            <div class="rightCol">
                <p>
                    <img src="<%=Url.Content("~/Content/Documentation/Images/button-keyword-overload.png")%>" alt="Screenshot example of button usage with overloaded keyword" />
                </p>
            </div>
            <div class="clear"></div>
        </div>

        <hr />

        <div>
            <h3>Button control with Url override for spidering</h3>
            <div class="leftCol">            
                <p>&lt;iframe src="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>button?UrlContext=http%3A%2F%2Fwww.google.com" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>
            </div>
            <div class="rightCol">
            </div>
            <div class="clear"></div>
        </div>

        <hr />
        
        <div>
            <h3>Button control which links directly to the top result</h3>
            <div class="leftCol">
                <p>&lt;iframe src="http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>button?ShowResultsPage=false" scrolling="no" frameborder="0" height="85" width="200"&gt;&lt;/iframe&gt;</p>
                <ul>
                    <li><a href="<%=Url.Content("~/TestPages/Button/SkipResultsPage.htm")%>">Skpping results page "I'm feeling lucky" style link</a></li>
                </ul>
            </div>
            <div class="rightCol">
                <p>
                    <img src="<%=Url.Content("~/Content/Documentation/Images/button-direct-to-first-link.png")%>" alt="Screenshot example of button usage with overloaded keyword" />
                </p>
            </div>
            <div class="clear"></div>
        </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>