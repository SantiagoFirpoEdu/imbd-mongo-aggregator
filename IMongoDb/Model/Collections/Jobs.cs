using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Jobs : IDbCollection
{
	public void Add(Job job)
	{
		jobs.TryAdd(job.Id, job);
	}

	private readonly IDictionary<ObjectId, Job> jobs = new Dictionary<ObjectId, Job>();
	public BsonArray ToBsonArray()
	{
		BsonArray jobArray = new();
		var converted = jobs.Select(BsonDocumentConverter);
		jobArray.AddRange(converted);
		return jobArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<ObjectId, Job> kv)
	{
		return kv.Value.ToBsonDocument();
	}
}