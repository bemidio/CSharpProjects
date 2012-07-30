using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;

namespace LuceneFacility
{
    public class Predicate
    {
        private Query _query;

        internal Predicate()
        {
        }

        internal void AddQuery(Query query)
        {
            this._query = query;
        }

        internal Query GetQuery()
        {
            return this._query;
        }

        public override string ToString()
        {
            return this._query.ToString();
        }
    }
}
