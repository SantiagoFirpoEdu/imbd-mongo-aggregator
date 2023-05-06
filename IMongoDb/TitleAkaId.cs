namespace IMongoDb;

public readonly record struct TitleAkaId(string TitleAkaTconst, int TitleAkaOrdering)
{
    public override int GetHashCode()
    {
        return HashCode.Combine(TitleAkaTconst, TitleAkaOrdering);
    }
}