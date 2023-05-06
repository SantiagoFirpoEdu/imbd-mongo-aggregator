namespace IMongoDb;

public readonly record struct TitlePrincipalId(string TitlePrincipalTconst, int TitlePrincipalOrdering)
{
    public override int GetHashCode()
    {
        return HashCode.Combine(TitlePrincipalTconst, TitlePrincipalOrdering);
    }
}