using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleRatings
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("averageRating")]
    public double averageRating { get; init; }
    
    [Name("numVotes")]
    public int numVotes { get; init; }
}