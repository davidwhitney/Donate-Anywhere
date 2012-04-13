<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GG.DonateAnywhere.Core.DonateAnywhereResult>" %>
Source data: <%: Model.RequestContext.SourceData%>
Keywords: <%: string.Join(", ",  Model.Keywords) %>
