using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

//tconst	parentTconst	seasonNumber	episodeNumber
public record TitleEpisode()
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("parentTconst")]
    public string parentTconst { get; init; }
    
    [Name("seasonNumber")]
    public string seasonNumber { get; init; }
    
    [Name("episodeNumber")]
    public string episodeNumber { get; init; }
}