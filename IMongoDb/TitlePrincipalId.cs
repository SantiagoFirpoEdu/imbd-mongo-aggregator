namespace IMongoDb;

public record TitlePrincipalId(string TitlePrincipalTconst, int TitlePrincipalOrdering)
{
    public override int GetHashCode()
    {
        return TitlePrincipalTconst.GetHashCode() + TitlePrincipalOrdering.GetHashCode();
    }
}