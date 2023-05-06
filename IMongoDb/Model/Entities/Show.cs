namespace IMongoDb.Model.Entities;

public class Show
{
	private string titleId;
	private DateOnly endYear;
	private IList<DBRef<string>> episodesIds;
}