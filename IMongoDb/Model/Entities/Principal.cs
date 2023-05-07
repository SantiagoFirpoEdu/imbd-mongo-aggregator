using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Principal")]
public class Principal
{
	[BsonId]
	private string _id;
	
	[BsonElement]
	private int ordering;
}