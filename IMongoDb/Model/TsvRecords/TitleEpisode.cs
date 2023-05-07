using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleEpisode
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("parentTconst")]
    public string parentTconst { get; init; }
    
    [Name("seasonNumber")]
    [NullValues("null")]
    public int? seasonNumber { get; init; }
    
    [Name("episodeNumber")]
    [NullValues("null")]
    public int? episodeNumber { get; init; }
}