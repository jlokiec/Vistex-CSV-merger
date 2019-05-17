using System.Collections.Generic;

namespace Model
{
    public class CsvData
    {
        public string[] FieldNames { get; set; }
        public List<CsvEntry> Entries { get; set; }
    }
}
