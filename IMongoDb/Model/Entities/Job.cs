using IMongoDb.Model.TsvRecords;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Job")]
public class Job
{
	[field: BsonId] public ObjectId Id { get; }

	[BsonElement]
	private int ordering;
	
	[BsonElement]
	[BsonIgnoreIfNull]
	private string? name;

	[BsonElement("workedOnTitles")]
	private MongoDBRef titleWorkedOnId;

	[BsonElement("crewMembers")]
	private MongoDBRef crewMemberIds;
	
	[BsonElement]
	[BsonIgnoreIfNull]
	private string? jobCategory;

	public Job(string? name, string? jobCategory, int ordering, MongoDBRef titleWorkedOnId, MongoDBRef crewMemberIds)
	{
		Id = ObjectId.GenerateNewId();
		this.name = name;
		this.jobCategory = jobCategory;
		this.ordering = ordering;
		this.titleWorkedOnId = titleWorkedOnId;
		this.crewMemberIds = crewMemberIds;
	}
}