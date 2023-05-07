using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class DirectorCollection
{
	public void Add(Director director)
	{
		directors.TryAdd(director.Id, director);
	}
	
	private readonly IDictionary<string, Director> directors = new Dictionary<string, Director>();
}