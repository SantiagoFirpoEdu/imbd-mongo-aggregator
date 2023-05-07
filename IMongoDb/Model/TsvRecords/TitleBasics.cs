using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

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
    [NullValues("null")]
    public int? runtimeMinutes { get; init; }
     
    [Name("genres")]
    public string genres { get; init; }
}

public class NullableIntConverter : Int32Converter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (int.TryParse(text, out int value))
        {
            return value;
        }

        return null;
    }
}