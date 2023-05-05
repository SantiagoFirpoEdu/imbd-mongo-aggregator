using IMongoDb.Model.Entities;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Collections;

public class Titles
{
	public bool Add(TitleBasics titleBasics)
	{
		var fromTitleBasics = Title.FromTitleBasics(titleBasics);
		return fromTitleBasics.WasSuccessful() && titles.TryAdd(titleBasics.tconst, fromTitleBasics.GetOkValueUnsafe());
	}
	
	private readonly IDictionary<string, Title> titles = new Dictionary<string, Title>();
}