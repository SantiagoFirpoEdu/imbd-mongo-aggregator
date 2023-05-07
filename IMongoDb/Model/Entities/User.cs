using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("User")]
public class User
{
	[BsonId]
	private string email;
	
	[BsonElement]
	private BsonDateTime birthDate;
	
	[BsonElement]
	private string gender;
	
	[BsonElement]
	private string country;
}

