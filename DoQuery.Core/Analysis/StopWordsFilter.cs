using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Analysis
{
    public class StopWordsFilter
    {

        /// <summary>
        /// Default English stop words.
        /// </summary>
        public static readonly string[] DefaultEnglishStopWords = new[]
        {
            "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in",
            "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the",
            "their", "then", "there", "these", "they", "this", "to", "was", "will", "with",
            "he", "she", "we", "you", "my", "your", "his", "her", "its", "our", "their",
            "me", "him", "us", "them", "who", "whom", "which", "what", "where", "when",
            "why", "how", "all", "any", "both", "each", "few", "more", "most", "other",
            "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than",
            "too", "very", "s", "t", "can", "will", "just", "don", "should", "now",
            "d", "ll", "m", "re", "ve", "y", "ain", "aren", "couldn", "didn", "doesn",
            "am","i","have", "has", "had", "having", "might", "must", "need", "ought", "shall",
        };

        /// <summary>
        /// Filters stop words from a list of tokens.
        /// </summary>
        /// <param name="tokens">Tokens to filter</param>
        /// <returns>Filtered tokens</returns>
        public List<string> Filter(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return new List<string>();
            }
            var stopWords = new HashSet<string>(DefaultEnglishStopWords, StringComparer.OrdinalIgnoreCase);
            var filteredWords = words.Where(word => !stopWords.Contains(word)).ToList();
            return filteredWords;
        }
    }
}
