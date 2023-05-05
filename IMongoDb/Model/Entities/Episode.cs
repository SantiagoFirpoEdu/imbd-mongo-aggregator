using IMongoDb.Model.Entities;

namespace IMongoDb.Entities;

public class Episode
{
	private Title title;
	private int seasonNumber;
	private int number;
	private int runtimeMinutes;
	private Show show;
}