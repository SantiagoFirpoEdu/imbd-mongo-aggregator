﻿using System.Collections.Immutable;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using IMongoDb;
using IMongoDb.Model;
using IMongoDb.TsvRecords;

DbRepository mongoDbRepository = new();
TsvRepository tsvRepository = new();

CsvConfiguration tsvConfig = new(CultureInfo.InvariantCulture)
{
    Delimiter = "\t",
    HasHeaderRecord = true
};

ParseTitleBasics();

void ParseTitleBasics()
{
    LoadNameBasics();
    LoadTitleAkas();
    LoadTitleBasics();
    LoadTitleCrew();
    LoadTitleEpisodes();
    LoadTitlePrincipals();
    LoadTitleRatings();
}

string GetRelativePath(string path)
{

    return Path.Combine(Environment.CurrentDirectory, path);
}

void LoadNameBasics()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.NameBasics));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var names = csvReader.GetRecords<NameBasics>().ToImmutableList();

    foreach (NameBasics? name in names.Where(name => name is not null))
    {
        tsvRepository.NameBasics.Add(name.Nconst, name);
    }

}

void LoadTitleAkas()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitleAkas));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titleAkas = csvReader.GetRecords<TitleAkas>().ToImmutableList();

    foreach (TitleAkas? titleAka in titleAkas.Where(titleAka => titleAka is not null))
    {
        tsvRepository.TitleAkas.Add(new TitleAkaId(titleAka.titleId, titleAka.ordering), titleAka);
    }
}

void LoadTitleBasics()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitleBasics));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titles = csvReader.GetRecords<TitleBasics>().ToImmutableList();

    foreach (TitleBasics? title in titles.Where(title => title is not null))
    {
        tsvRepository.TitleBasics.Add(title.tconst, title);
    }
}

void LoadTitleCrew()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitleCrew));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titleCrew = csvReader.GetRecords<TitleCrew>().ToImmutableList();

    foreach (TitleCrew? titleCrewMember in titleCrew.Where(titleCrewMember => titleCrewMember is not null))
    {
        tsvRepository.TitleCrew.Add(titleCrewMember.tconst, titleCrewMember);
    }
}

void LoadTitleEpisodes()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitleEpisode));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titleEpisodes = csvReader.GetRecords<TitleEpisode>().ToImmutableList();

    foreach (TitleEpisode? titleEpisode in titleEpisodes.Where(titleEpisode => titleEpisode is not null))
    {
        tsvRepository.TitleEpisodes.Add(titleEpisode.tconst, titleEpisode);
    }
}

void LoadTitlePrincipals()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitlePrincipals));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titlePrincipals = csvReader.GetRecords<TitlePrincipals>().ToImmutableList();

    foreach (TitlePrincipals? titlePrincipal in titlePrincipals.Where(titlePrincipal => titlePrincipal is not null))
    {
        tsvRepository.TitlePrincipals.Add(new TitlePrincipalId(titlePrincipal.tconst, titlePrincipal.ordering), titlePrincipal);
    }
}

void LoadTitleRatings()
{
    using StreamReader fileReader = new(GetRelativePath(FilePaths.TitleRatings));
    using CsvReader csvReader = new(fileReader, tsvConfig);

    csvReader.Read();
    csvReader.ReadHeader();

    var titleRatings = csvReader.GetRecords<TitleRatings>().ToImmutableList();

    foreach (TitleRatings? titleRating in titleRatings.Where(titleRating => titleRating is not null))
    {
        tsvRepository.TitleRatings.Add(titleRating.tconst, titleRating);
    }
}
