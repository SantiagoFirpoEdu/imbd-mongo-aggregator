namespace IMongoDb.Model.Entities;

public class Movie
{
	private string _id;
	private int runtimeMinutes;
	private IList<DBRef<string>> charactersIds;
}