using Model;
using System.IO;

namespace Logic
{
    public class CsvWriter
    {
        public void SaveFile(string fileName, CsvData csvData)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                var header = csvData.FieldNames;

                var headerCsv = string.Join(',', csvData.FieldNames);
                streamWriter.WriteLine(string.Join(',', csvData.FieldNames));

                foreach (var entry in csvData.Entries)
                {
                    streamWriter.WriteLine(entry.GetCsvRow(header));
                }
            }
        }
    }
}
