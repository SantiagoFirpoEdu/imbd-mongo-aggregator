using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleCrew
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("directors")]
    [NullValues("null")]
    public string? directors { get; init; }
    
    [Name("writers")]
    [NullValues("null")]
    public string? writers { get; init; }
}