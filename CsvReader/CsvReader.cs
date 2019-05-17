using CsvHelper.Configuration;
using Model;
using System.Collections.Generic;
using System.IO;

namespace CsvReader
{
    public class CsvReader
    {
        private static string CSV_DELIMITER = ",";

        private List<string> dimensionColumns;

        public CsvReader(List<string> dimensionColumns)
        {
            this.dimensionColumns = dimensionColumns;
        }

        public CsvData ReadFile(string fileName)
        {
            var csvData = new CsvData();
            var csvEntries = new List<CsvEntry>();
            var csvReaderConfiguration = new Configuration()
            {
                Delimiter = CSV_DELIMITER
            };

            using (var streamReader = File.OpenText(fileName))
            using (var csvReader = new CsvHelper.CsvReader(streamReader, csvReaderConfiguration))
            {
                csvReader.Read();
                csvReader.ReadHeader();
                var header = csvReader.Context.HeaderRecord;

                while (csvReader.Read())
                {
                    var csvEntry = new CsvEntry();

                    foreach (var name in header)
                    {
                        bool isDimension = false;

                        if (dimensionColumns.Contains(name))
                        {
                            isDimension = true;
                        }

                        var value = csvReader.GetField(name);
                        csvEntry.AddValue(name, value, isDimension);
                    }

                    csvEntries.Add(csvEntry);
                }

                csvData.FieldNames = header;
            }

            csvData.Entries = csvEntries;
            return csvData;
        }
    }
}
