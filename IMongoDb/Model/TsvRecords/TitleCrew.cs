using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

//tconst	directors	writers
//tt0000017	nm1587194,nm0804434	\N
public record TitleCrew()
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("directors")]
    public string directors { get; init; }
    
    [Name("writers")]
    public string writers { get; init; }
}