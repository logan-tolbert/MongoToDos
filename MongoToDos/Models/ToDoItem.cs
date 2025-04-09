using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoToDos.Enums;

namespace MongoToDos.Models;
public class ToDoItem
{
    public ObjectId Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Backlog;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}