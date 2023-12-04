/***************************************************************************************************
* Priscila Fry
* CIS317
* Date: 11/20/2023
*
* Project description: Database DailyTaskDB table with CRUD and Query.

***************************************************************************************************/

using System.Data.SQLite;

public class DailyTaskDB : GoalDB
{
    public static void CreateDailyTasksTable(SQLiteConnection conn)
    {
        string sql =
            "CREATE TABLE IF NOT EXISTS DailyTasks(\n"
            + "   TaskId INTEGER PRIMARY KEY AUTOINCREMENT,\n"
            + "   TaskType varchar(20) NOT NULL,\n"
            + "   GoalId INTEGER,\n"
            + "   Title varchar(150) NOT NULL,\n"
            + "   IsCompleted INTEGER NOT NULL DEFAULT(0),\n"
            + "   FOREIGN KEY (GoalId) REFERENCES Goals(GoalId)\n"
            + "      ON UPDATE CASCADE\n"
            + "      ON DELETE CASCADE\n);";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void AddDailyTask(SQLiteConnection conn, DailyTask task, int goalId)
    {
        if (GoalDB.GoalExists(conn, goalId))
        {
            string sql = "INSERT INTO DailyTasks (TaskType, GoalId, Title, IsCompleted) " +
                        "VALUES (@TaskType, @GoalId, @Title, @IsCompleted);";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TaskType", task.TaskType);
                cmd.Parameters.AddWithValue("@GoalId", goalId);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            Console.WriteLine($"Error: Goal with ID {goalId} does not exist.");
        }
    }

    // Update and other methods can be similarly modified.
    public static void UpdateDailyTask(SQLiteConnection conn, DailyTask task)
    {
        // Assuming that TaskId, GoalId, Title, TaskType, and IsCompleted are properties of the DailyTask class
        string updateSql = "UPDATE DailyTasks SET TaskType = @TaskType, GoalId = @GoalId, Title = @Title,  IsCompleted = @IsCompleted WHERE TaskId = @TaskId";

        using (SQLiteCommand updateCmd = new SQLiteCommand(updateSql, conn))
        {

            updateCmd.Parameters.AddWithValue("@TaskType", task.TaskType);
            updateCmd.Parameters.AddWithValue("@GoalId", task.GoalId);
            updateCmd.Parameters.AddWithValue("@Title", task.Title);
            updateCmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
            updateCmd.Parameters.AddWithValue("@TaskId", task.TaskId);

            updateCmd.ExecuteNonQuery();
        }
    }

    public static void DeleteDailyTask(SQLiteConnection conn, int taskId)
    {
        string deleteSql = "DELETE FROM DailyTasks WHERE TaskId = @TaskId";
        using (SQLiteCommand cmd = new SQLiteCommand(deleteSql, conn))
        {
            cmd.Parameters.AddWithValue("@TaskId", taskId);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<DailyTask> GetAllTasks(SQLiteConnection conn, int goalId)
    {
        List<DailyTask> tasks = new List<DailyTask>();
        string readTasksSql = "SELECT * FROM DailyTasks WHERE GoalId = @GoalId;";
        SQLiteCommand readTasksCmd = new SQLiteCommand(readTasksSql, conn);
        readTasksCmd.Parameters.AddWithValue("@GoalId", goalId);

        using (SQLiteDataReader reader = readTasksCmd.ExecuteReader())
        {
            while (reader.Read())
            {                               //taskId, string taskType, int goalId, string title, bool isCompleted
                int taskId = reader.GetInt32(0);
                string taskType = reader.GetString(1);
                int fetchedGoalId = reader.GetInt32(2);
                string title = reader.GetString(3);
                bool isCompleted = reader.GetBoolean(4);


                DailyTask task = new DailyTask(taskId, taskType, fetchedGoalId, title, isCompleted)
                {
                    IsCompleted = isCompleted
                };

                tasks.Add(task);
            }
        }

        return tasks;
    }

    public static DailyTask? GetTask(SQLiteConnection conn, int taskId)
    {
        string getTaskSql = "SELECT * FROM DailyTasks WHERE TaskId = @TaskId LIMIT 1;";
        SQLiteCommand getTaskCmd = new SQLiteCommand(getTaskSql, conn);
        getTaskCmd.Parameters.AddWithValue("@TaskId", taskId);

        using (SQLiteDataReader reader = getTaskCmd.ExecuteReader())
        {
            if (reader.Read())
            {


                string taskType = reader.GetString(1);
                int fetchedGoalId = reader.GetInt32(2);
                string title = reader.GetString(3);
                bool isCompleted = reader.GetBoolean(4);


                DailyTask task = new DailyTask(taskId, taskType, fetchedGoalId, title, isCompleted)
                {
                    IsCompleted = isCompleted
                };

                return task;
            }
        }

        // Return null if the task is not found
        return null;
    }

}