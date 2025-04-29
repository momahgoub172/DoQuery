using DoQuery.Core.Analysis.Stemmers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Analysis.Analyzer
{
    public class SimpleAnalyzer : IAnalyzer
    {
        private readonly Tokenizer _tokenizer;
        private readonly StopWordsFilter _stopWordsFilter;
        private readonly EnglishStemmer _stemmer;

        /// <summary>
        /// Creates a new Simple analyzer with default components.
        /// </summary>
        public SimpleAnalyzer()
        {
            _tokenizer = new Tokenizer();
            _stopWordsFilter = new StopWordsFilter();
            _stemmer = new EnglishStemmer();
        }

        /// <summary>
        /// Analyzes text by tokenizing, filtering stop words, and stemming.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>List of processed tokens</returns>
        public List<string> Analyze(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
                return new List<string>();

            // Step 1: Tokenize
            var tokens = _tokenizer.Tokenize(text);

            // Step 2: Filter stop words
            tokens = _stopWordsFilter.Filter(tokens);

            // Step 3: Stem words
            tokens = _stemmer.Stem(tokens);

            return tokens;
        }
    }
}
