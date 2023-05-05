using IMongoDb.Entities;
using IMongoDb.Monads;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Entities;
public class Title
{
	public Title(TitleBasics titleBasics)
	{
		throw new NotImplementedException();
	}

	private class TitleRating
	{
		private double averageRating;
		private int numVotes;
		private Title titleRated;
	}
	
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
	
	public static Result<Title, ETitleConversionError> FromTitleBasics(TitleBasics titleBasics)
	{
	}
}

public enum ETitleConversionError
{
	Placeholder,
}