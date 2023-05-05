using IMongoDb.Collections;
using IMongoDb.Model.Entities;

namespace IMongoDb.Entities;

public class UserRating
{
	private int score;

	private Title ratedTitles;

	private User ratedBy;

	private UserRatings userRatings;
}