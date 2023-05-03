using IMongoDb.Collections;

namespace IMongoDb.Entities;

public class CrewMember
{
	private string _id;

	private string primaryName;

	private DateOnly birthYear;

	private DateOnly deathYear;

	private Character character;

	private Principal principal;

	private Title title;

	private Job[] job;

	private CrewMembers crewMembers;
}