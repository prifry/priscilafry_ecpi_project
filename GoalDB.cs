using System.Data.SQLite;
/***************************************************************************************************
* Priscila Fry
* CIS317
* Date: 11/20/2023
*
* Project description: Database GoalDB table with CRUD and Query.

***************************************************************************************************/

public class GoalDB
{
    public static void CreateGoalsTable(SQLiteConnection conn)
    {
        string sql =
            "CREATE TABLE IF NOT EXISTS Goals(\n"
            + "   GoalId INTEGER PRIMARY KEY AUTOINCREMENT,\n"
            + "   UserId INTEGER,\n"
            + "   Title varchar(60),\n"
            + "   IsCompleted INTEGER NOT NULL DEFAULT(0),\n"
            + "   FOREIGN KEY (UserId) REFERENCES Users(UserId)\n"
            + "      ON UPDATE CASCADE\n"
            + "      ON DELETE CASCADE\n);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void AddGoal(SQLiteConnection conn, Goal goal, int userId)
    {
        string insertSql = "INSERT INTO Goals(UserId, Title, IsCompleted) VALUES (@UserId, @Title, @IsCompleted)";
        SQLiteCommand insertCmd = new SQLiteCommand(insertSql, conn);
        insertCmd.Parameters.AddWithValue("@UserId", userId);
        insertCmd.Parameters.AddWithValue("@Title", goal.Title);
        insertCmd.Parameters.AddWithValue("@IsCompleted", goal.IsCompleted ? 1 : 0);
        insertCmd.ExecuteNonQuery();
    }

    public static void UpdateGoal(SQLiteConnection conn, Goal goal)
    {
        string sql = $"UPDATE Goals SET Title = '{goal.Title}', IsCompleted = {(goal.IsCompleted ? 1 : 0)} WHERE GoalId = {goal.GoalId}";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void DeleteGoal(SQLiteConnection conn, int goalId)
    {

        string deleteSql = "DELETE FROM Goals WHERE GoalId = @GoalId";
        SQLiteCommand deleteCmd = new SQLiteCommand(deleteSql, conn);
        deleteCmd.Parameters.AddWithValue("@GoalId", goalId);
        deleteCmd.ExecuteNonQuery();
    }

    public static List<Goal> GetAllGoals(SQLiteConnection conn)
    {
        List<Goal> goals = new List<Goal>();
        string sql = "SELECT Goals.GoalId, Goals.Title, Goals.IsCompleted, Users.UserId, Users.UserName FROM Goals " +
            "LEFT JOIN Users ON Goals.UserId = Users.UserId";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {

            int goalId = rdr.GetInt32(0);
            string title = rdr.GetString(1);
            bool isCompleted = rdr.GetBoolean(2);

            Goal goal = new Goal(goalId, title, isCompleted);
            goals.Add(goal);
        }
        return goals;
    }

    public static Goal? GetGoal(SQLiteConnection conn, int goalId)
    {
        string sql = "SELECT * FROM Goals WHERE GoalId = @GoalId";
        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@GoalId", goalId);

        using SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            int retrievedGoalId = rdr.GetInt32(0);
            string title = rdr.GetString(1);
            bool isCompleted = rdr.GetBoolean(2);

            Goal goal = new Goal(retrievedGoalId, title, isCompleted);
            return goal;
        }

        // Return null if the goal is not found
        return null;
    }

    public static bool GoalExists(SQLiteConnection conn, int goalId)
    {
        string sql = "SELECT COUNT(*) FROM Goals WHERE GoalId = @GoalId;";

        using SQLiteCommand cmd = new SQLiteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@GoalId", goalId);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    public static void DisplayAllGoals(SQLiteConnection conn)
    {
        List<Goal> goals = GetAllGoals(conn);

        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal.ToString());
        }
    }

}