using IMongoDb.Model.Collections;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("UserRating")]
public class UserRating
{
	public static UserRating GetFakeUserRating(string ratedTitleId, string userEmail)
	{
		int score = Faker.RandomNumber.Next(1, 5);
		return new UserRating(score, ratedTitleId, userEmail);
	}

	[BsonId]
	public UserRatingId Id { get; }

	[BsonElement]
	private int score;

	private UserRating(int score, string titleId, string userEmail)
	{
		this.score = score;
		Id = new UserRatingId(userEmail, titleId);
	}
}