using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class Users
{
	private readonly IDictionary<string, User> users = new Dictionary<string, User>();
}