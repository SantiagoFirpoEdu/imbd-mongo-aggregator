namespace IMongoDb.Entities;
public class Title
{
	private string _id;
	private string primaryTitle;
	private string originalTitle;
	private bool isAdult;
	private DateOnly releaseYear;
	private Genre genre;
	private TitleRating rating;
	private AlternativeTitle[] alternativeTitles;
	private UserRating[] userRating;
	private CrewMember crewMember;
	private Job[] job;
	private Episode[] episode;
	private Movie[] movie;
	private Show[] show;
	private TitleGenre[] titleGenre;

	private class TitleRating
	{
		private double averageRating;
		private int numVotes;
		private Title titleRated;
	}
}