using System;
using System.Collections.Generic;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereResult
    {
        public Keywords Keywords { get; set; }
        public IList<SearchResult> Results { get; set; }
        public IList<SearchResult> RelatedResults { get; set; }
        public IDonateAnywhereRequestContext RequestContext { get; set; }
    }
}