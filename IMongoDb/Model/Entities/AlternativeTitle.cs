namespace IMongoDb.Model.Entities;

public record AlternativeTitle
{
	private int ordering;
	private string region;
	private string language;
	private string title;
	private bool isOriginalTitle;
	private DBRef<string> originalTitleId;
	private List<string> types;
	private List<string> attributes;

	public AlternativeTitle(int ordering, string region, string language, string title, bool isOriginalTitle, DBRef<string> originalTitleId, List<string> types, List<string> attributes)
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
}