using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

Console.WriteLine("Hello, World!");

CsvConfiguration tsvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = "\t",
    HasHeaderRecord = true
};

using StreamReader fileReader = new StreamReader("");
using CsvReader csvReader = new CsvReader(fileReader, tsvConfig);
var a = csvReader.GetRecords<>()