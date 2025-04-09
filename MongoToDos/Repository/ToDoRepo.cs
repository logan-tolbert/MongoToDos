using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbClient.Lib;
using MongoToDos.Models;
using MongoToDos.Services;

namespace MongoToDos.Repository;

public class ToDoRepo
{
    public IEnumerable<BsonDocument> GetAllToDos(IMongoCollection<BsonDocument> collection)
    {
        var filter = Builders<BsonDocument>.Filter.Empty;
        return collection.Find(filter).ToList();
    }
    
    public void CreateToDoItem(IMongoCollection<BsonDocument> collection, ToDoItem newItem)
    {
        var newDocument = new BsonDocument
        {
            { "title", newItem.Title },
            { "description", newItem.Description }
        };
        collection.InsertOne(newDocument);
        Console.WriteLine($"'{newItem.Title}' created successfully with ID: {newDocument["_id"]}");
    }
    

}