using DoQuery.Core.Analysis.Analyzer;
using DoQuery.Core.Indexing;
using DoQuery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Search
{
    public class SearchService
    {
        private readonly BasicSearch _searcher;
        private readonly Dictionary<string, Document> _documentLookup; // Maps document IDs to Document objects

        public SearchService(IAnalyzer analyzer, InvertedIndex index)
        {
            _searcher = new BasicSearch(analyzer, index);

            // Create a document lookup dictionary from the index's documents
            _documentLookup = new Dictionary<string, Document>();
            foreach (var doc in index.GetAllDocuments())
            {
                _documentLookup[doc.Id] = doc;
            }
        }


        /// <summary>
        /// Searches for documents matching the query and returns detailed results
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="maxResults">Maximum number of results to return</param>
        /// <returns>List of search result details</returns>
        public List<SearchResultDetail> Search(string query, int maxResults = 10)
        {
            var result  = _searcher.Search(query, maxResults);
            return result
                .Select(docId => new SearchResultDetail
                {
                    DocumentId = docId,
                    Document = _documentLookup.ContainsKey(docId) ? _documentLookup[docId] : null
                })
                .Where(d => d.Document != null) // Filter out null documents
                .ToList();
        }

    }

    public class SearchResultDetail
    {
        public string DocumentId { get; set; }
        //public double Score { get; set; }
        public Document Document { get; set; }
        //public string Snippet { get; set; }
    }
}
