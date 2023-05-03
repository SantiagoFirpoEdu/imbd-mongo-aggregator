using ImdbToJson.Collections;

namespace ImdbToJson.Entities;

public class UserRating
{
	private int score;

	private Title ratedTitles;

	private User ratedBy;

	private UserRatings userRatings;
}