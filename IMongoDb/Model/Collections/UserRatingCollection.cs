using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class UserRatingCollection
{
	private readonly IDictionary<UserRatingId, UserRating> userRatings = new Dictionary<UserRatingId, UserRating>();
}