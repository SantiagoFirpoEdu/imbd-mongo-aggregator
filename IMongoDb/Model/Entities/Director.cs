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

		Director result = new(principal.nconst);
		return Result<Director, EDirectorConversionError>.Ok(result);
	}

	[BsonId]
	public string Id { get; }
	
	private Director(string id)
	{
		Id = id;
	}
}