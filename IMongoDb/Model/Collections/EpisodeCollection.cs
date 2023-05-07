using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class EpisodeCollection
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

	private readonly IDictionary<string, Episode> episodes = new Dictionary<string, Episode>();
}