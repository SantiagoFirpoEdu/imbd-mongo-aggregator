namespace IMongoDb.Model.Entities;

public record Character
{
	private string _id;
	private string name;
	private List<DBRef<string>> playedByActorsIds;
	private List<DBRef<string>> titlesIds;
}
