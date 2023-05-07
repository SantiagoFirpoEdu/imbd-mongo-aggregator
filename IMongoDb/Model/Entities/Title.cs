using IMongoDb.Converters;
using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Title")]
public class Title
{
	public static Result<Title, ETitleConversionError> FromTitleBasics(TitleBasics titleBasics, Genres genres)
	{
		Title result = new()
		{
			Id = titleBasics.tconst,
			Type = titleBasics.titleType,
			PrimaryTitle = titleBasics.primaryTitle,
			OriginalTitle = titleBasics.originalTitle,
			IsAdult = titleBasics.isAdult == "1",
			ReleaseYear = new BsonDateTime(DateOnly.ParseExact(titleBasics.startYear, "yyyy").ToDateTime(TimeOnly.MinValue)),
		};
		
		result.GenresIds.AddRange(PopulateGenres(titleBasics, genres));

		return Result<Title, ETitleConversionError>.Ok(result);
	}

	private static IEnumerable<MongoDBRef> PopulateGenres(TitleBasics titleBasics, Genres genres)
	{
		string[] splitGenreNames = titleBasics.genres.Split(",");
		
		foreach (string genreName in splitGenreNames)
		{
			Genre genre = genres.FindOrAddByName(genreName);
			yield return new MongoDBRef(CollectionNames.GenresCollectionName, genre.Id);
		}
	}

	[BsonDiscriminator("TitleRating")]
	private record struct TitleRating
	{
		public TitleRating(double averageRating, int numVotes)
		{
			this.averageRating = averageRating;
			this.numVotes = numVotes;
		}

		[BsonElement]
		private double averageRating;
		
		[BsonElement]
		private int numVotes;
	}

	[BsonId]
	public string Id { get; private set; }

	[BsonElement]
	public string Type { get; private set; }
	
	[BsonElement]
	public string PrimaryTitle { get; private set; }
	
	[BsonElement]
	public string OriginalTitle { get; private set; }
	
	[BsonElement]
	public bool IsAdult { get; private set; }
	
	[BsonElement]
	public BsonDateTime ReleaseYear { get; private set; }
	
	[BsonElement("genres")]
	public List<MongoDBRef> GenresIds { get; } = new();

	[BsonElement]
	private TitleRating rating;
	
	[BsonElement]
	private IList<AlternativeTitle> alternativeTitles = new List<AlternativeTitle>();
	
	[BsonElement]
	private IList<MongoDBRef> charactersIds = new List<MongoDBRef>();
}