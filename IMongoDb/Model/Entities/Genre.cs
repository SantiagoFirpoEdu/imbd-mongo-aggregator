namespace IMongoDb.Model.Entities;

public class Genre
{
	public static Genre[] FromCsv(string titleBasicsGenres)
	{
		return titleBasicsGenres.Split(",")
								.Select(GenreNameMapper)
								.ToArray();
	}

	private static Genre GenreNameMapper(string genreString)
	{
		return new Genre {name = genreString};
	}
	
	private string _id;
	private string name;
	private DBRef<string> parentGenreId;
}