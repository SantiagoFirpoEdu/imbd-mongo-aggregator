using System.Text.Json.Serialization;
using IMongoDb.Model.Collections;
using IMongoDb.Monads;
using IMongoDb.TsvRecords;

namespace IMongoDb.Model.Entities;

public record AlternativeTitle
{
	public Result<AlternativeTitle, EAlternativeTitleConversionError> FromTitleAka(TitleAka titleAka)
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
			new DBRef<string>(titleAka.titleId, CollectionNames.TitlesCollectionName),
			titleTypes.ToList(),
			titleAttributes.ToList()
		);
		
		return Result<AlternativeTitle, EAlternativeTitleConversionError>.Ok(alternativeTitle);
	}
	
	private AlternativeTitle(int ordering, string region, string language, string title, bool isOriginalTitle, DBRef<string> originalTitleId, IList<string> types, IList<string> attributes)
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

	private int ordering;
	private string region;
	private string language;
	private string title;
	private bool isOriginalTitle;
	
	[JsonPropertyName("originalTitle")]
	private DBRef<string> originalTitleId;

	private IList<string> types;
	private IList<string> attributes;

}

public enum EAlternativeTitleConversionError
{
}