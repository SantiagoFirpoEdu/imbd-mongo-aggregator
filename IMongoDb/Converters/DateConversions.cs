using MongoDB.Bson;

namespace IMongoDb.Converters;

public static class DateConversions
{
	public static BsonDateTime ToBsonDateTime(string year)
	{
		return new BsonDateTime(DateOnly.ParseExact(year, "yyyy").ToDateTime(TimeOnly.MinValue));
	}
}