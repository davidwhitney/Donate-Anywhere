using System;
using System.Collections.Generic;
using System.Web;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereRequestContext : IDonateAnywhereRequestContext
    {
        public Uri UriToAnalyse { get; set; }
        public List<string> UserSuppliedKeywords { get; private set; }
        public bool ShowResultsPage { get; set; }
        public HttpRequestBase OriginalRequest { get; set; }

        public bool EnoughInformationToBuildSuggestions
        {
            get { return UriToAnalyse != null || UserSuppliedKeywords.Count > 0; }
        }

        public bool RequiresFullPageAnalysis
        {
            get { return UriToAnalyse != null; }
        }

        public string SourceData
        {
            get { return UriToAnalyse!=null ? UriToAnalyse.ToString() : string.Join(", ", UserSuppliedKeywords); }
        }

        public DonateAnywhereRequestContext()
        {
            UserSuppliedKeywords = new List<string>();
        }
    }
}