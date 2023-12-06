using System.Data.SQLite;
using System.Runtime.InteropServices;

/************************************************************
* Priscila Fry
* Date: 11/20/2023
* Project description: Project IMotivational
*
*  Main Application
*
****************************************************************/
public class DBExample
{
    public static void Main(string[] args)
    {
        const string dbName = "PriscilaFry.db";
        Console.WriteLine("\nPriscila Fry, Week 4 Project\n");
        SQLiteConnection conn = SQLiteDatabase.Connect(dbName);

        if (conn != null)
        {
            UserDB.CreateTable(conn);
            GoalDB.CreateGoalsTable(conn);
            DailyTaskDB.CreateDailyTasksTable(conn);

            // Create Users
            User user1 = new User("Maria");
            User user2 = new User("Gato");
            User user3 = new User("Jane");

            UserDB.AddUser(conn, user1);
            UserDB.AddUser(conn, user2);
            UserDB.AddUser(conn, user3);

            // Create Goals for each user
            Goal goal1 = new Goal(1, "PowerPoint Presentation", false);
            Goal goal2 = new Goal(2, "Learn SQLite", true);
            Goal goal3 = new Goal(3, "Florida", false);


            GoalDB.AddGoal(conn, goal1, user1.UserId);
            GoalDB.AddGoal(conn, goal2, user2.UserId);
            GoalDB.AddGoal(conn, goal3, user3.UserId);

            // Create Daily Tasks for each goal
            DailyTask task1 = new DailyTask(1, "Task 1:\n", goal1.GoalId, "Type Work\n", false);
            DailyTask task2 = new DailyTask(2, "Task 2:\n", goal2.GoalId, "Type Study:\n", true);
            DailyTask task3 = new DailyTask(3, "Task 3:\n", goal3.GoalId, "Type Vacation:\n", false);




            // Display Motivational Quotes and Progress for each user
            DisplayUserMotivationalQuoteAndProgress(user1, conn);
            DailyTaskDB.AddDailyTask(conn, task1, goal1.GoalId);
            goal1.MarkGoalAsCompleted(user1, goal1);


            DisplayUserMotivationalQuoteAndProgress(user2, conn);
            DailyTaskDB.AddDailyTask(conn, task2, goal2.GoalId);
            goal2.MarkGoalAsCompleted(user2, goal2);


            DisplayUserMotivationalQuoteAndProgress(user3, conn);
            DailyTaskDB.AddDailyTask(conn, task3, goal3.GoalId);
            goal3.MarkGoalAsCompleted(user3, goal3);

            User updatedUser = new User(1, "Priscila Fry");
            Goal updatedGoal = new Goal(1, "Go Vacation", true);
            DailyTask updateTask = new DailyTask(1, "Type 1", 1, "Personal", true);

            Console.WriteLine("\n********Updated User and Goal**************\n");

            // Update user
            UserDB.UpdateUser(conn, updatedUser);
            PrintUser(updatedUser);


            // Update goal
            GoalDB.UpdateGoal(conn, updatedGoal);
            Console.WriteLine(updatedGoal);

            //Delete a user with ID=1
            int userToDelete = 2;
            UserDB.DeleteUser(conn, userToDelete);

            //delete a goal by ID=1
            int goalToDelete = 2;
            GoalDB.DeleteGoal(conn, goalToDelete);


            Console.WriteLine("*************Database Info after update and delete Maria was deleted**************");
            // Display Users with Goals and Tasks
            UserDB.DisplayAllUsers(conn);
        }


    }

    private static void PrintGoals(List<Goal> goals)
    {
        foreach (Goal goal in goals)
        {
            PrintGoal(goal);
        }
    }
    private static void PrintGoal(Goal goal)
    {
        Console.WriteLine($"Goal {goal.GoalId}: {goal.Title}");
    }

    private static void PrintUsers(List<User> users)
    {
        foreach (User user in users)
        {
            PrintUser(user);
        }
    }

    private static void PrintUser(User user)
    {
        Console.WriteLine($"User {user.UserId}: {user.UserName}");
    }

    private static void PrintDailyTasks(List<DailyTask> tasks)
    {
        foreach (DailyTask task in tasks)
        {
            Console.WriteLine($"Task {task.TaskId} for Goal {task.GoalId}: {task.Title}");
        }
    }


    static void DisplayUserMotivationalQuoteAndProgress(User user, SQLiteConnection conn)
    {
        user.DisplayMotivationalQuote();

        foreach (Goal goal in user.Goals)
        {
            Console.WriteLine($"Goal: {goal.Title}, Completed: {goal.IsCompleted}");

            foreach (DailyTask task in goal.DailyTasks)
            {
                task.DisplayProgress(user);
            }
        }
    }
}