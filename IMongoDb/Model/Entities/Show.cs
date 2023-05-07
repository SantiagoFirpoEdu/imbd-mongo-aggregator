using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Show")]
public class Show
{
	[BsonId]
	private string titleId;
	
	[BsonElement]
	private BsonDateTime endYear;
	
	[BsonElement("episodes")]
	private IList<MongoDBRef> episodesIds;
}