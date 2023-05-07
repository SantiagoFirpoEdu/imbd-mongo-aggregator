using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Genre")]
public class Genre
{
	public Genre(string id, string name)
	{
		Id = id;
		Name = name;
	}

	[BsonId]
	public string Id { get; }

	[BsonElement]
	public string Name { get; private set; }

	[BsonElement]
	[BsonIgnoreIfNull]
	private MongoDBRef? parentGenreId;
}