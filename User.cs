using System.Text; // StringBuilder class
/*****************************************************************************************************************
* Priscila Fry
* CIS317
* Date: 11/20/2023
*
*  Project description:  Represents a user in the motivational application who can set and track goals,
*   display random motivational quotes, and mark goals as completed. The class includes*
*   properties for the username, a list of goals, and a list of motivational quotes.
*   The user can interact with goals by adding them to the list, marking them as completed,
*   and displaying a summary of completed and not completed goals.
*
*******************************************************************************************************************/

// Class representing a user with goals and motivational quotes
// Class that demonstrates composition
public class User : IMotivational
{
    // Properties
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<Goal> Goals { get; set; }
    public List<string> MotivationalQuotes { get; set; }

    // Constructor
    public User(int userId, string username) : this(username)
    {
        UserId = userId;

    }

    public User(string username)
    {
        UserName = username;
        Goals = new List<Goal>();
        //Motivational quotes list
        MotivationalQuotes = new List<string>
        {
            "Believe in yourself and all that you are. Know that there is something inside you that is greater than any obstacle.",
            "The only way to do great work is to love what you do.",
            "A little more persistence, a little more effort and what seemed hopeless failure may turn to glorious success. —Elbert Hubbard",
            "The best way to not feel hopeless is to get up and do something. Don't wait for good things to happen to you. If you go out and make some good things happen, you will fill the world with hope, you will fill yourself with hope. ― Barack Obama"
            // Add more quotes as needed
        };
    }

    // Method to display a random motivational quote
    public void DisplayMotivationalQuote()
    {
        Random random = new Random();
        int index = random.Next(MotivationalQuotes.Count);
        string randomQuote = MotivationalQuotes[index];
        Console.WriteLine($"{UserName}: {randomQuote}\n");
    }

    // Method to add a goal to the user's list
    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public override string ToString()
    {

        // StringBuilder for efficient string concatenation
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($" UserId: {UserId}\n   User: {UserName}\n    Goals: {Goals.Count}");

        // Display completed goals
        sb.AppendLine("\nCompleted Goals:");
        foreach (Goal goal in Goals)
        {
            if (goal.IsCompleted)
            {
                sb.AppendLine($"  {goal}");
            }
        }

        // Display not completed goals
        sb.AppendLine("\nNot Completed Goals:");
        foreach (Goal goal in Goals)
        {
            if (!goal.IsCompleted)
            {
                sb.AppendLine($"  {goal}");
            }
        }

        return sb.ToString();
    }
}