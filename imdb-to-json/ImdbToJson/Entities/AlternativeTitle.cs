using System.Collections.ObjectModel;

namespace ImdbToJson.Entities;

public class AlternativeTitle
{
	private int ordering;

	private string region;

	private string language;

	private string title;

	private bool isOriginalTitle;

	private Title originalTitle;

	private Collection<string> types;

	private Collection<string> attributes;
}