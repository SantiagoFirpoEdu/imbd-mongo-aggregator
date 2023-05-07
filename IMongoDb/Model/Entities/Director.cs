using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Director")]
public class Director
{
	public static Result<Director, EDirectorConversionError> FromPrincipal(TitlePrincipal principal)
	{
		if (principal.category is not ("actor" or "actress" or "self"))
		{
			return Result<Director, EDirectorConversionError>.Error(EDirectorConversionError.NotAnActor);
		}

		Director result = new(principal.tconst);
		return Result<Director, EDirectorConversionError>.Ok(result);
	}

	public Director(string id)
	{
		_id = id;
	}

	[BsonId]
	private string _id;
}

public enum EDirectorConversionError
{
	NotAnActor
}