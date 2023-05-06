namespace IMongoDb.Model.Entities;

public class Job
{
	private string _id;
	private string name;
	private List<DBRef<string>> workedOnTitlesIds;
	private List<DBRef<string>> crewMembersIds;
	private string? jobCategory;
}