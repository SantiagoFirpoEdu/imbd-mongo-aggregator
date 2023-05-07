using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Actor")]
public record Actor
{
	public static Result<Actor, EActorConversionError> FromPrincipal(TitlePrincipal principal, Characters characters)
	{
		if (principal.category is not ("actor" or "actress" or "self"))
		{
			return Result<Actor, EActorConversionError>.Error(EActorConversionError.NotAnActor);
		}

		Actor result = new(principal.tconst);
		result.charactersPlayedIds.AddRange(CharactersCsvToList(principal, characters));
		return Result<Actor, EActorConversionError>.Ok(result);
	}

	private static List<MongoDBRef> CharactersCsvToList(TitlePrincipal principal, Characters characters)
	{
		string principalCharacters = principal.characters.Replace("[", "").Replace("]", "");
		string[] split = principalCharacters.Split(",");

		MongoDBRef CharacterRefCreator(string characterName)
		{
			Character character = characters.FindOrAddByName(characterName);
			return new MongoDBRef(CollectionNames.CharactersCollectionName, character.Id);
		}

		var dbRefs = split.Select(character => character.Replace("'", "")).Select(CharacterRefCreator).ToList();
		return dbRefs;
	}

	[BsonId]
	private string _id;
	
	[BsonElement("charactersPlayed")]
	private readonly List<MongoDBRef> charactersPlayedIds = new();

	public Actor(string id)
	{
		_id = id;
	}
}

public enum EActorConversionError
{
	NotAnActor,
}