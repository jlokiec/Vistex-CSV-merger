using System;
using System.Collections.Generic;

namespace Model
{
    public class CsvEntry
    {
        public Dictionary<string, string> DimensionValues { get; private set; }
        public Dictionary<string, string> MeasureValues { get; private set; }

        public CsvEntry()
        {
            DimensionValues = new Dictionary<string, string>();
            MeasureValues = new Dictionary<string, string>();
        }

        public void AddValue(string key, string value, bool isDimension)
        {
            if (isDimension)
            {
                DimensionValues.Add(key, value);
            }
            else
            {
                MeasureValues.Add(key, value);
            }
        }

        public override string ToString()
        {
            var s = string.Join(',', DimensionValues.Values);
            s += ',';
            s += string.Join(',', MeasureValues.Values);
            return s;
        }
    }
}
