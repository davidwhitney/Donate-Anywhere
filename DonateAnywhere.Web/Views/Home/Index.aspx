<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/HomeAndDocs.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">
    
    <div class="hero-unit">
        <h1>DonateAnywhere</h1>
        <p>Powered by JustGiving, driven by the content you love to read.</p>
        <p><a class="btn btn-primary btn-large">Learn more &raquo;</a></p>
    </div>
    
    
    <div class="row">
    <div class="span4">
        <h2>For Consumers</h2>

        <p>You can start using the DonateAnywhere bookmarklet now!</p>
        <p>Just drag the buton belowto your bookmark toolbar, and if you ever feel moved enough by what you're reading to donate to the cause, just click it, from anywhere.
        <br /><br />
        <span id="bookmark"><a class="btn"  href="javascript:window.location='http://<%=HttpContext.Current.Request.Url.Host%><%=Url.Content("~/") %>LandingPage?UrlContext='+encodeURI(window.location);" name="bmklink">DonateAnywhere</a></span>
        </p>

    </div>
          

    <div class="span4">
        <h2>For Developers</h2>
        <p>You can embed it in your projects</p>
    </div>
          

    <div class="span4">
        <h2>For Content Providers</h2>
        <p>You can embed it in your CMS.</p>
    </div>
    </div>

    
</asp:Content>