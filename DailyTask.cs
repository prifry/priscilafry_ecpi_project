using System;
using System.Data.SQLite;
using System.Collections.Generic;

/***************************************************************************************************
* Priscila Fry
* Date: 11/20/2023
*
* Project description: Represents a concrete class implementing the concret Goal class.
*   DailyTask extends Goal to model daily tasks with additional details
*   such as the type of the task. It provides methods to display progress
*   and a formatted string representation of the task.
*
***************************************************************************************************/


// Concrete class implementing Goal abstract class
public class DailyTask : Goal
{
    // Additional properties
    public int TaskId { get; set; }
    public string TaskType { get; set; }

    // Constructor that takes goalId, title, and taskType parameters and initializes the base class
    public DailyTask(int taskId, string taskType, int goalId, string title, bool isCompleted) : base(goalId, title, isCompleted)
    {
        TaskId = taskId;
        TaskType = taskType;

    }

    // Implementation of virtual method from the base class Goal to display progress
    public override void DisplayProgress(User user)
    {
        Console.WriteLine($"Progress for task '{TaskType}' of type '{Title}': {(IsCompleted ? "completed" : "in progress")}");
    }


    // Overrides the ToString() method to provide a formatted string representation of the DailyTask
    public override string ToString()
    {
        return $"TaskId: {TaskId}\n         TaskType: '{TaskType}'\n    DailyTask: '{Title}'\n  Completed: {IsCompleted}";
    }
}