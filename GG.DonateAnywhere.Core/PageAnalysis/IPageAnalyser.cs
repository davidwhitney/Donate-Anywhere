using System;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public interface IPageAnalyser
    {
        PageReport Analyse(Uri uri);
    }
}