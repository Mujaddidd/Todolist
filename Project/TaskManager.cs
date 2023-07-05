using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

public class TaskManager
{
    private string connectionString;

    public TaskManager(string connectionString)
    {
        this.connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string tableCmd = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Tasks' AND type = 'U') CREATE TABLE Tasks (Id INT PRIMARY KEY IDENTITY(1,1), Description VARCHAR(500), Completed BIT)";
            using (var command = new SqlCommand(tableCmd, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddTask(string description)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string addCmd = "INSERT INTO Tasks (Description, Completed) VALUES (@description, 0)";
            using (var command = new SqlCommand(addCmd, connection))
            {
                command.Parameters.AddWithValue("@description", description);
                command.ExecuteNonQuery();
            }
        }
    }

    public bool RemoveTask(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string removeCmd = "DELETE FROM Tasks WHERE Id = @id";
            using (var command = new SqlCommand(removeCmd, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public bool ToggleTaskCompletion(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string toggleCmd = "UPDATE Tasks SET Completed = 1 - Completed WHERE Id = @id";
            using (var command = new SqlCommand(toggleCmd, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public List<TaskItem> ListTasks()
    {
        List<TaskItem> tasks = new List<TaskItem>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string listCmd = "SELECT * FROM Tasks";
            using (var command = new SqlCommand(listCmd, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                    }
                }
            }
        }
        return tasks;
    }
}
