using Logic;
using System;
using System.Collections.Generic;

namespace Vistex_CSV_merger
{
    class Program
    {
        private static readonly string CSV_FILE_EXTENSION = ".csv";
        private static readonly List<string> DIMENSION_VALUES = new List<string>() { "Customer", "Product" };

        static void Main(string[] args)
        {
            if (!CheckNumberOfArguments(args))
            {
                Console.WriteLine("Bad number of arguments, you should provide\n1) base CSV file,\n2) updated CSV file,\n3) output CSV file.");
                return;
            }

            if (!CheckIfArgumentsAreCsvFiles(args))
            {
                Console.WriteLine("All file names must end with .csv extension.");
                return;
            }

            var baseFileName = args[0];
            var updatedFileName = args[1];
            var outputFileName = args[2];

            var csvReader = new Logic.CsvReader(DIMENSION_VALUES);
            var baseData = csvReader.ReadFile(baseFileName);
            var updatedData = csvReader.ReadFile(updatedFileName);

            var mergeService = new Logic.CsvMergeService();
            var mergedData = mergeService.MergeCsvFiles(baseData, updatedData);

            new CsvWriter().SaveFile(outputFileName, mergedData);
        }

        private static bool CheckNumberOfArguments(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Bad number of arguments, you should provide\n1) base CSV file\n2) updated CSV file\n3) output CSV file");
                return false;
            }
            return true;
        }

        private static bool CheckIfArgumentsAreCsvFiles(string[] args)
        {
            foreach (var fileName in args)
            {
                if (!fileName.EndsWith(CSV_FILE_EXTENSION, StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
