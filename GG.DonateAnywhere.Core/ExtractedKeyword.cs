namespace GG.DonateAnywhere.Core
{
    public class ExtractedKeyword
    {
        public string Keyword { get; set; }

        public ExtractedKeyword(string keyword)
        {
            Keyword = keyword;
        }

        public override string ToString()
        {
            return Keyword;
        }

        /// <summary>
        /// Checks if the other string.ToLower() contains this string.ToLower()
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool FuzzyMatches(string other)
        {
            if(string.IsNullOrWhiteSpace(other))
            {
                return true;
            }

            if(string.IsNullOrWhiteSpace(Keyword))
            {
                return true;
            }

            return other.ToLower().Contains(Keyword.ToLower());
        }
    }
}