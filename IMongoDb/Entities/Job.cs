using System.Collections.ObjectModel;

namespace IMongoDb.Entities;

public class Job
{
	private string _id;

	private string name;

	private Collection<Title> workedOn;

	private CrewMember crewMember;

	private JobCategory jobCategory;
}