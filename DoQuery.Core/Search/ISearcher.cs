using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Search
{
    public interface ISearcher
    {
        /// <summary>
        /// Searches for the specified query in the index.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of search results.</returns>
        List<string> Search(string query, int topNDocs);
    }
}
