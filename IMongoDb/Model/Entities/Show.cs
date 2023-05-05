using IMongoDb.Model.Entities;

namespace IMongoDb.Entities;

public class Show
{
	private Title title;
	private DateOnly endYear;
	private Episode[] episode;
}