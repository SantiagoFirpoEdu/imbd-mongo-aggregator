using IMongoDb.Converters;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Show")]
public class TvSeries
{
	public static Result<TvSeries, EShowConversionError> FromTitleBasics(TitleBasics titleBasicValue)
	{
		if (titleBasicValue.IsTvSeries())
		{
			return Result<TvSeries, EShowConversionError>.Ok(new TvSeries(titleBasicValue.tconst,
				DateConversions.ToNullableBsonDateTime(titleBasicValue.endYear)));
		}

		return Result<TvSeries, EShowConversionError>.Error(EShowConversionError.NotAShow);
	}

	[BsonId]
	public string Id { get; }

	[BsonElement]
	[BsonIgnoreIfNull]
	private BsonDateTime? endYear;

	private TvSeries(string id, BsonDateTime? endYear)
	{
		this.Id = id;
		this.endYear = endYear;
	}
}

public enum EShowConversionError
{
	NotAShow
}