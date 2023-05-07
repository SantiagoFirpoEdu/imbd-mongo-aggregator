using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Show")]
public class TvSeries
{
	[BsonId]
	public string Id { get; }

	[BsonElement]
	private BsonDateTime endYear;
	
	[BsonElement("episodes")]
	private IList<MongoDBRef> episodesIds = new List<MongoDBRef>();

	public TvSeries(string id, BsonDateTime endYear)
	{
		this.Id = id;
		this.endYear = endYear;
	}

	public static Result<TvSeries, EShowConversionError> FromTitleBasics(TitleBasics titleBasicValue)
	{
		throw new NotImplementedException();
	}
}

public enum EShowConversionError
{
	NotAShow
}