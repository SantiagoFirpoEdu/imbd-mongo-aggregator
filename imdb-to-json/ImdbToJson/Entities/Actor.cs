using ImdbToJson.Collections;

namespace ImdbToJson.Entities;

public class Actor : Principal
{
	private Principal principal;
	private Actors actors;
	private Role[] role;
}