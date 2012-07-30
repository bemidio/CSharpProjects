using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace LuceneFacility
{
    public class IndexCreator
    {
        private readonly Document doc;

        public IndexCreator()
        {
            doc = new Document();
        }

        public void AddDocument(KeyValuePair<string, string> fieldKeyValue, FieldIndexOption indexOption, FieldStore store)
        {
            doc.Add(new Field(fieldKeyValue.Key, fieldKeyValue.Value, GetLucentFieldStore(store), this.GetLucentFieldIndex(indexOption)));
        }

        public void Save(LuceneBag bag)
        {
            bag.Writer.AddDocument(this.doc);
        }

        private Field.Index GetLucentFieldIndex(FieldIndexOption indexOption)
        {
            switch (indexOption)
            {
                case FieldIndexOption.AnalyzedNoNorms:
                    return Field.Index.ANALYZED_NO_NORMS;
                case FieldIndexOption.No:
                    return Field.Index.NO;
                case FieldIndexOption.NotAnalyzed:
                    return Field.Index.NOT_ANALYZED;
                case FieldIndexOption.NotAnalyzedNoNorms:
                    return Field.Index.NOT_ANALYZED_NO_NORMS;
                default:
                    return Field.Index.ANALYZED;
            }
        }

        private Field.Store GetLucentFieldStore(FieldStore fieldStore)
        {
            switch (fieldStore)
            {
                case FieldStore.Compress:
                    return Field.Store.COMPRESS;
                case FieldStore.No:
                    return Field.Store.NO;
                default:
                    return Field.Store.YES;
            }
        }


    }
}
