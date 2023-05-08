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
	public Title(string id, string type, string primaryTitle, string originalTitle, bool isAdult,
		BsonDateTime? releaseYear)
	{
		Id = id ?? throw new ArgumentNullException(nameof(id));
		Type = type ?? throw new ArgumentNullException(nameof(type));
		PrimaryTitle = primaryTitle ?? throw new ArgumentNullException(nameof(primaryTitle));
		OriginalTitle = originalTitle ?? throw new ArgumentNullException(nameof(originalTitle));
		IsAdult = isAdult;
		ReleaseYear = releaseYear;
	}

	public static Result<Title, ETitleConversionError> FromTitleBasics(TitleBasics titleBasics, Genres genres)
	{
		string titleBasicsStartYear = titleBasics.startYear;
		Title result = new
		(
			titleBasics.tconst,
			titleBasics.titleType,
			titleBasics.primaryTitle,
			titleBasics.originalTitle,
			titleBasics.isAdult == "1",
			ToNullableBsonDateTime(titleBasicsStartYear)
		);

		if (titleBasicsStartYear is not null)
		{
		}
		
		result.GenresIds.AddRange(PopulateGenres(titleBasics, genres));

		return Result<Title, ETitleConversionError>.Ok(result);
	}

	public static BsonDateTime? ToNullableBsonDateTime(string? yearString)
	{

		return yearString != null
			? new BsonDateTime(DateOnly.ParseExact(yearString, "yyyy").ToDateTime(TimeOnly.MinValue))
			: null;
	}

	public void PopulateRatings(double averageRating, int numVotes)
	{
		rating = new TitleRating(averageRating, numVotes);
	}

	public void AddAlternativeTitle(AlternativeTitle alternativeTitle)
	{
		if (MaxAlternativeTitleAmount is not null && alternativeTitles.Count < MaxAlternativeTitleAmount)
		{
			alternativeTitles.Add(alternativeTitle);
		}
	}

	private static IEnumerable<MongoDBRef> PopulateGenres(TitleBasics titleBasics, Genres genres)
	{
		string[] splitGenreNames = titleBasics.genres.Split(",");
		
		foreach (string genreName in splitGenreNames)
		{
			Genre genre = genres.FindOrAddByName(genreName);
			string titleId = titleBasics.tconst;
			MongoDBRef titleRef = new(CollectionNames.TitlesCollectionName, titleId);
			genre.TitlesIds.TryAdd(titleId, titleRef);
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
	[BsonIgnoreIfNull]
	public BsonDateTime? ReleaseYear { get; private set; }
	
	[BsonElement("genres")]
	public List<MongoDBRef> GenresIds { get; } = new();

	[BsonElement]
	private TitleRating rating;
	
	[BsonElement]
	private readonly IList<AlternativeTitle> alternativeTitles = new List<AlternativeTitle>();
	
	[BsonElement]
	private IList<MongoDBRef> charactersIds = new List<MongoDBRef>();
	
	[BsonElement]
	private IList<MongoDBRef> writers = new List<MongoDBRef>();
	
	[BsonElement]
	private IList<MongoDBRef> directors = new List<MongoDBRef>();

	public void AddCrew(IEnumerable<string>? writersIds, IEnumerable<string>? directorsIds)
	{
		if (writersIds is not null)
		{
			foreach (string writerId in writersIds)
			{
				writers.Add(new MongoDBRef(CollectionNames.WritersCollectionName, writerId));
			}
		}

		if (directorsIds is null)
		{
			return;
		}

		foreach (string directorId in directorsIds)
		{
			directors.Add(new MongoDBRef(CollectionNames.DirectorsCollectionName, directorId));
		}
	}

	public void SetRatings(TitleRatings titleRating)
	{
		rating = new TitleRating(titleRating.averageRating, titleRating.numVotes);
	}

	private static readonly int? MaxAlternativeTitleAmount = null;
}