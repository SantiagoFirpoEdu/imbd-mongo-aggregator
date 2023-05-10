using System.Text;
using IMongoDb;
using IMongoDb.Model;

void WriteInsertsBothFiles(DbRepository mongoDbRepository1, string collectionNameToWrite)
{
    var insertsResult = mongoDbRepository1.GetInserts(collectionNameToWrite);

    if (insertsResult.WasSuccessful())
    {
        ref string insertsArray = ref insertsResult.GetOk().GetValue();
        const string baseOutputDirectory = "./file-output";
        const string insertsOutputDirectory = $"{baseOutputDirectory}/inserts";
        const string jsonOutputDirectory = $"{baseOutputDirectory}/jsons";

        Directory.CreateDirectory(insertsOutputDirectory);
        string insertsOutput = $"{insertsOutputDirectory}/{collectionNameToWrite}-inserts.txt";
        using (StreamWriter fileWriter = new(File.Create(insertsOutput), Encoding.UTF8))
        {
            string insertMany = $"db.{collectionNameToWrite}.insertMany({insertsArray})";
            fileWriter.Write(insertMany);
            Console.WriteLine($"Inserts for {collectionNameToWrite} written to {insertsOutput}");
        }

        string jsonOutput = $"{jsonOutputDirectory}/{collectionNameToWrite}.json";
        using (StreamWriter fileWriter = new(File.Create(jsonOutput), Encoding.UTF8))
        {
            fileWriter.Write(insertsArray);
            Console.WriteLine($"Inserts for {collectionNameToWrite} written to {jsonOutput}");
        }
    }
    else
    {
        EGetInsertsError error = insertsResult.GetError().GetValue();
        Console.Error.WriteLine($"Error while getting inserts for {collectionNameToWrite}: {error}");
    }
}


DbRepository mongoDbRepository = new();
TsvRepository tsvRepository = new();

TsvLoader.LoadTsvs(tsvRepository);

mongoDbRepository.LoadFromTsvs(tsvRepository);

Console.Clear();

InsertAllCollections(mongoDbRepository);

void InsertAllCollections(DbRepository dbRepository)
{
    var collections = dbRepository.DbCollections;
    foreach (var kv in collections)
    {
        WriteInsertsBothFiles(dbRepository, kv.Key);
    }
}
