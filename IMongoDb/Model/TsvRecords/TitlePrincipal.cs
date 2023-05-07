using System.Diagnostics.Contracts;
using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record TitlePrincipal
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
    
    [Pure]
	public bool IsActor()
	{
		return category is "actor" or "actress" or "self";
	}

	public bool IsWriter()
	{
		return category is "writer";
	}
	
	public bool IsDirector()
	{
		return category is "director";
	}
}