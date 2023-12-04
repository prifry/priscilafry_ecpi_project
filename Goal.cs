using System;
using System.Data.SQLite;
using System.Collections.Generic;
/*********************************************************************************************************************
* Priscila Fry
* CIS317
* Date: 11/20/2023
* Project description:  This code defines an abstract class, Goal, representing a generic goal. It includes
*   properties for the title and completion status of the goal. The abstract class is
*   intended to be subclassed by specific goal types, each implementing a method to
*   display progress. The code provides a default implementation of the ToString method.
*
**********************************************************************************************************************/

// Concrete class representing a goal
public class Goal
{
    // Properties
    public int GoalId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public List<DailyTask> DailyTasks { get; set; }

    // Constructors
    public Goal(int goalId, string title, bool isCompleted)
    {
        GoalId = goalId;
        Title = title;
        IsCompleted = isCompleted;
        DailyTasks = new List<DailyTask>();
    }

    public Goal(string title, bool isCompleted = false)
    {
        Title = title;
        IsCompleted = isCompleted;
        DailyTasks = new List<DailyTask>();
    }
    // Method to display progress of the goal
    virtual public void DisplayProgress(User user)
    {
        // Provide implementation for displaying progress 
        Console.WriteLine("Displaying progress...");
    }

    // Override ToString method to provide a string representation of the goal
    public override string ToString()
    {
        return $"GoalId: {GoalId}\nGoal: {Title}\nCompleted: {IsCompleted}";
    }

    public void MarkGoalAsCompleted(User user, Goal goal)
    {
        Console.WriteLine($"\nWould you like to mark goal '{goal.Title}' as completed? (yes/no)");
        string? userInput = Console.ReadLine();

        if (userInput != null && userInput.ToLower() == "yes")
        {
            goal.IsCompleted = true;

            Console.WriteLine($"\n***Great job, {user.UserName}! Goal '{goal.Title}' marked as completed.\n");
        }
        else
        {
            Console.WriteLine($"\n###You can do it, {user.UserName}! Goal '{goal.Title}' not completed.\n");
        }
    }


}