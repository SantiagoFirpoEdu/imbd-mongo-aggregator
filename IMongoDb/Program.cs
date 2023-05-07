using IMongoDb;
using IMongoDb.Model;

DbRepository mongoDbRepository = new();
TsvRepository tsvRepository = new();

TsvLoader.LoadTsvs(tsvRepository);

mongoDbRepository.LoadFromTsvs(tsvRepository);

string? input = null;

do
{
    Console.WriteLine("Enter a collection name to get the inserts for:");
    input = Console.ReadLine();

    if (input is not null)
    {
        var insertsResult = mongoDbRepository.GetInserts(input);

        if (insertsResult.WasSuccessful())
        {
            string inserts = insertsResult.GetOk().GetValue();
            Console.WriteLine(inserts);
            Console.WriteLine("Inserts generated successfully. Copied to clipboard.");
            Clipboard.SetText(inserts);
        }
    }
}
while (input is not null && input != "exit");
