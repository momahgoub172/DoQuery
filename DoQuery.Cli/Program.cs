//test invertedIndex

using DoQuery.Core.Models;

var document = new Document("1");
document.AddField("title", "The quick John brown fox");
document.AddField("content", "jumps over the lazy dog");
document.AddField("author", "John Doe fox");

var document2 = new Document("2");
document2.AddField("content", "fox over dog lazy dog"); // Fixed: Changed 'document.AddField' to 'document2.AddField'

var invertedIndex = new DoQuery.Core.Indexing.InvertedIndex();
var analyzer = new DoQuery.Core.Analysis.Analyzer.SimpleAnalyzer();

invertedIndex.AddDocument(document, analyzer);
invertedIndex.AddDocument(document2, analyzer);

var docsOfterm = invertedIndex.GetDocumentsForTerm("fox");
var x = invertedIndex.GetAllDocuments();



foreach (var doc in docsOfterm)
{
    Console.WriteLine($"Document ID: {doc.Key}");
    Console.WriteLine("Positions: " + string.Join(", ", doc.Value));
}
