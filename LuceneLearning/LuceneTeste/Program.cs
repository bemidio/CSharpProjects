using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LuceneFacility
{
    class Program
    {
        internal static readonly DirectoryInfo IndexDir = new DirectoryInfo("/Lucene");

        static void Main()
        {
            var bag = new LuceneBag(@"C:/LuceneIndexFolder");
            MakeIndex(bag);

            Predicate pred = bag.CreateQuery("Nome", "fernando");
            var result = bag.Search(pred, 40);
        }

        public static void MakeIndex(LuceneBag bag)
        {
            DataTable dt = GetData();

            foreach (DataRow row in dt.Rows)
            {
                string id = row["Id"].ToString();
                string nome = row["Nome"].ToString();

                var fieldId = new KeyValuePair<string, string>("Id", id);
                var fieldNome = new KeyValuePair<string, string>("Nome", nome);

                var creator = new IndexCreator();
                creator.AddDocument(fieldId, FieldIndexOption.No, FieldStore.Yes);
                creator.AddDocument(fieldNome, FieldIndexOption.Analyzed, FieldStore.Yes);

                creator.Save(bag);
            }

            bag.CreateIndex();
        }

        public static DataTable GetData()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nome", typeof(string));

            AddRowToTable(dt, 9, "Batista");
            AddRowToTable(dt, 1, "Fernando Batista Emidio");
            AddRowToTable(dt, 2, "Vanessa Cristina Lima Francisco");
            AddRowToTable(dt, 3, "Maria Fernanda Lima Emidio");
            AddRowToTable(dt, 4, "Fernando Luiz Emidio");
            AddRowToTable(dt, 5, "Rita de Cássia de Paula Batista Emidio");
            AddRowToTable(dt, 6, "Felipe Batista Emidio");
            AddRowToTable(dt, 7, "Frederico Batista Emidio");
            AddRowToTable(dt, 8, "Isabela Sousa Emidio");

            return dt;
        }

        private static void AddRowToTable(DataTable dt, int id, string nome)
        {
            DataRow row = dt.NewRow();
            row["Id"] = id;
            row["Nome"] = nome;
            dt.Rows.Add(row);
        }
    }
}
