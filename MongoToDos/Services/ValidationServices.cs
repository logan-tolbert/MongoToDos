using MongoToDos.Models;

namespace MongoToDos.Services;

public static class ValidationServices
{
    private static bool ValidateTitle(string title)
    {
        if (!string.IsNullOrEmpty(title)) return true;
        Console.WriteLine("Title cannot be null or empty");
        return false;
    }
    
    private static bool ValidateDescription(string description)
    {
        if (!string.IsNullOrEmpty(description)) return true;
        Console.WriteLine("Description cannot be null or empty");
        return false;
    }

    public static bool ValidateModel(ToDoItem todoItem)
    {
        return ValidateTitle(todoItem.Title) && ValidateDescription(todoItem.Description);
    }


}