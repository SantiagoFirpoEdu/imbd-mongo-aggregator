using System.Text.Json.Serialization;

namespace IMongoDb;

public struct DBRef<TIdType>
{
    public DBRef(TIdType id, string collectionName)
    {
        this.id = id;
        this.collectionName = collectionName;
    }
    

    [JsonPropertyName("$id")]
    [JsonPropertyOrder(1)]
    public TIdType id { get; init; }
    
    [JsonPropertyName("$ref")]
    [JsonPropertyOrder(0)]
    public string collectionName { get; init; }
}