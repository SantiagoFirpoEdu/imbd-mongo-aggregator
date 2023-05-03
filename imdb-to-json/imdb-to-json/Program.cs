using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

Console.WriteLine("Hello, World!");

CsvConfiguration tsvConfig = new(CultureInfo.InvariantCulture)
{
    Delimiter = "\t",
    HasHeaderRecord = true
};

using StreamReader fileReader = new("");
using CsvReader csvReader = new(fileReader, tsvConfig);