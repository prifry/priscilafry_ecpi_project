/*******************************************************************
*	Name: Priscila Fry
*	Date: 11/28/2023
*	Assignment: CIS317 Week 4 Project - Database Interactions
*
*	Class to handle all interactions with the Users and Goal table in the
*	database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Users and Goal table.
* Note that the interactions are all done using standard SQL syntax
*	that is then executed by the SQLite library.
 */

using System.Data.SQLite;

public class UserDB
{


    public static void CreateTable(SQLiteConnection conn)
    {
        // SQL statement for creating a User table
        string sql =
            "CREATE TABLE IF NOT EXISTS Users (\n"
            + "   UserId integer PRIMARY KEY AUTOINCREMENT\n"
            + "   ,UserName varchar(20)\n);";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();


    }

    public static void AddUser(SQLiteConnection conn, User u)
    {
        string sql = string.Format("INSERT INTO Users (UserName) "
        + "VALUES ('{0}')",
        u.UserName);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void UpdateUser(SQLiteConnection conn, User u)
    {
        string sql = $"UPDATE Users SET UserName = '{u.UserName}' WHERE UserId = {u.UserId}";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();

    }

    public static void DeleteUser(SQLiteConnection conn, int userId)
    {

        string sql = string.Format("DELETE from Users WHERE UserId={0}", userId);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();

    }

    // Read all users
    public static List<User> GetAllUsers(SQLiteConnection conn)
    {
        List<User> users = new List<User>();
        string sql = "SELECT UserId, UserName FROM Users;";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();
        {
            while (rdr.Read())
            {
                users.Add(new User(rdr.GetInt32(0), rdr.GetString(1)));
            }
        }

        return users;
    }

    public static User GetUser(SQLiteConnection conn, int id)
    {
        string sql = string.Format("SELECT * FROM Users WHERE UserId = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            int userId = rdr.GetInt32(0);
            string userName = rdr.GetString(1);
            // Return the retrieved user
            return new User(userId, userName);

        }


        // If no user found, return null
        return new User(-1, string.Empty);
    }


    public static void DisplayAllUsers(SQLiteConnection conn)
    {
        string displayUsersSql = "SELECT * FROM Users;";
        SQLiteCommand displayUsersCmd = new SQLiteCommand(displayUsersSql, conn);

        using (SQLiteDataReader reader = displayUsersCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string username = reader.GetString(1);

                // Display information about the user
                Console.WriteLine($"User ID: {userId}\n Username: {username}\n");
            }
        }
    }


}

