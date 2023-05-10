using IMongoDb.Model.Collections;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Character")]
public record Character
	(
		[property: BsonId] string Id,
		[property: BsonElement("name")] string Name
	)
{
	[BsonElement("playedByActors")]
	private IList<MongoDBRef> PlayedByActorsIds => uniqueActorIds
		.Select(actorId => new MongoDBRef(CollectionNames.ActorsCollectionName, actorId)).ToList();

	[BsonElement("titles")]
	private IList<MongoDBRef> TitlesIds => uniqueTitleIds
		.Select(titleId => new MongoDBRef(CollectionNames.TitlesCollectionName, titleId)).ToList();

	[BsonIgnore]
	private readonly HashSet<string> uniqueTitleIds = new();
	
	[BsonIgnore]
	private readonly HashSet<string> uniqueActorIds = new();

	public void AddPlayedByActor(string actorId)
	{
		uniqueActorIds.Add(actorId);
	}
	
	public void AddTitle(string titleId)
	{
		uniqueTitleIds.Add(titleId);
	}
}
