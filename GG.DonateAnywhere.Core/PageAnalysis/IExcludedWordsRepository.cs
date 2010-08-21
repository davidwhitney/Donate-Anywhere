using System.Collections.Generic;
using System.Text;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public interface IExcludedWordsRepository
    {
        IList<string> RetrieveExcludedWords();
    }
}
