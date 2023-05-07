using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Movies : IDbCollection
{
	public void Add(Movie movie)
	{
		movies.TryAdd(movie.Id, movie);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray movieArray = new();
		var converted = movies.Select(BsonDocumentConverter);
		movieArray.AddRange(converted);
		return movieArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Movie> kv)
	{
		return kv.Value.ToBsonDocument();
	}
	
	private readonly IDictionary<string, Movie> movies = new Dictionary<string, Movie>();
}