using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class WriterCollection : IDbCollection
{
	public void Add(Writer writer)
	{
		writers.TryAdd(writer.Id, writer);
	}

	private readonly IDictionary<string, Writer> writers = new Dictionary<string, Writer>();
	public BsonArray ToBsonArray()
	{
		BsonArray writerArray = new();
		var converted = writers.Select(BsonDocumentConverter);
		writerArray.AddRange(converted);
		return writerArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Writer> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	public bool Contains(string writerId)
	{
		return writers.ContainsKey(writerId);
	}
}