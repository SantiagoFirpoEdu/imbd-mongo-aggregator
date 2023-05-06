namespace IMongoDb.Model.Entities;

public record Actor
{
	private string _id;
	private List<DBRef<string>> charactersPlayedIds;
}