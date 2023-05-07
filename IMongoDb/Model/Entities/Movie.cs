using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Movie")]
public class Movie
{
	public Movie(string id, int runtimeMinutes)
	{
		Id = id;
		this.runtimeMinutes = runtimeMinutes;
	}

	public static Result<Movie, EMovieConversionError> FromTitleBasics(TitleBasics titleBasicValue)
	{
		if (titleBasicValue.titleType != "movie")
		{
			return Result<Movie, EMovieConversionError>.Error(EMovieConversionError.NotAMovie);
		}

		Movie movie = new(titleBasicValue.tconst, titleBasicValue.runtimeMinutes);
		return Result<Movie, EMovieConversionError>.Ok(movie);
	}

	[field: BsonId] public string Id { get; }

	[BsonElement]
	private int runtimeMinutes;
	
	[BsonElement("characters")]
	private IList<MongoDBRef> charactersIds = new List<MongoDBRef>();
}

public enum EMovieConversionError
{
	NotAMovie
}