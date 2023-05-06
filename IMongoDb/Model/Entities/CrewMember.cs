namespace IMongoDb.Model.Entities;

public record CrewMember
{
	private string _id;
	private string primaryName;
	private DateOnly birthYear;
	private DateOnly deathYear;
	private List<DBRef<string>> workedOnJobIds;
}