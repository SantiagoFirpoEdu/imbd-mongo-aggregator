using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Characters
{
	public Character FindOrAddByName(string characterName)
	{
		bool NamePredicate(KeyValuePair<string, Character> character) => character.Value.Name == characterName;

		try
		{
			var existingCharacter = characters.First(NamePredicate);
			return existingCharacter.Value;
		}
		catch (InvalidOperationException)
		{
			ObjectId newId = ObjectId.GenerateNewId();
			string newIdString = newId.ToString();
			Character character = new(newIdString, characterName);
			characters.Add(newIdString, character);
			return character;
		}
	}
	private IDictionary<string, Character> characters;
}