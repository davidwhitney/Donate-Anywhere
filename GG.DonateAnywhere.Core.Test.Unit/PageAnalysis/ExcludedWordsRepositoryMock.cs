using System.Collections.Generic;
using GG.DonateAnywhere.Core.PageAnalysis;

namespace GG.DonateAnywhere.Core.Test.Unit.PageAnalysis
{
    public class ExcludedWordsRepositoryMock : List<string>, IExcludedWordsRepository
    {
        public IList<string> RetrieveExcludedWords()
        {
            return this;
        }
    }
}