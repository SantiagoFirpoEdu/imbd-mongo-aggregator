using System.Text.Json.Serialization;

namespace IMongoDb.Model.Entities;

public record Character
{

	public Character(string id, string name)
	{
		Id = id;
		Name = name;
	}

	public string Name { get; }

	[JsonPropertyName("_id")]
	public string Id { get; }
	private IList<DBRef<string>> playedByActorsIds = new List<DBRef<string>>();
	private IList<DBRef<string>> titlesIds = new List<DBRef<string>>();
}
