using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("UserRating")]
public class UserRating
{
	[BsonElement]
	private int score;
	
	[BsonElement]
	private MongoDBRef ratedTitle;
	
	[BsonElement]
	private MongoDBRef ratedBy;
}