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
		string? titleBasicsStartYear = titleBasics.startYear;
		Title result = new
		(
			titleBasics.tconst,
			titleBasics.titleType,
			titleBasics.primaryTitle,
			titleBasics.originalTitle,
			titleBasics.isAdult == "1",
			ToNullableBsonDateTime(titleBasicsStartYear)
		);
		
		result.GenresIds.AddRange(PopulateGenres(titleBasics, genres));

		return Result<Title, ETitleConversionError>.Ok(result);
	}

	public void AddAlternativeTitle(AlternativeTitle alternativeTitle)
	{
		if (MaxAlternativeTitleAmount is not null && alternativeTitles.Count < MaxAlternativeTitleAmount)
		{
			alternativeTitles.Add(alternativeTitle);
		}
	}

	[BsonId]
	public string Id { get; private set; }

	[BsonElement("type")]
	public string Type { get; private set; }
	
	[BsonElement("primaryTitle")]
	public string PrimaryTitle { get; private set; }
	
	[BsonElement("originalTitle")]
	public string OriginalTitle { get; private set; }
	
	[BsonElement("isAdult")]
	public bool IsAdult { get; private set; }
	
	[BsonElement("releaseYear")]
	[BsonIgnoreIfNull]
	public BsonDateTime? ReleaseYear { get; private set; }

	public void SetRatings(TitleRatings titleRating)
	{
		rating = new TitleRating(titleRating.averageRating, titleRating.numVotes);
	}

	public void AddActor(string actorId)
	{
		actorsIds.Add(actorId);
	}

	public void AddCharacter(string characterId)
	{
		uniqueCharactersIds.Add(characterId);
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
	
	[BsonElement("genres")]
	private List<MongoDBRef> GenresIds { get; } = new();

	[BsonElement]
	private TitleRating rating;
	
	[BsonElement]
	private readonly IList<AlternativeTitle> alternativeTitles = new List<AlternativeTitle>();
	
	[BsonElement]
	private IList<MongoDBRef> writers = new List<MongoDBRef>();

	[BsonElement("actors")]
	private IList<MongoDBRef> Actors => actorsIds
		.Select(actorId => new MongoDBRef(CollectionNames.ActorsCollectionName, actorId)).ToList();
	
	[BsonIgnore]
	private readonly HashSet<string> actorsIds = new();

	[BsonElement]
	private IList<MongoDBRef> directors = new List<MongoDBRef>();

	[BsonElement("characters")]
	private IList<MongoDBRef> characters => uniqueCharactersIds
		.Select(characterId => new MongoDBRef(CollectionNames.CharactersCollectionName, characterId)).ToList();

	[BsonIgnore]
	private readonly HashSet<string> uniqueCharactersIds = new();

	private static BsonDateTime? ToNullableBsonDateTime(string? yearString)
	{

		return yearString != null
			? new BsonDateTime(DateOnly.ParseExact(yearString, "yyyy").ToDateTime(TimeOnly.MinValue))
			: null;
	}
	
	private static IEnumerable<MongoDBRef> PopulateGenres(TitleBasics titleBasics, Genres genres)
	{
		string[] splitGenreNames = titleBasics.genres.Split(",");
		
		foreach (string genreName in splitGenreNames)
		{
			Genre genre = genres.FindOrAddByName(genreName);
			string titleId = titleBasics.tconst;
			MongoDBRef titleRef = new(CollectionNames.TitlesCollectionName, titleId);
			genre.TitlesIds.Add(titleRef);
			yield return new MongoDBRef(CollectionNames.GenresCollectionName, genre.Id);
		}
	}

	private Title(string id, string type, string primaryTitle, string originalTitle, bool isAdult,
		BsonDateTime? releaseYear)
	{
		Id = id ?? throw new ArgumentNullException(nameof(id));
		Type = type ?? throw new ArgumentNullException(nameof(type));
		PrimaryTitle = primaryTitle ?? throw new ArgumentNullException(nameof(primaryTitle));
		OriginalTitle = originalTitle ?? throw new ArgumentNullException(nameof(originalTitle));
		IsAdult = isAdult;
		ReleaseYear = releaseYear;
	}
	
	private static readonly int? MaxAlternativeTitleAmount = null;
}