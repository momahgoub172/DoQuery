
# DoQuery 🔍  
**DoQuery for Documents** - A lightweight, educational search engine inspired by Elasticsearch and Lucene  

[![.NET](https://img.shields.io/badge/.NET-6.0-blue)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://opensource.org/licenses/MIT)

A minimalistic full-text search library for .NET, designed to help developers understand the core concepts of search engines.  
*(Work in progress! Contributions welcome!)*  

---

## Features ✨  
- **Full-text search** with term matching and basic analysis (tokenization, stop-word removal and Stemmer).  
- **Flexible document schema** using `Dictionary<string, object>`.  
- **CLI** for quick indexing/searching and **Web API** for integration.  
- **Inverted index** core with configurable text analysis.  
- *(Coming soon!)*: Phrase search, TF-IDF scoring, and query operators (`AND`/`OR`/`NOT`).  

---

## Installation ⚡  
```bash
# Clone the repo
git clone https://github.com/momahgoub172/DoQuery

# Build the solution
dotnet build

# Run the CLI
cd src/DoQuery.Cli
dotnet run -- index --path ./sample_docs
```

---

## Getting Started 🚀  
### 1. Index Documents via CLI  
```bash
# Index JSON/text files in a directory
doquery index --path ./documents

# Search for "recipe"
doquery search --query "recipe"
```

### 2. Use the Web API  
Start the API:  
```bash
cd src/DoQuery.Api
dotnet run
```

Search via HTTP:  
```bash
curl -X POST http://localhost:5000/api/search \
  -H "Content-Type: application/json" \
  -d '{"Query": "search engine"}'
```

---

## Project Structure 🏗️  
```
DoQuery/
├── src/
│   ├── DoQuery.Core/     # Indexing, search, text analysis
│   ├── DoQuery.Cli/      # Command-line interface
│   ├── DoQuery.Api/      # REST API (ASP.NET Core)
│   └── DoQuery.Tests/    # Unit tests
├── samples/              # Example usage
└── docs/                 # Documentation
```

---

## Contributing 🤝  
1. **Fork** the repository.  
2. Submit a **pull request** with your improvements.  
3. Report bugs or feature requests via **GitHub Issues**.  

---
---

## DoQuery Development Backlog


### **Epic 1: Core Search Engine Functionality**  
**Priority**: Critical  
**User Stories**:  
1. **Implement Inverted Index**  
   - _Acceptance Criteria_:  
     - `InvertedIndex` class stores terms with document IDs and positions.  
     - Support adding/removing documents.  
     - Retrieve documents by term efficiently.  

2. **Basic Query Parsing**  
   - _Acceptance Criteria_:  
     - `QueryParser` handles simple terms (`"apple"`), phrases (`"red apple"`), and field-specific queries (`title:search`).  
     - Support `AND`/`OR`/`NOT` operators.  

3. **TF-IDF Scoring**  
   - _Acceptance Criteria_:  
     - `TfIdfScorer` calculates relevance scores.  
     - `SearchResult` orders documents by score.  

4. **Text Analysis Pipeline**  
   - _Acceptance Criteria_:  
     - Tokenizer splits text into lowercase tokens.  
     - StopWordsFilter removes common words (e.g., "the", "and").  
     - Stemmer reduces words to roots (e.g., "running" → "run").  

---

### **Epic 2: Command-Line Interface (CLI)**  
**Priority**: High  
**User Stories**:  
1. **Index Documents from Directory**  
   - _Acceptance Criteria_:  
     - `doquery index --path ./docs` parses JSON/Text files and adds to the index.  
     - Progress bar and summary (e.g., "Indexed 100 docs in 2s").  

2. **Interactive Search Command**  
   - _Acceptance Criteria_:  
     - `doquery search --query "error NOT warning"` displays top 10 results with highlights.  

3. **Index Statistics**  
   - _Acceptance Criteria_:  
     - `doquery stats` shows total documents, index size, and term counts.  

---

### **Epic 3: Web API**  
**Priority**: High  
**User Stories**:  
1. **Search REST Endpoint**  
   - _Acceptance Criteria_:  
     - `POST /api/search` accepts `SearchRequest` and returns paginated results.  
     - Response includes highlights and scores.  

2. **Index Management Endpoints**  
   - _Acceptance Criteria_:  
     - `GET /api/index/stats` returns metrics.  
     - `POST /api/index/clear` resets the index.  

3. **Error Handling Middleware**  
   - _Acceptance Criteria_:  
     - Custom exceptions (e.g., `QueryParseException`) return structured HTTP errors.  

---

### **Epic 4: Testing & Validation**  
**Priority**: High  
**User Stories**:  
1. **Unit Tests for Indexing**  
   - _Acceptance Criteria_:  
     - Tests cover `InvertedIndex` (add/remove), `IndexStorage` (serialization).  

2. **Query Parsing Tests**  
   - _Acceptance Criteria_:  
     - Verify complex queries (e.g., `title:"hello world" AND (error OR warn*)`).  

3. **Integration Tests for CLI/API**  
   - _Acceptance Criteria_:  
     - Test full indexing/search workflows.  

---

### **Epic 5: Documentation & Examples**  
**Priority**: Medium  
**User Stories**:  
1. **User Guide**  
   - _Acceptance Criteria_:  
     - `docs/GETTING_STARTED.md` explains installation and basic usage.  
     - `docs/QUERY_SYNTAX.md` details supported operators.  

2. **Sample Projects**  
   - _Acceptance Criteria_:  
     - `samples/` includes code for indexing PDFs and querying via API.  

---

### **Epic 6: Performance & Scalability**  
**Priority**: Medium  
**User Stories**:  
1. **Benchmark Indexing Speed**  
   - _Acceptance Criteria_:  
     - Profile large datasets (10k+ docs) and optimize bottlenecks.  

2. **Caching for Frequent Queries**  
   - _Acceptance Criteria_:  
     - Cache search results with LRU eviction.  

---

### **Epic 7: Future Features**  
**Priority**: Low  
**User Stories**:  
1. **Real-Time Indexing**  
   - _Acceptance Criteria_:  
     - Watch directory for changes and auto-update index.  

2. **Distributed Index Support**  
   - _Acceptance Criteria_:  
     - Shard index across multiple nodes.  

---

## Acknowledgements 💡  
Inspired by the elegance of **Elasticsearch** and **Lucene**. Built to learn, shared to teach.  

--- 

**Happy searching!** 🔍  
