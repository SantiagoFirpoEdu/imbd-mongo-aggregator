using IMongoDb.TsvRecords;

namespace IMongoDb.Model;

public class TsvRepository
{
    public IDictionary<string, NameBasics> NameBasics { get; } = new Dictionary<string, NameBasics>();
    public IDictionary<TitleAkaId, TitleAkas> TitleAkas { get; } = new Dictionary<TitleAkaId, TitleAkas>();
    public IDictionary<string, TitleBasics> TitleBasics { get; } = new Dictionary<string, TitleBasics>();
    public IDictionary<string, TitleCrew> TitleCrew { get; } = new Dictionary<string, TitleCrew>();
    public IDictionary<string, TitleEpisode> TitleEpisodes { get; } = new Dictionary<string, TitleEpisode>();
    public IDictionary<TitlePrincipalId, TitlePrincipals> TitlePrincipals { get; } = new Dictionary<TitlePrincipalId, TitlePrincipals>();
    public IDictionary<string, TitleRatings> TitleRatings { get; } = new Dictionary<string, TitleRatings>();
}