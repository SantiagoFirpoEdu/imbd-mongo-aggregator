using IMongoDb.Model.Collections;
using IMongoDb.Model.Entities;
using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace IMongoDb.Model;

public class DbRepository
{
    public DbRepository()
    {
        InitializeDbCollections();
    }

    public void LoadFromTsvs(TsvRepository tsvRepository)
    {
        LoadTitleBasics(tsvRepository);
        LoadNameBasics(tsvRepository);
        LoadTitleAkas(tsvRepository);
        LoadTitleEpisode(tsvRepository);
        LoadTitlePrincipals(tsvRepository);
        LoadTitleCrew(tsvRepository);
        LoadTitleRatings(tsvRepository);

        GenerateFakeUsersAndRatings();
    }

    public Result<string, EGetInsertsError> GetInserts(string collectionName)
    {
        JsonWriterSettings writerSettings = new()
        {
            OutputMode = JsonOutputMode.CanonicalExtendedJson
        };

        return DbCollections.TryGetValue(collectionName, out IDbCollection? dbCollection)
            ? Result<string, EGetInsertsError>.Ok(dbCollection.ToBsonArray().ToJson(writerSettings))
            : Result<string, EGetInsertsError>.Error(EGetInsertsError.CollectionNotFound);
    }
    
    public IDictionary<string, IDbCollection> DbCollections { get; } = new Dictionary<string, IDbCollection>();

    private void InitializeDbCollections()
    {
        DbCollections.Add(CollectionNames.WritersCollectionName, Writers);
        DbCollections.Add(CollectionNames.ActorsCollectionName, Actors);
        DbCollections.Add(CollectionNames.CharactersCollectionName, Characters);
        DbCollections.Add(CollectionNames.CrewMembersCollectionName, CrewMembers);
        DbCollections.Add(CollectionNames.DirectorsCollectionName, Directors);
        DbCollections.Add(CollectionNames.EpisodesCollectionName, EpisodeCollection);
        DbCollections.Add(CollectionNames.GenresCollectionName, Genres);
        DbCollections.Add(CollectionNames.JobsCollectionName, Jobs);
        DbCollections.Add(CollectionNames.MoviesCollectionName, Movies);
        DbCollections.Add(CollectionNames.TvSeriesCollectionName, TvSeriesCollection);
        DbCollections.Add(CollectionNames.TitlesCollectionName, Titles);
        DbCollections.Add(CollectionNames.UserRatingsCollectionName, UserRatingCollection);
        DbCollections.Add(CollectionNames.UsersCollectionName, Users);
    }

    private void GenerateFakeUsersAndRatings()
    {
        GenerateFakeUsers();
        GenerateFakeRatings();
    }

    private void GenerateFakeUsers()
    {
        int maxUsers = Faker.RandomNumber.Next(5, 250);
        for (int i = 0; i <= maxUsers; i++)
        {
            Users.Add(User.GetFakeUser());
        }
    }

    private void GenerateFakeRatings()
    {
        foreach (var kv in Users)
        {
            User user = kv.Value;
            int maxRatings = Faker.RandomNumber.Next(0, 120);
            for (int i = 0; i <= maxRatings; i++)
            {
                string randomTitleId = Titles.GetRandomTitleId();
                UserRating userRating = UserRating.GetFakeUserRating(randomTitleId, user.Email);
                UserRatingCollection.TryAdd(userRating);
            }
        }
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
            Titles.AddCrew(crewRecord, Writers, Directors);
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
            crewMember?.KnownForTitleIdsSet.Add(crewMemberTitleRef);

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
        var conversionResult = Actor.FromPrincipal(titlePrincipal, Characters, Titles);
        if (!conversionResult.WasSuccessful())
        {
            return;
        }

        Actor actor = conversionResult.GetOk().GetValue();
        Actors.Add(actor);
        Titles.AddActor(titlePrincipal.tconst, actor);
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
            bool loadResult = LoadAsTitle(titleBasicValue);
            if (!loadResult)
            {
                continue;
            }

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

    private bool LoadAsTitle(TitleBasics titleBasic)
    {
        var conversionResult = Title.FromTitleBasics(titleBasic, Genres);

        if (conversionResult.WasSuccessful())
        {
            Title title = conversionResult.GetOk().GetValue();
            return Titles.Add(title);
        }

        Console.Error.WriteLine($"Failed to convert title basics: {conversionResult.GetError().GetValue()}");
        return false;
    }
    
    private WriterCollection Writers { get; } = new();
    private ActorCollection Actors { get; } = new();
    private CharacterCollection Characters { get; } = new();
    private CrewMemberCollection CrewMembers { get; } = new();
    private DirectorCollection Directors { get; } = new();
    private EpisodeCollection EpisodeCollection { get; } = new();
    private Genres Genres { get; } = new();
    private Jobs Jobs { get; } = new();
    private Movies Movies { get; } = new();
    private TvSeriesCollection TvSeriesCollection { get; } = new();
    private Titles Titles { get; } = new();
    private UserRatingCollection UserRatingCollection { get; } = new();
    private Users Users { get; } = new();

}

public enum EGetInsertsError
{
    CollectionNotFound
}