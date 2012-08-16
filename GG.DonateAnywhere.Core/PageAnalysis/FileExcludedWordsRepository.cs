using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class FileExcludedWordsRepository : IExcludedWordsRepository
    {        
        private readonly string _fileName;
        private readonly List<string> _cachedExcludedWordsList;

        public FileExcludedWordsRepository(string fileName)
        {
            _fileName = fileName;
            _cachedExcludedWordsList = LoadWordExclusionList();
        }

        private List<string> LoadWordExclusionList()
        {
            var contents = File.ReadAllText(_fileName);
            contents = contents.Replace("\n", "\r");
            contents = contents.Replace("\r\r", "\r");
            contents = contents.Replace(Environment.NewLine, "\r");
            return contents.Split('\r').ToList();
        }

        public IList<string> RetrieveExcludedWords()
        {
            return _cachedExcludedWordsList;
        }
    }
}