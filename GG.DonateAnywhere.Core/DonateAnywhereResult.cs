using System;
using System.Collections.Generic;
using GG.DonateAnywhere.Core.Searching;

namespace GG.DonateAnywhere.Core
{
    public class DonateAnywhereResult
    {
        public IList<string> Keywords { get; set; }
        public IList<SearchResult> Results { get; set; }
        public IDonateAnywhereRequestContext RequestContext { get; set; }
    }
}