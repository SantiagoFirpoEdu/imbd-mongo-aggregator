using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class DirectorCollection : IDbCollection
{
	public void Add(Director director)
	{
		directors.TryAdd(director.Id, director);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray directorArray = new();
		var converted = directors.Select(BsonDocumentConverter);
		directorArray.AddRange(converted);

		return directorArray;
	}

	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Director> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, Director> directors = new Dictionary<string, Director>();

	public bool Contains(string directorId)
	{
		return directors.ContainsKey(directorId);
	}
}