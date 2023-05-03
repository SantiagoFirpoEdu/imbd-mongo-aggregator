namespace ImdbToJson.Entities;

public class Show : Title
{
	private DateOnly endYear;
	private Episode[] episode;
	private Title title;
}