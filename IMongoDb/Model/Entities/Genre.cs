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
	public List<MongoDBRef> TitlesIds { get; } = new();

	[BsonElement]
	[BsonIgnoreIfNull]
	private MongoDBRef? parentGenreId;
}