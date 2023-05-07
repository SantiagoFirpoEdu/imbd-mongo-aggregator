using System.Diagnostics.Contracts;
using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleBasics
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("titleType")]
    public string titleType { get; init; }
    
    [Name("primaryTitle")]
    public string primaryTitle { get; init; }
    
    [Name("originalTitle")]
    public string originalTitle { get; init; }
    
    [Name("isAdult")]
    public string isAdult { get; init; }
    
    [Name("startYear")]
    [NullValues("null")]
    public string? startYear { get; init; }
    
    [Name("endYear")]
    [NullValues("null")]
    public string? endYear { get; init; }

    [Name("runtimeMinutes")]
    [NullValues("null")]
    public int? runtimeMinutes { get; init; }
     
    [Name("genres")]
    public string genres { get; init; }
    
    [Pure]
    public bool IsMovie()
    {
        return titleType is "movie" or "tvMovie";
    }
    
    [Pure]
    public bool IsEpisode()
    {
        return titleType is "tvEpisode" or "tvSpecial";
    }

    [Pure]
    public bool IsTvSeries()
    {
        return titleType is "tvSeries" or "tvMiniSeries";
    }

}