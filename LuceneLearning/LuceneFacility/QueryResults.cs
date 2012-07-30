using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuceneFacility
{
    public class QueryResults
    {
        public HintResult Results { get; set; }
    }

    public class HintResult
    {
        internal HintResult()
        {
            this.FieldsValues = new Dictionary<string, string>();
        }

        public Dictionary<string, string> FieldsValues { get; set; }
    }
}
