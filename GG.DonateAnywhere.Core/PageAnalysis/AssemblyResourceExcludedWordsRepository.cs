using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GG.DonateAnywhere.Core.PageAnalysis
{
    public class AssemblyResourceExcludedWordsRepository: IExcludedWordsRepository
    {
        private readonly string _resourceName;
        private readonly List<string> _cachedExcludedWordsList;

        public AssemblyResourceExcludedWordsRepository(string resourceName)
        {
            _resourceName = resourceName;
            _cachedExcludedWordsList = LoadWordExclusionList();
        }

        private List<string> LoadWordExclusionList()
        {
            var resourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(_resourceName);
            var reader = new StreamReader(resourceStream);
            var contents = reader.ReadToEnd();
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