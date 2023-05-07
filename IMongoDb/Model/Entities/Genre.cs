using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Genre")]
public class Genre
{
	public Genre(string name, MongoDBRef? parentGenreId)
	{
		Id = ObjectId.GenerateNewId();
		Name = name;
		this.parentGenreId = parentGenreId;
	}

	[BsonId]
	public ObjectId Id { get; }

	[BsonElement]
	public string Name { get; private set; }

	[BsonElement("titles")]
	public IDictionary<string, MongoDBRef> TitlesIds { get; } = new Dictionary<string, MongoDBRef>();

	[BsonElement]
	[BsonIgnoreIfNull]
	private MongoDBRef? parentGenreId;
}