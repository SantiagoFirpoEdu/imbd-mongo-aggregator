using IMongoDb.Model.Entities;
using IMongoDb.Model.TsvRecords;
using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public class Titles : IDbCollection
{
	public bool Add(Title title)
	{
		if (MaxCount is not null && titles.Count >= MaxCount)
		{
			return false;
		}

		return titles.TryAdd(title.Id, title);
	}


	public void AddAlternativeTitle(AlternativeTitle alternativeTitle)
	{
		string originalTitleId = alternativeTitle.OriginalTitleId;
		if (TryGetTitle(originalTitleId, out Title? title))
		{
			title?.AddAlternativeTitle(alternativeTitle);
		}
		else
		{
			Console.Error.WriteLine("Alternative title {0} not found", originalTitleId);
		}
	}

	public void AddCrew(TitleCrew crewRecord, WriterCollection writerCollection, DirectorCollection directorCollection)
	{
		string? writersRecord = crewRecord.writers;
		string[]? writers = writersRecord?.Split(",");
		string[]? directors = crewRecord.directors?.Split(",");
		
		if (TryGetTitle(crewRecord.tconst, out Title? title))
		{
			title?.AddCrew(writers, directors, writerCollection, directorCollection);
		}
		else
		{
			Console.Error.WriteLine("Title tconst {0} not found when adding its crew", crewRecord.tconst);
		}
	}

	public void AddRatings(TitleRatings titleRating)
	{
		if (TryGetTitle(titleRating.tconst, out Title? title))
		{
			title?.SetRatings(titleRating);
		}
		else
		{
			Console.Error.WriteLine("Title tconst {0} not found when adding its ratings", titleRating.tconst);
		}
	}
	
	public bool ContainsTitle(string titleId)
	{
		return titles.ContainsKey(titleId);
	}

	private bool TryGetTitle(string originalTitleId, out Title? title)
	{
		return titles.TryGetValue(originalTitleId, out title);
	}
	
	private readonly IDictionary<string, Title> titles = new Dictionary<string, Title>();

	public string GetRandomTitleId()
	{
		return titles.ElementAt(Faker.RandomNumber.Next(0, titles.Count - 1)).Key;
	}

	public BsonArray ToBsonArray()
	{
		BsonArray titleArray = new();
		var converted = titles.Select(BsonDocumentConverter);
		titleArray.AddRange(converted);
		return titleArray;
	}
	
	private static BsonDocument BsonDocumentConverter(KeyValuePair<string, Title> kv)
	{
		return kv.Value.ToBsonDocument();
	}

	private static readonly int? MaxCount = 50000;

	public void AddActor(string titleId, Actor actor)
	{
		if (titles.TryGetValue(titleId, out Title? title))
		{
			title.AddActor(actor.Id);
		}
	}
}