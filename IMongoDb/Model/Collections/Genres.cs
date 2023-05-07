using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Genres
{
	public Genre FindOrAddByName(string genreName)
	{
		bool NamePredicate(KeyValuePair<string, Genre> genre) => genre.Value.Name == genreName;

		try
		{
			var existingCharacter = genres.First(NamePredicate);
			return existingCharacter.Value;
		}
		catch (InvalidOperationException)
		{
			ObjectId newId = ObjectId.GenerateNewId();
			string newIdString = newId.ToString();
			Genre genre = new(newIdString, genreName);
			genres.Add(newIdString, genre);
			return genre;
		}
	}

	private IDictionary<string, Character> characters = new Dictionary<string, Character>();
	private IDictionary<string, Genre> genres = new Dictionary<string, Genre>();
}