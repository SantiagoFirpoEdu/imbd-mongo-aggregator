using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Actor")]
public record Actor([property: BsonId] string Id)
{
	public static Result<Actor, EActorConversionError> FromPrincipal(TitlePrincipal principal,
		CharacterCollection characters, Titles titles)
	{
		if (!principal.IsActor())
		{
			return Result<Actor, EActorConversionError>.Error(EActorConversionError.NotAnActor);
		}

		var characterIds = CharactersCsvToList(principal, characters);
		titles.AddCharacters(principal.tconst, characterIds);

		Actor result = new(principal.nconst);
		return Result<Actor, EActorConversionError>.Ok(result);
	}

	private static IEnumerable<string> CharactersCsvToList(TitlePrincipal principal, CharacterCollection characters)
	{
		string principalCharacters = principal.characters.Replace("[", "").Replace("]", "");
		string[] split = principalCharacters.Split(",");

		string crewMemberId = principal.nconst;
		string titleId = principal.tconst;

		string CharacterIdMapper(string characterName)
		{
			Character character = characters.FindOrAddByName(characterName);
			character.AddPlayedByActor(crewMemberId);
			character.AddTitle(titleId);
			return character.Id;
		}

		var characterIds = split.Select(character => character.Replace("'", "")).Select(CharacterIdMapper);

		return characterIds;
	}

	[BsonElement("charactersPlayed")]
	private IList<MongoDBRef> CharactersPlayedIds => uniqueCharactersPlayedIds
		.Select(characterId => new MongoDBRef(CollectionNames.CharactersCollectionName, characterId)).ToList();
	
	[BsonIgnore]
	private readonly HashSet<string> uniqueCharactersPlayedIds = new();

	public void CopyTo(Actor foundActor)
	{
		foreach (string characterId in uniqueCharactersPlayedIds)
		{
			foundActor.uniqueCharactersPlayedIds.Add(characterId);
		}
	}
}