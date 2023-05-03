using IMongoDb.Collections;

namespace IMongoDb.Entities;

public class Actor : Principal
{
	private Principal principal;
	private Actors actors;
	private Role[] role;
}