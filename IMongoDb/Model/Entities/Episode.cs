namespace IMongoDb.Model.Entities;

public class Episode
{
	private string _id;
	private int seasonNumber;
	private int number;
	private int runtimeMinutes;
	private DBRef<string> showId;
}