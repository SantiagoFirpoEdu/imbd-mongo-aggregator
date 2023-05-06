namespace IMongoDb.Model.Collections;

public readonly record struct UserRatingId
{
    public override int GetHashCode()
    {
        return HashCode.Combine(userEmail, titleId);
    }

    private readonly string userEmail;
    private readonly string titleId;
}