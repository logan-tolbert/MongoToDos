using MongoDB.Bson;
using MongoDB.Driver;
using MongoToDos.Enums;
using MongoToDos.Models;

namespace MongoToDos.Services;

public class MongoDbService
{
    public static FilterDefinition<BsonDocument> CreateBsonFilter<TValue>(string field, TValue value) 
        => Builders<BsonDocument>.Filter.Eq(field, BsonValue.Create(value));
    
    public static ToDoItem MapBsonAsToDoItem(BsonDocument doc)
    {
        return new ToDoItem
        {
            Id = doc["_id"].AsObjectId,
            Title = doc["title"].AsString,
            Description = doc.Contains("description") ? doc["description"].AsString : string.Empty,
            Status = doc.Contains("status") && Enum.TryParse<Status>(doc["status"].AsString, true, out var status) ? status : Status.Backlog,
            CreatedAt = doc.Contains("createdAt") ? doc["createdAt"].ToUniversalTime() : DateTime.UtcNow
        };
    }
}