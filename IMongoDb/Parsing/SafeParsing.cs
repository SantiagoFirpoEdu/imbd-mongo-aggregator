using IMongoDb.Monads;

namespace IMongoDb.Parsing;

public static class SafeParsing
{
    public static Option<int> parseToInt(string toParse)
    {
        bool wasSuccessful = int.TryParse(toParse, out int parsed);

        return wasSuccessful ? Option<int>.Some(parsed) : Option<int>.None();
    }
}