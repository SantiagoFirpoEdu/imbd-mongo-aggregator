namespace IMongoDb.Model.Entities;

public class Movie
{
	private string _id;
	private int runtimeMinutes;
	private List<DBRef<string>> charactersIds;
}