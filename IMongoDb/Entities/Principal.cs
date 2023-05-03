namespace IMongoDb.Entities;

public class Principal : CrewMember
{
	private string _id;
	private int ordering;
	private Writer[] writer;
	private Director[] director;
	private Actor[] actor;
}