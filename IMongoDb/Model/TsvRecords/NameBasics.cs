using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

public record NameBasics
{
    [Name("nconst")]
    public string Nconst { get; init;  }
    
    [Name("primaryName")]
    public string PrimaryName { get; init;  }
    
    [Name("birthYear")]
    public string BirthYear { get; init;  }
    
    [Name("deathYear")]
    public string DeathYear { get; init;  }
    
    [Name("primaryProfession")]
    public string PrimaryProfession { get; init;  }
    
    [Name("knownForTitles")]
    public string KnownForTitles { get; init;  }

    public void Deconstruct(out string Nconst, out string PrimaryName, out string BirthYear, out string DeathYear, out string PrimaryProfession, out string KnownForTitles)
    {
        Nconst = this.Nconst;
        PrimaryName = this.PrimaryName;
        BirthYear = this.BirthYear;
        DeathYear = this.DeathYear;
        PrimaryProfession = this.PrimaryProfession;
        KnownForTitles = this.KnownForTitles;
    }
}
