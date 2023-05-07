using IMongoDb.Model.Collections;
using IMongoDb.Model.Entities;
using IMongoDb.Model.TsvRecords;
using MongoDB.Driver;

namespace IMongoDb.Model;

public class DbRepository
{
    public WriterCollection Writers { get; } = new();
    public ActorCollection Actors { get; } = new();
    public CharacterCollection Characters { get; } = new();
    public CrewMemberCollection CrewMembers { get; } = new();
    public DirectorCollection Directors { get; } = new();
    public EpisodeCollection EpisodeCollection { get; } = new();
    public Genres Genres { get; } = new();
    public Jobs Jobs { get; } = new();
    public Movies Movies { get; } = new();
    public TVSeriesCollection TvSeriesCollection { get; } = new();
    public Titles Titles { get; } = new();
    public UserRatingCollection UserRatingCollection { get; } = new();
    public Users Users { get; } = new();

    public void LoadFromTsvs(TsvRepository tsvRepository)
    {
        LoadTitleBasics(tsvRepository);
        LoadNameBasics(tsvRepository);
        LoadTitleAkas(tsvRepository);
        LoadTitleCrew(tsvRepository);
        LoadTitleEpisode(tsvRepository);
        LoadTitlePrincipals(tsvRepository);
        LoadTitleRatings(tsvRepository);
        
        //TODO: sanitize nconst and tconst references, as some have been deleted.
    }

    private void LoadTitleRatings(TsvRepository tsvRepository)
    {
        var ratings = tsvRepository.TitleRatings;
        
        foreach (var kv in ratings)
        {
            TitleRatings titleRating = kv.Value;
            Titles.AddRatings(titleRating);
        }
    }

    private void LoadTitleCrew(TsvRepository tsvRepository)
    {
        var titleCrew = tsvRepository.TitlesCrews;

        foreach (var kv in titleCrew)
        {
            TitleCrew crewRecord = kv.Value;
            Titles.AddCrew(crewRecord);
        }
    }

    private void LoadTitleAkas(TsvRepository tsvRepository)
    {
        var titleAkas = tsvRepository.TitleAkas;
        foreach (var kv in titleAkas)
        {
            TitleAka titleAka = kv.Value;
            AlternativeTitle alternativeTitle = AlternativeTitle.FromTitleAka(titleAka);
            Titles.AddAlternativeTitle(alternativeTitle);
        }
    }

    private void LoadTitleEpisode(TsvRepository tsvRepository)
    {
        var titleEpisodes = tsvRepository.TitleEpisodes;
        foreach (var titleEpisode in titleEpisodes)
        {
            TitleEpisode episode = titleEpisode.Value;

            string episodeTitleId = episode.tconst;
            if (!Titles.ContainsTitle(episodeTitleId))
            {
                Console.Error.WriteLine($"Failed to find tconst {episodeTitleId} for episode");
                continue;
            }

            string episodeParentTvSeriesId = episode.parentTconst;
            if (!Titles.ContainsTitle(episodeParentTvSeriesId))
            {
                Console.Error.WriteLine($"Failed to find parent tv series tconst {episodeParentTvSeriesId} for episode");
                continue;
            }
            
            Episode existingEpisode = EpisodeCollection.FindOrAdd(episodeTitleId);
            existingEpisode.FromTitleEpisode(episode);
        }
    }

    private void LoadTitlePrincipals(TsvRepository tsvRepository)
    {
        var principals = tsvRepository.TitlePrincipals;
        foreach (var kv in principals)
        {
            TitlePrincipal titlePrincipal = kv.Value;
            string titleId = titlePrincipal.tconst;
            string crewMemberId = titlePrincipal.nconst;

            if (!CrewMembers.TryGet(crewMemberId, out CrewMember? crewMember))
            {
                Console.Error.WriteLine($"Failed to find crew member with nconst {crewMemberId}");
                continue;
            }

            MongoDBRef crewMemberTitleRef = new(CollectionNames.TitlesCollectionName, titleId);
            crewMember?.KnownForTitlesIds.Add(crewMemberTitleRef);

            if (!Titles.ContainsTitle(titleId))
            {
                Console.Error.WriteLine($"Failed to find tconst {titleId} for crew member with nconst {crewMemberId}");
                continue;
            }

            MongoDBRef titleRef = new(CollectionNames.TitlesCollectionName, titleId);
            MongoDBRef crewMemberRef = new(CollectionNames.CrewMembersCollectionName, crewMemberId);
            Job job = new(
                titlePrincipal.job,
                titlePrincipal.category,
                titlePrincipal.ordering,
                titleRef,
                crewMemberRef
            );

            Jobs.Add(job);
            LoadAsSubclass(titlePrincipal);
        }
    }

