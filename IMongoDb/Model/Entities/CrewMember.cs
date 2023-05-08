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
	public string Id { get; private set; }

	[BsonElement]
	private string primaryName;
	
	[BsonElement]
	private BsonDateTime birthYear;
	
	[BsonElement("deathYear")]
	[BsonIgnoreIfNull]
	private BsonDateTime? deathYear;


	[BsonElement("knownForTitles")]
	public HashSet<MongoDBRef> KnownForTitleIdsSet { get; } = new();

	public static CrewMember FromNameBasics(NameBasics nameBasics)
	{
		CrewMember crewMember = new()
		{
			Id = nameBasics.Nconst,
			primaryName = nameBasics.PrimaryName,
		};

		if (nameBasics.BirthYear is not null)
		{
			crewMember.birthYear = DateConversions.ToBsonDateTime(nameBasics.BirthYear);
		}
		
		if (nameBasics.DeathYear is not null)
		{
			crewMember.deathYear = DateConversions.ToBsonDateTime(nameBasics.DeathYear);
		}

		string[]? titleIdsKnownFor = nameBasics.KnownForTitles?.Split(",");

		if (titleIdsKnownFor is null)
		{
			return crewMember;
		}

		foreach (string titleId in titleIdsKnownFor)
		{
			crewMember.KnownForTitleIdsSet.Add(new MongoDBRef(CollectionNames.TitlesCollectionName, titleId));
		}

		return crewMember;
	}
}