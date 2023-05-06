using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class UserRatings
{
	private IDictionary<UserRatingId, UserRating> userRatings;
}