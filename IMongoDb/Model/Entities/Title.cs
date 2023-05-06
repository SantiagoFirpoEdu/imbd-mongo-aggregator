using IMongoDb.Entities;
using IMongoDb.Monads;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Entities;
public class Title
{
	public static Result<Title, ETitleConversionError> FromTitleBasics(TitleBasics titleBasics)
	{
		Title result = new()
		{
			_id = titleBasics.tconst,
			type = titleBasics.titleType,
			primaryTitle = titleBasics.primaryTitle,
			originalTitle = titleBasics.originalTitle,
			isAdult = titleBasics.isAdult == "1",
			releaseYear = DateOnly.ParseExact(titleBasics.startYear, "yyyy"),
			genre = Genre.FromCsv(titleBasics.genres),
		};

		return Result<Title, ETitleConversionError>.Ok(result);
	}
	
	private class TitleRating
	{
		private double averageRating;
		private int numVotes;
		private Title titleRated;
	}
	
	private string _id;
	private string type;
	private string primaryTitle;
	private string originalTitle;
	private bool isAdult;
	private DateOnly releaseYear;
	private Genre[] genre;
	private TitleRating rating;
	private AlternativeTitle[] alternativeTitles;
	private UserRating[] userRating;
	private CrewMember crewMember;
	private Job[] job;
	private Episode[] episode;
	private Movie[] movie;
	private Show[] show;
	private List<TitleGenre> titleGenre;
}