using IMongoDb.Model.Collections;
using IMongoDb.Model.Entities;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace IMongoDb.Model;

public class DbRepository
{
    public Actors Actors { get; } = new();
    public Characters Characters { get; } = new();
    public CrewMembers CrewMembers { get; } = new();
    public Directors Directors { get; } = new();
    public Episodes Episodes { get; } = new();
    public Genres Genres { get; } = new();
    public Jobs Jobs { get; } = new();
    public Movies Movies { get; } = new();
    public Principals Principals { get; } = new();
    public Shows Shows { get; } = new();
    public Titles Titles { get; } = new();
    public UserRatings UserRatings { get; } = new();
    public Users Users { get; } = new();

    public void LoadFromTsvs(TsvRepository tsvRepository)
    {
        var titleBasics = tsvRepository.TitleBasics;
        foreach (var titleBasic in titleBasics)
        {
            var conversionResult = Title.FromTitleBasics(titleBasic.Value, Genres);

            var title = conversionResult.GetOk();
            if (title.IsSet())
            {
                Title value = title.GetValue();
                Titles.Add(value);
                BsonDocument bsonDocument = value.ToBsonDocument();
                JsonWriterSettings jsonWriterSettings = new();
                jsonWriterSettings.Indent = true;
                jsonWriterSettings.NewLineChars = "\n";
                string json = bsonDocument.ToJson(writerSettings: jsonWriterSettings);
                Console.WriteLine(json);
            }
        }
    }
}