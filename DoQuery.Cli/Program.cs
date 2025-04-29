//test invertedIndex

using DoQuery.Core.Models;

var document = new Document("1");
document.AddField("content", "jumps  fox the lazy dog");

var document2 = new Document("2");
document2.AddField("content", " over dog lazy dog");
var document3 = new Document("3");
document3.AddField("content", "dog jumps over the lazy fox");

var document4 = new Document("4");
document4.AddField("content", "this test star fox over values");

var invertedIndex = new DoQuery.Core.Indexing.InvertedIndex();
var analyzer = new DoQuery.Core.Analysis.Analyzer.SimpleAnalyzer();

invertedIndex.AddDocument(document, analyzer);
invertedIndex.AddDocument(document2, analyzer);
invertedIndex.AddDocument(document3, analyzer);
invertedIndex.AddDocument(document4, analyzer);

var searchService = new DoQuery.Core.Search.SearchService(analyzer, invertedIndex);

var results = searchService.Search("fox over", 10);
foreach (var result in results)
{
    Console.WriteLine($"Document ID: {result.DocumentId}");
    Console.WriteLine($"Document Content: {result.Document.Fields["content"]}");
    Console.WriteLine();
}