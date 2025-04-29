using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DoQuery.Core.Analysis
{
    public class Tokenizer
    {
        /*
         * \w is a built-in character class that matches any “word” character, which in .NET means equivalent to [A-Za-z0-9_].
         * +  is a quantifier meaning “one or more”
         */
        private static readonly Regex TokenPattern = new Regex(@"\w+", RegexOptions.Compiled);

        /// <summary>
        /// Tokenizes input text into individual words/tokens.
        /// </summary>
        /// <param name="text">Input text to tokenize</param>
        /// <returns>List of tokens</returns>
        public List<string> Tokenize(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<string>();
            }

            var tokens = new List<string>();
            var matches = TokenPattern.Matches(text);

            foreach (Match match in matches)
            {
                tokens.Add(match.Value.ToLowerInvariant());
            }

            return tokens;
        }
    }
}
