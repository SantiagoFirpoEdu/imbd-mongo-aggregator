using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Episode")]
public class Episode
{
	[BsonId]
	private string _id;
	
	[BsonElement]
	private int seasonNumber;
	
	[BsonElement]
	private int number;
	
	[BsonElement]
	private int runtimeMinutes;
	
	[BsonElement]
	private MongoDBRef showId;
}