using System;
using System.Collections.Generic;
using System.Web;

namespace GG.DonateAnywhere.Core
{
    public interface IDonateAnywhereRequestContext
    {
        bool EnoughInformationToBuildSuggestions { get; }
        bool RequiresFullPageAnalysis { get; }
        Uri UriToAnalyse { get; set; }
        List<string> UserSuppliedKeywords { get; }
        string SourceData { get; }
        bool ShowResultsPage { get; set; }
        HttpRequestBase OriginalRequest { get; set; }
    }
}