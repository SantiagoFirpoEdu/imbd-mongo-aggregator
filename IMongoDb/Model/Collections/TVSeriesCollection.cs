using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class TVSeriesCollection
{
	public void Add(TvSeries tvSeries)
	{
		tvSeriesDictionary.TryAdd(tvSeries.Id, tvSeries);
	}

	private readonly IDictionary<string, TvSeries> tvSeriesDictionary = new Dictionary<string, TvSeries>();
}