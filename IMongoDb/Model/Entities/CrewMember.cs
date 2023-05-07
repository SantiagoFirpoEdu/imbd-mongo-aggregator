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
	[BsonElement]
	private string _id;
	
	[BsonElement]
	private string primaryName;
	
	[BsonElement]
	private BsonDateTime birthYear;
	
	[BsonElement("WorkedOn")]
	private BsonDateTime deathYear;
	
	[BsonElement("WorkedOn")]
	private List<MongoDBRef> workedOnJobIds = new();

	public static CrewMember FromNameBasics(NameBasics nameBasics)
	{
		CrewMember crewMember = new()
		{
			_id = nameBasics.Nconst,
			primaryName = nameBasics.PrimaryName,
			birthYear = DateConversions.ToBsonDateTime(nameBasics.BirthYear),
			deathYear = DateConversions.ToBsonDateTime(nameBasics.DeathYear),
		};

		return crewMember;
	}
}