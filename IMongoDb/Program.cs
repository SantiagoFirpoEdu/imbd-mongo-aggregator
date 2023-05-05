using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using IMongoDb;
using IMongoDb.Model;
using IMongoDb.TsvRecords;

DbRepository mongoDbRepository = new();

CsvConfiguration tsvConfig = new(CultureInfo.InvariantCulture)
{
    Delimiter = "\t",
    HasHeaderRecord = true
};

ParseTitleBasics(tsvConfig);

void ParseTitleBasics(IReaderConfiguration csvConfiguration)
{
    using StreamReader fileReader = new(FilePaths.TitleBasics);
    using CsvReader csvReader = new(fileReader, csvConfiguration);

    csvReader.Read();
    csvReader.ReadHeader();

    var titles = csvReader.GetRecords<TitleBasics>();

    foreach (TitleBasics? title in titles)
    {
        if (title is null)
        {
            continue;
        }
        
        mongoDbRepository.Titles.Add(title);
    }
}