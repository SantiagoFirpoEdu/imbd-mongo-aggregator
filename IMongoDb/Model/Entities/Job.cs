using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Job")]
public class Job
{
	[BsonId]
	public ObjectId Id { get; }

	[BsonElement]
	private int ordering;
	
	[BsonElement]
	[BsonIgnoreIfNull]
	private string? name;

	[BsonElement("workedOnTitle")]
	private MongoDBRef titleWorkedOnId;

	[BsonElement("crewMember")]
	private MongoDBRef crewMemberId;
	
	[BsonElement]
	[BsonIgnoreIfNull]
	private string? jobCategory;

	public Job(string? name, string? jobCategory, int ordering, MongoDBRef titleWorkedOnId, MongoDBRef crewMemberId)
	{
		Id = ObjectId.GenerateNewId();
		this.name = name;
		this.jobCategory = jobCategory;
		this.ordering = ordering;
		this.titleWorkedOnId = titleWorkedOnId;
		this.crewMemberId = crewMemberId;
	}
}