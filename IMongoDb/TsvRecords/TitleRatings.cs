using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

public record TitleRatings()
{
    [Name("tcost")]
    public string tconst { get; init; }
    
    [Name("averageRating")]
    public string averageRating { get; init; }
    
    [Name("numVotes")]
    public string numVotes { get; init; }
}