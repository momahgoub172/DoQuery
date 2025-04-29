using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Analysis.Analyzer
{
    public interface IAnalyzer
    {
        /// <summary>
        /// Analyzes text and returns a list of tokens.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>List of processed tokens</returns>
        List<string> Analyze(string text);
    }
}
