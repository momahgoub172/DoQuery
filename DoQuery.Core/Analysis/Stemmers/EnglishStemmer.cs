using System;
using System.Collections.Generic;
using System.Linq;

namespace DoQuery.Core.Analysis.Stemmers
{
    /// <summary>
    /// Implements a simplified Porter stemming algorithm for English words.
    /// </summary>
    public class EnglishStemmer : IStemmer
    {
        // Pre-defined list of common suffixes to remove
        private static readonly string[] CommonSuffixes = new[]
        {
            "ational", "tional", "enci", "anci", "izer", "ator", "alli", "alism",
            "fulness", "ousness", "iveness", "ment", "ent", "ion", "ou", "ism",
            "ate", "iti", "ous", "ive", "ize"
        };

        // Pre-defined set of vowels for quick lookup
        private static readonly HashSet<char> Vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        /// <summary>
        /// Stems a list of tokens to their root forms.
        /// </summary>
        /// <param name="tokens">Tokens to stem</param>
        /// <returns>List of stemmed tokens</returns>
        public List<string> Stem(List<string> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            // Allocate exact capacity for performance
            var stemmedTokens = new List<string>(tokens.Count);

            foreach (var token in tokens)
            {
                stemmedTokens.Add(StemWord(token));
            }

            return stemmedTokens;
        }

        /// <summary>
        /// Stems a single word to its root form.
        /// </summary>
        /// <param name="word">Word to stem</param>
        /// <returns>Stemmed word</returns>
        public string StemWord(string word)
        {
            // Early exit for short words or empty strings
            if (string.IsNullOrEmpty(word) || word.Length <= 2)
                return word;

            string result = word;

            // Step 1: Handle plural forms and -ed/-ing endings
            result = RemovePlurals(result);
            result = HandleVerbEndings(result);

            // Step 2: Convert 'y' to 'i' in specific contexts
            result = ConvertYToI(result);

            // Step 3: Remove common suffixes
            result = RemoveCommonSuffixes(result);

            return result;
        }

        /// <summary>
        /// Handles various plural forms.
        /// </summary>
        private string RemovePlurals(string word)
        {
            if (word.EndsWith("sses"))
                return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ies"))
                return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ss"))
                return word;

            if (word.EndsWith("s") && word.Length > 3)
                return word.Substring(0, word.Length - 1);

            return word;
        }

        /// <summary>
        /// Handles past tense (-ed) and present participle (-ing) verb endings.
        /// </summary>
        private string HandleVerbEndings(string word)
        {
            // Handle -eed endings (ensure minimum length and only remove extra 'e')
            if (word.EndsWith("eed") && word.Length > 4)
                return word.Substring(0, word.Length - 1);

            // Handle -ed endings (ensure stem contains a vowel)
            if (word.EndsWith("ed") && word.Length > 3)
            {
                string stem = word.Substring(0, word.Length - 2);
                if (ContainsVowel(stem))
                    return stem;
            }

            // Handle -ing endings (ensure stem contains a vowel)
            if (word.EndsWith("ing") && word.Length > 4)
            {
                string stem = word.Substring(0, word.Length - 3);
                if (ContainsVowel(stem))
                    return stem;
            }

            return word;
        }

        /// <summary>
        /// Converts terminal 'y' to 'i' when preceded by a consonant.
        /// </summary>
        private string ConvertYToI(string word)
        {
            if (word.EndsWith("y") && word.Length > 2)
            {
                char penultimate = word[word.Length - 2];
                if (!IsVowel(penultimate))
                    return word.Substring(0, word.Length - 1) + "i";
            }

            return word;
        }

        /// <summary>
        /// Removes common suffixes from words.
        /// </summary>
        private string RemoveCommonSuffixes(string word)
        {
            foreach (string suffix in CommonSuffixes)
            {
                if (word.Length > suffix.Length + 2 && word.EndsWith(suffix))
                {
                    return word.Substring(0, word.Length - suffix.Length);
                }
            }

            return word;
        }

        /// <summary>
        /// Checks if a word contains at least one vowel.
        /// </summary>
        private bool ContainsVowel(string word)
        {
            return word.Any(IsVowel);
        }

        /// <summary>
        /// Determines if a character is a vowel.
        /// Treats 'y' as a vowel when not preceded by another vowel.
        /// </summary>
        private bool IsVowel(char c)
        {
            c = char.ToLowerInvariant(c);
            return Vowels.Contains(c) || c == 'y';
        }
    }
}