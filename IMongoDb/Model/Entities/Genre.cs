using IMongoDb.Model.Entities;

namespace IMongoDb.Entities;

public class Genre
{
	private string _id;
	private string name;
	private Title title;
	private Genre parent;
	private Genre[] children;
	private TitleGenre[] titleGenre;
}