    private void LoadAsSubclass(TitlePrincipal titlePrincipal)
    {
        if (titlePrincipal.IsDirector())
        {
            LoadPrincipalAsDirector(titlePrincipal);
        }
        else if (titlePrincipal.IsWriter())
        {
            LoadPrincipalAsWriter(titlePrincipal);
        }
        else if (titlePrincipal.IsActor())
        {
            LoadPrincipalAsActor(titlePrincipal);
        }
    }

    private void LoadPrincipalAsActor(TitlePrincipal titlePrincipal)
    {

        var conversionResult = Actor.FromPrincipal(titlePrincipal, Characters);
        if (conversionResult.WasSuccessful())
        {
            Actors.Add(conversionResult.GetOk().GetValue());
        }
    }

    private void LoadPrincipalAsWriter(TitlePrincipal titlePrincipal)
    {
        var conversionResult = Writer.FromPrincipal(titlePrincipal);
        if (conversionResult.WasSuccessful())
        {
            Writers.Add(conversionResult.GetOk().GetValue());
        }
    }

    private void LoadPrincipalAsDirector(TitlePrincipal titlePrincipal)
    {
        var conversionResult = Director.FromPrincipal(titlePrincipal);
        if (conversionResult.WasSuccessful())
        {
            Directors.Add(conversionResult.GetOk().GetValue());
        }
    }

    private void LoadNameBasics(TsvRepository tsvRepository)
    {
        var nameBasics = tsvRepository.NameBasics;
        foreach (var name in nameBasics)
        {
            NameBasics nameValue = name.Value;
            LoadAsCrewMember(nameValue);
        }
    }

    private void LoadAsCrewMember(NameBasics name)
    {
        CrewMember conversionResult = CrewMember.FromNameBasics(name);
        CrewMembers.Add(conversionResult);
    }

    private void LoadTitleBasics(TsvRepository tsvRepository)
    {
        var titleBasics = tsvRepository.TitleBasics;
        foreach (var titleBasic in titleBasics)
        {
            TitleBasics titleBasicValue = titleBasic.Value;
            LoadAsTitle(titleBasicValue);
            if (titleBasicValue.IsMovie())
            {
                LoadAsMovie(titleBasicValue);
            }
            else if (titleBasicValue.IsEpisode())
            {
                LoadAsEpisode(titleBasicValue);
            }
            else if (titleBasicValue.IsTvSeries())
            {
                LoadAsTvSeries(titleBasicValue);
            }
        }
    }

    private void LoadAsTvSeries(TitleBasics titleBasicValue)
    {
        var conversionResult = TvSeries.FromTitleBasics(titleBasicValue);

        if (conversionResult.WasSuccessful())
        {
            TvSeriesCollection.Add(conversionResult.GetOk().GetValue());
        }
    }

    private void LoadAsEpisode(TitleBasics titleBasicValue)
    {
        Episode episode = EpisodeCollection.FindOrAdd(titleBasicValue.tconst);
        episode.FromTitleBasics(titleBasicValue);
    }

    private void LoadAsMovie(TitleBasics titleBasicValue)
    {
        var conversionResult = Movie.FromTitleBasics(titleBasicValue);

        if (!conversionResult.WasSuccessful())
        {
            return;
        }

        Movie title = conversionResult.GetOk().GetValue();
        Movies.Add(title);
    }

    private void LoadAsTitle(TitleBasics titleBasic)
    {
        var conversionResult = Title.FromTitleBasics(titleBasic, Genres);

        if (conversionResult.WasSuccessful())
        {
            Title title = conversionResult.GetOk().GetValue();
            Titles.Add(title);
        }
        else
        {
            Console.Error.WriteLine($"Failed to convert title basics: {conversionResult.GetError().GetValue()}");
        }
    }
}