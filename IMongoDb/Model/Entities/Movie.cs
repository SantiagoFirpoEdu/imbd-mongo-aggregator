using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Movie")]
public class Movie
{
	[BsonId]
	private string _id;
	
	[BsonElement]
	private int runtimeMinutes;
	
	[BsonElement]
	private IList<MongoDBRef> charactersIds;
}