using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class WriterCollection
{
	public void Add(Writer writer)
	{
		writers.TryAdd(writer.Id, writer);
	}

	public IList<Writer> ToList()
	{
		return writers.Values.ToList();
	}

	private readonly IDictionary<string, Writer> writers = new Dictionary<string, Writer>();
}