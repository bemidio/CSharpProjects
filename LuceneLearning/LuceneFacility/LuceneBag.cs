using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace LuceneFacility
{
    public class LuceneBag
    {
        private Directory _directory;
        private readonly StandardAnalyzer _analyzer;
        private Searcher _searcher;
        private IndexWriter writer;

        public LuceneBag(string luceneFolder)
        {
            _directory = FSDirectory.Open(new DirectoryInfo(luceneFolder));
            this._analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        }

        public IndexWriter Writer
        {
            get
            {
                if (this.writer == null)
                    this.writer = new IndexWriter(this._directory, this._analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

                return this.writer;
            }
        }

        public void CreateIndex()
        {
            Writer.Optimize();
            Writer.Commit();
            Writer.Close();
        }

        //public void CreateIndex(IndexCreator indexCreator)
        //{
        //    #region OldCode
        //    //lock (_lockObject)
        //    //{
        //    //    var writer = new IndexWriter(this._directory, _analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

        //    //    DataSet ds = new DataSet();

        //    //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    //    {
        //    //        writer.AddDocument(CreateDocs(
        //    //            (int) dr["bug_id"],
        //    //            (string) dr["bug_text"]));
        //    //    }

        //    //    writer.Optimize();
        //    //    writer.Close();
        //    //}
        //    #endregion

        //    var writer = new IndexWriter(this._directory, _analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

        //    indexCreator.SaveDocument(writer);
        //}

        #region Old Code
        //private Document CreateDocs(int id, string text)
        //{
        //    var doc = new Document();
        //    doc.Add(new Field(
        //                "text",
        //                new StringReader(text)));

        //    doc.Add(new Field(
        //                "bug_id",
        //                Convert.ToString(id),
        //                Field.Store.YES,
        //                Field.Index.UN_TOKENIZED));

        //    // For the highlighter, store the raw text
        //    doc.Add(new Field(
        //                "raw_text",
        //                text,
        //                Field.Store.YES,
        //                Field.Index.UN_TOKENIZED));

        //    return doc;
        //}

        #endregion

        private void threadproc_update(object obj)
        {
            //lock (_lockObject) // If a thread is updating the index, no other thread should be doing anything with it.
            //{
            //    if (_searcher != null)
            //    {
            //        _searcher.Close();
            //        _searcher = null;
            //    }

            //    var modifier = new IndexModifier(this._directory, _analyzer, false);

            //    // same as build, but uses "modifier" instead of write.
            //    // uses additional "where" clause for bugid

            //    int bug_id = (int)obj;

            //    modifier.DeleteDocuments(new Term("bug_id", Convert.ToString(bug_id)));

            //    var ds = new DataSet();

            //    foreach (DataRow dr in ds.Tables[0].Rows) // one row...
            //    {
            //        modifier.AddDocument(CreateDocs(
            //            (int)dr["bug_id"],
            //            (string)dr["bug_text"]));
            //    }

            //    modifier.Flush();
            //    modifier.Close();
            //}
        }


        public Predicate CreateQuery(string key, string value)
        {
            // ReSharper disable CSharpWarnings::CS0612
            var parser = new QueryParser(key, _analyzer);
            // ReSharper restore CSharpWarnings::CS0612

            if (string.IsNullOrWhiteSpace(value))
                throw new Exception();

            Query query = parser.Parse(value);

            var predicate = new Predicate();
            predicate.AddQuery(query);
            return predicate;
        }

        public List<HintResult> Search(Predicate query, int count)
        {
            if (_searcher == null)
            {
                _searcher = new IndexSearcher(this._directory, true);
            }

            TopDocs docsFound = _searcher.Search(query.GetQuery(), count);

            var list = new List<HintResult>();

            foreach (var score in docsFound.ScoreDocs)
            {
                Document doc = this._searcher.Doc(score.doc);

                var result = new HintResult();

                foreach (var field in doc.GetFields())
                {
                    //result.FieldsValues.Add(field. .Name(), field.StringValue());
                }

                list.Add(result);
            }

            return list;
        }

        #region Old Code
        //public void SearchOld(string text_user_entered)
        //{
        //    QueryParser parser = new QueryParser("Nome", _analyzer);
        //    Query query = null;

        //    if (string.IsNullOrWhiteSpace(text_user_entered))
        //        throw new Exception();

        //    query = parser.Parse(text_user_entered);

        //    Hits hits = null;

        //    if (_searcher == null)
        //    {
        //        _searcher = new IndexSearcher(this._directory, true);
        //    }

        //    hits = _searcher.Search(query);

        //    for (int i = 0; i < hits.Length(); i++)
        //    {
        //        Document doc = hits.Doc(i);
        //    }

        //}

        #endregion
    }
}