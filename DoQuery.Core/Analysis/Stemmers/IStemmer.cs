using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Analysis.Stemmers
{
    public interface IStemmer
    {
        public List<string> Stem(List<string> words);
    }
}
