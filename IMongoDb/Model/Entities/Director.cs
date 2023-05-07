using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Director")]
public class Director
{
	public static Result<Director, EDirectorConversionError> FromPrincipal(TitlePrincipal principal)
	{
		if (!principal.IsDirector())
		{
			return Result<Director, EDirectorConversionError>.Error(EDirectorConversionError.NotADirector);
		}

		Director result = new(principal.tconst);
		return Result<Director, EDirectorConversionError>.Ok(result);

	}

	public Director(string id)
	{
		Id = id;
	}

	[BsonId]
	public string Id { get; }
}

public enum EDirectorConversionError
{
	NotADirector
}