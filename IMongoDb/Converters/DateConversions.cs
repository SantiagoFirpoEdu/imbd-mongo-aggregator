using MongoDB.Bson;

namespace IMongoDb.Converters;

public static class DateConversions
{
	public static BsonDateTime ToBsonDateTime(string year)
	{
		return new BsonDateTime(DateOnly.ParseExact(year, "yyyy").ToDateTime(TimeOnly.MinValue));
	}

	public static BsonDateTime? ToNullableBsonDateTime(string? yearString)
	{

		return yearString != null
			? new BsonDateTime(DateOnly.ParseExact(yearString, "yyyy").ToDateTime(TimeOnly.MinValue))
			: null;
	}
}
