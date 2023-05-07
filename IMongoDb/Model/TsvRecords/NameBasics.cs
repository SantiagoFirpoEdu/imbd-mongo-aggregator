using CsvHelper.Configuration.Attributes;

namespace IMongoDb.Model.TsvRecords;

public record NameBasics
{
    [Name("nconst")]
    public string Nconst { get; init;  }
    
    [Name("primaryName")]
    public string PrimaryName { get; init;  }
    
    [Name("birthYear")]
    [NullValues("null")]
    public string? BirthYear { get; init;  }
    
    [Name("deathYear")]
    [NullValues("null")]
    public string? DeathYear { get; init;  }
    
    [Name("primaryProfession")]
    public string PrimaryProfession { get; init;  }
    
    [Name("knownForTitles")]
    public string KnownForTitles { get; init;  }
}
