using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;

namespace LuceneFacility
{
   public enum FieldIndexOption
   {
       Analyzed ,
       AnalyzedNoNorms ,
       No ,
       NotAnalyzed ,
       NotAnalyzedNoNorms 
   }

    public enum FieldStore
    {
        Yes,
        No, 
        Compress
    }
}
