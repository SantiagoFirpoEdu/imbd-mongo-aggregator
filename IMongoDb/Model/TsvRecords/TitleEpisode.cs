using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleEpisode
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("parentTconst")]
    public string parentTconst { get; init; }
    
    [Name("seasonNumber")]
    public int seasonNumber { get; init; }
    
    [Name("episodeNumber")]
    public int episodeNumber { get; init; }
}