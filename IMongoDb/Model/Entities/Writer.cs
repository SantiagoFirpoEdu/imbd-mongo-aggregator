using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities
{
	[BsonDiscriminator("Writer")]
	public class Writer
	{
		[BsonId]
		private string _id;
	}

}

