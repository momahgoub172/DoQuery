using DoQuery.Core.Analysis.Analyzer;
using DoQuery.Core.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Search
{
    public class BasicSearch : ISearcher
    {
        //Term and Multi-Term Search

        //TODO:  Calculate scores for each document

        private readonly IAnalyzer _analyzer;
        private readonly InvertedIndex _index;

        public BasicSearch(IAnalyzer analyzer, InvertedIndex index)
        {
            _analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
            _index = index ?? throw new ArgumentNullException(nameof(index));
        }


        /// <summary>
        /// Searches for the specified query in the index.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of search results.</returns>
        public List<string> Search(string query , int topNDocs)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));
            }

            var queryTerms = _analyzer.Analyze(query);
            // Get matching documents for each term
            var termDocuments = new Dictionary<string, Dictionary<string, List<int>>>();
            foreach (var term in queryTerms)
            {
                if (!termDocuments.ContainsKey(term))
                {
                    termDocuments[term] = _index.GetDocumentsForTerm(term);
                }
            }

            return termDocuments
                .SelectMany(kvp => kvp.Value.Keys) // Get all document IDs
                .Distinct() // Remove duplicates
                .Select(docId => new SearchResult { DocumentId = int.Parse(docId) }) // Create SearchResult objects
                .OrderByDescending(sr => sr.DocumentId) //TODO:  Replace with actual score
                .Take(topNDocs)
                .Select(sr => _index.GetDocument(sr.DocumentId.ToString()).Id)
                .ToList();
        }
    }

    //TODO:  Calculate scores for each document
    public class SearchResult
    {
        public int DocumentId { get; set; }
    }
}
