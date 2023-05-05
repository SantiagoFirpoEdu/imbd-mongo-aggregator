using IMongoDb.Collections;

namespace IMongoDb.Entities;

public class Actor
{
	private string _id;
	private Principal principal;
	private Actors actors;
	private Role[] role;
}