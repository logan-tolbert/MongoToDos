using MongoDB.Bson;
using MongoDbClient.Lib;
using MongoToDos.Commands;
using MongoToDos.Enums;
using MongoToDos.Models;
using static System.Console;

// Configuration
const string host = "localhost";
const int port = 27017;
const string db = "test";
var dbContext = new MongoDbContext(host, port, db);
var collection = dbContext.LoadCollection<BsonDocument>("todoDB");

WriteLine("Mongo ToDos");
WriteLine("What would you like to do?");
WriteLine($"[{MenuOptions.S}]ee all TODOs");
WriteLine($"[{MenuOptions.A}]dd a TODO");
WriteLine($"[{MenuOptions.R}]emove a TODO");
WriteLine($"[{MenuOptions.E}]xit");
Write("");

var selectedOptionInput = ReadLine()!;

if (Enum.TryParse<MenuOptions>(selectedOptionInput.ToUpper(), out var selectedOption))
{
    switch (selectedOption)
    {
        case MenuOptions.S:
            Command.ReadAllToDos(collection);
            break;
        case MenuOptions.A:
            Write("Enter title: ");
            var title = ReadLine()!;

            Write("Enter description: ");
            var description = ReadLine()!;

            var newToDo = new ToDoItem() { Title = title, Description = description };

            Command.CreateAToDoCommand(collection, newToDo);
            break;
        case MenuOptions.R:
            Command.DeleteToDo();
            break;
        case MenuOptions.E:
            WriteLine("Thanks for using ToDo Commander.");
            Environment.Exit(0);
            break;
        default:
            WriteLine("Invalid option");
            break;
    }
}
else
{
    WriteLine("Invalid option");
}







