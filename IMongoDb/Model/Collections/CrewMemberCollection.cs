using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class CrewMemberCollection : IDbCollection
{
	public void Add(CrewMember conversionResult)
	{
		crewMembers.Add(conversionResult.Id, conversionResult);
	}

	public bool TryGet(string principalValueNconst, out CrewMember? crewMember)
	{
		return crewMembers.TryGetValue(principalValueNconst, out crewMember);
	}

	public BsonArray ToBsonArray()
	{
		BsonArray crewMemberList = new();
		var converted = crewMembers.Select(BsonDocumentConverter);
		crewMemberList.AddRange(converted);

		return crewMemberList;
	}

	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, CrewMember> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, CrewMember> crewMembers = new Dictionary<string, CrewMember>();
}
