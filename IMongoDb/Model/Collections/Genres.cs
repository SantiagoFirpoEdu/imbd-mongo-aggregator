using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Genres
{
	public Genre FindOrAddByName(string genreName)
	{
		bool NamePredicate(KeyValuePair<ObjectId, Genre> genre) => genre.Value.Name == genreName;

		try
		{
			var existingCharacter = genres.First(NamePredicate);
			return existingCharacter.Value;
		}
		catch (InvalidOperationException)
		{
			Genre genre = new(genreName, null);
			genres.Add(genre.Id, genre);
			return genre;
		}
	}

	private readonly IDictionary<ObjectId, Genre> genres = new Dictionary<ObjectId, Genre>();
}