using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Collections;

public readonly record struct UserRatingId
{
    public override int GetHashCode()
    {
        return HashCode.Combine(userEmail, titleId);
    }

    [BsonElement]
    private readonly string userEmail;
    
    [BsonElement]
    private readonly string titleId;

    public UserRatingId(string userEmail, string titleId)
    {
        this.userEmail = userEmail;
        this.titleId = titleId;
    }
}