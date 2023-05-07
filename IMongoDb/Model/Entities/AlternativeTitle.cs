using IMongoDb.Model.Collections;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("AlternativeTitle")]
public record AlternativeTitle
{
	public static Result<AlternativeTitle, EAlternativeTitleConversionError> FromTitleAka(TitleAka titleAka)
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
			new MongoDBRef(CollectionNames.TitlesCollectionName, titleAka.titleId),
			titleTypes.ToList(),
			titleAttributes.ToList()
		);
		
		return Result<AlternativeTitle, EAlternativeTitleConversionError>.Ok(alternativeTitle);
	}
	
	private AlternativeTitle(int ordering, string region, string language, string title, bool isOriginalTitle, MongoDBRef originalTitleId, IList<string> types, IList<string> attributes)
	{
		this.ordering = ordering;
		this.region = region;
		this.language = language;
		this.title = title;
		this.isOriginalTitle = isOriginalTitle;
		this.originalTitleId = originalTitleId;
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
	
	[BsonElement("originalTitle")]
	private MongoDBRef originalTitleId;

	[BsonElement]
	private IList<string> types;
	
	[BsonElement]
	private IList<string> attributes;
}

public enum EAlternativeTitleConversionError
{
}