using IMongoDb.Converters;
using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("CrewMember")]
public record CrewMember
{
	[field: BsonElement] public string Id { get; private set; }

	[BsonElement]
	private string primaryName;
	
	[BsonElement]
	private BsonDateTime birthYear;
	
	[BsonElement("deathYear")]
	private BsonDateTime deathYear;

	[BsonElement("knownForTitles")]
	public List<MongoDBRef> KnownForTitlesIds { get; } = new();

	public static CrewMember FromNameBasics(NameBasics nameBasics)
	{
		CrewMember crewMember = new()
		{
			Id = nameBasics.Nconst,
			primaryName = nameBasics.PrimaryName,
			birthYear = DateConversions.ToBsonDateTime(nameBasics.BirthYear),
			deathYear = DateConversions.ToBsonDateTime(nameBasics.DeathYear),
		};

		return crewMember;
	}
}