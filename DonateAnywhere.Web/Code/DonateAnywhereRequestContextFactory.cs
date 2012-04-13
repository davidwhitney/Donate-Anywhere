using System;
using System.Linq;
using System.Web;
using GG.DonateAnywhere.Core;

namespace DonateAnywhere.Web.Code
{
    public class DonateAnywhereRequestContextFactory : IDonateAnywhereRequestContextFactory
    {
        public IDonateAnywhereRequestContext BuildContext(HttpContextBase httpContextBase)
        {
            var donateAnywhereRequestContext = new DonateAnywhereRequestContext();
            
            MapUrlToAnalyse(donateAnywhereRequestContext, httpContextBase);
            MapUserSuppliedKeywords(donateAnywhereRequestContext, httpContextBase);
            MapResultsPageBypass(donateAnywhereRequestContext, httpContextBase);
            MapRequest(donateAnywhereRequestContext, httpContextBase);

            return donateAnywhereRequestContext;
        }

        private static void MapUrlToAnalyse(IDonateAnywhereRequestContext donateAnywhereRequestContext, HttpContextBase httpContextBase)
        {
            donateAnywhereRequestContext.UriToAnalyse = httpContextBase.Request.UrlReferrer;
            if(httpContextBase.Request.QueryString.AllKeys.Contains("UrlContext"))
            {
                donateAnywhereRequestContext.UriToAnalyse = new Uri(httpContextBase.Request.QueryString["UrlContext"]);
            }
        }

        private static void MapUserSuppliedKeywords(DonateAnywhereRequestContext donateAnywhereRequestContext, HttpContextBase httpContextBase)
        {
            if(httpContextBase.Request.QueryString.AllKeys.Contains("Keywords"))
            {
                donateAnywhereRequestContext.UserSuppliedKeywords.AddRange(httpContextBase.Request.QueryString["Keywords"].Split(','));
            }
        }

        private static void MapResultsPageBypass(DonateAnywhereRequestContext donateAnywhereRequestContext, HttpContextBase httpContextBase)
        {
            if(!httpContextBase.Request.QueryString.ToString().ToLower().Contains("showresultspage=false"))
            {
                donateAnywhereRequestContext.ShowResultsPage = true;
            }
        }

        private static void MapRequest(DonateAnywhereRequestContext donateAnywhereRequestContext, HttpContextBase httpContextBase)
        {
            donateAnywhereRequestContext.OriginalRequest = httpContextBase.Request;
        }

    }
}