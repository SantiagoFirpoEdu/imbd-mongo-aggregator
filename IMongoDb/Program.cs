using System.Text;
using IMongoDb;
using IMongoDb.Model;

DbRepository mongoDbRepository = new();
TsvRepository tsvRepository = new();

TsvLoader.LoadTsvs(tsvRepository);

mongoDbRepository.LoadFromTsvs(tsvRepository);

string? collectionName = null;

Console.Clear();
do
{
    Console.WriteLine("Enter a collection name to get the inserts for:");
    collectionName = Console.ReadLine();
    
    Console.WriteLine("Do you wish to write it as a shell insertMany command? Type 'yes' or 'no'");
    string? writeAsShell = Console.ReadLine();
    bool shouldWriteAsShell = writeAsShell is not null && writeAsShell == "yes";

    if (collectionName is not null)
    {
        var insertsResult = mongoDbRepository.GetInserts(collectionName);

        if (insertsResult.WasSuccessful())
        {
            ref string insertsArray = ref insertsResult.GetOk().GetValue();
            const string fileOutput = "./file-output";
            string output = shouldWriteAsShell
                ? $"{fileOutput}/{collectionName}-inserts.txt"
                : $"{fileOutput}/{collectionName}.json";

            Directory.CreateDirectory(fileOutput);
            using (StreamWriter fileWriter = new(File.Create(output), Encoding.UTF8))
            {
                fileWriter.Write(shouldWriteAsShell ? $"db.{collectionName}.insertMany({insertsArray})" : insertsArray);
            }

            Console.WriteLine($"Inserts for {collectionName} written to {output}");
            Console.WriteLine("Do you wish to exit? Type 'exit'");
        }
        else
        {
            EGetInsertsError error = insertsResult.GetError().GetValue();
            Console.Error.WriteLine($"Error while getting inserts for {collectionName}: {error}");
            Console.WriteLine("Do you wish to exit? Type 'exit'");
        }
    }
}
while (collectionName is not null && collectionName != "exit");
