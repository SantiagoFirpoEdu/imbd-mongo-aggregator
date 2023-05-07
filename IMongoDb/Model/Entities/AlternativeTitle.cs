using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("AlternativeTitle")]
public record AlternativeTitle
{
	public static AlternativeTitle FromTitleAka(TitleAka titleAka)
	{
		string[] titleTypes = titleAka.types == "null" ? Array.Empty<string>() : titleAka.types.Split(',');
		string[] titleAttributes = titleAka.attributes == "null" ? Array.Empty<string>() : titleAka.attributes.Split(',');
		AlternativeTitle alternativeTitle = new
		(
			titleAka.ordering,
			titleAka.region,
			titleAka.language,
			titleAka.title,
			titleAka.isOriginalTitle == "1",
			titleAka.titleId,
			titleTypes.ToList(),
			titleAttributes.ToList()
		);
		
		return alternativeTitle;
	}
	
	private AlternativeTitle(int ordering, string region, string language, string title, bool isOriginalTitle, string originalTitleId, IList<string> types, IList<string> attributes)
	{
		this.ordering = ordering;
		this.region = region;
		this.language = language;
		this.title = title;
		this.isOriginalTitle = isOriginalTitle;
		this.OriginalTitleId = originalTitleId;
		this.types = types;
		this.attributes = attributes;
	}

	[BsonElement]
	private int ordering;
	
	[BsonElement]
	private string region;
	
	[BsonElement]
	private string language;
	
	[BsonElement]
	private string title;
	
	[BsonElement]
	private bool isOriginalTitle;

	[BsonIgnore]
	public string OriginalTitleId { get; }

	[BsonElement]
	private IList<string> types;
	
	[BsonElement]
	private IList<string> attributes;
}
