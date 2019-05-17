using System;
using System.Collections.Generic;
using System.Linq;

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

        public string GetCsvRow(string[] fieldNames)
        {
            var s = "";

            foreach (var fieldName in fieldNames)
            {
                if (DimensionValues.ContainsKey(fieldName))
                {
                    s += DimensionValues[fieldName];
                }
                else if (MeasureValues.ContainsKey(fieldName))
                {
                    s += MeasureValues[fieldName];
                }

                if (!fieldName.Equals(fieldNames.Last()))
                {
                    s += ",";
                }
            }

            return s;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CsvEntry))
            {
                return false;
            }

            var otherObject = (CsvEntry)obj;
            return this.DimensionValues.SequenceEqual(otherObject.DimensionValues);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DimensionValues);
        }
    }
}
