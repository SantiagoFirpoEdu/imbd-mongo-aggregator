using IMongoDb.Model.Entities;

namespace IMongoDb.Model.Collections;

public class Movies
{
	public void Add(Movie movie)
	{
		movies.TryAdd(movie.Id, movie);
	}
	
	private readonly IDictionary<string, Movie> movies = new Dictionary<string, Movie>();
}