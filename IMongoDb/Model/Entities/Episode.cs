using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("Episode")]
public class Episode
{
	public static Episode FromTitleEpisode(TitleEpisode titleEpisode)
	{
		Episode episode = new()
		{
			_id = titleEpisode.tconst,
			seasonNumber = titleEpisode.seasonNumber,
			number = titleEpisode.episodeNumber,
			//TODO get runtime minutes from title
			runtimeMinutes = titleEpisode.runtimeMinutes,
			showId = new MongoDBRef(CollectionNames.TitlesCollectionName, titleEpisode.parentTconst)
		};

		return episode;
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