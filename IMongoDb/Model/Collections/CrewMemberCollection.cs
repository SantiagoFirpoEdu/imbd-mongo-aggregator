using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class CrewMemberCollection
{
	public void Add(CrewMember conversionResult)
	{
		crewMembers.Add(conversionResult.Id, conversionResult);
	}

	public bool TryGet(string principalValueNconst, out CrewMember? crewMember)
	{
		return crewMembers.TryGetValue(principalValueNconst, out crewMember);
	}

	private readonly IDictionary<string, CrewMember> crewMembers = new Dictionary<string, CrewMember>();
}
