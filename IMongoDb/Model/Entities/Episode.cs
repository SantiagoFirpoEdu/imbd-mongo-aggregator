using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Episode")]
public class Episode
{
	public void FromTitleEpisode(TitleEpisode titleEpisode)
	{
		_id = titleEpisode.tconst;
		seasonNumber = titleEpisode.seasonNumber;
		number = titleEpisode.episodeNumber;
		showId = new MongoDBRef(CollectionNames.TitlesCollectionName, titleEpisode.parentTconst);
	}
	
	public void FromTitleBasics(TitleBasics titleBasicValue)
	{
		_id = titleBasicValue.tconst;
		runtimeMinutes = titleBasicValue.runtimeMinutes;
	}

	[BsonId]
	private string _id;
	
	[BsonElement]
	private int seasonNumber;
	
	[BsonElement]
	private int number;
	
	[BsonElement]
	private int runtimeMinutes;
	
	[BsonElement("show")]
	private MongoDBRef showId;
}