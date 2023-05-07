using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleCrew
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("directors")]
    public string directors { get; init; }
    
    [Name("writers")]
    public string writers { get; init; }
}