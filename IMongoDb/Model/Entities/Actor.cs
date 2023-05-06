using IMongoDb.Model.Collections;
using IMongoDb.Monads;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Entities;

public record Actor
{
	public static Result<Actor, EActorConversionError> FromPrincipal(TitlePrincipal principal, Characters characters)
	{
		if (principal.category is "actor" or "actress" or "self")
		{
			return Result<Actor, EActorConversionError>.Ok(new Actor
			{
				_id = principal.tconst,
				charactersPlayedIds = CharactersCsvToList(principal, characters)
			});
		}

		return Result<Actor, EActorConversionError>.Error(EActorConversionError.NotAnActor);
	}

	private static List<DBRef<string>> CharactersCsvToList(TitlePrincipal principal, Characters characters)
	{
		string principalCharacters = principal.characters.Replace("[", "").Replace("]", "");
		string[] split = principalCharacters.Split(",");

		DBRef<string> CharacterRefCreator(string character) => new(characters.FindOrAddByName(character).Id, CollectionNames.CharactersCollectionName);

		List<DBRef<string>> dbRefs = split.Select(character => character.Replace("'", "")).Select(CharacterRefCreator).ToList();
		return dbRefs;
	}

	private string _id;
	private List<DBRef<string>> charactersPlayedIds;
}

public enum EActorConversionError
{
	NotAnActor,
}