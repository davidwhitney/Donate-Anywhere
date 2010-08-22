<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/HomeAndDocs.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">   
<h1>What is Donate Anywhere?</h1>
<p>
It's MAGIC.
</p>
<h1>For Consumers!</h1>
<p>You can start using the DonateAnywhere bookmarklet now!</p>
<p>Just drag the following link to your bookmark toolbar, and if you ever feel moved enough by what you're reading to donate to the cause, just click it, from anywhere.
<br /><br />
<span id="bookmark"><a href="javascript:window.location='http://localhost:3253/LandingPage?UrlContext='+encodeURI(window.location);" name="bmklink">DonateAnywhere</a></span>
</p>
<h1>For Developers!</h1>
</asp:Content>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="extranavigationitems" ContentPlaceHolderID="extranavigationitems" runat="server">
</asp:Content>
