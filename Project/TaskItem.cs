public class TaskItem
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public bool Completed { get; private set; }

    public TaskItem(int id, string description)
    {
        Id = id;
        Description = description;
    }

    public TaskItem(int id, string description, bool completed)
    {
        Id = id;
        Description = description;
        Completed = completed;
    }

    public void ToggleCompletion()
    {
        Completed = !Completed;
    }
}