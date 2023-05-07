using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Job")]
public class Job
{
	[BsonId]
	private string _id;
	
	[BsonElement]
	private string name;
	
	[BsonElement("workedOnTitles")]
	private List<MongoDBRef> workedOnTitlesIds;
	
	[BsonElement("crewMembers")]
	private List<MongoDBRef> crewMembersIds;
	
	[BsonElement]
	private string? jobCategory;
}