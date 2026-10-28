using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;

namespace MetaImporter
{
    internal class CsvUtilities
    {
        public List<ReviewRecord> LoadRecords(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))

                return csv.GetRecords<ReviewRecord>().ToList();
        }
    }
}
