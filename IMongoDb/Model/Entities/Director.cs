using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Director")]
public class Director
{
	public static Result<Director, EDirectorConversionError> FromPrincipal(TitlePrincipal principal)
	{
		if (principal.category is not "director")
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

	[field: BsonId] public string Id { get; }
}

public enum EDirectorConversionError
{
	NotADirector
}