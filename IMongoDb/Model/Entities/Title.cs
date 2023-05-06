using System.Text.Json.Serialization;
using IMongoDb.Converters;
using IMongoDb.Monads;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Entities;
public class Title
{
	public static Result<Title, ETitleConversionError> FromTitleBasics(TitleBasics titleBasics)
	{
		Title result = new()
		{
			Id = titleBasics.tconst,
			Type = titleBasics.titleType,
			PrimaryTitle = titleBasics.primaryTitle,
			OriginalTitle = titleBasics.originalTitle,
			IsAdult = titleBasics.isAdult == "1",
			ReleaseYear = DateOnly.ParseExact(titleBasics.startYear, "yyyy"),
		};

		return Result<Title, ETitleConversionError>.Ok(result);
	}
	
	private record struct TitleRating
	{
		private double averageRating;
		private int numVotes;
	}

	[JsonPropertyName("_id")]
	public string Id { get; private set; }

	public string Type { get; private set; }
	public string PrimaryTitle { get; private set; }
	public string OriginalTitle { get; private set; }
	public bool IsAdult { get; private set; }
	public DateOnly ReleaseYear { get; private set; }
	
	[JsonPropertyName("genres")]
	public IList<DBRef<string>> GenresIds { get; } = new List<DBRef<string>>();

	private TitleRating rating;
	private IList<AlternativeTitle> alternativeTitles;
	private IList<DBRef<string>> charactersIds;
}