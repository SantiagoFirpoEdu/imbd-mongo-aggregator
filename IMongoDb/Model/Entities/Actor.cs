using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Actor")]
public record Actor([property: BsonId] string Id)
{
	public static Result<Actor, EActorConversionError> FromPrincipal(TitlePrincipal principal, CharacterCollection characters)
	{
		if (!principal.IsActor())
		{
			return Result<Actor, EActorConversionError>.Error(EActorConversionError.NotAnActor);
		}

		Actor result = new(principal.nconst);
		result.charactersPlayedIds.AddRange(CharactersCsvToList(principal, characters));
		return Result<Actor, EActorConversionError>.Ok(result);

	}

	private static IEnumerable<MongoDBRef> CharactersCsvToList(TitlePrincipal principal, CharacterCollection characters)
	{
		string principalCharacters = principal.characters.Replace("[", "").Replace("]", "");
		string[] split = principalCharacters.Split(",");

		MongoDBRef CharacterRefCreator(string characterName)
		{
			Character character = characters.FindOrAddByName(characterName);
			return new MongoDBRef(CollectionNames.CharactersCollectionName, character.Id);
		}

		var dbRefs = split.Select(character => character.Replace("'", "")).Select(CharacterRefCreator);
		return dbRefs;
	}

	[BsonElement("charactersPlayed")]
	private readonly List<MongoDBRef> charactersPlayedIds = new();
}

public enum EActorConversionError
{
	NotAnActor,
}