namespace IMongoDb;

public record TitleAkaId(string TitleAkaTconst, int TitleAkaOrdering)
{
    public override int GetHashCode()
    {
        return TitleAkaTconst.GetHashCode() + TitleAkaOrdering.GetHashCode();
    }
}