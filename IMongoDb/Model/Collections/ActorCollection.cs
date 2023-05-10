using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class ActorCollection : IDbCollection
{
	public void Add(Actor actor)
	{
		Actor existingActor = actors.TryGetValue(actor.Id, )
		actors.TryAdd(actor.Id, actor);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray userRatingList = new();
		var converted = actors.Select(DocumentConverter);
		userRatingList.AddRange(converted);

		return userRatingList;
	}

	private static BsonDocument DocumentConverter(KeyValuePair<string, Actor> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, Actor> actors = new Dictionary<string, Actor>();
}

