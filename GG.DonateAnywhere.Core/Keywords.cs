using System.Collections.Generic;
using System.Linq;

namespace GG.DonateAnywhere.Core
{
    public class Keywords : List<ExtractedKeyword>
    {
        public Keywords()
        {
        }

        public Keywords(IEnumerable<string> inner)
        {
            AddRange(inner.Select(keyword => new ExtractedKeyword(keyword)));
        }

        public List<string> ToListOfStrings()
        {
            return this.Select(item => item.Keyword).ToList();
        } 
    }
}