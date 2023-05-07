using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class CharacterCollection : IDbCollection
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

	public BsonArray ToBsonArray()
	{
		BsonArray charactersArray = new();
		var bsonDocuments = characters.Select(BsonDocumentConverter);
		charactersArray.AddRange(bsonDocuments);

		return charactersArray;
	}

	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Character> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, Character> characters = new Dictionary<string, Character>();
}