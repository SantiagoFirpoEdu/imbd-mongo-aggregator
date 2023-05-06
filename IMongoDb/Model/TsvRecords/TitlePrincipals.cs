using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

//tconst	ordering	nconst	category	job	characters
//tt0000001	1	nm1588970	self	\N	["Self"]
public record TitlePrincipals()
{
    [Name("tconst")]
    public string tconst { get; init; }
    
    [Name("ordering")]
    public int ordering { get; init; }
    
    [Name("nconst")]
    public string nconst { get; init; }
    
    [Name("category")]
    public string category { get; init; }
    
    [Name("job")]
    public string job { get; init; }
    
    [Name("characters")]
    public string characters { get; init; }
}