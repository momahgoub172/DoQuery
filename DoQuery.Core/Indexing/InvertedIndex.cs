using DoQuery.Core.Analysis.Analyzer;
using DoQuery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Indexing
{
    /// <summary>
    /// Core inverted index implementation that maps terms to documents and positions.
    /// </summary>
    public class InvertedIndex
    {

        private Dictionary<string, Dictionary<string, List<int>>> _index; //term -> (DocId -> list of term positions)
        private Dictionary<string, Document> _documents; //DocId -> Document objects
        // Statistics
        private int _totalTerms;
        private int _uniqueTerms;

        public InvertedIndex()
        {
            _index = new Dictionary<string, Dictionary<string, List<int>>>(StringComparer.OrdinalIgnoreCase);
            _documents = new Dictionary<string, Models.Document>();
            _totalTerms = 0;
            _uniqueTerms = 0;
        }


        /// <summary>
        /// Gets the total number of documents in the index.
        /// </summary>
        public int DocumentCount => _documents.Count;

        /// <summary>
        /// Gets the total number of unique terms in the index.
        /// </summary>
        public int UniqueTermCount => _uniqueTerms;

        /// <summary>
        /// Gets the total number of terms indexed (including duplicates).
        /// </summary>
        public int TotalTermCount => _totalTerms;







        /// <summary>
        /// Gets all documents in the index.
        /// </summary>
        /// <returns>Collection of all documents</returns>
        public ICollection<Models.Document> GetAllDocuments()
        {
            return _documents.Values;
        }



        /// <summary>
        /// Removes a document from the index.
        /// </summary>
        /// <param name="documentId">The ID of the document to remove</param>
        /// <returns>True if document was found and removed, false otherwise</returns>
        public bool RemoveDocument(string documentId)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException(nameof(documentId), "Document ID cannot be null or empty.");
            }
            if (!_documents.ContainsKey(documentId))
            {
                return false; // Document not found
            }

            // Remove the document from the index
            _index.Remove(documentId);

            // Remove the document from all terms
            foreach (var term in _index.Keys.ToList())
            {
                if (_index[term].ContainsKey(documentId))
                {
                    _index[term].Remove(documentId);

                    //if term was exsit in one document
                    if (_index[term].Count == 0)
                    {
                        _documents.Remove(term); //now term deleted from index
                        _uniqueTerms--;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Adds a document to the index, processing its fields and terms.
        /// </summary>
        /// <param name="document">The document to index</param>
        /// <param name="analyzer">Text analyzer to process content</param>
        /// <returns>Number of terms indexed</returns>
        public int AddDocument(Document document, IAnalyzer analyzer)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (analyzer == null) throw new ArgumentNullException(nameof(analyzer));

            if (_documents.ContainsKey(document.Id))
            {
                //Remove doc
                _documents.Remove(document.Id);
            }


            _documents[document.Id] = document;
            int indexedTerms = 0;

            //Process each filed to documsnt
            foreach (var field in document.Fields)
            {
                var fieldValue = field.Value.ToString() ?? string.Empty; //content

                var tokens = analyzer.Analyze(fieldValue);

                for (int i = 0; i < tokens.Count; i++)
                {
                    var term = tokens[i];
                    AddTermToIndex(term, document.Id, i);
                    indexedTerms++;
                }
            }
            return indexedTerms;
        }


        /// <summary>
        /// Gets document IDs containing the specified term.
        /// </summary>
        /// <param name="term">The term to search for</param>
        /// <returns>Dictionary mapping document IDs to position lists</returns>
        public Dictionary<string, List<int>> GetDocumentsForTerm(string term)
        {
            if(string.IsNullOrEmpty(term)) throw new ArgumentNullException(nameof(term));
            if(_index.TryGetValue(term, out var documents))
            {
                return new Dictionary<string, List<int>>(documents);
            }

            return new Dictionary<string, List<int>>();
        }


        /// <summary>
        /// Gets a document by its ID.
        /// </summary>
        /// <param name="documentId">The document ID</param>
        /// <returns>The document, or null if not found</returns>
        public Models.Document GetDocument(string documentId)
        {
            if(string.IsNullOrEmpty(documentId)) throw new ArgumentNullException(nameof(documentId));
            if(_documents.TryGetValue(documentId, out var document))
            {
                return document;
            }
            return null;
        }



        /// <summary>
        /// Gets the document frequency (number of documents containing the term).
        /// </summary>
        /// <param name="term">The term to check</param>
        /// <returns>Number of documents containing the term</returns>
        public int GetDocumentFrequency(string term)
        {
            if (string.IsNullOrEmpty(term) || !_index.ContainsKey(term))
                return 0;

            return _index[term].Count;
        }


        // Helper method to add a term to the index
        private void AddTermToIndex(string term, string documentId, int position)
        {
            //in index
            if(!_index.TryGetValue(term , out var documents)) //if found key -> return value which is documents Dictionary
            {
                documents = new Dictionary<string, List<int>>();
                _index[term] = documents;
                _uniqueTerms++;
            }

            if(!documents.TryGetValue(documentId , out var positions))
            {
                positions = new List<int>();
                documents[documentId] = positions;
            }

            positions.Add(position);
            _totalTerms++;
        }


        /// <summary>
        /// Clears the entire index.
        /// </summary>
        public void Clear()
        {
            _index.Clear();
            _documents.Clear();
            _totalTerms = 0;
            _uniqueTerms = 0;
        }


    }
}