using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class EpisodeCollection : IDbCollection
{
	public Episode FindOrAdd(string id)
	{
		if (episodes.TryGetValue(id, out Episode? existingEpisode))
		{
			return existingEpisode;
		}

		Episode episode = new Episode();
		episodes.Add(id, episode);

		return episode;
	}

	public BsonArray ToBsonArray()
	{
		BsonArray episodeArray = new();
		var converted = episodes.Select(BsonDocumentConverter);
		episodeArray.AddRange(converted);
		return episodeArray;
	}

	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Episode> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, Episode> episodes = new Dictionary<string, Episode>();
}