using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Jobs
{
	public void Add(Job job)
	{
		jobs.TryAdd(job.Id, job);
	}

	private readonly IDictionary<ObjectId, Job> jobs = new Dictionary<ObjectId, Job>();
}