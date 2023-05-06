using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class Titles
{
	public bool Add(Title title)
	{
		return titles.TryAdd(title.Id, title);
	}
	
	private readonly IDictionary<string, Title> titles = new Dictionary<string, Title>();
}