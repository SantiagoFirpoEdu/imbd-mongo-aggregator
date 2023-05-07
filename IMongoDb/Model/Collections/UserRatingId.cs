namespace IMongoDb.Model.Collections;

public readonly record struct UserRatingId
{
    public override int GetHashCode()
    {
        return HashCode.Combine(userEmail, titleId);
    }

    private readonly string userEmail;
    private readonly string titleId;

    public UserRatingId(string userEmail, string titleId)
    {
        this.userEmail = userEmail;
        this.titleId = titleId;
    }
}