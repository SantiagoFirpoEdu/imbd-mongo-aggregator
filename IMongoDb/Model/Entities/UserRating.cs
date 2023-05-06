namespace IMongoDb.Model.Entities;

public class UserRating
{
	private int score;
	private DBRef<string> ratedTitle;
	private DBRef<string> ratedBy;
}