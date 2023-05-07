using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class ActorCollection
{
	public void Add(Actor actor)
	{
		actors.TryAdd(actor.Id, actor);
	}

	private readonly IDictionary<string, Actor> actors = new Dictionary<string, Actor>();
}

