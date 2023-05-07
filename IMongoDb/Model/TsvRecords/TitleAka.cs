using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

/** Stores localized titles tsv records */
public record TitleAka()
{
    [Name("titleId")]
    public string titleId { get; init; }
    
    [Name("ordering")]
    public int ordering { get; init; }
    
    [Name("title")]
    public string title { get; init; }
    
    [Name("region")]
    public string region { get; init; }
    
    [Name("language")]
    public string language { get; init; }
    
    [Name("types")]
    public string types { get; init; }
    
    [Name("attributes")]
    public string attributes { get; init; }
    
    [Name("isOriginalTitle")]
    public string isOriginalTitle { get; init; }
}