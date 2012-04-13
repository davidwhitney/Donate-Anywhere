using System.Collections.Generic;
using System.Linq;

namespace GG.DonateAnywhere.Core.Searching
{
    public class SearchResults : List<SearchResult>
    {    
        public SearchResults()
        {
        }

        public SearchResults(IEnumerable<SearchResult> inner)
        {
            AddRange(inner);
        }

        public SearchResults Take(int limit)
        {
            var limitedSet = Enumerable.Take(this, limit).ToList();
            Clear();
            AddRange(limitedSet);
            return this;
        }

        public SearchResults BoostResultsWhichContainExactKeyword(Keywords keywords)
        {
            var topResults = new List<SearchResult>();
            var bottomResults = new List<SearchResult>();

            var boostedInsert = 0;

            if (keywords.Count < 1)
            {
                keywords.Insert(0, new ExtractedKeyword(string.Empty));
            }
            if (keywords.Count < 2)
            {
                keywords.Insert(1, new ExtractedKeyword(string.Empty));
            }
            if (keywords.Count < 3)
            {
                keywords.Insert(2, new ExtractedKeyword(string.Empty));
            }
            if (keywords.Count < 4)
            {
                keywords.Insert(3, new ExtractedKeyword(string.Empty));
            }

            foreach (var item in this)
            {
                if (keywords[0].FuzzyMatches(item.Title) && keywords[0].FuzzyMatches(item.Description))
                {
                    topResults.Insert(boostedInsert, item);
                    boostedInsert++;
                }
                else if (keywords[1].FuzzyMatches(item.Title) && keywords[1].FuzzyMatches(item.Description))
                {
                    topResults.Add(item);
                }
                else
                {
                    bottomResults.Add(item);
                }
            }

            topResults.AddRange(bottomResults);
            
            Clear();
            AddRange(topResults);

            return this;
        }


        public SearchResults RemoveResultsThatDontMention(Keywords keywords)
        {
            var filtered = this.Where(
                        item => keywords[0].FuzzyMatches(item.Title)
                                || keywords[1].FuzzyMatches(item.Title)
                                || keywords[2].FuzzyMatches(item.Title)
                                || keywords[3].FuzzyMatches(item.Title)
                                || keywords[0].FuzzyMatches(item.Description)
                                || keywords[1].FuzzyMatches(item.Description)
                                || keywords[2].FuzzyMatches(item.Description)
                                || keywords[3].FuzzyMatches(item.Description)).ToList();

            Clear();
            AddRange(filtered);

            return this;
        }

        public SearchResults RemoveAnyItemsThatAreAlsoIn(IEnumerable<SearchResult> otherCollection)
        {
            var relatedDictionary = this.ToDictionary(relatedItem => relatedItem.Title);
            foreach (var key in otherCollection.Select(item => item.Title).Where(relatedDictionary.ContainsKey))
            {
                relatedDictionary.Remove(key);
            }

            var filtered = relatedDictionary.Values.ToList();

            Clear();
            AddRange(filtered);

            return this;
        }

    }
}