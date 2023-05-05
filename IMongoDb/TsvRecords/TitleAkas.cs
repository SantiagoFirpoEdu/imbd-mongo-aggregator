﻿using CsvHelper.Configuration.Attributes;

namespace IMongoDb.TsvRecords;

/** Stores localized titles tsv records */
public record TitleAkas()
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