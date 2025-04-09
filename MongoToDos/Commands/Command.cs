using MongoDB.Bson;
using MongoDB.Driver;
using MongoToDos.Repository;
using MongoToDos.Services;
using MongoToDos.Models;
using static System.Console;
using static MongoToDos.Services.ValidationServices;

namespace MongoToDos.Commands;

public static class Command
{
    private static readonly ToDoRepo Repo = new();
    public static void ReadAllToDos(IMongoCollection<BsonDocument> collection)
    {
        var allTodos = Repo.GetAllToDos(collection).ToList();

        if (allTodos.Any())
        {
            WriteLine("Your To-Do List:");
            foreach (var doc in allTodos)
            {
                var todoItem = MongoDbService.MapBsonAsToDoItem(doc);

                WriteLine($"- ID: {todoItem.Id}");
                WriteLine($"  Title: {todoItem.Title}");
                WriteLine($"  Description: {todoItem.Description}");
                WriteLine($"  Status: {todoItem.Status}");
                WriteLine($"  Created At: {todoItem.CreatedAt.ToLocalTime()}");
                WriteLine();
            }
        }
        else
        {
            WriteLine("Your To-Do list is empty!");
        }
    }

    public static void  CreateAToDoCommand(IMongoCollection<BsonDocument> collection,ToDoItem newItem)
    {
        ValidateModel(newItem);
        Repo.CreateToDoItem(collection,newItem);
    }

    public static void DeleteToDo()
    {
        throw new NotImplementedException();
    }
}
