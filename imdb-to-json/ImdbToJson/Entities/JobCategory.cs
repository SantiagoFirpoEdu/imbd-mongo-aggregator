namespace ImdbToJson.Entities;

public class JobCategory
{
	private string _id;
	private string name;
	private JobCategory[] children;
	private JobCategory parent;
}