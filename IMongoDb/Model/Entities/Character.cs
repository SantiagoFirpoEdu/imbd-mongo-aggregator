using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Character")]
public record Character
	(
		[property: BsonId] string Id,
		[property: BsonElement] string Name
	)
{
	[BsonElement("playedByActors")]
	private IList<MongoDBRef> playedByActorsIds = new List<MongoDBRef>();
	
	[BsonElement("titles")]
	private IList<MongoDBRef> titlesIds = new List<MongoDBRef>();
}
