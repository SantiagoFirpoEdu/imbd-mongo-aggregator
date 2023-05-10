using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Movie")]
public class Movie
{
	public static Result<Movie, EMovieConversionError> FromTitleBasics(TitleBasics titleBasicValue)
	{
		if (!titleBasicValue.IsMovie())
		{
			return Result<Movie, EMovieConversionError>.Error(EMovieConversionError.NotAMovie);
		}

		Movie movie = new(titleBasicValue.tconst, titleBasicValue.runtimeMinutes);
		return Result<Movie, EMovieConversionError>.Ok(movie);

	}

	[BsonId]
	public string Id { get; }
	
	private Movie(string id, int? runtimeMinutes)
	{
		Id = id;
		this.runtimeMinutes = runtimeMinutes;
	}

	[BsonElement]
	private int? runtimeMinutes;
}

public enum EMovieConversionError
{
	NotAMovie
}