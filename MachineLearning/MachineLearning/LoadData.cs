using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;

namespace ML
{
    public static class LoadData
    {
        /// <summary>
        /// Reads the csv.
        /// </summary>
        /// <returns>The csv.</returns>
        /// <param name="filename">ファイル名</param>
        /// <typeparam name="T">CSVのカラム情報.</typeparam>
        /// <typeparam name="M">Mapper</typeparam>
        public static List<T> LoadCsv<T, M>(string filename) where M : CsvHelper.Configuration.CsvClassMap<T>
        {
            //絶対ファイルパス
            using (var parse = new CsvReader(new StreamReader(filename)))
            {
                parse.Configuration.HasHeaderRecord = false;
                parse.Configuration.RegisterClassMap<M>();
                List<T> data = parse.GetRecords<T>().ToList();
                return data;
            }
        }
    }

    class Iris
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public string Species { get; set; }

    }

    class IrisMap : CsvHelper.Configuration.CsvClassMap<Iris>
    {
        public IrisMap()
        {
            Map(x => x.SepalLength).Index(0);
            Map(x => x.SepalWidth).Index(1);
            Map(x => x.PetalLength).Index(2);
            Map(x => x.PetalWidth).Index(3);
            Map(x => x.Species).Index(4);
        }
    }
}