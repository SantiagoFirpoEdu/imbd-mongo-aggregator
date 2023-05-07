using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class UserRatingCollection : IDbCollection
{
	public void Add(UserRating userRating)
	{
		userRatings.Add(userRating.Id, userRating);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray userRatingList = new();

		var converted = userRatings.Select(BsonDocumentConverter);
		userRatingList.AddRange(converted);

		return userRatingList;
	}

	private static BsonDocument BsonDocumentConverter(KeyValuePair<UserRatingId, UserRating> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<UserRatingId, UserRating> userRatings = new Dictionary<UserRatingId, UserRating>();

	public bool TryAdd(UserRating userRating)
	{
		return userRatings.TryAdd(userRating.Id, userRating);
	}
}