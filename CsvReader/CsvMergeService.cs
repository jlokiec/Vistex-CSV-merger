using Model;
using System.Collections.Generic;
using System.Linq;

namespace CsvReader
{
    public class CsvMergeService
    {
        public CsvData MergeCsvFiles(CsvData baseData, CsvData updatedData)
        {
            AddMissingFieldsToBaseData(baseData, updatedData);
            PutExtraFieldValues(baseData, updatedData);
            return baseData;
        }

        private void AddMissingFieldsToBaseData(CsvData baseData, CsvData updatedData)
        {
            var baseFieldNames = baseData.FieldNames.ToList();
            var updatedFieldNames = updatedData.FieldNames.ToList();

            var missingFieldNames = GetMissingFieldNames(baseFieldNames, updatedFieldNames);

            foreach (var missingFieldName in missingFieldNames)
            {
                baseFieldNames.Add(missingFieldName);
            }
            baseData.FieldNames = baseFieldNames.ToArray();

            foreach (var entry in baseData.Entries)
            {
                AddMissingFields(entry, missingFieldNames);
            }
        }

        private List<string> GetMissingFieldNames(List<string> baseFieldNames, List<string> fieldNamesToCompare)
        {
            var missingFieldNames = new List<string>();

            foreach (var fieldName in fieldNamesToCompare)
            {
                if (!baseFieldNames.Contains(fieldName))
                {
                    missingFieldNames.Add(fieldName);
                }
            }

            return missingFieldNames;
        }

        private void AddMissingFields(CsvEntry entry, List<string> missingFieldNames)
        {
            foreach (var missingFieldName in missingFieldNames)
            {
                entry.AddValue(missingFieldName, null, false);
            }
        }

        private void PutExtraFieldValues(CsvData baseData, CsvData updatedData)
        {
            var missingFieldNames = GetMissingFieldNames(updatedData.FieldNames.ToList(), baseData.FieldNames.ToList());

            foreach (var updatedEntry in updatedData.Entries)
            {
                var matchingBaseEntry = baseData.Entries.Find((baseEntry) => baseEntry.Equals(updatedEntry));

                if (matchingBaseEntry != null)
                {
                    foreach (var key in updatedEntry.MeasureValues.Keys)
                    {
                        var updatedValue = updatedEntry.MeasureValues[key];
                        if (!updatedValue.Equals(matchingBaseEntry.MeasureValues[key]))
                        {
                            matchingBaseEntry.MeasureValues[key] = updatedValue;
                        }
                    }
                }
                else
                {
                    AddMissingFields(updatedEntry, missingFieldNames);
                    baseData.Entries.Add(updatedEntry);
                }
            }
        }
    }
}
