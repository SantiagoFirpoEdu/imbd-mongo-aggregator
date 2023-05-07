using IMongoDb;
using IMongoDb.Model;
using System.Windows.Forms;

DbRepository mongoDbRepository = new();
TsvRepository tsvRepository = new();

TsvLoader.LoadTsvs(tsvRepository);

mongoDbRepository.LoadFromTsvs(tsvRepository);

string? collectionName = null;

do
{
    Console.WriteLine("Enter a collection name to get the inserts for:");
    collectionName = Console.ReadLine();

    if (collectionName is not null)
    {
        var insertsResult = mongoDbRepository.GetInserts(collectionName);

        if (insertsResult.WasSuccessful())
        {
            ref string insertsArray = ref insertsResult.GetOk().GetValue();
            string inserts = $"db.{collectionName}.insertMany{insertsArray}";
            Console.WriteLine("Inserts generated successfully. Copied to clipboard.");
            Clipboard.SetText(inserts);
        }
    }
}
while (collectionName is not null && collectionName != "exit");
