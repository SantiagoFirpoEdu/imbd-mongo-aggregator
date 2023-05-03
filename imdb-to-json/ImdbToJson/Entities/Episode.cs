namespace ImdbToJson.Entities;

public class Episode : Title
{
	private int seasonNumber;
	private int number;
	private int runtimeMinutes;
	private Show show;
	private Title title;
}