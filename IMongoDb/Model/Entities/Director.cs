using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Director")]
public class Director
{
	[BsonId]
	private string _id;
}