using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class TvSeriesCollection : IDbCollection
{
	public void Add(TvSeries tvSeries)
	{
		tvSeriesDictionary.TryAdd(tvSeries.Id, tvSeries);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray tvSeriesArray = new();
		var converted = tvSeriesDictionary.Select(BsonDocumentConverter);
		tvSeriesArray.AddRange(converted);
		return tvSeriesArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, TvSeries> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, TvSeries> tvSeriesDictionary = new Dictionary<string, TvSeries>();
}