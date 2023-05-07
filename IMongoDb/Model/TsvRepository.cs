using IMongoDb.Model.TsvRecords;

namespace IMongoDb.Model;

public class TsvRepository
{
    public IDictionary<string, NameBasics> NameBasics { get; } = new Dictionary<string, NameBasics>();
    public IDictionary<TitleAkaId, TitleAka> TitleAkas { get; } = new Dictionary<TitleAkaId, TitleAka>();
    public IDictionary<string, TitleBasics> TitleBasics { get; } = new Dictionary<string, TitleBasics>();
    public IDictionary<string, TitleCrew> TitlesCrews { get; } = new Dictionary<string, TitleCrew>();
    public IDictionary<string, TitleEpisode> TitleEpisodes { get; } = new Dictionary<string, TitleEpisode>();
    public IDictionary<TitlePrincipalId, TitlePrincipal> TitlePrincipals { get; } = new Dictionary<TitlePrincipalId, TitlePrincipal>();
    public IDictionary<string, TitleRatings> TitleRatings { get; } = new Dictionary<string, TitleRatings>();
}