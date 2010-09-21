using System;
using System.Linq;
using GG.DonateAnywhere.Core.PageAnalysis;

namespace GG.DonateAnywhere.Core.Sanitise
{
    public class ContentCleaner
    {       
        private readonly IExcludedWordsRepository _excludedWordsRepository;

        public ContentCleaner()
            : this(new AssemblyResourceExcludedWordsRepository("GG.DonateAnywhere.Core.PageAnalysis.blacklist.txt"))
        {
        }

        public ContentCleaner(IExcludedWordsRepository excludedWordsRepository)
        {
            _excludedWordsRepository = excludedWordsRepository;
        }

        public string ClenseSourceData(string plainText)
        {
            plainText = plainText.ToLower().Trim();
            plainText = FilterCommonWords(plainText);
            return plainText;
        }

        public string FilterCommonWords(string source)
        {
            source = RemoveSpecialCharacters(source);
            var exclusionList = _excludedWordsRepository.RetrieveExcludedWords();

            var words = source.Split(' ').ToList();
            foreach (var blacklistedWord in exclusionList)
            {
                var word = blacklistedWord;
                words.RemoveAll(s => s == word);
            }

            return words.Aggregate("", (current, item) => current + (item + " ")).Trim();
        }

        public string RemoveSpecialCharacters(string source)
        {
            source = source.Replace(Environment.NewLine, " ");
            source = source.Replace(".", "");
            source = source.Replace(",", "");
            source = source.Replace(";", "");
            source = source.Replace("'", "");
            source = source.Replace("[", "");
            source = source.Replace("]", "");
            source = source.Replace("(", "");
            source = source.Replace(")", "");
            source = source.Replace("{", "");
            source = source.Replace("}", "");
            source = source.Replace("|", "");
            source = source.Replace("-", "");
            source = source.Replace("`", "");
            source = source.Replace("_", "");
            source = source.Replace("+", "");
            source = source.Replace("=", "");
            source = source.Replace("%", "");
            source = source.Replace("^", "");
            source = source.Replace("&", "");
            source = source.Replace("*", "");
            source = source.Replace("\\", "");
            source = source.Replace("/", "");
            source = source.Replace("?", "");
            source = source.Replace("~", "");
            source = source.Replace("#", "");
            source = source.Replace("!", "");
            source = source.Replace("\"", "");
            source = source.Replace("£", "");
            source = source.Replace("$", "");
            source = source.Replace(":", "");
            source = source.Replace("<", "");
            source = source.Replace(">", "");
            source = source.Replace("0", "");
            source = source.Replace("1", "");
            source = source.Replace("2", "");
            source = source.Replace("3", "");
            source = source.Replace("4", "");
            source = source.Replace("5", "");
            source = source.Replace("6", "");
            source = source.Replace("7", "");
            source = source.Replace("8", "");
            source = source.Replace("9", "");
            return source;
        }
    }
}
