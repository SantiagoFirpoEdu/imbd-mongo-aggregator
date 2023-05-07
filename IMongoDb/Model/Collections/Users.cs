using System.Collections;
using IMongoDb.Model.Entities;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Users : IEnumerable<KeyValuePair<string, User>>, IDbCollection
{
	public void Add(User getFakeUser)
	{
		users.TryAdd(getFakeUser.Email, getFakeUser);
	}

	public IEnumerator<KeyValuePair<string, User>> GetEnumerator()
	{
		return users.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public BsonArray ToBsonArray()
	{
		BsonArray userArray = new();
		var converted = users.Select(BsonDocumentConverter);
		userArray.AddRange(converted);
		return userArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, User> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private readonly IDictionary<string, User> users = new Dictionary<string, User>();
}