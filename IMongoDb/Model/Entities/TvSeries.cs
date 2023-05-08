using IMongoDb.Converters;
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
	[BsonIgnoreIfNull]
	private BsonDateTime? endYear;
	
	public TvSeries(string id, BsonDateTime? endYear)
	{
		this.Id = id;
		this.endYear = endYear;
	}

	public static Result<TvSeries, EShowConversionError> FromTitleBasics(TitleBasics titleBasicValue)
	{
		if (titleBasicValue.IsTvSeries())
		{
			return Result<TvSeries, EShowConversionError>.Ok(new TvSeries(titleBasicValue.tconst,
				DateConversions.ToNullableBsonDateTime(titleBasicValue.endYear)));
		}

		return Result<TvSeries, EShowConversionError>.Error(EShowConversionError.NotAShow);
	}
}

public enum EShowConversionError
{
	NotAShow
}