using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitleBasics()
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
    public string startYear { get; init; }
    
    [Name("endYear")]
    public string endYear { get; init; }

    [Name("runtimeMinutes")]
    public string runtimeMinutes { get; init; }
     
    [Name("genres")]
    public string genres { get; init; }
}