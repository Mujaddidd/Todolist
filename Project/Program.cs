using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=(localdb)\\local;Initial Catalog=Todolist;Integrated Security=True;";
        TaskManager taskManager = new TaskManager(connectionString);
        bool running = true;

        while (running)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Remove task");
            Console.WriteLine("3. Toggle task completion");
            Console.WriteLine("4. List tasks");
            Console.WriteLine("5. Exit");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();
                    taskManager.AddTask(description);
                    Console.WriteLine("Task added.");
                    break;
                case 2:
                    Console.Write("Enter task ID to remove: ");
                    int.TryParse(Console.ReadLine(), out int id);
                    if (taskManager.RemoveTask(id))
                        Console.WriteLine("Task removed.");
                    else
                        Console.WriteLine("Task not found.");
                    break;
                case 3:
                    Console.Write("Enter task ID to toggle completion: ");
                    int.TryParse(Console.ReadLine(), out id);
                    if (taskManager.ToggleTaskCompletion(id))
                        Console.WriteLine("Task completion toggled.");
                    else
                        Console.WriteLine("Task not found.");
                    break;
                case 4:
                    List<TaskItem> tasks = taskManager.ListTasks();
                    Console.WriteLine("\nTasks:");
                    foreach (TaskItem task in tasks)
                    {
                        Console.WriteLine($"ID: {task.Id}, Description: {task.Description}, Completed: {task.Completed}");
                    }
                    break;
                case 5:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
