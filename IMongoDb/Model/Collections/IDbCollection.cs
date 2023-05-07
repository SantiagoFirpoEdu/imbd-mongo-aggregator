using MongoDB.Bson;

namespace IMongoDb.Model.Collections;

public interface IDbCollection
{
    BsonArray ToBsonArray();
}