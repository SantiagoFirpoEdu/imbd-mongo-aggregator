using IMongoDb.Entities;
using IMongoDb.Model.Entities;
using IMongoDb.TsvRecords;

namespace IMongoDb.Collections;

public class Titles
{
	private IDictionary<string, Title> titles;

	public void Add(TitleBasics titleBasics)
	{
		var fromTitleBasics = Title.FromTitleBasics(titleBasics);
		if (fromTitleBasics)
			titles.Add(titleBasics.tconst, fromTitleBasics);
		return titles.TryAdd(titleBasics.tconst, fromTitleBasics);
	}
}