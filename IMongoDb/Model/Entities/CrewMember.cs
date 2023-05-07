using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("CrewMember")]
public record CrewMember
{
	[BsonElement]
	private string _id;
	
	[BsonElement]
	private string primaryName;
	
	[BsonElement]
	private BsonDateTime birthYear;
	
	[BsonElement("WorkedOn")]
	private BsonDateTime deathYear;
	
	[BsonElement("WorkedOn")]
	private List<MongoDBRef> workedOnJobIds;
}